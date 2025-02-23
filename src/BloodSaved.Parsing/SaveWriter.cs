using System.Reflection;
using System.Text;
using BloodSaved.Parsing.Attributes;
using BloodSaved.Parsing.Enums;
using BloodSaved.Parsing.Extensions;

namespace BloodSaved.Parsing
{
  internal class SaveWriter : BinaryWriter
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

    public SaveWriter() : base(new MemoryStream()) { }

    public byte[] ToArray()
    {
      return ((MemoryStream)BaseStream).ToArray();
    }

    public void SetCheckpoint()
    {
      Checkpoint = BaseStream.Position;
    }

    public void Reset()
    {
      BaseStream.Position = Checkpoint;
    }

    public void SetPosition(long offset)
    {
      BaseStream.Position = offset;
    }

    public void WriteItemId(ItemIds itemId)
    {
      WriteLengthPrefixedString(itemId.GetIdString(), itemId.GetEncoding());
    }

    public void WriteLengthPrefixedString(string value)
    {
      WriteLengthPrefixedString(value, Encoding.UTF8);
    }

    public void WriteLengthPrefixedString(string value, Encoding encoding)
    {
      value = string.IsNullOrEmpty(value) ? value : $"{value}\0";
      int stringLength = encoding.GetByteCount(value);
      stringLength = encoding == Encoding.Unicode ? -1 * (stringLength / 2) : stringLength;
      Write(stringLength);
      Write(encoding.GetBytes(value));
    }

    public void WriteArrayProperty(string propertyName,
      string arrayType,
      out long lengthOffset,
      out long countOffset,
      int length = 0,
      int count = 0)
    {
      WriteLengthPrefixedString(propertyName);
      WriteLengthPrefixedString(SaveConstants.ArrayProperty);
      lengthOffset = BaseStream.Position;
      Write(length);
      Write(Enumerable.Repeat((byte)0x00, 4).ToArray());
      WriteLengthPrefixedString(arrayType);
      Write((byte)0x00);
      countOffset = BaseStream.Position;
      Write(count);
    }

    public void WriteMapProperty(string propertyName,
      string keyType,
      string valueType,
      out long lengthOffset,
      out long countOffset,
      int length = 0,
      int count = 0)
    {
      WriteLengthPrefixedString(propertyName);
      WriteLengthPrefixedString(SaveConstants.MapProperty);
      lengthOffset = BaseStream.Position;
      Write(length);
      Write(0);
      WriteLengthPrefixedString(keyType);
      WriteLengthPrefixedString(valueType);
      Write((byte)0x00);
      Write(0);
      countOffset = BaseStream.Position;
      Write(count);
    }

    public void WriteStructProperty(string propertyName,
      string structName,
      Guid structId,
      out long lengthOffset,
      int length = 0)
    {
      WriteLengthPrefixedString(propertyName);
      WriteLengthPrefixedString(SaveConstants.StructProperty);
      lengthOffset = BaseStream.Position;
      Write(length);
      Write(0);
      WriteLengthPrefixedString(structName);
      Write(structId.ToByteArray());
      Write((byte)0x00);
    }

    public void WriteIntProperty(string propertyName,
      int value)
    {
      WriteLengthPrefixedString(propertyName);
      WriteLengthPrefixedString(SaveConstants.IntProperty);
      Write(s_intPropertyBytes.ToArray());
      Write(value);
    }

    public void WriteBoolProperty(string propertyName,
      bool value)
    {
      WriteLengthPrefixedString(propertyName);
      WriteLengthPrefixedString(SaveConstants.BoolProperty);
      Write(Enumerable.Repeat((byte)0x00, 8)
        .Concat(new[] { (value ? (byte)0x01 : (byte)0x00) })
        .Concat([(byte)0x00]).ToArray());
    }

    public void WriteFloatProperty(string propertyName,
      float value)
    {
      WriteLengthPrefixedString(propertyName);
      WriteLengthPrefixedString(SaveConstants.FloatProperty);
      Write(s_floatPropertyBytes.ToArray());
      Write(value);
    }

    public void WriteNameProperty(string propertyName,
      ItemIds itemId)
    {
      string itemIdString = itemId.GetIdString();
      Encoding encoding = itemId.GetEncoding();

      int itemIdStringLength = encoding == Encoding.Unicode
        ? encoding.GetByteCount(itemIdString) * -1
        : encoding.GetByteCount(itemIdString);

      WriteLengthPrefixedString(propertyName);
      WriteLengthPrefixedString(SaveConstants.NameProperty);
      Write(4 + itemIdStringLength + 1);
      Write(Enumerable.Repeat((byte)0x00, 5).ToArray());
      WriteLengthPrefixedString(itemIdString, encoding);
    }
  }
}