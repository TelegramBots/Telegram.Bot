using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to send answers to callback queries sent from
/// <see cref="Types.ReplyMarkups.InlineKeyboardMarkup">inline keyboards</see>. The answer will be
/// displayed to the user as a notification at the top of the chat screen or as an alert. On success,
/// <see langword="true"/> is returned.
/// </summary>
/// <remarks>
/// Alternatively, the user can be redirected to the specified Game URL.For this option to work, you
/// must first create a game for your bot via <c>@BotFather</c> and accept the terms. Otherwise, you
/// may use links like <c>t.me/your_bot? start = XXXX</c> that open your bot with a parameter.
/// </remarks>

public class AnswerCallbackQueryRequest : RequestBase<bool>
{
    /// <summary>
    /// Unique identifier for the query to be answered
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string CallbackQueryId { get; init; }

    /// <summary>
    /// Text of the notification. If not specified, nothing will be shown to the user, 0-200 characters
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; set; }

    /// <summary>
    /// If true, an alert will be shown by the client instead of a notification at the top of
    /// the chat screen. Defaults to <see langword="false"/>
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ShowAlert { get; set; }

    /// <summary>
    /// URL that will be opened by the user's client. If you have created a
    /// <a href="https://core.telegram.org/bots/api#game">Game</a> and accepted the conditions
    /// via <c>@BotFather</c>, specify the URL that opens your game — note that this will only work
    /// if the query comes from a callback_game button.
    /// <para>
    /// Otherwise, you may use links like <c>t.me/your_bot? start = XXXX</c> that open your bot with
    /// a parameter
    /// </para>
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Url { get; set; }

    /// <summary>
    /// The maximum amount of time in seconds that the result of the callback query may be cached
    /// client-side. Telegram apps will support caching starting in version 3.14. Defaults to 0
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? CacheTime { get; set; }

    /// <summary>
    /// Initializes a new request with callbackQueryId
    /// </summary>
    public AnswerCallbackQueryRequest() : base("answerCallbackQuery") { }

    /// <summary>
    /// Initializes a new request with callbackQueryId
    /// </summary>
    /// <param name="callbackQueryId">Unique identifier for the query to be answered</param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public AnswerCallbackQueryRequest(string callbackQueryId)
        : this()
    {
        CallbackQueryId = callbackQueryId;
    }
}
