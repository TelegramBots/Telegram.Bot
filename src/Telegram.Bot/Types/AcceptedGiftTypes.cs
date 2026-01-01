// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes the types of gifts that can be gifted to a user or a chat.</summary>
public partial class AcceptedGiftTypes
{
    /// <summary><see langword="true"/>, if unlimited regular gifts are accepted</summary>
    [JsonPropertyName("unlimited_gifts")]
    public bool UnlimitedGifts { get; set; }

    /// <summary><see langword="true"/>, if limited regular gifts are accepted</summary>
    [JsonPropertyName("limited_gifts")]
    public bool LimitedGifts { get; set; }

    /// <summary><see langword="true"/>, if unique gifts or gifts that can be upgraded to unique for free are accepted</summary>
    [JsonPropertyName("unique_gifts")]
    public bool UniqueGifts { get; set; }

    /// <summary><see langword="true"/>, if a Telegram Premium subscription is accepted</summary>
    [JsonPropertyName("premium_subscription")]
    public bool PremiumSubscription { get; set; }

    /// <summary><see langword="true"/>, if transfers of unique gifts from channels are accepted</summary>
    [JsonPropertyName("gifts_from_channels")]
    public bool GiftsFromChannels { get; set; }
}
