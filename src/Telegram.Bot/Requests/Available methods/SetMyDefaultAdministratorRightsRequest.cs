// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to change the default administrator rights requested by the bot when it's added as an
/// administrator to groups or channels. These rights will be suggested to users, but they are are free to
/// modify the list before adding the bot. Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SetMyDefaultAdministratorRightsRequest : RequestBase<bool>
{

    /// <summary>
    /// Optional. An object describing new default administrator rights. If not specified, the default administrator
    /// rights will be cleared.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ChatAdministratorRights? Rights { get; set; }

    /// <summary>
    /// Optional. Pass <see langword="true"/> to change the default administrator rights of the bot in channels. Otherwise,
    /// the default administrator rights of the bot for groups and supergroups will be changed.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? ForChannels { get; set; }

    /// <summary>
    ///
    /// </summary>
    public SetMyDefaultAdministratorRightsRequest()
        : base("setMyDefaultAdministratorRights")
    { }
}
