namespace Telegram.Bot.Requests;

/// <summary>If you sent an invoice requesting a shipping address and the parameter <em>IsFlexible</em> was specified, the Bot API will send an <see cref="Update"/> with a <em>ShippingQuery</em> field to the bot. Use this method to reply to shipping queries<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class AnswerShippingQueryRequest() : RequestBase<bool>("answerShippingQuery")
{
    /// <summary>Unique identifier for the query to be answered</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string ShippingQueryId { get; set; }

    /// <summary>Pass <see langword="true"/> if delivery to the specified address is possible and <see langword="false"/> if there are any problems (for example, if delivery to the specified address is not possible)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required bool Ok { get; set; }

    /// <summary>A array of available shipping options.</summary>
    public IEnumerable<ShippingOption>? ShippingOptions { get; set; }

    /// <summary>Error message in human readable form that explains why it is impossible to complete the order (e.g. “Sorry, delivery to your desired address is unavailable”). Telegram will display this message to the user.</summary>
    public string? ErrorMessage { get; set; }
}
