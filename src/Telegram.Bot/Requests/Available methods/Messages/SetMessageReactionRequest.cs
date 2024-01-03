// ReSharper disable once CheckNamespace
using Telegram.Bot.Requests.Abstractions;

namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to change the chosen reactions on a message. Service messages can't be reacted to.
/// Automatically forwarded messages from a channel to its discussion group have the same
/// available reactions as messages in the channel.
/// Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SetMessageReactionRequest : RequestBase<bool>,
    IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// Identifier of the target message. If the message belongs to a media group, the reaction
    /// is set to the first non-deleted message in the group instead.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int MessageId { get; }

    /// <summary>
    /// New list of reaction types to set on the message. Currently, as non-premium users, bots can
    /// set up to one reaction per message. A custom emoji reaction can be used if it is either
    /// already present on the message or explicitly allowed by chat administrators.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ReactionType[]? Reaction { get; set; }

    /// <summary>
    /// Pass <see langword="true"/> to set the reaction with a big animation
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? IsBig { get; set; }

    /// <summary>
    /// Initializes a new request with chatId and messageId
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">
    /// Identifier of the target message. If the message belongs to a media group, the reaction
    /// is set to the first non-deleted message in the group instead.
    /// </param>
    public SetMessageReactionRequest(ChatId chatId, int messageId)
    : base("setMessageReaction")
    {
        ChatId = chatId;
        MessageId = messageId;
    }
}
