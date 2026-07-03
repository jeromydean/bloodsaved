using BloodSaved.Parsing.Models;
using BloodSaved.Parsing.Sections;
using System.Reflection;

namespace BloodSaved.Parsing.Tests
{
  public class QuestDataTests
  {
    [Fact]
    public void QuestDataRoundTripsAcceptedAndDoneQuests()
    {
      QuestData questData = new QuestData();
      AcceptedQuest acceptedQuest = new AcceptedQuest
      {
        QuestId = "Quest_Enemy01",
        EnemyId01 = "N3003",
        NowKilling = 2,
        IsNew = true,
      };
      acceptedQuest.EnemyLocations.Add("m01SIP_001");
      questData.Accepted.Add(acceptedQuest);
      questData.Done.Add("Quest_Memento01");

      QuestData reloaded = QuestData.Deserialize(new SaveSection
      {
        Name = "QuestData",
        Type = "ArrayProperty",
        Data = questData.Serialize(),
      });

      Assert.Single(reloaded.Accepted);
      Assert.Equal("Quest_Enemy01", reloaded.Accepted[0].QuestId);
      Assert.Equal("N3003", reloaded.Accepted[0].EnemyId01);
      Assert.Equal(2, reloaded.Accepted[0].NowKilling);
      Assert.True(reloaded.Accepted[0].IsNew);
      Assert.Equal("m01SIP_001", reloaded.Accepted[0].EnemyLocations.Single());
      Assert.Equal("Quest_Memento01", reloaded.Done.Single());
    }

    [Fact]
    public void SetAllQuestsDoneClearsAcceptedAndAddsEveryKnownQuest()
    {
      QuestData questData = new QuestData();
      questData.Accepted.Add(new AcceptedQuest
      {
        QuestId = "Quest_Catering21",
        EnemyId01 = "None",
      });
      questData.Done.Add("Quest_Memento01");

      questData.SetAllQuestsDone();

      Assert.Empty(questData.Accepted);
      Assert.Equal(59, questData.Done.Count);
      Assert.Equal(QuestData.AllQuestIds, questData.Done);
      Assert.Contains("Quest_Catering21", questData.Done);

      QuestData reloaded = QuestData.Deserialize(new SaveSection
      {
        Name = "QuestData",
        Type = "ArrayProperty",
        Data = questData.Serialize(),
      });

      Assert.Empty(reloaded.Accepted);
      Assert.Equal(QuestData.AllQuestIds, reloaded.Done);
    }

    [Fact]
    public void SetCompletedQuestsPreservesQuestOrderAndClearsMatchingAccepted()
    {
      QuestData questData = new QuestData();
      questData.Accepted.Add(new AcceptedQuest
      {
        QuestId = "Quest_Catering21",
        EnemyId01 = "None",
      });
      questData.Accepted.Add(new AcceptedQuest
      {
        QuestId = "Quest_Enemy01",
        EnemyId01 = "N3003",
      });

      questData.SetCompletedQuests(new[]
      {
        "Quest_Catering21",
        "Quest_Memento01",
      });

      Assert.Equal(new[] { "Quest_Memento01", "Quest_Catering21" }, questData.Done);
      AcceptedQuest remainingAcceptedQuest = Assert.Single(questData.Accepted);
      Assert.Equal("Quest_Enemy01", remainingAcceptedQuest.QuestId);
    }

    [Fact]
    public void GraveManagerRoundTripsAllMementoQuests()
    {
      GraveManager graveManager = new GraveManager();

      graveManager.SetAllMementosCompleted();

      GraveManager reloaded = GraveManager.Deserialize(new SaveSection
      {
        Name = "GraveManager",
        Type = "ArrayProperty",
        Data = graveManager.Serialize(),
      });

      Assert.Equal(13, reloaded.Graves.Count);
      Assert.Equal(GraveManager.AllMementoQuestIds, reloaded.Graves);
    }

