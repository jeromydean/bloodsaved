using BloodSaved.Parsing.Models;

namespace BloodSaved.Parsing.Sections
{
  public class PcInfoRecord
  {
    private static readonly string[] CanonicalPropertyOrder =
    [
      SaveConstants.m_ContinuousJumpKickCount,
      SaveConstants.m_TotalMoveDistance,
      SaveConstants.m_TotalBackStepMoveDistance,
      SaveConstants.m_ArtsUseNum,
      SaveConstants.m_ArtsExperience,
      SaveConstants.m_BloodSteeleAmount,
    ];

    private readonly List<SaveSection> _saveSections = new();

    public int ContinuousJumpKickCount { get; set; }

    public float TotalMoveDistance { get; set; }

    public float TotalBackStepMoveDistance { get; set; }

    public int[] ArtsUseNum { get; set; } = new int[TechniqueConstants.ArtsSlotCount];

    public int[] ArtsExperience { get; set; } = new int[TechniqueConstants.ArtsSlotCount];

    public float BloodSteeleAmount { get; set; }

    public Guid StructId { get; private set; }

    internal static PcInfoRecord Deserialize(SaveReader saveReader)
    {
      PcInfoRecord pcInfo = new PcInfoRecord();
      saveReader.ReadStructProperty(SaveConstants.PCInfo, out _, out int structLength, out Guid structId);
      pcInfo.StructId = structId;

      long structEnd = saveReader.CurrentPosition + structLength;
      while (saveReader.CurrentPosition < structEnd)
      {
        saveReader.SetCheckpoint();
        string name = saveReader.ReadLengthPrefixedString();
        if (string.Equals(name, SaveConstants.None))
        {
          break;
        }

        string type = saveReader.ReadLengthPrefixedString();
        saveReader.Reset();

        switch (type)
        {
          case SaveConstants.IntProperty when string.Equals(name, SaveConstants.m_ContinuousJumpKickCount, StringComparison.Ordinal):
            pcInfo.ContinuousJumpKickCount = saveReader.ReadIntProperty(name);
            break;
          case SaveConstants.FloatProperty when string.Equals(name, SaveConstants.m_TotalMoveDistance, StringComparison.Ordinal):
            pcInfo.TotalMoveDistance = saveReader.ReadFloatProperty(name);
            break;
          case SaveConstants.FloatProperty when string.Equals(name, SaveConstants.m_TotalBackStepMoveDistance, StringComparison.Ordinal):
            pcInfo.TotalBackStepMoveDistance = saveReader.ReadFloatProperty(name);
            break;
          case SaveConstants.FloatProperty when string.Equals(name, SaveConstants.m_BloodSteeleAmount, StringComparison.Ordinal):
            pcInfo.BloodSteeleAmount = saveReader.ReadFloatProperty(name);
            break;
          case SaveConstants.ArrayProperty when string.Equals(name, SaveConstants.m_ArtsUseNum, StringComparison.Ordinal):
            saveReader.ReadArrayProperty(name, out _, out _, out int artsUseNumCount);
            pcInfo.ArtsUseNum = ReadInt32Array(saveReader, artsUseNumCount, TechniqueConstants.ArtsSlotCount);
            break;
          case SaveConstants.ArrayProperty when string.Equals(name, SaveConstants.m_ArtsExperience, StringComparison.Ordinal):
            saveReader.ReadArrayProperty(name, out _, out _, out int artsExperienceCount);
            pcInfo.ArtsExperience = ReadInt32Array(saveReader, artsExperienceCount, TechniqueConstants.ArtsSlotCount);
            break;
          case SaveConstants.IntProperty:
            saveReader.ReadIntProperty(name);
            break;
          case SaveConstants.FloatProperty:
            saveReader.ReadFloatProperty(name);
            break;
          case SaveConstants.BoolProperty:
            saveReader.ReadBoolProperty(name);
            break;
          case SaveConstants.NameProperty:
            saveReader.ReadNameProperty(name);
            break;
          case SaveConstants.ArrayProperty:
            saveReader.ReadArrayProperty(name, out _, out int arrayPropertyLength, out _);
            saveReader.Skip(arrayPropertyLength - 4);
            break;
          case SaveConstants.MapProperty:
            saveReader.ReadMapProperty(name, out _, out _, out int mapPropertyLength, out _);
            saveReader.Skip(mapPropertyLength - 8);
            break;
          case SaveConstants.StructProperty:
            saveReader.ReadStructProperty(name, out _, out int innerStructLength, out _);
            saveReader.Skip(innerStructLength);
            break;
          default:
            throw new NotImplementedException($"Unhandled PCInfo property '{name}' ({type}).");
        }

        pcInfo._saveSections.Add(new SaveSection
        {
          Name = name,
          Type = type,
          StartOffset = saveReader.Checkpoint,
          EndOffset = saveReader.CurrentPosition,
          Data = saveReader.CloneFromCheckpoint()
        });
      }

      return pcInfo;
    }

