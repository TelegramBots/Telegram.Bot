namespace Telegram.Bot.Types.Payments;

/// <summary>Describes a Telegram Star transaction.</summary>
public partial class StarTransaction
{
    /// <summary>Unique identifier of the transaction. Coincides with the identifer of the original transaction for refund transactions. Coincides with <em>SuccessfulPayment.TelegramPaymentChargeId</em> for successful incoming payments from users.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Id { get; set; } = default!;

    /// <summary>Number of Telegram Stars transferred by the transaction</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Amount { get; set; }

    /// <summary>Date the transaction was created</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime Date { get; set; }

    /// <summary><em>Optional</em>. Source of an incoming transaction (e.g., a user purchasing goods or services, Fragment refunding a failed withdrawal). Only for incoming transactions</summary>
    public TransactionPartner? Source { get; set; }

    /// <summary><em>Optional</em>. Receiver of an outgoing transaction (e.g., a user for a purchase refund, Fragment for a withdrawal). Only for outgoing transactions</summary>
    public TransactionPartner? Receiver { get; set; }
}
