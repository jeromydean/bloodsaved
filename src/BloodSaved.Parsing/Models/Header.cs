namespace BloodSaved.Parsing.Models
{
  public class Header
  {
    public uint SaveVersion { get; set; }
    public uint PackageFileVersion { get; set; }
    public uint Version { get; set; }
    public ushort EngineMajor { get; set; }
    public ushort EngineMinor { get; set; }
    public ushort EnginePatch { get; set; }
    public uint EngineChangeList { get; set; }
    public string Branch { get; set; }
    public uint CustomVersionFormat { get; set; }
    public List<CustomVersion> CustomVersions { get; set; }
    public Header()
    {
      CustomVersions = new List<CustomVersion>();
    }
  }
}
