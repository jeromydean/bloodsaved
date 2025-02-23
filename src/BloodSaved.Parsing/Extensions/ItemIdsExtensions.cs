using System.ComponentModel;
using System.Reflection;
using System.Text;
using BloodSaved.Parsing.Attributes;
using BloodSaved.Parsing.Enums;

namespace BloodSaved.Parsing.Extensions
{
  public static class ItemIdsExtensions
  {
    public static string GetIdString(this ItemIds itemId)
    {
      FieldInfo? itemIdFieldInfo = typeof(ItemIds).GetField(Enum.GetName(itemId));
      ItemIdAttribute? itemIdAttribute = itemIdFieldInfo.GetCustomAttribute<ItemIdAttribute>();

      return itemIdAttribute?.ItemId ?? itemId.ToString();
    }

    public static ItemCategories GetCategory(this ItemIds itemId)
    {
      FieldInfo? itemIdFieldInfo = typeof(ItemIds).GetField(Enum.GetName(itemId));
      ItemCategoryAttribute itemCategoriesAttribute = itemIdFieldInfo.GetCustomAttribute<ItemCategoryAttribute>();
      return itemCategoriesAttribute.Category;
    }

    public static string GetDescription(this ItemIds itemId)
    {
      FieldInfo? itemIdFieldInfo = typeof(ItemIds).GetField(Enum.GetName(itemId));
      DescriptionAttribute? descriptionAttribute = itemIdFieldInfo.GetCustomAttribute<DescriptionAttribute>();

      return descriptionAttribute?.Description ?? itemId.ToString();
    }

    public static Encoding GetEncoding(this ItemIds itemId)
    {
      FieldInfo? itemIdFieldInfo = typeof(ItemIds).GetField(Enum.GetName(itemId));
      SerializedAsUnicodeAttribute? unicodeSerializationAttribute = itemIdFieldInfo.GetCustomAttribute<SerializedAsUnicodeAttribute>();

      return unicodeSerializationAttribute != null
        ? Encoding.Unicode
        : Encoding.UTF8;
    }
  }
}