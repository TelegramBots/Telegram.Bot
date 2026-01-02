// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Payments;

/// <summary>Describes a Telegram Star transaction. Note that if the buyer initiates a chargeback with the payment provider from whom they acquired Stars (e.g., Apple, Google) following this transaction, the refunded Stars will be deducted from the bot's balance. This is outside of Telegram's control.</summary>
public partial class StarTransaction
{
    /// <summary>Unique identifier of the transaction. Coincides with the identifier of the original transaction for refund transactions. Coincides with <em>SuccessfulPayment.TelegramPaymentChargeId</em> for successful incoming payments from users.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Id { get; set; } = default!;

    /// <summary>Integer amount of Telegram Stars transferred by the transaction</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public long Amount { get; set; }

    /// <summary><em>Optional</em>. The number of 1/1000000000 shares of Telegram Stars transferred by the transaction; from 0 to 999999999</summary>
    [JsonPropertyName("nanostar_amount")]
    public long? NanostarAmount { get; set; }

    /// <summary>Date the transaction was created</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime Date { get; set; }

    /// <summary><em>Optional</em>. Source of an incoming transaction (e.g., a user purchasing goods or services, Fragment refunding a failed withdrawal). Only for incoming transactions</summary>
    public TransactionPartner? Source { get; set; }

    /// <summary><em>Optional</em>. Receiver of an outgoing transaction (e.g., a user for a purchase refund, Fragment for a withdrawal). Only for outgoing transactions</summary>
    public TransactionPartner? Receiver { get; set; }
}
