namespace Telegram.Bot.Requests;

/// <summary>Use this method to delete multiple messages simultaneously. If some of the specified messages can't be found, they are skipped.<para>Returns: </para></summary>
public partial class DeleteMessagesRequest : RequestBase<bool>, IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>A list of 1-100 identifiers of messages to delete. See <see cref="TelegramBotClientExtensions.DeleteMessageAsync">DeleteMessage</see> for limitations on which messages can be deleted</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<int> MessageIds { get; set; }

    /// <summary>Instantiates a new <see cref="DeleteMessagesRequest"/></summary>
    public DeleteMessagesRequest() : base("deleteMessages") { }
}
