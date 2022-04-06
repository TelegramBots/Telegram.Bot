using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Extensions;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this method to send a group of photos, videos, documents or audios as an album. Documents and
/// audio files can be only grouped in an album with messages of the same type. On success, an array
/// of <see cref="Message"/>s that were sent is returned.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class SendMediaGroupRequest : FileRequestBase<Message[]>, IChatTargetable
{
    /// <inheritdoc />
    [JsonProperty(Required = Required.Always)]
    public ChatId ChatId { get; }

    /// <summary>
    /// An array describing messages to be sent, must include 2-10 items
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public IEnumerable<IAlbumInputMedia> Media { get; }

    /// <inheritdoc cref="Abstractions.Documentation.DisableNotification"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? DisableNotification { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ProtectContent"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? ProtectContent { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.ReplyToMessageId"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? ReplyToMessageId { get; set; }

    /// <inheritdoc cref="Abstractions.Documentation.AllowSendingWithoutReply"/>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? AllowSendingWithoutReply { get; set; }

    /// <summary>
    /// Initializes a request with chatId and media
    /// </summary>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="media">An array describing messages to be sent, must include 2-10 items</param>
    public SendMediaGroupRequest(ChatId chatId, IEnumerable<IAlbumInputMedia> media)
        : base("sendMediaGroup")
    {
        ChatId = chatId;
        Media = media;
    }

    /// <inheritdoc />
    public override HttpContent ToHttpContent()
    {
        var httpContent = GenerateMultipartFormDataContent();
        httpContent.AddContentIfInputFileStream(Media.Cast<IInputMedia>().ToArray());
        return httpContent;
    }
}