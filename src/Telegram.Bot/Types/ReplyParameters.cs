namespace Telegram.Bot.Types;

/// <summary>
/// Describes reply parameters for the message that is being sent.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ReplyParameters
{
    /// <summary>
    /// Identifier of the message that will be replied to in the current chat, or in the chat <see cref="ChatId"/> if it is specified
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int MessageId { get; }

    /// <summary>
    /// Optional. If the message to be replied to is from a different chat, unique identifier for the
    /// chat or username of the channel (in the format <c>@channelusername</c>)
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ChatId? ChatId { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true"/> if the message should be sent even if the specified message
    /// to be replied to is not found; can be used only for replies in the same chat and forum topic.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? AllowSendingWithoutReply { get; set; }

    /// <summary>
    /// Optional. Quoted part of the message to be replied to; 0-1024 characters after entities parsing.
    /// The quote must be an exact substring of the message to be replied to, including bold, italic,
    /// underline, strikethrough, spoiler, and custom_emoji entities.
    /// The message will fail to send if the quote isn't found in the original message.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Quote { get; set; }

    /// <summary>
    /// Optional. Mode for parsing entities in the quote. See formatting options for more details.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? QuoteParseMode { get; set; }

    /// <summary>
    /// Optional. A JSON-serialized list of special entities that appear in the quote.
    /// It can be specified instead of <see cref="QuoteParseMode"/>.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public MessageEntity[]? QuoteEntities { get; set; }

    /// <summary>
    /// Optional. Position of the quote in the original message in UTF-16 code units
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? QuotePosition { get; set; }
}
