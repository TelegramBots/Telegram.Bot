using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Telegram.Bot.Helpers;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Send video files, Telegram clients support mp4 videos
    /// </summary>
    public class SendVideoRequest : FileRequestBase<Message>,
                                    INotifiableMessage,
                                    IReplyMessage
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// Video file to send
        /// </summary>
        public FileToSend Video { get; set; }

        /// <summary>
        /// Duration of the video in seconds
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Duration { get; set; }

        /// <summary>
        /// Video width
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Width { get; set; }

        /// <summary>
        /// Video height
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Height { get; set; }

        /// <summary>
        /// Video caption (may also be used when resending videos by file_id), 0-200 characters
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Caption { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool DisableNotification { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ReplyToMessageId { get; set; }

        /// <summary>
        /// Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public SendVideoRequest()
            : base("sendVideo")
        { }

        /// <summary>
        /// Initializes a new request with chatId and video
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="video">Video to send</param>
        public SendVideoRequest(ChatId chatId, FileToSend video)
            : this()
        {
            ChatId = chatId;
            Video = video;
        }

        /// <summary>
        /// Generate content of HTTP message
        /// </summary>
        /// <param name="serializerSettings">JSON serialization setting</param>
        /// <returns>Content of HTTP request</returns>
        public override HttpContent ToHttpContent(JsonSerializerSettings serializerSettings)
        {
            HttpContent content;

            if (Video.Type == FileType.Stream)
            {
                var parameters = new Dictionary<string, object>
                {
                    { nameof(ChatId).ToSnakeCased(), ChatId},
                    { nameof(Video).ToSnakeCased(), Video },
                    { nameof(Caption).ToSnakeCased(), Caption },
                    { nameof(ReplyMarkup).ToSnakeCased(), ReplyMarkup }
                };

                if (Width != default)
                {
                    parameters.Add(nameof(Width).ToSnakeCased(), Width);
                }

                if (Height != default)
                {
                    parameters.Add(nameof(Height).ToSnakeCased(), Height);
                }

                if (Duration != default)
                {
                    parameters.Add(nameof(Duration).ToSnakeCased(), Duration);
                }

                if (ReplyToMessageId != default)
                {
                    parameters.Add(nameof(ReplyToMessageId).ToSnakeCased(), ReplyToMessageId);
                }

                if (DisableNotification != default)
                {
                    parameters.Add(nameof(DisableNotification).ToSnakeCased(), DisableNotification);
                }

                content = GetMultipartContent(parameters, serializerSettings);
            }
            else
            {
                content = base.ToHttpContent(serializerSettings);
            }

            return content;
        }
    }
}
