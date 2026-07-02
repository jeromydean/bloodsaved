using BloodSaved.Parsing;
using BloodSaved.Parsing.Enums;
using BloodSaved.Parsing.Extensions;
using BloodSaved.Parsing.Sections;

namespace BloodSaved.Parsing.Tests
{
  public class GameRecordTests
  {
    [Fact]
    public void DenOfBehemothsGameRecordReadsCorrectly()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.DenOfBehemoths);
      SaveSlot saveSlot = SaveSlot.Load(savePath);

      Assert.NotNull(saveSlot.GameRecord);
      Assert.Equal(36, saveSlot.GameRecord.TotalBookshelfDiary.Count);
      Assert.Equal(8, saveSlot.GameRecord.TotalBookshelfDiary[ArtsId.CriticalSwing.GetArtsIdKey()]);
      Assert.Equal(11, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(800, saveSlot.GameRecord.GetArtsExperience(ArtsId.CriticalSwing));
      Assert.Equal(800, saveSlot.GameRecord.PcInfo.ArtsExperience[28]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ContinuousJumpKickCount);
      Assert.Equal("Critical Swing", ArtsId.CriticalSwing.GetTechniqueName());
      Assert.Equal("Arts005", ArtsId.Parry.GetArtsIdKey());
    }

    [Fact]
    public void EarlyGameSaveCanAddArtsExperience()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.ArvantvilleNoCheatNoBonus);
      string tempPath = Path.Combine(Path.GetTempPath(), "bloodsaved-early-arts-test.sav");
      try
      {
        SaveSlot saveSlot = SaveSlot.Load(savePath);

        Assert.NotNull(saveSlot.GameRecord);
        Assert.DoesNotContain(
          saveSlot.GameRecord.PcInfo.ArtsExperience,
          experience => experience != 0);

        saveSlot.GameRecord.SetArtsMastered(ArtsId.Parry, mastered: true);
        saveSlot.Save(tempPath);

        SaveSlot reloaded = SaveSlot.Load(tempPath);
        Assert.True(reloaded.GameRecord!.IsArtsMastered(ArtsId.Parry));
        Assert.Equal(TechniqueConstants.MasteryExperience, reloaded.GameRecord.GetArtsExperience(ArtsId.Parry));
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
    public void RootStorySlot0BaselineHasNoTechniqueProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0Baseline);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsUseNum, useCount => Assert.Equal(0, useCount));

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId), artsId.GetTechniqueName());
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasSurpriseGiftPartialProgressAtIndexFifteen()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0SurpriseGiftPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (artsId == ArtsId.SurpriseGift)
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasSurpriseGiftAndPowerSlashPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0SurpriseGiftAndPowerSlashPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (artsId is ArtsId.SurpriseGift or ArtsId.PowerSlash)
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasSurpriseGiftPowerSlashAndForceBlastPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0SurpriseGiftPowerSlashAndForceBlastPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[1]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(3, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      ArtsId[] progressedTechniques = [ArtsId.SurpriseGift, ArtsId.PowerSlash, ArtsId.ForceBlast];
      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasFourTechniquesPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0FourTechniquesPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(36, ArtsId.Parry.GetArrayIndex());
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(5, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Parry));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Parry));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Parry));
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[1]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ArtsUseNum[36]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      ArtsId[] progressedTechniques =
      [
        ArtsId.SurpriseGift,
        ArtsId.PowerSlash,
        ArtsId.ForceBlast,
        ArtsId.Parry,
      ];

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasFiveTechniquesPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0FiveTechniquesPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(36, ArtsId.Parry.GetArrayIndex());
      Assert.Equal(35, ArtsId.RapidFire.GetArrayIndex());
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(5, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Parry));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.RapidFire));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Parry));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.RapidFire));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Parry));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.RapidFire));
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[1]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[35]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ArtsUseNum[36]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      ArtsId[] progressedTechniques =
      [
        ArtsId.SurpriseGift,
        ArtsId.PowerSlash,
        ArtsId.ForceBlast,
        ArtsId.Parry,
        ArtsId.RapidFire,
      ];

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasSixTechniquesPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0SixTechniquesPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(36, ArtsId.Parry.GetArrayIndex());
      Assert.Equal(35, ArtsId.RapidFire.GetArrayIndex());
      Assert.Equal(37, ArtsId.EleventhHour.GetArrayIndex());
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(5, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Parry));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.RapidFire));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.EleventhHour));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Parry));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.RapidFire));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.EleventhHour));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Parry));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.RapidFire));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.EleventhHour));
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[1]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[35]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ArtsUseNum[36]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[37]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(6, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      ArtsId[] progressedTechniques =
      [
        ArtsId.SurpriseGift,
        ArtsId.PowerSlash,
        ArtsId.ForceBlast,
        ArtsId.Parry,
        ArtsId.RapidFire,
        ArtsId.EleventhHour,
      ];

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasSevenTechniquesPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0SevenTechniquesPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(14, ArtsId.Assassinate.GetArrayIndex());
      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(36, ArtsId.Parry.GetArrayIndex());
      Assert.Equal(35, ArtsId.RapidFire.GetArrayIndex());
      Assert.Equal(37, ArtsId.EleventhHour.GetArrayIndex());
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Assassinate));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(5, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Parry));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.RapidFire));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.EleventhHour));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Assassinate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Parry));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.RapidFire));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.EleventhHour));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Assassinate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Parry));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.RapidFire));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.EleventhHour));
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[14]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[1]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[35]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ArtsUseNum[36]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[37]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(7, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      ArtsId[] progressedTechniques =
      [
        ArtsId.Assassinate,
        ArtsId.SurpriseGift,
        ArtsId.PowerSlash,
        ArtsId.ForceBlast,
        ArtsId.Parry,
        ArtsId.RapidFire,
        ArtsId.EleventhHour,
      ];

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasEightTechniquesPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0EightTechniquesPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(14, ArtsId.Assassinate.GetArrayIndex());
      Assert.Equal(16, ArtsId.BackSteal.GetArrayIndex());
      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(36, ArtsId.Parry.GetArrayIndex());
      Assert.Equal(35, ArtsId.RapidFire.GetArrayIndex());
      Assert.Equal(37, ArtsId.EleventhHour.GetArrayIndex());
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Assassinate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.BackSteal));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(5, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Parry));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.RapidFire));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.EleventhHour));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Assassinate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.BackSteal));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Parry));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.RapidFire));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.EleventhHour));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Assassinate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.BackSteal));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Parry));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.RapidFire));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.EleventhHour));
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[14]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[16]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[1]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[35]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ArtsUseNum[36]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[37]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(8, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      ArtsId[] progressedTechniques =
      [
        ArtsId.Assassinate,
        ArtsId.BackSteal,
        ArtsId.SurpriseGift,
        ArtsId.PowerSlash,
        ArtsId.ForceBlast,
        ArtsId.Parry,
        ArtsId.RapidFire,
        ArtsId.EleventhHour,
      ];

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasNineTechniquesPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0NineTechniquesPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(14, ArtsId.Assassinate.GetArrayIndex());
      Assert.Equal(16, ArtsId.BackSteal.GetArrayIndex());
      Assert.Equal(19, ArtsId.ThousandBlossoms.GetArrayIndex());
      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(36, ArtsId.Parry.GetArrayIndex());
      Assert.Equal(35, ArtsId.RapidFire.GetArrayIndex());
      Assert.Equal(37, ArtsId.EleventhHour.GetArrayIndex());
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Assassinate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.BackSteal));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ThousandBlossoms));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(5, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Parry));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.RapidFire));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.EleventhHour));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Assassinate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.BackSteal));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ThousandBlossoms));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Parry));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.RapidFire));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.EleventhHour));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Assassinate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.BackSteal));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ThousandBlossoms));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Parry));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.RapidFire));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.EleventhHour));
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[14]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[16]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[19]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[1]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[35]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ArtsUseNum[36]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[37]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(9, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      ArtsId[] progressedTechniques =
      [
        ArtsId.Assassinate,
        ArtsId.BackSteal,
        ArtsId.ThousandBlossoms,
        ArtsId.SurpriseGift,
        ArtsId.PowerSlash,
        ArtsId.ForceBlast,
        ArtsId.Parry,
        ArtsId.RapidFire,
        ArtsId.EleventhHour,
      ];

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasTenTechniquesPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0TenTechniquesPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(14, ArtsId.Assassinate.GetArrayIndex());
      Assert.Equal(16, ArtsId.BackSteal.GetArrayIndex());
      Assert.Equal(19, ArtsId.ThousandBlossoms.GetArrayIndex());
      Assert.Equal(24, ArtsId.HatchetHeel.GetArrayIndex());
      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(36, ArtsId.Parry.GetArrayIndex());
      Assert.Equal(35, ArtsId.RapidFire.GetArrayIndex());
      Assert.Equal(37, ArtsId.EleventhHour.GetArrayIndex());
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Assassinate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.BackSteal));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ThousandBlossoms));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.HatchetHeel));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(5, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Parry));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.RapidFire));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.EleventhHour));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Assassinate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.BackSteal));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ThousandBlossoms));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.HatchetHeel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Parry));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.RapidFire));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.EleventhHour));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Assassinate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.BackSteal));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ThousandBlossoms));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.HatchetHeel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Parry));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.RapidFire));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.EleventhHour));
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[14]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[16]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[19]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[24]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[1]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[35]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ArtsUseNum[36]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[37]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(10, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      ArtsId[] progressedTechniques =
      [
        ArtsId.Assassinate,
        ArtsId.BackSteal,
        ArtsId.ThousandBlossoms,
        ArtsId.HatchetHeel,
        ArtsId.SurpriseGift,
        ArtsId.PowerSlash,
        ArtsId.ForceBlast,
        ArtsId.Parry,
        ArtsId.RapidFire,
        ArtsId.EleventhHour,
      ];

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasElevenTechniquesPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0ElevenTechniquesPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(5, ArtsId.FlashingAirKick.GetArrayIndex());
      Assert.Equal(14, ArtsId.Assassinate.GetArrayIndex());
      Assert.Equal(16, ArtsId.BackSteal.GetArrayIndex());
      Assert.Equal(19, ArtsId.ThousandBlossoms.GetArrayIndex());
      Assert.Equal(24, ArtsId.HatchetHeel.GetArrayIndex());
      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(36, ArtsId.Parry.GetArrayIndex());
      Assert.Equal(35, ArtsId.RapidFire.GetArrayIndex());
      Assert.Equal(37, ArtsId.EleventhHour.GetArrayIndex());
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.FlashingAirKick));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Assassinate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.BackSteal));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ThousandBlossoms));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.HatchetHeel));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(5, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Parry));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.RapidFire));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.EleventhHour));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.FlashingAirKick));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Assassinate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.BackSteal));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ThousandBlossoms));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.HatchetHeel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Parry));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.RapidFire));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.EleventhHour));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.FlashingAirKick));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Assassinate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.BackSteal));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ThousandBlossoms));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.HatchetHeel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Parry));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.RapidFire));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.EleventhHour));
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[5]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[14]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[16]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[19]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[24]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[1]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[35]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ArtsUseNum[36]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[37]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(11, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      ArtsId[] progressedTechniques =
      [
        ArtsId.FlashingAirKick,
        ArtsId.Assassinate,
        ArtsId.BackSteal,
        ArtsId.ThousandBlossoms,
        ArtsId.HatchetHeel,
        ArtsId.SurpriseGift,
        ArtsId.PowerSlash,
        ArtsId.ForceBlast,
        ArtsId.Parry,
        ArtsId.RapidFire,
        ArtsId.EleventhHour,
      ];

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasTwelveTechniquesPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0TwelveTechniquesPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(5, ArtsId.FlashingAirKick.GetArrayIndex());
      Assert.Equal(14, ArtsId.Assassinate.GetArrayIndex());
      Assert.Equal(16, ArtsId.BackSteal.GetArrayIndex());
      Assert.Equal(19, ArtsId.ThousandBlossoms.GetArrayIndex());
      Assert.Equal(21, ArtsId.OrbitalWheel.GetArrayIndex());
      Assert.Equal(24, ArtsId.HatchetHeel.GetArrayIndex());
      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(36, ArtsId.Parry.GetArrayIndex());
      Assert.Equal(35, ArtsId.RapidFire.GetArrayIndex());
      Assert.Equal(37, ArtsId.EleventhHour.GetArrayIndex());
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.FlashingAirKick));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Assassinate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.BackSteal));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ThousandBlossoms));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.OrbitalWheel));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.HatchetHeel));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(5, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Parry));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.RapidFire));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.EleventhHour));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.FlashingAirKick));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Assassinate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.BackSteal));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ThousandBlossoms));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.OrbitalWheel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.HatchetHeel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Parry));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.RapidFire));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.EleventhHour));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.FlashingAirKick));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Assassinate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.BackSteal));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ThousandBlossoms));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.OrbitalWheel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.HatchetHeel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Parry));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.RapidFire));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.EleventhHour));
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[5]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[14]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[16]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[19]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[21]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[24]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[1]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[35]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ArtsUseNum[36]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[37]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(12, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      ArtsId[] progressedTechniques =
      [
        ArtsId.FlashingAirKick,
        ArtsId.Assassinate,
        ArtsId.BackSteal,
        ArtsId.ThousandBlossoms,
        ArtsId.OrbitalWheel,
        ArtsId.HatchetHeel,
        ArtsId.SurpriseGift,
        ArtsId.PowerSlash,
        ArtsId.ForceBlast,
        ArtsId.Parry,
        ArtsId.RapidFire,
        ArtsId.EleventhHour,
      ];

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasThirteenTechniquesPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0ThirteenTechniquesPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(5, ArtsId.FlashingAirKick.GetArrayIndex());
      Assert.Equal(14, ArtsId.Assassinate.GetArrayIndex());
      Assert.Equal(16, ArtsId.BackSteal.GetArrayIndex());
      Assert.Equal(19, ArtsId.ThousandBlossoms.GetArrayIndex());
      Assert.Equal(21, ArtsId.OrbitalWheel.GetArrayIndex());
      Assert.Equal(22, ArtsId.Penetrate.GetArrayIndex());
      Assert.Equal(24, ArtsId.HatchetHeel.GetArrayIndex());
      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(36, ArtsId.Parry.GetArrayIndex());
      Assert.Equal(35, ArtsId.RapidFire.GetArrayIndex());
      Assert.Equal(37, ArtsId.EleventhHour.GetArrayIndex());
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.FlashingAirKick));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Assassinate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.BackSteal));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ThousandBlossoms));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.OrbitalWheel));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Penetrate));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.HatchetHeel));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(5, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Parry));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.RapidFire));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.EleventhHour));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.FlashingAirKick));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Assassinate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.BackSteal));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ThousandBlossoms));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.OrbitalWheel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Penetrate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.HatchetHeel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Parry));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.RapidFire));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.EleventhHour));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.FlashingAirKick));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Assassinate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.BackSteal));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ThousandBlossoms));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.OrbitalWheel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Penetrate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.HatchetHeel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Parry));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.RapidFire));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.EleventhHour));
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[5]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[14]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[16]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[19]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[21]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[22]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[24]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[1]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[35]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ArtsUseNum[36]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[37]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(13, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      ArtsId[] progressedTechniques =
      [
        ArtsId.FlashingAirKick,
        ArtsId.Assassinate,
        ArtsId.BackSteal,
        ArtsId.ThousandBlossoms,
        ArtsId.OrbitalWheel,
        ArtsId.Penetrate,
        ArtsId.HatchetHeel,
        ArtsId.SurpriseGift,
        ArtsId.PowerSlash,
        ArtsId.ForceBlast,
        ArtsId.Parry,
        ArtsId.RapidFire,
        ArtsId.EleventhHour,
      ];

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasFourteenTechniquesPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0FourteenTechniquesPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(5, ArtsId.FlashingAirKick.GetArrayIndex());
      Assert.Equal(14, ArtsId.Assassinate.GetArrayIndex());
      Assert.Equal(16, ArtsId.BackSteal.GetArrayIndex());
      Assert.Equal(19, ArtsId.ThousandBlossoms.GetArrayIndex());
      Assert.Equal(21, ArtsId.OrbitalWheel.GetArrayIndex());
      Assert.Equal(22, ArtsId.Penetrate.GetArrayIndex());
      Assert.Equal(23, ArtsId.LungingSerpent.GetArrayIndex());
      Assert.Equal(24, ArtsId.HatchetHeel.GetArrayIndex());
      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(36, ArtsId.Parry.GetArrayIndex());
      Assert.Equal(35, ArtsId.RapidFire.GetArrayIndex());
      Assert.Equal(37, ArtsId.EleventhHour.GetArrayIndex());
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.FlashingAirKick));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Assassinate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.BackSteal));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ThousandBlossoms));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.OrbitalWheel));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Penetrate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.LungingSerpent));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.HatchetHeel));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(5, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Parry));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.RapidFire));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.EleventhHour));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.FlashingAirKick));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Assassinate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.BackSteal));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ThousandBlossoms));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.OrbitalWheel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Penetrate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.LungingSerpent));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.HatchetHeel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Parry));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.RapidFire));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.EleventhHour));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.FlashingAirKick));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Assassinate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.BackSteal));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ThousandBlossoms));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.OrbitalWheel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Penetrate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.LungingSerpent));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.HatchetHeel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Parry));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.RapidFire));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.EleventhHour));
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[5]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[14]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[16]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[19]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[21]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[22]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[23]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[24]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[1]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[35]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ArtsUseNum[36]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[37]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(14, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      ArtsId[] progressedTechniques =
      [
        ArtsId.FlashingAirKick,
        ArtsId.Assassinate,
        ArtsId.BackSteal,
        ArtsId.ThousandBlossoms,
        ArtsId.OrbitalWheel,
        ArtsId.Penetrate,
        ArtsId.LungingSerpent,
        ArtsId.HatchetHeel,
        ArtsId.SurpriseGift,
        ArtsId.PowerSlash,
        ArtsId.ForceBlast,
        ArtsId.Parry,
        ArtsId.RapidFire,
        ArtsId.EleventhHour,
      ];

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasFifteenTechniquesPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0FifteenTechniquesPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(5, ArtsId.FlashingAirKick.GetArrayIndex());
      Assert.Equal(14, ArtsId.Assassinate.GetArrayIndex());
      Assert.Equal(16, ArtsId.BackSteal.GetArrayIndex());
      Assert.Equal(19, ArtsId.ThousandBlossoms.GetArrayIndex());
      Assert.Equal(21, ArtsId.OrbitalWheel.GetArrayIndex());
      Assert.Equal(22, ArtsId.Penetrate.GetArrayIndex());
      Assert.Equal(23, ArtsId.LungingSerpent.GetArrayIndex());
      Assert.Equal(24, ArtsId.HatchetHeel.GetArrayIndex());
      Assert.Equal(38, ArtsId.SickleMoon.GetArrayIndex());
      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(36, ArtsId.Parry.GetArrayIndex());
      Assert.Equal(35, ArtsId.RapidFire.GetArrayIndex());
      Assert.Equal(37, ArtsId.EleventhHour.GetArrayIndex());
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.FlashingAirKick));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Assassinate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.BackSteal));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ThousandBlossoms));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.OrbitalWheel));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Penetrate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.LungingSerpent));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.HatchetHeel));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SickleMoon));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(5, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Parry));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.RapidFire));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.EleventhHour));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.FlashingAirKick));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Assassinate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.BackSteal));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ThousandBlossoms));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.OrbitalWheel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Penetrate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.LungingSerpent));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.HatchetHeel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SickleMoon));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Parry));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.RapidFire));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.EleventhHour));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.FlashingAirKick));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Assassinate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.BackSteal));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ThousandBlossoms));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.OrbitalWheel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Penetrate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.LungingSerpent));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.HatchetHeel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SickleMoon));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Parry));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.RapidFire));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.EleventhHour));
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[5]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[14]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[16]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[19]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[21]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[22]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[23]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[24]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[38]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[1]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[35]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ArtsUseNum[36]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[37]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(15, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      ArtsId[] progressedTechniques =
      [
        ArtsId.FlashingAirKick,
        ArtsId.Assassinate,
        ArtsId.BackSteal,
        ArtsId.ThousandBlossoms,
        ArtsId.OrbitalWheel,
        ArtsId.Penetrate,
        ArtsId.LungingSerpent,
        ArtsId.HatchetHeel,
        ArtsId.SickleMoon,
        ArtsId.SurpriseGift,
        ArtsId.PowerSlash,
        ArtsId.ForceBlast,
        ArtsId.Parry,
        ArtsId.RapidFire,
        ArtsId.EleventhHour,
      ];

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasSixteenTechniquesPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0SixteenTechniquesPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(5, ArtsId.FlashingAirKick.GetArrayIndex());
      Assert.Equal(14, ArtsId.Assassinate.GetArrayIndex());
      Assert.Equal(16, ArtsId.BackSteal.GetArrayIndex());
      Assert.Equal(19, ArtsId.ThousandBlossoms.GetArrayIndex());
      Assert.Equal(21, ArtsId.OrbitalWheel.GetArrayIndex());
      Assert.Equal(22, ArtsId.Penetrate.GetArrayIndex());
      Assert.Equal(23, ArtsId.LungingSerpent.GetArrayIndex());
      Assert.Equal(24, ArtsId.HatchetHeel.GetArrayIndex());
      Assert.Equal(32, ArtsId.CrimsonStorm.GetArrayIndex());
      Assert.Equal(34, ArtsId.Sansetsuzan.GetArrayIndex());
      Assert.Equal(38, ArtsId.SickleMoon.GetArrayIndex());
      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(36, ArtsId.Parry.GetArrayIndex());
      Assert.Equal(35, ArtsId.RapidFire.GetArrayIndex());
      Assert.Equal(37, ArtsId.EleventhHour.GetArrayIndex());
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.FlashingAirKick));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Assassinate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.BackSteal));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ThousandBlossoms));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.OrbitalWheel));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Penetrate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.LungingSerpent));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.HatchetHeel));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.CrimsonStorm));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Sansetsuzan));
      Assert.Equal(10, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SickleMoon));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(5, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Parry));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.RapidFire));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.EleventhHour));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.FlashingAirKick));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Assassinate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.BackSteal));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ThousandBlossoms));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.OrbitalWheel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Penetrate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.LungingSerpent));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.HatchetHeel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.CrimsonStorm));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Sansetsuzan));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SickleMoon));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Parry));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.RapidFire));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.EleventhHour));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.FlashingAirKick));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Assassinate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.BackSteal));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ThousandBlossoms));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.OrbitalWheel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Penetrate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.LungingSerpent));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.HatchetHeel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.CrimsonStorm));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Sansetsuzan));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SickleMoon));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Parry));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.RapidFire));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.EleventhHour));
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[5]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[14]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[16]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[19]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[21]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[22]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[23]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[24]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[32]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[34]);
      Assert.Equal(10, saveSlot.GameRecord.PcInfo.ArtsUseNum[38]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[1]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[35]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ArtsUseNum[36]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[37]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(17, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      ArtsId[] progressedTechniques =
      [
        ArtsId.FlashingAirKick,
        ArtsId.Assassinate,
        ArtsId.BackSteal,
        ArtsId.ThousandBlossoms,
        ArtsId.OrbitalWheel,
        ArtsId.Penetrate,
        ArtsId.LungingSerpent,
        ArtsId.HatchetHeel,
        ArtsId.CrimsonStorm,
        ArtsId.Sansetsuzan,
        ArtsId.SickleMoon,
        ArtsId.SurpriseGift,
        ArtsId.PowerSlash,
        ArtsId.ForceBlast,
        ArtsId.Parry,
        ArtsId.RapidFire,
        ArtsId.EleventhHour,
      ];

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasSeventeenTechniquesPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0SeventeenTechniquesPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(5, ArtsId.FlashingAirKick.GetArrayIndex());
      Assert.Equal(14, ArtsId.Assassinate.GetArrayIndex());
      Assert.Equal(16, ArtsId.BackSteal.GetArrayIndex());
      Assert.Equal(19, ArtsId.ThousandBlossoms.GetArrayIndex());
      Assert.Equal(21, ArtsId.OrbitalWheel.GetArrayIndex());
      Assert.Equal(22, ArtsId.Penetrate.GetArrayIndex());
      Assert.Equal(23, ArtsId.LungingSerpent.GetArrayIndex());
      Assert.Equal(24, ArtsId.HatchetHeel.GetArrayIndex());
      Assert.Equal(31, ArtsId.Jinrai.GetArrayIndex());
      Assert.Equal(32, ArtsId.CrimsonStorm.GetArrayIndex());
      Assert.Equal(34, ArtsId.Sansetsuzan.GetArrayIndex());
      Assert.Equal(38, ArtsId.SickleMoon.GetArrayIndex());
      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(36, ArtsId.Parry.GetArrayIndex());
      Assert.Equal(35, ArtsId.RapidFire.GetArrayIndex());
      Assert.Equal(37, ArtsId.EleventhHour.GetArrayIndex());
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.FlashingAirKick));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Assassinate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.BackSteal));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ThousandBlossoms));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.OrbitalWheel));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Penetrate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.LungingSerpent));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.HatchetHeel));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Jinrai));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.CrimsonStorm));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Sansetsuzan));
      Assert.Equal(10, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SickleMoon));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(5, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Parry));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.RapidFire));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.EleventhHour));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.FlashingAirKick));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Assassinate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.BackSteal));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ThousandBlossoms));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.OrbitalWheel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Penetrate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.LungingSerpent));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.HatchetHeel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Jinrai));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.CrimsonStorm));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Sansetsuzan));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SickleMoon));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Parry));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.RapidFire));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.EleventhHour));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.FlashingAirKick));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Assassinate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.BackSteal));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ThousandBlossoms));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.OrbitalWheel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Penetrate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.LungingSerpent));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.HatchetHeel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Jinrai));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.CrimsonStorm));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Sansetsuzan));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SickleMoon));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Parry));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.RapidFire));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.EleventhHour));
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[5]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[14]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[16]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[19]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[21]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[22]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[23]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[24]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[31]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[32]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[34]);
      Assert.Equal(10, saveSlot.GameRecord.PcInfo.ArtsUseNum[38]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[1]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[35]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ArtsUseNum[36]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[37]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(18, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      ArtsId[] progressedTechniques =
      [
        ArtsId.FlashingAirKick,
        ArtsId.Assassinate,
        ArtsId.BackSteal,
        ArtsId.ThousandBlossoms,
        ArtsId.OrbitalWheel,
        ArtsId.Penetrate,
        ArtsId.LungingSerpent,
        ArtsId.HatchetHeel,
        ArtsId.Jinrai,
        ArtsId.CrimsonStorm,
        ArtsId.Sansetsuzan,
        ArtsId.SickleMoon,
        ArtsId.SurpriseGift,
        ArtsId.PowerSlash,
        ArtsId.ForceBlast,
        ArtsId.Parry,
        ArtsId.RapidFire,
        ArtsId.EleventhHour,
      ];

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasEighteenTechniquesPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0EighteenTechniquesPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(5, ArtsId.FlashingAirKick.GetArrayIndex());
      Assert.Equal(14, ArtsId.Assassinate.GetArrayIndex());
      Assert.Equal(16, ArtsId.BackSteal.GetArrayIndex());
      Assert.Equal(19, ArtsId.ThousandBlossoms.GetArrayIndex());
      Assert.Equal(21, ArtsId.OrbitalWheel.GetArrayIndex());
      Assert.Equal(22, ArtsId.Penetrate.GetArrayIndex());
      Assert.Equal(23, ArtsId.LungingSerpent.GetArrayIndex());
      Assert.Equal(24, ArtsId.HatchetHeel.GetArrayIndex());
      Assert.Equal(30, ArtsId.Helmsplitter.GetArrayIndex());
      Assert.Equal(31, ArtsId.Jinrai.GetArrayIndex());
      Assert.Equal(32, ArtsId.CrimsonStorm.GetArrayIndex());
      Assert.Equal(34, ArtsId.Sansetsuzan.GetArrayIndex());
      Assert.Equal(38, ArtsId.SickleMoon.GetArrayIndex());
      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(36, ArtsId.Parry.GetArrayIndex());
      Assert.Equal(35, ArtsId.RapidFire.GetArrayIndex());
      Assert.Equal(37, ArtsId.EleventhHour.GetArrayIndex());
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.FlashingAirKick));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Assassinate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.BackSteal));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ThousandBlossoms));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.OrbitalWheel));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Penetrate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.LungingSerpent));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.HatchetHeel));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Helmsplitter));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Jinrai));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.CrimsonStorm));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Sansetsuzan));
      Assert.Equal(10, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SickleMoon));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(5, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Parry));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.RapidFire));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.EleventhHour));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.FlashingAirKick));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Assassinate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.BackSteal));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ThousandBlossoms));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.OrbitalWheel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Penetrate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.LungingSerpent));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.HatchetHeel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Helmsplitter));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Jinrai));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.CrimsonStorm));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Sansetsuzan));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SickleMoon));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Parry));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.RapidFire));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.EleventhHour));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.FlashingAirKick));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Assassinate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.BackSteal));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ThousandBlossoms));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.OrbitalWheel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Penetrate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.LungingSerpent));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.HatchetHeel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Helmsplitter));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Jinrai));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.CrimsonStorm));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Sansetsuzan));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SickleMoon));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Parry));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.RapidFire));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.EleventhHour));
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[5]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[14]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[16]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[19]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[21]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[22]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[23]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[24]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[30]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[31]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[32]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[34]);
      Assert.Equal(10, saveSlot.GameRecord.PcInfo.ArtsUseNum[38]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[1]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[35]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ArtsUseNum[36]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[37]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(19, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      ArtsId[] progressedTechniques =
      [
        ArtsId.FlashingAirKick,
        ArtsId.Assassinate,
        ArtsId.BackSteal,
        ArtsId.ThousandBlossoms,
        ArtsId.OrbitalWheel,
        ArtsId.Penetrate,
        ArtsId.LungingSerpent,
        ArtsId.HatchetHeel,
        ArtsId.Helmsplitter,
        ArtsId.Jinrai,
        ArtsId.CrimsonStorm,
        ArtsId.Sansetsuzan,
        ArtsId.SickleMoon,
        ArtsId.SurpriseGift,
        ArtsId.PowerSlash,
        ArtsId.ForceBlast,
        ArtsId.Parry,
        ArtsId.RapidFire,
        ArtsId.EleventhHour,
      ];

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasNineteenTechniquesPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0NineteenTechniquesPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(5, ArtsId.FlashingAirKick.GetArrayIndex());
      Assert.Equal(14, ArtsId.Assassinate.GetArrayIndex());
      Assert.Equal(16, ArtsId.BackSteal.GetArrayIndex());
      Assert.Equal(19, ArtsId.ThousandBlossoms.GetArrayIndex());
      Assert.Equal(21, ArtsId.OrbitalWheel.GetArrayIndex());
      Assert.Equal(22, ArtsId.Penetrate.GetArrayIndex());
      Assert.Equal(23, ArtsId.LungingSerpent.GetArrayIndex());
      Assert.Equal(24, ArtsId.HatchetHeel.GetArrayIndex());
      Assert.Equal(30, ArtsId.Helmsplitter.GetArrayIndex());
      Assert.Equal(31, ArtsId.Jinrai.GetArrayIndex());
      Assert.Equal(32, ArtsId.CrimsonStorm.GetArrayIndex());
      Assert.Equal(33, ArtsId.CrescentStroke.GetArrayIndex());
      Assert.Equal(34, ArtsId.Sansetsuzan.GetArrayIndex());
      Assert.Equal(38, ArtsId.SickleMoon.GetArrayIndex());
      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(36, ArtsId.Parry.GetArrayIndex());
      Assert.Equal(35, ArtsId.RapidFire.GetArrayIndex());
      Assert.Equal(37, ArtsId.EleventhHour.GetArrayIndex());
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.FlashingAirKick));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Assassinate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.BackSteal));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ThousandBlossoms));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.OrbitalWheel));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Penetrate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.LungingSerpent));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.HatchetHeel));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Helmsplitter));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Jinrai));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.CrimsonStorm));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.CrescentStroke));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Sansetsuzan));
      Assert.Equal(10, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SickleMoon));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(5, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Parry));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.RapidFire));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.EleventhHour));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.FlashingAirKick));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Assassinate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.BackSteal));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ThousandBlossoms));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.OrbitalWheel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Penetrate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.LungingSerpent));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.HatchetHeel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Helmsplitter));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Jinrai));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.CrimsonStorm));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.CrescentStroke));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Sansetsuzan));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SickleMoon));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Parry));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.RapidFire));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.EleventhHour));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.FlashingAirKick));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Assassinate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.BackSteal));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ThousandBlossoms));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.OrbitalWheel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Penetrate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.LungingSerpent));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.HatchetHeel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Helmsplitter));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Jinrai));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.CrimsonStorm));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.CrescentStroke));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Sansetsuzan));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SickleMoon));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Parry));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.RapidFire));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.EleventhHour));
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[5]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[14]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[16]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[19]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[21]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[22]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[23]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[24]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[30]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[31]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[32]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[33]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[34]);
      Assert.Equal(10, saveSlot.GameRecord.PcInfo.ArtsUseNum[38]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[1]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[35]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ArtsUseNum[36]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[37]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(20, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      ArtsId[] progressedTechniques =
      [
        ArtsId.FlashingAirKick,
        ArtsId.Assassinate,
        ArtsId.BackSteal,
        ArtsId.ThousandBlossoms,
        ArtsId.OrbitalWheel,
        ArtsId.Penetrate,
        ArtsId.LungingSerpent,
        ArtsId.HatchetHeel,
        ArtsId.Helmsplitter,
        ArtsId.Jinrai,
        ArtsId.CrimsonStorm,
        ArtsId.CrescentStroke,
        ArtsId.Sansetsuzan,
        ArtsId.SickleMoon,
        ArtsId.SurpriseGift,
        ArtsId.PowerSlash,
        ArtsId.ForceBlast,
        ArtsId.Parry,
        ArtsId.RapidFire,
        ArtsId.EleventhHour,
      ];

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0HasTwentyTechniquesPartialProgress()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0TwentyTechniquesPartial);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(5, ArtsId.FlashingAirKick.GetArrayIndex());
      Assert.Equal(14, ArtsId.Assassinate.GetArrayIndex());
      Assert.Equal(16, ArtsId.BackSteal.GetArrayIndex());
      Assert.Equal(19, ArtsId.ThousandBlossoms.GetArrayIndex());
      Assert.Equal(21, ArtsId.OrbitalWheel.GetArrayIndex());
      Assert.Equal(22, ArtsId.Penetrate.GetArrayIndex());
      Assert.Equal(23, ArtsId.LungingSerpent.GetArrayIndex());
      Assert.Equal(24, ArtsId.HatchetHeel.GetArrayIndex());
      Assert.Equal(27, ArtsId.TrucidatingGyre.GetArrayIndex());
      Assert.Equal(30, ArtsId.Helmsplitter.GetArrayIndex());
      Assert.Equal(31, ArtsId.Jinrai.GetArrayIndex());
      Assert.Equal(32, ArtsId.CrimsonStorm.GetArrayIndex());
      Assert.Equal(33, ArtsId.CrescentStroke.GetArrayIndex());
      Assert.Equal(34, ArtsId.Sansetsuzan.GetArrayIndex());
      Assert.Equal(38, ArtsId.SickleMoon.GetArrayIndex());
      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());
      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(36, ArtsId.Parry.GetArrayIndex());
      Assert.Equal(35, ArtsId.RapidFire.GetArrayIndex());
      Assert.Equal(37, ArtsId.EleventhHour.GetArrayIndex());
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.FlashingAirKick));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Assassinate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.BackSteal));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ThousandBlossoms));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.OrbitalWheel));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Penetrate));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.LungingSerpent));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.HatchetHeel));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.TrucidatingGyre));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Helmsplitter));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Jinrai));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.CrimsonStorm));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.CrescentStroke));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Sansetsuzan));
      Assert.Equal(10, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SickleMoon));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.SurpriseGift));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.PowerSlash));
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(5, saveSlot.GameRecord.GetArtsUseCount(ArtsId.Parry));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.RapidFire));
      Assert.Equal(2, saveSlot.GameRecord.GetArtsUseCount(ArtsId.EleventhHour));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.FlashingAirKick));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Assassinate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.BackSteal));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ThousandBlossoms));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.OrbitalWheel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Penetrate));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.LungingSerpent));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.HatchetHeel));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.TrucidatingGyre));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Helmsplitter));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Jinrai));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.CrimsonStorm));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.CrescentStroke));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Sansetsuzan));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SickleMoon));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.SurpriseGift));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.PowerSlash));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.Parry));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.RapidFire));
      Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(ArtsId.EleventhHour));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.FlashingAirKick));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Assassinate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.BackSteal));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ThousandBlossoms));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.OrbitalWheel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Penetrate));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.LungingSerpent));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.HatchetHeel));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.TrucidatingGyre));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Helmsplitter));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Jinrai));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.CrimsonStorm));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.CrescentStroke));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Sansetsuzan));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SickleMoon));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.SurpriseGift));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Parry));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.RapidFire));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.EleventhHour));
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[5]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[14]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[16]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[19]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[21]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[22]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[23]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[24]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[27]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[30]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[31]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[32]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[33]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[34]);
      Assert.Equal(10, saveSlot.GameRecord.PcInfo.ArtsUseNum[38]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[1]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[15]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[35]);
      Assert.Equal(5, saveSlot.GameRecord.PcInfo.ArtsUseNum[36]);
      Assert.Equal(2, saveSlot.GameRecord.PcInfo.ArtsUseNum[37]);
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[47]);
      Assert.Equal(21, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));
      Assert.All(saveSlot.GameRecord.PcInfo.ArtsExperience, experience => Assert.Equal(0, experience));

      ArtsId[] progressedTechniques =
      [
        ArtsId.FlashingAirKick,
        ArtsId.Assassinate,
        ArtsId.BackSteal,
        ArtsId.ThousandBlossoms,
        ArtsId.OrbitalWheel,
        ArtsId.Penetrate,
        ArtsId.LungingSerpent,
        ArtsId.HatchetHeel,
        ArtsId.TrucidatingGyre,
        ArtsId.Helmsplitter,
        ArtsId.Jinrai,
        ArtsId.CrimsonStorm,
        ArtsId.CrescentStroke,
        ArtsId.Sansetsuzan,
        ArtsId.SickleMoon,
        ArtsId.SurpriseGift,
        ArtsId.PowerSlash,
        ArtsId.ForceBlast,
        ArtsId.Parry,
        ArtsId.RapidFire,
        ArtsId.EleventhHour,
      ];

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void RootStorySlot0CompleteTechniqueMapHasLastingWoundAtIndexTwenty()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.RootStorySlot0CompleteTechniqueMap);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(20, ArtsId.LastingWound.GetArrayIndex());
      Assert.Equal(1, saveSlot.GameRecord.GetArtsUseCount(ArtsId.LastingWound));
      Assert.Equal(4, saveSlot.GameRecord.GetArtsUseCount(ArtsId.TrucidatingGyre));
      Assert.Equal(1, saveSlot.GameRecord.PcInfo.ArtsUseNum[20]);
      Assert.Equal(4, saveSlot.GameRecord.PcInfo.ArtsUseNum[27]);
      Assert.Equal(22, saveSlot.GameRecord.PcInfo.ArtsUseNum.Count(useCount => useCount != 0));

      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(5, ArtsId.FlashingAirKick.GetArrayIndex());
      Assert.Equal(14, ArtsId.Assassinate.GetArrayIndex());
      Assert.Equal(15, ArtsId.SurpriseGift.GetArrayIndex());
      Assert.Equal(16, ArtsId.BackSteal.GetArrayIndex());
      Assert.Equal(19, ArtsId.ThousandBlossoms.GetArrayIndex());
      Assert.Equal(21, ArtsId.OrbitalWheel.GetArrayIndex());
      Assert.Equal(22, ArtsId.Penetrate.GetArrayIndex());
      Assert.Equal(23, ArtsId.LungingSerpent.GetArrayIndex());
      Assert.Equal(24, ArtsId.HatchetHeel.GetArrayIndex());
      Assert.Equal(27, ArtsId.TrucidatingGyre.GetArrayIndex());
      Assert.Equal(28, ArtsId.CriticalSwing.GetArrayIndex());
      Assert.Equal(30, ArtsId.Helmsplitter.GetArrayIndex());
      Assert.Equal(31, ArtsId.Jinrai.GetArrayIndex());
      Assert.Equal(32, ArtsId.CrimsonStorm.GetArrayIndex());
      Assert.Equal(33, ArtsId.CrescentStroke.GetArrayIndex());
      Assert.Equal(34, ArtsId.Sansetsuzan.GetArrayIndex());
      Assert.Equal(35, ArtsId.RapidFire.GetArrayIndex());
      Assert.Equal(36, ArtsId.Parry.GetArrayIndex());
      Assert.Equal(37, ArtsId.EleventhHour.GetArrayIndex());
      Assert.Equal(38, ArtsId.SickleMoon.GetArrayIndex());
      Assert.Equal(47, ArtsId.PowerSlash.GetArrayIndex());

      ArtsId[] progressedTechniques =
      [
        ArtsId.FlashingAirKick,
        ArtsId.Assassinate,
        ArtsId.BackSteal,
        ArtsId.ThousandBlossoms,
        ArtsId.OrbitalWheel,
        ArtsId.Penetrate,
        ArtsId.LungingSerpent,
        ArtsId.HatchetHeel,
        ArtsId.LastingWound,
        ArtsId.TrucidatingGyre,
        ArtsId.Helmsplitter,
        ArtsId.Jinrai,
        ArtsId.CrimsonStorm,
        ArtsId.CrescentStroke,
        ArtsId.Sansetsuzan,
        ArtsId.SickleMoon,
        ArtsId.SurpriseGift,
        ArtsId.PowerSlash,
        ArtsId.ForceBlast,
        ArtsId.Parry,
        ArtsId.RapidFire,
        ArtsId.EleventhHour,
      ];

      foreach (ArtsId artsId in Enum.GetValues<ArtsId>())
      {
        if (progressedTechniques.Contains(artsId))
        {
          continue;
        }

        Assert.Equal(0, saveSlot.GameRecord.GetArtsUseCount(artsId));
        Assert.Equal(0, saveSlot.GameRecord.GetArtsExperience(artsId));
        Assert.False(saveSlot.GameRecord.IsArtsMastered(artsId));
      }
    }

    [Fact]
    public void OneNotMasteredSaveHasForceBlastProgressAtIndexOne()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.ForceBlastPartialProgress);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      Assert.Equal(1, ArtsId.ForceBlast.GetArrayIndex());
      Assert.Equal(5, saveSlot.GameRecord.GetArtsUseCount(ArtsId.ForceBlast));
      Assert.Equal(400, saveSlot.GameRecord.GetArtsExperience(ArtsId.ForceBlast));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.ForceBlast));
      Assert.Equal(400, saveSlot.GameRecord.PcInfo.ArtsExperience[1]);
    }

    [Fact]
    public void EarlyGameSaveCanAddMissingBorrowBooksCount()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.ArvantvilleNoCheatNoBonus);
      string tempPath = Path.Combine(Path.GetTempPath(), "bloodsaved-early-borrow-books-test.sav");
      try
      {
        SaveSlot saveSlot = SaveSlot.Load(savePath);
        Assert.NotNull(saveSlot.GameRecord);
        Assert.Equal(0, saveSlot.GameRecord.BorrowBooksCount);

        saveSlot.GameRecord.BorrowBooksCount = 3;
        saveSlot.Save(tempPath);

        SaveSlot reloaded = SaveSlot.Load(tempPath);
        Assert.Equal(3, reloaded.GameRecord!.BorrowBooksCount);
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
    public void EarlyGameSaveCanAddContinuousJumpKickCount()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.ArvantvilleNoCheatNoBonus);
      string tempPath = Path.Combine(Path.GetTempPath(), "bloodsaved-early-jump-kick-test.sav");
      try
      {
        SaveSlot saveSlot = SaveSlot.Load(savePath);
        Assert.NotNull(saveSlot.GameRecord);
        Assert.Equal(0, saveSlot.GameRecord.PcInfo.ContinuousJumpKickCount);

        saveSlot.GameRecord.PcInfo.ContinuousJumpKickCount = 12;
        saveSlot.Save(tempPath);

        SaveSlot reloaded = SaveSlot.Load(tempPath);
        Assert.Equal(12, reloaded.GameRecord!.PcInfo.ContinuousJumpKickCount);
      }
      finally
      {
        if (File.Exists(tempPath))
        {
          File.Delete(tempPath);
        }
      }
    }
  }
}
