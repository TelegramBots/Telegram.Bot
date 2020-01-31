using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
// ReSharper disable CheckNamespace

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Send a native poll. A native poll can't be sent to a private chat.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public class SendPollRequest : RequestBase<Message>,
                                   INotifiableMessage,
                                   IReplyMessage,
                                   IReplyMarkupMessage<IReplyMarkup>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public ChatId ChatId { get; }

        /// <summary>
        /// Poll question, 1-255 characters
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Question { get; }

        /// <summary>
        /// List of answer options, 2-10 strings 1-100 characters each
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public IEnumerable<string> Options { get; }

        /// <summary>
        /// Optional. True, if the poll needs to be anonymous, defaults to True
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? IsAnonymous { get; set; }

        /// <summary>
        /// Optional. Poll type, <see cref="PollType.Quiz"/> or <see cref="PollType.Regular"/>, defaults to <see cref="PollType.Regular"/>
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public PollType? Type { get; set; }

        /// <summary>
        /// Optional. True, if the poll allows multiple answers, ignored for polls in quiz mode, defaults to False
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? AllowsMultipleAnswers { get; set; }

        /// <summary>
        /// Optional. 0-based identifier of the correct answer option, required for polls in quiz mode
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? CorrectOptionId { get; set; }

        /// <summary>
        /// Optional. Pass True, if the poll needs to be immediately closed
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? IsClosed { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool DisableNotification { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int ReplyToMessageId { get; set; }

        /// <inheritdoc />
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        /// Initializes a new request with chatId, question and <see cref="PollOption"/>
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="question">Poll question, 1-255 characters</param>
        /// <param name="options">List of answer options, 2-10 strings 1-100 characters each</param>
        public SendPollRequest(ChatId chatId, string question, IEnumerable<string> options)
            : base("sendPoll")
        {
            ChatId = chatId;
            Question = question;
            Options = options;
        }
    }
}
