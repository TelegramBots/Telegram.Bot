using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Extensions;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;
using File = Telegram.Bot.Types.File;

namespace Telegram.Bot;

/// <summary>
/// Extension methods that map to requests from Bot API documentation
/// </summary>
public static partial class TelegramBotClientExtensions
{
    #region Getting updates

    /// <summary>
    /// Use this method to receive incoming updates using long polling
    /// (<a href="https://en.wikipedia.org/wiki/Push_technology#Long_polling">wiki</a>)
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="offset">
    /// Identifier of the first update to be returned. Must be greater by one than the highest among the
    /// identifiers of previously received updates. By default, updates starting with the earliest unconfirmed
    /// update are returned. An update is considered confirmed as soon as
    /// <see cref="GetUpdatesAsync(ITelegramBotClient,GetUpdatesRequest,CancellationToken)"/> is called with an
    /// <paramref name="offset"/> higher than its <see cref="Update.Id"/>. The negative offset can be
    /// specified to retrieve updates starting from <paramref name="offset">-offset</paramref> update from the end
    /// of the updates queue. All previous updates will forgotten.
    /// </param>
    /// <param name="limit">
    /// Limits the number of updates to be retrieved. Values between 1-100 are accepted. Defaults to 100
    /// </param>
    /// <param name="timeout">
    /// Timeout in seconds for long polling. Defaults to 0, i.e. usual short polling. Should be positive, short
    /// polling should be used for testing purposes only.
    /// </param>
    /// <param name="allowedUpdates">
    /// A list of the update types you want your bot to receive. For example, specify
    /// [<see cref="UpdateType.Message"/>, <see cref="UpdateType.EditedChannelPost"/>,
    /// <see cref="UpdateType.CallbackQuery"/>] to only receive updates of these types. See
    /// <see cref="UpdateType"/> for a complete list of available update types. Specify an empty list to receive
    /// all update types except <see cref="UpdateType.ChatMember"/> (default). If not specified, the previous
    /// setting will be used.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <remarks>
    /// <list type="number">
    /// <item>This method will not work if an outgoing webhook is set up</item>
    /// <item>
    /// In order to avoid getting duplicate updates, recalculate <paramref name="offset"/> after each server
    /// response
    /// </item>
    /// </list>
    /// </remarks>
    /// <returns>An Array of <see cref="Update"/> objects is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Update[]> GetUpdatesAsync(
        this ITelegramBotClient botClient,
        int? offset = default,
        int? limit = default,
        int? timeout = default,
        IEnumerable<UpdateType>? allowedUpdates = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new GetUpdatesRequest
                {
                    Offset = offset,
                    Limit = limit,
                    Timeout = timeout,
                    AllowedUpdates = allowedUpdates,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to specify a URL and receive incoming updates via an outgoing webhook.
    /// Whenever there is an update for the bot, we will send an HTTPS POST request to the
    /// specified URL, containing an <see cref="Types.Update"/>. In case of
    /// an unsuccessful request, we will give up after a reasonable amount of attempts.
    /// Returns <see langword="true"/> on success.
    /// <para>
    /// If you'd like to make sure that the webhook was set by you, you can specify secret data
    /// in the parameter <see cref="SetWebhookRequest.SecretToken"/> . If specified, the request
    /// will contain a header "X-Telegram-Bot-Api-Secret-Token" with the secret token as content.
    /// </para>
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="url">HTTPS URL to send updates to. Use an empty string to remove webhook integration</param>
    /// <param name="certificate">
    /// Upload your public key certificate so that the root certificate in use can be checked. See our
    /// <a href="https://core.telegram.org/bots/self-signed">self-signed guide</a> for details
    /// </param>
    /// <param name="ipAddress">
    /// The fixed IP address which will be used to send webhook requests instead of the IP address resolved
    /// through DNS
    /// </param>
    /// <param name="maxConnections">
    /// Maximum allowed number of simultaneous HTTPS connections to the webhook for update
    /// delivery, 1-100. Defaults to <i>40</i>. Use lower values to limit the load on your
    /// bot's server, and higher values to increase your bot's throughput.
    /// </param>
    /// <param name="allowedUpdates">
    /// <para>A list of the update types you want your bot to receive. For example, specify
    /// [<see cref="UpdateType.Message"/>, <see cref="UpdateType.EditedChannelPost"/>,
    /// <see cref="UpdateType.CallbackQuery"/>] to only receive updates of these types. See
    /// <see cref="UpdateType"/> for a complete list of available update types. Specify an empty list to receive
    /// all update types except <see cref="UpdateType.ChatMember"/> (default). If not specified, the previous
    /// setting will be used
    /// </para>
    /// <para>
    /// Please note that this parameter doesn't affect updates created before the call to the
    /// <see cref="SetWebhookAsync(ITelegramBotClient,SetWebhookRequest,CancellationToken)"/>,
    /// so unwanted updates may be received for a short period of time.
    /// </para>
    /// </param>
    /// <param name="dropPendingUpdates">Pass <see langword="true"/> to drop all pending updates</param>
    /// <param name="secretToken">
    /// A secret token to be sent in a header "<c>X-Telegram-Bot-Api-Secret-Token</c>" in every webhook request,
    /// 1-256 characters. Only characters <c>A-Z</c>, <c>a-z</c>, <c>0-9</c>, <c>_</c> and <c>-</c>
    /// are allowed. The header is useful to ensure that the request comes from a webhook set by you.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <remarks>
    /// <list type="number">
    /// <item>
    /// You will not be able to receive updates using
    /// <see cref="GetUpdatesAsync(ITelegramBotClient,GetUpdatesRequest,CancellationToken)"/> for as long as
    /// an outgoing webhook is set up
    /// </item>
    /// <item>
    /// To use a self-signed certificate, you need to upload your
    /// <a href="https://core.telegram.org/bots/self-signed">public key certificate</a> using
    /// <paramref name="certificate"/> parameter. Please upload as <see cref="InputFileStream"/>, sending a
    /// string will not work
    /// </item>
    /// <item>Ports currently supported for webhooks: <b>443, 80, 88, 8443</b></item>
    /// </list>
    /// If you're having any trouble setting up webhooks, please check out this
    /// <a href="https://core.telegram.org/bots/webhooks">amazing guide to Webhooks</a>.
    /// </remarks>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetWebhookAsync(
        this ITelegramBotClient botClient,
        string url,
        InputFileStream? certificate = default,
        string? ipAddress = default,
        int? maxConnections = default,
        IEnumerable<UpdateType>? allowedUpdates = default,
        bool? dropPendingUpdates = default,
        string? secretToken = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetWebhookRequest
                {
                    Url = url,
                    Certificate = certificate,
                    IpAddress = ipAddress,
                    MaxConnections = maxConnections,
                    AllowedUpdates = allowedUpdates,
                    DropPendingUpdates = dropPendingUpdates,
                    SecretToken = secretToken,
                },
                cancellationToken
            )
            .ConfigureAwait(false);


    /// <summary>
    /// Use this method to remove webhook integration if you decide to switch back to
    /// <see cref="GetUpdatesAsync(ITelegramBotClient,GetUpdatesRequest,CancellationToken)"/>
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="dropPendingUpdates">Pass <see langword="true"/> to drop all pending updates</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns true on success</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task DeleteWebhookAsync(
        this ITelegramBotClient botClient,
        bool? dropPendingUpdates = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new DeleteWebhookRequest { DropPendingUpdates = dropPendingUpdates },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get current webhook status.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// On success, returns a <see cref="WebhookInfo"/> object. If the bot is using
    /// <see cref="GetUpdatesAsync(ITelegramBotClient,GetUpdatesRequest,CancellationToken)"/>,
    /// will return an object with the <see cref="WebhookInfo.Url"/> field empty.
    /// </returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<WebhookInfo> GetWebhookInfoAsync(
        this ITelegramBotClient botClient,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new GetWebhookInfoRequest(), cancellationToken)
            .ConfigureAwait(false);

    #endregion Getting updates

    #region Available methods

