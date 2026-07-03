using BloodSaved.Parsing.Attributes;

namespace BloodSaved.Parsing.Enums
{
  /// <summary>
  /// Index into the 48-element m_ArtsUseNum / m_ArtsExperience arrays (EWpnSPEntry save layout).
  /// </summary>
  public enum TechniqueCommandSlot
  {
    [MapsToArts(ArtsId.Assassinate)]
    Assassinate = 0,

    [MapsToArts(ArtsId.SurpriseGift)]
    SurpriseGift = 1,

    [MapsToArts(ArtsId.BackSteal)]
    BackSteal = 2,

    [MapsToArts(ArtsId.ForceBlast)]
    ForceBlast = 3,

    [MapsToArts(ArtsId.Parry)]
    Parry = 4,

    [MapsToArts(ArtsId.PowerSlash)]
    PowerSlash = 5,

    ThrowWeapon = 6,

    [MapsToArts(ArtsId.SickleMoon)]
    SickleMoon = 7,

    [MapsToArts(ArtsId.TrucidatingGyre)]
    TrucidatingGyre = 8,

    [MapsToArts(ArtsId.ThousandBlossoms)]
    ThousandBlossoms = 9,

    [MapsToArts(ArtsId.LastingWound)]
    LastingWound = 10,

    [MapsToArts(ArtsId.Penetrate)]
    Penetrate = 11,

    [MapsToArts(ArtsId.LungingSerpent)]
    LungingSerpent = 12,

    [MapsToArts(ArtsId.OrbitalWheel)]
    OrbitalWheel = 13,

    [MapsToArts(ArtsId.HatchetHeel)]
    HatchetHeel = 14,

    [MapsToArts(ArtsId.FlashingAirKick)]
    FlashingAirKick = 15,

    [MapsToArts(ArtsId.CriticalSwing)]
    CriticalSwing = 16,

    [MapsToArts(ArtsId.CrimsonStorm)]
    CrimsonStorm = 17,

    [MapsToArts(ArtsId.Helmsplitter)]
    Helmsplitter = 18,

    [MapsToArts(ArtsId.Jinrai)]
    Jinrai = 19,

    [MapsToArts(ArtsId.CrescentStroke)]
    CrescentStroke = 20,

    [MapsToArts(ArtsId.Sansetsuzan)]
    Sansetsuzan = 21,

    [MapsToArts(ArtsId.EleventhHour)]
    EleventhHour = 22,

    [MapsToArts(ArtsId.BrynhildsBlessing)]
    Invert = 23,

    [MapsToArts(ArtsId.RapidFire)]
    RapidFire = 24,

    ZangetsuShizukuJin = 25,
    ZangetsuKiriageZan = 26,
    ZangetsuMatterOfVoid = 27,
  }
}
