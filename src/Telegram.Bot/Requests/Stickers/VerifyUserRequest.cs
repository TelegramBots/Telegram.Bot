namespace Telegram.Bot.Requests;

/// <summary>Verifies a user <a href="https://telegram.org/verify#third-party-verification">on behalf of the organization</a> which is represented by the bot.<para>Returns: </para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class VerifyUserRequest() : RequestBase<bool>("verifyUser"), IUserTargetable
{
    /// <summary>Unique identifier of the target user</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>Custom description for the verification; 0-70 characters. Must be empty if the organization isn't allowed to provide a custom verification description.</summary>
    public string? CustomDescription { get; set; }
}
