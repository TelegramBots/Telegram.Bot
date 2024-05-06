namespace Telegram.Bot.Types.Enums;

/// <summary>
/// Type of action to broadcast
/// </summary>
[JsonConverter(typeof(ChatActionConverter))]
public enum ChatAction
{
    /// <summary>
    /// Typing
    /// </summary>
    Typing = 1,

    /// <summary>
    /// Uploading a <see cref="PhotoSize"/>
    /// </summary>
    UploadPhoto,

    /// <summary>
    /// Recording a <see cref="Video"/>
    /// </summary>
    RecordVideo,

    /// <summary>
    /// Uploading a <see cref="Video"/>
    /// </summary>
    UploadVideo,

    /// <summary>
    /// Recording a <see cref="Voice"/>
    /// </summary>
    RecordVoice,

    /// <summary>
    /// Uploading a <see cref="Voice"/>
    /// </summary>
    UploadVoice,

    /// <summary>
    /// Uploading a <see cref="Document"/>
    /// </summary>
    UploadDocument,

    /// <summary>
    /// Finding a <see cref="Location"/>
    /// </summary>
    FindLocation,

    /// <summary>
    /// Recording a <see cref="VideoNote"/>
    /// </summary>
    RecordVideoNote,

    /// <summary>
    /// Uploading a <see cref="VideoNote"/>
    /// </summary>
    UploadVideoNote,

    /// <summary>
    /// Choosing a <see cref="Sticker"/>
    /// </summary>
    ChooseSticker,
}
