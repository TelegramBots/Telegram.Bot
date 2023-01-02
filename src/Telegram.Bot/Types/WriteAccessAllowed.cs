namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about a user allowing a bot added to the attachment menu to write
/// messages. Currently holds no information.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class WriteAccessAllowed { }
