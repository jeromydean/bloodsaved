using BloodSaved.Parsing.Models;

namespace BloodSaved.Parsing.Sections
{
  public class CompletedTutorials : ISerializableSection<CompletedTutorials>
  {
    public List<string> Tutorials { get; set; }
    public CompletedTutorials()
    {
      Tutorials = new List<string>();
    }

    public static CompletedTutorials Deserialize(SaveSection saveSection)
    {
      CompletedTutorials completedTutorials = new CompletedTutorials();
      using (SaveReader saveReader = new SaveReader(saveSection.Data))
      {
        saveReader.ReadArrayProperty(SaveConstants.CompletedTutorials, out _, out int completedTutorialsLength, out int completedTutorialsCount);
        long completedTutorialsCountOffset = saveReader.CurrentPosition - 4;

        for (int t = 0; t < completedTutorialsCount; t++)
        {
          completedTutorials.Tutorials.Add(saveReader.ReadLengthPrefixedString());
        }

        if (!saveReader.EndOfStream)
        {
          throw new InvalidDataException($"Expected end of 'CompletedTutorials' section.");
        }

        int completedTutorialsActualLength = (int)(saveReader.CurrentPosition - completedTutorialsCountOffset);
        if (completedTutorialsActualLength != completedTutorialsLength)
        {
          throw new InvalidDataException($"CompletedTutorials section has invalid length specified, specifed={completedTutorialsLength}, actual={completedTutorialsActualLength}.");
        }

        if (completedTutorials.Tutorials.Count != completedTutorialsCount)
        {
          throw new InvalidDataException($"CompletedTutorials section has invalid count specified, specifed={completedTutorialsCount}, actual={completedTutorials.Tutorials.Count}.");
        }
      }
      return completedTutorials;
    }

    public byte[] Serialize()
    {
      using (SaveWriter saveWriter = new SaveWriter())
      {
        saveWriter.WriteArrayProperty(SaveConstants.CompletedTutorials, SaveConstants.NameProperty, out long completedTutorialsLengthOffset, out _, count: Tutorials.Count);
        long startStructureStartOffset = saveWriter.CurrentPosition - 4;

        foreach (string tutorial in Tutorials)
        {
          saveWriter.WriteLengthPrefixedString(tutorial);
        }

        //set length
        int structurelength = (int)(saveWriter.CurrentPosition - startStructureStartOffset);
        saveWriter.SetCheckpoint();
        saveWriter.SetPosition(completedTutorialsLengthOffset);
        saveWriter.Write(structurelength);
        saveWriter.Reset();

        return saveWriter.ToArray();
      }
    }
  }
}