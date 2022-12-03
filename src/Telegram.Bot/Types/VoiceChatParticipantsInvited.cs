namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about new members invited to a voice chat.
/// </summary>
[Obsolete("This type will be removed in the next major version, use VideoChatParticipantsInvited instead")]
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class VoiceChatParticipantsInvited
{
    /// <summary>
    /// Optional. New members that were invited to the voice chat
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public User[] Users { get; set; } = default!;
}
