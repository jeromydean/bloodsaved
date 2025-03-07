using BloodSaved.Parsing.Enums;
using BloodSaved.Parsing.Extensions;
using BloodSaved.Parsing.Models;

namespace BloodSaved.Parsing.Sections
{
  public class ShardPossession : ISerializableSection<ShardPossession>
  {
    private bool _shardIsUpperCase;

    public List<Shard> Shards { get; set; }

    public List<SkillShard> Skills { get; set; }

    public List<ShardAttribute> ShardAttributes { get; set; }

    public ShardPossession()
    {
      Shards = new List<Shard>();
      Skills = new List<SkillShard>();
      ShardAttributes = new List<ShardAttribute>();
    }

    public static ShardPossession Deserialize(SaveSection saveSection)
    {
      ShardPossession shardPossession = new ShardPossession();
      using (SaveReader saveReader = new SaveReader(saveSection.Data))
      {
        saveReader.ReadArrayProperty(SaveConstants.ShardPossession, out string shardPossessionArrayType, out int shardPossessionLength, out int shardPossessionCount);
        long shardPossessionCountOffset = saveReader.CurrentPosition - 4;

        if (!string.Equals(shardPossessionArrayType, SaveConstants.ByteProperty))
        {
          throw new InvalidDataException($"ShardPossession array type of '{shardPossessionArrayType}' is not correct, expected ByteProperty.");
        }

        saveReader.VerifyAndReadLengthPrefixedString(SaveConstants.None);
        saveReader.VerifyNullPadBytes(4);

        for (int x = 0; x < 4; x++)
        {
          if (string.Equals(saveReader.PeekLengthPrefixedString(), SaveConstants.m_Possession))
          {
            saveReader.ReadMapProperty(SaveConstants.m_Possession, out _, out _, out _, out int m_PossessionCount);
            for (int s = 0; s < m_PossessionCount; s++)
            {
              ItemIds itemId = saveReader.ReadLengthPrefixedString().ToItemId();
              ItemCategories itemIdCategory = itemId.GetCategory();

              //verify the shard is in the proper category
              ItemCategories expectedCategory = default(ItemCategories);
              switch (x)
              {
                case 0:
                  expectedCategory = ItemCategories.ConjureShards;
                  break;
                case 1:
                  expectedCategory = ItemCategories.DirectionalShards;
                  break;
                case 2:
                  expectedCategory = ItemCategories.FamiliarShards;
                  break;
                case 3:
                  expectedCategory = ItemCategories.ManipulativeShards;
                  break;
              }

              if (itemIdCategory != expectedCategory)
              {
                throw new InvalidDataException($"Item category ('{itemIdCategory}') does not match the inventory set category ('{expectedCategory}').");
              }

              shardPossession.Shards.Add(new Shard
              {
                Index = s,
                ItemId = itemId,
                Rank = saveReader.ReadIntProperty(SaveConstants.Rank),
                Quantity = saveReader.ReadIntProperty(SaveConstants.Grade),
                Equipped = saveReader.ReadBoolProperty(SaveConstants.Equipped),
                GradeValue = saveReader.ReadFloatProperty(SaveConstants.GradeValue),
                RankValue = saveReader.ReadFloatProperty(SaveConstants.RankValue)
              });

              saveReader.VerifyAndReadLengthPrefixedString(SaveConstants.None);
            }
          }

          saveReader.VerifyAndReadLengthPrefixedString(SaveConstants.None);
          saveReader.VerifyNullPadBytes(4);
        }

        //skill shards
        {
          if (string.Equals(saveReader.PeekLengthPrefixedString(), SaveConstants.m_Skill))
          {
            saveReader.ReadMapProperty(SaveConstants.m_Skill, out _, out _, out _, out int m_SkillCount);
            for (int i = 0; i < m_SkillCount; i++)
            {
              ItemIds itemId = saveReader.ReadLengthPrefixedString().ToItemId();
              ItemCategories itemIdCategory = itemId.GetCategory();
              if (itemIdCategory != ItemCategories.SkillShards)
              {
                throw new InvalidDataException($"Item category ('{itemIdCategory}') does not match the inventory set category ('{ItemCategories.SkillShards}').");
              }

              bool isOn = saveReader.ReadBoolProperty(SaveConstants.IsOn);

              //can be lowercase "shard" or "Shard"
              string shardString = saveReader.PeekLengthPrefixedString();
              if (string.Equals(shardString, SaveConstants.shard))
              {
                saveReader.ReadNameProperty(SaveConstants.shard);
              }
              else
              {
                saveReader.ReadNameProperty("Shard");
                shardPossession._shardIsUpperCase = true;
              }

              saveReader.ReadStructProperty(SaveConstants.PossessionData, out _, out _, out _);

              shardPossession.Skills.Add(new SkillShard
              {
                Index = i,
                ItemId = itemId,
                IsOn = isOn,
                Rank = saveReader.ReadIntProperty(SaveConstants.Rank),
                Quantity = saveReader.ReadIntProperty(SaveConstants.Grade),
                Equipped = saveReader.ReadBoolProperty(SaveConstants.Equipped),
                GradeValue = saveReader.ReadFloatProperty(SaveConstants.GradeValue),
                RankValue = saveReader.ReadFloatProperty(SaveConstants.RankValue)
              });

              saveReader.VerifyAndReadLengthPrefixedString(SaveConstants.None);
              saveReader.VerifyAndReadLengthPrefixedString(SaveConstants.None);
            }
          }
        }

        saveReader.ReadMapProperty(SaveConstants.m_UpStatus, out _, out _, out _, out int m_UpStatusCount);
        for (int i = 0; i < m_UpStatusCount; i++)
        {
          shardPossession.ShardAttributes.Add(new ShardAttribute
          {
            Name = saveReader.ReadLengthPrefixedString(),
            Value = saveReader.ReadSingle()
          });
        }

        if (string.Equals(saveReader.PeekLengthPrefixedString(), SaveConstants.m_Possession))
        {
          saveReader.ReadMapProperty(SaveConstants.m_Possession, out _, out _, out _, out int m_PossessionCount);
          for (int i = 0; i < m_PossessionCount; i++)
          {
            ItemIds itemId = saveReader.ReadLengthPrefixedString().ToItemId();
            ItemCategories itemIdCategory = itemId.GetCategory();
            if (itemIdCategory != ItemCategories.PassiveShards)
            {
              throw new InvalidDataException($"Item category ('{itemIdCategory}') does not match the inventory set category ('{ItemCategories.PassiveShards}').");
            }

            shardPossession.Shards.Add(new Shard
            {
              Index = i,
              ItemId = itemId,
              Rank = saveReader.ReadIntProperty(SaveConstants.Rank),
              Quantity = saveReader.ReadIntProperty(SaveConstants.Grade),
              Equipped = saveReader.ReadBoolProperty(SaveConstants.Equipped),
              GradeValue = saveReader.ReadFloatProperty(SaveConstants.GradeValue),
              RankValue = saveReader.ReadFloatProperty(SaveConstants.RankValue)
            });

            saveReader.VerifyAndReadLengthPrefixedString(SaveConstants.None);
          }
        }

        saveReader.VerifyAndReadLengthPrefixedString(SaveConstants.None);
        saveReader.VerifyNullPadBytes(4);

        if (!saveReader.EndOfStream)
        {
          throw new InvalidDataException($"Expected end of 'ShardPossession' element.");
        }

        int shardPossessionActualLength = (int)(saveReader.CurrentPosition - shardPossessionCountOffset);
        if (shardPossessionActualLength != shardPossessionLength)
        {
          throw new InvalidDataException($"ShardPossession section has invalid length specified, specifed={shardPossessionLength}, actual={shardPossessionActualLength}.");
        }

        if (shardPossessionActualLength - 4 != shardPossessionCount)
        {
          throw new InvalidDataException($"ShardPossession section has invalid count specified, specifed={shardPossessionCount}, actual={(shardPossessionActualLength - 4)}.");
        }
      }
      return shardPossession;
    }

