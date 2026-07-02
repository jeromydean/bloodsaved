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
      Assert.Equal(11, saveSlot.GameRecord.GetArtsUseCount(ArtsId.FlashingAirKick));
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
    public void UserSaveReadsSevenMasteredTechniquesAtCorrectIndices()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.SevenMasteredTechniques);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      Assert.NotNull(saveSlot.GameRecord);

      ArtsId[] masteredTechniques =
      [
        ArtsId.SurpriseGift,
        ArtsId.ThousandBlossoms,
        ArtsId.OrbitalWheel,
        ArtsId.FlashingAirKick,
        ArtsId.HatchetHeel,
        ArtsId.EleventhHour,
        ArtsId.RapidFire,
      ];

      foreach (ArtsId artsId in masteredTechniques)
      {
        Assert.True(saveSlot.GameRecord.IsArtsMastered(artsId), artsId.GetTechniqueName());
      }

      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.PowerSlash));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Jinrai));
      Assert.False(saveSlot.GameRecord.IsArtsMastered(ArtsId.Sansetsuzan));
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
