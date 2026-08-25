// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary><see cref="RichMessageButton"/>: <em>Optional</em>. Style of the button. Must be one of <see cref="Danger">Danger</see>, <see cref="Success">Success</see>, <see cref="Primary">Primary</see>, or <see cref="Link">Link</see> (the button is shown as a regular link without borders). Apps may use theme-specific colors for the button background and text based on the style. The style <see cref="Link">Link</see> is allowed only for callback buttons.</summary>
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
