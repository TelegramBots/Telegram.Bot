namespace Telegram.Bot.Requests;

/// <summary>Once the user has confirmed their payment and shipping details, the Bot API sends the final confirmation in the form of an <see cref="Update"/> with the field <em>PreCheckoutQuery</em>. Use this method to respond to such pre-checkout queries <b>Note:</b> The Bot API must receive an answer within 10 seconds after the pre-checkout query was sent.<para>Returns: </para></summary>
public partial class AnswerPreCheckoutQueryRequest : RequestBase<bool>
{
    /// <summary>Unique identifier for the query to be answered</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string PreCheckoutQueryId { get; set; }

    /// <summary>Specify <see langword="true"/> if everything is alright at this stage and the bot is ready to proceed.<br/> Use <see langword="false"/> and fill <see cref="ErrorMessage"/> if there are any problems.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required bool Ok { get; set; }

    /// <summary>Error message in human readable form that explains the reason for failure to proceed with the checkout (e.g. "Sorry, somebody just bought the last of our amazing black T-shirts while you were busy filling out your payment details. Please choose a different color or garment!"). Telegram will display this message to the user.</summary>
    public string? ErrorMessage { get; set; }

    /// <summary>Initializes an instance of <see cref="AnswerPreCheckoutQueryRequest"/></summary>
    /// <param name="preCheckoutQueryId">Unique identifier for the query to be answered</param>
    /// <param name="ok">Specify <see langword="true"/> if everything is alright at this stage and the bot is ready to proceed.<br/> Use <see langword="false"/> and fill <see cref="ErrorMessage"/> if there are any problems.</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public AnswerPreCheckoutQueryRequest(string preCheckoutQueryId, bool ok) : this()
    {
        PreCheckoutQueryId = preCheckoutQueryId;
        Ok = ok;
    }

    /// <summary>Instantiates a new <see cref="AnswerPreCheckoutQueryRequest"/></summary>
    public AnswerPreCheckoutQueryRequest() : base("answerPreCheckoutQuery") { }
}
