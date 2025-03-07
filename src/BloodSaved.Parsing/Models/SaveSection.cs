namespace BloodSaved.Parsing.Models
{
  public class SaveSection
  {
    public bool IsLegacyVersion { get; set;  }
    public string Name { get; set; }
    public string Type { get; set; }
    public long StartOffset { get; set; }
    public long EndOffset { get; set; }
    public byte[] Data { get; set; }
  }
}