// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
///
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class GetForumTopicIconStickersRequest : RequestBase<Sticker[]>
{
    /// <summary>
    ///
    /// </summary>
    public GetForumTopicIconStickersRequest() : base("getForumTopicIconStickers")
    { }
}
