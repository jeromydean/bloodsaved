namespace BloodSaved.Parsing.Attributes
{
  /// <summary>
  /// Index into the 48-element m_ArtsUseNum / m_ArtsExperience arrays.
  /// This order matches EWpnSPEntry layout in save data, not TechniqueCommandSlot.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field)]
  public class TechniqueArrayIndexAttribute : Attribute
  {
    public int Index { get; }

    public TechniqueArrayIndexAttribute(int index)
    {
      Index = index;
    }
  }
}
