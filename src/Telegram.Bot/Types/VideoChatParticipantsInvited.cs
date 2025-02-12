// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents a service message about new members invited to a video chat.</summary>
public partial class VideoChatParticipantsInvited
{
    /// <summary>New members that were invited to the video chat</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User[] Users { get; set; } = default!;

    /// <summary>Implicit conversion to User[] (Users)</summary>
    public static implicit operator User[](VideoChatParticipantsInvited self) => self.Users;
    /// <summary>Implicit conversion from User[] (Users)</summary>
    public static implicit operator VideoChatParticipantsInvited(User[] users) => new() { Users = users };
}
