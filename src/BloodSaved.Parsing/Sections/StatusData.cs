using System.Diagnostics;
using System.Text;
using BloodSaved.Parsing.Models;

namespace BloodSaved.Parsing.Sections
{
  public class StatusData : ISerializableSection<StatusData>
  {
    private List<SaveSection> _saveSections;
    //Debug.WriteLine($"StatusData - name='{name}', type='{type}'");
    //StatusData - name='HitPoint', type='IntProperty'
    //StatusData - name='MagicPoint', type='IntProperty'
    //StatusData - name='TotalExperience', type='IntProperty'
    //StatusData - name='FamiliarTotalExperience', type='MapProperty'
    //StatusData - name='LastExperience', type='IntProperty'
    //StatusData - name='AdditionalMaxHP', type='FloatProperty'
    //StatusData - name='AdditionalMaxMP', type='FloatProperty'
    //StatusData - name='AdditionalMaxBullet', type='IntProperty'
    //StatusData - name='InitializedExtraMode', type='BoolProperty'
    //StatusData - name='CurrentSkillShardList', type='MapProperty'
    //StatusData - name='EquipSpecialAttributeBuff', type='ArrayProperty'
    //StatusData - name='EquipSpecialAttributeBuffMaxHP', type='FloatProperty'
    //StatusData - name='EquipSpecialAttributeBuffMaxMP', type='FloatProperty'

    public StatusData()
    {
      _saveSections = new List<SaveSection>();
    }

    public static StatusData Deserialize(SaveSection saveSection)
    {
      StatusData statusData = new StatusData();
      using (SaveReader saveReader = new SaveReader(saveSection.Data))
      {
        saveReader.ReadArrayProperty("StatusData", out _, out int statusDataLength, out int statusDataCount);

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

          switch (type)
          {
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
            case SaveConstants.MapProperty:
              saveReader.ReadMapProperty(name, out _, out _, out int mapPropertyLength, out _);
              saveReader.Skip(mapPropertyLength - 8);//the length includes 4 null pad bytes and 4 bytes for the count 
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

          statusData._saveSections.Add(new SaveSection
          {
            Name = name,
            StartOffset = saveReader.Checkpoint,
            EndOffset = saveReader.CurrentPosition,
            Data = saveReader.CloneFromCheckpoint()
          });
        }

        saveReader.VerifyNullPadBytes(4);

        if (!saveReader.EndOfStream)
        {
          throw new InvalidDataException($"Expected end of '{nameof(StatusData)}' element.");
        }
      }

      return statusData;
    }

    public byte[] Serialize()
    {
      throw new NotImplementedException();
    }
  }
}