    public byte[] Serialize()
    {
      using SaveWriter saveWriter = new SaveWriter();
      saveWriter.WriteStructProperty(SaveConstants.PCInfo, SaveConstants.PBPCRecord, StructId, out long structLengthOffset);
      saveWriter.SetCheckpoint();

      HashSet<string> presentInSave = _saveSections
        .Select(section => section.Name)
        .ToHashSet(StringComparer.Ordinal);
      HashSet<string> synthesizedWritten = new(StringComparer.Ordinal);

      foreach (SaveSection section in _saveSections)
      {
        WriteSynthesizedPropertiesBefore(saveWriter, section.Name, presentInSave, synthesizedWritten);

        if (TryWriteKnownProperty(saveWriter, section))
        {
          continue;
        }

        saveWriter.Write(section.Data);
      }

      WriteSynthesizedPropertiesBefore(saveWriter, nextSectionName: null, presentInSave, synthesizedWritten);

      saveWriter.WriteLengthPrefixedString(SaveConstants.None);

      int structDataLength = (int)(saveWriter.CurrentPosition - saveWriter.Checkpoint);
      saveWriter.SetPosition(structLengthOffset);
      saveWriter.Write(structDataLength);

      return saveWriter.ToArray();
    }

    private void WriteSynthesizedPropertiesBefore(
      SaveWriter saveWriter,
      string? nextSectionName,
      HashSet<string> presentInSave,
      HashSet<string> synthesizedWritten)
    {
      int nextOrder = GetCanonicalOrder(nextSectionName);

      foreach (string propertyName in CanonicalPropertyOrder)
      {
        if (GetCanonicalOrder(propertyName) >= nextOrder)
        {
          continue;
        }

        if (presentInSave.Contains(propertyName) || !synthesizedWritten.Add(propertyName))
        {
          continue;
        }

        if (!ShouldSynthesizeProperty(propertyName))
        {
          synthesizedWritten.Remove(propertyName);
          continue;
        }

        WriteKnownProperty(saveWriter, propertyName);
      }
    }

    private static int GetCanonicalOrder(string? propertyName)
    {
      if (propertyName == null)
      {
        return int.MaxValue;
      }

      for (int i = 0; i < CanonicalPropertyOrder.Length; i++)
      {
        if (string.Equals(CanonicalPropertyOrder[i], propertyName, StringComparison.Ordinal))
        {
          return i;
        }
      }

      return int.MaxValue;
    }

    private bool ShouldSynthesizeArtsArrays()
    {
      return ArtsUseNum.Any(value => value != 0) || ArtsExperience.Any(value => value != 0);
    }

