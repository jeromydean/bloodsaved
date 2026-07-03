using BloodSaved.Parsing.Enums;

namespace BloodSaved.Parsing
{
  public static class TechniqueConstants
  {
    public const int ArtsSlotCount = 48;

    /// <summary>Legacy default; real thresholds are per-technique (Expertise × 100).</summary>
    public const int MasteryExperience = 800;

    public static bool IsMasteredExperience(ArtsId artsId, int experience)
    {
      return TechniqueMasteryThresholdMap.IsMasteredExperience(artsId, experience);
    }

    public static int GetMasteryExperience(ArtsId artsId)
    {
      return TechniqueMasteryThresholdMap.GetMasteryExperience(artsId);
    }

    public static bool TryGetNativeMasteredValues(ArtsId artsId, out int useCount, out int experience)
    {
      return TechniqueNativeMasteredValues.TryGet(artsId, out useCount, out experience);
    }
  }
}
