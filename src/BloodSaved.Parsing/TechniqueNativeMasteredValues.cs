using BloodSaved.Parsing.Enums;
using BloodSaved.Parsing.Extensions;

namespace BloodSaved.Parsing
{
  /// <summary>
  /// Use count and experience pairs from a native in-game all-mastered save.
  /// Some techniques (notably Crimson Storm) require matching use/exp pairs, not just threshold exp with use=1.
  /// </summary>
  internal static class TechniqueNativeMasteredValues
  {
    internal static bool TryGet(ArtsId artsId, out int useCount, out int experience)
    {
      if (s_values.TryGetValue(artsId, out (int Use, int Exp) pair))
      {
        useCount = pair.Use;
        experience = pair.Exp;
        return true;
      }

      if (!artsId.HasMastery())
      {
        useCount = 0;
        experience = 0;
        return false;
      }

      useCount = 1;
      experience = TechniqueMasteryThresholdMap.GetMasteryExperience(artsId);
      return true;
    }

    private static readonly Dictionary<ArtsId, (int Use, int Exp)> s_values = new()
    {
      { ArtsId.ForceBlast, (1, 12000) },
      { ArtsId.FlashingAirKick, (4, 5000) },
      { ArtsId.Assassinate, (1, 5000) },
      { ArtsId.SurpriseGift, (1, 8000) },
      { ArtsId.ThousandBlossoms, (2, 50000) },
      { ArtsId.LastingWound, (1, 8000) },
      { ArtsId.OrbitalWheel, (2, 5000) },
      { ArtsId.Penetrate, (1, 8000) },
      { ArtsId.HatchetHeel, (1, 8000) },
      { ArtsId.TrucidatingGyre, (4, 5000) },
      { ArtsId.CriticalSwing, (1, 5000) },
      { ArtsId.Helmsplitter, (5, 5000) },
      { ArtsId.Jinrai, (8, 9008) },
      { ArtsId.CrimsonStorm, (10, 15144) },
      { ArtsId.CrescentStroke, (2, 5000) },
      { ArtsId.Sansetsuzan, (27, 8000) },
      { ArtsId.RapidFire, (4, 8000) },
      { ArtsId.Parry, (5, 5000) },
      { ArtsId.EleventhHour, (3, 5000) },
      { ArtsId.SickleMoon, (10, 8000) },
      { ArtsId.PowerSlash, (1, 5000) },
    };
  }
}
