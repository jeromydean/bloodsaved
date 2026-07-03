using BloodSaved.Parsing.Enums;
using BloodSaved.Parsing.Extensions;

namespace BloodSaved.Parsing
{
  internal static class GameRecordArchiveStatKeys
  {
    internal const int MaxUniqueFamiliarsSummoned = 11;

    internal static IReadOnlyList<string> FamiliarSpawnKeys { get; } = Enum.GetValues<ItemId>()
      .Where(itemId => itemId.GetCategory() == ItemCategory.FamiliarShards)
      .Select(itemId => itemId.ToString())
      .OrderBy(key => key, StringComparer.Ordinal)
      .ToArray();
  }
}
