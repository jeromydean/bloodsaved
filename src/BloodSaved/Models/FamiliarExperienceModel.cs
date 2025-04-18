using BloodSaved.Parsing.Enums;
using BloodSaved.Parsing.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BloodSaved.Models
{
  public class FamiliarExperienceModel : ObservableObject
  {
    private readonly ItemId _itemId;
    private readonly string _name;
    private readonly string _description;
    private int? _experience;
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

    public FamiliarExperienceModel(ItemId itemId,
      int? experience = 0)
    {
      _itemId = itemId;
      _name = itemId.GetName();
      _description = itemId.GetDescription();
      _experience = experience;
    }
  }
}
