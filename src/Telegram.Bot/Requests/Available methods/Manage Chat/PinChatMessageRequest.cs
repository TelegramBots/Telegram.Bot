using Telegram.Bot.Requests.Abstractions;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to add a message to the list of pinned messages in a chat. If the chat is not a
/// private chat, the bot must be an administrator in the chat for this to work and must have the
/// '<see cref="ChatPermissions.CanPinMessages"/>' admin right in a supergroup or
/// '<see cref="ChatMemberAdministrator.CanEditMessages"/>' admin right in a channel.
/// Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class PinChatMessageRequest : RequestBase<bool>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// Identifier of a message to pin
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int MessageId { get; }

    /// <inheritdoc cref="Abstractions.Documentation.DisableNotification"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? DisableNotification { get; set; }

    /// <summary>
    /// Initializes a new request with chatId and messageId
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">Identifier of a message to pin</param>
    public PinChatMessageRequest(ChatId chatId, int messageId)
        : base("pinChatMessage")
    {
        ChatId = chatId;
        MessageId = messageId;
    }
}