    private bool ShouldSynthesizeProperty(string propertyName)
    {
      return propertyName switch
      {
        SaveConstants.m_ContinuousJumpKickCount => ContinuousJumpKickCount > 0,
        SaveConstants.m_TotalMoveDistance => TotalMoveDistance != 0f,
        SaveConstants.m_TotalBackStepMoveDistance => TotalBackStepMoveDistance != 0f,
        SaveConstants.m_ArtsUseNum => ShouldSynthesizeArtsArrays(),
        SaveConstants.m_ArtsExperience => ShouldSynthesizeArtsArrays(),
        SaveConstants.m_BloodSteeleAmount => BloodSteeleAmount != 0f,
        _ => false,
      };
    }

    private bool TryWriteKnownProperty(SaveWriter saveWriter, SaveSection section)
    {
      if (!IsKnownProperty(section.Name))
      {
        return false;
      }

      WriteKnownProperty(saveWriter, section.Name);
      return true;
    }

    private static bool IsKnownProperty(string propertyName)
    {
      return string.Equals(propertyName, SaveConstants.m_ContinuousJumpKickCount, StringComparison.Ordinal)
        || string.Equals(propertyName, SaveConstants.m_TotalMoveDistance, StringComparison.Ordinal)
        || string.Equals(propertyName, SaveConstants.m_TotalBackStepMoveDistance, StringComparison.Ordinal)
        || string.Equals(propertyName, SaveConstants.m_ArtsUseNum, StringComparison.Ordinal)
        || string.Equals(propertyName, SaveConstants.m_ArtsExperience, StringComparison.Ordinal)
        || string.Equals(propertyName, SaveConstants.m_BloodSteeleAmount, StringComparison.Ordinal);
    }

    private void WriteKnownProperty(SaveWriter saveWriter, string propertyName)
    {
      switch (propertyName)
      {
        case SaveConstants.m_ContinuousJumpKickCount:
          saveWriter.WriteIntProperty(SaveConstants.m_ContinuousJumpKickCount, ContinuousJumpKickCount);
          break;
        case SaveConstants.m_TotalMoveDistance:
          saveWriter.WriteFloatProperty(SaveConstants.m_TotalMoveDistance, TotalMoveDistance);
          break;
        case SaveConstants.m_TotalBackStepMoveDistance:
          saveWriter.WriteFloatProperty(SaveConstants.m_TotalBackStepMoveDistance, TotalBackStepMoveDistance);
          break;
        case SaveConstants.m_ArtsUseNum:
          WriteInt32ArrayProperty(saveWriter, SaveConstants.m_ArtsUseNum, ArtsUseNum);
          break;
        case SaveConstants.m_ArtsExperience:
          WriteInt32ArrayProperty(saveWriter, SaveConstants.m_ArtsExperience, ArtsExperience);
          break;
        case SaveConstants.m_BloodSteeleAmount:
          saveWriter.WriteFloatProperty(SaveConstants.m_BloodSteeleAmount, BloodSteeleAmount);
          break;
      }
    }

    private static int[] ReadInt32Array(SaveReader saveReader, int count, int expectedCount)
    {
      if (count != expectedCount)
      {
        throw new InvalidDataException($"Expected {expectedCount} array entries but read {count}.");
      }

      int[] values = new int[count];
      for (int i = 0; i < count; i++)
      {
        values[i] = saveReader.ReadInt32();
      }

      return values;
    }

    private static void WriteInt32ArrayProperty(SaveWriter saveWriter, string propertyName, int[] values)
    {
      saveWriter.WriteArrayProperty(propertyName, SaveConstants.IntProperty, out long lengthOffset, out _, count: values.Length);
      long payloadStart = saveWriter.CurrentPosition - 4;

      foreach (int value in values)
      {
        saveWriter.Write(value);
      }

      int payloadLength = (int)(saveWriter.CurrentPosition - payloadStart);
      long resumePosition = saveWriter.CurrentPosition;
      saveWriter.SetPosition(lengthOffset);
      saveWriter.Write(payloadLength);
      saveWriter.SetPosition(resumePosition);
    }
  }
}
