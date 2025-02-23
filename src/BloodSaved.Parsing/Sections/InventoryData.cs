﻿using System.Diagnostics;
using System.Reflection.PortableExecutable;
using BloodSaved.Parsing.Enums;
using BloodSaved.Parsing.Extensions;
using BloodSaved.Parsing.Models;

namespace BloodSaved.Parsing.Sections
{
  public class InventoryData : ISerializableSection<InventoryData>
  {
    private byte _magicByte1;
    private byte _magicByte2;

    public string[] EquippedItems { get; set; }

    public List<InventoryItem> Items { get; private set; }

    public InventoryData()
    {
      EquippedItems = new string[129];
      Items = new List<InventoryItem>();
    }

    public static InventoryData Deserialize(byte[] serialized)
    {
      InventoryData inventoryData = new InventoryData();
      using (SaveReader saveReader = new SaveReader(serialized))
      {
        saveReader.ReadArrayProperty(SaveConstants.InventoryData, out string inventoryDataArrayType, out int inventoryDataLength, out int inventoryDataCount);
        long inventoryDataCountOffset = saveReader.CurrentPosition - 4;

        if (!string.Equals(inventoryDataArrayType, SaveConstants.ByteProperty))
        {
          throw new InvalidDataException($"InventoryData array type of '{inventoryDataArrayType}' is not correct, expected ByteProperty.");
        }

        //equipped list
        {
          //yes, it took a lot of trial and error to finally realize 129 is the magic number here
          //tested on both PC and PS4 saves
          for (int i = 0; i < 129; i++)
          {
            inventoryData.EquippedItems[i] = saveReader.ReadLengthPrefixedString();
          }
        }

        //inventory
        {
          int inventorySetIndex = 0;
          int inventorySetCount = 0;
          int inventorySetSize = saveReader.ReadInt32();

          while (inventorySetIndex < 15)
          {
            ItemCategories setIndexCategory = (ItemCategories)inventorySetIndex;
            ItemIds itemId = saveReader.ReadLengthPrefixedString().ToItemId();
            ItemCategories itemIdCategory = itemId.GetCategory();

            //verify the setIndexCategory and item category match
            if (setIndexCategory != itemIdCategory)
            {
              throw new InvalidDataException($"Item category ('{itemIdCategory}') does not match the inventory set category ('{setIndexCategory}').");
            }

            inventoryData.Items.Add(new InventoryItem
            {
              ItemId = itemId,
              Quantity = saveReader.ReadInt32(),
              Index = saveReader.ReadInt32(),
              Rank = saveReader.ReadInt32(),
              GradeValue = saveReader.ReadSingle(),
              RankValue = saveReader.ReadSingle(),
              Unknown = saveReader.ReadInt32()
            });

            inventorySetCount++;
            if (inventorySetCount == inventorySetSize)
            {
              while (inventorySetIndex < 15)
              {
                inventorySetIndex++;
                if (inventorySetIndex < 15)
                {
                  Debug.WriteLine($"finding next set size, current={inventorySetIndex}");
                  inventorySetSize = saveReader.ReadInt32();

                  if (inventorySetSize != 0)
                  {
                    inventorySetCount = 0;
                    break;
                  }
                }
              }
            }
          }
        }

        //normally 0x01
        inventoryData._magicByte1 = saveReader.ReadByte();

        //skill shards
        {
          int skillShardCount = saveReader.ReadInt32();
          for (int s = 0; s < skillShardCount; s++)
          {
            ItemIds itemId = saveReader.ReadLengthPrefixedString().ToItemId();
            ItemCategories itemIdCategory = itemId.GetCategory();

            //verify the setIndexCategory and item category match
            if (ItemCategories.SkillShards != itemIdCategory)
            {
              throw new InvalidDataException($"Item category ('{itemIdCategory}') does not match the inventory set category ('{ItemCategories.SkillShards}').");
            }

            inventoryData.Items.Add(new InventoryItem
            {
              ItemId = itemId,
              Quantity = saveReader.ReadInt32(),
              Index = saveReader.ReadInt32(),
              Rank = saveReader.ReadInt32(),
              GradeValue = saveReader.ReadSingle(),
              RankValue = saveReader.ReadSingle(),
              Unknown = saveReader.ReadInt32()
            });
          }
        }

        //0x04
        inventoryData._magicByte2 = saveReader.ReadByte();

        if (!saveReader.EndOfStream)
        {
          throw new InvalidDataException($"Expected end of 'InventoryData' section.");
        }

        int inventoryDataActualLength = (int)(saveReader.CurrentPosition - inventoryDataCountOffset);
        if (inventoryDataActualLength != inventoryDataLength)
        {
          throw new InvalidDataException($"InventoryData section has invalid length specified, specifed={inventoryDataLength}, actual={inventoryDataActualLength}.");
        }

        if (inventoryDataActualLength - 4 != inventoryDataCount)
        {
          throw new InvalidDataException($"InventoryData section has invalid count specified, specifed={inventoryDataCount}, actual={(inventoryDataActualLength - 4)}.");
        }
      }
      return inventoryData;
    }

