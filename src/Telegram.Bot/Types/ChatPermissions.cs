namespace Telegram.Bot.Types;

/// <summary>Describes actions that a non-administrator user is allowed to take in a chat.</summary>
public partial class ChatPermissions
{
    /// <summary><em>Optional</em>. <see langword="true"/>, if the user is allowed to send text messages, contacts, giveaways, giveaway winners, invoices, locations and venues</summary>
    public bool CanSendMessages { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user is allowed to send audios</summary>
    public bool CanSendAudios { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user is allowed to send documents</summary>
    public bool CanSendDocuments { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user is allowed to send photos</summary>
    public bool CanSendPhotos { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user is allowed to send videos</summary>
    public bool CanSendVideos { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user is allowed to send video notes</summary>
    public bool CanSendVideoNotes { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user is allowed to send voice notes</summary>
    public bool CanSendVoiceNotes { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user is allowed to send polls</summary>
    public bool CanSendPolls { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user is allowed to send animations, games, stickers and use inline bots</summary>
    public bool CanSendOtherMessages { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user is allowed to add web page previews to their messages</summary>
    public bool CanAddWebPagePreviews { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user is allowed to change the chat title, photo and other settings. Ignored in public supergroups</summary>
    public bool CanChangeInfo { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user is allowed to invite new users to the chat</summary>
    public bool CanInviteUsers { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user is allowed to pin messages. Ignored in public supergroups</summary>
    public bool CanPinMessages { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the user is allowed to create forum topics. If omitted defaults to the value of <see cref="CanPinMessages">CanPinMessages</see></summary>
    public bool CanManageTopics { get; set; }
}
