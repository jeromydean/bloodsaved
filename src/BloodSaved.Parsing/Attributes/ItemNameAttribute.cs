namespace BloodSaved.Parsing.Attributes
{
  public class ItemNameAttribute : Attribute
  {
    public string Name { get; set; }
    public ItemNameAttribute(string name)
    {
      Name = name;
    }
  }
}
