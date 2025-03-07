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
using Avalonia.Media;
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
    public ICommand ExitCommand { get; private set; }
    public ICommand AboutCommand { get; private set; }
    public ICommand ShardSelectionChangedCommand { get; private set; }
    public ICommand SetSelectedShardGradeCommand { get; private set; }
    public ICommand SetSelectedShardRankCommand { get; private set; }
    public ICommand ItemSelectionChangedCommand { get; private set; }
    public ICommand SetSelectedItemQuantityCommand { get; private set; }

    public ObservableCollection<InventoryItemModel> InventoryItems { get; private set; }
    public ObservableCollection<InventoryItemModel> Shards { get; private set; }

    public MainViewModel(IFilePickerService filePickerService,
      IWindowService windowService)
    {
      _filePickerService = filePickerService;
      _windowService = windowService;

      OpenedCommand = new AsyncRelayCommand(Initialize);
      OpenCommand = new AsyncRelayCommand(OpenFilePicker);
      CloseCommand = new RelayCommand(CloseSaveSlot);
      SaveCommand = new RelayCommand(SaveSaveSlot);
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

        InventoryItems.Clear();
        foreach (ItemIds itemId in Enum.GetValues<ItemIds>().Where(i => i.GetCategory() <= ItemCategories.Books))
        {
          InventoryItem? inventoryItem = _saveSlot.Inventory.Items.SingleOrDefault(i => i.ItemId == itemId);
          InventoryItems.Add(new InventoryItemModel(itemId,
            quantity: inventoryItem?.Quantity));
        }

        Shards.Clear();
        foreach (ItemIds itemId in Enum.GetValues<ItemIds>().Where(i => i.GetCategory() >= ItemCategories.ConjureShards))
        {
          InventoryItem? inventoryItem = itemId.GetCategory() == ItemCategories.SkillShards
            ? _saveSlot.ShardPossession.Skills.SingleOrDefault(i => i.ItemId == itemId)
            : _saveSlot.ShardPossession.Shards.SingleOrDefault(i => i.ItemId == itemId);

          Shards.Add(new InventoryItemModel(itemId,
            quantity: inventoryItem?.Quantity,
            rank: inventoryItem?.Rank));
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
      IStorageFile? storageFile = (await _filePickerService.Show("Open Save File",
        allowMultiple: false,
        suggestedStartLocation: Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"BloodstainedRotN/Saved/SaveGames".Replace('/', Path.DirectorySeparatorChar)),
        filters: new[] { new("Save Slot") { Patterns = new[] { "*.sav" }, MimeTypes = new[] { "/*" } }, FilePickerFileTypes.All })).SingleOrDefault();

      if (storageFile != null)
      {
        LoadSaveGame(storageFile.Path.AbsolutePath);
      }
    }

    private void SaveSaveSlot()
    {
      _saveSlot.AddOrUpdateInventory(InventoryItems.Where(i => i.IsDirty).Select(im => new InventoryItem
      {
        ItemId = im.ItemId,
        Quantity = im.Quantity ?? 0
      }));

      //normal shards
      _saveSlot.AddOrUpdateInventory(Shards.Where(s => s.IsDirty && s.ItemId.GetCategory() != ItemCategories.SkillShards).Select(sm => new Shard
      {
        ItemId = sm.ItemId,
        Quantity = sm.Quantity ?? 0,
        Rank = sm.Rank ?? 0
      }));

      //skill shards
      _saveSlot.AddOrUpdateInventory(Shards.Where(s => s.IsDirty && s.ItemId.GetCategory() == ItemCategories.SkillShards).Select(sm => new SkillShard
      {
        ItemId = sm.ItemId,
        Quantity = sm.Quantity ?? 0,
        Rank = sm.Rank ?? 0
      }));

      _saveSlot.Save(_loadedSaveSlotPath);
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
      _windowService.ShowWindow<AboutView>();
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
