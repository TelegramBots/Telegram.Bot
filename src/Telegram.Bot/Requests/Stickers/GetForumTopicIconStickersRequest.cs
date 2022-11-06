using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class GetForumTopicIconStickersRequest : RequestBase<Sticker[]>
{
    public GetForumTopicIconStickersRequest() : base("getForumTopicIconStickers")
    { }
}
