using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types;

/// <summary>
/// A placeholder, currently holds no information. Use <see href="https://t.me/botfather">@BotFather</see> to set up your game.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class CallbackGame
{
}