﻿using BloodSaved.Parsing.Enums;

namespace BloodSaved.Parsing.Models
{
  public class InventoryItem : IItem
  {
    public int Index { get; set; } = int.MaxValue;
    public ItemId ItemId { get; set; }
    public int Quantity { get; set; }//aka shard grade
    public int Rank { get; set; }
    public float GradeValue { get; set; }
    public float RankValue { get; set; }
    public int Unknown { get; set; }
  }
}