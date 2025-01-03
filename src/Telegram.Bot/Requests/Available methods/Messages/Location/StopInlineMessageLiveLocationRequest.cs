namespace Telegram.Bot.Requests;

/// <summary>Use this method to stop updating a live location message before <em>LivePeriod</em> expires.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class StopInlineMessageLiveLocationRequest() : RequestBase<bool>("stopMessageLiveLocation"), IBusinessConnectable
{
    /// <summary>Identifier of the inline message</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string InlineMessageId { get; set; }

    /// <summary>An object for a new <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</summary>
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message to be edited was sent</summary>
    public string? BusinessConnectionId { get; set; }
}
