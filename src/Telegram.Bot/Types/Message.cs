using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [JsonProperty(Required = Required.Always)]
        public int MessageId { get; set; }

        /// <summary>
        /// Sender
        /// </summary>
        [JsonProperty]
        public User From { get; set; }

        /// <summary>
        /// Date the message was sent
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Conversation the message belongs to
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Chat Chat { get; set; }

        /// <summary>
        /// Optional. For forwarded messages, sender of the original message
        /// </summary>
        [JsonProperty]
        public User ForwardFrom { get; set; }

        /// <summary>
        /// Optional. For messages forwarded from a channel, information about the original channel
        /// </summary>
        [JsonProperty]
        public Chat ForwardFromChat { get; set; }

        /// <summary>
        /// Optional. For forwarded channel posts, identifier of the original message in the channel
        /// </summary>
        [JsonProperty]
        public int ForwardFromMessageId { get; set; }

        /// <summary>
        /// Optional. For messages forwarded from channels, signature of the post author if present
        /// </summary>
        [JsonProperty]
        public string ForwardSignature { get; set; }

        /// <summary>
        /// Optional. For forwarded messages, date the original message was sent in Unix time
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? ForwardDate { get; set; }

        /// <summary>
        /// Optional. For replies, the original message. Note that the Message object in this field will not contain further reply_to_message fields even if it itself is a reply.
        /// </summary>
        [JsonProperty]
        public Message ReplyToMessage { get; set; }

        /// <summary>
        /// Optional. Date the message was last edited in Unix time
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? EditDate { get; set; }

        /// <summary>
        /// Optional. Signature of the post author for messages in channels
        /// </summary>
        [JsonProperty]
        public string AuthorSignature { get; set; }

        /// <summary>
        /// Optional. For text messages, the actual UTF-8 text of the message
        /// </summary>
        [JsonProperty]
        public string Text { get; set; }

        /// <summary>
        /// Optional. For text messages, special entities like usernames, URLs, bot commands, etc. that appear in the text
        /// </summary>
        [JsonProperty]
        public List<MessageEntity> Entities { get; set; } = new List<MessageEntity>();

        /// <summary>
        /// Gets the entity values.
        /// </summary>
        /// <value>
        /// The entity contents.
        /// </value>
        public List<string> EntityValues
            => Entities.Select(entity => Text.Substring(entity.Offset, entity.Length)).ToList();

        /// <summary>
        /// Optional. For messages with a caption, special entities like usernames, URLs, bot commands, etc. that appear in the caption
        /// </summary>
        [JsonProperty]
        public List<MessageEntity> CaptionEntities { get; set; } = new List<MessageEntity>();

        /// <summary>
        /// Gets the caption entity values.
        /// </summary>
        /// <value>
        /// The caption entity contents.
        /// </value>
        public IEnumerable<string> CaptionEntityValues => CaptionEntities
            .Select(entity => Caption.Substring(entity.Offset, entity.Length));

        /// <summary>
        /// Optional. Message is an audio file, information about the file
        /// </summary>
        [JsonProperty]
        public Audio Audio { get; set; }

        /// <summary>
        /// Optional. Message is a general file, information about the file
        /// </summary>
        [JsonProperty]
        public Document Document { get; set; }

        /// <summary>
        /// Message is a game, information about the game.
        /// </summary>
        [JsonProperty]
        public Game Game { get; set; }

        /// <summary>
        /// Optional. Message is a photo, available sizes of the photo
        /// </summary>
        [JsonProperty]
        public PhotoSize[] Photo { get; set; }

        /// <summary>
        /// Optional. Message is a sticker, information about the sticker
        /// </summary>
        [JsonProperty]
        public Sticker Sticker { get; set; }

        /// <summary>
        /// Optional. Message is a video, information about the video
        /// </summary>
        [JsonProperty]
        public Video Video { get; set; }

        /// <summary>
        /// Message is a voice message, information about the file
        /// </summary>
        [JsonProperty]
        public Voice Voice { get; set; }

        /// <summary>
        /// Optional. Message is a <see cref="VideoNote"/>, information about the video message
        /// </summary>
        [JsonProperty]
        public VideoNote VideoNote { get; set; }

        /// <summary>
        /// Caption for the photo or video
        /// </summary>
        [JsonProperty]
        public string Caption { get; set; }

        /// <summary>
        /// Optional. Message is a shared contact, information about the contact
        /// </summary>
        [JsonProperty]
        public Contact Contact { get; set; }

        /// <summary>
        /// Optional. Message is a shared location, information about the location
        /// </summary>
        [JsonProperty]
        public Location Location { get; set; }

        /// <summary>
        /// Optional. Message is a venue, information about the venue
        /// </summary>
        [JsonProperty]
        public Venue Venue { get; set; }

        /// <summary>
        /// Optional. A new member was added to the group, information about them (this member may be bot itself)
        /// </summary>
        [JsonProperty]
        public User NewChatMember { get; set; }

        /// <summary>
        /// Optional. New members that were added to the group or supergroup and information about them (the bot itself may be one of these members)
        /// </summary>
        [JsonProperty]
        public User[] NewChatMembers { get; set; }

        /// <summary>
        /// Optional. A member was removed from the group, information about them (this member may be bot itself)
        /// </summary>
        [JsonProperty]
        public User LeftChatMember { get; set; }

        /// <summary>
        /// Optional. A group title was changed to this value
        /// </summary>
        [JsonProperty]
        public string NewChatTitle { get; set; }

        /// <summary>
        /// Optional. A group photo was change to this value
        /// </summary>
        [JsonProperty]
        public PhotoSize[] NewChatPhoto { get; set; }

        /// <summary>
        /// Optional. Informs that the group photo was deleted
        /// </summary>
        [JsonProperty]
        public bool DeleteChatPhoto { get; set; }

        /// <summary>
        /// Optional. Informs that the group has been created
        /// </summary>
        [JsonProperty]
        public bool GroupChatCreated { get; set; }

        /// <summary>
        /// Optional. Service message: the supergroup has been created
        /// </summary>
        [JsonProperty]
        public bool SupergroupChatCreated { get; set; }

        /// <summary>
        /// Optional. Service message: the channel has been created
        /// </summary>
        [JsonProperty]
        public bool ChannelChatCreated { get; set; }

        /// <summary>
        /// Optional. The group has been migrated to a supergroup with the specified identifier
        /// </summary>
        [JsonProperty]
        public long MigrateToChatId { get; set; }

        /// <summary>
        /// Optional. The supergroup has been migrated from a group with the specified identifier
        /// </summary>
        [JsonProperty]
        public long MigrateFromChatId { get; set; }

        /// <summary>
        /// Optional. Specified message was pinned. Note that the Message object in this field will not contain further reply_to_message fields even if it is itself a reply
        /// </summary>
        [JsonProperty]
        public Message PinnedMessage { get; set; }

        /// <summary>
        /// Optional. Message is an invoice for a payment
        /// </summary>
        [JsonProperty]
        public Invoice Invoice { get; set; }

        /// <summary>
        /// Optional. Message is a service message about a successful payment
        /// </summary>
        [JsonProperty]
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
                    MigrateFromChatId == default ||
                    MigrateToChatId == default)
                    return MessageType.ServiceMessage;

                return MessageType.UnknownMessage;
            }
        }
    }
}
