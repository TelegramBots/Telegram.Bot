// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Payments;

/// <summary><see cref="TransactionPartnerUser"/>: Type of the transaction, currently one of <see cref="InvoicePayment">InvoicePayment</see> for payments via invoices, <see cref="PaidMediaPayment">PaidMediaPayment</see> for payments for paid media, <see cref="GiftPurchase">GiftPurchase</see> for gifts sent by the bot, <see cref="PremiumPurchase">PremiumPurchase</see> for Telegram Premium subscriptions gifted by the bot, <see cref="BusinessAccountTransfer">BusinessAccountTransfer</see> for direct transfers from managed business accounts</summary>
[JsonConverter(typeof(EnumConverter<TransactionPartnerUserTransactionType>))]
public enum TransactionPartnerUserTransactionType
{
    /// <summary>“InvoicePayment” transactiontype</summary>
    InvoicePayment = 1,
    /// <summary>“PaidMediaPayment” transactiontype</summary>
    PaidMediaPayment,
    /// <summary>“GiftPurchase” transactiontype</summary>
    GiftPurchase,
    /// <summary>“PremiumPurchase” transactiontype</summary>
    PremiumPurchase,
    /// <summary>“BusinessAccountTransfer” transactiontype</summary>
    BusinessAccountTransfer,
}
