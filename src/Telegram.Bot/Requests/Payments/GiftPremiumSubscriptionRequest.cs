// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Gifts a Telegram Premium subscription to the given user.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class GiftPremiumSubscriptionRequest() : RequestBase<bool>("giftPremiumSubscription"), IUserTargetable
{
    /// <summary>Unique identifier of the target user who will receive a Telegram Premium subscription</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Number of months the Telegram Premium subscription will be active for the user; must be one of 3, 6, or 12</summary>
    [JsonPropertyName("month_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int MonthCount { get; set; }

    /// <summary>Number of Telegram Stars to pay for the Telegram Premium subscription; must be 1000 for 3 months, 1500 for 6 months, and 2500 for 12 months</summary>
    [JsonPropertyName("star_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long StarCount { get; set; }

    /// <summary>Text that will be shown along with the service message about the subscription; 0-128 characters</summary>
    public string? Text { get; set; }

    /// <summary>Mode for parsing entities in the text. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details. Entities other than <see cref="MessageEntityType.Bold">Bold</see>, <see cref="MessageEntityType.Italic">Italic</see>, <see cref="MessageEntityType.Underline">Underline</see>, <see cref="MessageEntityType.Strikethrough">Strikethrough</see>, <see cref="MessageEntityType.Spoiler">Spoiler</see>, and <see cref="MessageEntityType.CustomEmoji">CustomEmoji</see> are ignored.</summary>
    [JsonPropertyName("text_parse_mode")]
    public ParseMode TextParseMode { get; set; }

    /// <summary>A list of special entities that appear in the gift text. It can be specified instead of <see cref="TextParseMode">TextParseMode</see>. Entities other than “bold”, “italic”, “underline”, “strikethrough”, “spoiler”, and “CustomEmoji” are ignored.</summary>
    [JsonPropertyName("text_entities")]
    public IEnumerable<MessageEntity>? TextEntities { get; set; }
}
