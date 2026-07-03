using BloodSaved.Parsing.Enums;
using BloodSaved.Parsing.Extensions;

namespace BloodSaved.Parsing
{
  /// <summary>
  /// Per-technique mastery experience caps from PB_DT_ArtsCommandMaster Expertise × 100.
  /// </summary>
  internal static class TechniqueMasteryThresholdMap
  {
    internal static int GetMasteryExperience(ArtsId artsId)
    {
      return s_thresholds[artsId];
    }

    internal static bool IsMasteredExperience(ArtsId artsId, int experience)
    {
      if (!artsId.HasMastery())
      {
        return false;
      }

      return experience >= GetMasteryExperience(artsId);
    }

    private static readonly Dictionary<ArtsId, int> s_thresholds = new()
    {
      { ArtsId.Assassinate, 5000 },
      { ArtsId.SurpriseGift, 8000 },
      { ArtsId.PowerSlash, 5000 },
      { ArtsId.ForceBlast, 12000 },
      { ArtsId.Parry, 5000 },
      { ArtsId.BackSteal, 0 },
      { ArtsId.SickleMoon, 8000 },
      { ArtsId.TrucidatingGyre, 5000 },
      { ArtsId.ThousandBlossoms, 50000 },
      { ArtsId.LastingWound, 8000 },
      { ArtsId.OrbitalWheel, 5000 },
      { ArtsId.Penetrate, 8000 },
      { ArtsId.LungingSerpent, 0 },
      { ArtsId.FlashingAirKick, 5000 },
      { ArtsId.HatchetHeel, 8000 },
      { ArtsId.CriticalSwing, 5000 },
      { ArtsId.Jinrai, 8000 },
      { ArtsId.Helmsplitter, 5000 },
      { ArtsId.CrimsonStorm, 15144 },
      { ArtsId.CrescentStroke, 5000 },
      { ArtsId.EleventhHour, 5000 },
      { ArtsId.Sansetsuzan, 8000 },
      { ArtsId.RapidFire, 8000 },
      { ArtsId.BrynhildsBlessing, 0 },
    };
  }
}
