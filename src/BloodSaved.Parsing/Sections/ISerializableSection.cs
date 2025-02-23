namespace BloodSaved.Parsing.Sections
{
  public interface ISerializableSection<T>
  {
    static abstract T Deserialize(byte[] serialized);
    byte[] Serialize();
  }
}