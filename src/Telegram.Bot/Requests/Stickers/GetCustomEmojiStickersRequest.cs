// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method to get information about custom emoji stickers by their identifiers.<para>Returns: An Array of <see cref="Sticker"/> objects.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class GetCustomEmojiStickersRequest() : RequestBase<Sticker[]>("getCustomEmojiStickers")
{
    /// <summary>A list of custom emoji identifiers. At most 200 custom emoji identifiers can be specified.</summary>
    [JsonPropertyName("custom_emoji_ids")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required IEnumerable<string> CustomEmojiIds { get; set; }
}
