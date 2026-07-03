using BloodSaved.Parsing.Enums;
using BloodSaved.Parsing.Extensions;
using BloodSaved.Parsing.Models;

namespace BloodSaved.Parsing.Sections
{
  public class GameRecord : ISerializableSection<GameRecord>
  {
    private readonly List<SaveSection> _saveSections = new();

    public PcInfoRecord PcInfo { get; set; } = new PcInfoRecord();

    public Dictionary<string, int> TotalBookshelfDiary { get; set; } = new(StringComparer.Ordinal);

    public Dictionary<string, int> TotalSpawnFamiliar { get; set; } = new(StringComparer.Ordinal);

    public Dictionary<string, int> TotalKill { get; set; } = new(StringComparer.Ordinal);

    public Dictionary<string, int> TotalItem { get; set; } = new(StringComparer.Ordinal);

    public Dictionary<string, int> TotalDropCount { get; set; } = new(StringComparer.Ordinal);

    private bool _totalBookshelfDiaryDirty;

    private bool _totalSpawnFamiliarDirty;

    private bool _totalKillDirty;

    private bool _totalItemDirty;

    private bool _totalDropCountDirty;

    public int TotalSalesGold { get; set; }

    public int BorrowBooksCount { get; set; }

    public int CookedDishesType { get; set; }

    public string LastEatingDish { get; set; } = string.Empty;

    public int AlchemyCraftCount { get; set; }

    public int QuestClearCount { get; set; }

    public int OpenTreasureBoxCount { get; set; }

    public int DestroyedCandleCount { get; set; }

    public int TotalODChairAction { get; set; }

    public int TotalBrokenWall { get; set; }

    public int TotalRoomInCount { get; set; }

    public int TotalKillCount { get; set; }

  private static readonly string[] CanonicalPropertyOrder =
  [
    SaveConstants.PCInfo,
    SaveConstants.m_TotalSalesGold,
    SaveConstants.m_BorrowBooksCount,
    SaveConstants.m_CookedDishesType,
    SaveConstants.m_EatingDishesTypes,
    SaveConstants.m_LastEatingDish,
    SaveConstants.m_AlchemyCraftCount,
    SaveConstants.m_QuestClearCount,
    SaveConstants.m_OpenTreasureBoxCount,
    SaveConstants.m_DestroyedCandleCount,
    SaveConstants.m_TotalItem,
    SaveConstants.m_TotalItemConsumed,
    SaveConstants.m_TotalKill,
    SaveConstants.m_TotalRoomIn,
    SaveConstants.m_TotalBookshelfDiary,
    SaveConstants.m_TotalTipsOpened,
    SaveConstants.m_TotalODChairAction,
    SaveConstants.m_TotalBrokenWall,
    SaveConstants.m_TotalRoomInCount,
    SaveConstants.m_TotalKillCount,
    SaveConstants.m_TotalSpawnFamiliar,
    SaveConstants.m_TotalDropCount,
  ];

  private static readonly string[] SynthesizableIntProperties =
  [
    SaveConstants.m_TotalSalesGold,
    SaveConstants.m_BorrowBooksCount,
    SaveConstants.m_CookedDishesType,
    SaveConstants.m_AlchemyCraftCount,
    SaveConstants.m_QuestClearCount,
    SaveConstants.m_OpenTreasureBoxCount,
    SaveConstants.m_DestroyedCandleCount,
    SaveConstants.m_TotalODChairAction,
    SaveConstants.m_TotalBrokenWall,
    SaveConstants.m_TotalRoomInCount,
    SaveConstants.m_TotalKillCount,
  ];

