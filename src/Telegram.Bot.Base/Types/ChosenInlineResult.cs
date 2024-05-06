namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a result of an <see cref="InlineQuery"/> that was chosen by the <see cref="User"/>
/// and sent to their chat partner.
/// </summary>
public class ChosenInlineResult
{
    /// <summary>
    /// The unique identifier for the result that was chosen.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string ResultId { get; set; } = default!;

    /// <summary>
    /// The user that chose the result.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User From { get; set; } = default!;

    /// <summary>
    /// Optional. Sender location, only for bots that require user location
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Location? Location { get; set; }

    /// <summary>
    /// Optional. Identifier of the sent inline message. Available only if there is an inline keyboard attached
    /// to the message. Will be also received in callback queries and can be used to edit the message.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InlineMessageId { get; set; }

    /// <summary>
    /// The query that was used to obtain the result.
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Query { get; set; } = default!;
}
