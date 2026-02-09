// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Changes the profile photo of the bot.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetMyProfilePhotoRequest() : FileRequestBase<bool>("setMyProfilePhoto")
{
    /// <summary>The new profile photo to set</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputProfilePhoto Photo { get; set; }
}
