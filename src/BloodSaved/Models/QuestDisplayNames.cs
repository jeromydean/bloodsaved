using System;
using System.Collections.Generic;

namespace BloodSaved.Models
{
  public static class QuestDisplayNames
  {
    private static readonly IReadOnlyList<string> EnemyNames =
    [
      "my husband",
      "Rosaly",
      "Annette",
      "Lisa",
      "Simon",
      "Porter",
      "Dan",
      "Edward",
      "Shawn",
      "Dario",
      "Trevor",
      "Martha",
      "Mathius",
      "Soleiyu",
      "Carl",
      "Mitta",
      "Sypha",
      "Juste",
      "Richter",
      "Julius",
    ];

    private static readonly IReadOnlyList<string> MementoNames =
    [
      "Catharine",
      "Caleb",
      "Noel",
      "Morris",
      "Talia",
      "Norah",
      "Edith",
      "Alba",
      "Dennis",
      "Lily",
      "Wanda",
      "Rachel",
      "Nadia",
      "Enos",
      "George",
    ];

    private static readonly IReadOnlyList<string> CateringNames =
    [
      "a Portable Snack",
      "something with Sea Urchin",
      "a Risotto",
      "a Novel Idea",
      "Something Spicy",
      "a Baked Dish",
      "Saucy Strands",
      "Yellow Kernels",
      "Noodles in Broth",
      "Something Refreshing",
      "a Classic Sweet",
      "Crispy Skin",
      "Something Breaded",
      "Fried Finger Food",
      "Food That Falls Apart",
      "Something Fluffy",
      "Something Irresistible",
      "a Beef Hot Pot",
      "a Rolled-up Treat",
      "a Miracle",
      "Something Life-changing",
    ];

    private static readonly IReadOnlyList<string> StrayManTitles =
    [
      "The Stranded Man",
      "The Still Stranded Man",
      "Why, Stranded Man?",
    ];

    public static string GetDisplayName(string questId)
    {
      if (TryGetNumberedQuestName(questId, "Quest_Enemy", EnemyNames, out string enemyName))
      {
        return $"Avenge the death of {enemyName}!";
      }

      if (TryGetNumberedQuestName(questId, "Quest_Memento", MementoNames, out string mementoName))
      {
        return $"In Memory of {mementoName}";
      }

      if (TryGetNumberedQuestName(questId, "Quest_Catering", CateringNames, out string cateringName))
      {
        return $"Craving {cateringName}";
      }

      if (TryGetNumberedQuestName(questId, "Quest_StrayMan", StrayManTitles, out string strayManTitle))
      {
        return strayManTitle;
      }

      return questId;
    }

    private static bool TryGetNumberedQuestName(
      string questId,
      string prefix,
      IReadOnlyList<string> values,
      out string value)
    {
      value = string.Empty;
      if (!questId.StartsWith(prefix, StringComparison.Ordinal)
        || !int.TryParse(questId[prefix.Length..], out int questNumber)
        || questNumber < 1
        || questNumber > values.Count)
      {
        return false;
      }

      value = values[questNumber - 1];
      return true;
    }
  }
}
