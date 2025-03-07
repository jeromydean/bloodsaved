using BloodSaved.Parsing.Enums;
using BloodSaved.Parsing.Models;

namespace BloodSaved.Parsing.Sections
{
  public class Info : ISerializableSection<Info>
  {
    private List<SaveSection> _saveSections;
    //Debug.WriteLine($"Info - name='{name}', type='{type}'");
    //Info - name='Version', type='IntProperty'
    //Info - name='SlotName', type='StrProperty'
    //Info - name='DateTime', type='StructProperty'
    //Info - name='RoomID', type='NameProperty'
    //Info - name='AreaID', type='NameProperty'
    //Info - name='Level', type='IntProperty'
    //Info - name='MapCompleteness', type='FloatProperty'
    //Info - name='GameLevel', type='ByteProperty'
    //Info - name='EPBGameLevel', type='Info - name='TotalKills', type='IntProperty'
    //Info - name='totalCoins', type='IntProperty'
    //Info - name='TotalPlaySec', type='FloatProperty'
    //Info - name='GameModeType', type='ByteProperty'
    //Info - name='EPBGameModeType', type='Info - name='TrueEndCount', type='IntProperty'
    //Info - name='BadEndCount', type='IntProperty'
    //Info - name='CanInherite', type='BoolProperty'
    //Info - name='SavedToDisk', type='BoolProperty'
    //Info - name='HasDLCs', type='ArrayProperty'

    public int Version
    {
      get;
      private set;
    }

    public int TotalCoins
    {
      get;
      private set;
    }

    public string SlotName
    {
      get;
      private set;
    }

    public EPBGameLevel EPBGameLevel
    {
      get;
      private set;
    }

    public EPBGameModeType EPBGameModeType
    {
      get;
      private set;
    }

    public Info()
    {
      _saveSections = new List<SaveSection>();
    }

    public static Info Deserialize(SaveSection saveSection)
    {
      Info info = new Info();
      using (SaveReader saveReader = new SaveReader(saveSection.Data))
      {
        saveReader.ReadStructProperty("Info", out _, out int infoLength, out _);

        while (true)
        {
          saveReader.SetCheckpoint();

          string name = saveReader.ReadLengthPrefixedString();


          if (string.Equals(name, SaveConstants.None))
          {
            break;
          }

          string type = saveReader.ReadLengthPrefixedString();


          saveReader.Reset();

          if (string.Equals(name, "EPBGameLevel"))
          {
            saveReader.VerifyAndReadLengthPrefixedString("EPBGameLevel");
            saveReader.ReadByte();
            info.EPBGameLevel = Enum.Parse<EPBGameLevel>(saveReader.ReadLengthPrefixedString().Replace("EPBGameLevel::", string.Empty));
            continue;
          }
          else if (string.Equals(name, "EPBGameModeType"))
          {
            saveReader.VerifyAndReadLengthPrefixedString("EPBGameModeType");
            saveReader.ReadByte();
            info.EPBGameModeType = Enum.Parse<EPBGameModeType>(saveReader.ReadLengthPrefixedString().Replace("EPBGameModeType::", string.Empty));
            continue;
          }

          switch (type)
          {
            case "IntProperty" when string.Equals(name, "Version", StringComparison.OrdinalIgnoreCase):
              info.Version = saveReader.ReadIntProperty(name);
              break;
            case "IntProperty" when string.Equals(name, "totalCoins", StringComparison.OrdinalIgnoreCase):
              info.TotalCoins = saveReader.ReadIntProperty(name);
              break;
            case "IntProperty":
              saveReader.ReadIntProperty(name);
              break;
            case "StrProperty":
              saveReader.ReadStrProperty(name);
              break;
            case "NameProperty":
              saveReader.ReadNameProperty(name);
              break;
            case "FloatProperty":
              saveReader.ReadFloatProperty(name);
              break;
            case "ByteProperty":
              saveReader.ReadByteProperty(name);
              break;
            case "BoolProperty":
              saveReader.ReadBoolProperty(name);
              break;
            case "EnumProperty":
              saveReader.ReadEnumProperty(name, out string enumType, out string enumValue);
              break;
            case SaveConstants.ArrayProperty:
              saveReader.ReadArrayProperty(name, out _, out int arrayPropertyLength, out _);
              saveReader.Skip(arrayPropertyLength - 4);//the length includes 4 bytes for the count 
              break;
            case SaveConstants.StructProperty:
              saveReader.ReadStructProperty(name, out _, out int structPropertyLength, out _);
              saveReader.Skip(structPropertyLength);
              break;
            default:
              throw new NotImplementedException();
          }
        }

        if (!saveReader.EndOfStream)
        {
          throw new InvalidDataException($"Expected end of 'Info' element.");
        }
      }

      return info;
    }
    public byte[] Serialize()
    {
      throw new NotImplementedException();
    }
  }
}