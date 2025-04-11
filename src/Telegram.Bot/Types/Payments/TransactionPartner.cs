// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Payments;

/// <summary>This object describes the source of a transaction, or its recipient for outgoing transactions. Currently, it can be one of<br/><see cref="TransactionPartnerUser"/>, <see cref="TransactionPartnerChat"/>, <see cref="TransactionPartnerAffiliateProgram"/>, <see cref="TransactionPartnerFragment"/>, <see cref="TransactionPartnerTelegramAds"/>, <see cref="TransactionPartnerTelegramApi"/>, <see cref="TransactionPartnerOther"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<TransactionPartner>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(TransactionPartnerUser), "user")]
[CustomJsonDerivedType(typeof(TransactionPartnerChat), "chat")]
[CustomJsonDerivedType(typeof(TransactionPartnerAffiliateProgram), "affiliate_program")]
[CustomJsonDerivedType(typeof(TransactionPartnerFragment), "fragment")]
[CustomJsonDerivedType(typeof(TransactionPartnerTelegramAds), "telegram_ads")]
[CustomJsonDerivedType(typeof(TransactionPartnerTelegramApi), "telegram_api")]
[CustomJsonDerivedType(typeof(TransactionPartnerOther), "other")]
public abstract partial class TransactionPartner
{
    /// <summary>Type of the transaction partner</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract TransactionPartnerType Type { get; }
}

/// <summary>Describes a transaction with a user.</summary>
public partial class TransactionPartnerUser : TransactionPartner
{
    /// <summary>Type of the transaction partner, always <see cref="TransactionPartnerType.User"/></summary>
    public override TransactionPartnerType Type => TransactionPartnerType.User;

    /// <summary>Type of the transaction, currently one of <see cref="TransactionPartnerUserTransactionType.InvoicePayment">InvoicePayment</see> for payments via invoices, <see cref="TransactionPartnerUserTransactionType.PaidMediaPayment">PaidMediaPayment</see> for payments for paid media, <see cref="TransactionPartnerUserTransactionType.GiftPurchase">GiftPurchase</see> for gifts sent by the bot, <see cref="TransactionPartnerUserTransactionType.PremiumPurchase">PremiumPurchase</see> for Telegram Premium subscriptions gifted by the bot, <see cref="TransactionPartnerUserTransactionType.BusinessAccountTransfer">BusinessAccountTransfer</see> for direct transfers from managed business accounts</summary>
    [JsonPropertyName("transaction_type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public TransactionPartnerUserTransactionType TransactionType { get; set; }

    /// <summary>Information about the user</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User User { get; set; } = default!;

    /// <summary><em>Optional</em>. Information about the affiliate that received a commission via this transaction. Can be available only for “InvoicePayment” and “PaidMediaPayment” transactions.</summary>
    public AffiliateInfo? Affiliate { get; set; }

    /// <summary><em>Optional</em>. Bot-specified invoice payload. Can be available only for “InvoicePayment” transactions.</summary>
    [JsonPropertyName("invoice_payload")]
    public string? InvoicePayload { get; set; }

    /// <summary><em>Optional</em>. The duration of the paid subscription. Can be available only for “InvoicePayment” transactions.</summary>
    [JsonPropertyName("subscription_period")]
    public int? SubscriptionPeriod { get; set; }

    /// <summary><em>Optional</em>. Information about the paid media bought by the user; for “PaidMediaPayment” transactions only</summary>
    [JsonPropertyName("paid_media")]
    public PaidMedia[]? PaidMedia { get; set; }

    /// <summary><em>Optional</em>. Bot-specified paid media payload. Can be available only for “PaidMediaPayment” transactions.</summary>
    [JsonPropertyName("paid_media_payload")]
    public string? PaidMediaPayload { get; set; }

    /// <summary><em>Optional</em>. The gift sent to the user by the bot; for “GiftPurchase” transactions only</summary>
    public Gift? Gift { get; set; }

    /// <summary><em>Optional</em>. Number of months the gifted Telegram Premium subscription will be active for; for “PremiumPurchase” transactions only</summary>
    [JsonPropertyName("premium_subscription_duration")]
    public int? PremiumSubscriptionDuration { get; set; }
}

/// <summary>Describes a transaction with a chat.</summary>
public partial class TransactionPartnerChat : TransactionPartner
{
    /// <summary>Type of the transaction partner, always <see cref="TransactionPartnerType.Chat"/></summary>
    public override TransactionPartnerType Type => TransactionPartnerType.Chat;

    /// <summary>Information about the chat</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat Chat { get; set; } = default!;

    /// <summary><em>Optional</em>. The gift sent to the chat by the bot</summary>
    public Gift? Gift { get; set; }
}

/// <summary>Describes the affiliate program that issued the affiliate commission received via this transaction.</summary>
public partial class TransactionPartnerAffiliateProgram : TransactionPartner
{
    /// <summary>Type of the transaction partner, always <see cref="TransactionPartnerType.AffiliateProgram"/></summary>
    public override TransactionPartnerType Type => TransactionPartnerType.AffiliateProgram;

    /// <summary><em>Optional</em>. Information about the bot that sponsored the affiliate program</summary>
    [JsonPropertyName("sponsor_user")]
    public User? SponsorUser { get; set; }

    /// <summary>The number of Telegram Stars received by the bot for each 1000 Telegram Stars received by the affiliate program sponsor from referred users</summary>
    [JsonPropertyName("commission_per_mille")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int CommissionPerMille { get; set; }
}

/// <summary>Describes a withdrawal transaction with Fragment.</summary>
public partial class TransactionPartnerFragment : TransactionPartner
{
    /// <summary>Type of the transaction partner, always <see cref="TransactionPartnerType.Fragment"/></summary>
    public override TransactionPartnerType Type => TransactionPartnerType.Fragment;

    /// <summary><em>Optional</em>. State of the transaction if the transaction is outgoing</summary>
    [JsonPropertyName("withdrawal_state")]
    public RevenueWithdrawalState? WithdrawalState { get; set; }
}

/// <summary>Describes a withdrawal transaction to the Telegram Ads platform.</summary>
public partial class TransactionPartnerTelegramAds : TransactionPartner
{
    /// <summary>Type of the transaction partner, always <see cref="TransactionPartnerType.TelegramAds"/></summary>
    public override TransactionPartnerType Type => TransactionPartnerType.TelegramAds;
}

/// <summary>Describes a transaction with payment for <a href="https://core.telegram.org/bots/api#paid-broadcasts">paid broadcasting</a>.</summary>
public partial class TransactionPartnerTelegramApi : TransactionPartner
{
    /// <summary>Type of the transaction partner, always <see cref="TransactionPartnerType.TelegramApi"/></summary>
    public override TransactionPartnerType Type => TransactionPartnerType.TelegramApi;

    /// <summary>The number of successful requests that exceeded regular limits and were therefore billed</summary>
    [JsonPropertyName("request_count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int RequestCount { get; set; }
}

/// <summary>Describes a transaction with an unknown source or recipient.</summary>
public partial class TransactionPartnerOther : TransactionPartner
{
    /// <summary>Type of the transaction partner, always <see cref="TransactionPartnerType.Other"/></summary>
    public override TransactionPartnerType Type => TransactionPartnerType.Other;
}
