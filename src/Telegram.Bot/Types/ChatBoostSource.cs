using Telegram.Bot.Converters;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types;

/// <summary>
/// This object describes the source of a chat boost. It can be one of
/// <list type="bullet">
/// <item><see cref="ChatBoostSourcePremium"/></item>
/// <item><see cref="ChatBoostSourceGiftCode"/></item>
/// <item><see cref="ChatBoostSourceGiveaway"/></item>
/// </list>
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
[JsonConverter(typeof(ChatBoostSourceConverter))]
public abstract class ChatBoostSource
{
    /// <summary>
    /// Source of the boost
    /// </summary>
    [JsonProperty]
    public abstract ChatBoostSourceType Source { get; }
}

/// <summary>
/// The boost was obtained by subscribing to Telegram Premium or by gifting a Telegram Premium subscription to another user.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ChatBoostSourcePremium : ChatBoostSource
{
    /// <summary>
    /// Source of the boost, always "premium"
    /// </summary>
    public override ChatBoostSourceType Source => ChatBoostSourceType.Premium;

    /// <summary>
    /// User that boosted the chat
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public User User { get; set; } = default!;
}

/// <summary>
/// The boost was obtained by the creation of Telegram Premium gift codes to boost a chat.
/// Each such code boosts the chat 4 times for the duration of the corresponding Telegram Premium subscription.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ChatBoostSourceGiftCode : ChatBoostSource
{
    /// <summary>
    /// Source of the boost, always "gift_code"
    /// </summary>
    public override ChatBoostSourceType Source => ChatBoostSourceType.GiftCode;

    /// <summary>
    /// User for which the gift code was created
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public User User { get; set; } = default!;
}

/// <summary>
/// The boost was obtained by the creation of a Telegram Premium giveaway.
/// This boosts the chat 4 times for the duration of the corresponding Telegram Premium subscription.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ChatBoostSourceGiveaway : ChatBoostSource
{
    /// <summary>
    /// Source of the boost, always "giveaway"
    /// </summary>
    public override ChatBoostSourceType Source => ChatBoostSourceType.Giveaway;

    /// <summary>
    /// Identifier of a message in the chat with the giveaway; the message could have been deleted already.
    /// May be 0 if the message isn't sent yet.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int GiveawayMessageId { get; set; }

    /// <summary>
    /// Optional. User that won the prize in the giveaway if any
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public User? User { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the giveaway was completed, but there was no user to win the prize
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? IsUnclaimed { get; set; }
}
