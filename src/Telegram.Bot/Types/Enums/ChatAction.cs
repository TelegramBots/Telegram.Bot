using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// Type of action to broadcast
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter), true)]
    public enum ChatAction
    {
        /// <summary>
        /// Typing
        /// </summary>
        Typing,

        /// <summary>
        /// Uploading a <see cref="PhotoSize"/>
        /// </summary>
        [EnumMember(Value = "upload_photo")]
        UploadPhoto,

        /// <summary>
        /// Recording a <see cref="Video"/>
        /// </summary>
        [EnumMember(Value = "record_video")]
        RecordVideo,

        /// <summary>
        /// Uploading a <see cref="Video"/>
        /// </summary>
        [EnumMember(Value = "upload_video")]
        UploadVideo,

        /// <summary>
        /// Recording an <see cref="Audio"/>
        /// </summary>
        [EnumMember(Value = "record_audio")]
        [Obsolete("use RecordVoice instead", true)]
        RecordAudio,

        /// <summary>
        /// Recording an <see cref="Voice"/>
        /// </summary>
        [EnumMember(Value = "record_voice")]
        RecordVoice,

        /// <summary>
        /// Uploading an <see cref="Audio"/>
        /// </summary>
        [EnumMember(Value = "upload_audio")]
        [Obsolete("use UploadVoice instead", true)]
        UploadAudio,

        /// <summary>
        /// Uploading an <see cref="Voice"/>
        /// </summary>
        [EnumMember(Value = "upload_voice")]
        UploadVoice,

        /// <summary>
        /// Uploading <see cref="Document"/>
        /// </summary>
        [EnumMember(Value = "upload_document")]
        UploadDocument,

        /// <summary>
        /// Finding a <see cref="Location"/>
        /// </summary>
        [EnumMember(Value = "find_location")]
        FindLocation,

        /// <summary>
        /// Recording a <see cref="VideoNote"/>
        /// </summary>
        [EnumMember(Value = "record_video_note")]
        RecordVideoNote,

        /// <summary>
        /// Uploading a <see cref="VideoNote"/>
        /// </summary>
        [EnumMember(Value = "upload_video_note")]
        UploadVideoNote,
    }
}
