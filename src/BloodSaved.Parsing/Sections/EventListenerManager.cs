using BloodSaved.Parsing.Models;

namespace BloodSaved.Parsing.Sections
{
  public class EventListenerManager : ISerializableSection<EventListenerManager>
  {
    private const string FlagArrayPropertyName = "m_flagArray";
    private const string IntArrayPropertyName = "m_intArray";
    private const int EventArrayLength = 256;

    public bool[] FlagArray { get; private set; } = new bool[EventArrayLength];
    public int[] IntArray { get; private set; } = new int[EventArrayLength];
    public bool IsDirty { get; private set; }

    public static EventListenerManager Deserialize(SaveSection saveSection)
    {
      EventListenerManager eventListenerManager = new EventListenerManager();
      using SaveReader saveReader = new SaveReader(saveSection.Data);
      saveReader.ReadArrayProperty(SaveConstants.EventListenerManager, out string arrayType, out int length, out _);
      if (!string.Equals(arrayType, SaveConstants.ByteProperty, StringComparison.Ordinal))
      {
        throw new InvalidDataException($"EventListenerManager array type of '{arrayType}' is not correct, expected ByteProperty.");
      }

      long sectionEnd = saveReader.CurrentPosition - 4 + length;
      while (saveReader.CurrentPosition < sectionEnd)
      {
        saveReader.SetCheckpoint();
        string name = saveReader.ReadLengthPrefixedString();
        if (string.Equals(name, SaveConstants.None, StringComparison.Ordinal))
        {
          saveReader.VerifyNullPadBytes(4);
          break;
        }

        string type = saveReader.ReadLengthPrefixedString();
        saveReader.Reset();

        if (string.Equals(name, FlagArrayPropertyName, StringComparison.Ordinal)
          && string.Equals(type, SaveConstants.ArrayProperty, StringComparison.Ordinal))
        {
          eventListenerManager.FlagArray = ReadBoolArray(saveReader, FlagArrayPropertyName);
        }
        else if (string.Equals(name, IntArrayPropertyName, StringComparison.Ordinal)
          && string.Equals(type, SaveConstants.ArrayProperty, StringComparison.Ordinal))
        {
          eventListenerManager.IntArray = ReadIntArray(saveReader, IntArrayPropertyName);
        }
        else
        {
          SkipProperty(saveReader, name, type);
        }
      }

      return eventListenerManager;
    }

    public void EnsureQuestCompletionState()
    {
      EnsureArrayLengths();

      // These are the EventListenerManager values present in the calibrated
      // almost-all-quests-complete save.
      FlagArray[1] = true;
      IntArray[10] = 1;
      IntArray[13] = 1;
      IntArray[21] = 1;
      IsDirty = true;
    }

    public byte[] Serialize()
    {
      EnsureArrayLengths();

      using SaveWriter saveWriter = new SaveWriter();
      saveWriter.WriteArrayProperty(SaveConstants.EventListenerManager, SaveConstants.ByteProperty, out long lengthOffset, out long countOffset);

      WriteBoolArray(saveWriter, FlagArrayPropertyName, FlagArray);
      WriteIntArray(saveWriter, IntArrayPropertyName, IntArray);
      saveWriter.WriteLengthPrefixedString(SaveConstants.None);
      saveWriter.Write(0);

      PatchByteArrayLength(saveWriter, lengthOffset, countOffset);
      return saveWriter.ToArray();
    }

    private void EnsureArrayLengths()
    {
      if (FlagArray.Length != EventArrayLength)
      {
        bool[] values = new bool[EventArrayLength];
        Array.Copy(FlagArray, values, Math.Min(FlagArray.Length, values.Length));
        FlagArray = values;
      }

      if (IntArray.Length != EventArrayLength)
      {
        int[] values = new int[EventArrayLength];
        Array.Copy(IntArray, values, Math.Min(IntArray.Length, values.Length));
        IntArray = values;
      }
    }

    private static bool[] ReadBoolArray(SaveReader saveReader, string propertyName)
    {
      saveReader.ReadArrayProperty(propertyName, out string arrayType, out _, out int count);
      if (!string.Equals(arrayType, SaveConstants.BoolProperty, StringComparison.Ordinal))
      {
        throw new InvalidDataException($"{propertyName} array type of '{arrayType}' is not correct, expected BoolProperty.");
      }

      bool[] values = new bool[count];
      for (int i = 0; i < count; i++)
      {
        values[i] = saveReader.ReadByte() != 0;
      }

      return values;
    }

    private static int[] ReadIntArray(SaveReader saveReader, string propertyName)
    {
      saveReader.ReadArrayProperty(propertyName, out string arrayType, out _, out int count);
      if (!string.Equals(arrayType, SaveConstants.IntProperty, StringComparison.Ordinal))
      {
        throw new InvalidDataException($"{propertyName} array type of '{arrayType}' is not correct, expected IntProperty.");
      }

      int[] values = new int[count];
      for (int i = 0; i < count; i++)
      {
        values[i] = saveReader.ReadInt32();
      }

      return values;
    }

    private static void WriteBoolArray(SaveWriter saveWriter, string propertyName, bool[] values)
    {
      saveWriter.WriteArrayProperty(propertyName, SaveConstants.BoolProperty, out long lengthOffset, out long countOffset, count: values.Length);
      foreach (bool value in values)
      {
        saveWriter.Write(value ? (byte)1 : (byte)0);
      }

      PatchArrayLengthOnly(saveWriter, lengthOffset, countOffset);
    }

    private static void WriteIntArray(SaveWriter saveWriter, string propertyName, int[] values)
    {
      saveWriter.WriteArrayProperty(propertyName, SaveConstants.IntProperty, out long lengthOffset, out long countOffset, count: values.Length);
      foreach (int value in values)
      {
        saveWriter.Write(value);
      }

      PatchArrayLengthOnly(saveWriter, lengthOffset, countOffset);
    }

    private static void PatchArrayLengthOnly(SaveWriter saveWriter, long lengthOffset, long countOffset)
    {
      long resumePosition = saveWriter.CurrentPosition;
      saveWriter.SetPosition(lengthOffset);
      saveWriter.Write((int)(resumePosition - countOffset));
      saveWriter.SetPosition(resumePosition);
    }

    private static void PatchByteArrayLength(SaveWriter saveWriter, long lengthOffset, long countOffset)
    {
      long resumePosition = saveWriter.CurrentPosition;
      saveWriter.SetPosition(lengthOffset);
      saveWriter.Write((int)(resumePosition - countOffset));
      saveWriter.SetPosition(countOffset);
      saveWriter.Write((int)(resumePosition - countOffset - 4));
      saveWriter.SetPosition(resumePosition);
    }

    private static void SkipProperty(SaveReader saveReader, string name, string type)
    {
      switch (type)
      {
        case SaveConstants.ArrayProperty:
          saveReader.ReadArrayProperty(name, out _, out int arrayLength, out _);
          saveReader.Skip(arrayLength - 4);
          break;
        case SaveConstants.StructProperty:
          saveReader.ReadStructProperty(name, out _, out int structLength, out _);
          saveReader.Skip(structLength);
          break;
        case SaveConstants.BoolProperty:
          saveReader.ReadBoolProperty(name);
          break;
        case SaveConstants.NameProperty:
          saveReader.ReadNameProperty(name);
          break;
        case SaveConstants.IntProperty:
          saveReader.ReadIntProperty(name);
          break;
        default:
          throw new InvalidDataException($"Unknown EventListenerManager property type '{type}'.");
      }
    }
  }
}
