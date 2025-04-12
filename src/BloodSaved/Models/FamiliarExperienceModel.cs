using Avalonia.Controls.Primitives;
using BloodSaved.Parsing.Enums;
using BloodSaved.Parsing.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BloodSaved.Models
{
  public class FamiliarExperienceModel : ObservableObject
  {
    private readonly ItemIds _itemId;
    private readonly string _name;
    private int? _experience;
    private bool _isDirty;

    public ItemIds ItemId
    {
      get => _itemId;
    }

    public string Name
    {
      get => _name;
    }

    public int? Experience
    {
      get => _experience;
      set
      {
        if (SetProperty(ref _experience, value))
        {
          _isDirty = true;
        }
      }
    }

    public bool IsDirty
    {
      get => _isDirty;
    }

    public FamiliarExperienceModel(ItemIds itemId,
      int? experience = 0)
    {
      _itemId = itemId;
      _name = itemId.GetDescription();
      _experience = experience;
    }
  }
}
