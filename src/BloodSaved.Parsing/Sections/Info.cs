using BloodSaved.Parsing.Enums;
using BloodSaved.Parsing.Extensions;
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
    //Info - name='EPBGameLevel', type='
    //Info - name='TotalKills', type='IntProperty'
    //Info - name='TotalCoins', type='IntProperty'
    //Info - name='TotalPlaySec', type='FloatProperty'
    //Info - name='GameModeType', type='ByteProperty'
    //Info - name='EPBGameModeType', type='
    //Info -name='TrueEndCount', type='IntProperty'
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
      set;
    }

    public string SlotName
    {
      get;
      private set;
    }

    public EPBGameLevel EPBGameLevel
    {
      get;
      set;
    }

    public EPBGameModeType EPBGameModeType
    {
      get;
      set;
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
          }
          else if (string.Equals(name, "EPBGameModeType"))
          {
            saveReader.VerifyAndReadLengthPrefixedString("EPBGameModeType");
            saveReader.ReadByte();
            info.EPBGameModeType = Enum.Parse<EPBGameModeType>(saveReader.ReadLengthPrefixedString().Replace("EPBGameModeType::", string.Empty));
          }
          else
          {
            switch (type)
            {
              case "IntProperty" when string.Equals(name, "Version", StringComparison.OrdinalIgnoreCase):
                info.Version = saveReader.ReadIntProperty(name);
                break;
              case "IntProperty" when string.Equals(name, "TotalCoins", StringComparison.OrdinalIgnoreCase):
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

          info._saveSections.Add(new SaveSection
          {
            Name = name,
            Type = type,
            StartOffset = saveReader.Checkpoint,
            EndOffset = saveReader.CurrentPosition,
            Data = saveReader.CloneFromCheckpoint()
          });
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
      //new games won't have the property set so we need to add it
      //let the writer write the data -- order doesn't seem to matter
      if (TotalCoins != 0
        && !_saveSections.Any(s => string.Equals(s.Name, "TotalCoins", StringComparison.OrdinalIgnoreCase)))
      {
        _saveSections.Add(new SaveSection
        {
          Name = "TotalCoins",
          Type = SaveConstants.IntProperty
        });
      }

      using (SaveWriter saveWriter = new SaveWriter())
      {
        saveWriter.WriteStructProperty("Info", "PBSaveGameDataInfo", Guid.Empty, out long infoLengthOffset);
        saveWriter.SetCheckpoint();

        foreach (SaveSection section in _saveSections)
        {
          if (string.Equals(section.Name, "TotalCoins"))
          {
            saveWriter.WriteIntProperty("TotalCoins", TotalCoins);
          }
          else if (string.Equals(section.Name, "EPBGameLevel"))
          {
            saveWriter.WriteLengthPrefixedString("EPBGameLevel");
            saveWriter.Write((byte)0x00);
            saveWriter.WriteLengthPrefixedString(EPBGameLevel.GetDescription());
          }
          else
          {
            saveWriter.Write(section.Data);
          }
        }

        saveWriter.WriteLengthPrefixedString(SaveConstants.None);

        int infoDataLength = (int)(saveWriter.CurrentPosition - saveWriter.Checkpoint);
        saveWriter.SetPosition(infoLengthOffset);
        saveWriter.Write(infoDataLength);

        return saveWriter.ToArray();
      }
    }
  }
}