// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes the type of a query from a widget. Currently, it can be one of<br/><see cref="WidgetQueryTypeData"/>, <see cref="WidgetQueryTypeSignData"/>, <see cref="WidgetQueryTypeSentTransaction"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<WidgetQueryType>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(WidgetQueryTypeData), "data")]
[CustomJsonDerivedType(typeof(WidgetQueryTypeSignData), "sign_data")]
[CustomJsonDerivedType(typeof(WidgetQueryTypeSentTransaction), "sent_transaction")]
public abstract partial class WidgetQueryType
{
    /// <summary>Type of the query</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract WidgetQueryTypeKind Type { get; }
}

/// <summary>The query contains data for the bot.</summary>
public partial class WidgetQueryTypeData : WidgetQueryType
{
    /// <summary>Type of the query, always <see cref="WidgetQueryTypeKind.Data"/></summary>
    public override WidgetQueryTypeKind Type => WidgetQueryTypeKind.Data;

    /// <summary><em>Optional</em>. TON wallet card of the user who sent the query, if it was sent to the bot along with the query</summary>
    [JsonPropertyName("wallet_card")]
    public TonWalletCard? WalletCard { get; set; }
}

/// <summary>The query contains data signed by a TON wallet of the user who sent the query. The data is signed for the domain <c>bot&lt;BotId&gt;</c>, where <c>&lt;BotId&gt;</c> is the identifier of the bot.</summary>
public partial class WidgetQueryTypeSignData : WidgetQueryType
{
    /// <summary>Type of the query, always <see cref="WidgetQueryTypeKind.SignData"/></summary>
    public override WidgetQueryTypeKind Type => WidgetQueryTypeKind.SignData;

    /// <summary>The wallet that signed the data</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public WidgetQueryWallet Wallet { get; set; } = default!;

    /// <summary>JSON-serialized signed data</summary>
    [JsonPropertyName("sign_data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string SignData { get; set; } = default!;
}

/// <summary>The query notifies the bot about a transaction sent from a TON wallet of the user who sent the query.</summary>
public partial class WidgetQueryTypeSentTransaction : WidgetQueryType
{
    /// <summary>Type of the query, always <see cref="WidgetQueryTypeKind.SentTransaction"/></summary>
    public override WidgetQueryTypeKind Type => WidgetQueryTypeKind.SentTransaction;

    /// <summary>The wallet from which the transaction was sent</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public WidgetQueryWallet Wallet { get; set; } = default!;

    /// <summary>Value of <see cref="MsgHash">MsgHash</see> of the transaction</summary>
    [JsonPropertyName("msg_hash")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string MsgHash { get; set; } = default!;
}
