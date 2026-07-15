// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a media element embedded in an outgoing rich message.</summary>
public partial class InputRichMessageMedia
{
    /// <summary>Unique identifier of the media used in a <c>tg://photo?id=</c>, <c>tg://video?id=</c>, or <c>tg://audio?id=</c> link. 1-64 characters, only <c>A-Z</c>, <c>a-z</c>, <c>0-9</c>, <c>_</c> and <c>-</c> are allowed.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public string Id { get; set; } = default!;

    /// <summary>The media to be sent. Everything except the media itself and its properties is ignored.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public IInputRichMedia Media { get; set; } = default!;
}
