using BloodSaved.Parsing.Models;

namespace BloodSaved.Parsing.Sections
{
  public class m_Traverse : ISerializableSection<m_Traverse>
  {
    public const int TraverseDataLength = 20000;

    public byte[] TraverseData { get; private set; } = new byte[TraverseDataLength];

    public static m_Traverse Deserialize(SaveSection saveSection)
    {
      m_Traverse m_Traverse = new m_Traverse();
      using SaveReader saveReader = new SaveReader(saveSection.Data);
      saveReader.ReadArrayProperty("m_Traverse", out _, out _, out int statusDataCount);

      if (statusDataCount != TraverseDataLength)
      {
        throw new InvalidDataException($"Expected {TraverseDataLength} bytes of traversal data.");
      }

      //200 block X axis by 100 block Y axis = 20000
      m_Traverse.TraverseData = saveReader.ReadBytes(statusDataCount);

      if (!saveReader.EndOfStream)
      {
        throw new InvalidDataException($"Expected end of '{nameof(m_Traverse)}' element.");
      }

      return m_Traverse;
    }

    public void SetFullyDiscovered()
    {
      for (int i = 0; i < SaveConstants.CompleteMap.Length; i++)
      {
        if (SaveConstants.CompleteMap[i] != 0)
        {
          TraverseData[i] = 1;
        }
      }
    }

    public byte[] Serialize()
    {
      using SaveWriter saveWriter = new SaveWriter();
      saveWriter.WriteArrayProperty("m_Traverse", SaveConstants.ByteProperty, out long traverseLengthOffset, out _, count: TraverseDataLength);
      long startStructureStartOffset = saveWriter.CurrentPosition - 4;

      saveWriter.Write(TraverseData);

      int structureLength = (int)(saveWriter.CurrentPosition - startStructureStartOffset);
      saveWriter.SetCheckpoint();
      saveWriter.SetPosition(traverseLengthOffset);
      saveWriter.Write(structureLength);
      saveWriter.Reset();

      return saveWriter.ToArray();
    }
  }
}
