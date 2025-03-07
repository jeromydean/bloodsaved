using System.ComponentModel;

namespace BloodSaved.Parsing.Enums
{
  public enum ItemCategories
  {
    Weapons = 0,
    Supplies = 1,
    [Description("Body Armor")]
    BodyArmor = 2,
    [Description("Head Armor")]
    HeadArmor = 3,
    Accessories = 4,
    Scarves = 5,
    [Description("Potions/Meals")]
    PotionsMeals = 6,
    Materials = 7,
    [Description("Keys/Recipes")]
    KeysRecipes = 8,
    Books = 9,
    [Description("Conjure Shards")]
    ConjureShards = 10,
    [Description("Manipulative Shards")]
    ManipulativeShards = 11,
    [Description("Directional Shards")]
    DirectionalShards = 12,
    [Description("Passive Shards")]
    PassiveShards = 13,
    [Description("Familiar Shards")]
    FamiliarShards = 14,
    [Description("Skill Shards")]
    SkillShards = 15
  }
}
