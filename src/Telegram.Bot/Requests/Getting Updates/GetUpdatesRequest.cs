using System.Collections.Generic;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to receive incoming updates using long polling
/// (<a href="https://en.wikipedia.org/wiki/Push_technology#Long_polling">wiki</a>).
/// An Array of <see cref="Update"/> objects is returned.
/// </summary>
/// <remarks>
/// <list type="number">
/// <item>This method will not work if an outgoing webhook is set up.</item>
/// <item>
/// In order to avoid getting duplicate updates, recalculate <see cref="GetUpdatesRequest.Offset"/>
/// after each server response.
/// </item>
/// </list>
/// </remarks>
[JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class GetUpdatesRequest : RequestBase<Update[]>
{
    /// <summary>
    /// Identifier of the first update to be returned. Must be greater by one than the highest among
    /// the identifiers of previously received updates. By default, updates starting with the earliest
    /// unconfirmed update are returned. An update is considered confirmed as soon as
    /// <see cref="GetUpdatesRequest"/> is called with an <see cref="Offset"/> higher than its
    /// <see cref="Update.Id"/>. The negative offset can be specified to retrieve updates
    /// starting from <see cref="Offset">-offset</see> update from the end of the updates queue.
    /// All previous updates will forgotten.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Offset { get; set; }

    /// <summary>
    /// Limits the number of updates to be retrieved. Values between 1-100 are accepted. Defaults to 100
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Limit { get; set; }

    /// <summary>
    /// Timeout in seconds for long polling. Defaults to 0, i.e. usual short polling. Should be positive,
    /// short polling should be used for testing purposes only.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Timeout { get; set; }

    /// <summary>
    /// A list of the update types you want your bot to receive. For example, specify
    /// [<see cref="UpdateType.Message"/>, <see cref="UpdateType.EditedChannelPost"/>,
    /// <see cref="UpdateType.CallbackQuery"/>] to only receive updates of these types.
    /// See <see cref="UpdateType"/> for a complete list of available update types. Specify
    /// an empty list to receive all update types except <see cref="UpdateType.ChatMember"/>
    /// (default). If not specified, the previous setting will be used.
    /// </summary>
    /// <remarks>
    /// Please note that this parameter doesn't affect updates created before the call to the
    /// getUpdates, so unwanted updates may be received for a short period of time.
    /// </remarks>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public IEnumerable<UpdateType>? AllowedUpdates { get; set; }

    /// <summary>
    /// Initializes a new GetUpdates request
    /// </summary>
    public GetUpdatesRequest()
        : base("getUpdates")
    { }
}
