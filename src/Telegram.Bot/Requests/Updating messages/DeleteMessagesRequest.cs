namespace Telegram.Bot.Requests;

/// <summary>Use this method to delete multiple messages simultaneously. If some of the specified messages can't be found, they are skipped.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class DeleteMessagesRequest() : RequestBase<bool>("deleteMessages"), IChatTargetable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>A list of 1-100 identifiers of messages to delete. See <see cref="TelegramBotClientExtensions.DeleteMessage">DeleteMessage</see> for limitations on which messages can be deleted</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<int> MessageIds { get; set; }
}
