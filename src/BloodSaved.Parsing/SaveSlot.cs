using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using BloodSaved.Parsing.Enums;
using BloodSaved.Parsing.Extensions;
using BloodSaved.Parsing.Models;
using BloodSaved.Parsing.Sections;

namespace BloodSaved.Parsing
{
  public class SaveSlot
  {
    private static readonly IReadOnlyList<byte> s_cipherKey = [0x3a, 0x3b, 0x3c, 0x3d, 0x3e, 0x3f, 0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4a, 0x4b, 0x4c, 0x4d, 0x4e, 0x4f, 0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5a, 0x5b, 0x5c, 0x5d, 0x5e, 0x5f, 0x60, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69, 0x6a, 0x6b, 0x6c, 0x6d, 0x6e, 0x6f, 0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7a, 0x7b, 0x7c, 0x7d, 0x7e, 0x7f, 0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88, 0x89, 0x8a, 0x8b, 0x8c, 0x8d, 0x8e, 0x8f, 0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97, 0x98, 0x99, 0x9a, 0x9b, 0x9c, 0x9d, 0x9e, 0x9f, 0xa0, 0xa1, 0xa2, 0xa3, 0xa4, 0xa5, 0xa6, 0xa7, 0xa8, 0xa9, 0xaa, 0xab, 0xac, 0xad, 0xae, 0xaf, 0xb0, 0xb1, 0xb2, 0xb3, 0xb4, 0xb5, 0xb6, 0xb7, 0xb8, 0xb9, 0xba, 0xbb, 0xbc, 0xbd, 0xbe, 0xbf, 0xc0, 0xc1, 0xc2, 0xc3, 0xc4, 0xc5, 0xc6, 0xc7, 0xc8, 0xc9, 0xca, 0xcb, 0xcc, 0xcd, 0xce, 0xcf, 0xd0, 0xd1, 0xd2, 0xd3, 0xd4, 0xd5, 0xd6, 0xd7, 0xd8, 0xd9, 0xda, 0xdb, 0xdc, 0xdd, 0xde, 0xdf, 0xe0, 0xe1, 0xe2, 0xe3, 0xe4, 0xe5, 0xe6, 0xe7, 0xe8, 0xe9, 0xea, 0xeb, 0xec, 0xed, 0xee, 0xef, 0xf0, 0xf1, 0xf2, 0xf3, 0xf4, 0xf5, 0xf6, 0xf7, 0xf8, 0xf9, 0xfa, 0xfb, 0xfc, 0xfd, 0xfe, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e, 0x2f, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39];
    private static readonly IReadOnlyList<byte> s_cipheredPreamble = [0x7d, 0x6d, 0x7d, 0x6e];
    private static readonly IReadOnlyList<byte> s_plainPreamble = [0x47, 0x56, 0x41, 0x53];

    private Header _header;
    private string _saveClassName;
    private List<SaveSection> _saveSections;

    public CompletedTutorials CompletedTutorials
    {
      get;
      private set;
    }

    public StatusData StatusData
    {
      get;
      private set;
    }

    public Info Info
    {
      get;
      private set;
    }

    public InventoryData Inventory
    {
      get;
      private set;
    }

    public ShardPossession ShardPossession
    {
      get;
      private set;
    }

    public m_RoomInfo m_RoomInfo
    {
      get;
      private set;
    }

    public m_Traverse m_Traverse
    {
      get;
      private set;
    }

    private SaveSlot()
    {
      _saveSections = new List<SaveSection>();
    }

