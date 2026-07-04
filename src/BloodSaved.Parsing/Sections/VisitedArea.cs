using BloodSaved.Parsing.Models;

namespace BloodSaved.Parsing.Sections
{
  public class VisitedArea : ISerializableSection<VisitedArea>
  {
    public const string SectionName = "VisitedArea";

    public static readonly IReadOnlyList<string> CompleteMapAreas =
    [
      "ACT01_SIP",
      "ACT02_VIL",
      "ACT03_ENT",
      "ACT04_GDN",
      "ACT05_SAN",
      "ACT09_TRN",
      "ACT08_TWR",
      "ACT07_LIB",
      "ACT13_ARC",
      "ACT19_K2C",
      "ACT06_KNG",
      "ACT88_BKR003",
      "ACT11_UGD",
      "ACT12_SND",
      "ACT14_TAR",
      "ACT17_RVA",
      "ACT51_EBT",
      "ACT15_JPN",
      "ACT88_BKR004",
      "ACT20_JRN",
      "ACT10_BIG",
      "ACT18_ICE",
      "ACT88_BKR001",
      "ACT88_BKR002",
    ];

    public List<string> Areas { get; } = [];
    public bool IsDirty { get; private set; }

    public static VisitedArea Deserialize(SaveSection saveSection)
    {
      VisitedArea visitedArea = new();
      using SaveReader saveReader = new(saveSection.Data);
      saveReader.ReadArrayProperty(SectionName, out string arrayType, out int arrayLength, out int count);
      if (!string.Equals(arrayType, SaveConstants.NameProperty, StringComparison.Ordinal))
      {
        throw new InvalidDataException($"{SectionName} array type of '{arrayType}' is not correct, expected NameProperty.");
      }

      long arrayEnd = saveReader.CurrentPosition - 4 + arrayLength;
      for (int i = 0; i < count; i++)
      {
        visitedArea.Areas.Add(saveReader.ReadLengthPrefixedString());
      }

      saveReader.BaseStream.Position = arrayEnd;
      if (!saveReader.EndOfStream)
      {
        throw new InvalidDataException($"Expected end of '{nameof(VisitedArea)}' element.");
      }

      return visitedArea;
    }

    public void SetAllAreasVisited()
    {
      foreach (string area in CompleteMapAreas)
      {
        if (!Areas.Contains(area, StringComparer.Ordinal))
        {
          Areas.Add(area);
        }
      }

      IsDirty = true;
    }

    public byte[] Serialize()
    {
      using SaveWriter saveWriter = new();
      saveWriter.WriteArrayProperty(SectionName, SaveConstants.NameProperty, out long lengthOffset, out long countOffset, count: Areas.Count);
      foreach (string area in Areas)
      {
        saveWriter.WriteLengthPrefixedString(area);
      }

      PatchArrayLengthOnly(saveWriter, lengthOffset, countOffset);
      return saveWriter.ToArray();
    }

    private static void PatchArrayLengthOnly(SaveWriter saveWriter, long lengthOffset, long countOffset)
    {
      long resumePosition = saveWriter.CurrentPosition;
      saveWriter.SetPosition(lengthOffset);
      saveWriter.Write((int)(resumePosition - countOffset));
      saveWriter.SetPosition(resumePosition);
    }
  }
}
