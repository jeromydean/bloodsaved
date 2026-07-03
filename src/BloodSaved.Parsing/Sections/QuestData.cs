using BloodSaved.Parsing.Models;

namespace BloodSaved.Parsing.Sections
{
  public class QuestData : ISerializableSection<QuestData>
  {
    private const string AcceptedPropertyName = "Accepted";
    private const string DonePropertyName = "Done";
    private const string AcceptedStructName = "PBAcceptedQuestData";

    public static readonly IReadOnlyList<string> AllQuestIds =
    [
      "Quest_Enemy01",
      "Quest_Memento01",
      "Quest_Memento02",
      "Quest_Enemy02",
      "Quest_Catering01",
      "Quest_Enemy03",
      "Quest_Enemy04",
      "Quest_Enemy05",
      "Quest_Memento03",
      "Quest_Memento04",
      "Quest_Catering17",
      "Quest_StrayMan01",
      "Quest_Enemy06",
      "Quest_Memento05",
      "Quest_Catering14",
      "Quest_Catering08",
      "Quest_Catering06",
      "Quest_Enemy08",
      "Quest_Enemy07",
      "Quest_Memento06",
      "Quest_Memento07",
      "Quest_Memento08",
      "Quest_Memento09",
      "Quest_Catering11",
      "Quest_Enemy09",
      "Quest_Memento10",
      "Quest_Enemy11",
      "Quest_Enemy10",
      "Quest_Enemy12",
      "Quest_Enemy13",
      "Quest_Enemy14",
      "Quest_Memento11",
      "Quest_Memento12",
      "Quest_Memento13",
      "Quest_Enemy15",
      "Quest_StrayMan02",
      "Quest_Catering05",
      "Quest_Catering04",
      "Quest_Catering19",
      "Quest_Catering07",
      "Quest_Catering10",
      "Quest_Catering13",
      "Quest_Catering16",
      "Quest_Catering20",
      "Quest_Catering12",
      "Quest_Catering02",
      "Quest_Catering03",
      "Quest_Enemy16",
      "Quest_StrayMan03",
      "Quest_Enemy17",
      "Quest_Catering09",
      "Quest_Catering15",
      "Quest_Catering18",
      "Quest_Catering21",
      "Quest_Memento14",
      "Quest_Memento15",
      "Quest_Enemy18",
      "Quest_Enemy19",
      "Quest_Enemy20",
    ];

    public List<AcceptedQuest> Accepted { get; } = [];
    public List<string> Done { get; } = [];
    public bool IsDirty { get; private set; }

    public static QuestData Deserialize(SaveSection saveSection)
    {
      QuestData questData = new QuestData();
      using SaveReader saveReader = new SaveReader(saveSection.Data);
      saveReader.ReadArrayProperty(SaveConstants.QuestData, out string arrayType, out int questDataLength, out _);
      if (!string.Equals(arrayType, SaveConstants.ByteProperty, StringComparison.Ordinal))
      {
        throw new InvalidDataException($"QuestData array type of '{arrayType}' is not correct, expected ByteProperty.");
      }

      long questDataEnd = saveReader.CurrentPosition - 4 + questDataLength;
      while (saveReader.CurrentPosition < questDataEnd)
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

        if (string.Equals(name, AcceptedPropertyName, StringComparison.Ordinal)
          && string.Equals(type, SaveConstants.ArrayProperty, StringComparison.Ordinal))
        {
          questData.Accepted.AddRange(ReadAcceptedArray(saveReader));
        }
        else if (string.Equals(name, DonePropertyName, StringComparison.Ordinal)
          && string.Equals(type, SaveConstants.ArrayProperty, StringComparison.Ordinal))
        {
          questData.Done.AddRange(ReadNameArrayProperty(saveReader, DonePropertyName));
        }
        else
        {
          SkipProperty(saveReader, name, type);
        }
      }

      return questData;
    }

    public void SetAllQuestsDone()
    {
      SetCompletedQuests(AllQuestIds);
    }

    public void SetCompletedQuests(IEnumerable<string> questIds)
    {
      HashSet<string> completed = questIds.ToHashSet(StringComparer.Ordinal);

      Accepted.RemoveAll(quest => completed.Contains(quest.QuestId));
      Done.Clear();
      Done.AddRange(AllQuestIds.Where(completed.Contains));
      IsDirty = true;
    }

    public byte[] Serialize()
    {
      using SaveWriter saveWriter = new SaveWriter();
      saveWriter.WriteArrayProperty(SaveConstants.QuestData, SaveConstants.ByteProperty, out long questDataLengthOffset, out long questDataCountOffset);

      if (Accepted.Count > 0)
      {
        WriteAcceptedArray(saveWriter);
      }

      WriteNameArrayProperty(saveWriter, DonePropertyName, Done);

      saveWriter.WriteLengthPrefixedString(SaveConstants.None);
      saveWriter.Write(0);

      PatchByteArrayLength(saveWriter, questDataLengthOffset, questDataCountOffset);
      return saveWriter.ToArray();
    }