    public static GameRecord Deserialize(SaveSection saveSection)
    {
      GameRecord gameRecord = new GameRecord();
      using SaveReader saveReader = new SaveReader(saveSection.Data);
      saveReader.ReadArrayProperty(SaveConstants.GameRecord, out _, out _, out _);

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

        if (string.Equals(name, SaveConstants.PCInfo, StringComparison.Ordinal)
          && type == SaveConstants.StructProperty)
        {
          gameRecord.PcInfo = PcInfoRecord.Deserialize(saveReader);
        }
        else if (string.Equals(name, SaveConstants.m_TotalBookshelfDiary, StringComparison.Ordinal)
          && type == SaveConstants.MapProperty)
        {
          ReadNameIntMap(saveReader, name, gameRecord.TotalBookshelfDiary);
        }
        else if (string.Equals(name, SaveConstants.m_TotalSpawnFamiliar, StringComparison.Ordinal)
          && type == SaveConstants.MapProperty)
        {
          ReadNameIntMap(saveReader, name, gameRecord.TotalSpawnFamiliar);
        }
        else if (string.Equals(name, SaveConstants.m_TotalKill, StringComparison.Ordinal)
          && type == SaveConstants.MapProperty)
        {
          ReadNameIntMap(saveReader, name, gameRecord.TotalKill);
        }
        else if (string.Equals(name, SaveConstants.m_TotalItem, StringComparison.Ordinal)
          && type == SaveConstants.MapProperty)
        {
          ReadNameIntMapOrSkip(saveReader, name, gameRecord.TotalItem);
        }
        else if (string.Equals(name, SaveConstants.m_TotalDropCount, StringComparison.Ordinal)
          && type == SaveConstants.MapProperty)
        {
          ReadNameIntMapOrSkip(saveReader, name, gameRecord.TotalDropCount);
        }
        else
        {
          switch (type)
          {
            case SaveConstants.IntProperty:
              gameRecord.AssignIntProperty(name, saveReader.ReadIntProperty(name));
              break;
            case SaveConstants.FloatProperty:
              saveReader.ReadFloatProperty(name);
              break;
            case SaveConstants.BoolProperty:
              saveReader.ReadBoolProperty(name);
              break;
            case SaveConstants.NameProperty when string.Equals(name, SaveConstants.m_LastEatingDish, StringComparison.Ordinal):
              gameRecord.LastEatingDish = saveReader.ReadNameProperty(name);
              break;
            case SaveConstants.NameProperty:
              saveReader.ReadNameProperty(name);
              break;
            case SaveConstants.ArrayProperty:
              saveReader.ReadArrayProperty(name, out _, out int arrayPropertyLength, out _);
              saveReader.Skip(arrayPropertyLength - 4);
              break;
            case SaveConstants.MapProperty:
              saveReader.ReadMapProperty(name, out _, out _, out int mapPropertyLength, out _);
              saveReader.Skip(mapPropertyLength - 8);
              break;
            case SaveConstants.StructProperty:
              saveReader.ReadStructProperty(name, out _, out int structPropertyLength, out _);
              saveReader.Skip(structPropertyLength);
              break;
            default:
              throw new NotImplementedException($"Unhandled GameRecord property '{name}' ({type}).");
          }
        }

        gameRecord._saveSections.Add(new SaveSection
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
        throw new InvalidDataException($"Expected end of '{nameof(GameRecord)}' element.");
      }

      return gameRecord;
    }

    public byte[] Serialize()
    {
      using SaveWriter saveWriter = new SaveWriter();
      saveWriter.WriteArrayProperty(SaveConstants.GameRecord, SaveConstants.ByteProperty, out long gameRecordLengthOffset, out long gameRecordCountOffset);
      saveWriter.SetCheckpoint();

      HashSet<string> presentInSave = _saveSections
        .Select(section => section.Name)
        .ToHashSet(StringComparer.Ordinal);
      HashSet<string> synthesizedWritten = new(StringComparer.Ordinal);

      foreach (SaveSection section in _saveSections)
      {
        WriteSynthesizedPropertiesBefore(saveWriter, section.Name, presentInSave, synthesizedWritten);

        if (TryWriteKnownProperty(saveWriter, section))
        {
          continue;
        }

        saveWriter.Write(section.Data);
      }

      WriteSynthesizedPropertiesBefore(saveWriter, nextSectionName: null, presentInSave, synthesizedWritten);

      saveWriter.WriteLengthPrefixedString(SaveConstants.None);
      saveWriter.Write(0);

      int gameRecordLength = (int)(saveWriter.CurrentPosition - gameRecordCountOffset);
      saveWriter.SetPosition(gameRecordLengthOffset);
      saveWriter.Write(gameRecordLength);
      saveWriter.SetPosition(gameRecordCountOffset);
      saveWriter.Write(gameRecordLength - 4);

      return saveWriter.ToArray();
    }

