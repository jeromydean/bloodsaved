using BloodSaved.Parsing;
using BloodSaved.Parsing.Enums;
using BloodSaved.Parsing.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BloodSaved.Models
{
  public class TechniqueModel : ObservableObject
  {
    private readonly ArtsId _artsId;
    private readonly int _arrayIndex;
    private readonly string _name;
    private readonly bool _hasMastery;
    private readonly bool _hasCodexKey;
    private int _useCount;
    private int _experience;
    private int _codexOpenCount;
    private bool _isMastered;
    private bool _isDirty;

    public ArtsId ArtsId => _artsId;

    public int ArrayIndex => _arrayIndex;

    public string Name => _name;

    public string ArtsIdKey => _artsId.GetArtsIdKey();

    public bool HasMastery => _hasMastery;

    public bool HasCodexKey => _hasCodexKey;

    public int UseCount => _useCount;

    public int Experience => _experience;

    public int CodexOpenCount => _codexOpenCount;

    public bool IsMastered
    {
      get => _isMastered;
      set
      {
        if (!_hasMastery)
        {
          return;
        }

        if (!SetProperty(ref _isMastered, value))
        {
          return;
        }

        if (value)
        {
          TechniqueConstants.TryGetNativeMasteredValues(_artsId, out _useCount, out _experience);
          EnsureCodexOpen();
        }
        else
        {
          _experience = 0;
        }

        _isDirty = true;
      }
    }

    public bool IsDirty => _isDirty;

    public TechniqueModel(
      ArtsId artsId,
      int useCount,
      int experience,
      int codexOpenCount)
    {
      _artsId = artsId;
      _arrayIndex = artsId.GetArrayIndex();
      _name = artsId.GetTechniqueName();
      _hasMastery = artsId.HasMastery();
      _hasCodexKey = artsId.HasCodexKey();
      _useCount = useCount;
      _experience = experience;
      _codexOpenCount = codexOpenCount;
      _isMastered = _hasMastery && TechniqueConstants.IsMasteredExperience(artsId, _experience);
    }

    private void EnsureCodexOpen()
    {
      if (!_hasCodexKey || _codexOpenCount >= 1)
      {
        return;
      }

      _codexOpenCount = 1;
      _isDirty = true;
    }
  }
}
