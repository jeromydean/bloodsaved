using System;
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

    private SaveSlot? _saveSlot;

    private SKSvg? _mapSvg;

    [ObservableProperty]
    private SKPicture? _map;

    [ObservableProperty]
    private double _mapScale = DefaultMapScale;

    [ObservableProperty]
    private bool _isSaveSlotLoaded;

    public ICommand OpenedCommand { get; private set; }
    public ICommand OpenCommand { get; private set; }
    public ICommand CloseCommand { get; private set; }
    public ICommand ExitCommand { get; private set; }
    public ICommand AboutCommand { get; private set; }

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
      ExitCommand = new RelayCommand(CloseApplication);
      AboutCommand = new RelayCommand(ShowAbout);

      InventoryItems = new ObservableCollection<InventoryItemModel>();
      Shards = new ObservableCollection<InventoryItemModel>();
    }

    private async Task Initialize()
    {

    }

    private void LoadSaveGame(string path)
    {
      try
      {
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
          InventoryItem? inventoryItem = _saveSlot.Inventory.Items.SingleOrDefault(i => i.ItemId == itemId);
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

        IsSaveSlotLoaded = true;
      }
      catch(Exception ex)
      {
        //todo show user the error
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

    private void CloseSaveSlot()
    {
      IsSaveSlotLoaded = false;
      _saveSlot = null;
      InventoryItems.Clear();
      Shards.Clear();
      Map?.Dispose();
      _mapSvg?.Dispose();
      MapScale = DefaultMapScale;
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
