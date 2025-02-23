namespace BloodSaved.Parsing.Extensions
{
  public static class ByteArrayExtensions
  {
    public static string ToHexString(this byte[] bytes)
    {
      return string.Join(", ", bytes.Select(b => $"0x{b.ToString("X2")}"));
    }
  }
}