    private void WriteSynthesizedPropertiesBefore(
      SaveWriter saveWriter,
      string? nextSectionName,
      HashSet<string> presentInSave,
      HashSet<string> synthesizedWritten)
    {
      int nextOrder = GetCanonicalOrder(nextSectionName);

      foreach (string propertyName in CanonicalPropertyOrder)
      {
        if (GetCanonicalOrder(propertyName) >= nextOrder)
        {
          continue;
        }

        if (presentInSave.Contains(propertyName) || !synthesizedWritten.Add(propertyName))
        {
          continue;
        }

        if (string.Equals(propertyName, SaveConstants.PCInfo, StringComparison.Ordinal))
        {
          if (!PcInfo.HasSerializableData)
          {
            synthesizedWritten.Remove(propertyName);
            continue;
          }

          saveWriter.Write(PcInfo.Serialize());
          continue;
        }

        if (string.Equals(propertyName, SaveConstants.m_TotalBookshelfDiary, StringComparison.Ordinal))
        {
          if (TotalBookshelfDiary.Count == 0)
          {
            synthesizedWritten.Remove(propertyName);
            continue;
          }

          _totalBookshelfDiaryDirty = true;
          WriteNameIntMapProperty(saveWriter, SaveConstants.m_TotalBookshelfDiary, TotalBookshelfDiary);
          continue;
        }

        if (string.Equals(propertyName, SaveConstants.m_TotalSpawnFamiliar, StringComparison.Ordinal))
        {
          if (TotalSpawnFamiliar.Count == 0)
          {
            synthesizedWritten.Remove(propertyName);
            continue;
          }

          _totalSpawnFamiliarDirty = true;
          WriteNameIntMapProperty(saveWriter, SaveConstants.m_TotalSpawnFamiliar, TotalSpawnFamiliar);
          continue;
        }

        if (string.Equals(propertyName, SaveConstants.m_TotalKill, StringComparison.Ordinal))
        {
          if (TotalKill.Count == 0)
          {
            synthesizedWritten.Remove(propertyName);
            continue;
          }

          _totalKillDirty = true;
          WriteNameIntMapProperty(saveWriter, SaveConstants.m_TotalKill, TotalKill);
          continue;
        }

        if (string.Equals(propertyName, SaveConstants.m_TotalItem, StringComparison.Ordinal))
        {
          if (TotalItem.Count == 0)
          {
            synthesizedWritten.Remove(propertyName);
            continue;
          }

          _totalItemDirty = true;
          WriteNameIntMapProperty(saveWriter, SaveConstants.m_TotalItem, TotalItem);
          continue;
        }

        if (string.Equals(propertyName, SaveConstants.m_TotalDropCount, StringComparison.Ordinal))
        {
          if (TotalDropCount.Count == 0)
          {
            synthesizedWritten.Remove(propertyName);
            continue;
          }

          _totalDropCountDirty = true;
          WriteNameIntMapProperty(saveWriter, SaveConstants.m_TotalDropCount, TotalDropCount);
          continue;
        }

        if (!SynthesizableIntProperties.Contains(propertyName, StringComparer.Ordinal))
        {
          synthesizedWritten.Remove(propertyName);
          continue;
        }

        if (GetIntProperty(propertyName) == 0)
        {
          synthesizedWritten.Remove(propertyName);
          continue;
        }

        WriteIntProperty(saveWriter, propertyName);
      }
    }

    private static int GetCanonicalOrder(string? propertyName)
    {
      if (propertyName == null)
      {
        return int.MaxValue;
      }

      for (int i = 0; i < CanonicalPropertyOrder.Length; i++)
      {
        if (string.Equals(CanonicalPropertyOrder[i], propertyName, StringComparison.Ordinal))
        {
          return i;
        }
      }

      return int.MaxValue;
    }

