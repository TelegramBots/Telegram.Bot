using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SendVoiceAsync" /> method.
    /// </summary>
    public class SendVoiceParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Audio file to send.
        /// </summary>
        public InputOnlineFile Voice { get; set; }

        /// <summary>
        ///     Voice message caption, 0-1024 characters
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        ///     Change, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your bot's message.
        /// </summary>
        public ParseMode ParseMode { get; set; }

        /// <summary>
        ///     Duration of sent audio in seconds
        /// </summary>
        public int Duration { get; set; }

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
        ///     Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard
        ///     or to force a reply from the user.
        /// </summary>
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId"></param>
        public SendVoiceParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Voice" /> property.
        /// </summary>
        /// <param name="voice">Audio file to send.</param>
        public SendVoiceParameters WithVoice(InputOnlineFile voice)
        {
            Voice = voice;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Caption" /> property.
        /// </summary>
        /// <param name="caption">Voice message caption, 0-1024 characters</param>
        public SendVoiceParameters WithCaption(string caption)
        {
            Caption = caption;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ParseMode" /> property.
        /// </summary>
        /// <param name="parseMode">
        ///     Change, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your
        ///     bot's message.
        /// </param>
        public SendVoiceParameters WithParseMode(ParseMode parseMode)
        {
            ParseMode = parseMode;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Duration" /> property.
        /// </summary>
        /// <param name="duration">Duration of sent audio in seconds</param>
        public SendVoiceParameters WithDuration(int duration)
        {
            Duration = duration;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="DisableNotification" /> property.
        /// </summary>
        /// <param name="disableNotification">
        ///     Sends the message silently. iOS users will not receive a notification, Android users
        ///     will receive a notification with no sound.
        /// </param>
        public SendVoiceParameters WithDisableNotification(bool disableNotification)
        {
            DisableNotification = disableNotification;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyToMessageId" /> property.
        /// </summary>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        public SendVoiceParameters WithReplyToMessageId(int replyToMessageId)
        {
            ReplyToMessageId = replyToMessageId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyMarkup" /> property.
        /// </summary>
        /// <param name="replyMarkup">
        ///     Additional interface options. A JSON-serialized object for a custom reply keyboard,
        ///     instructions to hide keyboard or to force a reply from the user.
        /// </param>
        public SendVoiceParameters WithReplyMarkup(IReplyMarkup replyMarkup)
        {
            ReplyMarkup = replyMarkup;
            return this;
        }
    }
}
