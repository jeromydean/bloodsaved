using System.Security.Cryptography;

namespace BloodSaved.Parsing.Tests
{
  public class SaveSlotTests
  {
    [Theory]
    [InlineData(@".\Resources\Saves\(PS4_StorySlot0)ue4savegame.ps4.sav")]
    [InlineData(@".\Resources\Saves\(PS4_StorySlot1)ue4savegame.ps4.sav")]
    [InlineData(@".\Resources\Saves\(PC_NEW_GAME)Story_Slot0.sav")]
    [InlineData(@".\Resources\Saves\(UNKNOWN)Story_Slot0")]
    [InlineData(@".\Resources\Saves\(UNKNOWN)Story_Slot1")]
    public void UnmodifiedSaveSlotSerializesCorrectly(string saveSlotPath)
    {
      byte[] inputBytes = File.ReadAllBytes(saveSlotPath);
      SaveSlot saveSlot = SaveSlot.Load(saveSlotPath);

      byte[] serializedBytes = null;
      using (MemoryStream serializedStream = new MemoryStream())
      {
        saveSlot.Save(serializedStream);
        serializedBytes = serializedStream.ToArray();
      }

      Assert.Equal(inputBytes.Length, serializedBytes.Length);

      using (SHA256 sha256 = SHA256.Create())
      {
        string inputHash = BitConverter.ToString(sha256.ComputeHash(inputBytes)).Replace("-", string.Empty);
        string serializedHash = BitConverter.ToString(sha256.ComputeHash(serializedBytes)).Replace("-", string.Empty);

        Assert.Equal(inputHash, serializedHash);
      }
    }
  }
}