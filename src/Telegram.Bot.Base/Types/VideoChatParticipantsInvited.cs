namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about new members invited to a video chat.
/// </summary>
public class VideoChatParticipantsInvited
{
    /// <summary>
    /// Optional. New members that were invited to the voice chat
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User[] Users { get; set; } = default!;
}
