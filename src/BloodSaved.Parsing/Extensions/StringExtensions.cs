using System.Reflection;
using BloodSaved.Parsing.Attributes;
using BloodSaved.Parsing.Enums;

namespace BloodSaved.Parsing.Extensions
{
  public static class StringExtensions
  {
    public static ItemId ToItemId(this string value)
    {
      //fixing my previous mistakes on not serializing these as unicode
      if (string.Equals(value, "HeyI???mGrump"))
      {
        value = "HeyI’mGrump";
      }
      else if (string.Equals(value, "I???mNotSoGrump"))
      {
        value = "I’mNotSoGrump";
      }

      if (Enum.TryParse<ItemId>(value, out ItemId id))
      {
        return id;
      }

      foreach (ItemId itemId in Enum.GetValues<ItemId>())
      {
        FieldInfo? itemIdFieldInfo = typeof(ItemId).GetField(itemId.ToString());
        ItemIdAttribute? itemIdAttribute = itemIdFieldInfo.GetCustomAttribute<ItemIdAttribute>();

        if (string.Equals(value, itemId.ToString(), StringComparison.OrdinalIgnoreCase)
          || (itemIdAttribute != null && string.Equals(value, itemIdAttribute.ItemId, StringComparison.OrdinalIgnoreCase)))
        {
          return itemId;
        }
      }

      throw new InvalidDataException($"Unknown item id '{value}'.");
    }
  }
}