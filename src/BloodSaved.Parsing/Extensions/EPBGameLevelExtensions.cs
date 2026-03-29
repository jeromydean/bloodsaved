using System.ComponentModel;
using System.Reflection;
using BloodSaved.Parsing.Enums;

namespace BloodSaved.Parsing.Extensions
{
  public static class EPBGameLevelExtensions
  {
    public static string GetDescription(this EPBGameLevel level)
    {
      FieldInfo? fieldInfo = typeof(EPBGameLevel).GetField(Enum.GetName(level));
      DescriptionAttribute? descriptionAttribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();

      return descriptionAttribute?.Description ?? level.ToString();
    }
    public static string GetDescription(this EPBGameModePlayer player)
    {
      FieldInfo? fieldInfo = typeof(EPBGameModePlayer).GetField(Enum.GetName(player));
      DescriptionAttribute? descriptionAttribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();

      return descriptionAttribute?.Description ?? player.ToString();
    }
  }
}