    public byte[] Serialize()
    {
      using (SaveWriter saveWriter = new SaveWriter())
      {
        saveWriter.WriteArrayProperty(SaveConstants.ShardPossession, SaveConstants.ByteProperty, out long shardPossessionLengthOffset, out long shardPossessionCountOffset);

        saveWriter.WriteLengthPrefixedString(SaveConstants.None);
        saveWriter.Write(0);

        //ConjureShards/DirectionalShards/FamiliarShards/ManipulativeShards
        {
          for (int x = 0; x < 4; x++)
          {
            ItemCategories currentCategory = default;
            switch (x)
            {
              case 0:
                currentCategory = ItemCategories.ConjureShards;
                break;
              case 1:
                currentCategory = ItemCategories.DirectionalShards;
                break;
              case 2:
                currentCategory = ItemCategories.FamiliarShards;
                break;
              case 3:
                currentCategory = ItemCategories.ManipulativeShards;
                break;
            }

            List<Shard> categoryShards = Shards.Where(s => s.ItemId.GetCategory() == currentCategory)
              .OrderBy(s => s.Index).ThenBy(s => s.ItemId.ToString()).ToList();

            if (categoryShards.Any())
            {
              saveWriter.WriteMapProperty(SaveConstants.m_Possession, SaveConstants.NameProperty,
                SaveConstants.StructProperty, out long m_PossessionLengthOffset, out long m_PossessionCountOffset, count: categoryShards.Count);

              for (int s = 0; s < categoryShards.Count; s++)
              {
                Shard currentShard = categoryShards[s];
                saveWriter.WriteItemId(currentShard.ItemId);
                saveWriter.WriteIntProperty(SaveConstants.Rank, currentShard.Rank);
                saveWriter.WriteIntProperty(SaveConstants.Grade, currentShard.Quantity);
                saveWriter.WriteBoolProperty(SaveConstants.Equipped, currentShard.Equipped);
                saveWriter.WriteFloatProperty(SaveConstants.GradeValue, currentShard.GradeValue);
                saveWriter.WriteFloatProperty(SaveConstants.RankValue, currentShard.RankValue);
                saveWriter.WriteLengthPrefixedString(SaveConstants.None);
              }

              int m_PossessionLength = (int)(saveWriter.CurrentPosition - (m_PossessionCountOffset - 4));
              saveWriter.SetCheckpoint();
              saveWriter.SetPosition(m_PossessionLengthOffset);
              saveWriter.Write(m_PossessionLength);
              saveWriter.Reset();
            }

            saveWriter.WriteLengthPrefixedString(SaveConstants.None);
            saveWriter.Write(0);
          }
        }

        //skill shards
        {
          List<SkillShard> skillShards = Skills.Where(s => s.ItemId.GetCategory() == ItemCategories.SkillShards)
            .OrderBy(s => s.Index).ThenBy(s => s.ItemId.ToString()).ToList();

          if (skillShards.Any())
          {
            saveWriter.WriteMapProperty(SaveConstants.m_Skill, SaveConstants.NameProperty,
              SaveConstants.StructProperty, out long m_SkillLengthOffset, out long m_SkillCountOffset, count: skillShards.Count);

            for (int s = 0; s < skillShards.Count; s++)
            {
              SkillShard skillShard = skillShards[s];

              saveWriter.WriteItemId(skillShard.ItemId);
              saveWriter.WriteBoolProperty(SaveConstants.IsOn, skillShard.IsOn);

              if (!_shardIsUpperCase)
              {
                saveWriter.WriteNameProperty(SaveConstants.shard, skillShard.ItemId);
              }
              else
              {
                saveWriter.WriteNameProperty("Shard", skillShard.ItemId);
              }

              saveWriter.WriteStructProperty(SaveConstants.PossessionData, SaveConstants.PBShardPossessionData,
                Guid.Empty, out long possessionRawOffset);

              //checkpoint so we can fix the length
              saveWriter.SetCheckpoint();

              saveWriter.WriteIntProperty(SaveConstants.Rank, skillShard.Rank);
              saveWriter.WriteIntProperty(SaveConstants.Grade, skillShard.Quantity);
              saveWriter.WriteBoolProperty(SaveConstants.Equipped, skillShard.Equipped);
              saveWriter.WriteFloatProperty(SaveConstants.GradeValue, skillShard.GradeValue);
              saveWriter.WriteFloatProperty(SaveConstants.RankValue, skillShard.RankValue);
              saveWriter.WriteLengthPrefixedString(SaveConstants.None);

              int possessionDataLength = (int)(saveWriter.CurrentPosition - saveWriter.Checkpoint);
              saveWriter.SetCheckpoint();
              saveWriter.SetPosition(possessionRawOffset);
              saveWriter.Write(possessionDataLength);
              saveWriter.Reset();

              saveWriter.WriteLengthPrefixedString(SaveConstants.None);
            }

            int m_SkillLength = (int)(saveWriter.CurrentPosition - (m_SkillCountOffset - 4));
            saveWriter.SetCheckpoint();
            saveWriter.SetPosition(m_SkillLengthOffset);
            saveWriter.Write(m_SkillLength);
            saveWriter.Reset();
          }
        }

        //m_UpStatus
        {
          saveWriter.WriteMapProperty(SaveConstants.m_UpStatus, SaveConstants.ByteProperty,
            SaveConstants.FloatProperty, out long m_UpStatusLengthOffset, out long m_UpStatusCountOffset, count: ShardAttributes.Count);

          foreach (ShardAttribute shardAttribute in ShardAttributes)
          {
            saveWriter.WriteLengthPrefixedString(shardAttribute.Name);
            saveWriter.Write(shardAttribute.Value);
          }

          int m_UpStatusLength = (int)(saveWriter.CurrentPosition - (m_UpStatusCountOffset - 4));
          saveWriter.SetCheckpoint();
          saveWriter.SetPosition(m_UpStatusLengthOffset);
          saveWriter.Write(m_UpStatusLength);
          saveWriter.Reset();
        }

        //passive shards
        {
          List<Shard> passiveShards = Shards.Where(s => s.ItemId.GetCategory() == ItemCategories.PassiveShards)
            .OrderBy(s => s.Index).ThenBy(s => s.ItemId.ToString()).ToList();

          if (passiveShards.Any())
          {
            saveWriter.WriteMapProperty(SaveConstants.m_Possession, SaveConstants.NameProperty,
              SaveConstants.StructProperty, out long m_PossessionLengthOffset, out long m_PossessionCountOffset, count: passiveShards.Count);

            for (int s = 0; s < passiveShards.Count; s++)
            {
              Shard currentShard = passiveShards[s];
              saveWriter.WriteItemId(currentShard.ItemId);
              saveWriter.WriteIntProperty(SaveConstants.Rank, currentShard.Rank);
              saveWriter.WriteIntProperty(SaveConstants.Grade, currentShard.Quantity);
              saveWriter.WriteBoolProperty(SaveConstants.Equipped, currentShard.Equipped);
              saveWriter.WriteFloatProperty(SaveConstants.GradeValue, currentShard.GradeValue);
              saveWriter.WriteFloatProperty(SaveConstants.RankValue, currentShard.RankValue);
              saveWriter.WriteLengthPrefixedString(SaveConstants.None);
            }

            //back off the count bytes and the 4 null pad bytes
            int m_PossessionLength = (int)(saveWriter.CurrentPosition - (m_PossessionCountOffset - 4));
            saveWriter.SetCheckpoint();
            saveWriter.SetPosition(m_PossessionLengthOffset);
            saveWriter.Write(m_PossessionLength);
            saveWriter.Reset();
          }
        }

        saveWriter.WriteLengthPrefixedString(SaveConstants.None);
        saveWriter.Write(0);

        //set length
        int shardPossessionLength = (int)(saveWriter.CurrentPosition - shardPossessionCountOffset);
        saveWriter.SetPosition(shardPossessionLengthOffset);
        saveWriter.Write(shardPossessionLength);
        saveWriter.SetPosition(shardPossessionCountOffset);
        saveWriter.Write(shardPossessionLength - 4);

        return saveWriter.ToArray();
      }
    }
  }
}
