using BloodSaved.Parsing;
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
    private byte[] _flagBytes;
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
          if ((value ?? 0) > 0 && InventoryFlagBytes.IsEmpty(_flagBytes))
          {
            _flagBytes = InventoryFlagBytes.DefaultForNewInInventory(_itemId);
          }

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

    public byte[] FlagBytes
    {
      get => _flagBytes;
    }

    public bool HasFirstTimeFoodBonus =>
      _itemId.GetCategory() == ItemCategory.Food
      && InventoryFlagBytes.HasFirstTimeFoodBonus(_flagBytes);

    public bool IsDirty
    {
      get => _isDirty;
    }

    public InventoryItemModel(ItemId itemId,
      int? quantity = 0,
      int? rank = 0,
      byte[]? flagBytes = null)
    {
      _itemId = itemId;
      _name = itemId.GetName();
      _description = itemId.GetDescription();
      _category = itemId.GetCategory().GetDescription();
      _quantity = quantity;
      _rank = rank;
      _flagBytes = flagBytes != null
        ? InventoryFlagBytes.Copy(flagBytes)
        : InventoryFlagBytes.CreateEmpty();
    }
  }
}
