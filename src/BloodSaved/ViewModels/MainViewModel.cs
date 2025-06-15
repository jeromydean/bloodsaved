using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using BloodSaved.Models;
using BloodSaved.Parsing;
using BloodSaved.Parsing.Enums;
using BloodSaved.Parsing.Extensions;
using BloodSaved.Parsing.Models;
using BloodSaved.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkiaSharp;
using Svg.Skia;

namespace BloodSaved.ViewModels
{
  public partial class MainViewModel : ViewModelBase
  {
    private readonly IWindowService _windowService;
    private readonly IFilePickerService _filePickerService;

    private const double DefaultMapScale = 1d;

    private string? _loadedSaveSlotPath;
    private SaveSlot? _saveSlot;

    [ObservableProperty]
    private string _loadErrorText;

    private SKSvg? _mapSvg;

    [ObservableProperty]
    private SKPicture? _map;

    [ObservableProperty]
    private double _mapScale = DefaultMapScale;

    [ObservableProperty]
    private bool _isSaveSlotLoaded;

    [ObservableProperty]
    private int _totalExperience;

    [ObservableProperty]
    private int _totalCoins;

    [ObservableProperty]
    private EPBGameLevel _ePBGameLevel;
    

    private List<InventoryItemModel>? _selectedShards = null;

    [ObservableProperty]
    private bool _shardsSelected;

    private List<InventoryItemModel>? _selectedItems = null;

    [ObservableProperty]
    private bool _itemsSelected;

    public ICommand OpenedCommand { get; private set; }
    public ICommand OpenCommand { get; private set; }
    public ICommand CloseCommand { get; private set; }
    public ICommand SaveCommand { get; private set; }
    public ICommand SaveAsCommand { get; private set; }
    public ICommand ExitCommand { get; private set; }
    public ICommand AboutCommand { get; private set; }
    public ICommand ShardSelectionChangedCommand { get; private set; }
    public ICommand SetSelectedShardGradeCommand { get; private set; }
    public ICommand SetSelectedShardRankCommand { get; private set; }
    public ICommand ItemSelectionChangedCommand { get; private set; }
    public ICommand SetSelectedItemQuantityCommand { get; private set; }

    public ObservableCollection<InventoryItemModel> InventoryItems { get; private set; }
    public ObservableCollection<InventoryItemModel> Shards { get; private set; }
    public ObservableCollection<EPBGameLevel> EPBGameLevels { get; private set; }
    public ObservableCollection<FamiliarExperienceModel> FamiliarExperience { get; private set; }

    public MainViewModel(IFilePickerService filePickerService,
      IWindowService windowService)
    {
      _filePickerService = filePickerService;
      _windowService = windowService;

      OpenedCommand = new AsyncRelayCommand(Initialize);
      OpenCommand = new AsyncRelayCommand(OpenFilePicker);
      CloseCommand = new RelayCommand(CloseSaveSlot);
      SaveCommand = new RelayCommand(SaveSaveSlot);
      SaveAsCommand = new AsyncRelayCommand<bool>(SaveAs);
      ExitCommand = new RelayCommand(CloseApplication);
      AboutCommand = new RelayCommand(ShowAbout);

      ShardSelectionChangedCommand = new RelayCommand<IList>(SelectedShardsChanged);
      SetSelectedShardGradeCommand = new RelayCommand<int>((g) =>
      {
        _selectedShards.ForEach(s => s.Quantity = g);
      });
      SetSelectedShardRankCommand = new RelayCommand<int>((r) =>
      {
        _selectedShards.ForEach(s => s.Rank = r);
      });

      ItemSelectionChangedCommand = new RelayCommand<IList>(SelectedItemsChanged);
      SetSelectedItemQuantityCommand = new RelayCommand<int>((q) =>
      {
        _selectedItems.ForEach(s => s.Quantity = q);
      });

      InventoryItems = new ObservableCollection<InventoryItemModel>();
      Shards = new ObservableCollection<InventoryItemModel>();
      EPBGameLevels = new ObservableCollection<EPBGameLevel>(Enum.GetValues<EPBGameLevel>());
      FamiliarExperience = new ObservableCollection<FamiliarExperienceModel>();
    }

    private void SelectedItemsChanged(IList selectedItems)
    {
      _selectedItems = selectedItems.Cast<InventoryItemModel>().ToList();
      ItemsSelected = _selectedItems.Any();
    }

    private void SelectedShardsChanged(IList selectedShards)
    {
      _selectedShards = selectedShards.Cast<InventoryItemModel>().ToList();
      ShardsSelected = _selectedShards.Any();
    }

    private async Task Initialize()
    {

    }

