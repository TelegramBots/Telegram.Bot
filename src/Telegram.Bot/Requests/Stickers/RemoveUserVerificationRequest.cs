namespace Telegram.Bot.Requests;

/// <summary>Removes verification from a user who is currently verified on behalf of the organization represented by the bot.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class RemoveUserVerificationRequest() : RequestBase<bool>("removeUserVerification"), IUserTargetable
{
    /// <summary>Unique identifier of the target user</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }
}
