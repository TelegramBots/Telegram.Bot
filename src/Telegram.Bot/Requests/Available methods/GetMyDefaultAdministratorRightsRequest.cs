namespace Telegram.Bot.Requests;

/// <summary>Use this method to get the current default administrator rights of the bot.<para>Returns: <see cref="ChatAdministratorRights"/> on success.</para></summary>
public partial class GetMyDefaultAdministratorRightsRequest : RequestBase<ChatAdministratorRights>
{
    /// <summary>Pass <see langword="true"/> to get default administrator rights of the bot in channels. Otherwise, default administrator rights of the bot for groups and supergroups will be returned.</summary>
    public bool ForChannels { get; set; }

    /// <summary>Instantiates a new <see cref="GetMyDefaultAdministratorRightsRequest"/></summary>
    public GetMyDefaultAdministratorRightsRequest() : base("getMyDefaultAdministratorRights") { }
}
