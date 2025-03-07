using BloodSaved.Parsing.Models;

namespace BloodSaved.Parsing.Sections
{
  public class m_RoomInfo : ISerializableSection<m_RoomInfo>
  {
    public static m_RoomInfo Deserialize(SaveSection saveSection)
    {
      m_RoomInfo m_RoomInfo = new m_RoomInfo();
      using (SaveReader saveReader = new SaveReader(saveSection.Data))
      {
        saveReader.ReadArrayProperty("m_RoomInfo", out _, out int statusDataLength, out int statusDataCount);

        saveReader.ReadStructProperty("m_RoomInfo", out string structname, out int statusDataLengthx, out Guid statusDataCountx);

        for(int i = 0; i < statusDataCount; i++)
        {
          string roomName = saveReader.ReadNameProperty("RoomName");
          bool isComp = saveReader.ReadBoolProperty("IsComp");
          saveReader.VerifyAndReadLengthPrefixedString(SaveConstants.None);
        }

        if (!saveReader.EndOfStream)
        {
          throw new InvalidDataException($"Expected end of '{nameof(m_RoomInfo)}' element.");
        }
      }

      return m_RoomInfo;
    }

    public byte[] Serialize()
    {
      throw new NotImplementedException();
    }
  }
}
