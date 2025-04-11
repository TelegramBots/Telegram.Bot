// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Removes the current profile photo of a managed business account. Requires the <em>CanEditProfilePhoto</em> business bot right.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class RemoveBusinessAccountProfilePhotoRequest() : RequestBase<bool>("removeBusinessAccountProfilePhoto"), IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }

    /// <summary>Pass <see langword="true"/> to remove the public photo, which is visible even if the main photo is hidden by the business account's privacy settings. After the main photo is removed, the previous profile photo (if present) becomes the main photo.</summary>
    [JsonPropertyName("is_public")]
    public bool IsPublic { get; set; }
}
