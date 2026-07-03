namespace BloodSaved.Parsing.Attributes
{
  public class TechniqueNameAttribute : Attribute
  {
    public string Name { get; }

    public TechniqueNameAttribute(string name)
    {
      Name = name;
    }
  }
}
