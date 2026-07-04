namespace BloodSaved.Parsing.Tests
{
  public class MapTraverseTests
  {
    [Fact]
    public void EnteringWarpRoomAddsWarpRoomVisitState()
    {
      SaveSlot before = SaveSlot.Load(TestSavePaths.Story(TestSavePaths.StorySaves.BeforeArvantvilleWarpRoomEntry));
      SaveSlot after = SaveSlot.Load(TestSavePaths.Story(TestSavePaths.StorySaves.AfterArvantvilleWarpRoomEntry));

      Assert.DoesNotContain(before.m_RoomInfo!.Rooms, room => room.RoomName == "m02VIL_1100");
      Assert.DoesNotContain("m02VIL_1100", before.GameRecord.TotalRoomIn.Keys);

      Assert.Contains(after.m_RoomInfo!.Rooms, room => room.RoomName == "m02VIL_1100" && room.IsCompleted);
      Assert.Equal(1, after.GameRecord.TotalRoomIn["m02VIL_1100"]);
      Assert.False(before.ScenarioFlags.Flags[7258]);
      Assert.True(after.ScenarioFlags.Flags[7258]);
    }

    [Fact]
    public void SetMapFullyDiscoveredIncludesWarpRoomsObservedInStoryFixtures()
    {
      string storySaveDirectory = Path.GetDirectoryName(TestSavePaths.Story(TestSavePaths.StorySaves.StartOfNormalMiriamGame))!;
      string[] observedWarpRooms = Directory
        .EnumerateFiles(storySaveDirectory, "*.sav")
        .Select(SaveSlot.Load)
        .SelectMany(saveSlot => (saveSlot.m_RoomInfo?.Rooms.Select(room => room.RoomName) ?? Enumerable.Empty<string>())
          .Concat(saveSlot.GameRecord?.TotalRoomIn.Keys ?? Enumerable.Empty<string>()))
        .Where(roomName => roomName.EndsWith("_1100", StringComparison.Ordinal))
        .Distinct(StringComparer.Ordinal)
        .OrderBy(roomName => roomName, StringComparer.Ordinal)
        .ToArray();

      SaveSlot completed = SaveSlot.Load(TestSavePaths.Story(TestSavePaths.StorySaves.StartOfNormalMiriamGame));
      completed.SetMapFullyDiscovered();
      HashSet<string> completedRoomInfo = completed.m_RoomInfo!.Rooms
        .Select(room => room.RoomName)
        .ToHashSet(StringComparer.Ordinal);
      HashSet<string> completedRoomIn = completed.GameRecord.TotalRoomIn.Keys.ToHashSet(StringComparer.Ordinal);

      Assert.Equal(15, observedWarpRooms.Length);
      foreach (string warpRoom in observedWarpRooms)
      {
        Assert.Contains(warpRoom, completedRoomInfo);
        Assert.Contains(warpRoom, completedRoomIn);
      }
    }

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
    public void SetMapFullyDiscoveredCompletesRoomInfoEntries()
    {
      SaveSlot saveSlot = SaveSlot.Load(TestSavePaths.Story(TestSavePaths.StorySaves.ArvantvilleNoCheatNoBonus));

      Assert.NotNull(saveSlot.m_RoomInfo);
      Assert.Contains(saveSlot.m_RoomInfo.Rooms, room => !room.IsCompleted);

      saveSlot.SetMapFullyDiscovered();

      Assert.True(saveSlot.m_RoomInfo.Rooms.Count >= 493);
      Assert.All(saveSlot.m_RoomInfo.Rooms, room => Assert.True(room.IsCompleted));
      Assert.Equal(4, saveSlot.m_RoomInfo.Rooms.Count(room => room.RoomName == "m01SIP"));
      Assert.Contains(saveSlot.m_RoomInfo.Rooms, room => room.RoomName == "m02VIL_1100");
    }

    [Fact]
    public void SetMapFullyDiscoveredSynthesizesRoomInfoEntriesForNewGameSave()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.StartOfNormalMiriamGame);
      SaveSlot saveSlot = SaveSlot.Load(savePath);

      Assert.True(saveSlot.m_RoomInfo == null || saveSlot.m_RoomInfo.Rooms.Count == 0);

      saveSlot.SetMapFullyDiscovered();

      Assert.NotNull(saveSlot.m_RoomInfo);
      Assert.Equal(493, saveSlot.m_RoomInfo.Rooms.Count);
      Assert.All(saveSlot.m_RoomInfo.Rooms, room => Assert.True(room.IsCompleted));
      Assert.Contains(saveSlot.m_RoomInfo.Rooms, room => room.RoomName == "m02VIL");
      Assert.Contains(saveSlot.m_RoomInfo.Rooms, room => room.RoomName == "m02VIL_1100");
    }

    [Fact]
    public void SetMapFullyDiscoveredSynthesizesVisitedAreasForNewGameSave()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.StartOfNormalMiriamGame);
      SaveSlot saveSlot = SaveSlot.Load(savePath);

      Assert.Null(saveSlot.VisitedArea);
      Assert.Empty(saveSlot.GameRecord.TotalRoomIn);

      saveSlot.SetMapFullyDiscovered();

      Assert.NotNull(saveSlot.VisitedArea);
      Assert.Equal(BloodSaved.Parsing.Sections.VisitedArea.CompleteMapAreas, saveSlot.VisitedArea.Areas);
      Assert.Equal(415, saveSlot.GameRecord.TotalRoomIn.Count);
      Assert.Equal(1, saveSlot.GameRecord.TotalRoomIn["m02VIL"]);
      Assert.Equal(1, saveSlot.GameRecord.TotalRoomIn["m02VIL_1100"]);
    }

    [Fact]
    public void SetMapFullyDiscoveredAddsMissingVisitedAreasWithoutRemovingExistingAreas()
    {
      SaveSlot saveSlot = SaveSlot.Load(TestSavePaths.Story(TestSavePaths.StorySaves.ArvantvilleNoCheatNoBonus));

      Assert.NotNull(saveSlot.VisitedArea);
      Assert.Equal(["ACT01_SIP", "ACT02_VIL"], saveSlot.VisitedArea.Areas);

      saveSlot.SetMapFullyDiscovered();

      Assert.Equal(BloodSaved.Parsing.Sections.VisitedArea.CompleteMapAreas, saveSlot.VisitedArea.Areas);
    }

    [Fact]
    public void SetMapFullyDiscoveredPersistsRoomInfoEntriesThroughSave()
    {
      string savePath = TestSavePaths.Story(TestSavePaths.StorySaves.StartOfNormalMiriamGame);
      SaveSlot saveSlot = SaveSlot.Load(savePath);
      saveSlot.SetMapFullyDiscovered();

      string tempPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.sav");
      try
      {
        saveSlot.Save(tempPath);

        SaveSlot reloaded = SaveSlot.Load(tempPath);
        Assert.NotNull(reloaded.m_RoomInfo);
        Assert.Equal(493, reloaded.m_RoomInfo.Rooms.Count);
        Assert.All(reloaded.m_RoomInfo.Rooms, room => Assert.True(room.IsCompleted));
        Assert.NotNull(reloaded.VisitedArea);
        Assert.Equal(BloodSaved.Parsing.Sections.VisitedArea.CompleteMapAreas, reloaded.VisitedArea.Areas);
        Assert.Equal(415, reloaded.GameRecord.TotalRoomIn.Count);
        Assert.Equal(1, reloaded.GameRecord.TotalRoomIn["m02VIL"]);
        Assert.Equal(1, reloaded.GameRecord.TotalRoomIn["m02VIL_1100"]);
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
