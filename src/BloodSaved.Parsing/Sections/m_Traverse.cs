using BloodSaved.Parsing.Models;

namespace BloodSaved.Parsing.Sections
{
  public class m_Traverse : ISerializableSection<m_Traverse>
  {
    public byte[] TraverseData { get; private set; }
    public static m_Traverse Deserialize(SaveSection saveSection)
    {
      m_Traverse m_Traverse = new m_Traverse();
      using (SaveReader saveReader = new SaveReader(saveSection.Data))
      {
        saveReader.ReadArrayProperty("m_Traverse", out _, out int statusDataLength, out int statusDataCount);

        if (statusDataCount != 20000)
        {
          throw new InvalidDataException($"Expected 20000 bytes of traversal data.");
        }

        //200 block X axis by 100 block Y axis = 20000
        m_Traverse.TraverseData = saveReader.ReadBytes(statusDataCount);

        if (!saveReader.EndOfStream)
        {
          throw new InvalidDataException($"Expected end of '{nameof(m_Traverse)}' element.");
        }
      }

      return m_Traverse;
    }

    public byte[] Serialize()
    {
      throw new NotImplementedException();
    }
  }
}
