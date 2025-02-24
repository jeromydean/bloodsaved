using BloodSaved.Parsing.Enums;

namespace BloodSaved.Parsing.Models
{
  public interface IItem
  {
    int Index { get; set; }
    ItemIds ItemId { get; set; }
    int Quantity { get; set; }
    int Rank { get; set; }
    float GradeValue { get; set; }
    float RankValue { get; set; }
  }
}