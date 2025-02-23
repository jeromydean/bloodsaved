namespace BloodSaved.Parsing.Attributes
{
  public class ItemIdAttribute : Attribute
  {
    public string ItemId { get; set; }
    public ItemIdAttribute(string itemId)
    {
      ItemId = itemId;
    }
  }
}
