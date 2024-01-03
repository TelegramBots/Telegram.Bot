namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a service message about the creation of a scheduled giveaway.
/// Currently holds no information.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class GiveawayCreated
{
}
