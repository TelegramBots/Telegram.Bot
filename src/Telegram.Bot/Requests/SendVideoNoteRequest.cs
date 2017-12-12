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
    /// Send rounded video messages
    /// </summary>
    public class SendVideoNoteRequest : FileRequestBase<Message>,
                                        INotifiableMessage,
                                        IReplyMessage
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// Video note to send
        /// </summary>
        public FileToSend VideoNote { get; set; }

        /// <summary>
        /// Duration of sent video in seconds
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Duration { get; set; }

        /// <summary>
        /// Video width and height
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int Length { get; set; }

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
        public SendVideoNoteRequest()
            : base("sendVideoNote")
        { }

        /// <summary>
        /// Initializes a new request with chatId and video note
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="videoNote">Video note to send</param>
        public SendVideoNoteRequest(ChatId chatId, FileToSend videoNote)
            : this()
        {
            ChatId = chatId;
            VideoNote = videoNote;
        }

        /// <summary>
        /// Generate content of HTTP message
        /// </summary>
        /// <param name="serializerSettings">JSON serialization setting</param>
        /// <returns>Content of HTTP request</returns>
        public override HttpContent ToHttpContent(JsonSerializerSettings serializerSettings)
        {
            HttpContent content;

            if (VideoNote.Type == FileType.Stream)
            {
                var parameters = new Dictionary<string, object>
                {
                    { nameof(ChatId).ToSnakeCased(), ChatId},
                    { nameof(VideoNote).ToSnakeCased(), VideoNote},
                    { nameof(ReplyMarkup).ToSnakeCased(), ReplyMarkup }
                };

                if (Duration != default)
                {
                    parameters.Add(nameof(Duration).ToSnakeCased(), Duration);
                }

                if (Length != default)
                {
                    parameters.Add(nameof(Length).ToSnakeCased(), Length);
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
