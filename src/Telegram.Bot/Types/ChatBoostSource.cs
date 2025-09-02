// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes the source of a chat boost. It can be one of<br/><see cref="ChatBoostSourcePremium"/>, <see cref="ChatBoostSourceGiftCode"/>, <see cref="ChatBoostSourceGiveaway"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<ChatBoostSource>))]
[CustomJsonPolymorphic("source")]
[CustomJsonDerivedType(typeof(ChatBoostSourcePremium), "premium")]
[CustomJsonDerivedType(typeof(ChatBoostSourceGiftCode), "gift_code")]
[CustomJsonDerivedType(typeof(ChatBoostSourceGiveaway), "giveaway")]
public abstract partial class ChatBoostSource
{
    /// <summary>Source of the boost</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract ChatBoostSourceType Source { get; }
}

/// <summary>The boost was obtained by subscribing to Telegram Premium or by gifting a Telegram Premium subscription to another user.</summary>
public partial class ChatBoostSourcePremium : ChatBoostSource
{
    /// <summary>Source of the boost, always <see cref="ChatBoostSourceType.Premium"/></summary>
    public override ChatBoostSourceType Source => ChatBoostSourceType.Premium;

    /// <summary>User that boosted the chat</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User User { get; set; } = default!;
}

/// <summary>The boost was obtained by the creation of Telegram Premium gift codes to boost a chat. Each such code boosts the chat 4 times for the duration of the corresponding Telegram Premium subscription.</summary>
public partial class ChatBoostSourceGiftCode : ChatBoostSource
{
    /// <summary>Source of the boost, always <see cref="ChatBoostSourceType.GiftCode"/></summary>
    public override ChatBoostSourceType Source => ChatBoostSourceType.GiftCode;

    /// <summary>User for which the gift code was created</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User User { get; set; } = default!;
}

/// <summary>The boost was obtained by the creation of a Telegram Premium or a Telegram Star giveaway. This boosts the chat 4 times for the duration of the corresponding Telegram Premium subscription for Telegram Premium giveaways and <see cref="PrizeStarCount">PrizeStarCount</see> / 500 times for one year for Telegram Star giveaways.</summary>
public partial class ChatBoostSourceGiveaway : ChatBoostSource
{
    /// <summary>Source of the boost, always <see cref="ChatBoostSourceType.Giveaway"/></summary>
    public override ChatBoostSourceType Source => ChatBoostSourceType.Giveaway;

    /// <summary>Identifier of a message in the chat with the giveaway; the message could have been deleted already. May be 0 if the message isn't sent yet.</summary>
    [JsonPropertyName("giveaway_message_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int GiveawayMessageId { get; set; }

    /// <summary><em>Optional</em>. User that won the prize in the giveaway if any; for Telegram Premium giveaways only</summary>
    public User? User { get; set; }

    /// <summary><em>Optional</em>. The number of Telegram Stars to be split between giveaway winners; for Telegram Star giveaways only</summary>
    [JsonPropertyName("prize_star_count")]
    public long? PrizeStarCount { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the giveaway was completed, but there was no user to win the prize</summary>
    [JsonPropertyName("is_unclaimed")]
    public bool IsUnclaimed { get; set; }
}
