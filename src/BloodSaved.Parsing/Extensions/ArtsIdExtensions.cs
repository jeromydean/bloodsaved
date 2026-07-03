using System.Reflection;
using BloodSaved.Parsing.Attributes;
using BloodSaved.Parsing.Enums;

namespace BloodSaved.Parsing.Extensions
{
  public static class ArtsIdExtensions
  {
    public static bool HasCodexKey(this ArtsId artsId)
    {
      FieldInfo? field = typeof(ArtsId).GetField(artsId.ToString());
      return field?.GetCustomAttribute<ArtsIdKeyAttribute>() != null;
    }

    public static string GetArtsIdKey(this ArtsId artsId)
    {
      FieldInfo? field = typeof(ArtsId).GetField(artsId.ToString());
      ArtsIdKeyAttribute? keyAttribute = field?.GetCustomAttribute<ArtsIdKeyAttribute>();
      return keyAttribute?.Key ?? artsId.ToString();
    }

    public static string GetTechniqueName(this ArtsId artsId)
    {
      FieldInfo? field = typeof(ArtsId).GetField(artsId.ToString());
      TechniqueNameAttribute? nameAttribute = field?.GetCustomAttribute<TechniqueNameAttribute>();
      return nameAttribute?.Name ?? artsId.ToString();
    }

    public static bool TryParseArtsIdKey(string key, out ArtsId artsId)
    {
      foreach (ArtsId candidate in Enum.GetValues<ArtsId>())
      {
        if (string.Equals(candidate.GetArtsIdKey(), key, StringComparison.Ordinal))
        {
          artsId = candidate;
          return true;
        }
      }

      artsId = default;
      return false;
    }

    public static ArtsId? GetArtsId(this TechniqueCommandSlot slot)
    {
      FieldInfo? field = typeof(TechniqueCommandSlot).GetField(slot.ToString());
      MapsToArtsAttribute? mapsAttribute = field?.GetCustomAttribute<MapsToArtsAttribute>();
      return mapsAttribute?.ArtsId;
    }

    public static bool HasMastery(this ArtsId artsId)
    {
      FieldInfo? field = typeof(ArtsId).GetField(artsId.ToString());
      HasMasteryAttribute? masteryAttribute = field?.GetCustomAttribute<HasMasteryAttribute>();
      return masteryAttribute?.HasMastery ?? true;
    }

    public static TechniqueCommandSlot? GetCommandSlot(this ArtsId artsId)
    {
      foreach (TechniqueCommandSlot slot in Enum.GetValues<TechniqueCommandSlot>())
      {
        if (slot.GetArtsId() == artsId)
        {
          return slot;
        }
      }

      return null;
    }

    public static int GetArrayIndex(this ArtsId artsId)
    {
      FieldInfo? field = typeof(ArtsId).GetField(artsId.ToString());
      TechniqueArrayIndexAttribute? indexAttribute = field?.GetCustomAttribute<TechniqueArrayIndexAttribute>();
      if (indexAttribute != null)
      {
        return indexAttribute.Index;
      }

      return TechniqueArrayIndexMap.GetIndex(artsId);
    }

    public static bool TryGetArtsIdForArrayIndex(int arrayIndex, out ArtsId artsId)
    {
      foreach (ArtsId candidate in Enum.GetValues<ArtsId>())
      {
        if (candidate.GetArrayIndex() == arrayIndex)
        {
          artsId = candidate;
          return true;
        }
      }

      return TechniqueArrayIndexMap.TryGetArtsIdForIndex(arrayIndex, out artsId);
    }
  }
}
