namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about new members invited to a video chat.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class VideoChatParticipantsInvited
{
    /// <summary>
    /// Optional. New members that were invited to the voice chat
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public User[] Users { get; set; } = default!;
}
