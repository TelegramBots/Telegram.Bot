// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Changes the profile photo of a managed business account. Requires the <em>CanEditProfilePhoto</em> business bot right.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SetBusinessAccountProfilePhotoRequest() : FileRequestBase<bool>("setBusinessAccountProfilePhoto"), IBusinessConnectable
{
    /// <summary>Unique identifier of the business connection</summary>
    [JsonPropertyName("business_connection_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string BusinessConnectionId { get; set; }

    /// <summary>The new profile photo to set</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required InputProfilePhoto Photo { get; set; }

    /// <summary>Pass <see langword="true"/> to set the public photo, which will be visible even if the main photo is hidden by the business account's privacy settings. An account can have only one public photo.</summary>
    [JsonPropertyName("is_public")]
    public bool IsPublic { get; set; }
}
