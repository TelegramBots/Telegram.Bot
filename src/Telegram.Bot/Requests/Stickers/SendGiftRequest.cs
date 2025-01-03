namespace Telegram.Bot.Requests;

/// <summary>Sends a gift to the given user. The gift can't be converted to Telegram Stars by the user.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SendGiftRequest() : RequestBase<bool>("sendGift"), IUserTargetable
{
    /// <summary>Unique identifier of the target user that will receive the gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Identifier of the gift</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string GiftId { get; set; }

    /// <summary>Pass <see langword="true"/> to pay for the gift upgrade from the bot's balance, thereby making the upgrade free for the receiver</summary>
    public bool PayForUpgrade { get; set; }

    /// <summary>Text that will be shown along with the gift; 0-255 characters</summary>
    public string? Text { get; set; }

    /// <summary>Mode for parsing entities in the text. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details. Entities other than <see cref="MessageEntityType.Bold">Bold</see>, <see cref="MessageEntityType.Italic">Italic</see>, <see cref="MessageEntityType.Underline">Underline</see>, <see cref="MessageEntityType.Strikethrough">Strikethrough</see>, <see cref="MessageEntityType.Spoiler">Spoiler</see>, and <see cref="MessageEntityType.CustomEmoji">CustomEmoji</see> are ignored.</summary>
    public ParseMode TextParseMode { get; set; }

    /// <summary>A list of special entities that appear in the gift text. It can be specified instead of <see cref="TextParseMode">TextParseMode</see>. Entities other than <see cref="MessageEntityType.Bold">Bold</see>, <see cref="MessageEntityType.Italic">Italic</see>, <see cref="MessageEntityType.Underline">Underline</see>, <see cref="MessageEntityType.Strikethrough">Strikethrough</see>, <see cref="MessageEntityType.Spoiler">Spoiler</see>, and <see cref="MessageEntityType.CustomEmoji">CustomEmoji</see> are ignored.</summary>
    public IEnumerable<MessageEntity>? TextEntities { get; set; }
}
