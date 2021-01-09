using System;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SendPollAsync" /> method.
    /// </summary>
    public class SendPollParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Poll question, 1-255 characters
        /// </summary>
        public string Question { get; set; }

        /// <summary>
        ///     List of answer options, 2-10 strings 1-100 characters each
        /// </summary>
        public IEnumerable<string> Options { get; set; }

        /// <summary>
        ///     Sends the message silently. iOS users will not receive a notification, Android users will receive a notification
        ///     with no sound.
        /// </summary>
        public bool DisableNotification { get; set; }

        /// <summary>
        ///     If the message is a reply, ID of the original message
        /// </summary>
        public int ReplyToMessageId { get; set; }

        /// <summary>
        ///     Additional interface options. A JSON-serialized object for an inline keyboard, custom reply keyboard, instructions
        ///     to hide keyboard or to force a reply from the user.
        /// </summary>
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        ///     True, if the poll needs to be anonymous, defaults to True
        /// </summary>
        public bool? IsAnonymous { get; set; }

        /// <summary>
        ///     Poll type, “quiz” or “regular”, defaults to “regular”
        /// </summary>
        public PollType? Type { get; set; }

        /// <summary>
        ///     True, if the poll allows multiple answers, ignored for polls in quiz mode, defaults to False
        /// </summary>
        public bool? AllowsMultipleAnswers { get; set; }

        /// <summary>
        ///     0-based identifier of the correct answer option, required for polls in quiz mode
        /// </summary>
        public int? CorrectOptionId { get; set; }

        /// <summary>
        ///     Pass True, if the poll needs to be immediately closed
        /// </summary>
        public bool? IsClosed { get; set; }

        /// <summary>
        ///     Text that is shown when a user chooses an incorrect answer or taps on the lamp icon in a quiz-style poll
        /// </summary>
        public string Explanation { get; set; }

        /// <summary>
        ///     Mode for parsing entities in the explanation
        /// </summary>
        public ParseMode ExplanationParseMode { get; set; }

        /// <summary>
        ///     Amount of time in seconds the poll will be active after creation
        /// </summary>
        public int? OpenPeriod { get; set; }

        /// <summary>
        ///     Point in time when the poll will be automatically closed
        /// </summary>
        public DateTime? CloseDate { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId"></param>
        public SendPollParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Question" /> property.
        /// </summary>
        /// <param name="question">Poll question, 1-255 characters</param>
        public SendPollParameters WithQuestion(string question)
        {
            Question = question;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Options" /> property.
        /// </summary>
        /// <param name="options">List of answer options, 2-10 strings 1-100 characters each</param>
        public SendPollParameters WithOptions(IEnumerable<string> options)
        {
            Options = options;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="DisableNotification" /> property.
        /// </summary>
        /// <param name="disableNotification">
        ///     Sends the message silently. iOS users will not receive a notification, Android users
        ///     will receive a notification with no sound.
        /// </param>
        public SendPollParameters WithDisableNotification(bool disableNotification)
        {
            DisableNotification = disableNotification;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyToMessageId" /> property.
        /// </summary>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        public SendPollParameters WithReplyToMessageId(int replyToMessageId)
        {
            ReplyToMessageId = replyToMessageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyMarkup" /> property.
        /// </summary>
        /// <param name="replyMarkup">
        ///     Additional interface options. A JSON-serialized object for an inline keyboard, custom reply
        ///     keyboard, instructions to hide keyboard or to force a reply from the user.
        /// </param>
        public SendPollParameters WithReplyMarkup(IReplyMarkup replyMarkup)
        {
            ReplyMarkup = replyMarkup;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="IsAnonymous" /> property.
        /// </summary>
        /// <param name="isAnonymous">True, if the poll needs to be anonymous, defaults to True</param>
        public SendPollParameters WithIsAnonymous(bool? isAnonymous)
        {
            IsAnonymous = isAnonymous;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Type" /> property.
        /// </summary>
        /// <param name="type">Poll type, “quiz” or “regular”, defaults to “regular”</param>
        public SendPollParameters WithType(PollType? type)
        {
            Type = type;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="AllowsMultipleAnswers" /> property.
        /// </summary>
        /// <param name="allowsMultipleAnswers">
        ///     True, if the poll allows multiple answers, ignored for polls in quiz mode, defaults
        ///     to False
        /// </param>
        public SendPollParameters WithAllowsMultipleAnswers(bool? allowsMultipleAnswers)
        {
            AllowsMultipleAnswers = allowsMultipleAnswers;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="CorrectOptionId" /> property.
        /// </summary>
        /// <param name="correctOptionId">0-based identifier of the correct answer option, required for polls in quiz mode</param>
        public SendPollParameters WithCorrectOptionId(int? correctOptionId)
        {
            CorrectOptionId = correctOptionId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="IsClosed" /> property.
        /// </summary>
        /// <param name="isClosed">Pass True, if the poll needs to be immediately closed</param>
        public SendPollParameters WithIsClosed(bool? isClosed)
        {
            IsClosed = isClosed;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Explanation" /> property.
        /// </summary>
        /// <param name="explanation">
        ///     Text that is shown when a user chooses an incorrect answer or taps on the lamp icon in a
        ///     quiz-style poll
        /// </param>
        public SendPollParameters WithExplanation(string explanation)
        {
            Explanation = explanation;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ExplanationParseMode" /> property.
        /// </summary>
        /// <param name="explanationParseMode">Mode for parsing entities in the explanation</param>
        public SendPollParameters WithExplanationParseMode(ParseMode explanationParseMode)
        {
            ExplanationParseMode = explanationParseMode;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="OpenPeriod" /> property.
        /// </summary>
        /// <param name="openPeriod">Amount of time in seconds the poll will be active after creation</param>
        public SendPollParameters WithOpenPeriod(int? openPeriod)
        {
            OpenPeriod = openPeriod;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="CloseDate" /> property.
        /// </summary>
        /// <param name="closeDate">Point in time when the poll will be automatically closed</param>
        public SendPollParameters WithCloseDate(DateTime? closeDate)
        {
            CloseDate = closeDate;
            return this;
        }
    }
}
