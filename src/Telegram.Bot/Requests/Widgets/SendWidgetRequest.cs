// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to send a message with an <a href="https://core.telegram.org/bots/widgets">HTML widget</a>. Currently, in the production environment, widgets can be sent only to private chats, and sent widgets automatically have protected content and therefore can't be forwarded.<para>Returns: The sent <see cref="Message"/> is returned.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SendWidgetRequest() : RequestBase<Message>("sendWidget"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup in the format <c>@username</c></summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>HTML code of the widget, 1-32768 bytes. The code must not exceed 4096 bytes when compressed by zlib with the maximum compression level.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Html { get; set; }

    /// <summary>Unique identifier for the target message thread (topic) of a forum; for forum supergroups and private chats of bots with forum topic mode enabled only</summary>
    [JsonPropertyName("message_thread_id")]
    public int? MessageThreadId { get; set; }

    /// <summary>Width of the widget in logical pixels; omit if the widget must occupy the whole width of the message bubble. The width is clamped by the app to the range from 200 to the maximum width of the message bubble.</summary>
    public int? Width { get; set; }

    /// <summary>Height of the widget in logical pixels; omit if the height of the widget isn't fixed. The height is clamped by the app to the range 50-600.</summary>
    public int? Height { get; set; }

    /// <summary>Public data of the widget, 0-1024 bytes, which will be available wherever the widget is shown. If the widget is shared, then all messages with it will have the same public data.</summary>
    [JsonPropertyName("public_data")]
    public string? PublicData { get; set; }

    /// <summary>Data of the widget, which is specific to the message, 0-1024 bytes</summary>
    [JsonPropertyName("chat_data")]
    public string? ChatData { get; set; }

    /// <summary>Widget caption, 0-1024 characters after entities parsing</summary>
    public string? Caption { get; set; }

    /// <summary>Mode for parsing entities in the widget caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</summary>
    [JsonPropertyName("parse_mode")]
    public ParseMode ParseMode { get; set; }

    /// <summary>A list of special entities that appear in the caption, which can be specified instead of <see cref="ParseMode">ParseMode</see></summary>
    [JsonPropertyName("caption_entities")]
    public IEnumerable<MessageEntity>? CaptionEntities { get; set; }

    /// <summary>Pass <see langword="true"/> if the caption must be shown above the widget</summary>
    [JsonPropertyName("show_caption_above_media")]
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary>Pass <see langword="true"/> to allow the widget to send queries to the bot</summary>
    [JsonPropertyName("allow_queries")]
    public bool AllowQueries { get; set; }

    /// <summary>Pass <see langword="true"/> to create a shared widget, which can be sent again using the method <see cref="TelegramBotClientExtensions.SendSharedWidget">SendSharedWidget</see></summary>
    [JsonPropertyName("is_shared")]
    public bool IsShared { get; set; }

    /// <summary>Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</summary>
    [JsonPropertyName("disable_notification")]
    public bool DisableNotification { get; set; }

    /// <summary>Protects the contents of the sent message from forwarding and saving</summary>
    [JsonPropertyName("protect_content")]
    public bool ProtectContent { get; set; }

    /// <summary>Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance.</summary>
    [JsonPropertyName("allow_paid_broadcast")]
    public bool AllowPaidBroadcast { get; set; }

    /// <summary>Unique identifier of the message effect to be added to the message; for private chats only</summary>
    [JsonPropertyName("message_effect_id")]
    public string? MessageEffectId { get; set; }

    /// <summary>Description of the message to reply to</summary>
    [JsonPropertyName("reply_parameters")]
    public ReplyParameters? ReplyParameters { get; set; }

    /// <summary>Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user.</summary>
    [JsonPropertyName("reply_markup")]
    public ReplyMarkup? ReplyMarkup { get; set; }
}
