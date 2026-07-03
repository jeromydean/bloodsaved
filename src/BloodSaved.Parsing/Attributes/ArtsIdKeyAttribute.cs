namespace BloodSaved.Parsing.Attributes
{
  public class ArtsIdKeyAttribute : Attribute
  {
    public string Key { get; }

    public ArtsIdKeyAttribute(string key)
    {
      Key = key;
    }
  }
}
