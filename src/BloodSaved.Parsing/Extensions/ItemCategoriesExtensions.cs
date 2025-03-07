using System.ComponentModel;
using System.Reflection;
using BloodSaved.Parsing.Enums;

namespace BloodSaved.Parsing.Extensions
{
  public static class ItemCategoriesExtensions
  {
    public static string GetDescription(this ItemCategories itemCategory)
    {
      FieldInfo? itemCategoryFieldInfo = typeof(ItemCategories).GetField(Enum.GetName(itemCategory));
      DescriptionAttribute? descriptionAttribute = itemCategoryFieldInfo.GetCustomAttribute<DescriptionAttribute>();

      return descriptionAttribute?.Description ?? itemCategory.ToString();
    }
  }
}