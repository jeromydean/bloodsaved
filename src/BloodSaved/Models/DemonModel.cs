using CommunityToolkit.Mvvm.ComponentModel;

namespace BloodSaved.Models
{
  public class DemonModel : ObservableObject
  {
    private readonly string _demonId;
    private readonly string _name;
    private bool _isDiscovered;
    private bool _isDirty;

    public string DemonId => _demonId;

    public string Name => _name;

    public bool IsDiscovered
    {
      get => _isDiscovered;
      set
      {
        if (SetProperty(ref _isDiscovered, value))
        {
          _isDirty = true;
        }
      }
    }

    public bool IsDirty => _isDirty;

    public DemonModel(string demonId, bool isDiscovered)
    {
      _demonId = demonId;
      _name = DemonDisplayNames.GetDisplayName(demonId);
      _isDiscovered = isDiscovered;
    }
  }
}
