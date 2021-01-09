using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Requests.Parameters
{
    /// <summary>
    ///     Parameters for <see cref="ITelegramBotClient.SendAnimationAsync" /> method.
    /// </summary>
    public class SendAnimationParameters : ParametersBase
    {
        /// <summary>
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        ///     Animation to send
        /// </summary>
        public InputOnlineFile Animation { get; set; }

        /// <summary>
        ///     Duration of sent animation in seconds
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        ///     Animation width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        ///     Animation height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        ///     Thumbnail of the file sent. The thumbnail should be in JPEG format and less than 200 kB
        ///     in size. A thumbnail's width and height should not exceed 90. Thumbnails can't be reused and can be only
        ///     uploaded as a new file.
        /// </summary>
        public InputMedia Thumb { get; set; }

        /// <summary>
        ///     Animation caption
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        ///     Change, if you want Telegram apps to show bold, italic, fixed-width text or inline
        ///     URLs in your bot's message.
        /// </summary>
        public ParseMode ParseMode { get; set; }

        /// <summary>
        ///     Sends the message silently. iOS users will not receive a notification,
        ///     Android users will receive a notification with no sound.
        /// </summary>
        public bool DisableNotification { get; set; }

        /// <summary>
        ///     If the message is a reply, ID of the original message
        /// </summary>
        public int ReplyToMessageId { get; set; }

        /// <summary>
        ///     Additional interface options. A JSON-serialized object for a custom reply keyboard,
        ///     instructions to hide keyboard or to force a reply from the user.
        /// </summary>
        public IReplyMarkup ReplyMarkup { get; set; }

        /// <summary>
        ///     Sets <see cref="ChatId" /> property.
        /// </summary>
        /// <param name="chatId"></param>
        public SendAnimationParameters WithChatId(ChatId chatId)
        {
            ChatId = chatId;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Animation" /> property.
        /// </summary>
        /// <param name="animation">Animation to send</param>
        public SendAnimationParameters WithAnimation(InputOnlineFile animation)
        {
            Animation = animation;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Duration" /> property.
        /// </summary>
        /// <param name="duration">Duration of sent animation in seconds</param>
        public SendAnimationParameters WithDuration(int duration)
        {
            Duration = duration;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Width" /> property.
        /// </summary>
        /// <param name="width">Animation width</param>
        public SendAnimationParameters WithWidth(int width)
        {
            Width = width;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Height" /> property.
        /// </summary>
        /// <param name="height">Animation height</param>
        public SendAnimationParameters WithHeight(int height)
        {
            Height = height;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Thumb" /> property.
        /// </summary>
        /// <param name="thumb">
        ///     Thumbnail of the file sent. The thumbnail should be in JPEG format and less than 200 kB
        ///     in size. A thumbnail's width and height should not exceed 90. Thumbnails can't be reused and can be only
        ///     uploaded as a new file.
        /// </param>
        public SendAnimationParameters WithThumb(InputMedia thumb)
        {
            Thumb = thumb;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="Caption" /> property.
        /// </summary>
        /// <param name="caption">Animation caption</param>
        public SendAnimationParameters WithCaption(string caption)
        {
            Caption = caption;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ParseMode" /> property.
        /// </summary>
        /// <param name="parseMode">
        ///     Change, if you want Telegram apps to show bold, italic, fixed-width text or inline
        ///     URLs in your bot's message.
        /// </param>
        public SendAnimationParameters WithParseMode(ParseMode parseMode)
        {
            ParseMode = parseMode;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="DisableNotification" /> property.
        /// </summary>
        /// <param name="disableNotification">
        ///     Sends the message silently. iOS users will not receive a notification,
        ///     Android users will receive a notification with no sound.
        /// </param>
        public SendAnimationParameters WithDisableNotification(bool disableNotification)
        {
            DisableNotification = disableNotification;
            return this;
        }

        /// <summary>
        ///     Sets <see cref="ReplyToMessageId" /> property.
        /// </summary>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        public SendAnimationParameters WithReplyToMessageId(int replyToMessageId)
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
        public SendAnimationParameters WithReplyMarkup(IReplyMarkup replyMarkup)
        {
            ReplyMarkup = replyMarkup;
            return this;
        }
    }
}