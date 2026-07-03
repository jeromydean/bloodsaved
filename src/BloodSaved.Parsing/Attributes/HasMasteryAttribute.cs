namespace BloodSaved.Parsing.Attributes
{
  [AttributeUsage(AttributeTargets.Field)]
  public class HasMasteryAttribute : Attribute
  {
    public bool HasMastery { get; }

    public HasMasteryAttribute(bool hasMastery = true)
    {
      HasMastery = hasMastery;
    }
  }
}
