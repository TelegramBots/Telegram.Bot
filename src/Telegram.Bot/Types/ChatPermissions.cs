namespace Telegram.Bot.Types;

/// <summary>
/// Describes actions that a non-administrator user is allowed to take in a chat.
/// </summary>
public class ChatPermissions
{
    /// <summary>
    /// Optional. <see langword="true"/>, if the user is allowed to send text messages, contacts, locations and venues
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanSendMessages { get; set; }

    /// <summary>
    /// Optional. <see langword="true" />, if the user is allowed to send audios
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanSendAudios { get; set; }

    /// <summary>
    /// Optional. <see langword="true" />, if the user is allowed to send documents
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanSendDocuments { get; set; }

    /// <summary>
    /// Optional. <see langword="true" />, if the user is allowed to send photos
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanSendPhotos { get; set; }

    /// <summary>
    /// Optional. <see langword="true" />, if the user is allowed to send videos
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanSendVideos { get; set; }

    /// <summary>
    /// Optional. <see langword="true" />, if the user is allowed to send video notes
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanSendVideoNotes { get; set; }

    /// <summary>
    /// Optional. <see langword="true" />, if the user is allowed to send voice notes
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanSendVoiceNotes { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the user is allowed to send polls, implies <see cref="CanSendMessages"/>
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanSendPolls { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the user is allowed to send animations, games, stickers and use inline
    /// bots
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanSendOtherMessages { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the user is allowed to add web page previews to their messages
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanAddWebPagePreviews { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the user is allowed to change the chat title, photo and other settings.
    /// Ignored in public supergroups
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanChangeInfo { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the user is allowed to invite new users to the chat
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanInviteUsers { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the user is allowed to pin messages. Ignored in public supergroups
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanPinMessages { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the user is allowed to create forum topics.
    /// If omitted defaults to the value of <see cref="CanPinMessages"/>
    /// supergroups only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? CanManageTopics { get; set; }
}
