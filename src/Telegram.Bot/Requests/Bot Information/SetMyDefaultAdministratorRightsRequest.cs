// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to change the default administrator rights requested by the bot when it's added as an administrator to groups or channels. These rights will be suggested to users, but they are free to modify the list before adding the bot.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetMyDefaultAdministratorRightsRequest() : RequestBase<bool>("setMyDefaultAdministratorRights")
{
    /// <summary>An object describing new default administrator rights. If not specified, the default administrator rights will be cleared.</summary>
    public ChatAdministratorRights? Rights { get; set; }

    /// <summary>Pass <see langword="true"/> to change the default administrator rights of the bot in channels. Otherwise, the default administrator rights of the bot for groups and supergroups will be changed.</summary>
    [JsonPropertyName("for_channels")]
    public bool ForChannels { get; set; }
}
