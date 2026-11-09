// This file is NOT auto-generated - check https://github.com/tdlib/td/blob/master/td/telegram/files/FileType.h#L17
namespace Telegram.Bot.Types.Enums;

#pragma warning disable CS1591

/// <summary>Type of file referenced by a FileId string</summary>
[JsonConverter(typeof(EnumConverter<FileIdType>))]
public enum FileIdType
{
    Thumbnail,
    ProfilePhoto,
    Photo,
    VoiceNote,
    Video,
    Document,
    Encrypted,
    Temp,
    Sticker,
    Audio,
    Animation,
    EncryptedThumbnail,
    Wallpaper,
    VideoNote,
    SecureDecrypted,
    SecureEncrypted,
    Background,
    DocumentAsFile,
    Ringtone,
    CallLog,
    PhotoStory,
    VideoStory,
    SelfDestructingPhoto,
    SelfDestructingVideo,
    SelfDestructingVideoNote,
    SelfDestructingVoiceNote,
    LivePhoto,
    SelfDestructingLivePhoto,
    SpeechRecognitionAudio,
}

