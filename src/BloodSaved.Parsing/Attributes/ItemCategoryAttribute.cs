using BloodSaved.Parsing.Enums;

namespace BloodSaved.Parsing.Attributes
{
  public class ItemCategoryAttribute : Attribute
  {
    public ItemCategories Category { get; set; }
    public ItemCategoryAttribute(ItemCategories category)
    {
      Category = category;
    }
  }
}