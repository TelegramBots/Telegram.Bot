// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes an <a href="https://core.telegram.org/bots/widgets">HTML widget</a>.</summary>
public partial class Widget
{
    /// <summary><em>Optional</em>. Unique identifier of the shared widget, which can be used to send the widget again using the method <see cref="TelegramBotClientExtensions.SendSharedWidget">SendSharedWidget</see>; omitted if the widget isn't shared</summary>
    public string? Id { get; set; }

    /// <summary><em>Optional</em>. Width of the widget in logical pixels; omitted if the widget must occupy the whole width of the message bubble</summary>
    public int Width { get; set; }

    /// <summary><em>Optional</em>. Height of the widget in logical pixels; omitted if the height of the widget isn't fixed</summary>
    public int Height { get; set; }

    /// <summary>HTML code of the widget</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Html { get; set; } = default!;

    /// <summary><em>Optional</em>. Public data of the widget, which is available wherever the widget is shown. All messages with a shared widget have the same public data.</summary>
    [JsonPropertyName("public_data")]
    public string? PublicData { get; set; }

    /// <summary><em>Optional</em>. Data of the widget, which is specific to the message</summary>
    [JsonPropertyName("chat_data")]
    public string? ChatData { get; set; }
}