    private static IReadOnlyList<AcceptedQuest> ReadAcceptedArray(SaveReader saveReader)
    {
      List<AcceptedQuest> accepted = [];
      saveReader.ReadArrayProperty(AcceptedPropertyName, out string arrayType, out _, out int count);
      if (!string.Equals(arrayType, SaveConstants.StructProperty, StringComparison.Ordinal))
      {
        throw new InvalidDataException($"Accepted quest array type of '{arrayType}' is not correct, expected StructProperty.");
      }

      for (int i = 0; i < count; i++)
      {
        saveReader.SetCheckpoint();
        string nextName = saveReader.ReadLengthPrefixedString();
        saveReader.Reset();
        if (!string.Equals(nextName, AcceptedPropertyName, StringComparison.Ordinal))
        {
          break;
        }

        saveReader.ReadStructProperty(AcceptedPropertyName, out string structName, out int structLength, out _);
        if (!string.Equals(structName, AcceptedStructName, StringComparison.Ordinal))
        {
          throw new InvalidDataException($"Accepted quest struct type of '{structName}' is not correct, expected {AcceptedStructName}.");
        }

        long structEnd = saveReader.CurrentPosition + structLength;
        AcceptedQuest quest = new AcceptedQuest();
        while (saveReader.CurrentPosition < structEnd)
        {
          saveReader.SetCheckpoint();
          string name = saveReader.ReadLengthPrefixedString();
          if (string.Equals(name, SaveConstants.None, StringComparison.Ordinal))
          {
            break;
          }

          string type = saveReader.ReadLengthPrefixedString();
          saveReader.Reset();

          if (string.Equals(name, "QuestID", StringComparison.Ordinal)
            && string.Equals(type, SaveConstants.NameProperty, StringComparison.Ordinal))
          {
            quest.QuestId = saveReader.ReadNameProperty("QuestID");
          }
          else if (string.Equals(name, "EnemyID01", StringComparison.Ordinal)
            && string.Equals(type, SaveConstants.NameProperty, StringComparison.Ordinal))
          {
            quest.EnemyId01 = saveReader.ReadNameProperty("EnemyID01");
          }
          else if (string.Equals(name, "NowKilling", StringComparison.Ordinal)
            && string.Equals(type, SaveConstants.IntProperty, StringComparison.Ordinal))
          {
            quest.NowKilling = saveReader.ReadIntProperty("NowKilling");
          }
          else if (string.Equals(name, "isNew", StringComparison.Ordinal)
            && string.Equals(type, SaveConstants.BoolProperty, StringComparison.Ordinal))
          {
            quest.IsNew = saveReader.ReadBoolProperty("isNew");
          }
          else if (string.Equals(name, "EnemyLocations", StringComparison.Ordinal)
            && string.Equals(type, SaveConstants.ArrayProperty, StringComparison.Ordinal))
          {
            quest.EnemyLocations.AddRange(ReadNameArrayProperty(saveReader, "EnemyLocations"));
          }
          else
          {
            SkipProperty(saveReader, name, type);
          }
        }

        saveReader.BaseStream.Position = structEnd;
        accepted.Add(quest);
      }

      return accepted;
    }

    private void WriteAcceptedArray(SaveWriter saveWriter)
    {
      saveWriter.WriteArrayProperty(AcceptedPropertyName, SaveConstants.StructProperty, out long lengthOffset, out long countOffset, count: Accepted.Count);

      foreach (AcceptedQuest quest in Accepted)
      {
        saveWriter.WriteStructProperty(AcceptedPropertyName, AcceptedStructName, Guid.Empty, out long structLengthOffset);
        long structStart = saveWriter.CurrentPosition;

        saveWriter.WriteNameProperty("QuestID", quest.QuestId);
        saveWriter.WriteNameProperty("EnemyID01", quest.EnemyId01);
        saveWriter.WriteIntProperty("NowKilling", quest.NowKilling);
        saveWriter.WriteBoolProperty("isNew", quest.IsNew);
        WriteNameArrayProperty(saveWriter, "EnemyLocations", quest.EnemyLocations);
        saveWriter.WriteLengthPrefixedString(SaveConstants.None);

        long resumePosition = saveWriter.CurrentPosition;
        saveWriter.SetPosition(structLengthOffset);
        saveWriter.Write((int)(resumePosition - structStart));
        saveWriter.SetPosition(resumePosition);
      }

      PatchArrayLengthOnly(saveWriter, lengthOffset, countOffset);
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
      int length = (int)(saveWriter.CurrentPosition - countOffset);
      saveWriter.SetPosition(lengthOffset);
      saveWriter.Write(length);
      saveWriter.SetPosition(countOffset);
      saveWriter.Write(length - 4);
    }

    private static void SkipProperty(SaveReader saveReader, string name, string type)
    {
      switch (type)
      {
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
        case SaveConstants.FloatProperty:
          saveReader.ReadFloatProperty(name);
          break;
        case SaveConstants.MapProperty:
          saveReader.ReadMapProperty(name, out _, out _, out int mapLength, out _);
          saveReader.Skip(mapLength - 8);
          break;
        case SaveConstants.ArrayProperty:
          saveReader.ReadArrayProperty(name, out _, out int arrayLength, out _);
          saveReader.Skip(arrayLength - 4);
          break;
        case SaveConstants.IntProperty:
          saveReader.ReadIntProperty(name);
          break;
        default:
          throw new InvalidDataException($"Unknown QuestData property type '{type}'.");
      }
    }
  }

  public class AcceptedQuest
  {
    public string QuestId { get; set; } = SaveConstants.None;
    public string EnemyId01 { get; set; } = SaveConstants.None;
    public int NowKilling { get; set; }
    public bool IsNew { get; set; }
    public List<string> EnemyLocations { get; } = [];
  }
}
