namespace BloodSaved.Parsing.Tests
{
  public class MapTraverseTests
  {
    [Fact]
    public void SetMapFullyDiscoveredReachesCompleteMapRoomCount()
    {
      SaveSlot saveSlot = SaveSlot.Load(TestSavePaths.Story(TestSavePaths.StorySaves.ArvantvilleNoCheatNoBonus));
      saveSlot.SetMapFullyDiscovered();

      Assert.Equal(1553, saveSlot.GetTraversedRoomCount());
    }

    [Fact]
    public void SetMapFullyDiscoveredMarksEveryMapRoom()
    {
      SaveSlot saveSlot = SaveSlot.Load(TestSavePaths.Story(TestSavePaths.StorySaves.ArvantvilleNoCheatNoBonus));
      int before = saveSlot.GetTraversedRoomCount();

      saveSlot.SetMapFullyDiscovered();

      Assert.True(saveSlot.GetTraversedRoomCount() > before);
    }

    [Fact]
    public void SetMapFullyDiscoveredPersistsThroughSave()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.ArvantvilleNoCheatNoBonus);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      saveSlot.SetMapFullyDiscovered();
      int expectedCount = saveSlot.GetTraversedRoomCount();

      string tempPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.sav");
      try
      {
        saveSlot.Save(tempPath);

        SaveSlot reloaded = SaveSlot.Load(tempPath);
        Assert.Equal(expectedCount, reloaded.GetTraversedRoomCount());
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
    public void UnmodifiedTraverseSectionRoundTripsThroughSerialize()
    {
      SaveSlot saveSlot = SaveSlot.Load(TestSavePaths.Story(TestSavePaths.StorySaves.DenOfBehemoths));
      byte[] originalTraverseSection = saveSlot.GetTraverseSectionData();
      byte[] serializedTraverse = saveSlot.m_Traverse.Serialize();

      Assert.Equal(originalTraverseSection, serializedTraverse);
    }
  }
}
