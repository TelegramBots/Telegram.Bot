using System;
using Newtonsoft.Json;
using Telegram.Bot.Helpers;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a message.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Message
    {
        /// <summary>
        /// Unique message identifier
        /// </summary>
        [JsonProperty(PropertyName = "message_id", Required = Required.Always)]
        public int MessageId;

        /// <summary>
        /// Sender
        /// </summary>
        [JsonProperty(PropertyName = "from", Required = Required.Always)]
        public User From;

        /// <summary>
        /// Date the message was sent in Unix time
        /// </summary>
        [JsonProperty(PropertyName = "date", Required = Required.Always)]
        [JsonConverter(typeof (UnixDateTimeConverter))]
        public DateTime Date;

        /// <summary>
        /// Conversation the message belongs to — user in case of a private message, GroupChat in case of a group
        /// </summary>
        [JsonProperty(PropertyName = "chat", Required = Required.Always)]
        [JsonConverter(typeof (ConversationConverter))]
        public IConversation Chat;

        /// <summary>
        /// Optional. For forwarded messages, sender of the original message
        /// </summary>
        [JsonProperty(PropertyName = "forward_from", Required = Required.Default)]
        public User ForwardFrom;

        /// <summary>
        /// Optional. For forwarded messages, date the original message was sent in Unix time
        /// </summary>
        [JsonProperty(PropertyName = "forward_date", Required = Required.Default)]
        [JsonConverter(typeof (UnixDateTimeConverter))]
        public DateTime ForwardDate;

        /// <summary>
        /// Optional. For replies, the original message. Note that the Message object in this field will not contain further reply_to_message fields even if it itself is a reply.
        /// </summary>
        [JsonProperty(PropertyName = "reply_to_message", Required = Required.Default)]
        public Message ReplayToMessage;

        /// <summary>
        /// Optional. For text messages, the actual UTF-8 text of the message
        /// </summary>
        [JsonProperty(PropertyName = "text", Required = Required.Default)]
        public string Text;

        /// <summary>
        /// Optional. Message is an audio file, information about the file
        /// </summary>
        [JsonProperty(PropertyName = "audio", Required = Required.Default)]
        public Audio Audio;

        /// <summary>
        /// Optional. Message is a general file, information about the file
        /// </summary>
        [JsonProperty(PropertyName = "document", Required = Required.Default)]
        public Document Document;

        /// <summary>
        /// Optional. Message is a photo, available sizes of the photo
        /// </summary>
        [JsonProperty(PropertyName = "photo", Required = Required.Default)]
        public PhotoSize[] Photo;

        /// <summary>
        /// Optional. Message is a sticker, information about the sticker
        /// </summary>
        [JsonProperty(PropertyName = "sticker", Required = Required.Default)]
        public Sticker Sticker;

        /// <summary>
        /// Optional. Message is a video, information about the video
        /// </summary>
        [JsonProperty(PropertyName = "video", Required = Required.Default)]
        public Video Video;

        /// <summary>
        /// Optional. Message is a shared contact, information about the contact
        /// </summary>
        [JsonProperty(PropertyName = "contact", Required = Required.Default)]
        public Contact Contact;

        /// <summary>
        /// Optional. Message is a shared location, information about the location
        /// </summary>
        [JsonProperty(PropertyName = "location", Required = Required.Default)]
        public Location Location;

        /// <summary>
        /// Optional. A new member was added to the group, information about them (this member may be bot itself)
        /// </summary>
        [JsonProperty(PropertyName = "new_chat_participant", Required = Required.Default)]
        public User NewChatParticipant;

        /// <summary>
        /// Optional. A member was removed from the group, information about them (this member may be bot itself)
        /// </summary>
        [JsonProperty(PropertyName = "left_chat_participant", Required = Required.Default)]
        public User LeftChatParticipant;

        /// <summary>
        /// Optional. A group title was changed to this value
        /// </summary>
        [JsonProperty(PropertyName = "new_chat_title", Required = Required.Default)]
        public string NewChatTitle;

        /// <summary>
        /// Optional. A group photo was change to this value
        /// </summary>
        [JsonProperty(PropertyName = "new_chat_photo", Required = Required.Default)]
        public PhotoSize[] NewChatPhoto;

        /// <summary>
        /// Optional. Informs that the group photo was deleted
        /// </summary>
        [JsonProperty(PropertyName = "delete_chat_photo", Required = Required.Default)]
        public bool DeleteChatPhoto;

        /// <summary>
        /// Optional. Informs that the group has been created
        /// </summary>
        [JsonProperty(PropertyName = "group_chat_created", Required = Required.Default)]
        public bool GroupChatCreated;

        public MessageType Type
        {
            get
            {
                if (!string.IsNullOrEmpty(Text))
                    return MessageType.TextMessage;

                if (Audio != null)
                    return MessageType.AudioMessage;

                if (Document != null)
                    return MessageType.DocumentMessage;

                if (Photo != null)
                    return MessageType.PhotoMessage;

                if (Sticker != null)
                    return MessageType.StickerMessage;

                if (Video != null)
                    return MessageType.VideoMessage;

                if (Contact != null)
                    return  MessageType.ContactMessage;

                if (Location != null)
                    return MessageType.LocationMessage;

                throw new FormatException("MessageType unknown");
            }
        }
    }
}
