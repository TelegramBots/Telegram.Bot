namespace Telegram.Bot.Types;

/// <summary>
/// Describes actions that a non-administrator user is allowed to take in a chat.
/// </summary>
public partial class ChatPermissions
{
    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the user is allowed to send text messages, contacts, giveaways, giveaway winners, invoices, locations and venues
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanSendMessages { get; set; }

    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the user is allowed to send audios
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanSendAudios { get; set; }

    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the user is allowed to send documents
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanSendDocuments { get; set; }

    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the user is allowed to send photos
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanSendPhotos { get; set; }

    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the user is allowed to send videos
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanSendVideos { get; set; }

    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the user is allowed to send video notes
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanSendVideoNotes { get; set; }

    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the user is allowed to send voice notes
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanSendVoiceNotes { get; set; }

    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the user is allowed to send polls
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanSendPolls { get; set; }

    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the user is allowed to send animations, games, stickers and use inline bots
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanSendOtherMessages { get; set; }

    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the user is allowed to add web page previews to their messages
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanAddWebPagePreviews { get; set; }

    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the user is allowed to change the chat title, photo and other settings. Ignored in public supergroups
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanChangeInfo { get; set; }

    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the user is allowed to invite new users to the chat
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanInviteUsers { get; set; }

    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the user is allowed to pin messages. Ignored in public supergroups
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanPinMessages { get; set; }

    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the user is allowed to create forum topics. If omitted defaults to the value of <see cref="CanPinMessages">CanPinMessages</see>
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool CanManageTopics { get; set; }
}
