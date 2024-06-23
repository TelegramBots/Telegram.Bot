namespace Telegram.Bot.Requests;

/// <summary>Use this method to change the chosen reactions on a message. Service messages can't be reacted to. Automatically forwarded messages from a channel to its discussion group have the same available reactions as messages in the channel.<para>Returns: </para></summary>
public partial class SetMessageReactionRequest : RequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Identifier of the target message. If the message belongs to a media group, the reaction is set to the first non-deleted message in the group instead.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>A list of reaction types to set on the message. Currently, as non-premium users, bots can set up to one reaction per message. A custom emoji reaction can be used if it is either already present on the message or explicitly allowed by chat administrators.</summary>
    public IEnumerable<ReactionType>? Reaction { get; set; }

    /// <summary>Pass <see langword="true"/> to set the reaction with a big animation</summary>
    public bool IsBig { get; set; }

    /// <summary>Initializes an instance of <see cref="SetMessageReactionRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Identifier of the target message. If the message belongs to a media group, the reaction is set to the first non-deleted message in the group instead.</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SetMessageReactionRequest(ChatId chatId, int messageId) : this()
    {
        ChatId = chatId;
        MessageId = messageId;
    }

    /// <summary>Instantiates a new <see cref="SetMessageReactionRequest"/></summary>
    public SetMessageReactionRequest() : base("setMessageReaction") { }
}
