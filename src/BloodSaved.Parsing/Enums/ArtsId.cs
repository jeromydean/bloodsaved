using BloodSaved.Parsing.Attributes;

namespace BloodSaved.Parsing.Enums
{
  public enum ArtsId
  {
    [ArtsIdKey("Arts001"), TechniqueName("Assassinate")]
    Assassinate,

    [ArtsIdKey("Arts002"), TechniqueName("Surprise Gift")]
    SurpriseGift,

    [ArtsIdKey("Arts003"), TechniqueName("Power Slash")]
    PowerSlash,

    [ArtsIdKey("Arts004"), TechniqueName("Force Blast")]
    ForceBlast,

    [ArtsIdKey("Arts005"), TechniqueName("Parry")]
    Parry,

    [ArtsIdKey("Arts006"), TechniqueName("Back Steal"), HasMastery(false)]
    BackSteal,

    [ArtsIdKey("Arts007"), TechniqueName("Sickle Moon")]
    SickleMoon,

    [ArtsIdKey("Arts008"), TechniqueName("Trucidating Gyre")]
    TrucidatingGyre,

    [ArtsIdKey("Arts009"), TechniqueName("Thousand Blossoms")]
    ThousandBlossoms,

    [ArtsIdKey("Arts010"), TechniqueName("Lasting Wound")]
    LastingWound,

    [ArtsIdKey("Arts011"), TechniqueName("Orbital Wheel")]
    OrbitalWheel,

    [ArtsIdKey("Arts012"), TechniqueName("Penetrate")]
    Penetrate,

    [ArtsIdKey("Arts013"), TechniqueName("Lunging Serpent"), HasMastery(false)]
    LungingSerpent,

    [ArtsIdKey("Arts014"), TechniqueName("Flashing Air Kick")]
    FlashingAirKick,

    [ArtsIdKey("Arts015"), TechniqueName("Hatchet Heel")]
    HatchetHeel,

    [ArtsIdKey("Arts016"), TechniqueName("Critical Swing")]
    CriticalSwing,

    [ArtsIdKey("Arts017"), TechniqueName("Jinrai")]
    Jinrai,

    [ArtsIdKey("Arts018"), TechniqueName("Helmsplitter")]
    Helmsplitter,

    [ArtsIdKey("Arts019"), TechniqueName("Crimson Storm")]
    CrimsonStorm,

    [ArtsIdKey("Arts020"), TechniqueName("Crescent Stroke")]
    CrescentStroke,

    [ArtsIdKey("Arts021"), TechniqueName("Eleventh Hour")]
    EleventhHour,

    [ArtsIdKey("Arts022"), TechniqueName("Sansetsuzan")]
    Sansetsuzan,

    [ArtsIdKey("Arts023"), TechniqueName("Rapid Fire")]
    RapidFire,

    /// <summary>Hidden Valkyrie Sword technique (EWpnSPEntry::Invert). Not in Archives.</summary>
    [TechniqueName("Brynhild's Blessing"), HasMastery(false)]
    BrynhildsBlessing,
  }
}
