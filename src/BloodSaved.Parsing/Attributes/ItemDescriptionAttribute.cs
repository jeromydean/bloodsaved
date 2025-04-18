namespace BloodSaved.Parsing.Attributes
{
  public class ItemDescriptionAttribute : Attribute
  {
    public string Description { get; set; }
    public ItemDescriptionAttribute(string description)
    {
      Description = description;
    }
  }
}
