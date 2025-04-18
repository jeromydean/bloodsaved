using System.Xml.Linq;
using BloodSaved.Parsing.Attributes;
using BloodSaved.Parsing.Enums;
using BloodSaved.Parsing.Extensions;
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

    public int TotalExperience
    {
      get;
      set;
    }

    public Dictionary<ItemId, int> FamiliarTotalExperience { get; set; }

    public StatusData()
    {
      _saveSections = new List<SaveSection>();
      FamiliarTotalExperience = new Dictionary<ItemId, int>();
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
              int intValue = saveReader.ReadIntProperty(name);
              if (string.Equals(name, "TotalExperience", StringComparison.OrdinalIgnoreCase))
              {
                statusData.TotalExperience = intValue;
              }
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
            case SaveConstants.MapProperty when string.Equals(name, "FamiliarTotalExperience"):
              saveReader.ReadMapProperty(name, out _, out _, out int familiarTotalExperienceLength, out int familiarTotalExperienceCount);

              for (int i = 0; i < familiarTotalExperienceCount; i++)
              {
                ItemId familiarItemId = saveReader.ReadLengthPrefixedString().ToItemId();
                int experience = saveReader.ReadInt32();

                statusData.FamiliarTotalExperience.Add(familiarItemId, experience);
              }
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
            Type = type,
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
      using (SaveWriter saveWriter = new SaveWriter())
      {
        saveWriter.WriteArrayProperty("StatusData", SaveConstants.ByteProperty, out long statusDataLengthOffset, out long statusDataCountOffset);

        if (FamiliarTotalExperience.Any()
          && !_saveSections.Any(s => string.Equals(s.Name, "FamiliarTotalExperience", StringComparison.OrdinalIgnoreCase)))
        {
          _saveSections.Add(new SaveSection
          {
            Name = "FamiliarTotalExperience",
            Type = SaveConstants.MapProperty
          });
        }

        foreach (SaveSection section in _saveSections)
        {
          if (string.Equals(section.Name, "TotalExperience", StringComparison.OrdinalIgnoreCase))
          {
            saveWriter.WriteIntProperty("TotalExperience", TotalExperience);
          }
          else if (string.Equals(section.Name, "FamiliarTotalExperience", StringComparison.OrdinalIgnoreCase))
          {
            saveWriter.WriteMapProperty("FamiliarTotalExperience", SaveConstants.NameProperty,
              SaveConstants.IntProperty, out long familiarTotalExperienceLengthOffset, out long familiarTotalExperienceCountOffset, count: FamiliarTotalExperience.Count);

            foreach(KeyValuePair<ItemId, int> familiarExperience in FamiliarTotalExperience)
            {
              saveWriter.WriteItemId(familiarExperience.Key);
              saveWriter.Write(familiarExperience.Value);
            }

            int m_familiarTotalExperienceLength = (int)(saveWriter.CurrentPosition - (familiarTotalExperienceCountOffset - 4));
            saveWriter.SetCheckpoint();
            saveWriter.SetPosition(familiarTotalExperienceLengthOffset);
            saveWriter.Write(m_familiarTotalExperienceLength);
            saveWriter.Reset();
          }
          else
          {
            saveWriter.Write(section.Data);
          }
        }

        saveWriter.WriteLengthPrefixedString(SaveConstants.None);
        saveWriter.Write(0);

        int statusDataLength = (int)(saveWriter.CurrentPosition - statusDataCountOffset);
        saveWriter.SetPosition(statusDataLengthOffset);
        saveWriter.Write(statusDataLength);
        saveWriter.SetPosition(statusDataCountOffset);
        saveWriter.Write(statusDataLength - 4);

        return saveWriter.ToArray();
      }
    }
  }
}
