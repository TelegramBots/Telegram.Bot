// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to change the chosen reactions on a message. Service messages of some types can't be reacted to. Automatically forwarded messages from a channel to its discussion group have the same available reactions as messages in the channel. Bots can't use paid reactions.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetMessageReactionRequest() : RequestBase<bool>("setMessageReaction"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Identifier of the target message. If the message belongs to a media group, the reaction is set to the first non-deleted message in the group instead.</summary>
    [JsonPropertyName("message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>A list of reaction types to set on the message. Currently, as non-premium users, bots can set up to one reaction per message. A custom emoji reaction can be used if it is either already present on the message or explicitly allowed by chat administrators. Paid reactions can't be used by bots.</summary>
    public IEnumerable<ReactionType>? Reaction { get; set; }

    /// <summary>Pass <see langword="true"/> to set the reaction with a big animation</summary>
    [JsonPropertyName("is_big")]
    public bool IsBig { get; set; }
}
