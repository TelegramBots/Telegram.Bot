namespace Telegram.Bot.Requests;

/// <summary>Use this method to get custom emoji stickers, which can be used as a forum topic icon by any user.<para>Returns: An Array of <see cref="Sticker"/> objects.</para></summary>
public partial class GetForumTopicIconStickersRequest : ParameterlessRequest<Sticker[]>
{
    /// <summary>Instantiates a new <see cref="GetForumTopicIconStickersRequest"/></summary>
    public GetForumTopicIconStickersRequest() : base("getForumTopicIconStickers") { }
}
