using BloodSaved.Parsing.Models;

namespace BloodSaved.Parsing.Sections
{
  public interface ISerializableSection<T>
  {
    static abstract T Deserialize(SaveSection saveSection);
    byte[] Serialize();
  }
}