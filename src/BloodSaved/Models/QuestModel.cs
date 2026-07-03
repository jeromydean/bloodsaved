using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BloodSaved.Models
{
  public class QuestModel : ObservableObject
  {
    private readonly string _questId;
    private readonly string _name;
    private readonly string _category;
    private bool _isCompleted;
    private bool _isDirty;

    public string QuestId => _questId;

    public string Name => _name;

    public string Category => _category;

    public bool IsCompleted
    {
      get => _isCompleted;
      set
      {
        if (SetProperty(ref _isCompleted, value))
        {
          _isDirty = true;
        }
      }
    }

    public bool IsDirty => _isDirty;

    public QuestModel(string questId, bool isCompleted)
    {
      _questId = questId;
      _name = QuestDisplayNames.GetDisplayName(questId);
      _category = GetCategory(questId);
      _isCompleted = isCompleted;
    }

    private static string GetCategory(string questId)
    {
      if (questId.StartsWith("Quest_Enemy", StringComparison.Ordinal))
      {
        return "Enemy";
      }

      if (questId.StartsWith("Quest_Memento", StringComparison.Ordinal))
      {
        return "Memento";
      }

      if (questId.StartsWith("Quest_Catering", StringComparison.Ordinal))
      {
        return "Food";
      }

      if (questId.StartsWith("Quest_StrayMan", StringComparison.Ordinal))
      {
        return "Stray Man";
      }

      return "Other";
    }
  }
}