    public string GenerateSvgMap()
    {
      int dataWidth = 200;
      int dataHeight = 100;
      int roomHeight = 10;
      int roomWidth = 20;

      int minXRoom = 2;
      int maxXRoom = 119;
      int minYRoom = 22;
      int maxYRoom = 70;

      int mapWidth = minXRoom + maxXRoom + 1;

      StringBuilder svgBuilder = new StringBuilder();
      svgBuilder.AppendLine($@"<svg xmlns=""http://www.w3.org/2000/svg"" width=""{mapWidth * roomWidth}"" height=""{dataHeight * roomHeight}"" style=""background-color: #000000;"">");
      svgBuilder.AppendLine(@"  <defs>
        <linearGradient id=""completed"" x1=""0%"" x2=""0%"" y1=""0%"" y2=""100%"">
          <stop offset=""0%"" stop-color=""green"" />
          <stop offset=""100%"" stop-color=""white"" />
        </linearGradient>
        <linearGradient id=""missing"" x1=""0%"" x2=""0%"" y1=""0%"" y2=""100%"">
          <stop offset=""0%"" stop-color=""red"" />
          <stop offset=""100%"" stop-color=""white"" />
        </linearGradient>
      </defs>");

      int traversedRoomCount = 0;
      for (int i = 0; i < SaveConstants.CompleteMap.Length; i++)
      {
        int bottomLeftOriginY = i / dataWidth;
        int topLeftOriginY = (dataHeight - bottomLeftOriginY) - 1;
        int x = i % dataWidth;

        if (SaveConstants.CompleteMap[i] != 0)
        {
          bool isTraversed = m_Traverse.TraverseData[i] != 0;
          svgBuilder.AppendLine($@"<rect width=""{roomWidth}"" height=""{roomHeight}"" x=""{x * roomWidth}"" y=""{topLeftOriginY * roomHeight}"" fill=""url(#{(isTraversed ? "completed" : "missing")})"" style=""stroke-width:1;stroke:{(isTraversed ? "#ffffff" : "#ff0000")};"" />");

          if (isTraversed)
          {
            traversedRoomCount++;
          }
        }
      }
      svgBuilder.AppendLine($@"<text x=""{roomWidth * 10}"" y=""{roomHeight * minYRoom}"" fill=""#ffffff"" font-size=""30"">{traversedRoomCount}/{SaveConstants.TotalRooms} - {(traversedRoomCount/(double)SaveConstants.TotalRooms).ToString("P2")}</text>");
      svgBuilder.AppendLine(@"</svg>");
      string mapSvgData = svgBuilder.ToString();
      return mapSvgData;
    }

    public static byte[] Crypt(byte[] data)
    {
      for (int i = 0; i < data.Length; i++)
      {
        int keyIndex = i % s_cipherKey.Count;
        data[i] ^= s_cipherKey[keyIndex];
      }
      return data;
    }

    public static SaveSlot Load(string filename)
    {
      SaveSlot saveSlot = new SaveSlot();

      byte[] saveData = File.ReadAllBytes(filename);

      //decrypt the data if needed
      if (Enumerable.SequenceEqual(s_cipheredPreamble, saveData.Take(4)))
      {
        saveData = Crypt(saveData);
      }

      if (!Enumerable.SequenceEqual(s_plainPreamble, saveData.Take(4)))
      {
        throw new InvalidDataException("Unknown file header data.");
      }

      using (SaveReader saveReader = new SaveReader(saveData))
      {
        saveReader.SetCheckpoint();

        //skip preamble
        saveReader.Skip(4);

        saveSlot._header = saveReader.ReadHeader();

        saveSlot._saveClassName = saveReader.ReadLengthPrefixedString();

        saveSlot._saveSections.Add(new SaveSection
        {
          Name = "Header",
          StartOffset = 0,
          EndOffset = saveReader.BaseStream.Position,
          Data = saveReader.CloneFromCheckpoint()
        });

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
            case SaveConstants.StructProperty:
              saveReader.ReadStructProperty(name, out _, out int structPropertyLength, out _);
              saveReader.Skip(structPropertyLength);
              break;
            case SaveConstants.BoolProperty:
              saveReader.ReadBoolProperty(name);
              break;
            case SaveConstants.NameProperty:
              saveReader.ReadNameProperty(name);
              break;
            case SaveConstants.FloatProperty:
              saveReader.ReadFloatProperty(name);
              break;
            case SaveConstants.MapProperty:
              saveReader.ReadMapProperty(name, out _, out _, out int mapPropertyLength, out _);
              saveReader.Skip(mapPropertyLength - 8);//the length includes 4 null pad bytes and 4 bytes for the count 
              break;
            case SaveConstants.ArrayProperty:
              saveReader.ReadArrayProperty(name, out _, out int arrayPropertyLength, out _);
              saveReader.Skip(arrayPropertyLength - 4);//the length includes 4 bytes for the count 
              break;
            default:
              throw new InvalidDataException($"Unknown save section type of '{type}' at offset {saveReader.BaseStream.Position}.");
          }

          saveSlot._saveSections.Add(new SaveSection
          {
            Name = name,
            Type = type,
            StartOffset = saveReader.Checkpoint,
            EndOffset = saveReader.CurrentPosition,
            Data = saveReader.CloneFromCheckpoint()
          });
        }

        saveReader.VerifyNullPadBytes(4);
        saveReader.Skip(16);//md5 checksum

        if (!saveReader.EndOfStream)
        {
          throw new InvalidDataException($"Expected end of save data.");
        }
      }

      saveSlot.Info = Info.Deserialize(saveSlot._saveSections
        .Single(s => s.Name == SaveConstants.Info));

      if (saveSlot._saveSections.Any(s => s.Name == SaveConstants.CompletedTutorials))
      {
        saveSlot.CompletedTutorials = CompletedTutorials.Deserialize(saveSlot._saveSections
        .Single(s => s.Name == SaveConstants.CompletedTutorials));
      }

      if (saveSlot._saveSections.Any(s => s.Name == "m_RoomInfo"))
      {
        saveSlot.m_RoomInfo = m_RoomInfo.Deserialize(saveSlot._saveSections
        .Single(s => s.Name == "m_RoomInfo"));
      }

      //map data
      saveSlot.m_Traverse = m_Traverse.Deserialize(saveSlot._saveSections
        .Single(s => s.Name == "m_Traverse"));

      //total experience, etc.
      saveSlot.StatusData = StatusData.Deserialize(saveSlot._saveSections
        .Single(s => s.Name == SaveConstants.StatusData));

      saveSlot.Inventory = InventoryData.Deserialize(saveSlot._saveSections
        .Single(s => s.Name == SaveConstants.InventoryData));

      saveSlot.ShardPossession= ShardPossession.Deserialize(saveSlot._saveSections
        .Single(s => s.Name == SaveConstants.ShardPossession));

      return saveSlot;
    }

