// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>At most <b>one</b> of the optional fields can be present in any given object.</summary>
public partial class PollMedia
{
    /// <summary><em>Optional</em>. Media is an animation, information about the animation</summary>
    public Animation? Animation { get; set; }

    /// <summary><em>Optional</em>. Media is an audio file, information about the file; currently, can't be received in a poll option</summary>
    public Audio? Audio { get; set; }

    /// <summary><em>Optional</em>. Media is a general file, information about the file; currently, can't be received in a poll option</summary>
    public Document? Document { get; set; }

    /// <summary><em>Optional</em>. The HTTP link attached to the poll option</summary>
    public Link? Link { get; set; }

    /// <summary><em>Optional</em>. Media is a live photo, information about the live photo</summary>
    [JsonPropertyName("live_photo")]
    public LivePhoto? LivePhoto { get; set; }

    /// <summary><em>Optional</em>. Media is a shared location, information about the location</summary>
    public Location? Location { get; set; }

    /// <summary><em>Optional</em>. Media is a photo, available sizes of the photo</summary>
    public PhotoSize[]? Photo { get; set; }

    /// <summary><em>Optional</em>. Media is a sticker, information about the sticker; currently, for poll options only</summary>
    public Sticker? Sticker { get; set; }

    /// <summary><em>Optional</em>. Media is a venue, information about the venue</summary>
    public Venue? Venue { get; set; }

    /// <summary><em>Optional</em>. Media is a video, information about the video</summary>
    public Video? Video { get; set; }

    /// <summary>Gets the <see cref="PollMediaType">type</see> of the <see cref="PollMedia"/></summary>
    /// <value>The <see cref="PollMediaType">type</see> of the <see cref="PollMedia"/></value>
    [JsonIgnore]
    public PollMediaType Type => this switch
    {
        { Animation: not null }               => PollMediaType.Animation,
        { Audio: not null }                   => PollMediaType.Audio,
        { Document: not null }                => PollMediaType.Document,
        { Link: not null }                    => PollMediaType.Link,
        { LivePhoto: not null }               => PollMediaType.LivePhoto,
        { Location: not null }                => PollMediaType.Location,
        { Photo: not null }                   => PollMediaType.Photo,
        { Sticker: not null }                 => PollMediaType.Sticker,
        { Venue: not null }                   => PollMediaType.Venue,
        { Video: not null }                   => PollMediaType.Video,
        _                                     => PollMediaType.Unknown
    };
}