    private int GetIntProperty(string propertyName)
    {
      return propertyName switch
      {
        SaveConstants.m_TotalSalesGold => TotalSalesGold,
        SaveConstants.m_BorrowBooksCount => BorrowBooksCount,
        SaveConstants.m_CookedDishesType => CookedDishesType,
        SaveConstants.m_AlchemyCraftCount => AlchemyCraftCount,
        SaveConstants.m_QuestClearCount => QuestClearCount,
        SaveConstants.m_OpenTreasureBoxCount => OpenTreasureBoxCount,
        SaveConstants.m_DestroyedCandleCount => DestroyedCandleCount,
        SaveConstants.m_TotalODChairAction => TotalODChairAction,
        SaveConstants.m_TotalBrokenWall => TotalBrokenWall,
        SaveConstants.m_TotalRoomInCount => TotalRoomInCount,
        SaveConstants.m_TotalKillCount => TotalKillCount,
        _ => 0,
      };
    }

    private void WriteIntProperty(SaveWriter saveWriter, string propertyName)
    {
      saveWriter.WriteIntProperty(propertyName, GetIntProperty(propertyName));
    }

    private bool TryWriteKnownProperty(SaveWriter saveWriter, SaveSection section)
    {
      if (string.Equals(section.Name, SaveConstants.PCInfo, StringComparison.Ordinal))
      {
        saveWriter.Write(PcInfo.Serialize());
        return true;
      }

      if (string.Equals(section.Name, SaveConstants.m_TotalBookshelfDiary, StringComparison.Ordinal))
      {
        if (_totalBookshelfDiaryDirty)
        {
          WriteNameIntMapProperty(saveWriter, SaveConstants.m_TotalBookshelfDiary, TotalBookshelfDiary);
        }

        return _totalBookshelfDiaryDirty;
      }

      if (string.Equals(section.Name, SaveConstants.m_TotalSpawnFamiliar, StringComparison.Ordinal))
      {
        if (_totalSpawnFamiliarDirty)
        {
          WriteNameIntMapProperty(saveWriter, SaveConstants.m_TotalSpawnFamiliar, TotalSpawnFamiliar);
        }

        return _totalSpawnFamiliarDirty;
      }

      if (string.Equals(section.Name, SaveConstants.m_TotalKill, StringComparison.Ordinal))
      {
        if (_totalKillDirty)
        {
          WriteNameIntMapProperty(saveWriter, SaveConstants.m_TotalKill, TotalKill);
        }

        return _totalKillDirty;
      }

      if (string.Equals(section.Name, SaveConstants.m_TotalItem, StringComparison.Ordinal))
      {
        if (_totalItemDirty)
        {
          WriteNameIntMapProperty(saveWriter, SaveConstants.m_TotalItem, TotalItem);
        }

        return _totalItemDirty;
      }

      if (string.Equals(section.Name, SaveConstants.m_TotalDropCount, StringComparison.Ordinal))
      {
        if (_totalDropCountDirty)
        {
          WriteNameIntMapProperty(saveWriter, SaveConstants.m_TotalDropCount, TotalDropCount);
        }

        return _totalDropCountDirty;
      }

      if (string.Equals(section.Name, SaveConstants.m_TotalSalesGold, StringComparison.Ordinal))
      {
        WriteIntProperty(saveWriter, SaveConstants.m_TotalSalesGold);
        return true;
      }

      if (string.Equals(section.Name, SaveConstants.m_BorrowBooksCount, StringComparison.Ordinal))
      {
        WriteIntProperty(saveWriter, SaveConstants.m_BorrowBooksCount);
        return true;
      }

      if (string.Equals(section.Name, SaveConstants.m_CookedDishesType, StringComparison.Ordinal))
      {
        WriteIntProperty(saveWriter, SaveConstants.m_CookedDishesType);
        return true;
      }

      if (string.Equals(section.Name, SaveConstants.m_AlchemyCraftCount, StringComparison.Ordinal))
      {
        WriteIntProperty(saveWriter, SaveConstants.m_AlchemyCraftCount);
        return true;
      }

      if (string.Equals(section.Name, SaveConstants.m_QuestClearCount, StringComparison.Ordinal))
      {
        WriteIntProperty(saveWriter, SaveConstants.m_QuestClearCount);
        return true;
      }

      if (string.Equals(section.Name, SaveConstants.m_OpenTreasureBoxCount, StringComparison.Ordinal))
      {
        WriteIntProperty(saveWriter, SaveConstants.m_OpenTreasureBoxCount);
        return true;
      }

      if (string.Equals(section.Name, SaveConstants.m_DestroyedCandleCount, StringComparison.Ordinal))
      {
        WriteIntProperty(saveWriter, SaveConstants.m_DestroyedCandleCount);
        return true;
      }

      if (string.Equals(section.Name, SaveConstants.m_TotalODChairAction, StringComparison.Ordinal))
      {
        WriteIntProperty(saveWriter, SaveConstants.m_TotalODChairAction);
        return true;
      }

      if (string.Equals(section.Name, SaveConstants.m_TotalBrokenWall, StringComparison.Ordinal))
      {
        WriteIntProperty(saveWriter, SaveConstants.m_TotalBrokenWall);
        return true;
      }

      if (string.Equals(section.Name, SaveConstants.m_TotalRoomInCount, StringComparison.Ordinal))
      {
        WriteIntProperty(saveWriter, SaveConstants.m_TotalRoomInCount);
        return true;
      }

      if (string.Equals(section.Name, SaveConstants.m_TotalKillCount, StringComparison.Ordinal))
      {
        WriteIntProperty(saveWriter, SaveConstants.m_TotalKillCount);
        return true;
      }

      return false;
    }