    public void Save(string filename,
      bool crypt = true)
    {
      using (FileStream stream = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
      {
        Save(stream, crypt);
      }
    }

    public void Save(Stream stream,
      bool crypt = true)
    {
      byte[] savedata = null;
      using (SaveWriter writer = new SaveWriter())
      {
        foreach (SaveSection section in _saveSections)
        {
          switch (section.Name)
          {
            case SaveConstants.Info:
              writer.Write(Info.Serialize());
              break;
            case SaveConstants.StatusData:
              writer.Write(StatusData.Serialize());
              break;
            case SaveConstants.CompletedTutorials:
              writer.Write(CompletedTutorials.Serialize());
              break;
            case SaveConstants.InventoryData:
              writer.Write(Inventory.Serialize());
              break;
            case SaveConstants.ShardPossession:
              writer.Write(ShardPossession.Serialize());
              break;
            default:
              writer.Write(section.Data);
              break;
          }
        }

        writer.WriteLengthPrefixedString(SaveConstants.None);
        writer.Write(0);

        savedata = writer.ToArray();
      }

      byte[] checksum = new byte[16];
      if (crypt)
      {
        savedata = Crypt(savedata);

        using (MD5 md5 = MD5.Create())
        {
          checksum = md5.ComputeHash(savedata);
        }
      }

      using (BinaryWriter writer = new BinaryWriter(stream))
      {
        writer.Write(savedata);
        writer.Write(checksum);
      }
    }

    public void AddAll()
    {
      //add all items
      AddOrUpdateInventory(Enum.GetValues<ItemIds>()
        .Where(i => i.GetCategory() <= ItemCategories.Books)
        .Select(itemId => new InventoryItem
        {
          ItemId = itemId,
          Quantity = 999
        }));

      //add all shards
      AddOrUpdateInventory(Enum.GetValues<ItemIds>()
        .Where(i => i.GetCategory() >= ItemCategories.ConjureShards
        && i.GetCategory() <= ItemCategories.FamiliarShards)
        .Select(itemId => new Shard
        {
          ItemId = itemId,
          Quantity = 999,
          Rank = 999,
          RankValue = 50f,
          GradeValue = 50f
        }));

      //add all skills
      AddOrUpdateInventory(Enum.GetValues<ItemIds>()
        .Where(i => i.GetCategory() == ItemCategories.SkillShards)
        .Select(itemId => new SkillShard
        {
          ItemId = itemId,
          Quantity = 999,
          Rank = 999,
          RankValue = 50f,
          GradeValue = 50f
        }));
    }

    public void AddOrUpdateInventory(params IItem[] items)
    {
      AddOrUpdateInventory((IEnumerable<IItem>)items);
    }

    public void AddOrUpdateInventory(IEnumerable<IItem> items)
    {
      foreach (IItem item in items)
      {
        //new item
        if (!Inventory.Items.Any(i => i.ItemId == item.ItemId))
        {
          Inventory.Items.Add((InventoryItem)item);

          if (item is SkillShard skillShard)
          {
            ShardPossession.Skills.Add(new SkillShard
            {
              ItemId = skillShard.ItemId,
              IsOn = skillShard.IsOn,
              //Equipped = skillShard.Equipped,
              Quantity = skillShard.Quantity,
              GradeValue = skillShard.GradeValue,
              Rank = skillShard.Rank,
              RankValue = skillShard.RankValue
            });
          }
          else if (item is Shard shard)
          {
            ShardPossession.Shards.Add(new Shard
            {
              ItemId = shard.ItemId,
              //Equipped = shard.Equipped,
              Quantity = shard.Quantity,
              GradeValue = shard.GradeValue,
              Rank = shard.Rank,
              RankValue = shard.RankValue
            });
          }
        }
        else//update item
        {
          IItem existingItem = Inventory.Items.Single(i => i.ItemId == item.ItemId);
          existingItem.Quantity = item.Quantity;
          existingItem.GradeValue = item.GradeValue;
          existingItem.Rank = item.Rank;
          existingItem.RankValue = item.RankValue;

          if (item is SkillShard skillShard)
          {
            SkillShard existingShard = ShardPossession.Skills.Single(s => s.ItemId == item.ItemId);
            existingShard.IsOn = skillShard.IsOn;
            //existingShard.Equipped = skillShard.Equipped;
            existingShard.Quantity = skillShard.Quantity;
            existingShard.GradeValue = skillShard.GradeValue;
            existingShard.Rank = skillShard.Rank;
            existingShard.RankValue = skillShard.RankValue;
          }
          else if (item is Shard shard)
          {
            Shard existingShard = ShardPossession.Shards.Single(s => s.ItemId == item.ItemId);
            existingShard.Quantity = shard.Quantity;
            existingShard.GradeValue = shard.GradeValue;
            //existingShard.Equipped = shard.Equipped;
            existingShard.Rank = shard.Rank;
            existingShard.RankValue = shard.RankValue;
          }
        }
      }
    }
  }
}