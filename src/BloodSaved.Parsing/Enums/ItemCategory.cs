using System.ComponentModel;

namespace BloodSaved.Parsing.Enums
{
  public enum ItemCategory
  {
    [Description("Weapons")]
    Weapon,
    [Description("Bullets")]
    Bullet,
    [Description("Body Armor")]
    BodyArmor,
    [Description("Head Armor")]
    HeadArmor,
    [Description("Accessories")]
    Accessory,
    Scarves,
    [Description("Potions")]
    Potion,
    Food,
    Materials,
    Ingredients,
    Seed,
    [Description("Keys/Recipes")]
    Key,
    [Description("Books")]
    Book,
    [Description("Conjure")]
    ConjureShards,
    [Description("Manipulative")]
    ManipulativeShards,
    [Description("Directional")]
    DirectionalShards,
    [Description("Passive")]
    PassiveShards,
    [Description("Familiar")]
    FamiliarShards,
    [Description("Skill")]
    SkillShards
  }
}