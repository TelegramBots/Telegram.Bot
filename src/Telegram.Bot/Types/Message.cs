using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Telegram.Bot.Converters;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;

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
        [JsonProperty("message_id", Required = Required.Always)]
        public int MessageId { get; set; }

        /// <summary>
        /// Sender
        /// </summary>
        [JsonProperty("from", Required = Required.Default)]
        public User From { get; set; }

        /// <summary>
        /// Date the message was sent
        /// </summary>
        [JsonProperty("date", Required = Required.Always)]
        [JsonConverter(typeof (UnixDateTimeConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Conversation the message belongs to
        /// </summary>
        [JsonProperty("chat", Required = Required.Always)]
        public Chat Chat { get; set; }

        /// <summary>
        /// Optional. For forwarded messages, sender of the original message
        /// </summary>
        [JsonProperty("forward_from", Required = Required.Default)]
        public User ForwardFrom { get; set; }

        /// <summary>
        /// Optional. For messages forwarded from a channel, information about the original channel
        /// </summary>
        [JsonProperty("forward_from_chat", Required = Required.Default)]
        public Chat ForwardFromChat { get; set; }

        /// <summary>
        /// Optional. For forwarded channel posts, identifier of the original message in the channel
        /// </summary>
        [JsonProperty("forward_from_message_id", Required = Required.Default)]
        public int ForwardFromMessageId { get; set; }

        /// <summary>
        /// Optional. For forwarded messages, date the original message was sent in Unix time
        /// </summary>
        [JsonProperty("forward_date", Required = Required.Default)]
        [JsonConverter(typeof (UnixDateTimeConverter))]
        public DateTime? ForwardDate { get; set; }

        /// <summary>
        /// Optional. For replies, the original message. Note that the Message object in this field will not contain further reply_to_message fields even if it itself is a reply.
        /// </summary>
        [JsonProperty("reply_to_message", Required = Required.Default)]
        public Message ReplyToMessage { get; set; }

        /// <summary>
        /// Optional. Date the message was last edited in Unix time
        /// </summary>
        [JsonProperty("edit_date", Required = Required.Default)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? EditDate { get; set; }

        /// <summary>
        /// Optional. For text messages, the actual UTF-8 text of the message
        /// </summary>
        [JsonProperty("text", Required = Required.Default)]
        public string Text { get; set; }

        /// <summary>
        /// Optional. For text messages, special entities like usernames, URLs, bot commands, etc. that appear in the text
        /// </summary>
        [JsonProperty("entities", Required = Required.Default)]
        public List<MessageEntity> Entities { get; set; } = new List<MessageEntity>();

        /// <summary>
        /// Gets the entity values.
        /// </summary>
        /// <value>
        /// The entity contents.
        /// </value>
        [JsonIgnore]
        public List<string> EntityValues
            => Entities.ToList().Select(entity => Text.Substring(entity.Offset, entity.Length)).ToList();

        /// <summary>
        /// Optional. Message is an audio file, information about the file
        /// </summary>
        [JsonProperty("audio", Required = Required.Default)]
        public Audio Audio { get; set; }

        /// <summary>
        /// Optional. Message is a general file, information about the file
        /// </summary>
        [JsonProperty("document", Required = Required.Default)]
        public Document Document { get; set; }

        /// <summary>
        /// Message is a game, information about the game.
        /// </summary>
        [JsonProperty("game", Required = Required.Default)]
        public Game Game { get; set; }

        /// <summary>
        /// Optional. Message is a photo, available sizes of the photo
        /// </summary>
        [JsonProperty("photo", Required = Required.Default)]
        public PhotoSize[] Photo { get; set; }

        /// <summary>
        /// Optional. Message is a sticker, information about the sticker
        /// </summary>
        [JsonProperty("sticker", Required = Required.Default)]
        public Sticker Sticker { get; set; }

        /// <summary>
        /// Optional. Message is a video, information about the video
        /// </summary>
        [JsonProperty("video", Required = Required.Default)]
        public Video Video { get; set; }

        /// <summary>
        /// Message is a voice message, information about the file
        /// </summary>
        [JsonProperty("voice", Required = Required.Default)]
        public Voice Voice { get; set; }

        /// <summary>
        /// Optional. Message is a <see cref="VideoNote"/>, information about the video message
        /// </summary>
        [JsonProperty("video_note")]
        public VideoNote VideoNote { get; set; }

        /// <summary>
        /// Caption for the photo or video
        /// </summary>
        [JsonProperty("caption", Required = Required.Default)]
        public string Caption { get; set; }

        /// <summary>
        /// Optional. Message is a shared contact, information about the contact
        /// </summary>
        [JsonProperty("contact", Required = Required.Default)]
        public Contact Contact { get; set; }

        /// <summary>
        /// Optional. Message is a shared location, information about the location
        /// </summary>
        [JsonProperty("location", Required = Required.Default)]
        public Location Location { get; set; }

        /// <summary>
        /// Optional. Message is a venue, information about the venue
        /// </summary>
        [JsonProperty("venue", Required = Required.Default)]
        public Venue Venue { get; set; }

        /// <summary>
        /// Optional. A new member was added to the group, information about them (this member may be bot itself)
        /// </summary>
        [Obsolete("Use the NewChatMembers property")]
        [JsonProperty("new_chat_member", Required = Required.Default)]
        public User NewChatMember { get; set; }

        /// <summary>
        /// Optional. New members that were added to the group or supergroup and information about them (the bot itself may be one of these members)
        /// </summary>
        [JsonProperty("new_chat_members", Required = Required.Default)]
        public User[] NewChatMembers { get; set; }

        /// <summary>
        /// Optional. A member was removed from the group, information about them (this member may be bot itself)
        /// </summary>
        [JsonProperty("left_chat_member", Required = Required.Default)]
        public User LeftChatMember { get; set; }

        /// <summary>
        /// Optional. A group title was changed to this value
        /// </summary>
        [JsonProperty("new_chat_title", Required = Required.Default)]
        public string NewChatTitle { get; set; }

        /// <summary>
        /// Optional. A group photo was change to this value
        /// </summary>
        [JsonProperty("new_chat_photo", Required = Required.Default)]
        public PhotoSize[] NewChatPhoto { get; set; }

        /// <summary>
        /// Optional. Informs that the group photo was deleted
        /// </summary>
        [JsonProperty("delete_chat_photo", Required = Required.Default)]
        public bool DeleteChatPhoto { get; set; }

        /// <summary>
        /// Optional. Informs that the group has been created
        /// </summary>
        [JsonProperty("group_chat_created", Required = Required.Default)]
        public bool GroupChatCreated { get; set; }

        /// <summary>
        /// Optional. Service message: the supergroup has been created
        /// </summary>
        [JsonProperty("supergroup_chat_created", Required = Required.Default)]
        public bool SupergroupChatCreated { get; set; }

        /// <summary>
        /// Optional. Service message: the channel has been created
        /// </summary>
        [JsonProperty("channel_chat_created", Required = Required.Default)]
        public bool ChannelChatCreated { get; set; }

        /// <summary>
        /// Optional. The group has been migrated to a supergroup with the specified identifier
        /// </summary>
        [JsonProperty("migrate_to_chat_id", Required = Required.Default)]
        public long MigrateToChatId { get; set; }

        /// <summary>
        /// Optional. The supergroup has been migrated from a group with the specified identifier
        /// </summary>
        [JsonProperty("migrate_from_chat_id", Required = Required.Default)]
        public long MigrateFromChatId { get; set; }

        /// <summary>
        /// Optional. Specified message was pinned. Note that the Message object in this field will not contain further reply_to_message fields even if it is itself a reply
        /// </summary>
        [JsonProperty("pinned_message", Required = Required.Default)]
        public Message PinnedMessage { get; set; }

        /// <summary>
        /// Optional. Message is an invoice for a payment
        /// </summary>
        [JsonProperty("invoice")]
        public Invoice Invoice { get; set; }

        /// <summary>
        /// Optional. Message is a service message about a successful payment
        /// </summary>
        [JsonProperty("successful_payment")]
        public SuccessfulPayment SuccessfulPayment { get; set; }

        /// <summary>
        /// Gets the <see cref="MessageType"/> of the <see cref="Message"/>
        /// </summary>
        /// <value>
        /// The <see cref="MessageType"/> of the <see cref="Message"/>
        /// </value>
        public MessageType Type
        {
            get
            {
                if (Audio != null)
                    return MessageType.AudioMessage;

                if (Document != null)
                    return MessageType.DocumentMessage;

                if (Game != null)
                    return MessageType.GameMessage;

                if (Photo != null)
                    return MessageType.PhotoMessage;

                if (Sticker != null)
                    return MessageType.StickerMessage;

                if (Video != null)
                    return MessageType.VideoMessage;

                if (Voice != null)
                    return MessageType.VoiceMessage;

                if (Contact != null)
                    return MessageType.ContactMessage;

                if (Venue != null)
                    return MessageType.VenueMessage;

                if (Location != null)
                    return MessageType.LocationMessage;

                if (Text != null)
                    return MessageType.TextMessage;

                if (Invoice != null)
                    return MessageType.Invoice;

                if (SuccessfulPayment != null)
                    return MessageType.SuccessfulPayment;

                if (VideoNote != null)
                    return MessageType.VideoNoteMessage;

                if (NewChatMember != null ||
                    (NewChatMembers != null && NewChatMembers.Length > 0) ||
                    LeftChatMember != null ||
                    NewChatTitle != null ||
                    NewChatPhoto != null ||
                    PinnedMessage != null ||
                    DeleteChatPhoto ||
                    GroupChatCreated ||
                    SupergroupChatCreated ||
                    ChannelChatCreated ||
                    MigrateFromChatId == default(long) ||
                    MigrateToChatId == default(long))
                    return MessageType.ServiceMessage;

                return MessageType.UnknownMessage;
            }
        }
    }
}
