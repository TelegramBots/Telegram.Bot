namespace Telegram.Bot.Requests;

/// <summary>Use this method to stop a poll which was sent by the bot.<para>Returns: The stopped <see cref="Poll"/> is returned.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class StopPollRequest() : RequestBase<Poll>("stopPoll"), IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Identifier of the original message with the poll</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MessageId { get; set; }

    /// <summary>An object for a new message <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</summary>
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the message to be edited was sent</summary>
    public string? BusinessConnectionId { get; set; }
}
