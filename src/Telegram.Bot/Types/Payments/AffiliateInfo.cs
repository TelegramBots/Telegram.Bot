namespace Telegram.Bot.Types.Payments;

/// <summary>Contains information about the affiliate that received a commission via this transaction.</summary>
public partial class AffiliateInfo
{
    /// <summary><em>Optional</em>. The bot or the user that received an affiliate commission if it was received by a bot or a user</summary>
    public User? AffiliateUser { get; set; }

    /// <summary><em>Optional</em>. The chat that received an affiliate commission if it was received by a chat</summary>
    public Chat? AffiliateChat { get; set; }

    /// <summary>The number of Telegram Stars received by the affiliate for each 1000 Telegram Stars received by the bot from referred users</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int CommissionPerMille { get; set; }

    /// <summary>Integer amount of Telegram Stars received by the affiliate from the transaction, rounded to 0; can be negative for refunds</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Amount { get; set; }

    /// <summary><em>Optional</em>. The number of 1/1000000000 shares of Telegram Stars received by the affiliate; from -999999999 to 999999999; can be negative for refunds</summary>
    public int? NanostarAmount { get; set; }
}
