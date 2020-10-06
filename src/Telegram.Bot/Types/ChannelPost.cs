using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telegram.Bot.Types
{
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class ChannelPost
    {
        /// <summary>
        /// Unique message identifier
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public int MessageId { get; set; }

        /// <summary>
        /// Conversation the message belongs to
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public Chat Chat { get; set; }

        /// <summary>
        /// Date the message was sent
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Date { get; set; }

        /// <summary>
        /// Optional. For text messages, the actual UTF-8 text of the message
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Text { get; set; }

        /// <summary>
        /// Optional. Description is a sticker, information about the sticker
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Sticker Sticker { get; set; }

        /// <summary>
        /// Optional. Message is an animation, information about the animation. For backward compatibility, when this
        /// field is set, the document field will also be set
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Animation Animation { get; set; }

        /// <summary>
        /// Optional. Description is a general file, information about the file
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Document Document { get; set; }

        /// <summary>
        /// Optional. Message is a dice with random value from 1 to 6
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Dice Dice { get; set; }

        /// <summary>
        /// Optional. Bot through which the message was sent
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public User ViaBot { get; set; }

        /// <summary>
        /// Optional. Message is a native poll, information about the poll
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Poll Poll { get; set; }

        /// <summary>
        /// Optional. Description is a photo, available sizes of the photo
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public PhotoSize[] Photo { get; set; }

        /// <summary>
        /// Optional. Description is a video, information about the video
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Video Video { get; set; }

        /// <summary>
        /// Optional. Description is a <see cref="VideoNote"/>, information about the video message
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public VideoNote VideoNote { get; set; }

        /// <summary>
        /// Optional. Description is a shared location, information about the location
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Location Location { get; set; }

        /// <summary>
        /// Optional. Description is a venue, information about the venue
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Venue Venue { get; set; }

        /// <summary>
        /// Optional. Description is an audio file, information about the file
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Audio Audio { get; set; }

        /// <summary>
        /// Description is a voice message, information about the file
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Voice Voice { get; set; }

        /// <summary>
        /// Optional. Description is a shared contact, information about the contact
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Contact Contact { get; set; }

        /// <summary>
        /// Optional. For text messages, special entities like usernames, URLs, bot commands, etc. that appear in the text
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public MessageEntity[] Entities { get; set; }

        /// <summary>
        /// Gets the entity values.
        /// </summary>
        /// <value>
        /// The entity contents.
        /// </value>
        public IEnumerable<string> EntityValues =>
            Entities?.Select(entity => Text.Substring(entity.Offset, entity.Length));

    }
}