    private static void ReadNameIntMap(SaveReader saveReader, string propertyName, Dictionary<string, int> target)
    {
      saveReader.ReadMapProperty(propertyName, out _, out _, out _, out int count);
      for (int i = 0; i < count; i++)
      {
        string key = saveReader.ReadLengthPrefixedString();
        int value = saveReader.ReadInt32();
        target[key] = value;
      }
    }

    private static void ReadNameIntMapOrSkip(SaveReader saveReader, string propertyName, Dictionary<string, int> target)
    {
      saveReader.ReadMapProperty(propertyName, out string keyType, out string valueType, out int length, out int count);
      if (!string.Equals(keyType, SaveConstants.NameProperty, StringComparison.Ordinal)
        || !string.Equals(valueType, SaveConstants.IntProperty, StringComparison.Ordinal))
      {
        saveReader.Skip(length - 8);
        return;
      }

      for (int i = 0; i < count; i++)
      {
        string key = saveReader.ReadLengthPrefixedString();
        int value = saveReader.ReadInt32();
        target[key] = value;
      }
    }

    private static void WriteNameIntMapProperty(
      SaveWriter saveWriter,
      string propertyName,
      Dictionary<string, int> entries)
    {
      saveWriter.WriteMapProperty(propertyName, SaveConstants.NameProperty,
        SaveConstants.IntProperty, out long mapLengthOffset, out long mapCountOffset, count: entries.Count);

      foreach (KeyValuePair<string, int> entry in entries)
      {
        saveWriter.WriteLengthPrefixedString(entry.Key);
        saveWriter.Write(entry.Value);
      }

      int mapLength = (int)(saveWriter.CurrentPosition - (mapCountOffset - 4));
      long resumePosition = saveWriter.CurrentPosition;
      saveWriter.SetPosition(mapLengthOffset);
      saveWriter.Write(mapLength);
      saveWriter.SetPosition(resumePosition);
    }

    public int UniqueFamiliarsSummonedCount => TotalSpawnFamiliar.Count;

    public int DiscoveredDemonsCount => TotalKill.Keys.Count(GameRecordArchiveStatKeys.DemonKillKeys.Contains);

    public static IReadOnlyList<string> AllDemonKeys => GameRecordArchiveStatKeys.DemonKillKeys;

