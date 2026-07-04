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
      /// <summary>Repo-root Story_Slot0.sav — calibration baseline with no technique progress.</summary>
      public const string RootStorySlot0Baseline = "45403e70b21b6a6e30cb0d531c312bae017598f1c8571376dda326da6b5b6f8d";
      /// <summary>Repo-root Story_Slot0.sav — single non-mastered Surprise Gift (use=1 at index 15).</summary>
      public const string RootStorySlot0SurpriseGiftPartial = "717662cff793199a4302e0255ecdd2499ebd33a8312e5e6619b53e8c60e8ab8f";
      /// <summary>Repo-root Story_Slot0.sav — Surprise Gift + Power Slash partial progress (not mastered).</summary>
      public const string RootStorySlot0SurpriseGiftAndPowerSlashPartial = "b34333149e77e6145e65d22132d43597f031e86689df022f9315604588c44aef";
      /// <summary>Repo-root Story_Slot0.sav — Surprise Gift + Power Slash + Force Blast partial progress (not mastered).</summary>
      public const string RootStorySlot0SurpriseGiftPowerSlashAndForceBlastPartial = "303bd703e2fd97a7c206cb37ece324c84a86b46ef4d957a23292e30e051247cb";
      /// <summary>Repo-root Story_Slot0.sav — Surprise Gift, Power Slash, Force Blast, and Parry partial progress (not mastered).</summary>
      public const string RootStorySlot0FourTechniquesPartial = "0ec59d35d1c90c7b930e7a3af770d1af4fbc8c5fe2c277822d28d85483697a58";
      /// <summary>Repo-root Story_Slot0.sav — five techniques partial progress including Rapid Fire (not mastered).</summary>
      public const string RootStorySlot0FiveTechniquesPartial = "c3c91580008d23729fc3471d77619912378ce908d1090eaef3b311b787baeee9";
      /// <summary>Repo-root Story_Slot0.sav — six techniques partial progress including Eleventh Hour (not mastered).</summary>
      public const string RootStorySlot0SixTechniquesPartial = "eae230ecae3c7f54aa2d21339293fff807962f140e3803a4d5e5b5ada399cfb1";
      /// <summary>Repo-root Story_Slot0.sav — seven techniques partial progress including Assassinate (not mastered).</summary>
      public const string RootStorySlot0SevenTechniquesPartial = "4356ddb5bbdd2be6ff5527a320b25a2698f0a21c0a704e8057440286a29edff1";
      /// <summary>Repo-root Story_Slot0.sav — eight techniques partial progress including Back Steal (not mastered).</summary>
      public const string RootStorySlot0EightTechniquesPartial = "5c89ca6d1f7421790cdf39f93d59bde2f4e286ef21169507ce5dfec6c0bf3e0a";
      /// <summary>Repo-root Story_Slot0.sav — nine techniques partial progress including Thousand Blossoms (not mastered).</summary>
      public const string RootStorySlot0NineTechniquesPartial = "5dd292d5dbc6b7547795eea89367d5f617b97dd1ff4ced4c6e7785613762533d";
      /// <summary>Repo-root Story_Slot0.sav — ten techniques partial progress including Hatchet Heel (not mastered).</summary>
      public const string RootStorySlot0TenTechniquesPartial = "45afd9ba76d9a1b0a1283b4c90e8e76f2a4837433b2c4bf2815689300663a9b1";
      /// <summary>Repo-root Story_Slot0.sav — eleven techniques partial progress including Flashing Air Kick (not mastered).</summary>
      public const string RootStorySlot0ElevenTechniquesPartial = "10575c52921d1cd833e336e1e4129e1b73b7d2eec0a6be60fdaad41cf282f359";
      /// <summary>Repo-root Story_Slot0.sav — twelve techniques partial progress including Orbital Wheel (not mastered).</summary>
      public const string RootStorySlot0TwelveTechniquesPartial = "3f083e4eb026a7b68b8f0202060549ae26fd146a1415a955cbcb1717271be057";
      /// <summary>Repo-root Story_Slot0.sav — thirteen techniques partial progress including Penetrate (not mastered).</summary>
      public const string RootStorySlot0ThirteenTechniquesPartial = "09862e1229eca3bbaca8cf2196d7e386d97ce0ada27701586eac5a9307589229";
      /// <summary>Repo-root Story_Slot0.sav — fourteen techniques partial progress including Lunging Serpent (not mastered).</summary>
      public const string RootStorySlot0FourteenTechniquesPartial = "edc9ff48499908fee90b5f5b7a420f9ebb5fe5cb4722dc5589c3740dc6ef1c28";
      /// <summary>Repo-root Story_Slot0.sav — fifteen techniques partial progress including Sickle Moon (not mastered).</summary>
      public const string RootStorySlot0FifteenTechniquesPartial = "8308f77c8b8118b925151aaf5c6dbe634cee9c2b9a15da62b6980286caff401a";
      /// <summary>Repo-root Story_Slot0.sav — sixteen techniques partial progress including Sansetsuzan and accidental Crimson Storm (not mastered).</summary>
      public const string RootStorySlot0SixteenTechniquesPartial = "5104576e20370a4778856cb2ba9fbd6a1a9c141eba6a7a5efcfd88d9de78a518";
      /// <summary>Repo-root Story_Slot0.sav — seventeen techniques partial progress including Jinrai (not mastered).</summary>
      public const string RootStorySlot0SeventeenTechniquesPartial = "4aedc7610a63dfcbbae2f3b4adfda550ca85d0ca177adcdcc2756ebfba444231";
      /// <summary>Repo-root Story_Slot0.sav — eighteen techniques partial progress including Helmsplitter (not mastered).</summary>
      public const string RootStorySlot0EighteenTechniquesPartial = "49b9a304d20c8b5927eb1078dfd03fff519f8bf95b98f349ee7cef006574a617";
      /// <summary>Repo-root Story_Slot0.sav — nineteen techniques partial progress including Crescent Stroke (not mastered).</summary>
      public const string RootStorySlot0NineteenTechniquesPartial = "4e2801f58dabe8b5e3fdcffc4e5812327e5a8b8a10bf40e4fb4590fd58a7e831";
      /// <summary>Repo-root Story_Slot0.sav — twenty techniques partial progress including Trucidating Gyre (not mastered).</summary>
      public const string RootStorySlot0TwentyTechniquesPartial = "354c47e1970d42e866edbe073792a3245d5e90abc481fdac860255f4be2a3cb2";
      /// <summary>Repo-root Story_Slot0.sav — all masterable techniques mastered (native in-game Crimson Storm completion).</summary>
      public const string RootStorySlot0AllTechniquesMastered = "e501acfa279b5122101bf1957855066ad826cada560bd482fd5bc9e326f2e788";
      /// <summary>Repo-root Story_Slot0.sav — all techniques mastered plus Brynhild's Blessing performed (use at index 46).</summary>
      public const string RootStorySlot0BrynhildsBlessingPerformed = "07d36f5b6d911dac96dc52c96f2d597379c562d2012fd388ff736c2b910f39a1";
      /// <summary>Repo-root Story_Slot0.sav — all masterable techniques with partial progress including Lasting Wound (not mastered).</summary>
      public const string RootStorySlot0CompleteTechniqueMap = "b48879db7b47f79cfee4f403413517668fbfa10fe623ed6bc87060f0c90f7165";
      public const string ForceBlastPartialProgress = "d77797bc0da816cd708f8d3f6922b13b1be957120c517b6a05e2b5d781f050f5";
      public const string NativeAllQuestsComplete = "d77797bc0da816cd708f8d3f6922b13b1be957120c517b6a05e2b5d781f050f5";
      public const string StartOfNormalMiriamGame = "d777a2e330b4467dcc9e4c9eb974a19410fb0a80239dfcc3959f1696958f73b9";
      /// <summary>Repo-root Story_Slot0.sav — before first Arvantville warp-room entry.</summary>
      public const string BeforeArvantvilleWarpRoomEntry = "357dea583de46e8e07a583cf511959a9d32ab43fc1c30d65e443b141f6df0764";
      /// <summary>Repo-root Story_Slot0.sav — after first Arvantville warp-room entry.</summary>
      public const string AfterArvantvilleWarpRoomEntry = "4666bdb1dc78191287a7a2fe04f9396c71df5c716e695a057d8223dfadc862f4";
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
