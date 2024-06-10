using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// This object contains information about one answer option in a poll to send.
/// </summary>
public class InputPollOption
{
    /// <summary>
    /// Option text, 1-100 characters
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Text { get; set; }

    /// <summary>
    /// Optional. Mode for parsing entities in the text. See formatting options for more details.
    /// Currently, only custom emoji entities are allowed
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ParseMode? TextParseMode { get; set; }

    /// <summary>
    /// Optional. A list of special entities that appear in the poll option text. It can be specified instead of
    /// <see cref="TextParseMode"/>
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public MessageEntity[]? TextEntities { get; set; }

    /// <summary>
    /// Initializes an input poll option
    /// </summary>
    /// <param name="text"></param>
    [SetsRequiredMembers]
    public InputPollOption(string text) => Text = text;

    /// <summary>
    /// Initializes an input poll option
    /// </summary>
    public InputPollOption()
    { }
}
