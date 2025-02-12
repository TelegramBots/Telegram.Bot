// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object represents one size of a photo or a <see cref="Document">file</see> / <see cref="Sticker"/> thumbnail.</summary>
public partial class PhotoSize : FileBase
{
    /// <summary>Photo width</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Width { get; set; }

    /// <summary>Photo height</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int Height { get; set; }
}
