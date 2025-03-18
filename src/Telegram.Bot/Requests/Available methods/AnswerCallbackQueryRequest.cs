// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to send answers to callback queries sent from <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboards</a>. The answer will be displayed to the user as a notification at the top of the chat screen or as an alert</summary>
/// <remarks>Alternatively, the user can be redirected to the specified Game URL. For this option to work, you must first create a game for your bot via <a href="https://t.me/botfather">@BotFather</a> and accept the terms. Otherwise, you may use links like <c>t.me/your_bot?start=XXXX</c> that open your bot with a parameter.</remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class AnswerCallbackQueryRequest() : RequestBase<bool>("answerCallbackQuery")
{
    /// <summary>Unique identifier for the query to be answered</summary>
    [JsonPropertyName("callback_query_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string CallbackQueryId { get; set; }

    /// <summary>Text of the notification. If not specified, nothing will be shown to the user, 0-200 characters</summary>
    public string? Text { get; set; }

    /// <summary>If <see langword="true"/>, an alert will be shown by the client instead of a notification at the top of the chat screen. Defaults to <see langword="false"/>.</summary>
    [JsonPropertyName("show_alert")]
    public bool ShowAlert { get; set; }

    /// <summary>URL that will be opened by the user's client. If you have created a <see cref="Game"/> and accepted the conditions via <a href="https://t.me/botfather">@BotFather</a>, specify the URL that opens your game - note that this will only work if the query comes from a <see cref="InlineKeyboardButton"><em>CallbackGame</em></see> button.<br/><br/>Otherwise, you may use links like <c>t.me/your_bot?start=XXXX</c> that open your bot with a parameter.</summary>
    public string? Url { get; set; }

    /// <summary>The maximum amount of time in seconds that the result of the callback query may be cached client-side. Telegram apps will support caching starting in version 3.14. Defaults to 0.</summary>
    [JsonPropertyName("cache_time")]
    public int? CacheTime { get; set; }
}
