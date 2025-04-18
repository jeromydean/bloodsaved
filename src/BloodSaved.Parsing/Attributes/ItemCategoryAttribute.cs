using BloodSaved.Parsing.Enums;

namespace BloodSaved.Parsing.Attributes
{
  public class ItemCategoryAttribute : Attribute
  {
    public ItemCategory Category { get; set; }
    public ItemCategoryAttribute(ItemCategory category)
    {
      Category = category;
    }
  }
}