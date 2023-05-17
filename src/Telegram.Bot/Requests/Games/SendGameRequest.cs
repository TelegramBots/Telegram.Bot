using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to send a game. On success, the sent <see cref="Message"/> is returned.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SendGameRequest : RequestBase<Message>, IChatTargetable
{
    /// <summary>
    /// Unique identifier for the target chat
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public long ChatId { get; }

    /// <inheritdoc />
    ChatId IChatTargetable.ChatId => ChatId;

    /// <summary>
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? MessageThreadId { get; set; }

    /// <summary>
    /// Short name of the game, serves as the unique identifier for the game. Set up your games
    /// via <a href="https://t.me/botfather">@Botfather</a>
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string GameShortName { get; }

    /// <inheritdoc cref="Abstractions.Documentation.DisableNotification"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? DisableNotification { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ProtectContent"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? ProtectContent { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ReplyToMessageId"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? ReplyToMessageId { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.AllowSendingWithoutReply"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? AllowSendingWithoutReply { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.InlineReplyMarkup"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>
    /// Initializes a new request with chatId and gameShortName
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat</param>
    /// <param name="gameShortName">
    /// Short name of the game, serves as the unique identifier for the game. Set up your games via
    /// <a href="https://t.me/botfather">@Botfather</a>
    /// </param>
    public SendGameRequest(long chatId, string gameShortName)
        : base("sendGame")
    {
        ChatId = chatId;
        GameShortName = gameShortName;
    }
}
