using System.Text;
using BloodSaved.Parsing.Extensions;
using BloodSaved.Parsing.Models;

namespace BloodSaved.Parsing
{
  internal class SaveReader : BinaryReader
  {
    private static readonly IReadOnlyList<byte> s_intPropertyBytes = [0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00];
    private static readonly IReadOnlyList<byte> s_floatPropertyBytes = [0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00];

    public long Checkpoint
    {
      get;
      private set;
    }

    public long CurrentPosition
    {
      get
      {
        return BaseStream.Position;
      }
    }

    public bool EndOfStream
    {
      get
      {
        return BaseStream.Position == BaseStream.Length;
      }
    }

    public SaveReader(byte[] input) : base(new MemoryStream(input)) { }

    public Header ReadHeader()
    {
      Header header = new Header
      {
        SaveVersion = ReadUInt32(),
        PackageFileVersion = ReadUInt32(),
        Version = ReadUInt32(),
        EngineMajor = ReadUInt16(),
        EngineMinor = ReadUInt16(),
        EnginePatch = ReadUInt16(),
        EngineChangeList = ReadUInt32(),
        Branch = ReadNullTerminatedString(),
        CustomVersionFormat = ReadUInt32(),
      };
      uint customVersionLen = ReadUInt32();
      for (int cv = 0; cv < customVersionLen; cv++)
      {
        header.CustomVersions.Add(new CustomVersion
        {
          Id = ReadGuid(),
          Version = ReadUInt32()
        });
      }
      return header;
    }

    public void SetCheckpoint()
    {
      Checkpoint = BaseStream.Position;
    }

    public void Reset()
    {
      BaseStream.Position = Checkpoint;
    }

    public byte[] CloneFromCheckpoint()
    {
      long currentOffset = BaseStream.Position;
      BaseStream.Position = Checkpoint;
      return ReadBytes((int)(currentOffset - Checkpoint));
    }

    public void Skip(int count)
    {
      BaseStream.Position += count;
    }

    public string ReadNullTerminatedString()
    {
      return ReadNullTerminatedString(Encoding.UTF8);
    }

    public string ReadNullTerminatedString(Encoding encoding)
    {
      List<byte> strBytes = new List<byte>();
      byte b;
      while ((b = ReadByte()) != 0x00)
      {
        strBytes.Add(b);
      }
      return encoding.GetString(strBytes.ToArray());
    }

    public string PeekLengthPrefixedString()
    {
      long currentOffset = CurrentPosition;
      string value = ReadLengthPrefixedString();
      BaseStream.Position = currentOffset;
      return value;
    }

    public string ReadLengthPrefixedString()
    {
      int stringLength = ReadInt32();
      bool isUnicode = stringLength < 0;
      stringLength = isUnicode ? (-1 * stringLength) * 2 : stringLength;
      return isUnicode
        ? Encoding.Unicode.GetString(ReadBytes(stringLength)).TrimEnd('\0')
        : Encoding.UTF8.GetString(ReadBytes(stringLength)).TrimEnd('\0');
    }

    public Guid ReadGuid()
    {
      return new Guid(ReadBytes(16));
    }

    public void ReadStructProperty(string propertyName,
      out string structName,
      out int length,
      out Guid structId)
    {
      VerifyAndReadLengthPrefixedString(propertyName);
      VerifyAndReadLengthPrefixedString(SaveConstants.StructProperty);
      length = ReadInt32();
      VerifyNullPadBytes(4);
      structName = ReadLengthPrefixedString();
      structId = ReadGuid();
      VerifyNullPadBytes(1);
    }

    public int ReadIntProperty(string propertyName)
    {
      string actualPropertyName = ReadLengthPrefixedString();
      if (!string.Equals(propertyName, actualPropertyName))
      {
        throw new FormatException();
      }

      VerifyAndReadLengthPrefixedString(SaveConstants.IntProperty);

      if (!Enumerable.SequenceEqual(s_intPropertyBytes, ReadBytes(9)))
      {
        throw new NotImplementedException();
      }

      return ReadInt32();
    }

    public bool ReadBoolProperty(string propertyName)
    {
      VerifyAndReadLengthPrefixedString(propertyName);
      VerifyAndReadLengthPrefixedString(SaveConstants.BoolProperty);
      return ReadBytes(10).Any(b => b != 0);
    }

    public string ReadNameProperty(string propertyName)
    {
      VerifyAndReadLengthPrefixedString(propertyName);
      VerifyAndReadLengthPrefixedString(SaveConstants.NameProperty);
      int namePropertyLength = ReadInt32();
      VerifyNullPadBytes(5);
      return ReadLengthPrefixedString();
    }

    public void ReadMapProperty(string propertyName,
      out string keyType,
      out string valueType,
      out int length,
      out int count)
    {
      VerifyAndReadLengthPrefixedString(propertyName);
      VerifyAndReadLengthPrefixedString(SaveConstants.MapProperty);
      length = ReadInt32();
      VerifyNullPadBytes(4);
      keyType = ReadLengthPrefixedString();
      valueType = ReadLengthPrefixedString();
      VerifyNullPadBytes(5);
      count = ReadInt32();
    }

    public void ReadArrayProperty(string propertyName,
      out string arrayPropertyType,
      out int length,
      out int count)
    {
      VerifyAndReadLengthPrefixedString(propertyName);
      VerifyAndReadLengthPrefixedString(SaveConstants.ArrayProperty);
      length = ReadInt32();
      VerifyNullPadBytes(4);
      arrayPropertyType = ReadLengthPrefixedString();
      VerifyNullPadBytes(1);
      count = ReadInt32();
    }

    public float ReadFloatProperty(string propertyName)
    {
      VerifyAndReadLengthPrefixedString(propertyName);
      VerifyAndReadLengthPrefixedString(SaveConstants.FloatProperty);
      byte[] floatTypeData = ReadBytes(9);
      if (!Enumerable.SequenceEqual(s_floatPropertyBytes, floatTypeData))
      {
        throw new InvalidDataException($"Unknown float type encountered {{{floatTypeData.ToHexString()}}}");
      }
      return ReadSingle();
    }

    public void VerifyAndReadLengthPrefixedString(string expectedValue)
    {
      int stringLength = ReadInt32();
      bool isUnicode = stringLength < 0;
      stringLength = isUnicode ? (-1 * stringLength) * 2 : stringLength;
      string actualValue = isUnicode
        ? Encoding.Unicode.GetString(ReadBytes(stringLength)).TrimEnd('\0')
        : Encoding.UTF8.GetString(ReadBytes(stringLength)).TrimEnd('\0');

      if (!string.Equals(expectedValue, actualValue))
      {
        throw new InvalidDataException($"Expected '{expectedValue}' but read '{actualValue}'.");
      }
    }

    public void VerifyNullPadBytes(int count)
    {
      byte[] bytes = ReadBytes(count);
      if (!bytes.All(b => b == 0))
      {
        throw new InvalidDataException($"Expected {count} null bytes, read {{{bytes.ToHexString()}}}.");
      }
    }
  }
}