    private void LoadSaveGame(string path)
    {
      try
      {
        IsSaveSlotLoaded = false;
        LoadErrorText = null;
        _saveSlot = SaveSlot.Load(path);

        TotalCoins = _saveSlot.Info.TotalCoins;
        TotalExperience = _saveSlot.StatusData.TotalExperience;
        EPBGameLevel = _saveSlot.Info.EPBGameLevel;

        InventoryItems.Clear();
        foreach (IGrouping<ItemCategory, ItemId> groupedItems in Enum.GetValues<ItemId>().Where(i => i.GetCategory() <= ItemCategory.Book).GroupBy(i => i.GetCategory()).OrderBy(g => g.Key))
        {
          foreach(ItemId item in groupedItems.OrderBy(i => i.GetName()))
          {
            InventoryItem? inventoryItem = _saveSlot.Inventory.Items.SingleOrDefault(i => i.ItemId == item);
            InventoryItems.Add(new InventoryItemModel(item,
              quantity: inventoryItem?.Quantity));
          }
        }

        Shards.Clear();
        foreach (IGrouping<ItemCategory, ItemId> groupedItems in Enum.GetValues<ItemId>().Where(i => i.GetCategory() >= ItemCategory.ConjureShards).GroupBy(i => i.GetCategory()).OrderBy(g => g.Key))
        {
          foreach (ItemId item in groupedItems.OrderBy(i => i.GetName()))
          {
            InventoryItem? inventoryItem = item.GetCategory() == ItemCategory.SkillShards
              ? _saveSlot.ShardPossession.Skills.SingleOrDefault(i => i.ItemId == item)
              : _saveSlot.ShardPossession.Shards.SingleOrDefault(i => i.ItemId == item);

            Shards.Add(new InventoryItemModel(item,
              quantity: inventoryItem?.Quantity,
              rank: inventoryItem?.Rank));
          }
        }

        FamiliarExperience.Clear();
        foreach(ItemId familiarItem in Enum.GetValues<ItemId>().Where(i => i.GetCategory() == ItemCategory.FamiliarShards).OrderBy(i => i.GetName()))
        {
          KeyValuePair<ItemId, int> kvp = _saveSlot.StatusData.FamiliarTotalExperience.SingleOrDefault(de => de.Key == familiarItem);
          FamiliarExperience.Add(new FamiliarExperienceModel(familiarItem, kvp.Value));
        }

        Map?.Dispose();
        _mapSvg?.Dispose();
        using (MemoryStream mapStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(_saveSlot.GenerateSvgMap())))
        {
          _mapSvg = new SKSvg();
          _mapSvg.Load(mapStream);
        }

        MapScale = DefaultMapScale;
        Map = _mapSvg.Picture;

        _loadedSaveSlotPath = path;
        IsSaveSlotLoaded = true;
      }
      catch(Exception ex)
      {
        LoadErrorText = $"There was an error loading the save.  Verify you are loading a normal story save and not the system slot or classic game variants.{Environment.NewLine}{ex}";
      }
    }

    public async Task OpenFilePicker()
    {
      IStorageFile? storageFile = (await _filePickerService.OpenFilePickerAsync("Open Save File",
        allowMultiple: false,
        suggestedStartLocation: Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"BloodstainedRotN/Saved/SaveGames".Replace('/', Path.DirectorySeparatorChar)),
        filters: new[] { new("Save Slot") { Patterns = new[] { "*.sav" }, MimeTypes = new[] { "/*" } }, FilePickerFileTypes.All })).SingleOrDefault();

      if (storageFile != null)
      {
        LoadSaveGame(storageFile.Path.LocalPath);
      }
    }

    private void WriteChanges()
    {
      _saveSlot.Info.TotalCoins = TotalCoins;
      _saveSlot.StatusData.TotalExperience = TotalExperience;
      _saveSlot.Info.EPBGameLevel = EPBGameLevel;

      _saveSlot.AddOrUpdateInventory(InventoryItems.Where(i => i.IsDirty).Select(im => new InventoryItem
      {
        ItemId = im.ItemId,
        Quantity = im.Quantity ?? 0
      }));

      //normal shards
      _saveSlot.AddOrUpdateInventory(Shards.Where(s => s.IsDirty && s.ItemId.GetCategory() != ItemCategory.SkillShards).Select(sm => new Shard
      {
        ItemId = sm.ItemId,
        Quantity = sm.Quantity ?? 0,
        Rank = sm.Rank ?? 0
      }));

      //skill shards
      _saveSlot.AddOrUpdateInventory(Shards.Where(s => s.IsDirty && s.ItemId.GetCategory() == ItemCategory.SkillShards).Select(sm => new SkillShard
      {
        ItemId = sm.ItemId,
        Quantity = sm.Quantity ?? 0,
        Rank = sm.Rank ?? 0
      }));

      //familiar experience
      foreach (FamiliarExperienceModel fem in FamiliarExperience.Where(fe => fe.IsDirty))
      {
        _saveSlot.StatusData.FamiliarTotalExperience[fem.ItemId] = fem.Experience ?? 0;
      }
    }

    private void SaveSaveSlot()
    {
      WriteChanges();
      _saveSlot.Save(_loadedSaveSlotPath);
    }

    private async Task SaveAs(bool forceEncryption)
    {
      IStorageFile? saveAsStorageFile = await _filePickerService.SaveFilePickerAsync($"Save {(forceEncryption ? "Encrypted" : "Decrypted")} As");
      if (saveAsStorageFile != null)
      {
        WriteChanges();
        _saveSlot.Save(saveAsStorageFile.Path.LocalPath, forceEncryption);
      }
    }

    private void CloseSaveSlot()
    {
      IsSaveSlotLoaded = false;
      _saveSlot = null;
      InventoryItems.Clear();
      Shards.Clear();
      Map?.Dispose();
      _mapSvg?.Dispose();
      MapScale = DefaultMapScale;
      _loadedSaveSlotPath = null;
    }

    public void ShowAbout()
    {
      _windowService.ShowDialog<AboutView>();
    }

    public void CloseApplication()
    {
      if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktopApp)
      {
        desktopApp.Shutdown();
      }
    }
  }
}
