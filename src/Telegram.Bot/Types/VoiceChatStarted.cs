namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about a voice chat started in the chat. Currently holds no information.
/// </summary>
[Obsolete("This type will be removed in the next major version, use VoiceChatStarted instead")]
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class VoiceChatStarted
{ }
