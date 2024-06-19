namespace Telegram.Bot.Types;

/// <summary>
/// This object contains information about a message that is being replied to, which may come from another chat or forum topic.
/// </summary>
public partial class ExternalReplyInfo
{
    /// <summary>
    /// Origin of the message replied to by the given message
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public MessageOrigin Origin { get; set; } = default!;

    /// <summary>
    /// <em>Optional</em>. Chat the original message belongs to. Available only if the chat is a supergroup or a channel.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Chat? Chat { get; set; }

    /// <summary>
    /// <em>Optional</em>. Unique message identifier inside the original chat. Available only if the original chat is a supergroup or a channel.
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MessageId { get; set; }

    /// <summary>
    /// <em>Optional</em>. Options used for link preview generation for the original message, if it is a text message
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public LinkPreviewOptions? LinkPreviewOptions { get; set; }

    /// <summary>
    /// <em>Optional</em>. Message is an animation, information about the animation
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Animation? Animation { get; set; }

    /// <summary>
    /// <em>Optional</em>. Message is an audio file, information about the file
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Audio? Audio { get; set; }

    /// <summary>
    /// <em>Optional</em>. Message is a general file, information about the file
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Document? Document { get; set; }

    /// <summary>
    /// <em>Optional</em>. Message is a photo, available sizes of the photo
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PhotoSize[]? Photo { get; set; }

    /// <summary>
    /// <em>Optional</em>. Message is a sticker, information about the sticker
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Sticker? Sticker { get; set; }

    /// <summary>
    /// <em>Optional</em>. Message is a forwarded story
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Story? Story { get; set; }

    /// <summary>
    /// <em>Optional</em>. Message is a video, information about the video
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Video? Video { get; set; }

    /// <summary>
    /// <em>Optional</em>. Message is a <a href="https://telegram.org/blog/video-messages-and-telescope">video note</a>, information about the video message
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VideoNote? VideoNote { get; set; }

    /// <summary>
    /// <em>Optional</em>. Message is a voice message, information about the file
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Voice? Voice { get; set; }

    /// <summary>
    /// <em>Optional</em>. <see langword="true"/>, if the message media is covered by a spoiler animation
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool HasMediaSpoiler { get; set; }

    /// <summary>
    /// <em>Optional</em>. Message is a shared contact, information about the contact
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Contact? Contact { get; set; }

    /// <summary>
    /// <em>Optional</em>. Message is a dice with random value
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dice? Dice { get; set; }

    /// <summary>
    /// <em>Optional</em>. Message is a game, information about the game. <a href="https://core.telegram.org/bots/api#games">More about games »</a>
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Game? Game { get; set; }

    /// <summary>
    /// <em>Optional</em>. Message is a scheduled giveaway, information about the giveaway
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Giveaway? Giveaway { get; set; }

    /// <summary>
    /// <em>Optional</em>. A giveaway with public winners was completed
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public GiveawayWinners? GiveawayWinners { get; set; }

    /// <summary>
    /// <em>Optional</em>. Message is an invoice for a <a href="https://core.telegram.org/bots/api#payments">payment</a>, information about the invoice. <a href="https://core.telegram.org/bots/api#payments">More about payments »</a>
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Invoice? Invoice { get; set; }

    /// <summary>
    /// <em>Optional</em>. Message is a shared location, information about the location
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Location? Location { get; set; }

    /// <summary>
    /// <em>Optional</em>. Message is a native poll, information about the poll
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Poll? Poll { get; set; }

    /// <summary>
    /// <em>Optional</em>. Message is a venue, information about the venue
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Venue? Venue { get; set; }
}
