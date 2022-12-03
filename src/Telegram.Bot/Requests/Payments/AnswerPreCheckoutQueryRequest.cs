// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Once the user has confirmed their payment and shipping details, the Bot API sends the final
/// confirmation in the form of an <see cref="Types.Update"/> with the field
/// <see cref="Types.Update.PreCheckoutQuery"/>. Use this method to respond to such pre-checkout
/// queries. On success, <see langword="true"/> is returned.
/// </summary>
/// <remarks>
/// The Bot API must receive an answer within 10 seconds after the pre-checkout query was sent.
/// </remarks>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class AnswerPreCheckoutQueryRequest : RequestBase<bool>
{
    /// <summary>
    /// Unique identifier for the query to be answered
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string PreCheckoutQueryId { get; }

    /// <summary>
    /// Specify <see langword="true"/> if everything is alright (goods are available, etc.) and the
    /// bot is ready to proceed with the order. Use <see langword="false"/> if there are any problems.
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public bool Ok { get; }

    /// <summary>
    /// Required if <see cref="Ok"/> is <see langword="false"/>. Error message in human readable form that explains
    /// the reason for failure to proceed with the checkout (e.g. "Sorry, somebody just bought
    /// the last of our amazing black T-shirts while you were busy filling out your payment details.
    /// Please choose a different color or garment!"). Telegram will display this message to the user.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? ErrorMessage { get; }

    /// <summary>
    /// Initializes a new successful answerPreCheckoutQuery request
    /// </summary>
    /// <param name="preCheckoutQueryId">Unique identifier for the query to be answered</param>
    public AnswerPreCheckoutQueryRequest(string preCheckoutQueryId)
        : base("answerPreCheckoutQuery")
    {
        PreCheckoutQueryId = preCheckoutQueryId;
        Ok = true;
    }

    /// <summary>
    /// Initializes a new failing answerPreCheckoutQuery request with error message
    /// </summary>
    /// <param name="preCheckoutQueryId">Unique identifier for the query to be answered</param>
    /// <param name="errorMessage">
    /// Required if <see cref="Ok"/> is <see langword="true"/>. Error message in human readable form that explains the
    /// reason for failure to proceed with the checkout (e.g. "Sorry, somebody just bought the last of
    /// our amazing black T-shirts while you were busy filling out your payment details. Please
    /// choose a different color or garment!"). Telegram will display this message to the user.
    /// </param>
    public AnswerPreCheckoutQueryRequest(string preCheckoutQueryId, string errorMessage)
        : base("answerPreCheckoutQuery")
    {
        PreCheckoutQueryId = preCheckoutQueryId;
        Ok = false;
        ErrorMessage = errorMessage;
    }
}
