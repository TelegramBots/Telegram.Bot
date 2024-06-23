namespace Telegram.Bot.Types;

/// <summary>This object represents a service message about new members invited to a video chat.</summary>
public partial class VideoChatParticipantsInvited
{
    /// <summary>New members that were invited to the video chat</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User[] Users { get; set; } = default!;
}
