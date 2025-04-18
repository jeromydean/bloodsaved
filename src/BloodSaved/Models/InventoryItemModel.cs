using BloodSaved.Parsing.Enums;
using BloodSaved.Parsing.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BloodSaved.Models
{
  public class InventoryItemModel : ObservableObject
  {
    private readonly ItemId _itemId;
    private readonly string _name;
    private readonly string _description;
    private readonly string _category;
    private int? _quantity;
    private int? _rank;
    private bool _isDirty;

    public ItemId ItemId
    {
      get => _itemId;
    }

    public string Name
    {
      get => _name;
    }

    public string Description
    {
      get => _description;
    }

    public string Category
    {
      get => _category;
    }

    public int? Quantity
    {
      get => _quantity;
      set
      {
        if (SetProperty(ref _quantity, value))
        {
          _isDirty = true;
        }
      } 
    }

    public int? Rank
    {
      get => _rank;
      set
      {
        if (SetProperty(ref _rank, value))
        {
          _isDirty = true;
        }
      }
    }

    public bool IsDirty
    {
      get => _isDirty;
    }

    public InventoryItemModel(ItemId itemId,
      int? quantity = 0,
      int? rank = 0)
    {
      _itemId = itemId;
      _name = itemId.GetName();
      _description = itemId.GetDescription();
      _category = itemId.GetCategory().GetDescription();
      _quantity = quantity;
      _rank = rank;
    }
  }
}