    public byte[] Serialize()
    {
      using (SaveWriter saveWriter = new SaveWriter())
      {
        saveWriter.WriteArrayProperty(SaveConstants.InventoryData, SaveConstants.ByteProperty, out long inventoryDataLengthOffset, out long inventoryDataCountOffset);

        foreach (string equippedItem in EquippedItems)
        {
          saveWriter.WriteLengthPrefixedString(equippedItem);
        }

        for (int category = 0; category <= (int)ItemCategories.FamiliarShards; category++)
        {
          ItemCategories currentCategory = (ItemCategories)category;

          List<InventoryItem> categoryItems = Items.Where(item => item.ItemId.GetCategory() == currentCategory)
            .OrderBy(item => item.Index).ThenBy(item => item.ItemId.ToString()).ToList();

          saveWriter.Write(categoryItems.Count());
          
          if (categoryItems.Any())
          {
            int currentItemIndex = 0;
            foreach (InventoryItem currentItem in categoryItems)
            {
              saveWriter.WriteItemId(currentItem.ItemId);
              saveWriter.Write(currentItem.Quantity);
              saveWriter.Write(currentItemIndex);
              saveWriter.Write(currentItem.Rank);
              saveWriter.Write(currentItem.GradeValue);
              saveWriter.Write(currentItem.RankValue);
              saveWriter.Write(currentItem.Unknown);
              currentItemIndex++;
            }
          }
        }

        saveWriter.Write(_magicByte1);

        //skill shards
        {
          List<InventoryItem> skillShards = Items.Where(item => item.ItemId.GetCategory() == ItemCategories.SkillShards)
            .OrderBy(item => item.Index).ThenBy(item => item.ItemId.ToString()).ToList();
          saveWriter.Write(skillShards.Count);
          for (int s = 0; s < skillShards.Count; s++)
          {
            InventoryItem skillShard = skillShards[s];
            saveWriter.WriteItemId(skillShard.ItemId);
            saveWriter.Write(skillShard.Quantity);
            saveWriter.Write(s);
            saveWriter.Write(skillShard.Rank);
            saveWriter.Write(skillShard.GradeValue);
            saveWriter.Write(skillShard.RankValue);
            saveWriter.Write(skillShard.Unknown);
          }
        }

        saveWriter.Write(_magicByte2);

        //set length
        int inventoryDataLength = (int)(saveWriter.CurrentPosition - inventoryDataCountOffset);
        saveWriter.SetPosition(inventoryDataLengthOffset);
        saveWriter.Write(inventoryDataLength);
        saveWriter.SetPosition(inventoryDataCountOffset);
        saveWriter.Write(inventoryDataLength - 4);//count doesn't include it's own bytes

        return saveWriter.ToArray();
      }
    }
  }
}
