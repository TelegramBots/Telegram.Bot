namespace Telegram.Bot.Requests;

/// <summary>Use this method when you need to tell the user that something is happening on the bot's side. The status is set for 5 seconds or less (when a message arrives from your bot, Telegram clients clear its typing status).<br/>We only recommend using this method when a response from the bot will take a <b>noticeable</b> amount of time to arrive.<para>Returns: </para></summary>
/// <remarks>Example: The <a href="https://t.me/imagebot">ImageBot</a> needs some time to process a request and upload the image. Instead of sending a text message along the lines of “Retrieving image, please wait…”, the bot may use <see cref="TelegramBotClientExtensions.SendChatActionAsync">SendChatAction</see> with <see cref="Action">Action</see> = <em>UploadPhoto</em>. The user will see a “sending photo” status for the bot.</remarks>
public partial class SendChatActionRequest : RequestBase<bool>, IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Type of action to broadcast. Choose one, depending on what the user is about to receive: <em>typing</em> for <a href="https://core.telegram.org/bots/api#sendmessage">text messages</a>, <em>UploadPhoto</em> for <see cref="TelegramBotClientExtensions.SendPhotoAsync">photos</see>, <em>RecordVideo</em> or <em>UploadVideo</em> for <see cref="TelegramBotClientExtensions.SendVideoAsync">videos</see>, <em>RecordVoice</em> or <em>UploadVoice</em> for <see cref="TelegramBotClientExtensions.SendVoiceAsync">voice notes</see>, <em>UploadDocument</em> for <see cref="TelegramBotClientExtensions.SendDocumentAsync">general files</see>, <em>ChooseSticker</em> for <see cref="TelegramBotClientExtensions.SendStickerAsync">stickers</see>, <em>FindLocation</em> for <see cref="TelegramBotClientExtensions.SendLocationAsync">location data</see>, <em>RecordVideoNote</em> or <em>UploadVideoNote</em> for <see cref="TelegramBotClientExtensions.SendVideoNoteAsync">video notes</see>.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatAction Action { get; set; }

    /// <summary>Unique identifier for the target message thread; for supergroups only</summary>
    public int? MessageThreadId { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the action will be sent</summary>
    public string? BusinessConnectionId { get; set; }

    /// <summary>Initializes an instance of <see cref="SendChatActionRequest"/></summary>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="action">Type of action to broadcast. Choose one, depending on what the user is about to receive: <em>typing</em> for <a href="https://core.telegram.org/bots/api#sendmessage">text messages</a>, <em>UploadPhoto</em> for <see cref="TelegramBotClientExtensions.SendPhotoAsync">photos</see>, <em>RecordVideo</em> or <em>UploadVideo</em> for <see cref="TelegramBotClientExtensions.SendVideoAsync">videos</see>, <em>RecordVoice</em> or <em>UploadVoice</em> for <see cref="TelegramBotClientExtensions.SendVoiceAsync">voice notes</see>, <em>UploadDocument</em> for <see cref="TelegramBotClientExtensions.SendDocumentAsync">general files</see>, <em>ChooseSticker</em> for <see cref="TelegramBotClientExtensions.SendStickerAsync">stickers</see>, <em>FindLocation</em> for <see cref="TelegramBotClientExtensions.SendLocationAsync">location data</see>, <em>RecordVideoNote</em> or <em>UploadVideoNote</em> for <see cref="TelegramBotClientExtensions.SendVideoNoteAsync">video notes</see>.</param>
    [Obsolete("Use parameterless constructor with required properties")]
    [SetsRequiredMembers]
    public SendChatActionRequest(ChatId chatId, ChatAction action) : this()
    {
        ChatId = chatId;
        Action = action;
    }

    /// <summary>Instantiates a new <see cref="SendChatActionRequest"/></summary>
    public SendChatActionRequest() : base("sendChatAction") { }
}
