// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.Enums;

/// <summary>Type of action to broadcast</summary>
[JsonConverter(typeof(EnumConverter<ChatAction>))]
public enum ChatAction
{
    /// <summary>Typing <see cref="TelegramBotClientExtensions.SendMessage">text messages</see></summary>
    Typing = 1,
    /// <summary>Uploading <see cref="TelegramBotClientExtensions.SendPhoto">photos</see></summary>
    UploadPhoto,
    /// <summary>Recording <see cref="TelegramBotClientExtensions.SendVideo">videos</see></summary>
    RecordVideo,
    /// <summary>Uploading <see cref="TelegramBotClientExtensions.SendVideo">videos</see></summary>
    UploadVideo,
    /// <summary>Recording <see cref="TelegramBotClientExtensions.SendVoice">voice notes</see></summary>
    RecordVoice,
    /// <summary>Uploading <see cref="TelegramBotClientExtensions.SendVoice">voice notes</see></summary>
    UploadVoice,
    /// <summary>Uploading <see cref="TelegramBotClientExtensions.SendDocument">general files</see></summary>
    UploadDocument,
    /// <summary>Finding <see cref="TelegramBotClientExtensions.SendLocation">location data</see></summary>
    FindLocation,
    /// <summary>Recording <see cref="TelegramBotClientExtensions.SendVideoNote">video notes</see>.</summary>
    RecordVideoNote,
    /// <summary>Uploading <see cref="TelegramBotClientExtensions.SendVideoNote">video notes</see>.</summary>
    UploadVideoNote,
    /// <summary>Choosing <a href="https://core.telegram.org/bots/api#sendsticker">stickers</a></summary>
    ChooseSticker,
}
