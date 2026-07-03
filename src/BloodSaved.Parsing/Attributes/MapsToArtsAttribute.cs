using BloodSaved.Parsing.Enums;

namespace BloodSaved.Parsing.Attributes
{
  public class MapsToArtsAttribute : Attribute
  {
    public ArtsId ArtsId { get; }

    public MapsToArtsAttribute(ArtsId artsId)
    {
      ArtsId = artsId;
    }
  }
}
