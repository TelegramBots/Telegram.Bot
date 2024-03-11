using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Telegram.Bot.Extensions;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to send animation files (GIF or H.264/MPEG-4 AVC video without sound). On success,
/// the sent <see cref="Message"/> is returned. Bots can currently send animation files of up to
/// 50 MB in size, this limit may be changed in the future.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SendAnimationRequest : FileRequestBase<Message>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Animation to send. Pass a <see cref="InputFileId"/> as String to send an animation
    /// that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram
    /// to get an animation from the Internet, or upload a new animation using multipart/form-data
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public required InputFile Animation { get; init; }

    /// <summary>
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? MessageThreadId { get; set; }

    /// <summary>
    /// Duration of sent animation in seconds
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Duration { get; set; }

    /// <summary>
    /// Animation width
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Width { get; set; }

    /// <summary>
    /// Animation height
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? Height { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.Thumbnail"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InputFile? Thumbnail { get; set; }

    /// <summary>
    /// Animation caption (may also be used when resending animation by
    /// <see cref="InputFileId"/>), 0-1024 characters after entities parsing
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Caption { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ParseMode"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ParseMode? ParseMode { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.CaptionEntities"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public IEnumerable<MessageEntity>? CaptionEntities { get; set; }

    /// <summary>
    /// Pass <see langword="true"/> if the photo needs to be covered with a spoiler animation
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? HasSpoiler { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.DisableNotification"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? DisableNotification { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ProtectContent"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? ProtectContent { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ReplyParameters"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ReplyParameters? ReplyParameters { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ReplyMarkup"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public IReplyMarkup? ReplyMarkup { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ReplyToMessageId"/>
    [Obsolete($"This property is deprecated, use {nameof(ReplyParameters)} instead")]
    [JsonIgnore]
    public int? ReplyToMessageId
    {
        get => ReplyParameters?.MessageId;
        set
        {
            if (value is null)
            {
                ReplyParameters = null;
            }
            else
            {
                ReplyParameters ??= new();
                ReplyParameters.MessageId = value.Value;
            }
        }
    }

    /// <summary>
    /// Initializes a new request with chatId and animation
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="animation">
    /// Animation to send. Pass a <see cref="InputFileId"/> as String to send an animation
    /// that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to
    /// get an animation from the Internet, or upload a new animation using multipart/form-data
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public SendAnimationRequest(ChatId chatId, InputFile animation)
        : this()
    {
        ChatId = chatId;
        Animation = animation;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public SendAnimationRequest()
        : base("sendAnimation")
    { }

    /// <inheritdoc />
    public override HttpContent? ToHttpContent() =>
        Animation is InputFileStream || Thumbnail is InputFileStream
            ? GenerateMultipartFormDataContent("animation", "thumbnail")
                .AddContentIfInputFile(media: Animation, name: "animation")
                .AddContentIfInputFile(media: Thumbnail, name: "thumbnail")
            : base.ToHttpContent();
}