    public void SetUniqueFamiliarsSummonedCount(int count)
    {
      count = Math.Clamp(count, 0, GameRecordArchiveStatKeys.MaxUniqueFamiliarsSummoned);
      _totalSpawnFamiliarDirty = true;
      SetArchiveEntryCount(
        TotalSpawnFamiliar,
        GameRecordArchiveStatKeys.FamiliarSpawnKeys,
        count,
        defaultValue: 1);
    }

    public void SetAllDemonsDiscovered()
    {
      SetDiscoveredDemons(GameRecordArchiveStatKeys.DemonKillKeys);
    }

    public void SetDiscoveredDemons(IEnumerable<string> demonKeys)
    {
      _totalKillDirty = true;
      HashSet<string> discovered = demonKeys
        .Where(GameRecordArchiveStatKeys.DemonKillKeys.Contains)
        .ToHashSet(StringComparer.Ordinal);
      HashSet<string> archiveKeys = GameRecordArchiveStatKeys.GetDemonArchiveKeysForDemons(discovered)
        .ToHashSet(StringComparer.Ordinal);

      foreach (string key in GameRecordArchiveStatKeys.DemonArchiveKeys.Where(key => !archiveKeys.Contains(key)))
      {
        TotalKill.Remove(key);
      }

      foreach (string key in archiveKeys)
      {
        TotalKill[key] = TotalKill.TryGetValue(key, out int existing) && existing > 0 ? existing : 1;
      }

      foreach (string shardItemKey in GameRecordArchiveStatKeys.GetShardItemKeysForDemons(discovered))
      {
        if (!TotalItem.ContainsKey(shardItemKey))
        {
          TotalItem[shardItemKey] = 1;
          _totalItemDirty = true;
        }
      }

      TotalKillCount = Math.Max(TotalKillCount, discovered.Count);
    }

    private static void SetArchiveEntryCount(
      Dictionary<string, int> target,
      IReadOnlyList<string> canonicalKeys,
      int count,
      int defaultValue)
    {
      if (count < 0)
      {
        throw new ArgumentOutOfRangeException(nameof(count));
      }

      HashSet<string> retainedKeys = canonicalKeys.Take(count).ToHashSet(StringComparer.Ordinal);
      foreach (string key in canonicalKeys.Skip(count).Where(target.ContainsKey).ToArray())
      {
        target.Remove(key);
      }

      for (int i = 0; i < count && i < canonicalKeys.Count; i++)
      {
        string key = canonicalKeys[i];
        target[key] = target.TryGetValue(key, out int existing) && existing > 0 ? existing : defaultValue;
      }
    }

    private void AssignIntProperty(string name, int value)
    {
      switch (name)
      {
        case SaveConstants.m_TotalSalesGold:
          TotalSalesGold = value;
          break;
        case SaveConstants.m_BorrowBooksCount:
          BorrowBooksCount = value;
          break;
        case SaveConstants.m_CookedDishesType:
          CookedDishesType = value;
          break;
        case SaveConstants.m_AlchemyCraftCount:
          AlchemyCraftCount = value;
          break;
        case SaveConstants.m_QuestClearCount:
          QuestClearCount = value;
          break;
        case SaveConstants.m_OpenTreasureBoxCount:
          OpenTreasureBoxCount = value;
          break;
        case SaveConstants.m_DestroyedCandleCount:
          DestroyedCandleCount = value;
          break;
        case SaveConstants.m_TotalODChairAction:
          TotalODChairAction = value;
          break;
        case SaveConstants.m_TotalBrokenWall:
          TotalBrokenWall = value;
          break;
        case SaveConstants.m_TotalRoomInCount:
          TotalRoomInCount = value;
          break;
        case SaveConstants.m_TotalKillCount:
          TotalKillCount = value;
          break;
      }
    }

