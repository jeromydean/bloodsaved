namespace BloodSaved.Parsing.Tests
{
  internal static class TestSavePaths
  {
    private static readonly string SavesRoot = Path.Combine(AppContext.BaseDirectory, "Resources", "Saves");

    public static string Story(string sha256Hex) =>
      Path.Combine(SavesRoot, "story", $"{sha256Hex.ToLowerInvariant()}.sav");

    public static string System(string sha256Hex) =>
      Path.Combine(SavesRoot, "system", $"{sha256Hex.ToLowerInvariant()}.sav");

    public static class StorySaves
    {
      public const string DenOfBehemoths = "03a1ffd512128c6e4933fdbf808545d882eb15a301e6520ea12fdea07a188711";
      public const string ArvantvilleNoCheatNoBonus = "cccf93abb196a49f1ebde7d085a77216ebc9523de29c1eb7260743586a8fa0d2";
      public const string SevenMasteredTechniques = "7704ccea7ab28a34b02b21a4f29882d5717f6438ae32631c19b102a66a0e346a";
      public const string ForceBlastPartialProgress = "d77797bc0da816cd708f8d3f6922b13b1be957120c517b6a05e2b5d781f050f5";
    }

    public static class RoundTripOk
    {
      public static readonly string[] Story =
      [
        "307bc98a4a802a1b7df0e38acc7a1e54d929150b67c479443dacf3ef3decfc55",
        "50797c9a82580aa1534f2702302e324e779b57718f6f081fee7ae33c8c654f22",
        "7406d05dcf770465515735cdc7ac4c531c7ae477b3b3f9cd7fcd2da9f17509f7",
        "7e3e92c6b0b675198fcaf69c18f63742f247d3610026b662efaec1e6201d2b3d",
        "9773f58b15772e29c9a1af36a40d58e2d016cb28953545d04a817155f2adcd29",
        "bc3c71902aa81c7d8b900fe25bba88396a88af742f0819fab7c06111d6488ab6",
        "cccf93abb196a49f1ebde7d085a77216ebc9523de29c1eb7260743586a8fa0d2",
        "dfee77994687636deaae23930326d9c07c5a66fb16b4426fc2b360af77071675",
        "e2bc65307d06fd502b698032116ef1189a4c8142931d4169ddaa6bdddcd2595e",
        "f8767ddc7b5aebce8e61ab2e139c51190838ae28fb0af685d394d1fc1875329d",
      ];
    }
  }
}
