using Newtonsoft.Json.Converters;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about a voice chat scheduled in the chat.
/// </summary>
[Obsolete("This type will be removed in the next major version, use VideoChatScheduled instead")]
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class VoiceChatScheduled
{
    /// <summary>
    /// Point in time when the voice chat is supposed to be started by a chat administrator
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime StartDate { get; set; }
}
