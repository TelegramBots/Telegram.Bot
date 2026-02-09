// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary><see cref="KeyboardButton"/>: <em>Optional</em>. Style of the button. Must be one of <see cref="Danger">Danger</see> (red), <see cref="Success">Success</see> (green) or <see cref="Primary">Primary</see> (blue). If omitted, then an app-specific style is used.</summary>
[JsonConverter(typeof(EnumConverter<KeyboardButtonStyle>))]
public enum KeyboardButtonStyle
{
    /// <summary>“danger” style</summary>
    Danger = 1,
    /// <summary>“success” style</summary>
    Success,
    /// <summary>“primary” style</summary>
    Primary,
}
