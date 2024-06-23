namespace Telegram.Bot.Types.Payments;

/// <summary>This object describes the source of a transaction, or its recipient for outgoing transactions. Currently, it can be one of<br/><see cref="TransactionPartnerFragment"/>, <see cref="TransactionPartnerUser"/>, <see cref="TransactionPartnerOther"/></summary>
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(TransactionPartnerFragment), "fragment")]
[CustomJsonDerivedType(typeof(TransactionPartnerUser), "user")]
[CustomJsonDerivedType(typeof(TransactionPartnerOther), "other")]
public abstract partial class TransactionPartner
{
    /// <summary>Type of the transaction partner, always <see cref="TransactionPartner"/></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract TransactionPartnerType Type { get; }
}

/// <summary>Describes a withdrawal transaction with Fragment.</summary>
public partial class TransactionPartnerFragment : TransactionPartner
{
    /// <summary>Type of the transaction partner, always <see cref="TransactionPartnerType.Fragment"/></summary>
    public override TransactionPartnerType Type => TransactionPartnerType.Fragment;

    /// <summary><em>Optional</em>. State of the transaction if the transaction is outgoing</summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public RevenueWithdrawalState? WithdrawalState { get; set; }
}

/// <summary>Describes a transaction with a user.</summary>
public partial class TransactionPartnerUser : TransactionPartner
{
    /// <summary>Type of the transaction partner, always <see cref="TransactionPartnerType.User"/></summary>
    public override TransactionPartnerType Type => TransactionPartnerType.User;

    /// <summary>Information about the user</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User User { get; set; } = default!;
}

/// <summary>Describes a transaction with an unknown source or recipient.</summary>
public partial class TransactionPartnerOther : TransactionPartner
{
    /// <summary>Type of the transaction partner, always <see cref="TransactionPartnerType.Other"/></summary>
    public override TransactionPartnerType Type => TransactionPartnerType.Other;
}
