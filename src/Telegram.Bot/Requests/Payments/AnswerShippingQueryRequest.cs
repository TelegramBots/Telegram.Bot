using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types.Payments;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// If you sent an invoice requesting a shipping address and the parameter
/// <see cref="SendInvoiceRequest.IsFlexible"/> was specified, the Bot API will send an
/// <see cref="Types.Update"/> with a <see cref="Types.Update.ShippingQuery"/> field to the
/// bot. Use this method to reply to shipping queries. On success, <see langword="true"/> is returned.
/// </summary>
public class AnswerShippingQueryRequest : RequestBase<bool>
{
    /// <summary>
    /// Unique identifier for the query to be answered
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string ShippingQueryId { get; init; }

    /// <summary>
    /// Specify <see langword="true"/> if delivery to the specified address is possible and <see langword="false"/>
    /// if there are any problems (for example, if delivery to the specified address is not possible)
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required bool Ok { get; init; }

    /// <summary>
    /// Required if <see cref="Ok"/> is <see langword="true"/>. An array of available shipping options.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<ShippingOption>? ShippingOptions { get; init; }

    /// <summary>
    /// Required if <see cref="Ok"/> is <see langword="false"/>. Error message in human readable form that explains
    /// why it is impossible to complete the order (e.g. "Sorry, delivery to your desired address
    /// is unavailable'). Telegram will display this message to the user.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Initializes a new failing answerShippingQuery request with error message
    /// </summary>
    /// <param name="shippingQueryId">Unique identifier for the query to be answered</param>
    /// <param name="errorMessage">Error message in human readable form</param>
    [SetsRequiredMembers]
    public AnswerShippingQueryRequest(string shippingQueryId, string errorMessage)
        : this()
    {
        ShippingQueryId = shippingQueryId;
        Ok = false;
        ErrorMessage = errorMessage;
    }

    /// <summary>
    /// Initializes a new successful answerShippingQuery request with shipping options
    /// </summary>
    /// <param name="shippingQueryId">Unique identifier for the query to be answered</param>
    /// <param name="shippingOptions">An array of available shipping options</param>
    [SetsRequiredMembers]
    public AnswerShippingQueryRequest(
        string shippingQueryId,
        IEnumerable<ShippingOption> shippingOptions) : this()
    {
        ShippingQueryId = shippingQueryId;
        Ok = true;
        ShippingOptions = shippingOptions;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public AnswerShippingQueryRequest()
        : base("answerShippingQuery")
    { }
}
