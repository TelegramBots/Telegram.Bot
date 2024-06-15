// ReSharper disable once CheckNamespace

namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to get the current default administrator rights of the bot.
/// Returns <see cref="ChatAdministratorRights"/> on success.
/// </summary>
public class GetMyDefaultAdministratorRightsRequest : RequestBase<ChatAdministratorRights>
{
    /// <summary>
    /// Pass <see langword="true"/> to get default administrator rights of the bot in channels. Otherwise, default administrator
    /// rights of the bot for groups and supergroups will be returned.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ForChannels { get; set; }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public GetMyDefaultAdministratorRightsRequest()
        : base("getMyDefaultAdministratorRights")
    { }
}
