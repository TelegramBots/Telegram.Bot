// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>This object describes paid media. Currently, it can be one of<br/><see cref="PaidMediaPreview"/>, <see cref="PaidMediaPhoto"/>, <see cref="PaidMediaVideo"/></summary>
[JsonConverter(typeof(PolymorphicJsonConverter<PaidMedia>))]
[CustomJsonPolymorphic("type")]
[CustomJsonDerivedType(typeof(PaidMediaPreview), "preview")]
[CustomJsonDerivedType(typeof(PaidMediaPhoto), "photo")]
[CustomJsonDerivedType(typeof(PaidMediaVideo), "video")]
public abstract partial class PaidMedia
{
    /// <summary>Type of the paid media, always <see cref="PaidMedia"/></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public abstract PaidMediaType Type { get; }
}

/// <summary>The paid media isn't available before the payment.</summary>
public partial class PaidMediaPreview : PaidMedia
{
    /// <summary>Type of the paid media, always <see cref="PaidMediaType.Preview"/></summary>
    public override PaidMediaType Type => PaidMediaType.Preview;

    /// <summary><em>Optional</em>. Media width as defined by the sender</summary>
    public int Width { get; set; }

    /// <summary><em>Optional</em>. Media height as defined by the sender</summary>
    public int Height { get; set; }

    /// <summary><em>Optional</em>. Duration of the media in seconds as defined by the sender</summary>
    public int Duration { get; set; }
}

/// <summary>The paid media is a photo.</summary>
public partial class PaidMediaPhoto : PaidMedia
{
    /// <summary>Type of the paid media, always <see cref="PaidMediaType.Photo"/></summary>
    public override PaidMediaType Type => PaidMediaType.Photo;

    /// <summary>The photo</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public PhotoSize[] Photo { get; set; } = default!;
}

/// <summary>The paid media is a video.</summary>
public partial class PaidMediaVideo : PaidMedia
{
    /// <summary>Type of the paid media, always <see cref="PaidMediaType.Video"/></summary>
    public override PaidMediaType Type => PaidMediaType.Video;

    /// <summary>The video</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Video Video { get; set; } = default!;
}
