using System;
using System.Collections.Generic;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// The type of a <see cref="Message"/>
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// The <see cref="Message"/> is unknown
        /// </summary>
        UnknownMessage = 0,

        /// <summary>
        /// The <see cref="Message"/> contains text
        /// </summary>
        TextMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="PhotoSize"/>
        /// </summary>
        PhotoMessage,

        /// <summary>
        /// The <see cref="Message"/> contains an <see cref="Audio"/>
        /// </summary>
        AudioMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="Video"/>
        /// </summary>
        VideoMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="Voice"/>
        /// </summary>
        VoiceMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="Document"/>
        /// </summary>
        DocumentMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="Sticker"/>
        /// </summary>
        StickerMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="Location"/>
        /// </summary>
        LocationMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="Contact"/>
        /// </summary>
        ContactMessage,

        /// <summary>
        /// The <see cref="Message"/> contains meta informations, for example <see cref="Message.GroupChatCreated"/> or <see cref="Message.NewChatTitle"/>
        /// </summary>
        ServiceMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="Venue"/>
        /// </summary>
        VenueMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="Game"/>
        /// </summary>
        GameMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="VideoNote"/>
        /// </summary>
        VideoNoteMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="Invoice"/>
        /// </summary>
        Invoice,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="SuccessfulPayment"/>
        /// </summary>
        SuccessfulPayment,
    }

    internal static class MessageTypeExtension
    {
        internal static KeyValuePair<string, string> ToKeyValue(this MessageType type)
        {
            switch (type)
            {
                case MessageType.TextMessage:
                    return new KeyValuePair<string, string>("sendMessage", "text");
                case MessageType.PhotoMessage:
                    return new KeyValuePair<string, string>("sendPhoto", "photo");
                case MessageType.AudioMessage:
                    return new KeyValuePair<string, string>("sendAudio", "audio");
                case MessageType.VideoMessage:
                    return new KeyValuePair<string, string>("sendVideo", "video");
                case MessageType.VoiceMessage:
                    return new KeyValuePair<string, string>("sendVoice", "voice");
                case MessageType.DocumentMessage:
                    return new KeyValuePair<string, string>("sendDocument", "document");
                case MessageType.StickerMessage:
                    return new KeyValuePair<string, string>("sendSticker", "sticker");
                case MessageType.LocationMessage:
                    return new KeyValuePair<string, string>("sendLocation", "latitude");
                case MessageType.ContactMessage:
                    return new KeyValuePair<string, string>("sendContact", "phone_number");
                case MessageType.VenueMessage:
                    return new KeyValuePair<string, string>("sendVenue", "latitude");
                case MessageType.GameMessage:
                    return new KeyValuePair<string, string>("sendGame", "game_short_name");
                case MessageType.VideoNoteMessage:
                    return new KeyValuePair<string, string>("sendVideoNote", "video_note");
                case MessageType.Invoice:
                    return new KeyValuePair<string, string>("sendInvoice", "title");
                default:
                    throw new NotImplementedException();
            }
        } 
    }
}
