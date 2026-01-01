// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Use this method when you need to tell the user that something is happening on the bot's side. The status is set for 5 seconds or less (when a message arrives from your bot, Telegram clients clear its typing status).<br/>We only recommend using this method when a response from the bot will take a <b>noticeable</b> amount of time to arrive.</summary>
/// <remarks>Example: The <a href="https://t.me/imagebot">ImageBot</a> needs some time to process a request and upload the image. Instead of sending a text message along the lines of “Retrieving image, please wait…”, the bot may use <see cref="TelegramBotClientExtensions.SendChatAction">SendChatAction</see> with <see cref="Action">Action</see> = <em>UploadPhoto</em>. The user will see a “sending photo” status for the bot.</remarks>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SendChatActionRequest() : RequestBase<bool>("sendChatAction"), IChatTargetable, IBusinessConnectable
{
    /// <summary>Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>). Channel chats and channel direct messages chats aren't supported.</summary>
    [JsonPropertyName("chat_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatId ChatId { get; set; }

    /// <summary>Type of action to broadcast. Choose one, depending on what the user is about to receive: <em>typing</em> for <see cref="TelegramBotClientExtensions.SendMessage">text messages</see>, <em>UploadPhoto</em> for <see cref="TelegramBotClientExtensions.SendPhoto">photos</see>, <em>RecordVideo</em> or <em>UploadVideo</em> for <see cref="TelegramBotClientExtensions.SendVideo">videos</see>, <em>RecordVoice</em> or <em>UploadVoice</em> for <see cref="TelegramBotClientExtensions.SendVoice">voice notes</see>, <em>UploadDocument</em> for <see cref="TelegramBotClientExtensions.SendDocument">general files</see>, <em>ChooseSticker</em> for <see cref="TelegramBotClientExtensions.SendSticker">stickers</see>, <em>FindLocation</em> for <see cref="TelegramBotClientExtensions.SendLocation">location data</see>, <em>RecordVideoNote</em> or <em>UploadVideoNote</em> for <see cref="TelegramBotClientExtensions.SendVideoNote">video notes</see>.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required ChatAction Action { get; set; }

    /// <summary>Unique identifier of the business connection on behalf of which the action will be sent</summary>
    [JsonPropertyName("business_connection_id")]
    public string? BusinessConnectionId { get; set; }

    /// <summary>Unique identifier for the target message thread or topic of a forum; for supergroups and private chats of bots with forum topic mode enabled only</summary>
    [JsonPropertyName("message_thread_id")]
    public int? MessageThreadId { get; set; }
}
