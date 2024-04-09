using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types.Enums;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests;

/// <summary>
/// Use this request when you need to tell the user that something is happening on the bot’s side.
/// The status is set for 5 seconds or less (when a message arrives from your bot, Telegram clients
/// clear its typing status). Returns <see langword="true"/> on success.
/// </summary>
/// <remarks>
/// Example: The <a href="https://t.me/imagebot">ImageBot</a> needs some time to process a request
/// and upload the image. Instead of sending a text message along the lines of “Retrieving image,
/// please wait…”, the bot may use <see cref="SendChatActionRequest"/> with
/// <see cref="Action"/> = <see cref="ChatAction.UploadPhoto"/>. The user will see a “sending photo”
/// status for the bot.
/// <para>
/// We only recommend using this method when a response from the bot will take a <b>noticeable</b>
/// amount of time to arrive.
/// </para>
/// </remarks>

public class SendChatActionRequest : RequestBase<bool>, IChatTargetable, IBusinessConnectable
{
    /// <inheritdoc />
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BusinessConnectionId { get; set; }

    /// <inheritdoc />
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; init; }

    /// <summary>
    /// Type of action to broadcast. Choose one, depending on what the user is about to receive:
    /// <see cref="ChatAction.Typing"/> for <see cref="SendMessageRequest">text messages</see>,
    /// <see cref="ChatAction.UploadPhoto"/> for <see cref="SendPhotoRequest">photos</see>,
    /// <see cref="ChatAction.RecordVideo"/> or <see cref="ChatAction.UploadVideo"/> for
    /// <see cref="SendVideoRequest">videos</see>, <see cref="ChatAction.RecordVoice"/> or
    /// <see cref="ChatAction.UploadVoice"/> for <see cref="SendVoiceRequest">voice notes</see>,
    /// <see cref="ChatAction.UploadDocument"/> for <see cref="SendDocumentRequest">general files</see>,
    /// <see cref="ChatAction.FindLocation"/> for <see cref="SendLocationRequest">location data</see>,
    /// <see cref="ChatAction.RecordVideoNote"/> or <see cref="ChatAction.UploadVideoNote"/> for
    /// <see cref="SendVideoNoteRequest">video notes</see>
    /// </summary>
    [JsonRequired]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatAction Action { get; init; }

    /// <summary>
    /// Unique identifier for the target message thread; supergroups only
    /// </summary>
    [JsonInclude]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? MessageThreadId { get; set; }

    /// <summary>
    /// Initializes a new request chatId and action
    /// </summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="action">
    /// Type of action to broadcast. Choose one, depending on what the user is about to receive
    /// </param>
    [SetsRequiredMembers]
    [Obsolete("Use parameterless constructor with required properties")]
    public SendChatActionRequest(ChatId chatId, ChatAction action)
        : this()
    {
        ChatId = chatId;
        Action = action;
    }

    /// <summary>
    /// Initializes a new request
    /// </summary>
    public SendChatActionRequest()
        : base("sendChatAction")
    { }
}
