using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;
using File = Telegram.Bot.Types.File;

namespace Telegram.Bot // Intentionally not in Telegram.Bot.Helpers
{
    /// <summary>
    /// Adds a lot of helper methods
    /// </summary>
    public static class TelegramBotClientExtensions
    {
        #region File downloading

        /// <summary>
        /// Use this method to get information about a file. For the moment, bots can download files of up to 20MB in size.
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="fileBase">File identifier</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The File object</returns>
        /// <see href="https://core.telegram.org/bots/api#getfile"/>
        public static Task<File> GetFileAsync(
            this ITelegramBotClient bot,
            FileBase fileBase,
            CancellationToken cancellationToken = default
        ) =>
            bot.GetFileAsync(fileBase.FileId, cancellationToken);

        /// <summary>
        /// Use this method to download a file. Get <paramref name="filePath"/> by calling GetFileAsync
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="filePath">Path to file on server</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>File bytes</returns>
        public static async Task<byte[]> DownloadFileAsync(
            this ITelegramBotClient bot,
            string filePath,
            CancellationToken cancellationToken = default
        )
        {
            using (MemoryStream ms = new MemoryStream())
            {
                await bot.DownloadFileAsync(filePath, ms, cancellationToken)
                    .ConfigureAwait(false);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Use this method to download a file. Get <paramref name="file"/> by calling GetFileAsync
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="file">File on server</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>File bytes</returns>
        public static async Task<byte[]> DownloadFileAsync(
            this ITelegramBotClient bot,
            File file,
            CancellationToken cancellationToken = default
        )
        {
            using (MemoryStream ms = file.FileSize == default
                ? new MemoryStream()
                : new MemoryStream(file.FileSize))
            {
                await bot.DownloadFileAsync(file.FilePath, ms, cancellationToken)
                    .ConfigureAwait(false);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Use this method to get basic info about a file and download it.
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="fileBase">File identifier to get info about</param>
        /// <param name="destination">Destination stream to write file to</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>File info</returns>
        public static async Task<File> GetInfoAndDownloadFileAsync(
            this ITelegramBotClient bot,
            FileBase fileBase,
            Stream destination,
            CancellationToken cancellationToken = default
        ) =>
            await bot.GetInfoAndDownloadFileAsync(fileBase.FileId, destination, cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Use this method to get basic info about a file and download it.
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="fileId">File identifier</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>File bytes</returns>
        public static async Task<byte[]> GetInfoAndDownloadFileAsync(
            this ITelegramBotClient bot,
            string fileId,
            CancellationToken cancellationToken = default
        )
        {
            var file = await bot.GetFileAsync(fileId, cancellationToken)
                .ConfigureAwait(false);

            using (MemoryStream ms = file.FileSize == default
                ? new MemoryStream()
                : new MemoryStream(file.FileSize))
            {
                await bot.DownloadFileAsync(file.FilePath, ms, cancellationToken)
                    .ConfigureAwait(false);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Use this method to get basic info about a file and download it.
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="fileBase">File identifier</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>File bytes</returns>
        public static async Task<byte[]> GetInfoAndDownloadFileAsync(
            this ITelegramBotClient bot,
            FileBase fileBase,
            CancellationToken cancellationToken = default
        ) =>
            await GetInfoAndDownloadFileAsync(bot, fileBase.FileId, cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Use this method to get information about a file. For the moment, bots can download files of up to 20MB in size.
        /// </summary>
        /// <param name="fileBase">File identifier</param>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>The File object</returns>
        /// <see href="https://core.telegram.org/bots/api#getfile"/>
        public static Task<File> GetFileAsync(
            this FileBase fileBase,
            ITelegramBotClient bot,
            CancellationToken cancellationToken = default
        ) =>
            bot.GetFileAsync(fileBase.FileId, cancellationToken);

        /// <summary>
        /// Use this method to get basic info about a file and download it.
        /// </summary>
        /// <param name="fileBase">File identifier to get info about</param>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="destination">Destination stream to write file to</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>File info</returns>
        public static async Task<File> GetInfoAndDownloadFileAsync(
            this FileBase fileBase,
            ITelegramBotClient bot,
            Stream destination,
            CancellationToken cancellationToken = default
        ) =>
            await bot.GetInfoAndDownloadFileAsync(fileBase.FileId, destination, cancellationToken)
                .ConfigureAwait(false);

        /// <summary>
        /// Use this method to get basic info about a file and download it.
        /// </summary>
        /// <param name="fileBase">File identifier</param>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>File bytes</returns>
        public static async Task<byte[]> GetInfoAndDownloadFileAsync(
            this FileBase fileBase,
            ITelegramBotClient bot,
            CancellationToken cancellationToken = default
        ) =>
            await GetInfoAndDownloadFileAsync(bot, fileBase.FileId, cancellationToken)
                .ConfigureAwait(false);

        #endregion File downloading

        #region ChatId, MessageId => Message

        /// <summary>
        /// Use this method to forward messages of any kind. On success, the sent Description is returned.
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="chatId"><see cref="ChatId"/> for the target chat</param>
        /// <param name="messageToForward"><see cref="Message"/> to forward</param>
        /// <param name="disableNotification">Sends the message silently. iOS users will not receive a notification, Android users will receive a notification with no sound.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the sent Description is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#forwardmessage"/>
        public static Task<Message> ForwardMessageAsync(
            this ITelegramBotClient bot,
            ChatId chatId,
            Message messageToForward,
            bool disableNotification = default,
            CancellationToken cancellationToken = default
        ) =>
            bot.ForwardMessageAsync(chatId, messageToForward.Chat, messageToForward.MessageId, disableNotification, cancellationToken);

        /// <summary>
        /// Use this method to stop updating a live location message sent by the bot before live_period expires.
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="message">Sent <see cref="Message"/></param>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success the sent <see cref="Message"/> is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#stopmessagelivelocation"/>
        public static Task<Message> StopMessageLiveLocationAsync(
            this ITelegramBotClient bot,
            Message message,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            bot.StopMessageLiveLocationAsync(message.Chat, message.MessageId, replyMarkup, cancellationToken);

        /// <summary>
        /// Use this method to edit text messages sent by the bot or via the bot (for inline bots).
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="message"><see cref="Message"/> to edit</param>
        /// <param name="text">New text of the message</param>
        /// <param name="parseMode">Send Markdown or HTML, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in your bot's message.</param>
        /// <param name="disableWebPagePreview">Disables link previews for links in this message</param>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the edited Description is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#editmessagetext"/>
        public static Task<Message> EditMessageTextAsync(
            this ITelegramBotClient bot,
            Message message,
            string text,
            ParseMode parseMode = default,
            bool disableWebPagePreview = default,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            bot.EditMessageTextAsync(message.Chat, message.MessageId, text, parseMode, disableWebPagePreview, replyMarkup, cancellationToken);

        /// <summary>
        /// Use this method to edit captions of messages sent by the bot or via the bot (for inline bots).
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="message"><see cref="Message"/> to edit</param>
        /// <param name="caption">New caption of the message</param>
        /// <param name="parseMode">Send Markdown or HTML, if you want Telegram apps to show bold, italic, fixed-width text or inline URLs in the media caption.</param>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the edited Description is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#editmessagecaption"/>
        public static Task<Message> EditMessageCaptionAsync(
            this ITelegramBotClient bot,
            Message message,
            string caption,
            ParseMode parseMode = default,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            bot.EditMessageCaptionAsync(message.Chat, message.MessageId, caption, replyMarkup, cancellationToken, parseMode);

        /// <summary>
        /// Use this method to edit audio, document, photo, or video messages.
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="message"><see cref="Message"/> to edit</param>
        /// <param name="media">A JSON-serialized object for a new media content of the message</param>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success the edited <see cref="Message"/> is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#editmessagemedia"/>
        public static Task<Message> EditMessageMediaAsync(
            this ITelegramBotClient bot,
            Message message,
            InputMediaBase media,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            bot.EditMessageMediaAsync(message.Chat, message.MessageId, media, replyMarkup, cancellationToken);

        /// <summary>
        /// Use this method to edit only the reply markup of messages sent by the bot or via the bot (for inline bots).
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="message"><see cref="Message"/> to edit</param>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, the edited Description is returned.</returns>
        public static Task<Message> EditMessageReplyMarkupAsync(
            this ITelegramBotClient bot,
            Message message,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            bot.EditMessageReplyMarkupAsync(message.Chat, message.MessageId, replyMarkup, cancellationToken);

        /// <summary>
        /// Use this method to edit live location messages sent by the bot.
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="message"><see cref="Message"/> to edit</param>
        /// <param name="latitude">Latitude of location</param>
        /// <param name="longitude">Longitude of location</param>
        /// <param name="replyMarkup">A JSON-serialized object for an inline keyboard.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success the edited <see cref="Message"/> is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#editmessagelivelocation"/>
        public static Task<Message> EditMessageLiveLocationAsync(
            this ITelegramBotClient bot,
            Message message,
            float latitude,
            float longitude,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default
        ) =>
            bot.EditMessageLiveLocationAsync(message.Chat, message.MessageId, latitude, longitude, replyMarkup, cancellationToken);

        /// <summary>
        /// Use this method to delete a message. A message can only be deleted if it was sent less than 48 hours ago. Any such recently sent outgoing message may be deleted. Additionally, if the bot is an administrator in a group chat, it can delete any message. If the bot is an administrator in a supergroup, it can delete messages from any other user and service messages about people joining or leaving the group (other types of service messages may only be removed by the group creator). In channels, bots can only remove their own messages.
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="message"><see cref="Message"/> to delete</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns><c>true</c> on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#deletemessage"/>
        public static Task DeleteMessageAsync(
            this ITelegramBotClient bot,
            Message message,
            CancellationToken cancellationToken = default
        ) =>
            bot.DeleteMessageAsync(message.Chat, message.MessageId, cancellationToken);

        /// <summary>
        /// Use this method to pin a message in a supergroup. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="message"><see cref="Message"/> to pin</param>
        /// <param name="disableNotification">Pass True, if it is not necessary to send a notification to all group members about the new pinned message</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns true on success.</returns>
        /// <see href="https://core.telegram.org/bots/api#pinchatmessage"/>
        public static Task PinChatMessageAsync(
            this ITelegramBotClient bot,
            Message message,
            bool disableNotification = default,
            CancellationToken cancellationToken = default
        ) =>
            bot.PinChatMessageAsync(message.Chat, message.MessageId, disableNotification, cancellationToken);

        #endregion ChatId, MessageId => Message

        #region AnswerQueryAsync

        /// <summary>
        /// Use this method to send answers to an inline query.
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="inlineQuery">Query to answered</param>
        /// <param name="results">A array of results for the inline query</param>
        /// <param name="cacheTime">The maximum amount of time in seconds the result of the inline query may be cached on the server</param>
        /// <param name="isPersonal">Pass <c>true</c>, if results may be cached on the server side only for the user that sent the query. By default, results may be returned to any user who sends the same query</param>
        /// <param name="nextOffset">Pass the offset that a client should send in the next query with the same text to receive more results. Pass an empty string if there are no more results or if you don't support pagination. Offset length can't exceed 64 bytes.</param>
        /// <param name="switchPmText">If passed, clients will display a button with specified text that switches the user to a private chat with the bot and sends the bot a start message with the parameter switch_pm_parameter</param>
        /// <param name="switchPmParameter">Parameter for the start message sent to the bot when user presses the switch button</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, <c>true</c> is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#answerinlinequery"/>
        public static Task AnswerInlineQueryAsync(
            this ITelegramBotClient bot,
            InlineQuery inlineQuery,
            IEnumerable<InlineQueryResultBase> results,
            int? cacheTime = default,
            bool isPersonal = default,
            string nextOffset = default,
            string switchPmText = default,
            string switchPmParameter = default,
            CancellationToken cancellationToken = default
        ) =>
            bot.AnswerInlineQueryAsync(inlineQuery.Id, results, cacheTime, isPersonal, nextOffset, switchPmText, switchPmParameter, cancellationToken);

        /// <summary>
        /// Use this method to send answers to callback queries sent from inline keyboards. The answer will be displayed to the user as a notification at the top of the chat screen or as an alert.
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="callbackQuery">Query to answered</param>
        /// <param name="text">Text of the notification. If not specified, nothing will be shown to the user</param>
        /// <param name="showAlert">If true, an alert will be shown by the client instead of a notification at the top of the chat screen. Defaults to false.</param>
        /// <param name="url">
        /// URL that will be opened by the user's client. If you have created a Game and accepted the conditions via @Botfather, specify the URL that opens your game — note that this will only work if the query comes from a callback_game button.
        /// Otherwise, you may use links like telegram.me/your_bot? start = XXXX that open your bot with a parameter.
        /// </param>
        /// <param name="cacheTime">The maximum amount of time in seconds that the result of the callback query may be cached client-side. Telegram apps will support caching starting in version 3.14.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, <c>true</c> is returned.</returns>
        /// <remarks>
        /// Alternatively, the user can be redirected to the specified Game URL. For this option to work, you must first create a game for your bot via BotFather and accept the terms.
        /// Otherwise, you may use links like telegram.me/your_bot?start=XXXX that open your bot with a parameter.
        /// </remarks>
        /// <see href="https://core.telegram.org/bots/api#answercallbackquery"/>
        public static Task AnswerCallbackQueryAsync(
            this ITelegramBotClient bot,
            CallbackQuery callbackQuery,
            string text = default,
            bool showAlert = default,
            string url = default,
            int cacheTime = default,
            CancellationToken cancellationToken = default
        ) =>
            bot.AnswerCallbackQueryAsync(callbackQuery.Id, text, showAlert, url, cacheTime, cancellationToken);

        /// <summary>
        /// Use this method to reply to shipping queries with success and shipping options. If you sent an invoice requesting a shipping address and the parameter is_flexible was specified, the Bot API will send an Update with a shipping_query field to the bot.
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="shippingQuery">Query to answered</param>
        /// <param name="shippingOptions">Required if OK is True.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, True is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#answershippingquery"/>
        public static Task AnswerShippingQueryAsync(
            this ITelegramBotClient bot,
            ShippingQuery shippingQuery,
            IEnumerable<ShippingOption> shippingOptions,
            CancellationToken cancellationToken = default
        ) =>
            bot.AnswerShippingQueryAsync(shippingQuery.Id, shippingOptions, cancellationToken);

        /// <summary>
        /// Use this method to reply to shipping queries with failure and error message. If you sent an invoice requesting a shipping address and the parameter is_flexible was specified, the Bot API will send an Update with a shipping_query field to the bot.
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="shippingQuery">Query to answered</param>
        /// <param name="errorMessage">Required if OK is False. Error message in human readable form that explains why it is impossible to complete the order </param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, True is returned.</returns>
        /// <see href="https://core.telegram.org/bots/api#answershippingquery"/>
        public static Task AnswerShippingQueryAsync(
            this ITelegramBotClient bot,
            ShippingQuery shippingQuery,
            string errorMessage,
            CancellationToken cancellationToken = default
        ) =>
            bot.AnswerShippingQueryAsync(shippingQuery.Id, errorMessage, cancellationToken);

        /// <summary>
        /// Respond to a pre-checkout query with success
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="preCheckoutQuery">Query to answered</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, True is returned.</returns>
        /// <remarks>Note: The Bot API must receive an answer within 10 seconds after the pre-checkout query was sent.</remarks>
        /// <see href="https://core.telegram.org/bots/api#answerprecheckoutquery"/>
        public static Task AnswerPreCheckoutQueryAsync(
            this ITelegramBotClient bot,
            PreCheckoutQuery preCheckoutQuery,
            CancellationToken cancellationToken = default
        ) =>
            bot.AnswerPreCheckoutQueryAsync(preCheckoutQuery.Id, cancellationToken);

        /// <summary>
        /// Respond to a pre-checkout query with failure and error message
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="preCheckoutQuery">Query to answered</param>
        /// <param name="errorMessage">Required if OK is False. Error message in human readable form that explains the reason for failure to proceed with the checkout</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>On success, True is returned.</returns>
        /// <remarks>Note: The Bot API must receive an answer within 10 seconds after the pre-checkout query was sent.</remarks>
        /// <see href="https://core.telegram.org/bots/api#answerprecheckoutquery"/>
        public static Task AnswerPreCheckoutQueryAsync(
            this ITelegramBotClient bot,
            PreCheckoutQuery preCheckoutQuery,
            string errorMessage,
            CancellationToken cancellationToken = default
        ) =>
            bot.AnswerPreCheckoutQueryAsync(preCheckoutQuery.Id, errorMessage, cancellationToken);

        #endregion AnswerQueryAsync

        #region Other

        /// <summary>
        /// Use this method to promote or demote a user in a supergroup or a channel. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
        /// </summary>
        /// <param name="bot">The bot instance to use</param>
        /// <param name="messageFromUser">Message sent by the target user in the target chat</param>
        /// <param name="canChangeInfo">Pass True, if the administrator can change chat title, photo and other settings</param>
        /// <param name="canPostMessages">Pass True, if the administrator can create channel posts, channels only</param>
        /// <param name="canEditMessages">Pass True, if the administrator can edit messages of other users, channels only</param>
        /// <param name="canDeleteMessages">Pass True, if the administrator can delete messages of other users</param>
        /// <param name="canInviteUsers">Pass True, if the administrator can invite new users to the chat</param>
        /// <param name="canRestrictMembers">Pass True, if the administrator can restrict, ban or unban chat members</param>
        /// <param name="canPinMessages">Pass True, if the administrator can pin messages, supergroups only</param>
        /// <param name="canPromoteMembers">Pass True, if the administrator can add new administrators with a subset of his own privileges or demote administrators that he has promoted, directly or indirectly (promoted by administrators that were appointed by him)</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Returns True on success.</returns>
        /// <remarks>Pass False for all boolean parameters to demote a user.</remarks>
        /// <see href="https://core.telegram.org/bots/api#promotechatmember"/>
        public static Task PromoteChatMemberAsync(
            this ITelegramBotClient bot,
            Message messageFromUser,
            bool? canChangeInfo = default,
            bool? canPostMessages = default,
            bool? canEditMessages = default,
            bool? canDeleteMessages = default,
            bool? canInviteUsers = default,
            bool? canRestrictMembers = default,
            bool? canPinMessages = default,
            bool? canPromoteMembers = default,
            CancellationToken cancellationToken = default
        ) =>
            bot.PromoteChatMemberAsync(messageFromUser.Chat, messageFromUser.From.Id, canChangeInfo, canPostMessages, canEditMessages, canDeleteMessages, canInviteUsers, canRestrictMembers, canPinMessages, canPromoteMembers, cancellationToken);

        #endregion Other
    }
}