    [Fact]
    public void EventListenerManagerRoundTripsQuestCompletionState()
    {
      EventListenerManager eventListenerManager = new EventListenerManager();

      eventListenerManager.EnsureQuestCompletionState();

      EventListenerManager reloaded = EventListenerManager.Deserialize(new SaveSection
      {
        Name = "EventListenerManager",
        Type = "ArrayProperty",
        Data = eventListenerManager.Serialize(),
      });

      Assert.True(reloaded.FlagArray[1]);
      Assert.Equal(1, reloaded.IntArray[10]);
      Assert.Equal(1, reloaded.IntArray[13]);
      Assert.Equal(1, reloaded.IntArray[21]);
    }

    [Fact]
    public void ScenarioFlagsRoundTripsQuestArchiveUnlockFlags()
    {
      ScenarioFlags scenarioFlags = new ScenarioFlags();

      scenarioFlags.UnlockAllQuestArchiveEntries();

      ScenarioFlags reloaded = ScenarioFlags.Deserialize(new SaveSection
      {
        Name = "ScenarioFlags",
        Type = "ArrayProperty",
        Data = scenarioFlags.Serialize(),
      });

      Assert.Equal(6236, QuestArchiveFlagIndexes.GetQuestArchiveFlagIndex("Quest_Enemy01"));
      Assert.Equal(6255, QuestArchiveFlagIndexes.GetQuestArchiveFlagIndex("Quest_Enemy20"));
      Assert.Equal(6256, QuestArchiveFlagIndexes.GetQuestArchiveFlagIndex("Quest_Memento01"));
      Assert.Equal(6271, QuestArchiveFlagIndexes.GetQuestArchiveFlagIndex("Quest_Catering01"));
      Assert.Equal(6287, QuestArchiveFlagIndexes.GetQuestArchiveFlagIndex("Quest_Catering17"));
      Assert.Equal(6293, QuestArchiveFlagIndexes.GetQuestArchiveFlagIndex("Quest_StrayMan01"));
      Assert.All(QuestArchiveFlagIndexes.All, flagIndex => Assert.True(reloaded.Flags[flagIndex], flagIndex.ToString()));
    }

