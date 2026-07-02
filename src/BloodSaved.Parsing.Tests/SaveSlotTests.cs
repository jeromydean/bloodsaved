using System.Security.Cryptography;

namespace BloodSaved.Parsing.Tests
{
  public class SaveSlotTests
  {
    public static IEnumerable<object[]> RoundTripOkStorySaves =>
      TestSavePaths.RoundTripOk.Story.Select(hash => new object[] { hash });

    [Theory]
    [MemberData(nameof(RoundTripOkStorySaves))]
    public void StorySaveRoundTripsWithoutModification(string sha256Hex)
    {
      string saveSlotPath = TestSavePaths.Story(sha256Hex);
      byte[] inputBytes = File.ReadAllBytes(saveSlotPath);
      SaveSlot saveSlot = SaveSlot.Load(saveSlotPath);

      byte[] serializedBytes;
      using (MemoryStream serializedStream = new MemoryStream())
      {
        saveSlot.Save(serializedStream);
        serializedBytes = serializedStream.ToArray();
      }

      Assert.Equal(inputBytes.Length, serializedBytes.Length);

      using SHA256 sha256 = SHA256.Create();
      string inputHash = BitConverter.ToString(sha256.ComputeHash(inputBytes)).Replace("-", string.Empty);
      string serializedHash = BitConverter.ToString(sha256.ComputeHash(serializedBytes)).Replace("-", string.Empty);

      Assert.Equal(inputHash, serializedHash);
    }
  }
}
