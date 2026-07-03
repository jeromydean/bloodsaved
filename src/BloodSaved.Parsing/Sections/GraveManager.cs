using BloodSaved.Parsing.Models;

namespace BloodSaved.Parsing.Sections
{
  public class GraveManager : ISerializableSection<GraveManager>
  {
    private const string GravesPropertyName = "Graves";

    public static readonly IReadOnlyList<string> AllMementoQuestIds =
    [
      "Quest_Memento01",
      "Quest_Memento02",
      "Quest_Memento03",
      "Quest_Memento04",
      "Quest_Memento05",
      "Quest_Memento06",
      "Quest_Memento07",
      "Quest_Memento08",
      "Quest_Memento09",
      "Quest_Memento10",
      "Quest_Memento11",
      "Quest_Memento12",
      "Quest_Memento13",
    ];

    public List<string> Graves { get; } = [];
    public bool IsDirty { get; private set; }

    public static GraveManager Deserialize(SaveSection saveSection)
    {
      GraveManager graveManager = new GraveManager();
      using SaveReader saveReader = new SaveReader(saveSection.Data);
      saveReader.ReadArrayProperty(SaveConstants.GraveManager, out string arrayType, out int length, out _);
      if (!string.Equals(arrayType, SaveConstants.ByteProperty, StringComparison.Ordinal))
      {
        throw new InvalidDataException($"GraveManager array type of '{arrayType}' is not correct, expected ByteProperty.");
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

        if (string.Equals(name, GravesPropertyName, StringComparison.Ordinal)
          && string.Equals(type, SaveConstants.ArrayProperty, StringComparison.Ordinal))
        {
          graveManager.Graves.AddRange(ReadNameArrayProperty(saveReader, GravesPropertyName));
        }
        else
        {
          SkipProperty(saveReader, name, type);
        }
      }

      return graveManager;
    }

    public void SetAllMementosCompleted()
    {
      SetCompletedMementos(AllMementoQuestIds);
    }

    public void SetCompletedMementos(IEnumerable<string> questIds)
    {
      HashSet<string> completed = questIds.ToHashSet(StringComparer.Ordinal);
      Graves.Clear();
      Graves.AddRange(AllMementoQuestIds.Where(completed.Contains));
      IsDirty = true;
    }

    public byte[] Serialize()
    {
      using SaveWriter saveWriter = new SaveWriter();
      saveWriter.WriteArrayProperty(SaveConstants.GraveManager, SaveConstants.ByteProperty, out long lengthOffset, out long countOffset);

      WriteNameArrayProperty(saveWriter, GravesPropertyName, Graves);
      saveWriter.WriteLengthPrefixedString(SaveConstants.None);
      saveWriter.Write(0);

      int length = (int)(saveWriter.CurrentPosition - countOffset);
      saveWriter.SetPosition(lengthOffset);
      saveWriter.Write(length);
      saveWriter.SetPosition(countOffset);
      saveWriter.Write(length - 4);

      return saveWriter.ToArray();
    }

    private static IReadOnlyList<string> ReadNameArrayProperty(SaveReader saveReader, string propertyName)
    {
      List<string> values = [];
      saveReader.ReadArrayProperty(propertyName, out string arrayType, out int arrayLength, out int count);
      if (!string.Equals(arrayType, SaveConstants.NameProperty, StringComparison.Ordinal))
      {
        throw new InvalidDataException($"{propertyName} array type of '{arrayType}' is not correct, expected NameProperty.");
      }

      long arrayEnd = saveReader.CurrentPosition - 4 + arrayLength;
      for (int i = 0; i < count; i++)
      {
        values.Add(saveReader.ReadLengthPrefixedString());
      }

      saveReader.BaseStream.Position = arrayEnd;
      return values;
    }

    private static void WriteNameArrayProperty(SaveWriter saveWriter, string propertyName, IReadOnlyCollection<string> values)
    {
      saveWriter.WriteArrayProperty(propertyName, SaveConstants.NameProperty, out long lengthOffset, out long countOffset, count: values.Count);
      foreach (string value in values)
      {
        saveWriter.WriteLengthPrefixedString(value);
      }

      long resumePosition = saveWriter.CurrentPosition;
      saveWriter.SetPosition(lengthOffset);
      saveWriter.Write((int)(resumePosition - countOffset));
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
          throw new InvalidDataException($"Unknown GraveManager property type '{type}'.");
      }
    }
  }
}
