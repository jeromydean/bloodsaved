using BloodSaved.Parsing.Models;

namespace BloodSaved.Parsing.Sections
{
  public class ScenarioFlags : ISerializableSection<ScenarioFlags>
  {
    public const int ScenarioFlagCount = 11264;

    public bool[] Flags { get; private set; } = new bool[ScenarioFlagCount];
    public bool IsDirty { get; private set; }

    public static ScenarioFlags Deserialize(SaveSection saveSection)
    {
      ScenarioFlags scenarioFlags = new ScenarioFlags();
      using SaveReader saveReader = new SaveReader(saveSection.Data);
      saveReader.ReadArrayProperty(SaveConstants.ScenarioFlags, out string arrayType, out _, out int count);
      if (!string.Equals(arrayType, SaveConstants.BoolProperty, StringComparison.Ordinal))
      {
        throw new InvalidDataException($"ScenarioFlags array type of '{arrayType}' is not correct, expected BoolProperty.");
      }

      scenarioFlags.Flags = new bool[count];
      for (int i = 0; i < count; i++)
      {
        scenarioFlags.Flags[i] = saveReader.ReadByte() != 0;
      }

      return scenarioFlags;
    }

    public void UnlockAllQuestArchiveEntries()
    {
      UnlockQuestArchiveEntries(QuestData.AllQuestIds);
    }

    public void UnlockQuestArchiveEntries(IEnumerable<string> questIds)
    {
      EnsureFlagCount();
      foreach (int flagIndex in questIds.Select(QuestArchiveFlagIndexes.GetQuestArchiveFlagIndex))
      {
        Flags[flagIndex] = true;
      }

      IsDirty = true;
    }

    public byte[] Serialize()
    {
      EnsureFlagCount();

      using SaveWriter saveWriter = new SaveWriter();
      saveWriter.WriteArrayProperty(SaveConstants.ScenarioFlags, SaveConstants.BoolProperty, out long lengthOffset, out _, count: Flags.Length);
      long payloadStart = saveWriter.CurrentPosition - 4;

      foreach (bool flag in Flags)
      {
        saveWriter.Write(flag ? (byte)1 : (byte)0);
      }

      long resumePosition = saveWriter.CurrentPosition;
      saveWriter.SetPosition(lengthOffset);
      saveWriter.Write((int)(resumePosition - payloadStart));
      saveWriter.SetPosition(resumePosition);

      return saveWriter.ToArray();
    }

    private void EnsureFlagCount()
    {
      if (Flags.Length == ScenarioFlagCount)
      {
        return;
      }

      bool[] values = new bool[ScenarioFlagCount];
      Array.Copy(Flags, values, Math.Min(Flags.Length, values.Length));
      Flags = values;
    }
  }

  public static class QuestArchiveFlagIndexes
  {
    public static readonly IReadOnlyList<int> All = QuestData.AllQuestIds
      .Select(GetQuestArchiveFlagIndex)
      .ToArray();

    public static int GetQuestArchiveFlagIndex(string questId)
    {
      int number = int.Parse(questId[^2..]);
      if (questId.StartsWith("Quest_Enemy", StringComparison.Ordinal))
      {
        return 6235 + number;
      }

      if (questId.StartsWith("Quest_Memento", StringComparison.Ordinal))
      {
        return 6255 + number;
      }

      if (questId.StartsWith("Quest_Catering", StringComparison.Ordinal))
      {
        return 6270 + number;
      }

      if (questId.StartsWith("Quest_StrayMan", StringComparison.Ordinal))
      {
        return 6292 + number;
      }

      throw new InvalidDataException($"Unknown quest archive flag mapping for '{questId}'.");
    }
  }
}
