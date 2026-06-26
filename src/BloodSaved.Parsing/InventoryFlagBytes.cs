using BloodSaved.Parsing.Enums;
using BloodSaved.Parsing.Extensions;

namespace BloodSaved.Parsing;

/// <summary>
/// Known semantics for the 16-byte inventory item flag trailer (non-skill-shard items).
/// </summary>
public static class InventoryFlagBytes
{
  public const int Size = 16;

  /// <summary>Food only: player received the First Time Bonus for this dish.</summary>
  public const int FirstTimeFoodBonusOffset = 0;

  /// <summary>Item acquired / registered in the collection.</summary>
  public const int AcquiredOffset = 4;

  /// <summary>Item available in Dominique's shop or equivalent store listing.</summary>
  public const int InStoreOffset = 8;

  public static byte[] CreateEmpty() => new byte[Size];

  public static bool HasFirstTimeFoodBonus(ReadOnlySpan<byte> bytes) => bytes[FirstTimeFoodBonusOffset] == 0x01;

  public static bool IsAcquired(ReadOnlySpan<byte> bytes) => bytes[AcquiredOffset] == 0x01;

  public static bool IsInStore(ReadOnlySpan<byte> bytes) => bytes[InStoreOffset] == 0x01;

  public static byte[] ForFood(bool firstTimeBonusReceived, bool inStore)
  {
    byte[] bytes = CreateEmpty();
    if (firstTimeBonusReceived)
    {
      bytes[FirstTimeFoodBonusOffset] = 0x01;
    }

    if (inStore)
    {
      bytes[InStoreOffset] = 0x01;
    }

    return bytes;
  }

  public static byte[] Copy(ReadOnlySpan<byte> bytes)
  {
    byte[] copy = CreateEmpty();
    bytes.CopyTo(copy);
    return copy;
  }

  public static bool IsEmpty(ReadOnlySpan<byte> bytes)
  {
    for (int i = 0; i < Size; i++)
    {
      if (bytes[i] != 0)
      {
        return false;
      }
    }

    return true;
  }

  /// <summary>
  /// Default flags when an item is newly added to inventory (e.g. via the editor).
  /// </summary>
  public static byte[] DefaultForNewInInventory(ItemId itemId)
  {
    ItemCategory category = itemId.GetCategory();

    if (category == ItemCategory.Food)
    {
      return ForFood(firstTimeBonusReceived: false, inStore: true);
    }

    if (category == ItemCategory.Key
      || category == ItemCategory.Accessory
      || category == ItemCategory.Materials
      || itemId == ItemId.Corn)
    {
      byte[] bytes = CreateEmpty();
      bytes[AcquiredOffset] = 0x01;
      return bytes;
    }

    if (category == ItemCategory.Weapon
      || category == ItemCategory.Bullet
      || category == ItemCategory.BodyArmor
      || category == ItemCategory.HeadArmor
      || category == ItemCategory.Scarves
      || category == ItemCategory.Potion
      || category == ItemCategory.Ingredients
      || category == ItemCategory.ConjureShards
      || category == ItemCategory.DirectionalShards
      || category == ItemCategory.PassiveShards
      || category == ItemCategory.ManipulativeShards
      || category == ItemCategory.FamiliarShards)
    {
      byte[] bytes = CreateEmpty();
      bytes[AcquiredOffset] = 0x01;
      bytes[InStoreOffset] = 0x01;
      return bytes;
    }

    return CreateEmpty();
  }

  /// <summary>
  /// Default flags for cheat/add-all style inventory grants.
  /// </summary>
  public static byte[] DefaultForAddAll(ItemId itemId)
  {
    ItemCategory category = itemId.GetCategory();

    if (category == ItemCategory.Food)
    {
      return ForFood(firstTimeBonusReceived: true, inStore: true);
    }

    return DefaultForNewInInventory(itemId);
  }

  public static byte[] ResolveForInventoryItem(ItemId itemId, ReadOnlySpan<byte> flagBytes)
  {
    if (!IsEmpty(flagBytes))
    {
      return Copy(flagBytes);
    }

    return DefaultForNewInInventory(itemId);
  }

  /// <summary>
  /// Returns true when only known flag offsets are set for the item category.
  /// </summary>
  public static bool IsKnownPattern(ItemCategory category, ReadOnlySpan<byte> bytes)
  {
    for (int i = 0; i < Size; i++)
    {
      if (i is FirstTimeFoodBonusOffset or AcquiredOffset or InStoreOffset)
      {
        continue;
      }

      if (bytes[i] != 0)
      {
        return false;
      }
    }

    if (category == ItemCategory.Food)
    {
      // Food primarily uses [0] (first-time bonus) and [8] (in store). [4] appears rarely.
      return true;
    }

    return bytes[FirstTimeFoodBonusOffset] == 0;
  }
}
