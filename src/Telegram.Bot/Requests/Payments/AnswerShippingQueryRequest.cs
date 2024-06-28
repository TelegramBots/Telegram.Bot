namespace Telegram.Bot.Requests;

/// <summary>If you sent an invoice requesting a shipping address and the parameter <em>IsFlexible</em> was specified, the Bot API will send an <see cref="Update"/> with a <em>ShippingQuery</em> field to the bot. Use this method to reply to shipping queries<para>Returns: </para></summary>
public partial class AnswerShippingQueryRequest : RequestBase<bool>
{
    /// <summary>Unique identifier for the query to be answered</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string ShippingQueryId { get; set; }

    /// <summary>Specify <see langword="true"/> if everything is alright at this stage and the bot is ready to proceed.<br/> Use <see langword="false"/> and fill <see cref="ErrorMessage"/> if there are any problems.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required bool Ok { get; set; }

    /// <summary>A array of available shipping options.</summary>
    public IEnumerable<ShippingOption>? ShippingOptions { get; set; }

    /// <summary>Error message in human readable form that explains why it is impossible to complete the order (e.g. "Sorry, delivery to your desired address is unavailable'). Telegram will display this message to the user.</summary>
    public string? ErrorMessage { get; set; }

    /// <summary>Initializes an instance of <see cref="AnswerShippingQueryRequest"/></summary>
    /// <param name="shippingQueryId">Unique identifier for the query to be answered</param>
    /// <param name="ok">Specify <see langword="true"/> if everything is alright at this stage and the bot is ready to proceed.<br/> Use <see langword="false"/> and fill <see cref="ErrorMessage"/> if there are any problems.</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public AnswerShippingQueryRequest(string shippingQueryId, bool ok) : this()
    {
        ShippingQueryId = shippingQueryId;
        Ok = ok;
    }

    /// <summary>Instantiates a new <see cref="AnswerShippingQueryRequest"/></summary>
    public AnswerShippingQueryRequest() : base("answerShippingQuery") { }
}