    /// <summary>
    /// A simple method for testing your bot’s auth token.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns basic information about the bot in form of a <see cref="User"/> object.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<User> GetMeAsync(
        this ITelegramBotClient botClient,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new GetMeRequest(), cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to log out from the cloud Bot API server before launching the bot locally. You <b>must</b>
    /// log out the bot before running it locally, otherwise there is no guarantee that the bot will receive
    /// updates. After a successful call, you can immediately log in on a local server, but will not be able to
    /// log in back to the cloud Bot API server for 10 minutes.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task LogOutAsync(
        this ITelegramBotClient botClient,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new LogOutRequest(), cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to close the bot instance before moving it from one local server to another. You need to
    /// delete the webhook before calling this method to ensure that the bot isn't launched again after server
    /// restart. The method will return error 429 in the first 10 minutes after the bot is launched.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task CloseAsync(
        this ITelegramBotClient botClient,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new CloseRequest(), cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send text messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="text">Text of the message to be sent, 1-4096 characters after entities parsing</param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="parseMode">
    /// Mode for parsing entities in the new caption. See
    /// <a href="https://core.telegram.org/bots/api#formatting-options">formatting</a> options for more
    /// details
    /// </param>
    /// <param name="entities">
    /// List of special entities that appear in message text, which can be specified instead
    /// of <see cref="ParseMode"/>
    /// </param>
    /// <param name="linkPreviewOptions">Link preview generation options for the message</param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">Protects the contents of sent messages from forwarding and saving</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to <see cref="ForceReplyMarkup">force a
    /// reply</see> from the user
    /// </param>
    /// <param name="businessConnectionId">
    /// Unique identifier of the business connection on behalf of which the message will be sent
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    [Obsolete($"Use the method {nameof(SendMessageAsync)} instead")]
    public static async Task<Message> SendTextMessageAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string text,
        int? messageThreadId = default,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? entities = default,
        LinkPreviewOptions? linkPreviewOptions = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SendMessageRequest
                {
                    ChatId = chatId,
                    Text = text,
                    MessageThreadId = messageThreadId,
                    ParseMode = parseMode,
                    Entities = entities,
                    LinkPreviewOptions = linkPreviewOptions,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    ReplyParameters = replyParameters,
                    ReplyMarkup = replyMarkup,
                    BusinessConnectionId = businessConnectionId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to forward messages of any kind. Service messages can't be forwarded.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="fromChatId">
    /// Unique identifier for the chat where the original message was sent
    /// (or channel username in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">Message identifier in the chat specified in <paramref name="fromChatId"/></param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">Protects the contents of sent messages from forwarding and saving</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> ForwardMessageAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        ChatId fromChatId,
        int messageId,
        int? messageThreadId = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new ForwardMessageRequest
                {
                    ChatId = chatId,
                    FromChatId = fromChatId,
                    MessageId = messageId,
                    MessageThreadId = messageThreadId,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to forward multiple messages of any kind. If some of the specified messages can't be found
    /// or forwarded, they are skipped. Service messages and messages with protected content can't be forwarded.
    /// Album grouping is kept for forwarded messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="fromChatId">
    /// Unique identifier for the chat where the original messages were sent
    /// (or channel username in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageIds">
    /// Identifiers of 1-100 messages in the chat from_chat_id to forward.
    /// The identifiers must be specified in a strictly increasing order.
    /// </param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound.
    /// </param>
    /// <param name="protectContent">
    /// Protects the contents of sent messages from forwarding and saving
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, an array of <see cref="MessageId"/> of the sent messages is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<MessageId[]> ForwardMessagesAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        ChatId fromChatId,
        IEnumerable<int> messageIds,
        int? messageThreadId = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new ForwardMessagesRequest
                {
                    ChatId = chatId,
                    FromChatId = fromChatId,
                    MessageIds = messageIds,
                    MessageThreadId = messageThreadId,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to copy messages of any kind. Service messages and invoice messages can't be copied.
    /// The method is analogous to the method
    /// <see cref="ForwardMessageAsync(ITelegramBotClient,ForwardMessageRequest,CancellationToken)"/>,
    /// but the copied message doesn't have a link to the original message.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="fromChatId">
    /// Unique identifier for the chat where the original message was sent
    /// (or channel username in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">Message identifier in the chat specified in <paramref name="fromChatId"/></param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="caption">
    /// New caption for media, 0-1024 characters after entities parsing. If not specified, the original caption
    /// is kept
    /// </param>
    /// <param name="parseMode">
    /// Mode for parsing entities in the new caption. See
    /// <a href="https://core.telegram.org/bots/api#formatting-options">formatting</a> options for
    /// more details
    /// </param>
    /// <param name="captionEntities">
    /// List of special entities that appear in the caption, which can be specified instead
    /// of <see cref="ParseMode"/>
    /// </param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">Protects the contents of sent messages from forwarding and saving</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns the <see cref="MessageId"/> of the sent message on success.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<MessageId> CopyMessageAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        ChatId fromChatId,
        int messageId,
        int? messageThreadId = default,
        string? caption = default,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new CopyMessageRequest
                {
                    ChatId = chatId,
                    FromChatId = fromChatId,
                    MessageId = messageId,
                    MessageThreadId = messageThreadId,
                    Caption = caption,
                    ParseMode = parseMode,
                    CaptionEntities = captionEntities,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    ReplyParameters = replyParameters,
                    ReplyMarkup = replyMarkup,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to copy messages of any kind. If some of the specified messages can't be found or copied,
    /// they are skipped. Service messages, giveaway messages, giveaway winners messages, and invoice messages
    /// can't be copied. A quiz <see cref="Poll"/> can be copied only if the value of the field
    /// <see cref="Poll.CorrectOptionId">CorrectOptionId</see> is known to the bot. The method is analogous
    /// to the method
    /// <see cref="ForwardMessagesAsync(ITelegramBotClient,ForwardMessagesRequest,CancellationToken)"/>, but the
    /// copied messages don't have a link to the original message. Album grouping is kept for copied messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="fromChatId">
    /// Unique identifier for the chat where the original messages were sent
    /// (or channel username in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageIds">
    /// Identifiers of 1-100 messages in the chat <paramref name="fromChatId"/> to copy.
    /// The identifiers must be specified in a strictly increasing order.
    /// </param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound.
    /// </param>
    /// <param name="protectContent">
    /// Protects the contents of sent messages from forwarding and saving
    /// </param>
    /// <param name="removeCaption">
    /// Pass <see langword="true"/> to copy the messages without their captions
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, an array of <see cref="MessageId"/> of the sent messages is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<MessageId[]> CopyMessagesAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        ChatId fromChatId,
        int[] messageIds,
        int? messageThreadId = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        bool? removeCaption = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new CopyMessagesRequest
                {
                    ChatId = chatId,
                    FromChatId = fromChatId,
                    MessageIds = messageIds,
                    MessageThreadId = messageThreadId,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    RemoveCaption = removeCaption,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send photos.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="photo">
    /// Photo to send. Pass a <see cref="InputFileId"/> as String to send a photo that exists on
    /// the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get a photo from
    /// the Internet, or upload a new photo using multipart/form-data. The photo must be at most 10 MB in size.
    /// The photo's width and height must not exceed 10000 in total. Width and height ratio must be at most 20
    /// </param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="caption">
    /// Photo caption (may also be used when resending photos by <see cref="InputFileId"/>),
    /// 0-1024 characters after entities parsing
    /// </param>
    /// <param name="parseMode">
    /// Mode for parsing entities in the new caption. See
    /// <a href="https://core.telegram.org/bots/api#formatting-options">formatting</a> options for
    /// more details
    /// </param>
    /// <param name="captionEntities">
    /// List of special entities that appear in the caption, which can be specified instead
    /// of <see cref="ParseMode"/>
    /// </param>
    /// <param name="hasSpoiler">
    /// Pass <see langword="true"/> if the photo needs to be covered with a spoiler animation
    /// </param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">Protects the contents of sent messages from forwarding and saving</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="businessConnectionId">
    /// Unique identifier of the business connection on behalf of which the message will be sent
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> SendPhotoAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile photo,
        int? messageThreadId = default,
        string? caption = default,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool? hasSpoiler = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull().
            MakeRequestAsync(
                new SendPhotoRequest
                {
                    ChatId = chatId,
                    Photo = photo,
                    MessageThreadId = messageThreadId,
                    Caption = caption,
                    ParseMode = parseMode,
                    CaptionEntities = captionEntities,
                    HasSpoiler = hasSpoiler,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    ReplyParameters = replyParameters,
                    ReplyMarkup = replyMarkup,
                    BusinessConnectionId = businessConnectionId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send audio files, if you want Telegram clients to display them in the music player.
    /// Your audio must be in the .MP3 or .M4A format. Bots can currently send audio files of up to 50 MB in size,
    /// this limit may be changed in the future.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="audio">
    /// Audio file to send. Pass a <see cref="InputFileId"/> as String to send an audio file that
    /// exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get an audio
    /// file from the Internet, or upload a new one using multipart/form-data
    /// </param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="caption">Audio caption, 0-1024 characters after entities parsing</param>
    /// <param name="parseMode">
    /// Mode for parsing entities in the new caption. See
    /// <a href="https://core.telegram.org/bots/api#formatting-options">formatting</a> options for
    /// more details
    /// </param>
    /// <param name="captionEntities">
    /// List of special entities that appear in the caption, which can be specified instead
    /// of <see cref="ParseMode"/>
    /// </param>
    /// <param name="duration">Duration of the audio in seconds</param>
    /// <param name="performer">Performer</param>
    /// <param name="title">Track name</param>
    /// <param name="thumbnail">
    /// Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side.
    /// The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height
    /// should not exceed 320. Ignored if the file is not uploaded using multipart/form-data. Thumbnails can't be
    /// reused and can be only uploaded as a new file, so you can pass "attach://&lt;file_attach_name&gt;" if the
    /// thumbnail was uploaded using multipart/form-data under &lt;file_attach_name&gt;
    /// </param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">Protects the contents of sent messages from forwarding and saving</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="businessConnectionId">
    /// Unique identifier of the business connection on behalf of which the message will be sent
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> SendAudioAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile audio,
        int? messageThreadId = default,
        string? caption = default,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        int? duration = default,
        string? performer = default,
        string? title = default,
        InputFile? thumbnail = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SendAudioRequest
                {
                    ChatId = chatId,
                    Audio = audio,
                    MessageThreadId = messageThreadId,
                    Caption = caption,
                    ParseMode = parseMode,
                    CaptionEntities = captionEntities,
                    Duration = duration,
                    Performer = performer,
                    Title = title,
                    Thumbnail = thumbnail,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    ReplyParameters = replyParameters,
                    ReplyMarkup = replyMarkup,
                    BusinessConnectionId = businessConnectionId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send general files. Bots can currently send files of any type of up to 50 MB in size,
    /// this limit may be changed in the future.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="document">
    /// File to send. Pass a <see cref="InputFileId"/> as String to send a file that exists on the
    /// Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get a file from the Internet,
    /// or upload a new one using multipart/form-data
    /// </param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="thumbnail">
    /// Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side.
    /// The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height should
    /// not exceed 320. Ignored if the file is not uploaded using multipart/form-data. Thumbnails can't be reused
    /// and can be only uploaded as a new file, so you can pass "attach://&lt;file_attach_name&gt;" if the
    /// thumbnail was uploaded using multipart/form-data under &lt;file_attach_name&gt;
    /// </param>
    /// <param name="caption">
    /// Document caption (may also be used when resending documents by file_id), 0-1024 characters after
    /// entities parsing
    /// </param>
    /// <param name="parseMode">
    /// Mode for parsing entities in the new caption. See
    /// <a href="https://core.telegram.org/bots/api#formatting-options">formatting</a> options for
    /// more details
    /// </param>
    /// <param name="captionEntities">
    /// List of special entities that appear in the caption, which can be specified instead
    /// of <see cref="ParseMode"/>
    /// </param>
    /// <param name="disableContentTypeDetection">
    /// Disables automatic server-side content type detection for files uploaded using multipart/form-data
    /// </param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">Protects the contents of sent messages from forwarding and saving</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="businessConnectionId">
    /// Unique identifier of the business connection on behalf of which the message will be sent
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> SendDocumentAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile document,
        int? messageThreadId = default,
        InputFile? thumbnail = default,
        string? caption = default,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool? disableContentTypeDetection = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SendDocumentRequest
                {
                    ChatId = chatId,
                    Document = document,
                    MessageThreadId = messageThreadId,
                    Thumbnail = thumbnail,
                    Caption = caption,
                    ParseMode = parseMode,
                    CaptionEntities = captionEntities,
                    DisableContentTypeDetection = disableContentTypeDetection,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    ReplyParameters = replyParameters,
                    ReplyMarkup = replyMarkup,
                    BusinessConnectionId = businessConnectionId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send video files, Telegram clients support mp4 videos (other formats may be sent as
    /// <see cref="Document"/>). Bots can currently send video files of up to 50 MB in size, this limit may be
    /// changed in the future.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="video">
    /// Video to send. Pass a <see cref="InputFileId"/> as String to send a video that exists on
    /// the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get a video from the
    /// Internet, or upload a new video using multipart/form-data
    /// </param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="duration">Duration of sent video in seconds</param>
    /// <param name="width">Video width</param>
    /// <param name="height">Video height</param>
    /// <param name="thumbnail">
    /// Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side.
    /// The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height should
    /// not exceed 320. Ignored if the file is not uploaded using multipart/form-data. Thumbnails can't be reused
    /// and can be only uploaded as a new file, so you can pass "attach://&lt;file_attach_name&gt;" if the
    /// thumbnail was uploaded using multipart/form-data under &lt;file_attach_name&gt;
    /// </param>
    /// <param name="caption">
    /// Video caption (may also be used when resending videos by file_id), 0-1024 characters after entities parsing
    /// </param>
    /// <param name="parseMode">
    /// Mode for parsing entities in the new caption. See
    /// <a href="https://core.telegram.org/bots/api#formatting-options">formatting</a> options for
    /// more details
    /// </param>
    /// <param name="captionEntities">
    /// List of special entities that appear in the caption, which can be specified instead
    /// of <see cref="ParseMode"/>
    /// </param>
    /// <param name="hasSpoiler">
    /// Pass <see langword="true"/> if the video needs to be covered with a spoiler animation
    /// </param>
    /// <param name="supportsStreaming">Pass <see langword="true"/>, if the uploaded video is suitable for streaming</param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">Protects the contents of sent messages from forwarding and saving</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="businessConnectionId">
    /// Unique identifier of the business connection on behalf of which the message will be sent
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> SendVideoAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile video,
        int? messageThreadId = default,
        int? duration = default,
        int? width = default,
        int? height = default,
        InputFile? thumbnail = default,
        string? caption = default,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool? hasSpoiler = default,
        bool? supportsStreaming = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SendVideoRequest
                {
                    ChatId = chatId,
                    Video = video,
                    MessageThreadId = messageThreadId,
                    Duration = duration,
                    Width = width,
                    Height = height,
                    Thumbnail = thumbnail,
                    Caption = caption,
                    ParseMode = parseMode,
                    CaptionEntities = captionEntities,
                    HasSpoiler = hasSpoiler,
                    SupportsStreaming = supportsStreaming,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    ReplyParameters = replyParameters,
                    ReplyMarkup = replyMarkup,
                    BusinessConnectionId = businessConnectionId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send animation files (GIF or H.264/MPEG-4 AVC video without sound). Bots can currently
    /// send animation files of up to 50 MB in size, this limit may be changed in the future.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="animation">
    /// Animation to send. Pass a <see cref="InputFileId"/> as String to send an animation that
    /// exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get an
    /// animation from the Internet, or upload a new animation using multipart/form-data
    /// </param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="duration">Duration of sent animation in seconds</param>
    /// <param name="width">Animation width</param>
    /// <param name="height">Animation height</param>
    /// <param name="thumbnail">
    /// Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side.
    /// The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height should
    /// not exceed 320. Ignored if the file is not uploaded using multipart/form-data. Thumbnails can't be reused
    /// and can be only uploaded as a new file, so you can pass "attach://&lt;file_attach_name&gt;" if the
    /// thumbnail was uploaded using multipart/form-data under &lt;file_attach_name&gt;
    /// </param>
    /// <param name="caption">
    /// Animation caption (may also be used when resending animation by <see cref="InputFileId"/>),
    /// 0-1024 characters after entities parsing
    /// </param>
    /// <param name="parseMode">
    /// Mode for parsing entities in the new caption. See
    /// <a href="https://core.telegram.org/bots/api#formatting-options">formatting</a> options for
    /// more details
    /// </param>
    /// <param name="captionEntities">
    /// List of special entities that appear in the caption, which can be specified instead
    /// of <see cref="ParseMode"/>
    /// </param>
    /// <param name="hasSpoiler">
    /// Pass <see langword="true"/> if the animation needs to be covered with a spoiler animation
    /// </param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">Protects the contents of sent messages from forwarding and saving</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="businessConnectionId">
    /// Unique identifier of the business connection on behalf of which the message will be sent
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> SendAnimationAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile animation,
        int? messageThreadId = default,
        int? duration = default,
        int? width = default,
        int? height = default,
        InputFile? thumbnail = default,
        string? caption = default,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool? hasSpoiler = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SendAnimationRequest
                {
                    ChatId = chatId,
                    Animation = animation,
                    MessageThreadId = messageThreadId,
                    Duration = duration,
                    Width = width,
                    Height = height,
                    Thumbnail = thumbnail,
                    Caption = caption,
                    ParseMode = parseMode,
                    CaptionEntities = captionEntities,
                    HasSpoiler = hasSpoiler,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    ReplyParameters = replyParameters,
                    ReplyMarkup = replyMarkup,
                    BusinessConnectionId = businessConnectionId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send audio files, if you want Telegram clients to display the file as a playable voice
    /// message. For this to work, your audio must be in an .OGG file encoded with OPUS (other formats may be sent
    /// as <see cref="Audio"/> or <see cref="Document"/>). Bots can currently send voice messages of up to 50 MB
    /// in size, this limit may be changed in the future.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="voice">
    /// Audio file to send. Pass a <see cref="InputFileId"/> as String to send a file that exists
    /// on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get a file from
    /// the Internet, or upload a new one using multipart/form-data
    /// </param>
    /// <param name="caption">Voice message caption, 0-1024 characters after entities parsing</param>
    /// <param name="parseMode">
    /// Mode for parsing entities in the new caption. See
    /// <a href="https://core.telegram.org/bots/api#formatting-options">formatting</a> options for
    /// more details
    /// </param>
    /// <param name="captionEntities">
    /// List of special entities that appear in the caption, which can be specified instead
    /// of <see cref="ParseMode"/>
    /// </param>
    /// <param name="duration">Duration of the voice message in seconds</param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">Protects the contents of sent messages from forwarding and saving</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <param name="businessConnectionId">
    /// Unique identifier of the business connection on behalf of which the message will be sent
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> SendVoiceAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile voice,
        int? messageThreadId = default,
        string? caption = default,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        int? duration = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SendVoiceRequest
                {
                    ChatId = chatId,
                    Voice = voice,
                    MessageThreadId = messageThreadId,
                    Caption = caption,
                    ParseMode = parseMode,
                    CaptionEntities = captionEntities,
                    Duration = duration,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    ReplyParameters = replyParameters,
                    ReplyMarkup = replyMarkup,
                    BusinessConnectionId = businessConnectionId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// As of <a href="https://telegram.org/blog/video-messages-and-telescope">v.4.0</a>, Telegram clients
    /// support rounded square mp4 videos of up to 1 minute long. Use this method to send video messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="videoNote">
    /// Video note to send. Pass a <see cref="InputFileId"/> as String to send a video note that
    /// exists on the Telegram servers (recommended) or upload a new video using multipart/form-data. Sending
    /// video notes by a URL is currently unsupported
    /// </param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="duration">Duration of sent video in seconds</param>
    /// <param name="length">Video width and height, i.e. diameter of the video message</param>
    /// <param name="thumbnail">
    /// Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side.
    /// The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height should
    /// not exceed 320. Ignored if the file is not uploaded using multipart/form-data. Thumbnails can't be reused
    /// and can be only uploaded as a new file, so you can pass "attach://&lt;file_attach_name&gt;" if the
    /// thumbnail was uploaded using multipart/form-data under &lt;file_attach_name&gt;
    /// </param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">Protects the contents of sent messages from forwarding and saving</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="businessConnectionId">
    /// Unique identifier of the business connection on behalf of which the message will be sent
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> SendVideoNoteAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile videoNote,
        int? messageThreadId = default,
        int? duration = default,
        int? length = default,
        InputFile? thumbnail = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SendVideoNoteRequest
                {
                    ChatId = chatId,
                    VideoNote = videoNote,
                    MessageThreadId = messageThreadId,
                    Duration = duration,
                    Length = length,
                    Thumbnail = thumbnail,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    ReplyParameters = replyParameters,
                    ReplyMarkup = replyMarkup,
                    BusinessConnectionId = businessConnectionId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send a group of photos, videos, documents or audios as an album. Documents and audio
    /// files can be only grouped in an album with messages of the same type.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="media">An array describing messages to be sent, must include 2-10 items</param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">Protects the contents of sent messages from forwarding and saving</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="businessConnectionId">
    /// Unique identifier of the business connection on behalf of which the message will be sent
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, an array of <see cref="Message"/>s that were sent is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message[]> SendMediaGroupAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        IEnumerable<IAlbumInputMedia> media,
        int? messageThreadId = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        ReplyParameters? replyParameters = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SendMediaGroupRequest
                {
                    ChatId = chatId,
                    Media = media,
                    MessageThreadId = messageThreadId,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    ReplyParameters = replyParameters,
                    BusinessConnectionId = businessConnectionId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send point on the map.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="latitude">Latitude of location</param>
    /// <param name="longitude">Longitude of location</param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="horizontalAccuracy">The radius of uncertainty for the location, measured in meters; 0-1500</param>
    /// <param name="livePeriod">
    /// Period in seconds for which the location will be updated, should be between 60 and 86400
    /// </param>
    /// <param name="heading">
    /// For live locations, a direction in which the user is moving, in degrees. Must be between 1 and 360
    /// if specified
    /// </param>
    /// <param name="proximityAlertRadius">
    /// For live locations, a maximum distance for proximity alerts about approaching another chat member,
    /// in meters. Must be between 1 and 100000 if specified
    /// </param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">Protects the contents of sent messages from forwarding and saving</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="businessConnectionId">
    /// Unique identifier of the business connection on behalf of which the message will be sent
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> SendLocationAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        double latitude,
        double longitude,
        int? messageThreadId = default,
        double? horizontalAccuracy = default,
        int? livePeriod = default,
        int? heading = default,
        int? proximityAlertRadius = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SendLocationRequest
                {
                    ChatId = chatId,
                    Latitude = latitude,
                    Longitude = longitude,
                    MessageThreadId = messageThreadId,
                    HorizontalAccuracy = horizontalAccuracy,
                    LivePeriod = livePeriod,
                    Heading = heading,
                    ProximityAlertRadius = proximityAlertRadius,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    ReplyParameters = replyParameters,
                    ReplyMarkup = replyMarkup,
                    BusinessConnectionId = businessConnectionId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit live location messages. A location can be edited until its
    /// <see cref="Location.LivePeriod"/> expires or editing is explicitly disabled by a call to
    /// <see cref="StopMessageLiveLocationAsync(ITelegramBotClient, ChatId, int, InlineKeyboardMarkup?, CancellationToken)"/>.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">Identifier of the message to edit</param>
    /// <param name="latitude">Latitude of new location</param>
    /// <param name="longitude">Longitude of new location</param>
    /// <param name="horizontalAccuracy">
    /// The radius of uncertainty for the location, measured in meters; 0-1500
    /// </param>
    /// <param name="heading">
    /// Direction in which the user is moving, in degrees. Must be between 1 and 360 if specified
    /// </param>
    /// <param name="proximityAlertRadius">
    /// Maximum distance for proximity alerts about approaching another chat member, in meters.
    /// Must be between 1 and 100000 if specified
    /// </param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success the edited <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> EditMessageLiveLocationAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        double latitude,
        double longitude,
        float? horizontalAccuracy = default,
        int? heading = default,
        int? proximityAlertRadius = default,
        InlineKeyboardMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new EditMessageLiveLocationRequest
                {
                    ChatId = chatId,
                    MessageId = messageId,
                    Latitude = latitude,
                    Longitude = longitude,
                    HorizontalAccuracy = horizontalAccuracy,
                    Heading = heading,
                    ProximityAlertRadius = proximityAlertRadius,
                    ReplyMarkup = replyMarkup,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit live location messages. A location can be edited until its
    /// <see cref="Location.LivePeriod"/> expires or editing is explicitly disabled by a call to
    /// <see cref="StopMessageLiveLocationAsync(ITelegramBotClient, string, InlineKeyboardMarkup?, CancellationToken)"/>.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="latitude">Latitude of new location</param>
    /// <param name="longitude">Longitude of new location</param>
    /// <param name="horizontalAccuracy">
    /// The radius of uncertainty for the location, measured in meters; 0-1500
    /// </param>
    /// <param name="heading">
    /// Direction in which the user is moving, in degrees. Must be between 1 and 360 if specified
    /// </param>
    /// <param name="proximityAlertRadius">
    /// Maximum distance for proximity alerts about approaching another chat member, in meters.
    /// Must be between 1 and 100000 if specified
    /// </param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task EditMessageLiveLocationAsync(
        this ITelegramBotClient botClient,
        string inlineMessageId,
        double latitude,
        double longitude,
        float? horizontalAccuracy = default,
        int? heading = default,
        int? proximityAlertRadius = default,
        InlineKeyboardMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new EditInlineMessageLiveLocationRequest
                {
                    InlineMessageId = inlineMessageId,
                    Latitude = latitude,
                    Longitude = longitude,
                    HorizontalAccuracy = horizontalAccuracy,
                    Heading = heading,
                    ProximityAlertRadius = proximityAlertRadius,
                    ReplyMarkup = replyMarkup
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to stop updating a live location message before
    /// <see cref="Location.LivePeriod"/> expires.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">Identifier of the sent message</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success the sent <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> StopMessageLiveLocationAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        InlineKeyboardMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new StopMessageLiveLocationRequest
                {
                    ChatId = chatId,
                    MessageId = messageId,
                    ReplyMarkup = replyMarkup,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to stop updating a live location message before
    /// <see cref="Location.LivePeriod"/> expires.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task StopMessageLiveLocationAsync(
        this ITelegramBotClient botClient,
        string inlineMessageId,
        InlineKeyboardMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new StopInlineMessageLiveLocationRequest
                {
                    InlineMessageId = inlineMessageId,
                    ReplyMarkup = replyMarkup,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send information about a venue.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="latitude">Latitude of the venue</param>
    /// <param name="longitude">Longitude of the venue</param>
    /// <param name="title">Name of the venue</param>
    /// <param name="address">Address of the venue</param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="foursquareId">Foursquare identifier of the venue</param>
    /// <param name="foursquareType">
    /// Foursquare type of the venue, if known. (For example, “arts_entertainment/default”,
    /// “arts_entertainment/aquarium” or “food/icecream”.)
    /// </param>
    /// <param name="googlePlaceId">Google Places identifier of the venue</param>
    /// <param name="googlePlaceType">
    /// Google Places type of the venue. (See
    /// <a href="https://developers.google.com/places/web-service/supported_types">supported types</a>)
    /// </param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">Protects the contents of sent messages from forwarding and saving</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="businessConnectionId">
    /// Unique identifier of the business connection on behalf of which the message will be sent
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    /// <a href="https://core.telegram.org/bots/api#sendvenue"/>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> SendVenueAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        double latitude,
        double longitude,
        string title,
        string address,
        int? messageThreadId = default,
        string? foursquareId = default,
        string? foursquareType = default,
        string? googlePlaceId = default,
        string? googlePlaceType = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SendVenueRequest
                {
                    ChatId = chatId,
                    Latitude = latitude,
                    Longitude = longitude,
                    Title = title,
                    Address = address,
                    FoursquareId = foursquareId,
                    FoursquareType = foursquareType,
                    GooglePlaceId = googlePlaceId,
                    GooglePlaceType = googlePlaceType,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    ReplyParameters = replyParameters,
                    ReplyMarkup = replyMarkup,
                    MessageThreadId = messageThreadId,
                    BusinessConnectionId = businessConnectionId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send phone contacts.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="phoneNumber">Contact's phone number</param>
    /// <param name="firstName">Contact's first name</param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="lastName">Contact's last name</param>
    /// <param name="vCard">Additional data about the contact in the form of a vCard, 0-2048 bytes</param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">Protects the contents of sent messages from forwarding and saving</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="businessConnectionId">
    /// Unique identifier of the business connection on behalf of which the action will be sent
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> SendContactAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string phoneNumber,
        string firstName,
        int? messageThreadId = default,
        string? lastName = default,
        string? vCard = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SendContactRequest
                {
                    ChatId = chatId,
                    PhoneNumber = phoneNumber,
                    FirstName = firstName,
                    LastName = lastName,
                    Vcard = vCard,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    ReplyParameters = replyParameters,
                    ReplyMarkup = replyMarkup,
                    MessageThreadId = messageThreadId,
                    BusinessConnectionId = businessConnectionId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send a native poll.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="question">Poll question, 1-300 characters</param>
    /// <param name="options">A list of answer options, 2-10 strings 1-100 characters each</param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="isAnonymous"><see langword="true"/>, if the poll needs to be anonymous, defaults to <see langword="true"/></param>
    /// <param name="type">
    /// Poll type, <see cref="PollType.Quiz"/> or <see cref="PollType.Regular"/>,
    /// defaults to <see cref="PollType.Regular"/>
    /// </param>
    /// <param name="allowsMultipleAnswers">
    /// <see langword="true"/>, if the poll allows multiple answers, ignored for polls in quiz mode,
    /// defaults to <see langword="false"/>
    /// </param>
    /// <param name="correctOptionId">
    /// 0-based identifier of the correct answer option, required for polls in quiz mode
    /// </param>
    /// <param name="explanation">
    /// Text that is shown when a user chooses an incorrect answer or taps on the lamp icon in a quiz-style poll,
    /// 0-200 characters with at most 2 line feeds after entities parsing
    /// </param>
    /// <param name="explanationParseMode">
    /// Mode for parsing entities in the explanation. See
    /// <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a>
    /// for more details
    /// </param>
    /// <param name="explanationEntities">
    /// List of special entities that appear in the poll explanation, which can be specified instead
    /// of <see cref="ParseMode"/>
    /// </param>
    /// <param name="openPeriod">
    /// Amount of time in seconds the poll will be active after creation, 5-600. Can't be used together
    /// with <paramref name="closeDate"/>
    /// </param>
    /// <param name="closeDate">
    /// Point in time when the poll will be automatically closed. Must be at least 5 and no more than 600 seconds
    /// in the future. Can't be used together with <paramref name="openPeriod"/>
    /// </param>
    /// <param name="isClosed">
    /// Pass <see langword="true"/>, if the poll needs to be immediately closed. This can be useful for poll preview
    /// </param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">Protects the contents of sent messages from forwarding and saving</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="businessConnectionId">
    /// Unique identifier of the business connection on behalf of which the action will be sent
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> SendPollAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string question,
        IEnumerable<string> options,
        int? messageThreadId = default,
        bool? isAnonymous = default,
        PollType? type = default,
        bool? allowsMultipleAnswers = default,
        int? correctOptionId = default,
        string? explanation = default,
        ParseMode? explanationParseMode = default,
        IEnumerable<MessageEntity>? explanationEntities = default,
        int? openPeriod = default,
        DateTime? closeDate = default,
        bool? isClosed = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SendPollRequest
                {
                    ChatId = chatId,
                    Question = question,
                    Options = options,
                    MessageThreadId = messageThreadId,
                    IsAnonymous = isAnonymous,
                    Type = type,
                    AllowsMultipleAnswers = allowsMultipleAnswers,
                    CorrectOptionId = correctOptionId,
                    Explanation = explanation,
                    ExplanationParseMode = explanationParseMode,
                    ExplanationEntities = explanationEntities,
                    OpenPeriod = openPeriod,
                    CloseDate = closeDate,
                    IsClosed = isClosed,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    ReplyParameters = replyParameters,
                    ReplyMarkup = replyMarkup,
                    BusinessConnectionId = businessConnectionId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send an animated emoji that will display a random value.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="emoji">
    /// Emoji on which the dice throw animation is based. Currently, must be one of <see cref="Emoji.Dice"/>,
    /// <see cref="Emoji.Darts"/>, <see cref="Emoji.Basketball"/>, <see cref="Emoji.Football"/>,
    /// <see cref="Emoji.Bowling"/> or <see cref="Emoji.SlotMachine"/>. Dice can have values 1-6 for
    /// <see cref="Emoji.Dice"/>, <see cref="Emoji.Darts"/> and <see cref="Emoji.Bowling"/>, values 1-5 for
    /// <see cref="Emoji.Basketball"/> and <see cref="Emoji.Football"/>, and values 1-64 for
    /// <see cref="Emoji.SlotMachine"/>. Defaults to <see cref="Emoji.Dice"/>
    /// </param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">Protects the contents of sent messages from forwarding and saving</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="businessConnectionId">
    /// Unique identifier of the business connection on behalf of which the action will be sent
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> SendDiceAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int? messageThreadId = default,
        Emoji? emoji = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SendDiceRequest
                {
                    ChatId = chatId,
                    MessageThreadId = messageThreadId,
                    Emoji = emoji,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    ReplyParameters = replyParameters,
                    ReplyMarkup = replyMarkup,
                    BusinessConnectionId = businessConnectionId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method when you need to tell the user that something is happening on the bot’s side. The status is
    /// set for 5 seconds or less (when a message arrives from your bot, Telegram clients clear its typing status).
    /// </summary>
    /// <example>
    /// <para>
    /// The <a href="https://t.me/imagebot">ImageBot</a> needs some time to process a request and upload the
    /// image. Instead of sending a text message along the lines of “Retrieving image, please wait…”, the bot may
    /// use <see cref="SendChatActionAsync(ITelegramBotClient,SendChatActionRequest,CancellationToken)"/> with
    /// <see cref="SendChatActionRequest.Action"/> = <see cref="ChatAction.UploadPhoto"/>.
    /// The user will see a “sending photo” status for the bot.
    /// </para>
    /// <para>
    /// We only recommend using this method when a response from the bot will take a <b>noticeable</b> amount of
    /// time to arrive.
    /// </para>
    /// </example>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="chatAction">
    /// Type of action to broadcast. Choose one, depending on what the user is about to receive:
    /// <see cref="ChatAction.Typing"/> for <see cref="SendTextMessageAsync">text messages</see>,
    /// <see cref="ChatAction.UploadPhoto"/> for
    /// <see cref="SendPhotoAsync(ITelegramBotClient,SendPhotoRequest,CancellationToken)">photos</see>,
    /// <see cref="ChatAction.RecordVideo"/> or <see cref="ChatAction.UploadVideo"/> for
    /// <see cref="SendVideoAsync(ITelegramBotClient,SendVideoRequest,CancellationToken)">videos</see>,
    /// <see cref="ChatAction.RecordVoice"/> or <see cref="ChatAction.UploadVoice"/> for
    /// <see cref="SendVoiceAsync(ITelegramBotClient,SendVoiceRequest,CancellationToken)">voice notes</see>,
    /// <see cref="ChatAction.UploadDocument"/> for
    /// <see cref="SendDocumentAsync(ITelegramBotClient,SendDocumentRequest,CancellationToken)">general files</see>,
    /// <see cref="ChatAction.FindLocation"/> for
    /// <see cref="SendLocationAsync(ITelegramBotClient,SendLocationRequest,CancellationToken)">location data</see>,
    /// <see cref="ChatAction.RecordVideoNote"/> or <see cref="ChatAction.UploadVideoNote"/> for
    /// <see cref="SendVideoNoteAsync(ITelegramBotClient,SendVideoNoteRequest,CancellationToken)">video notes</see>
    /// </param>
    /// <param name="messageThreadId">Unique identifier for the target message thread; supergroups only</param>
    /// <param name="businessConnectionId">
    /// Unique identifier of the business connection on behalf of which the action will be sent
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SendChatActionAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        ChatAction chatAction,
        int? messageThreadId = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SendChatActionRequest
                {
                    ChatId = chatId,
                    Action = chatAction,
                    MessageThreadId = messageThreadId,
                    BusinessConnectionId = businessConnectionId,
                },
                cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the chosen reactions on a message. Service messages can't be reacted to.
    /// Automatically forwarded messages from a channel to its discussion group have the same
    /// available reactions as messages in the channel.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">
    /// Identifier of the target message. If the message belongs to a media group, the reaction
    /// is set to the first non-deleted message in the group instead.
    /// </param>
    /// <param name="reaction">
    /// New list of reaction types to set on the message. Currently, as non-premium users, bots can
    /// set up to one reaction per message. A custom emoji reaction can be used if it is either
    /// already present on the message or explicitly allowed by chat administrators.
    /// </param>
    /// <param name="isBig">
    /// Pass <see langword="true"/> to set the reaction with a big animation
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetMessageReactionAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        IEnumerable<ReactionType>? reaction,
        bool? isBig,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetMessageReactionRequest
                {
                    ChatId = chatId,
                    MessageId = messageId,
                    Reaction = reaction,
                    IsBig = isBig,
                },
                cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get a list of profile pictures for a user.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="offset">
    /// Sequential number of the first photo to be returned. By default, all photos are returned
    /// </param>
    /// <param name="limit">
    /// Limits the number of photos to be retrieved. Values between 1-100 are accepted. Defaults to 100
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns a <see cref="UserProfilePhotos"/> object</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<UserProfilePhotos> GetUserProfilePhotosAsync(
        this ITelegramBotClient botClient,
        long userId,
        int? offset = default,
        int? limit = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new GetUserProfilePhotosRequest
                {
                    UserId = userId,
                    Offset = offset,
                    Limit = limit,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get basic info about a file and prepare it for downloading. For the moment, bots can
    /// download files of up to 20MB in size. The file can then be downloaded via the link
    /// <c>https://api.telegram.org/file/bot&lt;token&gt;/&lt;file_path&gt;</c>, where <c>&lt;file_path&gt;</c>
    /// is taken from the response. It is guaranteed that the link will be valid for at least 1 hour.
    /// When the link expires, a new one can be requested by calling
    /// <see cref="GetFileAsync(ITelegramBotClient,GetFileRequest,CancellationToken)"/> again.
    /// </summary>
    /// <remarks>
    /// You can use <see cref="ITelegramBotClient.DownloadFileAsync"/> or
    /// <see cref="TelegramBotClientExtensions.GetInfoAndDownloadFileAsync"/> methods to download the file
    /// </remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="fileId">File identifier to get info about</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, a <see cref="File"/> object is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<File> GetFileAsync(
        this ITelegramBotClient botClient,
        string fileId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new GetFileRequest { FileId = fileId },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to ban a user in a group, a supergroup or a channel. In the case of supergroups and
    /// channels, the user will not be able to return to the chat on their own using invite links, etc., unless
    /// <see cref="UnbanChatMemberAsync(ITelegramBotClient, ChatId, long, bool?, CancellationToken)">unbanned</see>
    /// first. The bot must be an administrator in the chat for this to work and must have the appropriate
    /// admin rights.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target group or username of the target supergroup or channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="untilDate">
    /// Date when the user will be unbanned. If user is banned for more than 366 days or less than 30 seconds
    /// from the current time they are considered to be banned forever. Applied for supergroups and channels only
    /// </param>
    /// <param name="revokeMessages">
    /// Pass <see langword="true"/> to delete all messages from the chat for the user that is being removed.
    /// If <see langword="false"/>, the user will be able to see messages in the group that were sent before the user was
    /// removed. Always <see langword="true"/> for supergroups and channels
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task BanChatMemberAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        DateTime? untilDate = default,
        bool? revokeMessages = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new BanChatMemberRequest
                {
                    ChatId = chatId,
                    UserId = userId,
                    UntilDate = untilDate,
                    RevokeMessages = revokeMessages,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to unban a previously banned user in a supergroup or channel. The user will <b>not</b>
    /// return to the group or channel automatically, but will be able to join via link, etc. The bot must be an
    /// administrator for this to work. By default, this method guarantees that after the call the user is not a
    /// member of the chat, but will be able to join it. So if the user is a member of the chat they will also be
    /// <b>removed</b> from the chat. If you don't want this, use the parameter <paramref name="onlyIfBanned"/>
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target group or username of the target supergroup or channel
    /// (in the format <c>@username</c>)
    /// </param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="onlyIfBanned">Do nothing if the user is not banned</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task UnbanChatMemberAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        bool? onlyIfBanned = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new UnbanChatMemberRequest
                {
                    ChatId = chatId,
                    UserId = userId,
                    OnlyIfBanned = onlyIfBanned,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to restrict a user in a supergroup. The bot must be an administrator in the supergroup
    /// for this to work and must have the appropriate admin rights. Pass <see langword="true"/> for all permissions to
    /// lift restrictions from a user.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target supergroup
    /// (in the format <c>@supergroupusername</c>)
    /// </param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="permissions">New user permissions</param>
    /// <param name="useIndependentChatPermissions">
    /// Pass <see langword="true"/> if chat permissions are set independently. Otherwise, the
    /// <see cref="ChatPermissions.CanSendOtherMessages"/>, and <see cref="ChatPermissions.CanAddWebPagePreviews"/>
    /// permissions will imply the <see cref="ChatPermissions.CanSendMessages"/>,
    /// <see cref="ChatPermissions.CanSendAudios"/>, <see cref="ChatPermissions.CanSendDocuments"/>,
    /// <see cref="ChatPermissions.CanSendPhotos"/>, <see cref="ChatPermissions.CanSendVideos"/>,
    /// <see cref="ChatPermissions.CanSendVideoNotes"/>, and <see cref="ChatPermissions.CanSendVoiceNotes"/>
    /// permissions; the <see cref="ChatPermissions.CanSendPolls"/> permission will imply the
    /// <see cref="ChatPermissions.CanSendMessages"/> permission.
    /// </param>
    /// <param name="untilDate">Date when restrictions will be lifted for this user; Unix time. If 0, then the user is restricted forever</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task RestrictChatMemberAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        ChatPermissions permissions,
        bool? useIndependentChatPermissions = default,
        DateTime? untilDate = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new RestrictChatMemberRequest
                {
                    ChatId = chatId,
                    UserId = userId,
                    Permissions = permissions,
                    UntilDate = untilDate,
                    UseIndependentChatPermissions = useIndependentChatPermissions,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to promote or demote a user in a supergroup or a channel. The bot must be an administrator in
    /// the chat for this to work and must have the appropriate admin rights. Pass <c><see langword="false"/></c> for
    /// all boolean parameters to demote a user.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="isAnonymous">
    /// Pass <see langword="true"/>, if the administrator's presence in the chat is hidden
    /// </param>
    /// <param name="canManageChat">
    /// Pass <see langword="true"/>, if the administrator can access the chat event log, chat statistics, message
    /// statistics in channels, see channel members, see anonymous administrators in supergroups and ignore slow mode.
    /// Implied by any other administrator privilege
    /// </param>
    /// <param name="canPostMessages">
    /// Pass <see langword="true"/>, if the administrator can create channel posts, channels only
    /// </param>
    /// <param name="canEditMessages">
    /// Pass <see langword="true"/>, if the administrator can edit messages of other users, channels only
    /// </param>
    /// <param name="canDeleteMessages">
    /// Pass <see langword="true"/>, if the administrator can delete messages of other users
    /// </param>
    /// <param name="canPostStories">
    /// Pass <see langword="true"/> if the administrator can post stories in the channel; channels only
    /// </param>
    /// <param name="canEditStories">
    /// Pass <see langword="true"/> if the administrator can edit stories posted by other users; channels only
    /// </param>
    /// <param name="canDeleteStories">
    /// Pass <see langword="true"/> if the administrator can delete stories posted by other users; channels only
    /// </param>
    /// <param name="canManageVideoChats">
    /// Pass <see langword="true"/>, if the administrator can manage voice chats, supergroups only
    /// </param>
    /// <param name="canRestrictMembers">
    /// Pass <see langword="true"/>, if the administrator can restrict, ban or unban chat members
    /// </param>
    /// <param name="canPromoteMembers">
    /// Pass <see langword="true"/>, if the administrator can add new administrators with a subset of his own
    /// privileges or demote administrators that he has promoted, directly or indirectly (promoted by administrators
    /// that were appointed by him)
    /// </param>
    /// <param name="canChangeInfo">
    /// Pass <see langword="true"/>, if the administrator can change chat title, photo and other settings
    /// </param>
    /// <param name="canInviteUsers">
    /// Pass <see langword="true"/>, if the administrator can invite new users to the chat
    /// </param>
    /// <param name="canPinMessages">
    /// Pass <see langword="true"/>, if the administrator can pin messages, supergroups only
    /// </param>
    /// <param name="canManageTopic">
    /// Pass <see langword="true"/> if the user is allowed to create, rename, close, and reopen forum topics,
    /// supergroups only
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task PromoteChatMemberAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        bool? isAnonymous = default,
        bool? canManageChat = default,
        bool? canPostMessages = default,
        bool? canEditMessages = default,
        bool? canDeleteMessages = default,
        bool? canPostStories = default,
        bool? canEditStories = default,
        bool? canDeleteStories = default,
        bool? canManageVideoChats = default,
        bool? canRestrictMembers = default,
        bool? canPromoteMembers = default,
        bool? canChangeInfo = default,
        bool? canInviteUsers = default,
        bool? canPinMessages = default,
        bool? canManageTopic = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new PromoteChatMemberRequest
                {
                    ChatId = chatId,
                    UserId = userId,
                    IsAnonymous = isAnonymous,
                    CanManageChat = canManageChat,
                    CanPostMessages = canPostMessages,
                    CanEditMessages = canEditMessages,
                    CanDeleteMessages = canDeleteMessages,
                    CanPostStories = canPostStories,
                    CanEditStories = canEditStories,
                    CanDeleteStories = canDeleteStories,
                    CanManageVideoChat = canManageVideoChats,
                    CanRestrictMembers = canRestrictMembers,
                    CanPromoteMembers = canPromoteMembers,
                    CanChangeInfo = canChangeInfo,
                    CanInviteUsers = canInviteUsers,
                    CanPinMessages = canPinMessages,
                    CanManageTopics = canManageTopic,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set a custom title for an administrator in a supergroup promoted by the bot.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target supergroup
    /// (in the format <c>@supergroupusername</c>)
    /// </param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="customTitle">
    /// New custom title for the administrator; 0-16 characters, emoji are not allowed
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetChatAdministratorCustomTitleAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        string customTitle,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetChatAdministratorCustomTitleRequest
                {
                    ChatId = chatId,
                    UserId = userId,
                    CustomTitle = customTitle,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to ban a channel chat in a supergroup or a channel. The owner of the chat will not be
    /// able to send messages and join live streams on behalf of the chat, unless it is unbanned first. The bot
    /// must be an administrator in the supergroup or channel for this to work and must have the appropriate
    /// administrator rights. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target supergroup
    /// (in the format <c>@supergroupusername</c>)
    /// </param>
    /// <param name="senderChatId">Unique identifier of the target sender chat</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task BanChatSenderChatAsync(this ITelegramBotClient botClient,
        ChatId chatId,
        long senderChatId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new BanChatSenderChatRequest
                {
                    ChatId = chatId,
                    SenderChatId = senderChatId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to unban a previously banned channel chat in a supergroup or channel. The bot must be
    /// an administrator for this to work and must have the appropriate administrator rights.
    /// Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target supergroup
    /// (in the format <c>@supergroupusername</c>)
    /// </param>
    /// <param name="senderChatId">Unique identifier of the target sender chat</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task UnbanChatSenderChatAsync(this ITelegramBotClient botClient,
        ChatId chatId,
        long senderChatId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new UnbanChatSenderChatRequest
                {
                    ChatId = chatId,
                    SenderChatId = senderChatId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set default chat permissions for all members. The bot must be an administrator
    /// in the group or a supergroup for this to work and must have the can_restrict_members admin rights
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target supergroup
    /// (in the format <c>@supergroupusername</c>)
    /// </param>
    /// <param name="permissions">New default chat permissions</param>
    /// <param name="useIndependentChatPermissions">
    /// Pass <see langword="true"/> if chat permissions are set independently. Otherwise, the
    /// <see cref="ChatPermissions.CanSendOtherMessages"/>, and <see cref="ChatPermissions.CanAddWebPagePreviews"/>
    /// permissions will imply the <see cref="ChatPermissions.CanSendMessages"/>,
    /// <see cref="ChatPermissions.CanSendAudios"/>, <see cref="ChatPermissions.CanSendDocuments"/>,
    /// <see cref="ChatPermissions.CanSendPhotos"/>, <see cref="ChatPermissions.CanSendVideos"/>,
    /// <see cref="ChatPermissions.CanSendVideoNotes"/>, and <see cref="ChatPermissions.CanSendVoiceNotes"/>
    /// permissions; the <see cref="ChatPermissions.CanSendPolls"/> permission will imply the
    /// <see cref="ChatPermissions.CanSendMessages"/> permission.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetChatPermissionsAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        ChatPermissions permissions,
        bool? useIndependentChatPermissions = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetChatPermissionsRequest
                {
                    ChatId = chatId,
                    Permissions = permissions,
                    UseIndependentChatPermissions = useIndependentChatPermissions,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to generate a new primary invite link for a chat; any previously generated primary
    /// link is revoked. The bot must be an administrator in the chat for this to work and must have the
    /// appropriate admin rights
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<string> ExportChatInviteLinkAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new ExportChatInviteLinkRequest
                {
                    ChatId = chatId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to create an additional invite link for a chat. The bot must be an administrator
    /// in the chat for this to work and must have the appropriate admin rights. The link can be revoked
    /// using the method
    /// <see cref="RevokeChatInviteLinkAsync(ITelegramBotClient,RevokeChatInviteLinkRequest,CancellationToken)"/>
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="name">Invite link name; 0-32 characters</param>
    /// <param name="expireDate">Point in time when the link will expire</param>
    /// <param name="memberLimit">
    /// Maximum number of users that can be members of the chat simultaneously after joining the chat
    /// via this invite link; 1-99999
    /// </param>
    /// <param name="createsJoinRequest">
    /// Set to <see langword="true"/>, if users joining the chat via the link need to be approved by chat administrators.
    /// If <see langword="true"/>, <paramref name="memberLimit"/> can't be specified
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns the new invite link as <see cref="ChatInviteLink"/> object.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<ChatInviteLink> CreateChatInviteLinkAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string? name = default,
        DateTime? expireDate = default,
        int? memberLimit = default,
        bool? createsJoinRequest = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new CreateChatInviteLinkRequest
                {
                    ChatId = chatId,
                    Name = name,
                    ExpireDate = expireDate,
                    MemberLimit = memberLimit,
                    CreatesJoinRequest = createsJoinRequest,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit a non-primary invite link created by the bot. The bot must be an
    /// administrator in the chat for this to work and must have the appropriate admin rights
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="inviteLink">The invite link to edit</param>
    /// <param name="name">Invite link name; 0-32 characters</param>
    /// <param name="expireDate">Point in time when the link will expire</param>
    /// <param name="memberLimit">
    /// Maximum number of users that can be members of the chat simultaneously after joining the chat
    /// via this invite link; 1-99999
    /// </param>
    /// <param name="createsJoinRequest">
    /// Set to <see langword="true"/>, if users joining the chat via the link need to be approved by chat administrators.
    /// If <see langword="true"/>, <paramref name="memberLimit"/> can't be specified
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns the edited invite link as a <see cref="ChatInviteLink"/> object.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<ChatInviteLink> EditChatInviteLinkAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string inviteLink,
        string? name = default,
        DateTime? expireDate = default,
        int? memberLimit = default,
        bool? createsJoinRequest = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new EditChatInviteLinkRequest
                {
                    ChatId = chatId,
                    InviteLink = inviteLink,
                    Name = name,
                    ExpireDate = expireDate,
                    MemberLimit = memberLimit,
                    CreatesJoinRequest = createsJoinRequest,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to revoke an invite link created by the bot. If the primary link is revoked, a new
    /// link is automatically generated. The bot must be an administrator in the chat for this to work and
    /// must have the appropriate admin rights
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="inviteLink">The invite link to revoke</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns the revoked invite link as <see cref="ChatInviteLink"/> object.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<ChatInviteLink> RevokeChatInviteLinkAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string inviteLink,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new RevokeChatInviteLinkRequest
                {
                    ChatId = chatId,
                    InviteLink = inviteLink,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to approve a chat join request. The bot must be an administrator in the chat for this to
    /// work and must have the <see cref="ChatPermissions.CanInviteUsers"/> administrator right.
    /// Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<bool> ApproveChatJoinRequest(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new ApproveChatJoinRequest
                {
                    ChatId = chatId,
                    UserId = userId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to decline a chat join request. The bot must be an administrator in the chat for this to
    /// work and must have the <see cref="ChatPermissions.CanInviteUsers"/> administrator right.
    /// Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<bool> DeclineChatJoinRequest(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new DeclineChatJoinRequest
                {
                    ChatId = chatId,
                    UserId = userId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set a new profile photo for the chat. Photos can't be changed for private chats.
    /// The bot must be an administrator in the chat for this to work and must have the appropriate admin rights
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="photo">New chat photo, uploaded using multipart/form-data</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetChatPhotoAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFileStream photo,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetChatPhotoRequest
                {
                    ChatId = chatId,
                    Photo = photo,
                },
                cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to delete a chat photo. Photos can't be changed for private chats. The bot must be an
    /// administrator in the chat for this to work and must have the appropriate admin rights
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task DeleteChatPhotoAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new DeleteChatPhotoRequest { ChatId = chatId },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the title of a chat. Titles can't be changed for private chats. The bot
    /// must be an administrator in the chat for this to work and must have the appropriate admin rights
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="title">New chat title, 1-255 characters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetChatTitleAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string title,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetChatTitleRequest
                {
                    ChatId = chatId,
                    Title = title,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the description of a group, a supergroup or a channel. The bot must
    /// be an administrator in the chat for this to work and must have the appropriate admin rights
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="description">New chat Description, 0-255 characters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetChatDescriptionAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string? description = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetChatDescriptionRequest { ChatId = chatId, Description = description },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to add a message to the list of pinned messages in a chat. If the chat is not a private
    /// chat, the bot must be an administrator in the chat for this to work and must have the
    /// '<see cref="ChatMemberAdministrator.CanPinMessages"/>' admin right in a supergroup or
    /// '<see cref="ChatMemberAdministrator.CanEditMessages"/>' admin right in a channel
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">Identifier of a message to pin</param>
    /// <param name="disableNotification">
    /// Pass <c><see langword="true"/></c>, if it is not necessary to send a notification to all chat members about
    /// the new pinned message. Notifications are always disabled in channels and private chats
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task PinChatMessageAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        bool? disableNotification = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull().
            MakeRequestAsync(
                new PinChatMessageRequest
                {
                    ChatId = chatId,
                    MessageId = messageId,
                    DisableNotification = disableNotification,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to remove a message from the list of pinned messages in a chat. If the chat is not
    /// a private chat, the bot must be an administrator in the chat for this to work and must have the
    /// '<see cref="ChatMemberAdministrator.CanPinMessages"/>' admin right in a supergroup or
    /// '<see cref="ChatMemberAdministrator.CanEditMessages"/>' admin right in a channel
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">
    /// Identifier of a message to unpin. If not specified, the most recent pinned message (by sending date)
    /// will be unpinned
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task UnpinChatMessageAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int? messageId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull().
            MakeRequestAsync(
                new UnpinChatMessageRequest { ChatId = chatId, MessageId = messageId },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to clear the list of pinned messages in a chat. If the chat is not a private chat,
    /// the bot must be an administrator in the chat for this to work and must have the
    /// '<see cref="ChatMemberAdministrator.CanPinMessages"/>' admin right in a supergroup or
    /// '<see cref="ChatMemberAdministrator.CanEditMessages"/>' admin right in a channel
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task UnpinAllChatMessages(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull().
            MakeRequestAsync(new UnpinAllChatMessagesRequest { ChatId = chatId }, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method for your bot to leave a group, supergroup or channel.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target supergroup or channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task LeaveChatAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull().
            MakeRequestAsync(new LeaveChatRequest { ChatId = chatId }, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get up to date information about the chat (current name of the user for one-on-one
    /// conversations, current username of a user, group or channel, etc.)
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target supergroup or channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns a <see cref="Chat"/> object on success.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Chat> GetChatAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new GetChatRequest { ChatId = chatId }, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get a list of administrators in a chat.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target supergroup or channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// On success, returns an Array of <see cref="ChatMember"/> objects that contains information about all chat
    /// administrators except other bots. If the chat is a group or a supergroup and no administrators were
    /// appointed, only the creator will be returned
    /// </returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<ChatMember[]> GetChatAdministratorsAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new GetChatAdministratorsRequest { ChatId = chatId }, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get the number of members in a chat.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target supergroup or channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns <see cref="int"/> on success.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<int> GetChatMemberCountAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new GetChatMemberCountRequest { ChatId = chatId }, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get information about a member of a chat.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target supergroup or channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns a <see cref="ChatMember"/> object on success.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<ChatMember> GetChatMemberAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new GetChatMemberRequest { ChatId = chatId, UserId = userId },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set a new group sticker set for a supergroup. The bot must be an administrator in the
    /// chat for this to work and must have the appropriate admin rights. Use the field
    /// <see cref="Chat.CanSetStickerSet"/> optionally returned in
    /// <see cref="GetChatAsync(ITelegramBotClient,GetChatRequest,CancellationToken)"/> requests to check if the bot
    /// can use this method.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="stickerSetName">Name of the sticker set to be set as the group sticker set</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetChatStickerSetAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string stickerSetName,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetChatStickerSetRequest { ChatId = chatId, StickerSetName = stickerSetName },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to delete a group sticker set from a supergroup. The bot must be an administrator in the
    /// chat for this to work and must have the appropriate admin rights. Use the field
    /// <see cref="Chat.CanSetStickerSet"/> optionally returned in
    /// <see cref="GetChatAsync(ITelegramBotClient,GetChatRequest,CancellationToken)"/> requests to check if the bot
    /// can use this method
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task DeleteChatStickerSetAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new DeleteChatStickerSetRequest { ChatId = chatId }, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get custom emoji stickers, which can be used as a forum topic icon by any user.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns an Array of <see cref="Sticker"/> objects.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Sticker[]> GetForumTopicIconStickersAsync(
        this ITelegramBotClient botClient,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new GetForumTopicIconStickersRequest(), cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to create a topic in a forum supergroup chat. The bot must be an administrator in the chat for
    /// this to work and must have the <see cref="ChatAdministratorRights.CanManageTopics"/> administrator rights.
    /// Returns information about the created topic as a <see cref="ForumTopic"/> object.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="name">Topic name, 1-128 characters</param>
    /// <param name="iconColor">
    /// Color of the topic icon in RGB format. Currently, must be one of 7322096 (0x6FB9F0), 16766590 (0xFFD67E),
    /// 13338331 (0xCB86DB), 9367192 (0x8EEE98), 16749490 (0xFF93B2), or 16478047 (0xFB6F5F)
    /// </param>
    /// <param name="iconCustomEmojiId">
    /// Unique identifier of the custom emoji shown as the topic icon. Use
    /// <see cref="GetForumTopicIconStickersAsync(ITelegramBotClient,GetForumTopicIconStickersRequest,CancellationToken)"/>
    /// to get all allowed custom emoji identifiers
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// Returns information about the created topic as a <see cref="ForumTopic"/> object.
    /// </returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<ForumTopic> CreateForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string name,
        Color? iconColor = default,
        string? iconCustomEmojiId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new CreateForumTopicRequest
                {
                    ChatId = chatId,
                    Name = name,
                    IconColor = iconColor,
                    IconCustomEmojiId = iconCustomEmojiId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit name and icon of a topic in a forum supergroup chat. The bot must be an administrator
    /// in the chat for this to work and must have <see cref="ChatAdministratorRights.CanManageTopics"/> administrator
    /// rights, unless it is the creator of the topic. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageThreadId">Unique identifier for the target message thread of the forum topic</param>
    /// <param name="name">
    /// New topic name, 0-128 characters. If not specified or empty, the current name of the topic will be kept
    /// </param>
    /// <param name="iconCustomEmojiId">
    /// New unique identifier of the custom emoji shown as the topic icon. Use
    /// <see cref="GetForumTopicIconStickersRequest"/> to get all allowed custom emoji identifiers. Pass an empty
    /// string to remove the icon. If not specified, the current icon will be kept
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task EditForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageThreadId,
        string? name = default,
        string? iconCustomEmojiId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new EditForumTopicRequest
                {
                    ChatId = chatId,
                    MessageThreadId = messageThreadId,
                    Name = name,
                    IconCustomEmojiId = iconCustomEmojiId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to close an open topic in a forum supergroup chat. The bot must be an administrator in the chat
    /// for this to work and must have the <see cref="ChatAdministratorRights.CanManageTopics"/> administrator rights,
    /// unless it is the creator of the topic. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageThreadId">Unique identifier for the target message thread of the forum topic</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task CloseForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageThreadId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new CloseForumTopicRequest { ChatId = chatId, MessageThreadId = messageThreadId },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to reopen a closed topic in a forum supergroup chat. The bot must be an administrator in the
    /// chat for this to work and must have the <see cref="ChatAdministratorRights.CanManageTopics"/> administrator
    /// rights, unless it is the creator of the topic. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageThreadId">Unique identifier for the target message thread of the forum topic</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task ReopenForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageThreadId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new ReopenForumTopicRequest { ChatId = chatId, MessageThreadId = messageThreadId },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to delete a forum topic along with all its messages in a forum supergroup chat. The bot must be
    /// an administrator in the chat for this to work and must have the
    /// <see cref="ChatAdministratorRights.CanManageTopics"/> administrator rights. Returns <see langword="true"/>
    /// on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageThreadId">Unique identifier for the target message thread of the forum topic</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task DeleteForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageThreadId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new DeleteForumTopicRequest { ChatId = chatId, MessageThreadId = messageThreadId },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to clear the list of pinned messages in a forum topic. The bot must be an administrator in the
    /// chat for this to work and must have the <see cref="ChatAdministratorRights.CanPinMessages"/> administrator
    /// right in the supergroup. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageThreadId">Unique identifier for the target message thread of the forum topic</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task UnpinAllForumTopicMessagesAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageThreadId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new UnpinAllForumTopicMessagesRequest { ChatId = chatId, MessageThreadId = messageThreadId },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit the name of the 'General' topic in a forum supergroup chat. The bot must be an
    /// administrator in the chat for this to work and must have <see cref="ChatAdministratorRights.CanManageTopics"/>
    /// administrator rights. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="name">New topic name, 1-128 characters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task EditGeneralForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string name,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new EditGeneralForumTopicRequest { ChatId = chatId, Name = name },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to close an open 'General' topic in a forum supergroup chat. The bot must be an administrator
    /// in the chat for this to work and must have the <see cref="ChatAdministratorRights.CanManageTopics"/>
    /// administrator rights. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task CloseGeneralForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new CloseGeneralForumTopicRequest { ChatId = chatId }, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to reopen a closed 'General' topic in a forum supergroup chat. The bot must be an
    /// administrator in the chat for this to work and must have the
    /// <see cref="ChatAdministratorRights.CanManageTopics"/> administrator rights. The topic will be automatically
    /// unhidden if it was hidden. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task ReopenGeneralForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new ReopenGeneralForumTopicRequest { ChatId = chatId }, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to hide the 'General' topic in a forum supergroup chat. The bot must be an administrator in the
    /// chat for this to work and must have the <see cref="ChatAdministratorRights.CanManageTopics"/> administrator
    /// rights. The topic will be automatically closed if it was open. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task HideGeneralForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new HideGeneralForumTopicRequest { ChatId = chatId }, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to uhhide the 'General' topic in a forum supergroup chat. The bot must be an administrator
    /// in the chat for this to work and must have the <see cref="ChatAdministratorRights.CanManageTopics"/>
    /// administrator rights. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task UnhideGeneralForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new UnhideGeneralForumTopicRequest { ChatId = chatId }, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to clear the list of pinned messages in a General forum topic. The bot must be an administrator
    /// in the chat for this to work and must have the <see cref="ChatAdministratorRights.CanPinMessages"/>
    /// administrator right in the supergroup.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@supergroupusername</c>)
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task UnpinAllGeneralForumTopicMessagesAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new UnpinAllGeneralForumTopicMessagesRequest { ChatId = chatId }, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send answers to callback queries sent from
    /// <see cref="InlineKeyboardMarkup">inline keyboards</see>. The answer will be displayed
    /// to the user as a notification at the top of the chat screen or as an alert
    /// </summary>
    /// <remarks>
    /// Alternatively, the user can be redirected to the specified Game URL.For this option to work, you must
    /// first create a game for your bot via <c>@BotFather</c> and accept the terms. Otherwise, you may use
    /// links like <c>t.me/your_bot?start=XXXX</c> that open your bot with a parameter
    /// </remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="callbackQueryId">Unique identifier for the query to be answered</param>
    /// <param name="text">
    /// Text of the notification. If not specified, nothing will be shown to the user, 0-200 characters
    /// </param>
    /// <param name="showAlert">
    /// If <see langword="true"/>, an alert will be shown by the client instead of a notification at the top of the chat
    /// screen. Defaults to <see langword="false"/>
    /// </param>
    /// <param name="url">
    /// URL that will be opened by the user's client. If you have created a
    /// <a href="https://core.telegram.org/bots/api#game">Game</a> and accepted the conditions via
    /// <c>@BotFather</c>, specify the URL that opens your game — note that this will only work if the query
    /// comes from a callback_game button
    /// <para>
    /// Otherwise, you may use links like <c>t.me/your_bot?start=XXXX</c> that open your bot with a parameter
    /// </para>
    /// </param>
    /// <param name="cacheTime">
    /// The maximum amount of time in seconds that the result of the callback query may be cached client-side.
    /// Telegram apps will support caching starting in version 3.14
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task AnswerCallbackQueryAsync(
        this ITelegramBotClient botClient,
        string callbackQueryId,
        string? text = default,
        bool? showAlert = default,
        string? url = default,
        int? cacheTime = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new AnswerCallbackQueryRequest
                {
                    CallbackQueryId = callbackQueryId,
                    Text = text,
                    ShowAlert = showAlert,
                    Url = url,
                    CacheTime = cacheTime,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get the list of boosts added to a chat by a user.
    /// Requires administrator rights in the chat.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the chat or username of the channel (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="userId">
    /// Unique identifier of the target user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns a <see cref="UserChatBoosts"/> object.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<UserChatBoosts> GetUserChatBoostsAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new GetUserChatBoostsRequest { ChatId = chatId, UserId = userId }, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the list of the bot’s commands.
    /// See <a href="https://core.telegram.org/bots#commands"/> for more details about bot commands
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="commands">
    /// A list of bot commands to be set as the list of the bot’s commands. At most 100 commands can be specified
    /// </param>
    /// <param name="scope">
    /// An object, describing scope of users for which the commands are relevant.
    /// Defaults to <see cref="BotCommandScopeDefault"/>.
    /// </param>
    /// <param name="languageCode">
    /// A two-letter ISO 639-1 language code. If empty, commands will be applied to all users from the given
    /// <paramref name="scope"/>, for whose language there are no dedicated commands
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetMyCommandsAsync(
        this ITelegramBotClient botClient,
        IEnumerable<BotCommand> commands,
        BotCommandScope? scope = default,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetMyCommandsRequest
                {
                    Commands = commands,
                    Scope = scope,
                    LanguageCode = languageCode,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to delete the list of the bot’s commands for the given <paramref name="scope"/> and
    /// <paramref name="languageCode">user language</paramref>. After deletion,
    /// <a href="https://core.telegram.org/bots/api#determining-list-of-commands">higher level commands</a>
    /// will be shown to affected users
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="scope">
    /// An object, describing scope of users for which the commands are relevant.
    /// Defaults to <see cref="BotCommandScopeDefault"/>.
    /// </param>
    /// <param name="languageCode">
    /// A two-letter ISO 639-1 language code. If empty, commands will be applied to all users from the given
    /// <paramref name="scope"/>, for whose language there are no dedicated commands
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task DeleteMyCommandsAsync(
        this ITelegramBotClient botClient,
        BotCommandScope? scope = default,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new DeleteMyCommandsRequest
                {
                    Scope = scope,
                    LanguageCode = languageCode,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get the current list of the bot’s commands for the given <paramref name="scope"/> and
    /// <paramref name="languageCode">user language</paramref>
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="scope">
    /// An object, describing scope of users. Defaults to <see cref="BotCommandScopeDefault"/>.
    /// </param>
    /// <param name="languageCode">
    /// A two-letter ISO 639-1 language code or an empty string
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// Returns Array of <see cref="BotCommand"/> on success. If commands aren't set, an empty list is returned
    /// </returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<BotCommand[]> GetMyCommandsAsync(
        this ITelegramBotClient botClient,
        BotCommandScope? scope = default,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new GetMyCommandsRequest
                {
                    Scope = scope,
                    LanguageCode = languageCode,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the bot's name.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="name">
    /// New bot name; 0-64 characters. Pass an empty string to remove the dedicated name for the given language.
    /// </param>
    /// <param name="languageCode">
    /// A two-letter ISO 639-1 language code. If empty, the name will be shown to all users for whose language
    /// there is no dedicated name.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetMyNameAsync(
        this ITelegramBotClient botClient,
        string? name = default,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetMyNameRequest { Name = name, LanguageCode = languageCode },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get the current bot name for the given user language.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="languageCode">
    /// A two-letter ISO 639-1 language code or an empty string
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// Returns <see cref="BotName"/> on success.
    /// </returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<BotName> GetMyNameAsync(
        this ITelegramBotClient botClient,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new GetMyNameRequest { LanguageCode = languageCode },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the bot's description, which is shown in the chat
    /// with the bot if the chat is empty.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="description">
    /// New bot description; 0-512 characters. Pass an empty string to remove the
    /// dedicated description for the given language.
    /// </param>
    /// <param name="languageCode">
    /// A two-letter ISO 639-1 language code. If empty, the description will be applied
    /// to all users for whose language there is no dedicated description.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetMyDescriptionAsync(
        this ITelegramBotClient botClient,
        string? description = default,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetMyDescriptionRequest { Description = description, LanguageCode = languageCode },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get the current <see cref="BotDescription">bot description</see>
    /// for the given <paramref name="languageCode">user language</paramref>.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="languageCode">
    /// A two-letter ISO 639-1 language code or an empty string
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// Returns <see cref="BotDescription"/> on success.
    /// </returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<BotDescription> GetMyDescriptionAsync(
        this ITelegramBotClient botClient,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new GetMyDescriptionRequest { LanguageCode = languageCode },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the bot's short description,which is shown on
    /// the bot's profile page and is sent together with the link when users share the bot.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="shortDescription">
    /// New short description for the bot; 0-120 characters.
    /// Pass an empty string to remove the dedicated short description for the given language.
    /// </param>
    /// <param name="languageCode">
    /// A two-letter ISO 639-1 language code. If empty, the short description will be
    /// applied to all users for whose language there is no dedicated short description.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns></returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetMyShortDescriptionAsync(
        this ITelegramBotClient botClient,
        string? shortDescription = default,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetMyShortDescriptionRequest
                {
                    ShortDescription = shortDescription,
                    LanguageCode = languageCode,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get the current bot <see cref="BotShortDescription">short description</see>
    /// for the given <paramref name="languageCode">user language</paramref>.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="languageCode">
    /// A two-letter ISO 639-1 language code or an empty string
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// Returns <see cref="BotShortDescription"/> on success.
    /// </returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<BotShortDescription> GetMyShortDescriptionAsync(
        this ITelegramBotClient botClient,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new GetMyShortDescriptionRequest { LanguageCode = languageCode },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the bot’s menu button in a private chat, or the default menu button.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target private chat. If not specified, default bot’s menu button will be changed
    /// </param>
    /// <param name="menuButton">
    /// An object for the new bot’s menu button. Defaults to <see cref="MenuButtonDefault"/>
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetChatMenuButtonAsync(
        this ITelegramBotClient botClient,
        long? chatId = default,
        MenuButton? menuButton = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetChatMenuButtonRequest { ChatId = chatId, MenuButton = menuButton },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get the current value of the bot’s menu button in a private chat,
    /// or the default menu button.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target private chat. If not specified, default bot’s menu button will be returned
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns><see cref="MenuButton"/> set for the given chat id or a default one</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<MenuButton> GetChatMenuButtonAsync(
        this ITelegramBotClient botClient,
        long? chatId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new GetChatMenuButtonRequest { ChatId = chatId },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the default administrator rights requested by the bot when it's added as an
    /// administrator to groups or channels. These rights will be suggested to users, but they are free to modify
    /// the list before adding the bot.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="rights">
    /// An object describing new default administrator rights. If not specified, the default administrator rights
    /// will be cleared.
    /// </param>
    /// <param name="forChannels">
    /// Pass <see langword="true"/> to change the default administrator rights of the bot in channels. Otherwise, the default
    /// administrator rights of the bot for groups and supergroups will be changed.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetMyDefaultAdministratorRightsAsync(
        this ITelegramBotClient botClient,
        ChatAdministratorRights? rights = default,
        bool? forChannels = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetMyDefaultAdministratorRightsRequest
                {
                    Rights = rights,
                    ForChannels = forChannels,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get the current default administrator rights of the bot.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="forChannels">
    /// Pass <see langword="true"/> to change the default administrator rights of the bot in channels. Otherwise, the default
    /// administrator rights of the bot for groups and supergroups will be changed.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Default or channel <see cref="ChatAdministratorRights"/> </returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<ChatAdministratorRights> GetMyDefaultAdministratorRightsAsync(
        this ITelegramBotClient botClient,
        bool? forChannels = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new GetMyDefaultAdministratorRightsRequest { ForChannels = forChannels },
                cancellationToken
            )
            .ConfigureAwait(false);

    #endregion Available methods

    #region Updating messages

    /// <summary>
    /// Use this method to edit text and game messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">Identifier of the message to edit</param>
    /// <param name="text">New text of the message, 1-4096 characters after entities parsing</param>
    /// <param name="parseMode">
    /// Mode for parsing entities in the new caption. See
    /// <a href="https://core.telegram.org/bots/api#formatting-options">formatting</a> options for
    /// more details
    /// </param>
    /// <param name="entities">
    /// List of special entities that appear in message text, which can be specified instead
    /// of <see cref="ParseMode"/>
    /// </param>
    /// <param name="linkPreviewOptions">Link preview generation options for the message</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success the edited <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> EditMessageTextAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        string text,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? entities = default,
        LinkPreviewOptions? linkPreviewOptions = default,
        InlineKeyboardMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new EditMessageTextRequest
                {
                    ChatId = chatId,
                    MessageId = messageId,
                    Text = text,
                    ParseMode = parseMode,
                    Entities = entities,
                    LinkPreviewOptions = linkPreviewOptions,
                    ReplyMarkup = replyMarkup,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit text and game messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="text">New text of the message, 1-4096 characters after entities parsing</param>
    /// <param name="parseMode">
    /// Mode for parsing entities in the new caption. See
    /// <a href="https://core.telegram.org/bots/api#formatting-options">formatting</a> options for
    /// more details
    /// </param>
    /// <param name="entities">
    /// List of special entities that appear in message text, which can be specified instead
    /// of <see cref="ParseMode"/>
    /// </param>
    /// <param name="linkPreviewOptions">Link preview generation options for the message</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task EditMessageTextAsync(
        this ITelegramBotClient botClient,
        string inlineMessageId,
        string text,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? entities = default,
        LinkPreviewOptions? linkPreviewOptions = default,
        InlineKeyboardMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new EditInlineMessageTextRequest
                {
                    InlineMessageId = inlineMessageId,
                    Text = text,
                    ParseMode = parseMode,
                    Entities = entities,
                    LinkPreviewOptions = linkPreviewOptions,
                    ReplyMarkup = replyMarkup,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit captions of messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">Identifier of the message to edit</param>
    /// <param name="caption">New caption of the message, 0-1024 characters after entities parsing</param>
    /// <param name="parseMode">
    /// Mode for parsing entities in the new caption. See
    /// <a href="https://core.telegram.org/bots/api#formatting-options">formatting</a> options for
    /// more details
    /// </param>
    /// <param name="captionEntities">
    /// List of special entities that appear in the caption, which can be specified instead
    /// of <see cref="ParseMode"/>
    /// </param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success the edited <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> EditMessageCaptionAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        string? caption,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        InlineKeyboardMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new EditMessageCaptionRequest
                {
                    ChatId = chatId,
                    MessageId = messageId,
                    Caption = caption,
                    ParseMode = parseMode,
                    CaptionEntities = captionEntities,
                    ReplyMarkup = replyMarkup,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit captions of messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="caption">New caption of the message, 0-1024 characters after entities parsing</param>
    /// <param name="parseMode">
    /// Mode for parsing entities in the new caption. See
    /// <a href="https://core.telegram.org/bots/api#formatting-options">formatting</a> options for
    /// more details
    /// </param>
    /// <param name="captionEntities">
    /// List of special entities that appear in the caption, which can be specified instead
    /// of <see cref="ParseMode"/>
    /// </param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task EditMessageCaptionAsync(
        this ITelegramBotClient botClient,
        string inlineMessageId,
        string? caption,
        ParseMode? parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        InlineKeyboardMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new EditInlineMessageCaptionRequest
                {
                    InlineMessageId = inlineMessageId,
                    Caption = caption,
                    ParseMode = parseMode,
                    CaptionEntities = captionEntities,
                    ReplyMarkup = replyMarkup,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit animation, audio, document, photo, or video messages. If a message is part of
    /// a message album, then it can be edited only to an audio for audio albums, only to a document for document
    /// albums and to a photo or a video otherwise. Use a previously uploaded file via its
    /// <see cref="InputFileId"/> or specify a URL
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">Identifier of the message to edit</param>
    /// <param name="media">A new media content of the message</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success the edited <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> EditMessageMediaAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        InputMedia media,
        InlineKeyboardMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new EditMessageMediaRequest
                {
                    ChatId = chatId,
                    MessageId = messageId,
                    Media = media,
                    ReplyMarkup = replyMarkup,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit animation, audio, document, photo, or video messages. If a message is part of
    /// a message album, then it can be edited only to an audio for audio albums, only to a document for document
    /// albums and to a photo or a video otherwise. Use a previously uploaded file via its
    /// <see cref="InputFileId"/> or specify a URL
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="media">A new media content of the message</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task EditMessageMediaAsync(
        this ITelegramBotClient botClient,
        string inlineMessageId,
        InputMedia media,
        InlineKeyboardMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new EditInlineMessageMediaRequest
                {
                    InlineMessageId = inlineMessageId,
                    Media = media,
                    ReplyMarkup = replyMarkup,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit only the reply markup of messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">Identifier of the message to edit</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success the edited <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> EditMessageReplyMarkupAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        InlineKeyboardMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new EditMessageReplyMarkupRequest
                {
                    ChatId = chatId,
                    MessageId = messageId,
                    ReplyMarkup = replyMarkup,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit only the reply markup of messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task EditMessageReplyMarkupAsync(
        this ITelegramBotClient botClient,
        string inlineMessageId,
        InlineKeyboardMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new EditInlineMessageReplyMarkupRequest
                {
                    InlineMessageId = inlineMessageId,
                    ReplyMarkup = replyMarkup,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to stop a poll which was sent by the bot.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">Identifier of the original message with the poll</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the stopped <see cref="Poll"/> with the final results is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Poll> StopPollAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        InlineKeyboardMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new StopPollRequest
                {
                    ChatId = chatId,
                    MessageId = messageId,
                    ReplyMarkup = replyMarkup,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to delete a message, including service messages, with the following limitations:
    /// <list type="bullet">
    /// <item>A message can only be deleted if it was sent less than 48 hours ago</item>
    /// <item>A dice message in a private chat can only be deleted if it was sent more than 24 hours ago</item>
    /// <item>Bots can delete outgoing messages in private chats, groups, and supergroups</item>
    /// <item>Bots can delete incoming messages in private chats</item>
    /// <item>Bots granted can_post_messages permissions can delete outgoing messages in channels</item>
    /// <item>If the bot is an administrator of a group, it can delete any message there</item>
    /// <item>
    /// If the bot has can_delete_messages permission in a supergroup or a channel, it can delete any message there
    /// </item>
    /// </list>
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageId">Identifier of the message to delete</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task DeleteMessageAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new DeleteMessageRequest { ChatId = chatId, MessageId = messageId }, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to delete multiple messages simultaneously.
    /// If some of the specified messages can't be found, they are skipped.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="messageIds">
    /// Identifiers of 1-100 messages to delete. See
    /// <see cref="DeleteMessageAsync(ITelegramBotClient,DeleteMessageRequest,CancellationToken)"/> for limitations
    /// on which messages can be deleted
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task DeleteMessagesAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        IEnumerable<int> messageIds,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new DeleteMessagesRequest { ChatId = chatId, MessageIds = messageIds },
                cancellationToken
            )
            .ConfigureAwait(false);

    #endregion Updating messages

    #region Stickers

    /// <summary>
    /// Use this method to send static .WEBP, animated .TGS, or video .WEBM stickers.
    /// </summary>
    /// <param name="botClient">
    /// An instance of <see cref="ITelegramBotClient"/>
    /// </param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="sticker">
    /// Sticker to send. Pass a <see cref="InputFileId"/> as String to send a file that
    /// exists on the Telegram servers (recommended), pass an HTTP URL as a String
    /// for Telegram to get a .WEBP sticker from the Internet, or upload a new .WEBP
    /// or .TGS sticker using multipart/form-data.
    /// Video stickers can only be sent by a <see cref="InputFileId"/>.
    /// Animated stickers can't be sent via an HTTP URL.
    /// </param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="emoji">
    /// Emoji associated with the sticker; only for just uploaded stickers
    /// </param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">
    /// Protects the contents of sent messages from forwarding and saving
    /// </param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="businessConnectionId">
    /// Unique identifier of the business connection on behalf of which the action will be sent
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// On success, the sent <see cref="Message"/> is returned.
    /// </returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> SendStickerAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile sticker,
        int? messageThreadId = default,
        string? emoji = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SendStickerRequest
                {
                    ChatId = chatId,
                    Sticker = sticker,
                    MessageThreadId = messageThreadId,
                    Emoji = emoji,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    ReplyParameters = replyParameters,
                    ReplyMarkup = replyMarkup,
                    BusinessConnectionId = businessConnectionId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get a sticker set.
    /// </summary>
    /// <param name="botClient">
    /// An instance of <see cref="ITelegramBotClient"/>
    /// </param>
    /// <param name="name">
    /// Name of the sticker set
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// On success, a <see cref="StickerSet"/> object is returned.
    /// </returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<StickerSet> GetStickerSetAsync(
        this ITelegramBotClient botClient,
        string name,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new GetStickerSetRequest { Name = name }, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get information about custom emoji stickers by their identifiers.
    /// Returns an Array of <see cref="Sticker"/> objects.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="customEmojiIds">List of custom emoji identifiers. At most 200 custom emoji
    /// identifiers can be specified.</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, a <see cref="StickerSet"/> object is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Sticker[]> GetCustomEmojiStickersAsync(
        this ITelegramBotClient botClient,
        IEnumerable<string> customEmojiIds,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new GetCustomEmojiStickersRequest { CustomEmojiIds = customEmojiIds },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to upload a file with a sticker for later use in the
    /// <see cref="CreateNewStickerSetRequest"/> and <see cref="AddStickerToSetRequest"/>
    /// methods (the file can be used multiple times).
    /// </summary>
    /// <param name="botClient">
    /// An instance of <see cref="ITelegramBotClient"/>
    /// </param>
    /// <param name="userId">
    /// User identifier of sticker file owner
    /// </param>
    /// <param name="sticker">
    /// A file with the sticker in .WEBP, .PNG, .TGS, or .WEBM format.
    /// </param>
    /// <param name="stickerFormat">
    /// Format of the sticker
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// Returns the uploaded <see cref="File"/> on success.
    /// </returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<File> UploadStickerFileAsync(
        this ITelegramBotClient botClient,
        long userId,
        InputFileStream sticker,
        StickerFormat stickerFormat,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new UploadStickerFileRequest
                {
                    UserId = userId,
                    Sticker = sticker,
                    StickerFormat = stickerFormat,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to create a new sticker set owned by a user.
    /// </summary>
    /// <param name="botClient">
    /// An instance of <see cref="ITelegramBotClient"/>
    /// </param>
    /// <param name="userId">
    /// User identifier of created sticker set owner
    /// </param>
    /// <param name="name">
    /// Short name of sticker set, to be used in <c>t.me/addstickers/</c> URLs (e.g., <i>animals</i>). Can contain
    /// only English letters, digits and underscores. Must begin with a letter, can't contain consecutive
    /// underscores and must end in <i>"_by_&lt;bot username&gt;"</i>. <i>&lt;bot_username&gt;</i> is case
    /// insensitive. 1-64 characters
    /// </param>
    /// <param name="title">
    /// Sticker set title, 1-64 characters
    /// </param>
    /// <param name="stickers">
    /// A list of 1-50 initial stickers to be added to the sticker set
    /// </param>
    /// <param name="stickerFormat">
    /// Format of stickers in the set.
    /// </param>
    /// <param name="stickerType">
    /// Type of stickers in the set.
    /// By default, a regular sticker set is created.
    /// </param>
    /// <param name="needsRepainting">
    /// Pass <see langword="true"/> if stickers in the sticker set must be repainted to the
    /// color of text when used in messages, the accent color if used as emoji status, white
    /// on chat photos, or another appropriate color based on context;
    /// for <see cref="StickerType.CustomEmoji">custom emoji</see> sticker sets only
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task CreateNewStickerSetAsync(
        this ITelegramBotClient botClient,
        long userId,
        string name,
        string title,
        IEnumerable<InputSticker> stickers,
        StickerFormat stickerFormat,
        StickerType? stickerType = default,
        bool? needsRepainting = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new CreateNewStickerSetRequest
                {
                    UserId = userId,
                    Name = name,
                    Title = title,
                    Stickers = stickers,
                    StickerFormat = stickerFormat,
                    NeedsRepainting = needsRepainting,
                    StickerType = stickerType,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to add a new sticker to a set created by the bot.
    /// The format of the added sticker must match the format of the other stickers in the set.
    /// <list type="bullet">
    /// <item>
    /// Emoji sticker sets can have up to 200 stickers.
    /// </item>
    /// <item>
    /// Animated and video sticker sets can have up to 50 stickers.
    /// </item>
    /// <item>
    /// Static sticker sets can have up to 120 stickers.
    /// </item>
    /// </list>
    /// </summary>
    /// <param name="botClient">
    /// An instance of <see cref="ITelegramBotClient"/>
    /// </param>
    /// <param name="userId">
    /// User identifier of sticker set owner
    /// </param>
    /// <param name="name">
    /// Sticker set name
    /// </param>
    /// <param name="sticker">
    /// An object with information about the added sticker.
    /// If exactly the same sticker had already been added to the set, then the set isn't changed.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task AddStickerToSetAsync(
        this ITelegramBotClient botClient,
        long userId,
        string name,
        InputSticker sticker,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new AddStickerToSetRequest { UserId = userId, Name = name, Sticker = sticker, },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to move a sticker in a set created by the bot to a specific position.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="sticker">
    /// <see cref="InputFileId">File identifier</see> of the sticker
    /// </param>
    /// <param name="position">New sticker position in the set, zero-based</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetStickerPositionInSetAsync(
        this ITelegramBotClient botClient,
        InputFileId sticker,
        int position,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetStickerPositionInSetRequest { Sticker = sticker, Position = position, },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to delete a sticker from a set created by the bot.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="sticker">
    /// <see cref="InputFileId">File identifier</see> of the sticker
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task DeleteStickerFromSetAsync(
        this ITelegramBotClient botClient,
        InputFileId sticker,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new DeleteStickerFromSetRequest { Sticker = sticker },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the list of emoji assigned to a regular or custom emoji sticker.
    /// The sticker must belong to a sticker set created by the bot.
    /// </summary>
    /// <param name="botClient">
    /// An instance of <see cref="ITelegramBotClient"/>
    /// </param>
    /// <param name="sticker">
    /// <see cref="InputFileId">File identifier</see> of the sticker
    /// </param>
    /// <param name="emojiList">
    /// A list of 1-20 emoji associated with the sticker
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetStickerEmojiListAsync(
        this ITelegramBotClient botClient,
        InputFileId sticker,
        IEnumerable<string> emojiList,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetStickerEmojiListRequest { Sticker = sticker, EmojiList = emojiList },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change search keywords assigned to a regular or custom emoji sticker.
    /// The sticker must belong to a sticker set created by the bot.
    /// </summary>
    /// <param name="botClient">
    /// An instance of <see cref="ITelegramBotClient"/>
    /// </param>
    /// <param name="sticker">
    /// <see cref="InputFileId">File identifier</see> of the sticker
    /// </param>
    /// <param name="keywords">
    /// Optional. A list of 0-20 search keywords for the sticker
    /// with total length of up to 64 characters
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetStickerKeywordsAsync(
        this ITelegramBotClient botClient,
        InputFileId sticker,
        IEnumerable<string>? keywords = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetStickerKeywordsRequest { Sticker = sticker, Keywords = keywords, },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the mask position of a mask sticker.
    /// The sticker must belong to a sticker set that was created by the bot.
    /// </summary>
    /// <param name="botClient">
    /// An instance of <see cref="ITelegramBotClient"/>
    /// </param>
    /// <param name="sticker">
    /// <see cref="InputFileId">File identifier</see> of the sticker
    /// </param>
    /// <param name="maskPosition">
    /// n object with the position where the mask should be placed on faces.
    /// <see cref="Nullable">Omit</see> the parameter to remove the mask position.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetStickerMaskPositionAsync(
        this ITelegramBotClient botClient,
        InputFileId sticker,
        MaskPosition? maskPosition = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetStickerMaskPositionRequest { Sticker = sticker, MaskPosition = maskPosition },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set the title of a created sticker set.
    /// </summary>
    /// <param name="botClient">
    /// An instance of <see cref="ITelegramBotClient"/>
    /// </param>
    /// <param name="name">
    /// Sticker set name
    /// </param>
    /// <param name="title">
    /// Sticker set title, 1-64 characters
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetStickerSetTitleAsync(
        this ITelegramBotClient botClient,
        string name,
        string title,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetStickerSetTitleRequest { Name = name, Title = title },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set the thumbnail of a regular or mask sticker set.
    /// The format of the thumbnail file must match the format of the stickers in the set.
    /// Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">
    /// An instance of <see cref="ITelegramBotClient"/>
    /// </param>
    /// <param name="name">
    /// Sticker set name
    /// </param>
    /// <param name="userId">
    /// User identifier of the sticker set owner
    /// </param>
    /// <param name="format">Format of the thumbnail</param>
    /// <param name="thumbnail">
    /// A <b>.WEBP</b> or <b>.PNG</b> image with the thumbnail, must be up to 128 kilobytes in size and have
    /// a width and height of exactly 100px, or a <b>.TGS</b> animation with a thumbnail up to 32 kilobytes in
    /// size (see <a href="https://core.telegram.org/animated_stickers#technical-requirements"/> for animated
    /// sticker technical requirements), or a <b>WEBM</b> video with the thumbnail up to 32 kilobytes in size; see
    /// <a href="https://core.telegram.org/stickers#video-sticker-requirements"/> for video sticker technical
    /// requirements. Pass a <see cref="InputFileId"/> as a String to send a file that already exists on the
    /// Telegram servers, pass an HTTP URL as a String for Telegram to get a file from the Internet, or
    /// upload a new one using multipart/form-data. Animated and video sticker set thumbnails can't be uploaded
    /// via HTTP URL. If omitted, then the thumbnail is dropped and the first sticker is used as the thumbnail.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetStickerSetThumbnailAsync(
        this ITelegramBotClient botClient,
        string name,
        long userId,
        StickerFormat format,
        InputFile? thumbnail = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetStickerSetThumbnailRequest
                {
                    Name = name,
                    UserId = userId,
                    Thumbnail = thumbnail,
                    Format = format,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set the thumbnail of a custom emoji sticker set.
    /// </summary>
    /// <param name="botClient">
    /// An instance of <see cref="ITelegramBotClient"/>
    /// </param>
    /// <param name="name">
    /// Sticker set name
    /// </param>
    /// <param name="customEmojiId">
    /// Custom emoji identifier of a <see cref="Sticker"/> from the <see cref="StickerSet"/>;
    /// pass an <see langword="null"/> to drop the thumbnail and use the first sticker as the thumbnail.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetCustomEmojiStickerSetThumbnailAsync(
        this ITelegramBotClient botClient,
        string name,
        string? customEmojiId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetCustomEmojiStickerSetThumbnailRequest { Name = name, CustomEmojiId = customEmojiId },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to delete a sticker set that was created by the bot.
    /// </summary>
    /// <param name="botClient">
    /// An instance of <see cref="ITelegramBotClient"/>
    /// </param>
    /// <param name="name">
    /// Sticker set name
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task DeleteStickerSetAsync(
        this ITelegramBotClient botClient,
        string name,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new DeleteStickerSetRequest { Name = name }, cancellationToken)
            .ConfigureAwait(false);
    #endregion

    #region Inline mode

    /// <summary>
    /// Use this method to send answers to an inline query.
    /// </summary>
    /// <remarks>
    /// No more than <b>50</b> results per query are allowed.
    /// </remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="inlineQueryId">Unique identifier for the answered query</param>
    /// <param name="results">An array of results for the inline query</param>
    /// <param name="cacheTime">
    /// The maximum amount of time in seconds that the result of the inline query may be cached on the server.
    /// Defaults to 300
    /// </param>
    /// <param name="isPersonal">
    /// Pass <see langword="true"/>, if results may be cached on the server side only for the user that sent the query.
    /// By default, results may be returned to any user who sends the same query
    /// </param>
    /// <param name="nextOffset">
    /// Pass the offset that a client should send in the next query with the same text to receive more results.
    /// Pass an empty string if there are no more results or if you don't support pagination.
    /// Offset length can't exceed 64 bytes
    /// </param>
    /// <param name="button">
    /// An object describing a button to be shown above inline query results
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task AnswerInlineQueryAsync(
        this ITelegramBotClient botClient,
        string inlineQueryId,
        IEnumerable<InlineQueryResult> results,
        int? cacheTime = default,
        bool? isPersonal = default,
        string? nextOffset = default,
        InlineQueryResultsButton? button = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new AnswerInlineQueryRequest
                {
                    InlineQueryId = inlineQueryId,
                    Results = results,
                    CacheTime = cacheTime,
                    IsPersonal = isPersonal,
                    NextOffset = nextOffset,
                    Button = button,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set the result of an interaction with a Web App and send a corresponding message on
    /// behalf of the user to the chat from which the query originated. On success, a <see cref="SentWebAppMessage"/>
    /// object is returned.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="webAppQueryId">Unique identifier for the query to be answered</param>
    /// <param name="result">
    /// An object describing the message to be sent
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task AnswerWebAppQueryAsync(
        this ITelegramBotClient botClient,
        string webAppQueryId,
        InlineQueryResult result,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new AnswerWebAppQueryRequest { WebAppQueryId = webAppQueryId, Result = result },
                cancellationToken
            )
            .ConfigureAwait(false);

    #endregion Inline mode

    #region Payments

    /// <summary>
    /// Use this method to send invoices.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">
    /// Unique identifier for the target chat or username of the target channel
    /// (in the format <c>@channelusername</c>)
    /// </param>
    /// <param name="title">Product name, 1-32 characters</param>
    /// <param name="description">Product description, 1-255 characters</param>
    /// <param name="payload">
    /// Bot-defined invoice payload, 1-128 bytes. This will not be displayed to the user,
    /// use for your internal processes
    /// </param>
    /// <param name="providerToken">
    /// Payments provider token, obtained via <a href="https://t.me/botfather">@BotFather</a>
    /// </param>
    /// <param name="currency">
    /// Three-letter ISO 4217 currency code, see
    /// <a href="https://core.telegram.org/bots/payments#supported-currencies">more on currencies</a>
    /// </param>
    /// <param name="prices">
    /// Price breakdown, a list of components (e.g. product price, tax, discount, delivery cost, delivery tax,
    /// bonus, etc.)
    /// </param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="maxTipAmount">
    /// The maximum accepted amount for tips in the smallest units of the currency (integer, not float/double).
    /// For example, for a maximum tip of <c>US$ 1.45</c> pass <c><paramref name="maxTipAmount"/> = 145</c>.
    /// See the <i>exp</i> parameter in
    /// <a href="https://core.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the
    /// number of digits past the decimal point for each currency (2 for the majority of currencies).
    /// Defaults to 0
    /// </param>
    /// <param name="suggestedTipAmounts">
    /// An array of suggested amounts of tips in the <i>smallest units</i> of the currency (integer,
    /// <b>not</b> float/double). At most 4 suggested tip amounts can be specified. The suggested tip amounts must
    /// be positive, passed in a strictly increased order and must not exceed <paramref name="maxTipAmount"/>
    /// </param>
    /// <param name="startParameter">
    /// Unique deep-linking parameter. If left empty, <b>forwarded copies</b> of the sent message will have
    /// a <i>Pay</i> button, allowing multiple users to pay directly from the forwarded message, using the same
    /// invoice. If non-empty, forwarded copies of the sent message will have a <i>URL</i> button with a deep
    /// link to the bot (instead of a <i>Pay</i> button), with the value used as the start parameter
    /// </param>
    /// <param name="providerData">
    /// A data about the invoice, which will be shared with the payment provider. A detailed
    /// description of required fields should be provided by the payment provide
    /// </param>
    /// <param name="photoUrl">
    /// URL of the product photo for the invoice. Can be a photo of the goods or a marketing image for a service.
    /// People like it better when they see what they are paying for
    /// </param>
    /// <param name="photoSize">Photo size</param>
    /// <param name="photoWidth">Photo width</param>
    /// <param name="photoHeight">Photo height</param>
    /// <param name="needName">Pass <see langword="true"/>, if you require the user's full name to complete the order</param>
    /// <param name="needPhoneNumber">
    /// Pass <see langword="true"/>, if you require the user's phone number to complete the order
    /// </param>
    /// <param name="needEmail">Pass <see langword="true"/>, if you require the user's email to complete the order</param>
    /// <param name="needShippingAddress">
    /// Pass <see langword="true"/>, if you require the user's shipping address to complete the order
    /// </param>
    /// <param name="sendPhoneNumberToProvider">
    /// Pass <see langword="true"/>, if user's phone number should be sent to provider
    /// </param>
    /// <param name="sendEmailToProvider">
    /// Pass <see langword="true"/>, if user's email address should be sent to provider
    /// </param>
    /// <param name="isFlexible">Pass <see langword="true"/>, if the final price depends on the shipping method</param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">Protects the contents of sent messages from forwarding and saving</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> SendInvoiceAsync(
        this ITelegramBotClient botClient,
        long chatId,
        string title,
        string description,
        string payload,
        string providerToken,
        string currency,
        IEnumerable<LabeledPrice> prices,
        int? messageThreadId = default,
        int? maxTipAmount = default,
        IEnumerable<int>? suggestedTipAmounts = default,
        string? startParameter = default,
        string? providerData = default,
        string? photoUrl = default,
        int? photoSize = default,
        int? photoWidth = default,
        int? photoHeight = default,
        bool? needName = default,
        bool? needPhoneNumber = default,
        bool? needEmail = default,
        bool? needShippingAddress = default,
        bool? sendPhoneNumberToProvider = default,
        bool? sendEmailToProvider = default,
        bool? isFlexible = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        ReplyParameters? replyParameters = default,
        InlineKeyboardMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SendInvoiceRequest
                {
                    ChatId = chatId,
                    Title = title,
                    Description = description,
                    Payload = payload,
                    ProviderToken = providerToken,
                    Currency = currency,
                    Prices = prices,
                    MessageThreadId = messageThreadId,
                    MaxTipAmount = maxTipAmount,
                    SuggestedTipAmounts = suggestedTipAmounts,
                    StartParameter = startParameter,
                    ProviderData = providerData,
                    PhotoUrl = photoUrl,
                    PhotoSize = photoSize,
                    PhotoWidth = photoWidth,
                    PhotoHeight = photoHeight,
                    NeedName = needName,
                    NeedPhoneNumber = needPhoneNumber,
                    NeedEmail = needEmail,
                    NeedShippingAddress = needShippingAddress,
                    SendPhoneNumberToProvider = sendPhoneNumberToProvider,
                    SendEmailToProvider = sendEmailToProvider,
                    IsFlexible = isFlexible,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    ReplyParameters = replyParameters,
                    ReplyMarkup = replyMarkup,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to create a link for an invoice.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="title">Product name, 1-32 characters</param>
    /// <param name="description">Product description, 1-255 characters</param>
    /// <param name="payload">
    /// Bot-defined invoice payload, 1-128 bytes. This will not be displayed to the user,
    /// use for your internal processes
    /// </param>
    /// <param name="providerToken">
    /// Payments provider token, obtained via <a href="https://t.me/botfather">@BotFather</a>
    /// </param>
    /// <param name="currency">
    /// Three-letter ISO 4217 currency code, see
    /// <a href="https://core.telegram.org/bots/payments#supported-currencies">more on currencies</a>
    /// </param>
    /// <param name="prices">
    /// Price breakdown, a list of components (e.g. product price, tax, discount, delivery cost, delivery tax,
    /// bonus, etc.)
    /// </param>
    /// <param name="maxTipAmount">
    /// The maximum accepted amount for tips in the smallest units of the currency (integer, not float/double).
    /// For example, for a maximum tip of <c>US$ 1.45</c> pass <c><paramref name="maxTipAmount"/> = 145</c>.
    /// See the <i>exp</i> parameter in
    /// <a href="https://core.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the
    /// number of digits past the decimal point for each currency (2 for the majority of currencies).
    /// Defaults to 0
    /// </param>
    /// <param name="suggestedTipAmounts">
    /// An array of suggested amounts of tips in the <i>smallest units</i> of the currency (integer,
    /// <b>not</b> float/double). At most 4 suggested tip amounts can be specified. The suggested tip amounts must
    /// be positive, passed in a strictly increased order and must not exceed <paramref name="maxTipAmount"/>
    /// </param>
    /// <param name="providerData">
    /// Data about the invoice, which will be shared with the payment provider. A detailed
    /// description of required fields should be provided by the payment provide
    /// </param>
    /// <param name="photoUrl">
    /// URL of the product photo for the invoice. Can be a photo of the goods or a marketing image for a service.
    /// </param>
    /// <param name="photoSize">Photo size</param>
    /// <param name="photoWidth">Photo width</param>
    /// <param name="photoHeight">Photo height</param>
    /// <param name="needName">Pass <see langword="true"/>, if you require the user's full name to complete the order</param>
    /// <param name="needPhoneNumber">
    /// Pass <see langword="true"/>, if you require the user's phone number to complete the order
    /// </param>
    /// <param name="needEmail">Pass <see langword="true"/>, if you require the user's email to complete the order</param>
    /// <param name="needShippingAddress">
    /// Pass <see langword="true"/>, if you require the user's shipping address to complete the order
    /// </param>
    /// <param name="sendPhoneNumberToProvider">
    /// Pass <see langword="true"/>, if user's phone number should be sent to provider
    /// </param>
    /// <param name="sendEmailToProvider">
    /// Pass <see langword="true"/>, if user's email address should be sent to provider
    /// </param>
    /// <param name="isFlexible">Pass <see langword="true"/>, if the final price depends on the shipping method</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<string> CreateInvoiceLinkAsync(
        this ITelegramBotClient botClient,
        string title,
        string description,
        string payload,
        string providerToken,
        string currency,
        IEnumerable<LabeledPrice> prices,
        int? maxTipAmount = default,
        IEnumerable<int>? suggestedTipAmounts = default,
        string? providerData = default,
        string? photoUrl = default,
        int? photoSize = default,
        int? photoWidth = default,
        int? photoHeight = default,
        bool? needName = default,
        bool? needPhoneNumber = default,
        bool? needEmail = default,
        bool? needShippingAddress = default,
        bool? sendPhoneNumberToProvider = default,
        bool? sendEmailToProvider = default,
        bool? isFlexible = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new CreateInvoiceLinkRequest
                {
                    Title = title,
                    Description = description,
                    Payload = payload,
                    ProviderToken = providerToken,
                    Currency = currency,
                    Prices = prices,
                    MaxTipAmount = maxTipAmount,
                    SuggestedTipAmounts = suggestedTipAmounts,
                    ProviderData = providerData,
                    PhotoUrl = photoUrl,
                    PhotoSize = photoSize,
                    PhotoWidth = photoWidth,
                    PhotoHeight = photoHeight,
                    NeedName = needName,
                    NeedPhoneNumber = needPhoneNumber,
                    NeedEmail = needEmail,
                    NeedShippingAddress = needShippingAddress,
                    SendPhoneNumberToProvider = sendPhoneNumberToProvider,
                    SendEmailToProvider = sendEmailToProvider,
                    IsFlexible = isFlexible,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// If you sent an invoice requesting a shipping address and the parameter <c>isFlexible"</c> was specified,
    /// the Bot API will send an <see cref="Update"/> with a <see cref="ShippingQuery"/> field
    /// to the bot. Use this method to reply to shipping queries
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="shippingQueryId">Unique identifier for the query to be answered</param>
    /// <param name="shippingOptions">
    /// Required if ok is <see langword="true"/>. An array of available shipping options
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task AnswerShippingQueryAsync(
        this ITelegramBotClient botClient,
        string shippingQueryId,
        IEnumerable<ShippingOption> shippingOptions,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new AnswerShippingQueryRequest(shippingQueryId, shippingOptions),
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// If you sent an invoice requesting a shipping address and the parameter <c>isFlexible"</c> was specified,
    /// the Bot API will send an <see cref="Update"/> with a <see cref="ShippingQuery"/> field
    /// to the bot. Use this method to indicate failed shipping query
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="shippingQueryId">Unique identifier for the query to be answered</param>
    /// <param name="errorMessage">
    /// Required if <see cref="AnswerShippingQueryRequest.Ok"/> is <see langword="false"/>. Error message in
    /// human readable form that explains why it is impossible to complete the order (e.g. "Sorry, delivery to
    /// your desired address is unavailable'). Telegram will display this message to the user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task AnswerShippingQueryAsync(
        this ITelegramBotClient botClient,
        string shippingQueryId,
        string errorMessage,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new AnswerShippingQueryRequest(shippingQueryId, errorMessage),
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Once the user has confirmed their payment and shipping details, the Bot API sends the final confirmation
    /// in the form of an <see cref="Update"/> with the field <see cref="PreCheckoutQuery"/>.
    /// Use this method to respond to such pre-checkout queries.
    /// </summary>
    /// <remarks>
    /// <b>Note</b>: The Bot API must receive an answer within 10 seconds after the pre-checkout query was sent.
    /// </remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="preCheckoutQueryId">Unique identifier for the query to be answered</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task AnswerPreCheckoutQueryAsync(
        this ITelegramBotClient botClient,
        string preCheckoutQueryId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(new AnswerPreCheckoutQueryRequest(preCheckoutQueryId), cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Once the user has confirmed their payment and shipping details, the Bot API sends the final confirmation
    /// in the form of an <see cref="Update"/> with the field <see cref="PreCheckoutQuery"/>.
    /// Use this method to respond to indicate failed pre-checkout query
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="preCheckoutQueryId">Unique identifier for the query to be answered</param>
    /// <param name="errorMessage">
    /// Required if <see cref="AnswerPreCheckoutQueryRequest.Ok"/> is <see langword="false"/>. Error message in
    /// human readable form that explains the reason for failure to proceed with the checkout (e.g. "Sorry,
    /// somebody just bought the last of our amazing black T-shirts while you were busy filling out your payment
    /// details. Please choose a different color or garment!"). Telegram will display this message to the user
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task AnswerPreCheckoutQueryAsync(
        this ITelegramBotClient botClient,
        string preCheckoutQueryId,
        string errorMessage,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new AnswerPreCheckoutQueryRequest(preCheckoutQueryId, errorMessage),
                cancellationToken
            )
            .ConfigureAwait(false);

    #endregion Payments

    #region Games

    /// <summary>
    /// Use this method to send a game.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat</param>
    /// <param name="messageThreadId">
    /// Unique identifier for the target message thread (topic) of the forum; for forum supergroups only
    /// </param>
    /// <param name="gameShortName">
    /// Short name of the game, serves as the unique identifier for the game. Set up your games via
    /// <a href="https://t.me/botfather">@BotFather</a>
    /// </param>
    /// <param name="disableNotification">
    /// Sends the message silently. Users will receive a notification with no sound
    /// </param>
    /// <param name="protectContent">Protects the contents of sent messages from forwarding and saving</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">
    /// Additional interface options. An <see cref="InlineKeyboardMarkup">inline keyboard</see>,
    /// <see cref="ReplyKeyboardMarkup">custom reply keyboard</see>, instructions to
    /// <see cref="ReplyKeyboardRemove">remove reply keyboard</see> or to
    /// <see cref="ForceReplyMarkup">force a reply</see> from the user
    /// </param>
    /// <param name="businessConnectionId">
    /// Unique identifier of the business connection on behalf of which the message will be sent
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> SendGameAsync(
        this ITelegramBotClient botClient,
        long chatId,
        string gameShortName,
        int? messageThreadId = default,
        bool? disableNotification = default,
        bool? protectContent = default,
        ReplyParameters? replyParameters = default,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SendGameRequest
                {
                    ChatId = chatId,
                    GameShortName = gameShortName,
                    MessageThreadId = messageThreadId,
                    DisableNotification = disableNotification,
                    ProtectContent = protectContent,
                    ReplyParameters = replyParameters,
                    ReplyMarkup = replyMarkup,
                    BusinessConnectionId = businessConnectionId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set the score of the specified user in a game.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">User identifier</param>
    /// <param name="score">New score, must be non-negative</param>
    /// <param name="chatId">Unique identifier for the target chat</param>
    /// <param name="messageId">Identifier of the sent message</param>
    /// <param name="force">
    /// Pass <see langword="true"/>, if the high score is allowed to decrease. This can be useful when fixing mistakes
    /// or banning cheaters
    /// </param>
    /// <param name="disableEditMessage">
    /// Pass <see langword="true"/>, if the game message should not be automatically edited to include the current scoreboard
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// On success returns the edited <see cref="Message"/>. Returns an error, if the new score is not greater
    /// than the user's current score in the chat and <paramref name="force"/> is <see langword="false"/>
    /// </returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<Message> SetGameScoreAsync(
        this ITelegramBotClient botClient,
        long userId,
        int score,
        long chatId,
        int messageId,
        bool? force = default,
        bool? disableEditMessage = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetGameScoreRequest
                {
                    UserId = userId,
                    Score = score,
                    ChatId = chatId,
                    MessageId = messageId,
                    Force = force,
                    DisableEditMessage = disableEditMessage,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set the score of the specified user in a game.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">User identifier</param>
    /// <param name="score">New score, must be non-negative</param>
    /// <param name="inlineMessageId">Identifier of the inline message.</param>
    /// <param name="force">
    /// Pass <see langword="true"/>, if the high score is allowed to decrease. This can be useful when fixing mistakes
    /// or banning cheaters
    /// </param>
    /// <param name="disableEditMessage">
    /// Pass <see langword="true"/>, if the game message should not be automatically edited to include the current scoreboard
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// Returns an error, if the new score is not greater than the user's current score in the chat and
    /// <paramref name="force"/> is <see langword="false"/>
    /// </returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task SetGameScoreAsync(
        this ITelegramBotClient botClient,
        long userId,
        int score,
        string inlineMessageId,
        bool? force = default,
        bool? disableEditMessage = default,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new SetInlineGameScoreRequest
                {
                    UserId = userId,
                    Score = score,
                    InlineMessageId = inlineMessageId,
                    Force = force,
                    DisableEditMessage = disableEditMessage,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get data for high score tables. Will return the score of the specified user and
    /// several of their neighbors in a game.
    /// </summary>
    /// <remarks>
    /// This method will currently return scores for the target user, plus two of their closest neighbors on
    /// each side. Will also return the top three users if the user and his neighbors are not among them.
    /// Please note that this behavior is subject to change.
    /// </remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">Target user id</param>
    /// <param name="chatId">Unique identifier for the target chat</param>
    /// <param name="messageId">Identifier of the sent message</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, returns an Array of <see cref="GameHighScore"/> objects.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<GameHighScore[]> GetGameHighScoresAsync(
        this ITelegramBotClient botClient,
        long userId,
        long chatId,
        int messageId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new GetGameHighScoresRequest
                {
                    ChatId = chatId,
                    UserId = userId,
                    MessageId = messageId,
                },
                cancellationToken
            )
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get data for high score tables. Will return the score of the specified user and
    /// several of their neighbors in a game.
    /// </summary>
    /// <remarks>
    /// This method will currently return scores for the target user, plus two of their closest neighbors
    /// on each side. Will also return the top three users if the user and his neighbors are not among them.
    /// Please note that this behavior is subject to change.
    /// </remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">User identifier</param>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, returns an Array of <see cref="GameHighScore"/> objects.</returns>
    [Obsolete("Use the overload that accepts the corresponding request class")]
    public static async Task<GameHighScore[]> GetGameHighScoresAsync(
        this ITelegramBotClient botClient,
        long userId,
        string inlineMessageId,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(
                new GetInlineGameHighScoresRequest { UserId = userId, InlineMessageId = inlineMessageId },
                cancellationToken
            )
            .ConfigureAwait(false);

    #endregion Games
}
