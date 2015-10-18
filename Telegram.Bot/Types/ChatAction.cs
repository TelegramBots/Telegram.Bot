using System;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Type of action the Bot is performing
    /// </summary>
    public enum ChatAction
    {
        Typing,
        UploadPhoto,
        RecordVideo,
        UploadVideo,
        RecordAudio,
        UploadAudio,
        UploadDocument,
        FindLocation,
    }

    internal static class ChatActionExtension
    {
        internal static string ToActionString(this ChatAction action)
        {
            switch (action)
            {
                case ChatAction.Typing:
                    return "typing";
                case ChatAction.UploadPhoto:
                    return "upload_photo";
                case ChatAction.RecordVideo:
                    return "record_video";
                case ChatAction.UploadVideo:
                    return "upload_video";
                case ChatAction.RecordAudio:
                    return "record_audio";
                case ChatAction.UploadAudio:
                    return "upload_audio";
                case ChatAction.UploadDocument:
                    return "upload_document";
                case ChatAction.FindLocation:
                    return "find_location";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