    public Dictionary<ArtsId, int> GetArtsCodexOpenCounts()
    {
      Dictionary<ArtsId, int> artsCounts = new();
      foreach (KeyValuePair<string, int> entry in TotalBookshelfDiary)
      {
        if (ArtsIdExtensions.TryParseArtsIdKey(entry.Key, out ArtsId artsId))
        {
          artsCounts[artsId] = entry.Value;
        }
      }

      return artsCounts;
    }

    public int GetArtsUseCount(TechniqueCommandSlot slot)
    {
      ArtsId? artsId = slot.GetArtsId();
      return artsId.HasValue ? GetArtsUseCount(artsId.Value) : 0;
    }

    public int GetArtsExperience(TechniqueCommandSlot slot)
    {
      ArtsId? artsId = slot.GetArtsId();
      return artsId.HasValue ? GetArtsExperience(artsId.Value) : 0;
    }

    public void SetArtsUseCount(TechniqueCommandSlot slot, int useCount)
    {
      ArtsId? artsId = slot.GetArtsId();
      if (artsId.HasValue)
      {
        SetArtsUseCount(artsId.Value, useCount);
      }
    }

    public void SetArtsExperience(TechniqueCommandSlot slot, int experience)
    {
      ArtsId? artsId = slot.GetArtsId();
      if (artsId.HasValue)
      {
        SetArtsExperience(artsId.Value, experience);
      }
    }

    public int GetArtsUseCount(ArtsId artsId)
    {
      return PcInfo.ArtsUseNum[artsId.GetArrayIndex()];
    }

    public int GetArtsExperience(ArtsId artsId)
    {
      return PcInfo.ArtsExperience[artsId.GetArrayIndex()];
    }

    public int GetArtsCodexOpenCount(ArtsId artsId)
    {
      if (!artsId.HasCodexKey())
      {
        return 0;
      }

      string key = artsId.GetArtsIdKey();
      return TotalBookshelfDiary.TryGetValue(key, out int count) ? count : 0;
    }

    public void SetArtsCodexOpenCount(ArtsId artsId, int count)
    {
      if (!artsId.HasCodexKey())
      {
        return;
      }

      string key = artsId.GetArtsIdKey();
      if (count <= 0)
      {
        if (TotalBookshelfDiary.Remove(key))
        {
          _totalBookshelfDiaryDirty = true;
        }

        return;
      }

      _totalBookshelfDiaryDirty = true;
      TotalBookshelfDiary[key] = count;
    }

    public void EnsureArtsKnown(ArtsId artsId)
    {
      if (GetArtsUseCount(artsId) < 1)
      {
        SetArtsUseCount(artsId, 1);
        return;
      }

      EnsureArtsCodexOpen(artsId);
    }

    public void SetArtsUseCount(ArtsId artsId, int useCount)
    {
      PcInfo.ArtsUseNum[artsId.GetArrayIndex()] = useCount;
      if (useCount > 0)
      {
        EnsureArtsCodexOpen(artsId);
      }
    }

    public void SetArtsExperience(ArtsId artsId, int experience)
    {
      PcInfo.ArtsExperience[artsId.GetArrayIndex()] = experience;
      if (experience > 0)
      {
        EnsureArtsKnown(artsId);
      }
    }

    public bool IsArtsMastered(ArtsId artsId)
    {
      return TechniqueConstants.IsMasteredExperience(artsId, GetArtsExperience(artsId));
    }

    public void SetArtsMastered(ArtsId artsId, bool mastered)
    {
      if (!artsId.HasMastery())
      {
        return;
      }

      if (mastered)
      {
        TechniqueNativeMasteredValues.TryGet(artsId, out int useCount, out int experience);
        SetArtsUseCount(artsId, useCount);
        PcInfo.ArtsExperience[artsId.GetArrayIndex()] = experience;
        EnsureArtsCodexOpen(artsId);
        return;
      }

      SetArtsUseCount(artsId, 0);
      SetArtsExperience(artsId, 0);
    }

    private void EnsureArtsCodexOpen(ArtsId artsId)
    {
      if (artsId.HasCodexKey() && GetArtsCodexOpenCount(artsId) < 1)
      {
        SetArtsCodexOpenCount(artsId, 1);
      }
    }
  }
}
