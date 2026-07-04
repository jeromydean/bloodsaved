using BloodSaved.Parsing.Models;

namespace BloodSaved.Parsing.Sections
{
  public class m_RoomInfo : ISerializableSection<m_RoomInfo>
  {
    private const string SectionName = "m_RoomInfo";
    private const string StructName = "PBSaveGameDataRoominfo";
    private const string RoomNamePropertyName = "RoomName";
    private const string IsCompletedPropertyName = "IsComp";

    public List<RoomInfoEntry> Rooms { get; } = [];
    public bool IsDirty { get; private set; }
    private Guid _structId = Guid.Empty;

    public static m_RoomInfo Deserialize(SaveSection saveSection)
    {
      m_RoomInfo m_RoomInfo = new m_RoomInfo();
      using (SaveReader saveReader = new SaveReader(saveSection.Data))
      {
        saveReader.ReadArrayProperty(SectionName, out string arrayType, out _, out int roomCount);
        if (!string.Equals(arrayType, SaveConstants.StructProperty, StringComparison.Ordinal))
        {
          throw new InvalidDataException($"{SectionName} array type of '{arrayType}' is not correct, expected StructProperty.");
        }

        saveReader.ReadStructProperty(SectionName, out string structName, out _, out Guid structId);
        if (!string.Equals(structName, StructName, StringComparison.Ordinal))
        {
          throw new InvalidDataException($"{SectionName} struct type of '{structName}' is not correct, expected {StructName}.");
        }

        m_RoomInfo._structId = structId;
        for (int i = 0; i < roomCount; i++)
        {
          string roomName = saveReader.ReadNameProperty(RoomNamePropertyName);
          bool isCompleted = saveReader.ReadBoolProperty(IsCompletedPropertyName);
          saveReader.VerifyAndReadLengthPrefixedString(SaveConstants.None);

          m_RoomInfo.Rooms.Add(new RoomInfoEntry(roomName, isCompleted));
        }

        if (!saveReader.EndOfStream)
        {
          throw new InvalidDataException($"Expected end of '{nameof(m_RoomInfo)}' element.");
        }
      }

      return m_RoomInfo;
    }

    public void SetAllRoomsCompleted()
    {
      foreach (RoomInfoEntry room in Rooms)
      {
        room.IsCompleted = true;
      }

      IsDirty = true;
    }

    public void SetCompletedRooms(IEnumerable<string> roomNames)
    {
      SetAllRoomsCompleted();

      Dictionary<string, int> requiredCounts = roomNames
        .GroupBy(roomName => roomName, StringComparer.Ordinal)
        .ToDictionary(group => group.Key, group => group.Count(), StringComparer.Ordinal);
      Dictionary<string, int> currentCounts = Rooms
        .GroupBy(room => room.RoomName, StringComparer.Ordinal)
        .ToDictionary(group => group.Key, group => group.Count(), StringComparer.Ordinal);

      foreach ((string roomName, int requiredCount) in requiredCounts)
      {
        currentCounts.TryGetValue(roomName, out int currentCount);
        for (int i = currentCount; i < requiredCount; i++)
        {
          Rooms.Add(new RoomInfoEntry(roomName, isCompleted: true));
        }
      }

      IsDirty = true;
    }

    public byte[] Serialize()
    {
      using SaveWriter saveWriter = new SaveWriter();
      saveWriter.WriteArrayProperty(SectionName, SaveConstants.StructProperty, out long lengthOffset, out long countOffset, count: Rooms.Count);
      saveWriter.WriteStructProperty(SectionName, StructName, _structId, out long structLengthOffset);
      long payloadStart = saveWriter.CurrentPosition;

      foreach (RoomInfoEntry room in Rooms)
      {
        saveWriter.WriteNameProperty(RoomNamePropertyName, room.RoomName);
        saveWriter.WriteBoolProperty(IsCompletedPropertyName, room.IsCompleted);
        saveWriter.WriteLengthPrefixedString(SaveConstants.None);
      }

      long resumePosition = saveWriter.CurrentPosition;
      saveWriter.SetPosition(structLengthOffset);
      saveWriter.Write((int)(resumePosition - payloadStart));
      saveWriter.SetPosition(resumePosition);

      PatchArrayLengthOnly(saveWriter, lengthOffset, countOffset);
      return saveWriter.ToArray();
    }

    private static void PatchArrayLengthOnly(SaveWriter saveWriter, long lengthOffset, long countOffset)
    {
      long resumePosition = saveWriter.CurrentPosition;
      saveWriter.SetPosition(lengthOffset);
      saveWriter.Write((int)(resumePosition - countOffset));
      saveWriter.SetPosition(resumePosition);
    }
  }

  public class RoomInfoEntry(string roomName, bool isCompleted)
  {
    public string RoomName { get; } = roomName;
    public bool IsCompleted { get; set; } = isCompleted;
  }
}
