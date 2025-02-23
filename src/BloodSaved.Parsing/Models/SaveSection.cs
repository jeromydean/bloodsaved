namespace BloodSaved.Parsing.Models
{
  internal class SaveSection
  {
    public string Name { get; set; }
    public long StartOffset { get; set; }
    public long EndOffset { get; set; }
    public byte[] Data { get; set; }
  }
}