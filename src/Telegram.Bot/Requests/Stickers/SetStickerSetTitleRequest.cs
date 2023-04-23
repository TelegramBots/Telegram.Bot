namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to set the title of a created sticker set.
/// Returns <see langword="true"/> on success.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SetStickerSetTitleRequest : RequestBase<bool>
{
    /// <summary>
    /// Sticker set name
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Name { get; }

    /// <summary>
    /// Sticker set title, 1-64 characters
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public string Title { get; }

    /// <summary>
    /// Initializes a new request with name and title
    /// </summary>
    /// <param name="name">
    /// Sticker set name
    /// </param>
    /// <param name="title">
    /// Sticker set title, 1-64 characters
    /// </param>
    public SetStickerSetTitleRequest(string name, string title)
        : base("setStickerSetTitle")
    {
        Name = name;
        Title = title;
    }
}
