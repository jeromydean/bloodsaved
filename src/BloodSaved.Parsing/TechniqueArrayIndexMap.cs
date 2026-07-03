using BloodSaved.Parsing.Enums;

namespace BloodSaved.Parsing
{
  /// <summary>
  /// Complete map confirmed from native PC calibration saves (Story_Slot0 series + Den Critical Swing).
  /// Lasting Wound idx 20, Trucidating Gyre idx 27, Force Blast idx 1, Flashing Air Kick idx 5, Assassinate idx 14,
  /// Back Steal idx 16, Thousand Blossoms idx 19, Orbital Wheel idx 21, Penetrate idx 22, Lunging Serpent idx 23,
  /// Hatchet Heel idx 24, Critical Swing idx 28, Helmsplitter idx 30, Jinrai idx 31, Crimson Storm idx 32,
  /// Crescent Stroke idx 33, Sansetsuzan idx 34, Rapid Fire idx 35, Parry idx 36, Eleventh Hour idx 37,
  /// Sickle Moon idx 38, Surprise Gift idx 15, Power Slash idx 47, Brynhild's Blessing idx 46.
  /// </summary>
  internal static class TechniqueArrayIndexMap
  {
    internal static int GetIndex(ArtsId artsId)
    {
      return s_indices[artsId];
    }

    internal static bool TryGetArtsIdForIndex(int arrayIndex, out ArtsId artsId)
    {
      foreach (KeyValuePair<ArtsId, int> entry in s_indices)
      {
        if (entry.Value == arrayIndex)
        {
          artsId = entry.Key;
          return true;
        }
      }

      artsId = default;
      return false;
    }

    private static readonly Dictionary<ArtsId, int> s_indices = new()
    {
      { ArtsId.Assassinate, 14 },
      { ArtsId.SurpriseGift, 15 },
      { ArtsId.PowerSlash, 47 },
      { ArtsId.ForceBlast, 1 },
      { ArtsId.Parry, 36 },
      { ArtsId.BackSteal, 16 },
      { ArtsId.SickleMoon, 38 },
      { ArtsId.TrucidatingGyre, 27 },
      { ArtsId.ThousandBlossoms, 19 },
      { ArtsId.LastingWound, 20 },
      { ArtsId.OrbitalWheel, 21 },
      { ArtsId.Penetrate, 22 },
      { ArtsId.LungingSerpent, 23 },
      { ArtsId.FlashingAirKick, 5 },
      { ArtsId.HatchetHeel, 24 },
      { ArtsId.CriticalSwing, 28 },
      { ArtsId.Jinrai, 31 },
      { ArtsId.Helmsplitter, 30 },
      { ArtsId.CrimsonStorm, 32 },
      { ArtsId.CrescentStroke, 33 },
      { ArtsId.EleventhHour, 37 },
      { ArtsId.Sansetsuzan, 34 },
      { ArtsId.RapidFire, 35 },
      { ArtsId.BrynhildsBlessing, 46 },
    };
  }
}
