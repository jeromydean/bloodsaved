using System.Reflection;
using System.Text;
using BloodSaved.Parsing.Attributes;
using BloodSaved.Parsing.Enums;

namespace BloodSaved.Parsing.Extensions
{
  public static class ItemIdsExtensions
  {
    public static string GetName(this ItemId itemId)
    {
      FieldInfo? itemIdFieldInfo = typeof(ItemId).GetField(Enum.GetName(itemId));
      ItemNameAttribute? nameAttribute = itemIdFieldInfo.GetCustomAttribute<ItemNameAttribute>();

      return nameAttribute?.Name ?? itemId.GetIdString();
    }

    public static string GetIdString(this ItemId itemId)
    {
      FieldInfo? itemIdFieldInfo = typeof(ItemId).GetField(Enum.GetName(itemId));
      ItemIdAttribute? itemIdAttribute = itemIdFieldInfo.GetCustomAttribute<ItemIdAttribute>();

      return itemIdAttribute?.ItemId ?? itemId.ToString();
    }

    public static ItemCategory GetCategory(this ItemId itemId)
    {
      FieldInfo? itemIdFieldInfo = typeof(ItemId).GetField(Enum.GetName(itemId));
      ItemCategoryAttribute itemCategoriesAttribute = itemIdFieldInfo.GetCustomAttribute<ItemCategoryAttribute>();
      return itemCategoriesAttribute.Category;
    }

    public static string GetDescription(this ItemId itemId)
    {
      FieldInfo? itemIdFieldInfo = typeof(ItemId).GetField(Enum.GetName(itemId));
      ItemDescriptionAttribute? descriptionAttribute = itemIdFieldInfo.GetCustomAttribute<ItemDescriptionAttribute>();

      return descriptionAttribute?.Description ?? itemId.ToString();
    }

    public static Encoding GetEncoding(this ItemId itemId)
    {
      FieldInfo? itemIdFieldInfo = typeof(ItemId).GetField(Enum.GetName(itemId));
      SerializedAsUnicodeAttribute? unicodeSerializationAttribute = itemIdFieldInfo.GetCustomAttribute<SerializedAsUnicodeAttribute>();

      return unicodeSerializationAttribute != null
        ? Encoding.Unicode
        : Encoding.UTF8;
    }
  }
}