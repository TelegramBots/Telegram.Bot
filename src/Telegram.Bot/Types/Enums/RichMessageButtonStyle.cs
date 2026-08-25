// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary><see cref="RichMessageButton"/>: <em>Optional</em>. Style of the button. Must be one of <see cref="Danger">Danger</see> (red), <see cref="Success">Success</see> (green), <see cref="Primary">Primary</see> (blue) or <see cref="Link">Link</see> (the button is shown as a regular link without borders). If omitted, then an app-specific style is used. The style <see cref="Link">Link</see> is allowed only for callback buttons.</summary>
[JsonConverter(typeof(EnumConverter<RichMessageButtonStyle>))]
public enum RichMessageButtonStyle
{
    /// <summary>“danger” style</summary>
    Danger = 1,
    /// <summary>“success” style</summary>
    Success,
    /// <summary>“primary” style</summary>
    Primary,
    /// <summary>“link” style</summary>
    Link = 5,
}