    [Fact]
    public void CompleteAllQuestsSynthesizesMissingQuestSections()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.StartOfNormalMiriamGame);
      string tempPath = Path.Combine(Path.GetTempPath(), $"bloodsaved-start-quests-test-{Guid.NewGuid():N}.sav");
      try
      {
        SaveSlot saveSlot = SaveSlot.Load(savePath);
        RemoveTopLevelSection(saveSlot, "EventListenerManager");
        RemoveTopLevelSection(saveSlot, "GraveManager");
        RemoveTopLevelSection(saveSlot, "QuestData");
        RemoveTopLevelSection(saveSlot, "ScenarioFlags");

        saveSlot.CompleteAllQuests();
        saveSlot.Save(tempPath);

        SaveSlot reloaded = SaveSlot.Load(tempPath);
        Assert.Equal(59, reloaded.QuestData.Done.Count);
        Assert.Empty(reloaded.QuestData.Accepted);
        Assert.Equal(13, reloaded.GraveManager.Graves.Count);
        Assert.True(reloaded.EventListenerManager.FlagArray[1]);
        Assert.Equal(1, reloaded.EventListenerManager.IntArray[10]);
        Assert.Equal(1, reloaded.EventListenerManager.IntArray[13]);
        Assert.Equal(1, reloaded.EventListenerManager.IntArray[21]);
        Assert.All(QuestArchiveFlagIndexes.All, flagIndex => Assert.True(reloaded.ScenarioFlags.Flags[flagIndex], flagIndex.ToString()));
        Assert.Equal(59, reloaded.GameRecord.QuestClearCount);
      }
      finally
      {
        if (File.Exists(tempPath))
        {
          File.Delete(tempPath);
        }
      }
    }

    [Fact]
    public void SetCompletedQuestsSynthesizesOnlySelectedQuestState()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.StartOfNormalMiriamGame);
      string tempPath = Path.Combine(Path.GetTempPath(), $"bloodsaved-selected-quests-test-{Guid.NewGuid():N}.sav");
      try
      {
        SaveSlot saveSlot = SaveSlot.Load(savePath);
        RemoveTopLevelSection(saveSlot, "EventListenerManager");
        RemoveTopLevelSection(saveSlot, "GraveManager");
        RemoveTopLevelSection(saveSlot, "QuestData");
        RemoveTopLevelSection(saveSlot, "ScenarioFlags");

        saveSlot.SetCompletedQuests(new[]
        {
          "Quest_Enemy01",
          "Quest_Memento01",
          "Quest_Catering01",
        });
        saveSlot.Save(tempPath);

        SaveSlot reloaded = SaveSlot.Load(tempPath);
        Assert.Equal(new[] { "Quest_Enemy01", "Quest_Memento01", "Quest_Catering01" }, reloaded.QuestData.Done);
        Assert.Equal(new[] { "Quest_Memento01" }, reloaded.GraveManager.Graves);
        Assert.True(reloaded.ScenarioFlags.Flags[QuestArchiveFlagIndexes.GetQuestArchiveFlagIndex("Quest_Enemy01")]);
        Assert.True(reloaded.ScenarioFlags.Flags[QuestArchiveFlagIndexes.GetQuestArchiveFlagIndex("Quest_Memento01")]);
        Assert.True(reloaded.ScenarioFlags.Flags[QuestArchiveFlagIndexes.GetQuestArchiveFlagIndex("Quest_Catering01")]);
        Assert.False(reloaded.ScenarioFlags.Flags[QuestArchiveFlagIndexes.GetQuestArchiveFlagIndex("Quest_Enemy02")]);
        Assert.Equal(3, reloaded.GameRecord.QuestClearCount);
      }
      finally
      {
        if (File.Exists(tempPath))
        {
          File.Delete(tempPath);
        }
      }
    }

    [Fact]
    public void CompleteAllQuestsMatchesNativeAllQuestSections()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.StartOfNormalMiriamGame);
      string nativeAllQuestsPath = TestSavePaths.Story(TestSavePaths.StorySaves.NativeAllQuestsComplete);
      string tempPath = Path.Combine(Path.GetTempPath(), $"bloodsaved-native-quest-sections-test-{Guid.NewGuid():N}.sav");
      try
      {
        SaveSlot saveSlot = SaveSlot.Load(savePath);

        saveSlot.CompleteAllQuests();
        saveSlot.Save(tempPath);

        SaveSlot generated = SaveSlot.Load(tempPath);
        SaveSlot nativeAllQuests = SaveSlot.Load(nativeAllQuestsPath);

        Assert.Equal(
          GetTopLevelSectionData(nativeAllQuests, "EventListenerManager"),
          GetTopLevelSectionData(generated, "EventListenerManager"));
        Assert.Equal(
          GetTopLevelSectionData(nativeAllQuests, "GraveManager"),
          GetTopLevelSectionData(generated, "GraveManager"));
        Assert.Equal(
          GetTopLevelSectionData(nativeAllQuests, "QuestData"),
          GetTopLevelSectionData(generated, "QuestData"));
      }
      finally
      {
        if (File.Exists(tempPath))
        {
          File.Delete(tempPath);
        }
      }
    }

    private static void RemoveTopLevelSection(SaveSlot saveSlot, string sectionName)
    {
      FieldInfo? saveSectionsField = typeof(SaveSlot)
        .GetField("_saveSections", BindingFlags.Instance | BindingFlags.NonPublic);
      Assert.NotNull(saveSectionsField);

      List<SaveSection> saveSections = Assert.IsType<List<SaveSection>>(saveSectionsField.GetValue(saveSlot));
      saveSections.RemoveAll(section => string.Equals(section.Name, sectionName, StringComparison.Ordinal));
    }

    private static byte[] GetTopLevelSectionData(SaveSlot saveSlot, string sectionName)
    {
      FieldInfo? saveSectionsField = typeof(SaveSlot)
        .GetField("_saveSections", BindingFlags.Instance | BindingFlags.NonPublic);
      Assert.NotNull(saveSectionsField);

      List<SaveSection> saveSections = Assert.IsType<List<SaveSection>>(saveSectionsField.GetValue(saveSlot));
      return saveSections.Single(section => string.Equals(section.Name, sectionName, StringComparison.Ordinal)).Data;
    }
  }
}
