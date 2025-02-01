using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Requests;

namespace Telegram.Bot;

/// <summary>Extension methods that map to requests from Bot API documentation</summary>
public static partial class TelegramBotClientExtensions
{
    #region Getting updates

    /// <summary>Use this method to receive incoming updates using long polling (<a href="https://en.wikipedia.org/wiki/Push_technology#Long_polling">wiki</a>).</summary>
    /// <remarks><b>Notes</b><br/><b>1.</b> This method will not work if an outgoing webhook is set up.<br/><b>2.</b> In order to avoid getting duplicate updates, recalculate <paramref name="offset"/> after each server response.</remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="offset">Identifier of the first update to be returned. Must be greater by one than the highest among the identifiers of previously received updates. By default, updates starting with the earliest unconfirmed update are returned. An update is considered confirmed as soon as <see cref="TelegramBotClientExtensions.GetUpdates">GetUpdates</see> is called with an <paramref name="offset"/> higher than its <em>UpdateId</em>. The negative offset can be specified to retrieve updates starting from <em>-offset</em> update from the end of the updates queue. All previous updates will be forgotten.</param>
    /// <param name="limit">Limits the number of updates to be retrieved. Values between 1-100 are accepted. Defaults to 100.</param>
    /// <param name="timeout">Timeout in seconds for long polling. Defaults to 0, i.e. usual short polling. Should be positive, short polling should be used for testing purposes only.</param>
    /// <param name="allowedUpdates">A list of the update types you want your bot to receive. For example, specify <c>["message", "EditedChannelPost", "CallbackQuery"]</c> to only receive updates of these types. See <see cref="Update"/> for a complete list of available update types. Specify an empty list to receive all update types except <em>ChatMember</em>, <em>MessageReaction</em>, and <em>MessageReactionCount</em> (default). If not specified, the previous setting will be used.<br/><br/>Please note that this parameter doesn't affect updates created before the call to getUpdates, so unwanted updates may be received for a short period of time.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>An Array of <see cref="Update"/> objects.</returns>
    public static async Task<Update[]> GetUpdates(
        this ITelegramBotClient botClient,
        int? offset = default,
        int? limit = default,
        int? timeout = default,
        IEnumerable<UpdateType>? allowedUpdates = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetUpdatesRequest
    {
        Offset = offset,
        Limit = limit,
        Timeout = timeout,
        AllowedUpdates = allowedUpdates,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetUpdates">GetUpdates</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetUpdates instead")]
    public static Task<Update[]> GetUpdatesAsync(
        this ITelegramBotClient botClient,
        int? offset = default,
        int? limit = default,
        int? timeout = default,
        IEnumerable<UpdateType>? allowedUpdates = default,
        CancellationToken cancellationToken = default
    ) => botClient.GetUpdates(offset, limit, timeout, allowedUpdates, cancellationToken);

    /// <summary>Use this method to specify a URL and receive incoming updates via an outgoing webhook. Whenever there is an update for the bot, we will send an HTTPS POST request to the specified URL, containing a JSON-serialized <see cref="Update"/>. In case of an unsuccessful request (a request with response <a href="https://en.wikipedia.org/wiki/List_of_HTTP_status_codes">HTTP status code</a> different from <c>2XY</c>), we will repeat the request and give up after a reasonable amount of attempts.<br/>If you'd like to make sure that the webhook was set by you, you can specify secret data in the parameter <paramref name="secretToken"/>. If specified, the request will contain a header “X-Telegram-Bot-Api-Secret-Token” with the secret token as content.</summary>
    /// <remarks><p><b>Notes</b><br/><b>1.</b> You will not be able to receive updates using <see cref="TelegramBotClientExtensions.GetUpdates">GetUpdates</see> for as long as an outgoing webhook is set up.<br/><b>2.</b> To use a self-signed certificate, you need to upload your <a href="https://core.telegram.org/bots/self-signed">public key certificate</a> using <paramref name="certificate"/> parameter. Please upload as InputFile, sending a String will not work.<br/><b>3.</b> Ports currently supported <em>for webhooks</em>: <b>443, 80, 88, 8443</b>.</p><p>If you're having any trouble setting up webhooks, please check out this <a href="https://core.telegram.org/bots/webhooks">amazing guide to webhooks</a>.</p></remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="url">HTTPS URL to send updates to. Use an empty string to remove webhook integration</param>
    /// <param name="certificate">Upload your public key certificate so that the root certificate in use can be checked. See our <a href="https://core.telegram.org/bots/self-signed">self-signed guide</a> for details.</param>
    /// <param name="ipAddress">The fixed IP address which will be used to send webhook requests instead of the IP address resolved through DNS</param>
    /// <param name="maxConnections">The maximum allowed number of simultaneous HTTPS connections to the webhook for update delivery, 1-100. Defaults to <em>40</em>. Use lower values to limit the load on your bot's server, and higher values to increase your bot's throughput.</param>
    /// <param name="allowedUpdates">A list of the update types you want your bot to receive. For example, specify <c>["message", "EditedChannelPost", "CallbackQuery"]</c> to only receive updates of these types. See <see cref="Update"/> for a complete list of available update types. Specify an empty list to receive all update types except <em>ChatMember</em>, <em>MessageReaction</em>, and <em>MessageReactionCount</em> (default). If not specified, the previous setting will be used.<br/>Please note that this parameter doesn't affect updates created before the call to the setWebhook, so unwanted updates may be received for a short period of time.</param>
    /// <param name="dropPendingUpdates">Pass <see langword="true"/> to drop all pending updates</param>
    /// <param name="secretToken">A secret token to be sent in a header “X-Telegram-Bot-Api-Secret-Token” in every webhook request, 1-256 characters. Only characters <c>A-Z</c>, <c>a-z</c>, <c>0-9</c>, <c>_</c> and <c>-</c> are allowed. The header is useful to ensure that the request comes from a webhook set by you.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetWebhook(
        this ITelegramBotClient botClient,
        string url,
        InputFileStream? certificate = default,
        string? ipAddress = default,
        int? maxConnections = default,
        IEnumerable<UpdateType>? allowedUpdates = default,
        bool dropPendingUpdates = default,
        string? secretToken = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetWebhookRequest
    {
        Url = url,
        Certificate = certificate,
        IpAddress = ipAddress,
        MaxConnections = maxConnections,
        AllowedUpdates = allowedUpdates,
        DropPendingUpdates = dropPendingUpdates,
        SecretToken = secretToken,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetWebhook">SetWebhook</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetWebhook instead")]
    public static Task SetWebhookAsync(
        this ITelegramBotClient botClient,
        string url,
        InputFileStream? certificate = default,
        string? ipAddress = default,
        int? maxConnections = default,
        IEnumerable<UpdateType>? allowedUpdates = default,
        bool dropPendingUpdates = default,
        string? secretToken = default,
        CancellationToken cancellationToken = default
    ) => botClient.SetWebhook(url, certificate, ipAddress, maxConnections, allowedUpdates, dropPendingUpdates, secretToken, cancellationToken);


    /// <summary>Use this method to remove webhook integration if you decide to switch back to <see cref="TelegramBotClientExtensions.GetUpdates">GetUpdates</see>.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="dropPendingUpdates">Pass <see langword="true"/> to drop all pending updates</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task DeleteWebhook(
        this ITelegramBotClient botClient,
        bool dropPendingUpdates = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new DeleteWebhookRequest
    {
        DropPendingUpdates = dropPendingUpdates,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="DeleteWebhook">DeleteWebhook</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use DeleteWebhook instead")]
    public static Task DeleteWebhookAsync(
        this ITelegramBotClient botClient,
        bool dropPendingUpdates = default,
        CancellationToken cancellationToken = default
    ) => botClient.DeleteWebhook(dropPendingUpdates, cancellationToken);

    /// <summary>Use this method to get current webhook status.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>A <see cref="WebhookInfo"/> object. If the bot is using <see cref="TelegramBotClientExtensions.GetUpdates">GetUpdates</see>, will return an object with the <em>url</em> field empty.</returns>
    public static async Task<WebhookInfo> GetWebhookInfo(
        this ITelegramBotClient botClient,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetWebhookInfoRequest
    {
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetWebhookInfo">GetWebhookInfo</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetWebhookInfo instead")]
    public static Task<WebhookInfo> GetWebhookInfoAsync(
        this ITelegramBotClient botClient,
        CancellationToken cancellationToken = default
    ) => botClient.GetWebhookInfo(cancellationToken);

    #endregion Getting updates

    #region Available methods

    /// <summary>A simple method for testing your bot's authentication token.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>Basic information about the bot in form of a <see cref="User"/> object.</returns>
    public static async Task<User> GetMe(
        this ITelegramBotClient botClient,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetMeRequest
    {
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetMe">GetMe</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetMe instead")]
    public static Task<User> GetMeAsync(
        this ITelegramBotClient botClient,
        CancellationToken cancellationToken = default
    ) => botClient.GetMe(cancellationToken);

    /// <summary>Use this method to log out from the cloud Bot API server before launching the bot locally. You <b>must</b> log out the bot before running it locally, otherwise there is no guarantee that the bot will receive updates. After a successful call, you can immediately log in on a local server, but will not be able to log in back to the cloud Bot API server for 10 minutes.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task LogOut(
        this ITelegramBotClient botClient,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new LogOutRequest
    {
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="LogOut">LogOut</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use LogOut instead")]
    public static Task LogOutAsync(
        this ITelegramBotClient botClient,
        CancellationToken cancellationToken = default
    ) => botClient.LogOut(cancellationToken);

    /// <summary>Use this method to close the bot instance before moving it from one local server to another. You need to delete the webhook before calling this method to ensure that the bot isn't launched again after server restart. The method will return error 429 in the first 10 minutes after the bot is launched.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task Close(
        this ITelegramBotClient botClient,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new CloseRequest
    {
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="Close">Close</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use Close instead")]
    public static Task CloseAsync(
        this ITelegramBotClient botClient,
        CancellationToken cancellationToken = default
    ) => botClient.Close(cancellationToken);

    /// <summary>Use this method to send text messages.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="text">Text of the message to be sent, 1-4096 characters after entities parsing</param>
    /// <param name="parseMode">Mode for parsing entities in the message text. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</param>
    /// <param name="linkPreviewOptions">Link preview generation options for the message</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="entities">A list of special entities that appear in message text, which can be specified instead of <paramref name="parseMode"/></param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent message from forwarding and saving</param>
    /// <param name="messageEffectId">Unique identifier of the message effect to be added to the message; for private chats only</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be sent</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendMessage(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string text,
        ParseMode parseMode = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        LinkPreviewOptions? linkPreviewOptions = default,
        int? messageThreadId = default,
        IEnumerable<MessageEntity>? entities = default,
        bool disableNotification = default,
        bool protectContent = default,
        string? messageEffectId = default,
        string? businessConnectionId = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendMessageRequest
    {
        ChatId = chatId,
        Text = text,
        ParseMode = parseMode,
        ReplyParameters = replyParameters,
        ReplyMarkup = replyMarkup,
        LinkPreviewOptions = linkPreviewOptions,
        MessageThreadId = messageThreadId,
        Entities = entities,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        MessageEffectId = messageEffectId,
        BusinessConnectionId = businessConnectionId,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendMessage">SendMessage</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendMessage instead")]
    public static Task<Message> SendTextMessageAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string text,
        int? messageThreadId = default,
        ParseMode parseMode = default,
        IEnumerable<MessageEntity>? entities = default,
        LinkPreviewOptions? linkPreviewOptions = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        string? messageEffectId = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendMessage(chatId, text, parseMode, replyParameters, replyMarkup, linkPreviewOptions, messageThreadId, entities, disableNotification, protectContent, messageEffectId, businessConnectionId, allowPaidBroadcast, cancellationToken);

    /// <summary>Use this method to forward messages of any kind. Service messages and messages with protected content can't be forwarded.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="fromChatId">Unique identifier for the chat where the original message was sent (or channel username in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Message identifier in the chat specified in <paramref name="fromChatId"/></param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the forwarded message from forwarding and saving</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> ForwardMessage(
        this ITelegramBotClient botClient,
        ChatId chatId,
        ChatId fromChatId,
        int messageId,
        int? messageThreadId = default,
        bool disableNotification = default,
        bool protectContent = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new ForwardMessageRequest
    {
        ChatId = chatId,
        FromChatId = fromChatId,
        MessageId = messageId,
        MessageThreadId = messageThreadId,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="ForwardMessage">ForwardMessage</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use ForwardMessage instead")]
    public static Task<Message> ForwardMessageAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        ChatId fromChatId,
        int messageId,
        int? messageThreadId = default,
        bool disableNotification = default,
        bool protectContent = default,
        CancellationToken cancellationToken = default
    ) => botClient.ForwardMessage(chatId, fromChatId, messageId, messageThreadId, disableNotification, protectContent, cancellationToken);

    /// <summary>Use this method to forward multiple messages of any kind. If some of the specified messages can't be found or forwarded, they are skipped. Service messages and messages with protected content can't be forwarded. Album grouping is kept for forwarded messages.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="fromChatId">Unique identifier for the chat where the original messages were sent (or channel username in the format <c>@channelusername</c>)</param>
    /// <param name="messageIds">A list of 1-100 identifiers of messages in the chat <paramref name="fromChatId"/> to forward. The identifiers must be specified in a strictly increasing order.</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="disableNotification">Sends the messages <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the forwarded messages from forwarding and saving</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>An array of <see cref="MessageId"/> of the sent messages is returned.</returns>
    public static async Task<MessageId[]> ForwardMessages(
        this ITelegramBotClient botClient,
        ChatId chatId,
        ChatId fromChatId,
        IEnumerable<int> messageIds,
        int? messageThreadId = default,
        bool disableNotification = default,
        bool protectContent = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new ForwardMessagesRequest
    {
        ChatId = chatId,
        FromChatId = fromChatId,
        MessageIds = messageIds,
        MessageThreadId = messageThreadId,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="ForwardMessages">ForwardMessages</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use ForwardMessages instead")]
    public static Task<MessageId[]> ForwardMessagesAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        ChatId fromChatId,
        IEnumerable<int> messageIds,
        int? messageThreadId = default,
        bool disableNotification = default,
        bool protectContent = default,
        CancellationToken cancellationToken = default
    ) => botClient.ForwardMessages(chatId, fromChatId, messageIds, messageThreadId, disableNotification, protectContent, cancellationToken);

    /// <summary>Use this method to copy messages of any kind. Service messages, paid media messages, giveaway messages, giveaway winners messages, and invoice messages can't be copied. A quiz <see cref="Poll"/> can be copied only if the value of the field <em>CorrectOptionId</em> is known to the bot. The method is analogous to the method <see cref="TelegramBotClientExtensions.ForwardMessage">ForwardMessage</see>, but the copied message doesn't have a link to the original message.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="fromChatId">Unique identifier for the chat where the original message was sent (or channel username in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Message identifier in the chat specified in <paramref name="fromChatId"/></param>
    /// <param name="caption">New caption for media, 0-1024 characters after entities parsing. If not specified, the original caption is kept</param>
    /// <param name="parseMode">Mode for parsing entities in the new caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="captionEntities">A list of special entities that appear in the new caption, which can be specified instead of <paramref name="parseMode"/></param>
    /// <param name="showCaptionAboveMedia">Pass <see langword="true"/>, if the caption must be shown above the message media. Ignored if a new caption isn't specified.</param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent message from forwarding and saving</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The <see cref="MessageId"/> of the sent message on success.</returns>
    public static async Task<MessageId> CopyMessage(
        this ITelegramBotClient botClient,
        ChatId chatId,
        ChatId fromChatId,
        int messageId,
        string? caption = default,
        ParseMode parseMode = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        int? messageThreadId = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool showCaptionAboveMedia = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new CopyMessageRequest
    {
        ChatId = chatId,
        FromChatId = fromChatId,
        MessageId = messageId,
        Caption = caption,
        ParseMode = parseMode,
        ReplyParameters = replyParameters,
        ReplyMarkup = replyMarkup,
        MessageThreadId = messageThreadId,
        CaptionEntities = captionEntities,
        ShowCaptionAboveMedia = showCaptionAboveMedia,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="CopyMessage">CopyMessage</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use CopyMessage instead")]
    public static Task<MessageId> CopyMessageAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        ChatId fromChatId,
        int messageId,
        int? messageThreadId = default,
        string? caption = default,
        ParseMode parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool showCaptionAboveMedia = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default
    ) => botClient.CopyMessage(chatId, fromChatId, messageId, caption, parseMode, replyParameters, replyMarkup, messageThreadId, captionEntities, showCaptionAboveMedia, disableNotification, protectContent, allowPaidBroadcast, cancellationToken);

    /// <summary>Use this method to copy messages of any kind. If some of the specified messages can't be found or copied, they are skipped. Service messages, paid media messages, giveaway messages, giveaway winners messages, and invoice messages can't be copied. A quiz <see cref="Poll"/> can be copied only if the value of the field <em>CorrectOptionId</em> is known to the bot. The method is analogous to the method <see cref="TelegramBotClientExtensions.ForwardMessages">ForwardMessages</see>, but the copied messages don't have a link to the original message. Album grouping is kept for copied messages.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="fromChatId">Unique identifier for the chat where the original messages were sent (or channel username in the format <c>@channelusername</c>)</param>
    /// <param name="messageIds">A list of 1-100 identifiers of messages in the chat <paramref name="fromChatId"/> to copy. The identifiers must be specified in a strictly increasing order.</param>
    /// <param name="removeCaption">Pass <see langword="true"/> to copy the messages without their captions</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="disableNotification">Sends the messages <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent messages from forwarding and saving</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>An array of <see cref="MessageId"/> of the sent messages is returned.</returns>
    public static async Task<MessageId[]> CopyMessages(
        this ITelegramBotClient botClient,
        ChatId chatId,
        ChatId fromChatId,
        IEnumerable<int> messageIds,
        bool removeCaption = default,
        int? messageThreadId = default,
        bool disableNotification = default,
        bool protectContent = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new CopyMessagesRequest
    {
        ChatId = chatId,
        FromChatId = fromChatId,
        MessageIds = messageIds,
        RemoveCaption = removeCaption,
        MessageThreadId = messageThreadId,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="CopyMessages">CopyMessages</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use CopyMessages instead")]
    public static Task<MessageId[]> CopyMessagesAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        ChatId fromChatId,
        IEnumerable<int> messageIds,
        int? messageThreadId = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool removeCaption = default,
        CancellationToken cancellationToken = default
    ) => botClient.CopyMessages(chatId, fromChatId, messageIds, removeCaption, messageThreadId, disableNotification, protectContent, cancellationToken);

    /// <summary>Use this method to send photos.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="photo">Photo to send. Pass a FileId as String to send a photo that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get a photo from the Internet, or upload a new photo using <see cref="InputFileStream"/>. The photo must be at most 10 MB in size. The photo's width and height must not exceed 10000 in total. Width and height ratio must be at most 20. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    /// <param name="caption">Photo caption (may also be used when resending photos by <em>FileId</em>), 0-1024 characters after entities parsing</param>
    /// <param name="parseMode">Mode for parsing entities in the photo caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="captionEntities">A list of special entities that appear in the caption, which can be specified instead of <paramref name="parseMode"/></param>
    /// <param name="showCaptionAboveMedia">Pass <see langword="true"/>, if the caption must be shown above the message media</param>
    /// <param name="hasSpoiler">Pass <see langword="true"/> if the photo needs to be covered with a spoiler animation</param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent message from forwarding and saving</param>
    /// <param name="messageEffectId">Unique identifier of the message effect to be added to the message; for private chats only</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be sent</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendPhoto(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile photo,
        string? caption = default,
        ParseMode parseMode = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        int? messageThreadId = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool showCaptionAboveMedia = default,
        bool hasSpoiler = default,
        bool disableNotification = default,
        bool protectContent = default,
        string? messageEffectId = default,
        string? businessConnectionId = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendPhotoRequest
    {
        ChatId = chatId,
        Photo = photo,
        Caption = caption,
        ParseMode = parseMode,
        ReplyParameters = replyParameters,
        ReplyMarkup = replyMarkup,
        MessageThreadId = messageThreadId,
        CaptionEntities = captionEntities,
        ShowCaptionAboveMedia = showCaptionAboveMedia,
        HasSpoiler = hasSpoiler,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        MessageEffectId = messageEffectId,
        BusinessConnectionId = businessConnectionId,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendPhoto">SendPhoto</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendPhoto instead")]
    public static Task<Message> SendPhotoAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile photo,
        int? messageThreadId = default,
        string? caption = default,
        ParseMode parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool showCaptionAboveMedia = default,
        bool hasSpoiler = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        string? messageEffectId = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendPhoto(chatId, photo, caption, parseMode, replyParameters, replyMarkup, messageThreadId, captionEntities, showCaptionAboveMedia, hasSpoiler, disableNotification, protectContent, messageEffectId, businessConnectionId, allowPaidBroadcast, cancellationToken);

    /// <summary>Use this method to send audio files, if you want Telegram clients to display them in the music player. Your audio must be in the .MP3 or .M4A format.</summary>
    /// <remarks>Bots can currently send audio files of up to 50 MB in size, this limit may be changed in the future.<br/>For sending voice messages, use the <see cref="TelegramBotClientExtensions.SendVoice">SendVoice</see> method instead.</remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="audio">Audio file to send. Pass a FileId as String to send an audio file that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get an audio file from the Internet, or upload a new one using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    /// <param name="caption">Audio caption, 0-1024 characters after entities parsing</param>
    /// <param name="parseMode">Mode for parsing entities in the audio caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</param>
    /// <param name="duration">Duration of the audio in seconds</param>
    /// <param name="performer">Performer</param>
    /// <param name="title">Track name</param>
    /// <param name="thumbnail">Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side. The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height should not exceed 320. Ignored if the file is not uploaded using <see cref="InputFileStream"/>. Thumbnails can't be reused and can be only uploaded as a new file, so you can use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="captionEntities">A list of special entities that appear in the caption, which can be specified instead of <paramref name="parseMode"/></param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent message from forwarding and saving</param>
    /// <param name="messageEffectId">Unique identifier of the message effect to be added to the message; for private chats only</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be sent</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendAudio(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile audio,
        string? caption = default,
        ParseMode parseMode = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        int? duration = default,
        string? performer = default,
        string? title = default,
        InputFile? thumbnail = default,
        int? messageThreadId = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool disableNotification = default,
        bool protectContent = default,
        string? messageEffectId = default,
        string? businessConnectionId = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendAudioRequest
    {
        ChatId = chatId,
        Audio = audio,
        Caption = caption,
        ParseMode = parseMode,
        ReplyParameters = replyParameters,
        ReplyMarkup = replyMarkup,
        Duration = duration,
        Performer = performer,
        Title = title,
        Thumbnail = thumbnail,
        MessageThreadId = messageThreadId,
        CaptionEntities = captionEntities,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        MessageEffectId = messageEffectId,
        BusinessConnectionId = businessConnectionId,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendAudio">SendAudio</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendAudio instead")]
    public static Task<Message> SendAudioAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile audio,
        int? messageThreadId = default,
        string? caption = default,
        ParseMode parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        int? duration = default,
        string? performer = default,
        string? title = default,
        InputFile? thumbnail = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        string? messageEffectId = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendAudio(chatId, audio, caption, parseMode, replyParameters, replyMarkup, duration, performer, title, thumbnail, messageThreadId, captionEntities, disableNotification, protectContent, messageEffectId, businessConnectionId, allowPaidBroadcast, cancellationToken);

    /// <summary>Use this method to send general files.</summary>
    /// <remarks>Bots can currently send files of any type of up to 50 MB in size, this limit may be changed in the future.</remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="document">File to send. Pass a FileId as String to send a file that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get a file from the Internet, or upload a new one using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    /// <param name="caption">Document caption (may also be used when resending documents by <em>FileId</em>), 0-1024 characters after entities parsing</param>
    /// <param name="parseMode">Mode for parsing entities in the document caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</param>
    /// <param name="thumbnail">Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side. The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height should not exceed 320. Ignored if the file is not uploaded using <see cref="InputFileStream"/>. Thumbnails can't be reused and can be only uploaded as a new file, so you can use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="captionEntities">A list of special entities that appear in the caption, which can be specified instead of <paramref name="parseMode"/></param>
    /// <param name="disableContentTypeDetection">Disables automatic server-side content type detection for files uploaded using <see cref="InputFileStream"/></param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent message from forwarding and saving</param>
    /// <param name="messageEffectId">Unique identifier of the message effect to be added to the message; for private chats only</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be sent</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendDocument(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile document,
        string? caption = default,
        ParseMode parseMode = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        InputFile? thumbnail = default,
        int? messageThreadId = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool disableContentTypeDetection = default,
        bool disableNotification = default,
        bool protectContent = default,
        string? messageEffectId = default,
        string? businessConnectionId = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendDocumentRequest
    {
        ChatId = chatId,
        Document = document,
        Caption = caption,
        ParseMode = parseMode,
        ReplyParameters = replyParameters,
        ReplyMarkup = replyMarkup,
        Thumbnail = thumbnail,
        MessageThreadId = messageThreadId,
        CaptionEntities = captionEntities,
        DisableContentTypeDetection = disableContentTypeDetection,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        MessageEffectId = messageEffectId,
        BusinessConnectionId = businessConnectionId,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendDocument">SendDocument</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendDocument instead")]
    public static Task<Message> SendDocumentAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile document,
        int? messageThreadId = default,
        InputFile? thumbnail = default,
        string? caption = default,
        ParseMode parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool disableContentTypeDetection = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        string? messageEffectId = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendDocument(chatId, document, caption, parseMode, replyParameters, replyMarkup, thumbnail, messageThreadId, captionEntities, disableContentTypeDetection, disableNotification, protectContent, messageEffectId, businessConnectionId, allowPaidBroadcast, cancellationToken);

    /// <summary>Use this method to send video files, Telegram clients support MPEG4 videos (other formats may be sent as <see cref="Document"/>).</summary>
    /// <remarks>Bots can currently send video files of up to 50 MB in size, this limit may be changed in the future.</remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="video">Video to send. Pass a FileId as String to send a video that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get a video from the Internet, or upload a new video using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    /// <param name="caption">Video caption (may also be used when resending videos by <em>FileId</em>), 0-1024 characters after entities parsing</param>
    /// <param name="parseMode">Mode for parsing entities in the video caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</param>
    /// <param name="duration">Duration of sent video in seconds</param>
    /// <param name="width">Video width</param>
    /// <param name="height">Video height</param>
    /// <param name="thumbnail">Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side. The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height should not exceed 320. Ignored if the file is not uploaded using <see cref="InputFileStream"/>. Thumbnails can't be reused and can be only uploaded as a new file, so you can use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="captionEntities">A list of special entities that appear in the caption, which can be specified instead of <paramref name="parseMode"/></param>
    /// <param name="showCaptionAboveMedia">Pass <see langword="true"/>, if the caption must be shown above the message media</param>
    /// <param name="hasSpoiler">Pass <see langword="true"/> if the video needs to be covered with a spoiler animation</param>
    /// <param name="supportsStreaming">Pass <see langword="true"/> if the uploaded video is suitable for streaming</param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent message from forwarding and saving</param>
    /// <param name="messageEffectId">Unique identifier of the message effect to be added to the message; for private chats only</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be sent</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendVideo(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile video,
        string? caption = default,
        ParseMode parseMode = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        int? duration = default,
        int? width = default,
        int? height = default,
        InputFile? thumbnail = default,
        int? messageThreadId = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool showCaptionAboveMedia = default,
        bool hasSpoiler = default,
        bool supportsStreaming = default,
        bool disableNotification = default,
        bool protectContent = default,
        string? messageEffectId = default,
        string? businessConnectionId = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendVideoRequest
    {
        ChatId = chatId,
        Video = video,
        Caption = caption,
        ParseMode = parseMode,
        ReplyParameters = replyParameters,
        ReplyMarkup = replyMarkup,
        Duration = duration,
        Width = width,
        Height = height,
        Thumbnail = thumbnail,
        MessageThreadId = messageThreadId,
        CaptionEntities = captionEntities,
        ShowCaptionAboveMedia = showCaptionAboveMedia,
        HasSpoiler = hasSpoiler,
        SupportsStreaming = supportsStreaming,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        MessageEffectId = messageEffectId,
        BusinessConnectionId = businessConnectionId,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendVideo">SendVideo</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendVideo instead")]
    public static Task<Message> SendVideoAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile video,
        int? messageThreadId = default,
        int? duration = default,
        int? width = default,
        int? height = default,
        InputFile? thumbnail = default,
        string? caption = default,
        ParseMode parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool showCaptionAboveMedia = default,
        bool hasSpoiler = default,
        bool supportsStreaming = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        string? messageEffectId = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendVideo(chatId, video, caption, parseMode, replyParameters, replyMarkup, duration, width, height, thumbnail, messageThreadId, captionEntities, showCaptionAboveMedia, hasSpoiler, supportsStreaming, disableNotification, protectContent, messageEffectId, businessConnectionId, allowPaidBroadcast, cancellationToken);

    /// <summary>Use this method to send animation files (GIF or H.264/MPEG-4 AVC video without sound).</summary>
    /// <remarks>Bots can currently send animation files of up to 50 MB in size, this limit may be changed in the future.</remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="animation">Animation to send. Pass a FileId as String to send an animation that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get an animation from the Internet, or upload a new animation using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    /// <param name="caption">Animation caption (may also be used when resending animation by <em>FileId</em>), 0-1024 characters after entities parsing</param>
    /// <param name="parseMode">Mode for parsing entities in the animation caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</param>
    /// <param name="duration">Duration of sent animation in seconds</param>
    /// <param name="width">Animation width</param>
    /// <param name="height">Animation height</param>
    /// <param name="thumbnail">Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side. The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height should not exceed 320. Ignored if the file is not uploaded using <see cref="InputFileStream"/>. Thumbnails can't be reused and can be only uploaded as a new file, so you can use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="captionEntities">A list of special entities that appear in the caption, which can be specified instead of <paramref name="parseMode"/></param>
    /// <param name="showCaptionAboveMedia">Pass <see langword="true"/>, if the caption must be shown above the message media</param>
    /// <param name="hasSpoiler">Pass <see langword="true"/> if the animation needs to be covered with a spoiler animation</param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent message from forwarding and saving</param>
    /// <param name="messageEffectId">Unique identifier of the message effect to be added to the message; for private chats only</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be sent</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendAnimation(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile animation,
        string? caption = default,
        ParseMode parseMode = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        int? duration = default,
        int? width = default,
        int? height = default,
        InputFile? thumbnail = default,
        int? messageThreadId = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool showCaptionAboveMedia = default,
        bool hasSpoiler = default,
        bool disableNotification = default,
        bool protectContent = default,
        string? messageEffectId = default,
        string? businessConnectionId = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendAnimationRequest
    {
        ChatId = chatId,
        Animation = animation,
        Caption = caption,
        ParseMode = parseMode,
        ReplyParameters = replyParameters,
        ReplyMarkup = replyMarkup,
        Duration = duration,
        Width = width,
        Height = height,
        Thumbnail = thumbnail,
        MessageThreadId = messageThreadId,
        CaptionEntities = captionEntities,
        ShowCaptionAboveMedia = showCaptionAboveMedia,
        HasSpoiler = hasSpoiler,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        MessageEffectId = messageEffectId,
        BusinessConnectionId = businessConnectionId,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendAnimation">SendAnimation</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendAnimation instead")]
    public static Task<Message> SendAnimationAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile animation,
        int? messageThreadId = default,
        int? duration = default,
        int? width = default,
        int? height = default,
        InputFile? thumbnail = default,
        string? caption = default,
        ParseMode parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool showCaptionAboveMedia = default,
        bool hasSpoiler = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        string? messageEffectId = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendAnimation(chatId, animation, caption, parseMode, replyParameters, replyMarkup, duration, width, height, thumbnail, messageThreadId, captionEntities, showCaptionAboveMedia, hasSpoiler, disableNotification, protectContent, messageEffectId, businessConnectionId, allowPaidBroadcast, cancellationToken);

    /// <summary>Use this method to send audio files, if you want Telegram clients to display the file as a playable voice message. For this to work, your audio must be in an .OGG file encoded with OPUS, or in .MP3 format, or in .M4A format (other formats may be sent as <see cref="Audio"/> or <see cref="Document"/>).</summary>
    /// <remarks>Bots can currently send voice messages of up to 50 MB in size, this limit may be changed in the future.</remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="voice">Audio file to send. Pass a FileId as String to send a file that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get a file from the Internet, or upload a new one using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    /// <param name="caption">Voice message caption, 0-1024 characters after entities parsing</param>
    /// <param name="parseMode">Mode for parsing entities in the voice message caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</param>
    /// <param name="duration">Duration of the voice message in seconds</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="captionEntities">A list of special entities that appear in the caption, which can be specified instead of <paramref name="parseMode"/></param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent message from forwarding and saving</param>
    /// <param name="messageEffectId">Unique identifier of the message effect to be added to the message; for private chats only</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be sent</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendVoice(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile voice,
        string? caption = default,
        ParseMode parseMode = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        int? duration = default,
        int? messageThreadId = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool disableNotification = default,
        bool protectContent = default,
        string? messageEffectId = default,
        string? businessConnectionId = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendVoiceRequest
    {
        ChatId = chatId,
        Voice = voice,
        Caption = caption,
        ParseMode = parseMode,
        ReplyParameters = replyParameters,
        ReplyMarkup = replyMarkup,
        Duration = duration,
        MessageThreadId = messageThreadId,
        CaptionEntities = captionEntities,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        MessageEffectId = messageEffectId,
        BusinessConnectionId = businessConnectionId,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendVoice">SendVoice</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendVoice instead")]
    public static Task<Message> SendVoiceAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile voice,
        int? messageThreadId = default,
        string? caption = default,
        ParseMode parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        int? duration = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        string? messageEffectId = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendVoice(chatId, voice, caption, parseMode, replyParameters, replyMarkup, duration, messageThreadId, captionEntities, disableNotification, protectContent, messageEffectId, businessConnectionId, allowPaidBroadcast, cancellationToken);

    /// <summary>As of <a href="https://telegram.org/blog/video-messages-and-telescope">v.4.0</a>, Telegram clients support rounded square MPEG4 videos of up to 1 minute long. Use this method to send video messages.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="videoNote">Video note to send. Pass a FileId as String to send a video note that exists on the Telegram servers (recommended) or upload a new video using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a>. Sending video notes by a URL is currently unsupported</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</param>
    /// <param name="duration">Duration of sent video in seconds</param>
    /// <param name="length">Video width and height, i.e. diameter of the video message</param>
    /// <param name="thumbnail">Thumbnail of the file sent; can be ignored if thumbnail generation for the file is supported server-side. The thumbnail should be in JPEG format and less than 200 kB in size. A thumbnail's width and height should not exceed 320. Ignored if the file is not uploaded using <see cref="InputFileStream"/>. Thumbnails can't be reused and can be only uploaded as a new file, so you can use <see cref="InputFileStream(Stream, string?)"/> with a specific filename. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent message from forwarding and saving</param>
    /// <param name="messageEffectId">Unique identifier of the message effect to be added to the message; for private chats only</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be sent</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendVideoNote(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile videoNote,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        int? duration = default,
        int? length = default,
        InputFile? thumbnail = default,
        int? messageThreadId = default,
        bool disableNotification = default,
        bool protectContent = default,
        string? messageEffectId = default,
        string? businessConnectionId = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendVideoNoteRequest
    {
        ChatId = chatId,
        VideoNote = videoNote,
        ReplyParameters = replyParameters,
        ReplyMarkup = replyMarkup,
        Duration = duration,
        Length = length,
        Thumbnail = thumbnail,
        MessageThreadId = messageThreadId,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        MessageEffectId = messageEffectId,
        BusinessConnectionId = businessConnectionId,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendVideoNote">SendVideoNote</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendVideoNote instead")]
    public static Task<Message> SendVideoNoteAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile videoNote,
        int? messageThreadId = default,
        int? duration = default,
        int? length = default,
        InputFile? thumbnail = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        string? messageEffectId = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendVideoNote(chatId, videoNote, replyParameters, replyMarkup, duration, length, thumbnail, messageThreadId, disableNotification, protectContent, messageEffectId, businessConnectionId, allowPaidBroadcast, cancellationToken);

    /// <summary>Use this method to send paid media.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>). If the chat is a channel, all Telegram Star proceeds from this media will be credited to the chat's balance. Otherwise, they will be credited to the bot's balance.</param>
    /// <param name="starCount">The number of Telegram Stars that must be paid to buy access to the media; 1-2500</param>
    /// <param name="media">A array describing the media to be sent; up to 10 items</param>
    /// <param name="caption">Media caption, 0-1024 characters after entities parsing</param>
    /// <param name="parseMode">Mode for parsing entities in the media caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</param>
    /// <param name="payload">Bot-defined paid media payload, 0-128 bytes. This will not be displayed to the user, use it for your internal processes.</param>
    /// <param name="captionEntities">A list of special entities that appear in the caption, which can be specified instead of <paramref name="parseMode"/></param>
    /// <param name="showCaptionAboveMedia">Pass <see langword="true"/>, if the caption must be shown above the message media</param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent message from forwarding and saving</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be sent</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendPaidMedia(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int starCount,
        IEnumerable<InputPaidMedia> media,
        string? caption = default,
        ParseMode parseMode = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? payload = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool showCaptionAboveMedia = default,
        bool disableNotification = default,
        bool protectContent = default,
        string? businessConnectionId = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendPaidMediaRequest
    {
        ChatId = chatId,
        StarCount = starCount,
        Media = media,
        Caption = caption,
        ParseMode = parseMode,
        ReplyParameters = replyParameters,
        ReplyMarkup = replyMarkup,
        Payload = payload,
        CaptionEntities = captionEntities,
        ShowCaptionAboveMedia = showCaptionAboveMedia,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        BusinessConnectionId = businessConnectionId,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendPaidMedia">SendPaidMedia</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendPaidMedia instead")]
    public static Task<Message> SendPaidMediaAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int starCount,
        IEnumerable<InputPaidMedia> media,
        string? payload = default,
        string? caption = default,
        ParseMode parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool showCaptionAboveMedia = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendPaidMedia(chatId, starCount, media, caption, parseMode, replyParameters, replyMarkup, payload, captionEntities, showCaptionAboveMedia, disableNotification, protectContent, businessConnectionId, allowPaidBroadcast, cancellationToken);

    /// <summary>Use this method to send a group of photos, videos, documents or audios as an album. Documents and audio files can be only grouped in an album with messages of the same type.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="media">A array describing messages to be sent, must include 2-10 items</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="disableNotification">Sends messages <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent messages from forwarding and saving</param>
    /// <param name="messageEffectId">Unique identifier of the message effect to be added to the message; for private chats only</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be sent</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>An array of <see cref="Message">Messages</see> that were sent is returned.</returns>
    public static async Task<Message[]> SendMediaGroup(
        this ITelegramBotClient botClient,
        ChatId chatId,
        IEnumerable<IAlbumInputMedia> media,
        ReplyParameters? replyParameters = default,
        int? messageThreadId = default,
        bool disableNotification = default,
        bool protectContent = default,
        string? messageEffectId = default,
        string? businessConnectionId = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendMediaGroupRequest
    {
        ChatId = chatId,
        Media = media,
        ReplyParameters = replyParameters,
        MessageThreadId = messageThreadId,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        MessageEffectId = messageEffectId,
        BusinessConnectionId = businessConnectionId,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendMediaGroup">SendMediaGroup</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendMediaGroup instead")]
    public static Task<Message[]> SendMediaGroupAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        IEnumerable<IAlbumInputMedia> media,
        int? messageThreadId = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        string? messageEffectId = default,
        ReplyParameters? replyParameters = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendMediaGroup(chatId, media, replyParameters, messageThreadId, disableNotification, protectContent, messageEffectId, businessConnectionId, allowPaidBroadcast, cancellationToken);

    /// <summary>Use this method to send point on the map.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="latitude">Latitude of the location</param>
    /// <param name="longitude">Longitude of the location</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</param>
    /// <param name="horizontalAccuracy">The radius of uncertainty for the location, measured in meters; 0-1500</param>
    /// <param name="livePeriod">Period in seconds during which the location will be updated (see <a href="https://telegram.org/blog/live-locations">Live Locations</a>, should be between 60 and 86400, or 0x7FFFFFFF for live locations that can be edited indefinitely.</param>
    /// <param name="heading">For live locations, a direction in which the user is moving, in degrees. Must be between 1 and 360 if specified.</param>
    /// <param name="proximityAlertRadius">For live locations, a maximum distance for proximity alerts about approaching another chat member, in meters. Must be between 1 and 100000 if specified.</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent message from forwarding and saving</param>
    /// <param name="messageEffectId">Unique identifier of the message effect to be added to the message; for private chats only</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be sent</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendLocation(
        this ITelegramBotClient botClient,
        ChatId chatId,
        double latitude,
        double longitude,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        double? horizontalAccuracy = default,
        int? livePeriod = default,
        int? heading = default,
        int? proximityAlertRadius = default,
        int? messageThreadId = default,
        bool disableNotification = default,
        bool protectContent = default,
        string? messageEffectId = default,
        string? businessConnectionId = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendLocationRequest
    {
        ChatId = chatId,
        Latitude = latitude,
        Longitude = longitude,
        ReplyParameters = replyParameters,
        ReplyMarkup = replyMarkup,
        HorizontalAccuracy = horizontalAccuracy,
        LivePeriod = livePeriod,
        Heading = heading,
        ProximityAlertRadius = proximityAlertRadius,
        MessageThreadId = messageThreadId,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        MessageEffectId = messageEffectId,
        BusinessConnectionId = businessConnectionId,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendLocation">SendLocation</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendLocation instead")]
    public static Task<Message> SendLocationAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        double latitude,
        double longitude,
        int? messageThreadId = default,
        double? horizontalAccuracy = default,
        int? livePeriod = default,
        int? heading = default,
        int? proximityAlertRadius = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        string? messageEffectId = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendLocation(chatId, latitude, longitude, replyParameters, replyMarkup, horizontalAccuracy, livePeriod, heading, proximityAlertRadius, messageThreadId, disableNotification, protectContent, messageEffectId, businessConnectionId, allowPaidBroadcast, cancellationToken);

    /// <summary>Use this method to send information about a venue.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="latitude">Latitude of the venue</param>
    /// <param name="longitude">Longitude of the venue</param>
    /// <param name="title">Name of the venue</param>
    /// <param name="address">Address of the venue</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</param>
    /// <param name="foursquareId">Foursquare identifier of the venue</param>
    /// <param name="foursquareType">Foursquare type of the venue, if known. (For example, “arts_entertainment/default”, “arts_entertainment/aquarium” or “food/icecream”.)</param>
    /// <param name="googlePlaceId">Google Places identifier of the venue</param>
    /// <param name="googlePlaceType">Google Places type of the venue. (See <a href="https://developers.google.com/places/web-service/supported_types">supported types</a>.)</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent message from forwarding and saving</param>
    /// <param name="messageEffectId">Unique identifier of the message effect to be added to the message; for private chats only</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be sent</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendVenue(
        this ITelegramBotClient botClient,
        ChatId chatId,
        double latitude,
        double longitude,
        string title,
        string address,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? foursquareId = default,
        string? foursquareType = default,
        string? googlePlaceId = default,
        string? googlePlaceType = default,
        int? messageThreadId = default,
        bool disableNotification = default,
        bool protectContent = default,
        string? messageEffectId = default,
        string? businessConnectionId = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendVenueRequest
    {
        ChatId = chatId,
        Latitude = latitude,
        Longitude = longitude,
        Title = title,
        Address = address,
        ReplyParameters = replyParameters,
        ReplyMarkup = replyMarkup,
        FoursquareId = foursquareId,
        FoursquareType = foursquareType,
        GooglePlaceId = googlePlaceId,
        GooglePlaceType = googlePlaceType,
        MessageThreadId = messageThreadId,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        MessageEffectId = messageEffectId,
        BusinessConnectionId = businessConnectionId,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendVenue">SendVenue</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendVenue instead")]
    public static Task<Message> SendVenueAsync(
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
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        string? messageEffectId = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendVenue(chatId, latitude, longitude, title, address, replyParameters, replyMarkup, foursquareId, foursquareType, googlePlaceId, googlePlaceType, messageThreadId, disableNotification, protectContent, messageEffectId, businessConnectionId, allowPaidBroadcast, cancellationToken);

    /// <summary>Use this method to send phone contacts.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="phoneNumber">Contact's phone number</param>
    /// <param name="firstName">Contact's first name</param>
    /// <param name="lastName">Contact's last name</param>
    /// <param name="vcard">Additional data about the contact in the form of a <a href="https://en.wikipedia.org/wiki/VCard">vCard</a>, 0-2048 bytes</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent message from forwarding and saving</param>
    /// <param name="messageEffectId">Unique identifier of the message effect to be added to the message; for private chats only</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be sent</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendContact(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string phoneNumber,
        string firstName,
        string? lastName = default,
        string? vcard = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        int? messageThreadId = default,
        bool disableNotification = default,
        bool protectContent = default,
        string? messageEffectId = default,
        string? businessConnectionId = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendContactRequest
    {
        ChatId = chatId,
        PhoneNumber = phoneNumber,
        FirstName = firstName,
        LastName = lastName,
        Vcard = vcard,
        ReplyParameters = replyParameters,
        ReplyMarkup = replyMarkup,
        MessageThreadId = messageThreadId,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        MessageEffectId = messageEffectId,
        BusinessConnectionId = businessConnectionId,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendContact">SendContact</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendContact instead")]
    public static Task<Message> SendContactAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string phoneNumber,
        string firstName,
        int? messageThreadId = default,
        string? lastName = default,
        string? vcard = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        string? messageEffectId = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendContact(chatId, phoneNumber, firstName, lastName, vcard, replyParameters, replyMarkup, messageThreadId, disableNotification, protectContent, messageEffectId, businessConnectionId, allowPaidBroadcast, cancellationToken);

    /// <summary>Use this method to send a native poll.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="question">Poll question, 1-300 characters</param>
    /// <param name="options">A list of 2-10 answer options</param>
    /// <param name="isAnonymous"><see langword="true"/>, if the poll needs to be anonymous, defaults to <see langword="true"/></param>
    /// <param name="type">Poll type, <see cref="PollType.Quiz">Quiz</see> or <see cref="PollType.Regular">Regular</see>, defaults to <see cref="PollType.Regular">Regular</see></param>
    /// <param name="allowsMultipleAnswers"><see langword="true"/>, if the poll allows multiple answers, ignored for polls in quiz mode, defaults to <see langword="false"/></param>
    /// <param name="correctOptionId">0-based identifier of the correct answer option, required for polls in quiz mode</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</param>
    /// <param name="explanation">Text that is shown when a user chooses an incorrect answer or taps on the lamp icon in a quiz-style poll, 0-200 characters with at most 2 line feeds after entities parsing</param>
    /// <param name="explanationParseMode">Mode for parsing entities in the explanation. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</param>
    /// <param name="explanationEntities">A list of special entities that appear in the poll explanation. It can be specified instead of <paramref name="explanationParseMode"/></param>
    /// <param name="questionParseMode">Mode for parsing entities in the question. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details. Currently, only custom emoji entities are allowed</param>
    /// <param name="questionEntities">A list of special entities that appear in the poll question. It can be specified instead of <paramref name="questionParseMode"/></param>
    /// <param name="openPeriod">Amount of time in seconds the poll will be active after creation, 5-600. Can't be used together with <paramref name="closeDate"/>.</param>
    /// <param name="closeDate">Point in time when the poll will be automatically closed. Must be at least 5 and no more than 600 seconds in the future. Can't be used together with <paramref name="openPeriod"/>.</param>
    /// <param name="isClosed">Pass <see langword="true"/> if the poll needs to be immediately closed. This can be useful for poll preview.</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent message from forwarding and saving</param>
    /// <param name="messageEffectId">Unique identifier of the message effect to be added to the message; for private chats only</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be sent</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendPoll(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string question,
        IEnumerable<InputPollOption> options,
        bool isAnonymous = true,
        PollType? type = default,
        bool allowsMultipleAnswers = default,
        int? correctOptionId = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? explanation = default,
        ParseMode explanationParseMode = default,
        IEnumerable<MessageEntity>? explanationEntities = default,
        ParseMode questionParseMode = default,
        IEnumerable<MessageEntity>? questionEntities = default,
        int? openPeriod = default,
        DateTime? closeDate = default,
        bool isClosed = default,
        int? messageThreadId = default,
        bool disableNotification = default,
        bool protectContent = default,
        string? messageEffectId = default,
        string? businessConnectionId = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendPollRequest
    {
        ChatId = chatId,
        Question = question,
        Options = options,
        IsAnonymous = isAnonymous,
        Type = type,
        AllowsMultipleAnswers = allowsMultipleAnswers,
        CorrectOptionId = correctOptionId,
        ReplyParameters = replyParameters,
        ReplyMarkup = replyMarkup,
        Explanation = explanation,
        ExplanationParseMode = explanationParseMode,
        ExplanationEntities = explanationEntities,
        QuestionParseMode = questionParseMode,
        QuestionEntities = questionEntities,
        OpenPeriod = openPeriod,
        CloseDate = closeDate,
        IsClosed = isClosed,
        MessageThreadId = messageThreadId,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        MessageEffectId = messageEffectId,
        BusinessConnectionId = businessConnectionId,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendPoll">SendPoll</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendPoll instead")]
    public static Task<Message> SendPollAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string question,
        IEnumerable<InputPollOption> options,
        int? messageThreadId = default,
        ParseMode questionParseMode = default,
        IEnumerable<MessageEntity>? questionEntities = default,
        bool isAnonymous = true,
        PollType? type = default,
        bool allowsMultipleAnswers = default,
        int? correctOptionId = default,
        string? explanation = default,
        ParseMode explanationParseMode = default,
        IEnumerable<MessageEntity>? explanationEntities = default,
        int? openPeriod = default,
        DateTime? closeDate = default,
        bool isClosed = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        string? messageEffectId = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendPoll(chatId, question, options, isAnonymous, type, allowsMultipleAnswers, correctOptionId, replyParameters, replyMarkup, explanation, explanationParseMode, explanationEntities, questionParseMode, questionEntities, openPeriod, closeDate, isClosed, messageThreadId, disableNotification, protectContent, messageEffectId, businessConnectionId, allowPaidBroadcast, cancellationToken);

    /// <summary>Use this method to send an animated emoji that will display a random value.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="emoji">Emoji on which the dice throw animation is based. Currently, must be one of “🎲”, “🎯”, “🏀”, “⚽”, “🎳”, or “🎰”. Dice can have values 1-6 for “🎲”, “🎯” and “🎳”, values 1-5 for “🏀” and “⚽”, and values 1-64 for “🎰”. Defaults to “🎲”</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent message from forwarding</param>
    /// <param name="messageEffectId">Unique identifier of the message effect to be added to the message; for private chats only</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be sent</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendDice(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string? emoji = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        int? messageThreadId = default,
        bool disableNotification = default,
        bool protectContent = default,
        string? messageEffectId = default,
        string? businessConnectionId = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendDiceRequest
    {
        ChatId = chatId,
        Emoji = emoji,
        ReplyParameters = replyParameters,
        ReplyMarkup = replyMarkup,
        MessageThreadId = messageThreadId,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        MessageEffectId = messageEffectId,
        BusinessConnectionId = businessConnectionId,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendDice">SendDice</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendDice instead")]
    public static Task<Message> SendDiceAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int? messageThreadId = default,
        string? emoji = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        string? messageEffectId = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendDice(chatId, emoji, replyParameters, replyMarkup, messageThreadId, disableNotification, protectContent, messageEffectId, businessConnectionId, allowPaidBroadcast, cancellationToken);

    /// <summary>Use this method when you need to tell the user that something is happening on the bot's side. The status is set for 5 seconds or less (when a message arrives from your bot, Telegram clients clear its typing status).<br/>We only recommend using this method when a response from the bot will take a <b>noticeable</b> amount of time to arrive.</summary>
    /// <remarks>Example: The <a href="https://t.me/imagebot">ImageBot</a> needs some time to process a request and upload the image. Instead of sending a text message along the lines of “Retrieving image, please wait…”, the bot may use <see cref="TelegramBotClientExtensions.SendChatAction">SendChatAction</see> with <paramref name="action"/> = <em>UploadPhoto</em>. The user will see a “sending photo” status for the bot.</remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="action">Type of action to broadcast. Choose one, depending on what the user is about to receive: <em>typing</em> for <see cref="TelegramBotClientExtensions.SendMessage">text messages</see>, <em>UploadPhoto</em> for <see cref="TelegramBotClientExtensions.SendPhoto">photos</see>, <em>RecordVideo</em> or <em>UploadVideo</em> for <see cref="TelegramBotClientExtensions.SendVideo">videos</see>, <em>RecordVoice</em> or <em>UploadVoice</em> for <see cref="TelegramBotClientExtensions.SendVoice">voice notes</see>, <em>UploadDocument</em> for <see cref="TelegramBotClientExtensions.SendDocument">general files</see>, <em>ChooseSticker</em> for <see cref="TelegramBotClientExtensions.SendSticker">stickers</see>, <em>FindLocation</em> for <see cref="TelegramBotClientExtensions.SendLocation">location data</see>, <em>RecordVideoNote</em> or <em>UploadVideoNote</em> for <see cref="TelegramBotClientExtensions.SendVideoNote">video notes</see>.</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread; for supergroups only</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the action will be sent</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SendChatAction(
        this ITelegramBotClient botClient,
        ChatId chatId,
        ChatAction action,
        int? messageThreadId = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendChatActionRequest
    {
        ChatId = chatId,
        Action = action,
        MessageThreadId = messageThreadId,
        BusinessConnectionId = businessConnectionId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendChatAction">SendChatAction</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendChatAction instead")]
    public static Task SendChatActionAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        ChatAction action,
        int? messageThreadId = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendChatAction(chatId, action, messageThreadId, businessConnectionId, cancellationToken);

    /// <summary>Use this method to change the chosen reactions on a message. Service messages can't be reacted to. Automatically forwarded messages from a channel to its discussion group have the same available reactions as messages in the channel. Bots can't use paid reactions.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Identifier of the target message. If the message belongs to a media group, the reaction is set to the first non-deleted message in the group instead.</param>
    /// <param name="reaction">A list of reaction types to set on the message. Currently, as non-premium users, bots can set up to one reaction per message. A custom emoji reaction can be used if it is either already present on the message or explicitly allowed by chat administrators. Paid reactions can't be used by bots.</param>
    /// <param name="isBig">Pass <see langword="true"/> to set the reaction with a big animation</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetMessageReaction(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        IEnumerable<ReactionType>? reaction,
        bool isBig = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetMessageReactionRequest
    {
        ChatId = chatId,
        MessageId = messageId,
        Reaction = reaction,
        IsBig = isBig,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetMessageReaction">SetMessageReaction</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetMessageReaction instead")]
    public static Task SetMessageReactionAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        IEnumerable<ReactionType>? reaction,
        bool isBig = default,
        CancellationToken cancellationToken = default
    ) => botClient.SetMessageReaction(chatId, messageId, reaction, isBig, cancellationToken);

    /// <summary>Use this method to get a list of profile pictures for a user.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="offset">Sequential number of the first photo to be returned. By default, all photos are returned.</param>
    /// <param name="limit">Limits the number of photos to be retrieved. Values between 1-100 are accepted. Defaults to 100.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>A <see cref="UserProfilePhotos"/> object.</returns>
    public static async Task<UserProfilePhotos> GetUserProfilePhotos(
        this ITelegramBotClient botClient,
        long userId,
        int? offset = default,
        int? limit = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetUserProfilePhotosRequest
    {
        UserId = userId,
        Offset = offset,
        Limit = limit,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetUserProfilePhotos">GetUserProfilePhotos</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetUserProfilePhotos instead")]
    public static Task<UserProfilePhotos> GetUserProfilePhotosAsync(
        this ITelegramBotClient botClient,
        long userId,
        int? offset = default,
        int? limit = default,
        CancellationToken cancellationToken = default
    ) => botClient.GetUserProfilePhotos(userId, offset, limit, cancellationToken);

    /// <summary>Changes the emoji status for a given user that previously allowed the bot to manage their emoji status via the Mini App method <a href="https://core.telegram.org/bots/webapps#initializing-mini-apps">requestEmojiStatusAccess</a>.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="emojiStatusCustomEmojiId">Custom emoji identifier of the emoji status to set. Pass an empty string to remove the status.</param>
    /// <param name="emojiStatusExpirationDate">Expiration date of the emoji status, if any</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetUserEmojiStatus(
        this ITelegramBotClient botClient,
        long userId,
        string? emojiStatusCustomEmojiId = default,
        DateTime? emojiStatusExpirationDate = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetUserEmojiStatusRequest
    {
        UserId = userId,
        EmojiStatusCustomEmojiId = emojiStatusCustomEmojiId,
        EmojiStatusExpirationDate = emojiStatusExpirationDate,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetUserEmojiStatus">SetUserEmojiStatus</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetUserEmojiStatus instead")]
    public static Task SetUserEmojiStatusAsync(
        this ITelegramBotClient botClient,
        long userId,
        string? emojiStatusCustomEmojiId = default,
        DateTime? emojiStatusExpirationDate = default,
        CancellationToken cancellationToken = default
    ) => botClient.SetUserEmojiStatus(userId, emojiStatusCustomEmojiId, emojiStatusExpirationDate, cancellationToken);

    /// <summary>Use this method to get basic information about a file and prepare it for downloading. For the moment, bots can download files of up to 20MB in size.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="fileId">File identifier to get information about</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>A <see cref="TGFile"/> object is returned. The file can then be downloaded via <see cref="TelegramBotClient.DownloadFile">DownloadFile</see>, where <c>&lt;FilePath&gt;</c> is taken from the response. It is guaranteed that the link will be valid for at least 1 hour. When the link expires, a new one can be requested by calling <see cref="TelegramBotClientExtensions.GetFile">GetFile</see> again.<br/><b>Note:</b> This function may not preserve the original file name and MIME type. You should save the file's MIME type and name (if available) when the File object is received.</returns>
    public static async Task<TGFile> GetFile(
        this ITelegramBotClient botClient,
        string fileId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetFileRequest
    {
        FileId = fileId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetFile">GetFile</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetFile instead")]
    public static Task<TGFile> GetFileAsync(
        this ITelegramBotClient botClient,
        string fileId,
        CancellationToken cancellationToken = default
    ) => botClient.GetFile(fileId, cancellationToken);

    /// <summary>Use this method to ban a user in a group, a supergroup or a channel. In the case of supergroups and channels, the user will not be able to return to the chat on their own using invite links, etc., unless <see cref="TelegramBotClientExtensions.UnbanChatMember">unbanned</see> first. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target group or username of the target supergroup or channel (in the format <c>@channelusername</c>)</param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="untilDate">Date when the user will be unbanned, in UTC. If user is banned for more than 366 days or less than 30 seconds from the current time they are considered to be banned forever. Applied for supergroups and channels only.</param>
    /// <param name="revokeMessages">Pass <see langword="true"/> to delete all messages from the chat for the user that is being removed. If <see langword="false"/>, the user will be able to see messages in the group that were sent before the user was removed. Always <see langword="true"/> for supergroups and channels.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task BanChatMember(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        DateTime? untilDate = default,
        bool revokeMessages = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new BanChatMemberRequest
    {
        ChatId = chatId,
        UserId = userId,
        UntilDate = untilDate,
        RevokeMessages = revokeMessages,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="BanChatMember">BanChatMember</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use BanChatMember instead")]
    public static Task BanChatMemberAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        DateTime? untilDate = default,
        bool revokeMessages = default,
        CancellationToken cancellationToken = default
    ) => botClient.BanChatMember(chatId, userId, untilDate, revokeMessages, cancellationToken);

    /// <summary>Use this method to unban a previously banned user in a supergroup or channel. The user will <b>not</b> return to the group or channel automatically, but will be able to join via link, etc. The bot must be an administrator for this to work. By default, this method guarantees that after the call the user is not a member of the chat, but will be able to join it. So if the user is a member of the chat they will also be <b>removed</b> from the chat. If you don't want this, use the parameter <paramref name="onlyIfBanned"/>.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target group or username of the target supergroup or channel (in the format <c>@channelusername</c>)</param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="onlyIfBanned">Do nothing if the user is not banned</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task UnbanChatMember(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        bool onlyIfBanned = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new UnbanChatMemberRequest
    {
        ChatId = chatId,
        UserId = userId,
        OnlyIfBanned = onlyIfBanned,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="UnbanChatMember">UnbanChatMember</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use UnbanChatMember instead")]
    public static Task UnbanChatMemberAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        bool onlyIfBanned = default,
        CancellationToken cancellationToken = default
    ) => botClient.UnbanChatMember(chatId, userId, onlyIfBanned, cancellationToken);

    /// <summary>Use this method to restrict a user in a supergroup. The bot must be an administrator in the supergroup for this to work and must have the appropriate administrator rights. Pass <em>True</em> for all permissions to lift restrictions from a user.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="permissions">An object for new user permissions</param>
    /// <param name="useIndependentChatPermissions">Pass <see langword="true"/> if chat permissions are set independently. Otherwise, the <em>CanSendOtherMessages</em> and <em>CanAddWebPagePreviews</em> permissions will imply the <em>CanSendMessages</em>, <em>CanSendAudios</em>, <em>CanSendDocuments</em>, <em>CanSendPhotos</em>, <em>CanSendVideos</em>, <em>CanSendVideoNotes</em>, and <em>CanSendVoiceNotes</em> permissions; the <em>CanSendPolls</em> permission will imply the <em>CanSendMessages</em> permission.</param>
    /// <param name="untilDate">Date when restrictions will be lifted for the user, in UTC. If user is restricted for more than 366 days or less than 30 seconds from the current time, they are considered to be restricted forever</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task RestrictChatMember(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        ChatPermissions permissions,
        bool useIndependentChatPermissions = default,
        DateTime? untilDate = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new RestrictChatMemberRequest
    {
        ChatId = chatId,
        UserId = userId,
        Permissions = permissions,
        UseIndependentChatPermissions = useIndependentChatPermissions,
        UntilDate = untilDate,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="RestrictChatMember">RestrictChatMember</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use RestrictChatMember instead")]
    public static Task RestrictChatMemberAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        ChatPermissions permissions,
        bool useIndependentChatPermissions = default,
        DateTime? untilDate = default,
        CancellationToken cancellationToken = default
    ) => botClient.RestrictChatMember(chatId, userId, permissions, useIndependentChatPermissions, untilDate, cancellationToken);

    /// <summary>Use this method to promote or demote a user in a supergroup or a channel. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights. Pass <em>False</em> for all boolean parameters to demote a user.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="isAnonymous">Pass <see langword="true"/> if the administrator's presence in the chat is hidden</param>
    /// <param name="canManageChat">Pass <see langword="true"/> if the administrator can access the chat event log, get boost list, see hidden supergroup and channel members, report spam messages and ignore slow mode. Implied by any other administrator privilege.</param>
    /// <param name="canPostMessages">Pass <see langword="true"/> if the administrator can post messages in the channel, or access channel statistics; for channels only</param>
    /// <param name="canEditMessages">Pass <see langword="true"/> if the administrator can edit messages of other users and can pin messages; for channels only</param>
    /// <param name="canDeleteMessages">Pass <see langword="true"/> if the administrator can delete messages of other users</param>
    /// <param name="canPostStories">Pass <see langword="true"/> if the administrator can post stories to the chat</param>
    /// <param name="canEditStories">Pass <see langword="true"/> if the administrator can edit stories posted by other users, post stories to the chat page, pin chat stories, and access the chat's story archive</param>
    /// <param name="canDeleteStories">Pass <see langword="true"/> if the administrator can delete stories posted by other users</param>
    /// <param name="canManageVideoChats">Pass <see langword="true"/> if the administrator can manage video chats</param>
    /// <param name="canRestrictMembers">Pass <see langword="true"/> if the administrator can restrict, ban or unban chat members, or access supergroup statistics</param>
    /// <param name="canPromoteMembers">Pass <see langword="true"/> if the administrator can add new administrators with a subset of their own privileges or demote administrators that they have promoted, directly or indirectly (promoted by administrators that were appointed by him)</param>
    /// <param name="canChangeInfo">Pass <see langword="true"/> if the administrator can change chat title, photo and other settings</param>
    /// <param name="canInviteUsers">Pass <see langword="true"/> if the administrator can invite new users to the chat</param>
    /// <param name="canPinMessages">Pass <see langword="true"/> if the administrator can pin messages; for supergroups only</param>
    /// <param name="canManageTopics">Pass <see langword="true"/> if the user is allowed to create, rename, close, and reopen forum topics; for supergroups only</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task PromoteChatMember(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        bool isAnonymous = default,
        bool canManageChat = default,
        bool canPostMessages = default,
        bool canEditMessages = default,
        bool canDeleteMessages = default,
        bool canPostStories = default,
        bool canEditStories = default,
        bool canDeleteStories = default,
        bool canManageVideoChats = default,
        bool canRestrictMembers = default,
        bool canPromoteMembers = default,
        bool canChangeInfo = default,
        bool canInviteUsers = default,
        bool canPinMessages = default,
        bool canManageTopics = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new PromoteChatMemberRequest
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
        CanManageVideoChats = canManageVideoChats,
        CanRestrictMembers = canRestrictMembers,
        CanPromoteMembers = canPromoteMembers,
        CanChangeInfo = canChangeInfo,
        CanInviteUsers = canInviteUsers,
        CanPinMessages = canPinMessages,
        CanManageTopics = canManageTopics,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="PromoteChatMember">PromoteChatMember</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use PromoteChatMember instead")]
    public static Task PromoteChatMemberAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        bool isAnonymous = default,
        bool canManageChat = default,
        bool canPostMessages = default,
        bool canEditMessages = default,
        bool canDeleteMessages = default,
        bool canPostStories = default,
        bool canEditStories = default,
        bool canDeleteStories = default,
        bool canManageVideoChats = default,
        bool canRestrictMembers = default,
        bool canPromoteMembers = default,
        bool canChangeInfo = default,
        bool canInviteUsers = default,
        bool canPinMessages = default,
        bool canManageTopics = default,
        CancellationToken cancellationToken = default
    ) => botClient.PromoteChatMember(chatId, userId, isAnonymous, canManageChat, canPostMessages, canEditMessages, canDeleteMessages, canPostStories, canEditStories, canDeleteStories, canManageVideoChats, canRestrictMembers, canPromoteMembers, canChangeInfo, canInviteUsers, canPinMessages, canManageTopics, cancellationToken);

    /// <summary>Use this method to set a custom title for an administrator in a supergroup promoted by the bot.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="customTitle">New custom title for the administrator; 0-16 characters, emoji are not allowed</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetChatAdministratorCustomTitle(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        string customTitle,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetChatAdministratorCustomTitleRequest
    {
        ChatId = chatId,
        UserId = userId,
        CustomTitle = customTitle,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetChatAdministratorCustomTitle">SetChatAdministratorCustomTitle</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetChatAdministratorCustomTitle instead")]
    public static Task SetChatAdministratorCustomTitleAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        string customTitle,
        CancellationToken cancellationToken = default
    ) => botClient.SetChatAdministratorCustomTitle(chatId, userId, customTitle, cancellationToken);

    /// <summary>Use this method to ban a channel chat in a supergroup or a channel. Until the chat is <see cref="TelegramBotClientExtensions.UnbanChatSenderChat">unbanned</see>, the owner of the banned chat won't be able to send messages on behalf of <b>any of their channels</b>. The bot must be an administrator in the supergroup or channel for this to work and must have the appropriate administrator rights.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="senderChatId">Unique identifier of the target sender chat</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task BanChatSenderChat(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long senderChatId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new BanChatSenderChatRequest
    {
        ChatId = chatId,
        SenderChatId = senderChatId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="BanChatSenderChat">BanChatSenderChat</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use BanChatSenderChat instead")]
    public static Task BanChatSenderChatAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long senderChatId,
        CancellationToken cancellationToken = default
    ) => botClient.BanChatSenderChat(chatId, senderChatId, cancellationToken);

    /// <summary>Use this method to unban a previously banned channel chat in a supergroup or channel. The bot must be an administrator for this to work and must have the appropriate administrator rights.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="senderChatId">Unique identifier of the target sender chat</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task UnbanChatSenderChat(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long senderChatId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new UnbanChatSenderChatRequest
    {
        ChatId = chatId,
        SenderChatId = senderChatId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="UnbanChatSenderChat">UnbanChatSenderChat</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use UnbanChatSenderChat instead")]
    public static Task UnbanChatSenderChatAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long senderChatId,
        CancellationToken cancellationToken = default
    ) => botClient.UnbanChatSenderChat(chatId, senderChatId, cancellationToken);

    /// <summary>Use this method to set default chat permissions for all members. The bot must be an administrator in the group or a supergroup for this to work and must have the <em>CanRestrictMembers</em> administrator rights.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="permissions">An object for new default chat permissions</param>
    /// <param name="useIndependentChatPermissions">Pass <see langword="true"/> if chat permissions are set independently. Otherwise, the <em>CanSendOtherMessages</em> and <em>CanAddWebPagePreviews</em> permissions will imply the <em>CanSendMessages</em>, <em>CanSendAudios</em>, <em>CanSendDocuments</em>, <em>CanSendPhotos</em>, <em>CanSendVideos</em>, <em>CanSendVideoNotes</em>, and <em>CanSendVoiceNotes</em> permissions; the <em>CanSendPolls</em> permission will imply the <em>CanSendMessages</em> permission.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetChatPermissions(
        this ITelegramBotClient botClient,
        ChatId chatId,
        ChatPermissions permissions,
        bool useIndependentChatPermissions = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetChatPermissionsRequest
    {
        ChatId = chatId,
        Permissions = permissions,
        UseIndependentChatPermissions = useIndependentChatPermissions,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetChatPermissions">SetChatPermissions</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetChatPermissions instead")]
    public static Task SetChatPermissionsAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        ChatPermissions permissions,
        bool useIndependentChatPermissions = default,
        CancellationToken cancellationToken = default
    ) => botClient.SetChatPermissions(chatId, permissions, useIndependentChatPermissions, cancellationToken);

    /// <summary>Use this method to generate a new primary invite link for a chat; any previously generated primary link is revoked. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights.</summary>
    /// <remarks>Note: Each administrator in a chat generates their own invite links. Bots can't use invite links generated by other administrators. If you want your bot to work with invite links, it will need to generate its own link using <see cref="TelegramBotClientExtensions.ExportChatInviteLink">ExportChatInviteLink</see> or by calling the <see cref="TelegramBotClientExtensions.GetChat">GetChat</see> method. If your bot needs to generate a new primary invite link replacing its previous one, use <see cref="TelegramBotClientExtensions.ExportChatInviteLink">ExportChatInviteLink</see> again.</remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The new invite link as <em>String</em> on success.</returns>
    public static async Task<string> ExportChatInviteLink(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new ExportChatInviteLinkRequest
    {
        ChatId = chatId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="ExportChatInviteLink">ExportChatInviteLink</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use ExportChatInviteLink instead")]
    public static Task<string> ExportChatInviteLinkAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => botClient.ExportChatInviteLink(chatId, cancellationToken);

    /// <summary>Use this method to create an additional invite link for a chat. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights. The link can be revoked using the method <see cref="TelegramBotClientExtensions.RevokeChatInviteLink">RevokeChatInviteLink</see>.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="name">Invite link name; 0-32 characters</param>
    /// <param name="expireDate">Point in time when the link will expire</param>
    /// <param name="memberLimit">The maximum number of users that can be members of the chat simultaneously after joining the chat via this invite link; 1-99999</param>
    /// <param name="createsJoinRequest"><see langword="true"/>, if users joining the chat via the link need to be approved by chat administrators. If <see langword="true"/>, <paramref name="memberLimit"/> can't be specified</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The new invite link as <see cref="ChatInviteLink"/> object.</returns>
    public static async Task<ChatInviteLink> CreateChatInviteLink(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string? name = default,
        DateTime? expireDate = default,
        int? memberLimit = default,
        bool createsJoinRequest = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new CreateChatInviteLinkRequest
    {
        ChatId = chatId,
        Name = name,
        ExpireDate = expireDate,
        MemberLimit = memberLimit,
        CreatesJoinRequest = createsJoinRequest,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="CreateChatInviteLink">CreateChatInviteLink</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use CreateChatInviteLink instead")]
    public static Task<ChatInviteLink> CreateChatInviteLinkAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string? name = default,
        DateTime? expireDate = default,
        int? memberLimit = default,
        bool createsJoinRequest = default,
        CancellationToken cancellationToken = default
    ) => botClient.CreateChatInviteLink(chatId, name, expireDate, memberLimit, createsJoinRequest, cancellationToken);

    /// <summary>Use this method to edit a non-primary invite link created by the bot. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="inviteLink">The invite link to edit</param>
    /// <param name="name">Invite link name; 0-32 characters</param>
    /// <param name="expireDate">Point in time when the link will expire</param>
    /// <param name="memberLimit">The maximum number of users that can be members of the chat simultaneously after joining the chat via this invite link; 1-99999</param>
    /// <param name="createsJoinRequest"><see langword="true"/>, if users joining the chat via the link need to be approved by chat administrators. If <see langword="true"/>, <paramref name="memberLimit"/> can't be specified</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The edited invite link as a <see cref="ChatInviteLink"/> object.</returns>
    public static async Task<ChatInviteLink> EditChatInviteLink(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string inviteLink,
        string? name = default,
        DateTime? expireDate = default,
        int? memberLimit = default,
        bool createsJoinRequest = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new EditChatInviteLinkRequest
    {
        ChatId = chatId,
        InviteLink = inviteLink,
        Name = name,
        ExpireDate = expireDate,
        MemberLimit = memberLimit,
        CreatesJoinRequest = createsJoinRequest,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="EditChatInviteLink">EditChatInviteLink</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use EditChatInviteLink instead")]
    public static Task<ChatInviteLink> EditChatInviteLinkAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string inviteLink,
        string? name = default,
        DateTime? expireDate = default,
        int? memberLimit = default,
        bool createsJoinRequest = default,
        CancellationToken cancellationToken = default
    ) => botClient.EditChatInviteLink(chatId, inviteLink, name, expireDate, memberLimit, createsJoinRequest, cancellationToken);

    /// <summary>Use this method to create a <a href="https://telegram.org/blog/superchannels-star-reactions-subscriptions#star-subscriptions">subscription invite link</a> for a channel chat. The bot must have the <em>CanInviteUsers</em> administrator rights. The link can be edited using the method <see cref="TelegramBotClientExtensions.EditChatSubscriptionInviteLink">EditChatSubscriptionInviteLink</see> or revoked using the method <see cref="TelegramBotClientExtensions.RevokeChatInviteLink">RevokeChatInviteLink</see>.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target channel chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="subscriptionPeriod">The number of seconds the subscription will be active for before the next payment. Currently, it must always be 2592000 (30 days).</param>
    /// <param name="subscriptionPrice">The amount of Telegram Stars a user must pay initially and after each subsequent subscription period to be a member of the chat; 1-2500</param>
    /// <param name="name">Invite link name; 0-32 characters</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The new invite link as a <see cref="ChatInviteLink"/> object.</returns>
    public static async Task<ChatInviteLink> CreateChatSubscriptionInviteLink(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int subscriptionPeriod,
        int subscriptionPrice,
        string? name = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new CreateChatSubscriptionInviteLinkRequest
    {
        ChatId = chatId,
        SubscriptionPeriod = subscriptionPeriod,
        SubscriptionPrice = subscriptionPrice,
        Name = name,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="CreateChatSubscriptionInviteLink">CreateChatSubscriptionInviteLink</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use CreateChatSubscriptionInviteLink instead")]
    public static Task<ChatInviteLink> CreateChatSubscriptionInviteLinkAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int subscriptionPeriod,
        int subscriptionPrice,
        string? name = default,
        CancellationToken cancellationToken = default
    ) => botClient.CreateChatSubscriptionInviteLink(chatId, subscriptionPeriod, subscriptionPrice, name, cancellationToken);

    /// <summary>Use this method to edit a subscription invite link created by the bot. The bot must have the <em>CanInviteUsers</em> administrator rights.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="inviteLink">The invite link to edit</param>
    /// <param name="name">Invite link name; 0-32 characters</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The edited invite link as a <see cref="ChatInviteLink"/> object.</returns>
    public static async Task<ChatInviteLink> EditChatSubscriptionInviteLink(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string inviteLink,
        string? name = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new EditChatSubscriptionInviteLinkRequest
    {
        ChatId = chatId,
        InviteLink = inviteLink,
        Name = name,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="EditChatSubscriptionInviteLink">EditChatSubscriptionInviteLink</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use EditChatSubscriptionInviteLink instead")]
    public static Task<ChatInviteLink> EditChatSubscriptionInviteLinkAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string inviteLink,
        string? name = default,
        CancellationToken cancellationToken = default
    ) => botClient.EditChatSubscriptionInviteLink(chatId, inviteLink, name, cancellationToken);

    /// <summary>Use this method to revoke an invite link created by the bot. If the primary link is revoked, a new link is automatically generated. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier of the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="inviteLink">The invite link to revoke</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The revoked invite link as <see cref="ChatInviteLink"/> object.</returns>
    public static async Task<ChatInviteLink> RevokeChatInviteLink(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string inviteLink,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new RevokeChatInviteLinkRequest
    {
        ChatId = chatId,
        InviteLink = inviteLink,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="RevokeChatInviteLink">RevokeChatInviteLink</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use RevokeChatInviteLink instead")]
    public static Task<ChatInviteLink> RevokeChatInviteLinkAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string inviteLink,
        CancellationToken cancellationToken = default
    ) => botClient.RevokeChatInviteLink(chatId, inviteLink, cancellationToken);

    /// <summary>Use this method to approve a chat join request. The bot must be an administrator in the chat for this to work and must have the <em>CanInviteUsers</em> administrator right.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task ApproveChatJoinRequest(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new ApproveChatJoinRequest
    {
        ChatId = chatId,
        UserId = userId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="ApproveChatJoinRequest">ApproveChatJoinRequest</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use ApproveChatJoinRequest instead")]
    public static Task ApproveChatJoinRequestAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        CancellationToken cancellationToken = default
    ) => botClient.ApproveChatJoinRequest(chatId, userId, cancellationToken);

    /// <summary>Use this method to decline a chat join request. The bot must be an administrator in the chat for this to work and must have the <em>CanInviteUsers</em> administrator right.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task DeclineChatJoinRequest(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new DeclineChatJoinRequest
    {
        ChatId = chatId,
        UserId = userId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="DeclineChatJoinRequest">DeclineChatJoinRequest</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use DeclineChatJoinRequest instead")]
    public static Task DeclineChatJoinRequestAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        CancellationToken cancellationToken = default
    ) => botClient.DeclineChatJoinRequest(chatId, userId, cancellationToken);

    /// <summary>Use this method to set a new profile photo for the chat. Photos can't be changed for private chats. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="photo">New chat photo, uploaded using <see cref="InputFileStream"/></param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetChatPhoto(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFileStream photo,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetChatPhotoRequest
    {
        ChatId = chatId,
        Photo = photo,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetChatPhoto">SetChatPhoto</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetChatPhoto instead")]
    public static Task SetChatPhotoAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFileStream photo,
        CancellationToken cancellationToken = default
    ) => botClient.SetChatPhoto(chatId, photo, cancellationToken);

    /// <summary>Use this method to delete a chat photo. Photos can't be changed for private chats. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task DeleteChatPhoto(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new DeleteChatPhotoRequest
    {
        ChatId = chatId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="DeleteChatPhoto">DeleteChatPhoto</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use DeleteChatPhoto instead")]
    public static Task DeleteChatPhotoAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => botClient.DeleteChatPhoto(chatId, cancellationToken);

    /// <summary>Use this method to change the title of a chat. Titles can't be changed for private chats. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="title">New chat title, 1-128 characters</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetChatTitle(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string title,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetChatTitleRequest
    {
        ChatId = chatId,
        Title = title,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetChatTitle">SetChatTitle</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetChatTitle instead")]
    public static Task SetChatTitleAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string title,
        CancellationToken cancellationToken = default
    ) => botClient.SetChatTitle(chatId, title, cancellationToken);

    /// <summary>Use this method to change the description of a group, a supergroup or a channel. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="description">New chat description, 0-255 characters</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetChatDescription(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string? description = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetChatDescriptionRequest
    {
        ChatId = chatId,
        Description = description,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetChatDescription">SetChatDescription</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetChatDescription instead")]
    public static Task SetChatDescriptionAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string? description = default,
        CancellationToken cancellationToken = default
    ) => botClient.SetChatDescription(chatId, description, cancellationToken);

    /// <summary>Use this method to add a message to the list of pinned messages in a chat. If the chat is not a private chat, the bot must be an administrator in the chat for this to work and must have the 'CanPinMessages' administrator right in a supergroup or 'CanEditMessages' administrator right in a channel.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Identifier of a message to pin</param>
    /// <param name="disableNotification">Pass <see langword="true"/> if it is not necessary to send a notification to all chat members about the new pinned message. Notifications are always disabled in channels and private chats.</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be pinned</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task PinChatMessage(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        bool disableNotification = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new PinChatMessageRequest
    {
        ChatId = chatId,
        MessageId = messageId,
        DisableNotification = disableNotification,
        BusinessConnectionId = businessConnectionId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="PinChatMessage">PinChatMessage</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use PinChatMessage instead")]
    public static Task PinChatMessageAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        bool disableNotification = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.PinChatMessage(chatId, messageId, disableNotification, businessConnectionId, cancellationToken);

    /// <summary>Use this method to remove a message from the list of pinned messages in a chat. If the chat is not a private chat, the bot must be an administrator in the chat for this to work and must have the 'CanPinMessages' administrator right in a supergroup or 'CanEditMessages' administrator right in a channel.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Identifier of the message to unpin. Required if <paramref name="businessConnectionId"/> is specified. If not specified, the most recent pinned message (by sending date) will be unpinned.</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be unpinned</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task UnpinChatMessage(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int? messageId = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new UnpinChatMessageRequest
    {
        ChatId = chatId,
        MessageId = messageId,
        BusinessConnectionId = businessConnectionId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="UnpinChatMessage">UnpinChatMessage</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use UnpinChatMessage instead")]
    public static Task UnpinChatMessageAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int? messageId = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.UnpinChatMessage(chatId, messageId, businessConnectionId, cancellationToken);

    /// <summary>Use this method to clear the list of pinned messages in a chat. If the chat is not a private chat, the bot must be an administrator in the chat for this to work and must have the 'CanPinMessages' administrator right in a supergroup or 'CanEditMessages' administrator right in a channel.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task UnpinAllChatMessages(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new UnpinAllChatMessagesRequest
    {
        ChatId = chatId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="UnpinAllChatMessages">UnpinAllChatMessages</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use UnpinAllChatMessages instead")]
    public static Task UnpinAllChatMessagesAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => botClient.UnpinAllChatMessages(chatId, cancellationToken);

    /// <summary>Use this method for your bot to leave a group, supergroup or channel.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup or channel (in the format <c>@channelusername</c>)</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task LeaveChat(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new LeaveChatRequest
    {
        ChatId = chatId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="LeaveChat">LeaveChat</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use LeaveChat instead")]
    public static Task LeaveChatAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => botClient.LeaveChat(chatId, cancellationToken);

    /// <summary>Use this method to get up-to-date information about the chat.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup or channel (in the format <c>@channelusername</c>)</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>A <see cref="ChatFullInfo"/> object on success.</returns>
    public static async Task<ChatFullInfo> GetChat(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetChatRequest
    {
        ChatId = chatId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetChat">GetChat</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetChat instead")]
    public static Task<ChatFullInfo> GetChatAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => botClient.GetChat(chatId, cancellationToken);

    /// <summary>Use this method to get a list of administrators in a chat, which aren't bots.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup or channel (in the format <c>@channelusername</c>)</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>An Array of <see cref="ChatMember"/> objects.</returns>
    public static async Task<ChatMember[]> GetChatAdministrators(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetChatAdministratorsRequest
    {
        ChatId = chatId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetChatAdministrators">GetChatAdministrators</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetChatAdministrators instead")]
    public static Task<ChatMember[]> GetChatAdministratorsAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => botClient.GetChatAdministrators(chatId, cancellationToken);

    /// <summary>Use this method to get the number of members in a chat.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup or channel (in the format <c>@channelusername</c>)</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns><em>Int</em> on success.</returns>
    public static async Task<int> GetChatMemberCount(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetChatMemberCountRequest
    {
        ChatId = chatId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetChatMemberCount">GetChatMemberCount</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetChatMemberCount instead")]
    public static Task<int> GetChatMemberCountAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => botClient.GetChatMemberCount(chatId, cancellationToken);

    /// <summary>Use this method to get information about a member of a chat. The method is only guaranteed to work for other users if the bot is an administrator in the chat.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup or channel (in the format <c>@channelusername</c>)</param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>A <see cref="ChatMember"/> object on success.</returns>
    public static async Task<ChatMember> GetChatMember(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetChatMemberRequest
    {
        ChatId = chatId,
        UserId = userId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetChatMember">GetChatMember</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetChatMember instead")]
    public static Task<ChatMember> GetChatMemberAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        CancellationToken cancellationToken = default
    ) => botClient.GetChatMember(chatId, userId, cancellationToken);

    /// <summary>Use this method to set a new group sticker set for a supergroup. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights. Use the field <em>CanSetStickerSet</em> optionally returned in <see cref="TelegramBotClientExtensions.GetChat">GetChat</see> requests to check if the bot can use this method.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="stickerSetName">Name of the sticker set to be set as the group sticker set</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetChatStickerSet(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string stickerSetName,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetChatStickerSetRequest
    {
        ChatId = chatId,
        StickerSetName = stickerSetName,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetChatStickerSet">SetChatStickerSet</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetChatStickerSet instead")]
    public static Task SetChatStickerSetAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string stickerSetName,
        CancellationToken cancellationToken = default
    ) => botClient.SetChatStickerSet(chatId, stickerSetName, cancellationToken);

    /// <summary>Use this method to delete a group sticker set from a supergroup. The bot must be an administrator in the chat for this to work and must have the appropriate administrator rights. Use the field <em>CanSetStickerSet</em> optionally returned in <see cref="TelegramBotClientExtensions.GetChat">GetChat</see> requests to check if the bot can use this method.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task DeleteChatStickerSet(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new DeleteChatStickerSetRequest
    {
        ChatId = chatId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="DeleteChatStickerSet">DeleteChatStickerSet</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use DeleteChatStickerSet instead")]
    public static Task DeleteChatStickerSetAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => botClient.DeleteChatStickerSet(chatId, cancellationToken);

    /// <summary>Use this method to get custom emoji stickers, which can be used as a forum topic icon by any user.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>An Array of <see cref="Sticker"/> objects.</returns>
    public static async Task<Sticker[]> GetForumTopicIconStickers(
        this ITelegramBotClient botClient,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetForumTopicIconStickersRequest
    {
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetForumTopicIconStickers">GetForumTopicIconStickers</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetForumTopicIconStickers instead")]
    public static Task<Sticker[]> GetForumTopicIconStickersAsync(
        this ITelegramBotClient botClient,
        CancellationToken cancellationToken = default
    ) => botClient.GetForumTopicIconStickers(cancellationToken);

    /// <summary>Use this method to create a topic in a forum supergroup chat. The bot must be an administrator in the chat for this to work and must have the <em>CanManageTopics</em> administrator rights.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="name">Topic name, 1-128 characters</param>
    /// <param name="iconColor">Color of the topic icon in RGB format. Currently, must be one of 7322096 (0x6FB9F0), 16766590 (0xFFD67E), 13338331 (0xCB86DB), 9367192 (0x8EEE98), 16749490 (0xFF93B2), or 16478047 (0xFB6F5F)</param>
    /// <param name="iconCustomEmojiId">Unique identifier of the custom emoji shown as the topic icon. Use <see cref="TelegramBotClientExtensions.GetForumTopicIconStickers">GetForumTopicIconStickers</see> to get all allowed custom emoji identifiers.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>Information about the created topic as a <see cref="ForumTopic"/> object.</returns>
    public static async Task<ForumTopic> CreateForumTopic(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string name,
        int? iconColor = default,
        string? iconCustomEmojiId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new CreateForumTopicRequest
    {
        ChatId = chatId,
        Name = name,
        IconColor = iconColor,
        IconCustomEmojiId = iconCustomEmojiId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="CreateForumTopic">CreateForumTopic</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use CreateForumTopic instead")]
    public static Task<ForumTopic> CreateForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string name,
        int? iconColor = default,
        string? iconCustomEmojiId = default,
        CancellationToken cancellationToken = default
    ) => botClient.CreateForumTopic(chatId, name, iconColor, iconCustomEmojiId, cancellationToken);

    /// <summary>Use this method to edit name and icon of a topic in a forum supergroup chat. The bot must be an administrator in the chat for this to work and must have the <em>CanManageTopics</em> administrator rights, unless it is the creator of the topic.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread of the forum topic</param>
    /// <param name="name">New topic name, 0-128 characters. If not specified or empty, the current name of the topic will be kept</param>
    /// <param name="iconCustomEmojiId">New unique identifier of the custom emoji shown as the topic icon. Use <see cref="TelegramBotClientExtensions.GetForumTopicIconStickers">GetForumTopicIconStickers</see> to get all allowed custom emoji identifiers. Pass an empty string to remove the icon. If not specified, the current icon will be kept</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task EditForumTopic(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageThreadId,
        string? name = default,
        string? iconCustomEmojiId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new EditForumTopicRequest
    {
        ChatId = chatId,
        MessageThreadId = messageThreadId,
        Name = name,
        IconCustomEmojiId = iconCustomEmojiId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="EditForumTopic">EditForumTopic</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use EditForumTopic instead")]
    public static Task EditForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageThreadId,
        string? name = default,
        string? iconCustomEmojiId = default,
        CancellationToken cancellationToken = default
    ) => botClient.EditForumTopic(chatId, messageThreadId, name, iconCustomEmojiId, cancellationToken);

    /// <summary>Use this method to close an open topic in a forum supergroup chat. The bot must be an administrator in the chat for this to work and must have the <em>CanManageTopics</em> administrator rights, unless it is the creator of the topic.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread of the forum topic</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task CloseForumTopic(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageThreadId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new CloseForumTopicRequest
    {
        ChatId = chatId,
        MessageThreadId = messageThreadId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="CloseForumTopic">CloseForumTopic</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use CloseForumTopic instead")]
    public static Task CloseForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageThreadId,
        CancellationToken cancellationToken = default
    ) => botClient.CloseForumTopic(chatId, messageThreadId, cancellationToken);

    /// <summary>Use this method to reopen a closed topic in a forum supergroup chat. The bot must be an administrator in the chat for this to work and must have the <em>CanManageTopics</em> administrator rights, unless it is the creator of the topic.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread of the forum topic</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task ReopenForumTopic(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageThreadId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new ReopenForumTopicRequest
    {
        ChatId = chatId,
        MessageThreadId = messageThreadId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="ReopenForumTopic">ReopenForumTopic</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use ReopenForumTopic instead")]
    public static Task ReopenForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageThreadId,
        CancellationToken cancellationToken = default
    ) => botClient.ReopenForumTopic(chatId, messageThreadId, cancellationToken);

    /// <summary>Use this method to delete a forum topic along with all its messages in a forum supergroup chat. The bot must be an administrator in the chat for this to work and must have the <em>CanDeleteMessages</em> administrator rights.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread of the forum topic</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task DeleteForumTopic(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageThreadId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new DeleteForumTopicRequest
    {
        ChatId = chatId,
        MessageThreadId = messageThreadId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="DeleteForumTopic">DeleteForumTopic</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use DeleteForumTopic instead")]
    public static Task DeleteForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageThreadId,
        CancellationToken cancellationToken = default
    ) => botClient.DeleteForumTopic(chatId, messageThreadId, cancellationToken);

    /// <summary>Use this method to clear the list of pinned messages in a forum topic. The bot must be an administrator in the chat for this to work and must have the <em>CanPinMessages</em> administrator right in the supergroup.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread of the forum topic</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task UnpinAllForumTopicMessages(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageThreadId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new UnpinAllForumTopicMessagesRequest
    {
        ChatId = chatId,
        MessageThreadId = messageThreadId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="UnpinAllForumTopicMessages">UnpinAllForumTopicMessages</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use UnpinAllForumTopicMessages instead")]
    public static Task UnpinAllForumTopicMessagesAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageThreadId,
        CancellationToken cancellationToken = default
    ) => botClient.UnpinAllForumTopicMessages(chatId, messageThreadId, cancellationToken);

    /// <summary>Use this method to edit the name of the 'General' topic in a forum supergroup chat. The bot must be an administrator in the chat for this to work and must have the <em>CanManageTopics</em> administrator rights.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="name">New topic name, 1-128 characters</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task EditGeneralForumTopic(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string name,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new EditGeneralForumTopicRequest
    {
        ChatId = chatId,
        Name = name,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="EditGeneralForumTopic">EditGeneralForumTopic</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use EditGeneralForumTopic instead")]
    public static Task EditGeneralForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string name,
        CancellationToken cancellationToken = default
    ) => botClient.EditGeneralForumTopic(chatId, name, cancellationToken);

    /// <summary>Use this method to close an open 'General' topic in a forum supergroup chat. The bot must be an administrator in the chat for this to work and must have the <em>CanManageTopics</em> administrator rights.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task CloseGeneralForumTopic(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new CloseGeneralForumTopicRequest
    {
        ChatId = chatId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="CloseGeneralForumTopic">CloseGeneralForumTopic</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use CloseGeneralForumTopic instead")]
    public static Task CloseGeneralForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => botClient.CloseGeneralForumTopic(chatId, cancellationToken);

    /// <summary>Use this method to reopen a closed 'General' topic in a forum supergroup chat. The bot must be an administrator in the chat for this to work and must have the <em>CanManageTopics</em> administrator rights. The topic will be automatically unhidden if it was hidden.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task ReopenGeneralForumTopic(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new ReopenGeneralForumTopicRequest
    {
        ChatId = chatId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="ReopenGeneralForumTopic">ReopenGeneralForumTopic</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use ReopenGeneralForumTopic instead")]
    public static Task ReopenGeneralForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => botClient.ReopenGeneralForumTopic(chatId, cancellationToken);

    /// <summary>Use this method to hide the 'General' topic in a forum supergroup chat. The bot must be an administrator in the chat for this to work and must have the <em>CanManageTopics</em> administrator rights. The topic will be automatically closed if it was open.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task HideGeneralForumTopic(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new HideGeneralForumTopicRequest
    {
        ChatId = chatId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="HideGeneralForumTopic">HideGeneralForumTopic</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use HideGeneralForumTopic instead")]
    public static Task HideGeneralForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => botClient.HideGeneralForumTopic(chatId, cancellationToken);

    /// <summary>Use this method to unhide the 'General' topic in a forum supergroup chat. The bot must be an administrator in the chat for this to work and must have the <em>CanManageTopics</em> administrator rights.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task UnhideGeneralForumTopic(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new UnhideGeneralForumTopicRequest
    {
        ChatId = chatId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="UnhideGeneralForumTopic">UnhideGeneralForumTopic</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use UnhideGeneralForumTopic instead")]
    public static Task UnhideGeneralForumTopicAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => botClient.UnhideGeneralForumTopic(chatId, cancellationToken);

    /// <summary>Use this method to clear the list of pinned messages in a General forum topic. The bot must be an administrator in the chat for this to work and must have the <em>CanPinMessages</em> administrator right in the supergroup.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target supergroup (in the format <c>@supergroupusername</c>)</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task UnpinAllGeneralForumTopicMessages(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new UnpinAllGeneralForumTopicMessagesRequest
    {
        ChatId = chatId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="UnpinAllGeneralForumTopicMessages">UnpinAllGeneralForumTopicMessages</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use UnpinAllGeneralForumTopicMessages instead")]
    public static Task UnpinAllGeneralForumTopicMessagesAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => botClient.UnpinAllGeneralForumTopicMessages(chatId, cancellationToken);

    /// <summary>Use this method to send answers to callback queries sent from <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboards</a>. The answer will be displayed to the user as a notification at the top of the chat screen or as an alert</summary>
    /// <remarks>Alternatively, the user can be redirected to the specified Game URL. For this option to work, you must first create a game for your bot via <a href="https://t.me/botfather">@BotFather</a> and accept the terms. Otherwise, you may use links like <c>t.me/your_bot?start=XXXX</c> that open your bot with a parameter.</remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="callbackQueryId">Unique identifier for the query to be answered</param>
    /// <param name="text">Text of the notification. If not specified, nothing will be shown to the user, 0-200 characters</param>
    /// <param name="showAlert">If <see langword="true"/>, an alert will be shown by the client instead of a notification at the top of the chat screen. Defaults to <see langword="false"/>.</param>
    /// <param name="url">URL that will be opened by the user's client. If you have created a <see cref="Game"/> and accepted the conditions via <a href="https://t.me/botfather">@BotFather</a>, specify the URL that opens your game - note that this will only work if the query comes from a <see cref="InlineKeyboardButton"><em>CallbackGame</em></see> button.<br/><br/>Otherwise, you may use links like <c>t.me/your_bot?start=XXXX</c> that open your bot with a parameter.</param>
    /// <param name="cacheTime">The maximum amount of time in seconds that the result of the callback query may be cached client-side. Telegram apps will support caching starting in version 3.14. Defaults to 0.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task AnswerCallbackQuery(
        this ITelegramBotClient botClient,
        string callbackQueryId,
        string? text = default,
        bool showAlert = default,
        string? url = default,
        int? cacheTime = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new AnswerCallbackQueryRequest
    {
        CallbackQueryId = callbackQueryId,
        Text = text,
        ShowAlert = showAlert,
        Url = url,
        CacheTime = cacheTime,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="AnswerCallbackQuery">AnswerCallbackQuery</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use AnswerCallbackQuery instead")]
    public static Task AnswerCallbackQueryAsync(
        this ITelegramBotClient botClient,
        string callbackQueryId,
        string? text = default,
        bool showAlert = default,
        string? url = default,
        int? cacheTime = default,
        CancellationToken cancellationToken = default
    ) => botClient.AnswerCallbackQuery(callbackQueryId, text, showAlert, url, cacheTime, cancellationToken);

    /// <summary>Use this method to get the list of boosts added to a chat by a user. Requires administrator rights in the chat.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the chat or username of the channel (in the format <c>@channelusername</c>)</param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>A <see cref="UserChatBoosts"/> object.</returns>
    public static async Task<UserChatBoosts> GetUserChatBoosts(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetUserChatBoostsRequest
    {
        ChatId = chatId,
        UserId = userId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetUserChatBoosts">GetUserChatBoosts</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetUserChatBoosts instead")]
    public static Task<UserChatBoosts> GetUserChatBoostsAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        long userId,
        CancellationToken cancellationToken = default
    ) => botClient.GetUserChatBoosts(chatId, userId, cancellationToken);

    /// <summary>Use this method to get information about the connection of the bot with a business account.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="businessConnectionId">Unique identifier of the business connection</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>A <see cref="BusinessConnection"/> object on success.</returns>
    public static async Task<BusinessConnection> GetBusinessConnection(
        this ITelegramBotClient botClient,
        string businessConnectionId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetBusinessConnectionRequest
    {
        BusinessConnectionId = businessConnectionId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetBusinessConnection">GetBusinessConnection</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetBusinessConnection instead")]
    public static Task<BusinessConnection> GetBusinessConnectionAsync(
        this ITelegramBotClient botClient,
        string businessConnectionId,
        CancellationToken cancellationToken = default
    ) => botClient.GetBusinessConnection(businessConnectionId, cancellationToken);

    /// <summary>Use this method to change the list of the bot's commands. See <a href="https://core.telegram.org/bots/features#commands">this manual</a> for more details about bot commands.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="commands">A list of bot commands to be set as the list of the bot's commands. At most 100 commands can be specified.</param>
    /// <param name="scope">An object, describing scope of users for which the commands are relevant. Defaults to <see cref="BotCommandScopeDefault"/>.</param>
    /// <param name="languageCode">A two-letter ISO 639-1 language code. If empty, commands will be applied to all users from the given scope, for whose language there are no dedicated commands</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetMyCommands(
        this ITelegramBotClient botClient,
        IEnumerable<BotCommand> commands,
        BotCommandScope? scope = default,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetMyCommandsRequest
    {
        Commands = commands,
        Scope = scope,
        LanguageCode = languageCode,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetMyCommands">SetMyCommands</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetMyCommands instead")]
    public static Task SetMyCommandsAsync(
        this ITelegramBotClient botClient,
        IEnumerable<BotCommand> commands,
        BotCommandScope? scope = default,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) => botClient.SetMyCommands(commands, scope, languageCode, cancellationToken);

    /// <summary>Use this method to delete the list of the bot's commands for the given scope and user language. After deletion, <a href="https://core.telegram.org/bots/api#determining-list-of-commands">higher level commands</a> will be shown to affected users.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="scope">An object, describing scope of users for which the commands are relevant. Defaults to <see cref="BotCommandScopeDefault"/>.</param>
    /// <param name="languageCode">A two-letter ISO 639-1 language code. If empty, commands will be applied to all users from the given scope, for whose language there are no dedicated commands</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task DeleteMyCommands(
        this ITelegramBotClient botClient,
        BotCommandScope? scope = default,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new DeleteMyCommandsRequest
    {
        Scope = scope,
        LanguageCode = languageCode,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="DeleteMyCommands">DeleteMyCommands</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use DeleteMyCommands instead")]
    public static Task DeleteMyCommandsAsync(
        this ITelegramBotClient botClient,
        BotCommandScope? scope = default,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) => botClient.DeleteMyCommands(scope, languageCode, cancellationToken);

    /// <summary>Use this method to get the current list of the bot's commands for the given scope and user language.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="scope">An object, describing scope of users. Defaults to <see cref="BotCommandScopeDefault"/>.</param>
    /// <param name="languageCode">A two-letter ISO 639-1 language code or an empty string</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>An Array of <see cref="BotCommand"/> objects. If commands aren't set, an empty list is returned.</returns>
    public static async Task<BotCommand[]> GetMyCommands(
        this ITelegramBotClient botClient,
        BotCommandScope? scope = default,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetMyCommandsRequest
    {
        Scope = scope,
        LanguageCode = languageCode,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetMyCommands">GetMyCommands</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetMyCommands instead")]
    public static Task<BotCommand[]> GetMyCommandsAsync(
        this ITelegramBotClient botClient,
        BotCommandScope? scope = default,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) => botClient.GetMyCommands(scope, languageCode, cancellationToken);

    /// <summary>Use this method to change the bot's name.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="name">New bot name; 0-64 characters. Pass an empty string to remove the dedicated name for the given language.</param>
    /// <param name="languageCode">A two-letter ISO 639-1 language code. If empty, the name will be shown to all users for whose language there is no dedicated name.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetMyName(
        this ITelegramBotClient botClient,
        string? name = default,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetMyNameRequest
    {
        Name = name,
        LanguageCode = languageCode,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetMyName">SetMyName</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetMyName instead")]
    public static Task SetMyNameAsync(
        this ITelegramBotClient botClient,
        string? name = default,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) => botClient.SetMyName(name, languageCode, cancellationToken);

    /// <summary>Use this method to get the current bot name for the given user language.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="languageCode">A two-letter ISO 639-1 language code or an empty string</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns><see cref="BotName"/> on success.</returns>
    public static async Task<BotName> GetMyName(
        this ITelegramBotClient botClient,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetMyNameRequest
    {
        LanguageCode = languageCode,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetMyName">GetMyName</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetMyName instead")]
    public static Task<BotName> GetMyNameAsync(
        this ITelegramBotClient botClient,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) => botClient.GetMyName(languageCode, cancellationToken);

    /// <summary>Use this method to change the bot's description, which is shown in the chat with the bot if the chat is empty.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="description">New bot description; 0-512 characters. Pass an empty string to remove the dedicated description for the given language.</param>
    /// <param name="languageCode">A two-letter ISO 639-1 language code. If empty, the description will be applied to all users for whose language there is no dedicated description.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetMyDescription(
        this ITelegramBotClient botClient,
        string? description = default,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetMyDescriptionRequest
    {
        Description = description,
        LanguageCode = languageCode,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetMyDescription">SetMyDescription</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetMyDescription instead")]
    public static Task SetMyDescriptionAsync(
        this ITelegramBotClient botClient,
        string? description = default,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) => botClient.SetMyDescription(description, languageCode, cancellationToken);

    /// <summary>Use this method to get the current bot description for the given user language.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="languageCode">A two-letter ISO 639-1 language code or an empty string</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns><see cref="BotDescription"/> on success.</returns>
    public static async Task<BotDescription> GetMyDescription(
        this ITelegramBotClient botClient,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetMyDescriptionRequest
    {
        LanguageCode = languageCode,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetMyDescription">GetMyDescription</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetMyDescription instead")]
    public static Task<BotDescription> GetMyDescriptionAsync(
        this ITelegramBotClient botClient,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) => botClient.GetMyDescription(languageCode, cancellationToken);

    /// <summary>Use this method to change the bot's short description, which is shown on the bot's profile page and is sent together with the link when users share the bot.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="shortDescription">New short description for the bot; 0-120 characters. Pass an empty string to remove the dedicated short description for the given language.</param>
    /// <param name="languageCode">A two-letter ISO 639-1 language code. If empty, the short description will be applied to all users for whose language there is no dedicated short description.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetMyShortDescription(
        this ITelegramBotClient botClient,
        string? shortDescription = default,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetMyShortDescriptionRequest
    {
        ShortDescription = shortDescription,
        LanguageCode = languageCode,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetMyShortDescription">SetMyShortDescription</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetMyShortDescription instead")]
    public static Task SetMyShortDescriptionAsync(
        this ITelegramBotClient botClient,
        string? shortDescription = default,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) => botClient.SetMyShortDescription(shortDescription, languageCode, cancellationToken);

    /// <summary>Use this method to get the current bot short description for the given user language.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="languageCode">A two-letter ISO 639-1 language code or an empty string</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns><see cref="BotShortDescription"/> on success.</returns>
    public static async Task<BotShortDescription> GetMyShortDescription(
        this ITelegramBotClient botClient,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetMyShortDescriptionRequest
    {
        LanguageCode = languageCode,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetMyShortDescription">GetMyShortDescription</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetMyShortDescription instead")]
    public static Task<BotShortDescription> GetMyShortDescriptionAsync(
        this ITelegramBotClient botClient,
        string? languageCode = default,
        CancellationToken cancellationToken = default
    ) => botClient.GetMyShortDescription(languageCode, cancellationToken);

    /// <summary>Use this method to change the bot's menu button in a private chat, or the default menu button.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target private chat. If not specified, default bot's menu button will be changed</param>
    /// <param name="menuButton">An object for the bot's new menu button. Defaults to <see cref="MenuButtonDefault"/></param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetChatMenuButton(
        this ITelegramBotClient botClient,
        long? chatId = default,
        MenuButton? menuButton = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetChatMenuButtonRequest
    {
        ChatId = chatId,
        MenuButton = menuButton,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetChatMenuButton">SetChatMenuButton</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetChatMenuButton instead")]
    public static Task SetChatMenuButtonAsync(
        this ITelegramBotClient botClient,
        long? chatId = default,
        MenuButton? menuButton = default,
        CancellationToken cancellationToken = default
    ) => botClient.SetChatMenuButton(chatId, menuButton, cancellationToken);

    /// <summary>Use this method to get the current value of the bot's menu button in a private chat, or the default menu button.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target private chat. If not specified, default bot's menu button will be returned</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns><see cref="MenuButton"/> on success.</returns>
    public static async Task<MenuButton> GetChatMenuButton(
        this ITelegramBotClient botClient,
        long? chatId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetChatMenuButtonRequest
    {
        ChatId = chatId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetChatMenuButton">GetChatMenuButton</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetChatMenuButton instead")]
    public static Task<MenuButton> GetChatMenuButtonAsync(
        this ITelegramBotClient botClient,
        long? chatId = default,
        CancellationToken cancellationToken = default
    ) => botClient.GetChatMenuButton(chatId, cancellationToken);

    /// <summary>Use this method to change the default administrator rights requested by the bot when it's added as an administrator to groups or channels. These rights will be suggested to users, but they are free to modify the list before adding the bot.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="rights">An object describing new default administrator rights. If not specified, the default administrator rights will be cleared.</param>
    /// <param name="forChannels">Pass <see langword="true"/> to change the default administrator rights of the bot in channels. Otherwise, the default administrator rights of the bot for groups and supergroups will be changed.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetMyDefaultAdministratorRights(
        this ITelegramBotClient botClient,
        ChatAdministratorRights? rights = default,
        bool forChannels = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetMyDefaultAdministratorRightsRequest
    {
        Rights = rights,
        ForChannels = forChannels,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetMyDefaultAdministratorRights">SetMyDefaultAdministratorRights</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetMyDefaultAdministratorRights instead")]
    public static Task SetMyDefaultAdministratorRightsAsync(
        this ITelegramBotClient botClient,
        ChatAdministratorRights? rights = default,
        bool forChannels = default,
        CancellationToken cancellationToken = default
    ) => botClient.SetMyDefaultAdministratorRights(rights, forChannels, cancellationToken);

    /// <summary>Use this method to get the current default administrator rights of the bot.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="forChannels">Pass <see langword="true"/> to get default administrator rights of the bot in channels. Otherwise, default administrator rights of the bot for groups and supergroups will be returned.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns><see cref="ChatAdministratorRights"/> on success.</returns>
    public static async Task<ChatAdministratorRights> GetMyDefaultAdministratorRights(
        this ITelegramBotClient botClient,
        bool forChannels = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetMyDefaultAdministratorRightsRequest
    {
        ForChannels = forChannels,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetMyDefaultAdministratorRights">GetMyDefaultAdministratorRights</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetMyDefaultAdministratorRights instead")]
    public static Task<ChatAdministratorRights> GetMyDefaultAdministratorRightsAsync(
        this ITelegramBotClient botClient,
        bool forChannels = default,
        CancellationToken cancellationToken = default
    ) => botClient.GetMyDefaultAdministratorRights(forChannels, cancellationToken);

    #endregion Available methods

    #region Updating messages

    /// <summary>Use this method to edit text and <a href="https://core.telegram.org/bots/api#games">game</a> messages.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Identifier of the message to edit</param>
    /// <param name="text">New text of the message, 1-4096 characters after entities parsing</param>
    /// <param name="parseMode">Mode for parsing entities in the message text. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</param>
    /// <param name="entities">A list of special entities that appear in message text, which can be specified instead of <paramref name="parseMode"/></param>
    /// <param name="linkPreviewOptions">Link preview generation options for the message</param>
    /// <param name="replyMarkup">An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message to be edited was sent</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The edited <see cref="Message"/> is returned</returns>
    public static async Task<Message> EditMessageText(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        string text,
        ParseMode parseMode = default,
        IEnumerable<MessageEntity>? entities = default,
        LinkPreviewOptions? linkPreviewOptions = default,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new EditMessageTextRequest
    {
        ChatId = chatId,
        MessageId = messageId,
        Text = text,
        ParseMode = parseMode,
        Entities = entities,
        LinkPreviewOptions = linkPreviewOptions,
        ReplyMarkup = replyMarkup,
        BusinessConnectionId = businessConnectionId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="EditMessageText">EditMessageText</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use EditMessageText instead")]
    public static Task<Message> EditMessageTextAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        string text,
        ParseMode parseMode = default,
        IEnumerable<MessageEntity>? entities = default,
        LinkPreviewOptions? linkPreviewOptions = default,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.EditMessageText(chatId, messageId, text, parseMode, entities, linkPreviewOptions, replyMarkup, businessConnectionId, cancellationToken);

    /// <summary>Use this method to edit text and <a href="https://core.telegram.org/bots/api#games">game</a> messages.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="text">New text of the message, 1-4096 characters after entities parsing</param>
    /// <param name="parseMode">Mode for parsing entities in the message text. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</param>
    /// <param name="entities">A list of special entities that appear in message text, which can be specified instead of <paramref name="parseMode"/></param>
    /// <param name="linkPreviewOptions">Link preview generation options for the message</param>
    /// <param name="replyMarkup">An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message to be edited was sent</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task EditMessageText(
        this ITelegramBotClient botClient,
        string inlineMessageId,
        string text,
        ParseMode parseMode = default,
        IEnumerable<MessageEntity>? entities = default,
        LinkPreviewOptions? linkPreviewOptions = default,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new EditInlineMessageTextRequest
    {
        InlineMessageId = inlineMessageId,
        Text = text,
        ParseMode = parseMode,
        Entities = entities,
        LinkPreviewOptions = linkPreviewOptions,
        ReplyMarkup = replyMarkup,
        BusinessConnectionId = businessConnectionId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="EditMessageText">EditMessageText</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use EditMessageText instead")]
    public static Task EditMessageTextAsync(
        this ITelegramBotClient botClient,
        string inlineMessageId,
        string text,
        ParseMode parseMode = default,
        IEnumerable<MessageEntity>? entities = default,
        LinkPreviewOptions? linkPreviewOptions = default,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.EditMessageText(inlineMessageId, text, parseMode, entities, linkPreviewOptions, replyMarkup, businessConnectionId, cancellationToken);

    /// <summary>Use this method to edit captions of messages.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Identifier of the message to edit</param>
    /// <param name="caption">New caption of the message, 0-1024 characters after entities parsing</param>
    /// <param name="parseMode">Mode for parsing entities in the message caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</param>
    /// <param name="captionEntities">A list of special entities that appear in the caption, which can be specified instead of <paramref name="parseMode"/></param>
    /// <param name="showCaptionAboveMedia">Pass <see langword="true"/>, if the caption must be shown above the message media. Supported only for animation, photo and video messages.</param>
    /// <param name="replyMarkup">An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message to be edited was sent</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The edited <see cref="Message"/> is returned</returns>
    public static async Task<Message> EditMessageCaption(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        string? caption,
        ParseMode parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool showCaptionAboveMedia = default,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new EditMessageCaptionRequest
    {
        ChatId = chatId,
        MessageId = messageId,
        Caption = caption,
        ParseMode = parseMode,
        CaptionEntities = captionEntities,
        ShowCaptionAboveMedia = showCaptionAboveMedia,
        ReplyMarkup = replyMarkup,
        BusinessConnectionId = businessConnectionId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="EditMessageCaption">EditMessageCaption</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use EditMessageCaption instead")]
    public static Task<Message> EditMessageCaptionAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        string? caption,
        ParseMode parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool showCaptionAboveMedia = default,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.EditMessageCaption(chatId, messageId, caption, parseMode, captionEntities, showCaptionAboveMedia, replyMarkup, businessConnectionId, cancellationToken);

    /// <summary>Use this method to edit captions of messages.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="caption">New caption of the message, 0-1024 characters after entities parsing</param>
    /// <param name="parseMode">Mode for parsing entities in the message caption. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details.</param>
    /// <param name="captionEntities">A list of special entities that appear in the caption, which can be specified instead of <paramref name="parseMode"/></param>
    /// <param name="showCaptionAboveMedia">Pass <see langword="true"/>, if the caption must be shown above the message media. Supported only for animation, photo and video messages.</param>
    /// <param name="replyMarkup">An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message to be edited was sent</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task EditMessageCaption(
        this ITelegramBotClient botClient,
        string inlineMessageId,
        string? caption,
        ParseMode parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool showCaptionAboveMedia = default,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new EditInlineMessageCaptionRequest
    {
        InlineMessageId = inlineMessageId,
        Caption = caption,
        ParseMode = parseMode,
        CaptionEntities = captionEntities,
        ShowCaptionAboveMedia = showCaptionAboveMedia,
        ReplyMarkup = replyMarkup,
        BusinessConnectionId = businessConnectionId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="EditMessageCaption">EditMessageCaption</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use EditMessageCaption instead")]
    public static Task EditMessageCaptionAsync(
        this ITelegramBotClient botClient,
        string inlineMessageId,
        string? caption,
        ParseMode parseMode = default,
        IEnumerable<MessageEntity>? captionEntities = default,
        bool showCaptionAboveMedia = default,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.EditMessageCaption(inlineMessageId, caption, parseMode, captionEntities, showCaptionAboveMedia, replyMarkup, businessConnectionId, cancellationToken);

    /// <summary>Use this method to edit animation, audio, document, photo, or video messages, or to add media to text messages. If a message is part of a message album, then it can be edited only to an audio for audio albums, only to a document for document albums and to a photo or a video otherwise. When an inline message is edited, a new file can't be uploaded; use a previously uploaded file via its FileId or specify a URL.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Identifier of the message to edit</param>
    /// <param name="media">An object for a new media content of the message</param>
    /// <param name="replyMarkup">An object for a new <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message to be edited was sent</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The edited <see cref="Message"/> is returned</returns>
    public static async Task<Message> EditMessageMedia(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        InputMedia media,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new EditMessageMediaRequest
    {
        ChatId = chatId,
        MessageId = messageId,
        Media = media,
        ReplyMarkup = replyMarkup,
        BusinessConnectionId = businessConnectionId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="EditMessageMedia">EditMessageMedia</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use EditMessageMedia instead")]
    public static Task<Message> EditMessageMediaAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        InputMedia media,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.EditMessageMedia(chatId, messageId, media, replyMarkup, businessConnectionId, cancellationToken);

    /// <summary>Use this method to edit animation, audio, document, photo, or video messages, or to add media to text messages. If a message is part of a message album, then it can be edited only to an audio for audio albums, only to a document for document albums and to a photo or a video otherwise. When an inline message is edited, a new file can't be uploaded; use a previously uploaded file via its FileId or specify a URL.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="media">An object for a new media content of the message</param>
    /// <param name="replyMarkup">An object for a new <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message to be edited was sent</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task EditMessageMedia(
        this ITelegramBotClient botClient,
        string inlineMessageId,
        InputMedia media,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new EditInlineMessageMediaRequest
    {
        InlineMessageId = inlineMessageId,
        Media = media,
        ReplyMarkup = replyMarkup,
        BusinessConnectionId = businessConnectionId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="EditMessageMedia">EditMessageMedia</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use EditMessageMedia instead")]
    public static Task EditMessageMediaAsync(
        this ITelegramBotClient botClient,
        string inlineMessageId,
        InputMedia media,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.EditMessageMedia(inlineMessageId, media, replyMarkup, businessConnectionId, cancellationToken);

    /// <summary>Use this method to edit live location messages. A location can be edited until its <paramref name="livePeriod"/> expires or editing is explicitly disabled by a call to <see cref="TelegramBotClientExtensions.StopMessageLiveLocation">StopMessageLiveLocation</see>.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Identifier of the message to edit</param>
    /// <param name="latitude">Latitude of new location</param>
    /// <param name="longitude">Longitude of new location</param>
    /// <param name="livePeriod">New period in seconds during which the location can be updated, starting from the message send date. If 0x7FFFFFFF is specified, then the location can be updated forever. Otherwise, the new value must not exceed the current <paramref name="livePeriod"/> by more than a day, and the live location expiration date must remain within the next 90 days. If not specified, then <paramref name="livePeriod"/> remains unchanged</param>
    /// <param name="horizontalAccuracy">The radius of uncertainty for the location, measured in meters; 0-1500</param>
    /// <param name="heading">Direction in which the user is moving, in degrees. Must be between 1 and 360 if specified.</param>
    /// <param name="proximityAlertRadius">The maximum distance for proximity alerts about approaching another chat member, in meters. Must be between 1 and 100000 if specified.</param>
    /// <param name="replyMarkup">An object for a new <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message to be edited was sent</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The edited <see cref="Message"/> is returned</returns>
    public static async Task<Message> EditMessageLiveLocation(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        double latitude,
        double longitude,
        int? livePeriod = default,
        double? horizontalAccuracy = default,
        int? heading = default,
        int? proximityAlertRadius = default,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new EditMessageLiveLocationRequest
    {
        ChatId = chatId,
        MessageId = messageId,
        Latitude = latitude,
        Longitude = longitude,
        LivePeriod = livePeriod,
        HorizontalAccuracy = horizontalAccuracy,
        Heading = heading,
        ProximityAlertRadius = proximityAlertRadius,
        ReplyMarkup = replyMarkup,
        BusinessConnectionId = businessConnectionId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="EditMessageLiveLocation">EditMessageLiveLocation</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use EditMessageLiveLocation instead")]
    public static Task<Message> EditMessageLiveLocationAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        double latitude,
        double longitude,
        int? livePeriod = default,
        double? horizontalAccuracy = default,
        int? heading = default,
        int? proximityAlertRadius = default,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.EditMessageLiveLocation(chatId, messageId, latitude, longitude, livePeriod, horizontalAccuracy, heading, proximityAlertRadius, replyMarkup, businessConnectionId, cancellationToken);

    /// <summary>Use this method to edit live location messages. A location can be edited until its <paramref name="livePeriod"/> expires or editing is explicitly disabled by a call to <see cref="TelegramBotClientExtensions.StopMessageLiveLocation">StopMessageLiveLocation</see>.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="latitude">Latitude of new location</param>
    /// <param name="longitude">Longitude of new location</param>
    /// <param name="livePeriod">New period in seconds during which the location can be updated, starting from the message send date. If 0x7FFFFFFF is specified, then the location can be updated forever. Otherwise, the new value must not exceed the current <paramref name="livePeriod"/> by more than a day, and the live location expiration date must remain within the next 90 days. If not specified, then <paramref name="livePeriod"/> remains unchanged</param>
    /// <param name="horizontalAccuracy">The radius of uncertainty for the location, measured in meters; 0-1500</param>
    /// <param name="heading">Direction in which the user is moving, in degrees. Must be between 1 and 360 if specified.</param>
    /// <param name="proximityAlertRadius">The maximum distance for proximity alerts about approaching another chat member, in meters. Must be between 1 and 100000 if specified.</param>
    /// <param name="replyMarkup">An object for a new <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message to be edited was sent</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task EditMessageLiveLocation(
        this ITelegramBotClient botClient,
        string inlineMessageId,
        double latitude,
        double longitude,
        int? livePeriod = default,
        double? horizontalAccuracy = default,
        int? heading = default,
        int? proximityAlertRadius = default,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new EditInlineMessageLiveLocationRequest
    {
        InlineMessageId = inlineMessageId,
        Latitude = latitude,
        Longitude = longitude,
        LivePeriod = livePeriod,
        HorizontalAccuracy = horizontalAccuracy,
        Heading = heading,
        ProximityAlertRadius = proximityAlertRadius,
        ReplyMarkup = replyMarkup,
        BusinessConnectionId = businessConnectionId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="EditMessageLiveLocation">EditMessageLiveLocation</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use EditMessageLiveLocation instead")]
    public static Task EditMessageLiveLocationAsync(
        this ITelegramBotClient botClient,
        string inlineMessageId,
        double latitude,
        double longitude,
        int? livePeriod = default,
        double? horizontalAccuracy = default,
        int? heading = default,
        int? proximityAlertRadius = default,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.EditMessageLiveLocation(inlineMessageId, latitude, longitude, livePeriod, horizontalAccuracy, heading, proximityAlertRadius, replyMarkup, businessConnectionId, cancellationToken);

    /// <summary>Use this method to stop updating a live location message before <em>LivePeriod</em> expires.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Identifier of the message with live location to stop</param>
    /// <param name="replyMarkup">An object for a new <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message to be edited was sent</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The edited <see cref="Message"/> is returned</returns>
    public static async Task<Message> StopMessageLiveLocation(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new StopMessageLiveLocationRequest
    {
        ChatId = chatId,
        MessageId = messageId,
        ReplyMarkup = replyMarkup,
        BusinessConnectionId = businessConnectionId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="StopMessageLiveLocation">StopMessageLiveLocation</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use StopMessageLiveLocation instead")]
    public static Task<Message> StopMessageLiveLocationAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.StopMessageLiveLocation(chatId, messageId, replyMarkup, businessConnectionId, cancellationToken);

    /// <summary>Use this method to stop updating a live location message before <em>LivePeriod</em> expires.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="replyMarkup">An object for a new <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message to be edited was sent</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task StopMessageLiveLocation(
        this ITelegramBotClient botClient,
        string inlineMessageId,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new StopInlineMessageLiveLocationRequest
    {
        InlineMessageId = inlineMessageId,
        ReplyMarkup = replyMarkup,
        BusinessConnectionId = businessConnectionId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="StopMessageLiveLocation">StopMessageLiveLocation</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use StopMessageLiveLocation instead")]
    public static Task StopMessageLiveLocationAsync(
        this ITelegramBotClient botClient,
        string inlineMessageId,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.StopMessageLiveLocation(inlineMessageId, replyMarkup, businessConnectionId, cancellationToken);

    /// <summary>Use this method to edit only the reply markup of messages.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Identifier of the message to edit</param>
    /// <param name="replyMarkup">An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message to be edited was sent</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The edited <see cref="Message"/> is returned</returns>
    public static async Task<Message> EditMessageReplyMarkup(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new EditMessageReplyMarkupRequest
    {
        ChatId = chatId,
        MessageId = messageId,
        ReplyMarkup = replyMarkup,
        BusinessConnectionId = businessConnectionId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="EditMessageReplyMarkup">EditMessageReplyMarkup</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use EditMessageReplyMarkup instead")]
    public static Task<Message> EditMessageReplyMarkupAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.EditMessageReplyMarkup(chatId, messageId, replyMarkup, businessConnectionId, cancellationToken);

    /// <summary>Use this method to edit only the reply markup of messages.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="replyMarkup">An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message to be edited was sent</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task EditMessageReplyMarkup(
        this ITelegramBotClient botClient,
        string inlineMessageId,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new EditInlineMessageReplyMarkupRequest
    {
        InlineMessageId = inlineMessageId,
        ReplyMarkup = replyMarkup,
        BusinessConnectionId = businessConnectionId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="EditMessageReplyMarkup">EditMessageReplyMarkup</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use EditMessageReplyMarkup instead")]
    public static Task EditMessageReplyMarkupAsync(
        this ITelegramBotClient botClient,
        string inlineMessageId,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.EditMessageReplyMarkup(inlineMessageId, replyMarkup, businessConnectionId, cancellationToken);

    /// <summary>Use this method to stop a poll which was sent by the bot.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Identifier of the original message with the poll</param>
    /// <param name="replyMarkup">An object for a new message <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>.</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message to be edited was sent</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The stopped <see cref="Poll"/> is returned.</returns>
    public static async Task<Poll> StopPoll(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new StopPollRequest
    {
        ChatId = chatId,
        MessageId = messageId,
        ReplyMarkup = replyMarkup,
        BusinessConnectionId = businessConnectionId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="StopPoll">StopPoll</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use StopPoll instead")]
    public static Task<Poll> StopPollAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.StopPoll(chatId, messageId, replyMarkup, businessConnectionId, cancellationToken);

    /// <summary>Use this method to delete a message, including service messages, with the following limitations:<br/>- A message can only be deleted if it was sent less than 48 hours ago.<br/>- Service messages about a supergroup, channel, or forum topic creation can't be deleted.<br/>- A dice message in a private chat can only be deleted if it was sent more than 24 hours ago.<br/>- Bots can delete outgoing messages in private chats, groups, and supergroups.<br/>- Bots can delete incoming messages in private chats.<br/>- Bots granted <em>CanPostMessages</em> permissions can delete outgoing messages in channels.<br/>- If the bot is an administrator of a group, it can delete any message there.<br/>- If the bot has <em>CanDeleteMessages</em> permission in a supergroup or a channel, it can delete any message there.<br/>Returns <em>True</em> on success.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageId">Identifier of the message to delete</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task DeleteMessage(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new DeleteMessageRequest
    {
        ChatId = chatId,
        MessageId = messageId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="DeleteMessage">DeleteMessage</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use DeleteMessage instead")]
    public static Task DeleteMessageAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        int messageId,
        CancellationToken cancellationToken = default
    ) => botClient.DeleteMessage(chatId, messageId, cancellationToken);

    /// <summary>Use this method to delete multiple messages simultaneously. If some of the specified messages can't be found, they are skipped.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="messageIds">A list of 1-100 identifiers of messages to delete. See <see cref="TelegramBotClientExtensions.DeleteMessage">DeleteMessage</see> for limitations on which messages can be deleted</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task DeleteMessages(
        this ITelegramBotClient botClient,
        ChatId chatId,
        IEnumerable<int> messageIds,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new DeleteMessagesRequest
    {
        ChatId = chatId,
        MessageIds = messageIds,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="DeleteMessages">DeleteMessages</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use DeleteMessages instead")]
    public static Task DeleteMessagesAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        IEnumerable<int> messageIds,
        CancellationToken cancellationToken = default
    ) => botClient.DeleteMessages(chatId, messageIds, cancellationToken);

    #endregion Updating messages

    #region Stickers

    /// <summary>Use this method to send static .WEBP, <a href="https://telegram.org/blog/animated-stickers">animated</a> .TGS, or <a href="https://telegram.org/blog/video-stickers-better-reactions">video</a> .WEBM stickers.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="sticker">Sticker to send. Pass a FileId as String to send a file that exists on the Telegram servers (recommended), pass an HTTP URL as a String for Telegram to get a .WEBP sticker from the Internet, or upload a new .WEBP, .TGS, or .WEBM sticker using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a>. Video and animated stickers can't be sent via an HTTP URL.</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">Additional interface options. An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>, <a href="https://core.telegram.org/bots/features#keyboards">custom reply keyboard</a>, instructions to remove a reply keyboard or to force a reply from the user</param>
    /// <param name="emoji">Emoji associated with the sticker; only for just uploaded stickers</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent message from forwarding and saving</param>
    /// <param name="messageEffectId">Unique identifier of the message effect to be added to the message; for private chats only</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be sent</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendSticker(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile sticker,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? emoji = default,
        int? messageThreadId = default,
        bool disableNotification = default,
        bool protectContent = default,
        string? messageEffectId = default,
        string? businessConnectionId = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendStickerRequest
    {
        ChatId = chatId,
        Sticker = sticker,
        ReplyParameters = replyParameters,
        ReplyMarkup = replyMarkup,
        Emoji = emoji,
        MessageThreadId = messageThreadId,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        MessageEffectId = messageEffectId,
        BusinessConnectionId = businessConnectionId,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendSticker">SendSticker</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendSticker instead")]
    public static Task<Message> SendStickerAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        InputFile sticker,
        int? messageThreadId = default,
        string? emoji = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        string? messageEffectId = default,
        ReplyParameters? replyParameters = default,
        IReplyMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendSticker(chatId, sticker, replyParameters, replyMarkup, emoji, messageThreadId, disableNotification, protectContent, messageEffectId, businessConnectionId, allowPaidBroadcast, cancellationToken);

    /// <summary>Use this method to get a sticker set.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="name">Name of the sticker set</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>A <see cref="StickerSet"/> object is returned.</returns>
    public static async Task<StickerSet> GetStickerSet(
        this ITelegramBotClient botClient,
        string name,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetStickerSetRequest
    {
        Name = name,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetStickerSet">GetStickerSet</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetStickerSet instead")]
    public static Task<StickerSet> GetStickerSetAsync(
        this ITelegramBotClient botClient,
        string name,
        CancellationToken cancellationToken = default
    ) => botClient.GetStickerSet(name, cancellationToken);

    /// <summary>Use this method to get information about custom emoji stickers by their identifiers.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="customEmojiIds">A list of custom emoji identifiers. At most 200 custom emoji identifiers can be specified.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>An Array of <see cref="Sticker"/> objects.</returns>
    public static async Task<Sticker[]> GetCustomEmojiStickers(
        this ITelegramBotClient botClient,
        IEnumerable<string> customEmojiIds,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetCustomEmojiStickersRequest
    {
        CustomEmojiIds = customEmojiIds,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetCustomEmojiStickers">GetCustomEmojiStickers</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetCustomEmojiStickers instead")]
    public static Task<Sticker[]> GetCustomEmojiStickersAsync(
        this ITelegramBotClient botClient,
        IEnumerable<string> customEmojiIds,
        CancellationToken cancellationToken = default
    ) => botClient.GetCustomEmojiStickers(customEmojiIds, cancellationToken);

    /// <summary>Use this method to upload a file with a sticker for later use in the <see cref="TelegramBotClientExtensions.CreateNewStickerSet">CreateNewStickerSet</see>, <see cref="TelegramBotClientExtensions.AddStickerToSet">AddStickerToSet</see>, or <see cref="TelegramBotClientExtensions.ReplaceStickerInSet">ReplaceStickerInSet</see> methods (the file can be used multiple times).</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">User identifier of sticker file owner</param>
    /// <param name="sticker">A file with the sticker in .WEBP, .PNG, .TGS, or .WEBM format. See <a href="https://core.telegram.org/stickers">https://core.telegram.org/stickers</a> for technical requirements. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a></param>
    /// <param name="stickerFormat">Format of the sticker, must be one of <see cref="StickerFormat.Static">Static</see>, <see cref="StickerFormat.Animated">Animated</see>, <see cref="StickerFormat.Video">Video</see></param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The uploaded <see cref="TGFile"/> on success.</returns>
    public static async Task<TGFile> UploadStickerFile(
        this ITelegramBotClient botClient,
        long userId,
        InputFileStream sticker,
        StickerFormat stickerFormat,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new UploadStickerFileRequest
    {
        UserId = userId,
        Sticker = sticker,
        StickerFormat = stickerFormat,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="UploadStickerFile">UploadStickerFile</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use UploadStickerFile instead")]
    public static Task<TGFile> UploadStickerFileAsync(
        this ITelegramBotClient botClient,
        long userId,
        InputFileStream sticker,
        StickerFormat stickerFormat,
        CancellationToken cancellationToken = default
    ) => botClient.UploadStickerFile(userId, sticker, stickerFormat, cancellationToken);

    /// <summary>Use this method to create a new sticker set owned by a user. The bot will be able to edit the sticker set thus created.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">User identifier of created sticker set owner</param>
    /// <param name="name">Short name of sticker set, to be used in <c>t.me/addstickers/</c> URLs (e.g., <em>animals</em>). Can contain only English letters, digits and underscores. Must begin with a letter, can't contain consecutive underscores and must end in <c>"_by_&lt;BotUsername&gt;"</c>. <c>&lt;BotUsername&gt;</c> is case insensitive. 1-64 characters.</param>
    /// <param name="title">Sticker set title, 1-64 characters</param>
    /// <param name="stickers">A list of 1-50 initial stickers to be added to the sticker set</param>
    /// <param name="stickerType">Type of stickers in the set, pass <see cref="StickerType.Regular">Regular</see>, <see cref="StickerType.Mask">Mask</see>, or <see cref="StickerType.CustomEmoji">CustomEmoji</see>. By default, a regular sticker set is created.</param>
    /// <param name="needsRepainting">Pass <see langword="true"/> if stickers in the sticker set must be repainted to the color of text when used in messages, the accent color if used as emoji status, white on chat photos, or another appropriate color based on context; for custom emoji sticker sets only</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task CreateNewStickerSet(
        this ITelegramBotClient botClient,
        long userId,
        string name,
        string title,
        IEnumerable<InputSticker> stickers,
        StickerType? stickerType = default,
        bool needsRepainting = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new CreateNewStickerSetRequest
    {
        UserId = userId,
        Name = name,
        Title = title,
        Stickers = stickers,
        StickerType = stickerType,
        NeedsRepainting = needsRepainting,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="CreateNewStickerSet">CreateNewStickerSet</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use CreateNewStickerSet instead")]
    public static Task CreateNewStickerSetAsync(
        this ITelegramBotClient botClient,
        long userId,
        string name,
        string title,
        IEnumerable<InputSticker> stickers,
        StickerType? stickerType = default,
        bool needsRepainting = default,
        CancellationToken cancellationToken = default
    ) => botClient.CreateNewStickerSet(userId, name, title, stickers, stickerType, needsRepainting, cancellationToken);

    /// <summary>Use this method to add a new sticker to a set created by the bot. Emoji sticker sets can have up to 200 stickers. Other sticker sets can have up to 120 stickers.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">User identifier of sticker set owner</param>
    /// <param name="name">Sticker set name</param>
    /// <param name="sticker">An object with information about the added sticker. If exactly the same sticker had already been added to the set, then the set isn't changed.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task AddStickerToSet(
        this ITelegramBotClient botClient,
        long userId,
        string name,
        InputSticker sticker,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new AddStickerToSetRequest
    {
        UserId = userId,
        Name = name,
        Sticker = sticker,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="AddStickerToSet">AddStickerToSet</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use AddStickerToSet instead")]
    public static Task AddStickerToSetAsync(
        this ITelegramBotClient botClient,
        long userId,
        string name,
        InputSticker sticker,
        CancellationToken cancellationToken = default
    ) => botClient.AddStickerToSet(userId, name, sticker, cancellationToken);

    /// <summary>Use this method to move a sticker in a set created by the bot to a specific position.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="sticker">File identifier of the sticker</param>
    /// <param name="position">New sticker position in the set, zero-based</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetStickerPositionInSet(
        this ITelegramBotClient botClient,
        InputFileId sticker,
        int position,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetStickerPositionInSetRequest
    {
        Sticker = sticker,
        Position = position,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetStickerPositionInSet">SetStickerPositionInSet</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetStickerPositionInSet instead")]
    public static Task SetStickerPositionInSetAsync(
        this ITelegramBotClient botClient,
        InputFileId sticker,
        int position,
        CancellationToken cancellationToken = default
    ) => botClient.SetStickerPositionInSet(sticker, position, cancellationToken);

    /// <summary>Use this method to delete a sticker from a set created by the bot.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="sticker">File identifier of the sticker</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task DeleteStickerFromSet(
        this ITelegramBotClient botClient,
        InputFileId sticker,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new DeleteStickerFromSetRequest
    {
        Sticker = sticker,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="DeleteStickerFromSet">DeleteStickerFromSet</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use DeleteStickerFromSet instead")]
    public static Task DeleteStickerFromSetAsync(
        this ITelegramBotClient botClient,
        InputFileId sticker,
        CancellationToken cancellationToken = default
    ) => botClient.DeleteStickerFromSet(sticker, cancellationToken);

    /// <summary>Use this method to replace an existing sticker in a sticker set with a new one. The method is equivalent to calling <see cref="TelegramBotClientExtensions.DeleteStickerFromSet">DeleteStickerFromSet</see>, then <see cref="TelegramBotClientExtensions.AddStickerToSet">AddStickerToSet</see>, then <see cref="TelegramBotClientExtensions.SetStickerPositionInSet">SetStickerPositionInSet</see>.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">User identifier of the sticker set owner</param>
    /// <param name="name">Sticker set name</param>
    /// <param name="oldSticker">File identifier of the replaced sticker</param>
    /// <param name="sticker">An object with information about the added sticker. If exactly the same sticker had already been added to the set, then the set remains unchanged.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task ReplaceStickerInSet(
        this ITelegramBotClient botClient,
        long userId,
        string name,
        string oldSticker,
        InputSticker sticker,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new ReplaceStickerInSetRequest
    {
        UserId = userId,
        Name = name,
        OldSticker = oldSticker,
        Sticker = sticker,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="ReplaceStickerInSet">ReplaceStickerInSet</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use ReplaceStickerInSet instead")]
    public static Task ReplaceStickerInSetAsync(
        this ITelegramBotClient botClient,
        long userId,
        string name,
        string oldSticker,
        InputSticker sticker,
        CancellationToken cancellationToken = default
    ) => botClient.ReplaceStickerInSet(userId, name, oldSticker, sticker, cancellationToken);

    /// <summary>Use this method to change the list of emoji assigned to a regular or custom emoji sticker. The sticker must belong to a sticker set created by the bot.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="sticker">File identifier of the sticker</param>
    /// <param name="emojiList">A list of 1-20 emoji associated with the sticker</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetStickerEmojiList(
        this ITelegramBotClient botClient,
        InputFileId sticker,
        IEnumerable<string> emojiList,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetStickerEmojiListRequest
    {
        Sticker = sticker,
        EmojiList = emojiList,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetStickerEmojiList">SetStickerEmojiList</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetStickerEmojiList instead")]
    public static Task SetStickerEmojiListAsync(
        this ITelegramBotClient botClient,
        InputFileId sticker,
        IEnumerable<string> emojiList,
        CancellationToken cancellationToken = default
    ) => botClient.SetStickerEmojiList(sticker, emojiList, cancellationToken);

    /// <summary>Use this method to change search keywords assigned to a regular or custom emoji sticker. The sticker must belong to a sticker set created by the bot.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="sticker">File identifier of the sticker</param>
    /// <param name="keywords">A list of 0-20 search keywords for the sticker with total length of up to 64 characters</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetStickerKeywords(
        this ITelegramBotClient botClient,
        InputFileId sticker,
        IEnumerable<string>? keywords = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetStickerKeywordsRequest
    {
        Sticker = sticker,
        Keywords = keywords,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetStickerKeywords">SetStickerKeywords</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetStickerKeywords instead")]
    public static Task SetStickerKeywordsAsync(
        this ITelegramBotClient botClient,
        InputFileId sticker,
        IEnumerable<string>? keywords = default,
        CancellationToken cancellationToken = default
    ) => botClient.SetStickerKeywords(sticker, keywords, cancellationToken);

    /// <summary>Use this method to change the <see cref="MaskPosition">mask position</see> of a mask sticker. The sticker must belong to a sticker set that was created by the bot.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="sticker">File identifier of the sticker</param>
    /// <param name="maskPosition">An object with the position where the mask should be placed on faces. Omit the parameter to remove the mask position.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetStickerMaskPosition(
        this ITelegramBotClient botClient,
        InputFileId sticker,
        MaskPosition? maskPosition = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetStickerMaskPositionRequest
    {
        Sticker = sticker,
        MaskPosition = maskPosition,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetStickerMaskPosition">SetStickerMaskPosition</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetStickerMaskPosition instead")]
    public static Task SetStickerMaskPositionAsync(
        this ITelegramBotClient botClient,
        InputFileId sticker,
        MaskPosition? maskPosition = default,
        CancellationToken cancellationToken = default
    ) => botClient.SetStickerMaskPosition(sticker, maskPosition, cancellationToken);

    /// <summary>Use this method to set the title of a created sticker set.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="name">Sticker set name</param>
    /// <param name="title">Sticker set title, 1-64 characters</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetStickerSetTitle(
        this ITelegramBotClient botClient,
        string name,
        string title,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetStickerSetTitleRequest
    {
        Name = name,
        Title = title,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetStickerSetTitle">SetStickerSetTitle</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetStickerSetTitle instead")]
    public static Task SetStickerSetTitleAsync(
        this ITelegramBotClient botClient,
        string name,
        string title,
        CancellationToken cancellationToken = default
    ) => botClient.SetStickerSetTitle(name, title, cancellationToken);

    /// <summary>Use this method to set the thumbnail of a regular or mask sticker set. The format of the thumbnail file must match the format of the stickers in the set.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="name">Sticker set name</param>
    /// <param name="userId">User identifier of the sticker set owner</param>
    /// <param name="format">Format of the thumbnail, must be one of <see cref="StickerFormat.Static">Static</see> for a <b>.WEBP</b> or <b>.PNG</b> image, <see cref="StickerFormat.Animated">Animated</see> for a <b>.TGS</b> animation, or <see cref="StickerFormat.Video">Video</see> for a <b>.WEBM</b> video</param>
    /// <param name="thumbnail">A <b>.WEBP</b> or <b>.PNG</b> image with the thumbnail, must be up to 128 kilobytes in size and have a width and height of exactly 100px, or a <b>.TGS</b> animation with a thumbnail up to 32 kilobytes in size (see <a href="https://core.telegram.org/stickers#animation-requirements">https://core.telegram.org/stickers#animation-requirements</a> for animated sticker technical requirements), or a <b>.WEBM</b> video with the thumbnail up to 32 kilobytes in size; see <a href="https://core.telegram.org/stickers#video-requirements">https://core.telegram.org/stickers#video-requirements</a> for video sticker technical requirements. Pass a <em>FileId</em> as a String to send a file that already exists on the Telegram servers, pass an HTTP URL as a String for Telegram to get a file from the Internet, or upload a new one using <see cref="InputFileStream"/>. <a href="https://core.telegram.org/bots/api#sending-files">More information on Sending Files »</a>. Animated and video sticker set thumbnails can't be uploaded via HTTP URL. If omitted, then the thumbnail is dropped and the first sticker is used as the thumbnail.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetStickerSetThumbnail(
        this ITelegramBotClient botClient,
        string name,
        long userId,
        StickerFormat format,
        InputFile? thumbnail = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetStickerSetThumbnailRequest
    {
        Name = name,
        UserId = userId,
        Format = format,
        Thumbnail = thumbnail,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetStickerSetThumbnail">SetStickerSetThumbnail</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetStickerSetThumbnail instead")]
    public static Task SetStickerSetThumbnailAsync(
        this ITelegramBotClient botClient,
        string name,
        long userId,
        StickerFormat format,
        InputFile? thumbnail = default,
        CancellationToken cancellationToken = default
    ) => botClient.SetStickerSetThumbnail(name, userId, format, thumbnail, cancellationToken);

    /// <summary>Use this method to set the thumbnail of a custom emoji sticker set.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="name">Sticker set name</param>
    /// <param name="customEmojiId">Custom emoji identifier of a sticker from the sticker set; pass an empty string to drop the thumbnail and use the first sticker as the thumbnail.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetCustomEmojiStickerSetThumbnail(
        this ITelegramBotClient botClient,
        string name,
        string? customEmojiId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetCustomEmojiStickerSetThumbnailRequest
    {
        Name = name,
        CustomEmojiId = customEmojiId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetCustomEmojiStickerSetThumbnail">SetCustomEmojiStickerSetThumbnail</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetCustomEmojiStickerSetThumbnail instead")]
    public static Task SetCustomEmojiStickerSetThumbnailAsync(
        this ITelegramBotClient botClient,
        string name,
        string? customEmojiId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SetCustomEmojiStickerSetThumbnail(name, customEmojiId, cancellationToken);

    /// <summary>Use this method to delete a sticker set that was created by the bot.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="name">Sticker set name</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task DeleteStickerSet(
        this ITelegramBotClient botClient,
        string name,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new DeleteStickerSetRequest
    {
        Name = name,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="DeleteStickerSet">DeleteStickerSet</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use DeleteStickerSet instead")]
    public static Task DeleteStickerSetAsync(
        this ITelegramBotClient botClient,
        string name,
        CancellationToken cancellationToken = default
    ) => botClient.DeleteStickerSet(name, cancellationToken);

    /// <summary>Returns the list of gifts that can be sent by the bot to users.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>A <see cref="GiftList"/> object.</returns>
    public static async Task<GiftList> GetAvailableGifts(
        this ITelegramBotClient botClient,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetAvailableGiftsRequest
    {
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetAvailableGifts">GetAvailableGifts</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetAvailableGifts instead")]
    public static Task<GiftList> GetAvailableGiftsAsync(
        this ITelegramBotClient botClient,
        CancellationToken cancellationToken = default
    ) => botClient.GetAvailableGifts(cancellationToken);

    /// <summary>Sends a gift to the given user. The gift can't be converted to Telegram Stars by the user.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">Unique identifier of the target user that will receive the gift</param>
    /// <param name="giftId">Identifier of the gift</param>
    /// <param name="text">Text that will be shown along with the gift; 0-255 characters</param>
    /// <param name="textParseMode">Mode for parsing entities in the text. See <a href="https://core.telegram.org/bots/api#formatting-options">formatting options</a> for more details. Entities other than <see cref="MessageEntityType.Bold">Bold</see>, <see cref="MessageEntityType.Italic">Italic</see>, <see cref="MessageEntityType.Underline">Underline</see>, <see cref="MessageEntityType.Strikethrough">Strikethrough</see>, <see cref="MessageEntityType.Spoiler">Spoiler</see>, and <see cref="MessageEntityType.CustomEmoji">CustomEmoji</see> are ignored.</param>
    /// <param name="textEntities">A list of special entities that appear in the gift text. It can be specified instead of <paramref name="textParseMode"/>. Entities other than <see cref="MessageEntityType.Bold">Bold</see>, <see cref="MessageEntityType.Italic">Italic</see>, <see cref="MessageEntityType.Underline">Underline</see>, <see cref="MessageEntityType.Strikethrough">Strikethrough</see>, <see cref="MessageEntityType.Spoiler">Spoiler</see>, and <see cref="MessageEntityType.CustomEmoji">CustomEmoji</see> are ignored.</param>
    /// <param name="payForUpgrade">Pass <see langword="true"/> to pay for the gift upgrade from the bot's balance, thereby making the upgrade free for the receiver</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SendGift(
        this ITelegramBotClient botClient,
        long userId,
        string giftId,
        string? text = default,
        ParseMode textParseMode = default,
        IEnumerable<MessageEntity>? textEntities = default,
        bool payForUpgrade = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendGiftRequest
    {
        UserId = userId,
        GiftId = giftId,
        Text = text,
        TextParseMode = textParseMode,
        TextEntities = textEntities,
        PayForUpgrade = payForUpgrade,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendGift">SendGift</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendGift instead")]
    public static Task SendGiftAsync(
        this ITelegramBotClient botClient,
        long userId,
        string giftId,
        bool payForUpgrade = default,
        string? text = default,
        ParseMode textParseMode = default,
        IEnumerable<MessageEntity>? textEntities = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendGift(userId, giftId, text, textParseMode, textEntities, payForUpgrade, cancellationToken);

    /// <summary>Verifies a user <a href="https://telegram.org/verify#third-party-verification">on behalf of the organization</a> which is represented by the bot.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="customDescription">Custom description for the verification; 0-70 characters. Must be empty if the organization isn't allowed to provide a custom verification description.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task VerifyUser(
        this ITelegramBotClient botClient,
        long userId,
        string? customDescription = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new VerifyUserRequest
    {
        UserId = userId,
        CustomDescription = customDescription,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="VerifyUser">VerifyUser</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use VerifyUser instead")]
    public static Task VerifyUserAsync(
        this ITelegramBotClient botClient,
        long userId,
        string? customDescription = default,
        CancellationToken cancellationToken = default
    ) => botClient.VerifyUser(userId, customDescription, cancellationToken);

    /// <summary>Verifies a chat <a href="https://telegram.org/verify#third-party-verification">on behalf of the organization</a> which is represented by the bot.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="customDescription">Custom description for the verification; 0-70 characters. Must be empty if the organization isn't allowed to provide a custom verification description.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task VerifyChat(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string? customDescription = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new VerifyChatRequest
    {
        ChatId = chatId,
        CustomDescription = customDescription,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="VerifyChat">VerifyChat</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use VerifyChat instead")]
    public static Task VerifyChatAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string? customDescription = default,
        CancellationToken cancellationToken = default
    ) => botClient.VerifyChat(chatId, customDescription, cancellationToken);

    /// <summary>Removes verification from a user who is currently verified <a href="https://telegram.org/verify#third-party-verification">on behalf of the organization</a> represented by the bot.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">Unique identifier of the target user</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task RemoveUserVerification(
        this ITelegramBotClient botClient,
        long userId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new RemoveUserVerificationRequest
    {
        UserId = userId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="RemoveUserVerification">RemoveUserVerification</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use RemoveUserVerification instead")]
    public static Task RemoveUserVerificationAsync(
        this ITelegramBotClient botClient,
        long userId,
        CancellationToken cancellationToken = default
    ) => botClient.RemoveUserVerification(userId, cancellationToken);

    /// <summary>Removes verification from a chat that is currently verified <a href="https://telegram.org/verify#third-party-verification">on behalf of the organization</a> represented by the bot.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task RemoveChatVerification(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new RemoveChatVerificationRequest
    {
        ChatId = chatId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="RemoveChatVerification">RemoveChatVerification</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use RemoveChatVerification instead")]
    public static Task RemoveChatVerificationAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        CancellationToken cancellationToken = default
    ) => botClient.RemoveChatVerification(chatId, cancellationToken);

    #endregion

    #region Inline mode

    /// <summary>Use this method to send answers to an inline query<br/>No more than <b>50</b> results per query are allowed.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="inlineQueryId">Unique identifier for the answered query</param>
    /// <param name="results">A array of results for the inline query</param>
    /// <param name="cacheTime">The maximum amount of time in seconds that the result of the inline query may be cached on the server. Defaults to 300.</param>
    /// <param name="isPersonal">Pass <see langword="true"/> if results may be cached on the server side only for the user that sent the query. By default, results may be returned to any user who sends the same query.</param>
    /// <param name="nextOffset">Pass the offset that a client should send in the next query with the same text to receive more results. Pass an empty string if there are no more results or if you don't support pagination. Offset length can't exceed 64 bytes.</param>
    /// <param name="button">An object describing a button to be shown above inline query results</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task AnswerInlineQuery(
        this ITelegramBotClient botClient,
        string inlineQueryId,
        IEnumerable<InlineQueryResult> results,
        int? cacheTime = default,
        bool isPersonal = default,
        string? nextOffset = default,
        InlineQueryResultsButton? button = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new AnswerInlineQueryRequest
    {
        InlineQueryId = inlineQueryId,
        Results = results,
        CacheTime = cacheTime,
        IsPersonal = isPersonal,
        NextOffset = nextOffset,
        Button = button,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="AnswerInlineQuery">AnswerInlineQuery</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use AnswerInlineQuery instead")]
    public static Task AnswerInlineQueryAsync(
        this ITelegramBotClient botClient,
        string inlineQueryId,
        IEnumerable<InlineQueryResult> results,
        int? cacheTime = default,
        bool isPersonal = default,
        string? nextOffset = default,
        InlineQueryResultsButton? button = default,
        CancellationToken cancellationToken = default
    ) => botClient.AnswerInlineQuery(inlineQueryId, results, cacheTime, isPersonal, nextOffset, button, cancellationToken);

    /// <summary>Use this method to set the result of an interaction with a <a href="https://core.telegram.org/bots/webapps">Web App</a> and send a corresponding message on behalf of the user to the chat from which the query originated.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="webAppQueryId">Unique identifier for the query to be answered</param>
    /// <param name="result">An object describing the message to be sent</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>A <see cref="SentWebAppMessage"/> object is returned.</returns>
    public static async Task<SentWebAppMessage> AnswerWebAppQuery(
        this ITelegramBotClient botClient,
        string webAppQueryId,
        InlineQueryResult result,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new AnswerWebAppQueryRequest
    {
        WebAppQueryId = webAppQueryId,
        Result = result,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="AnswerWebAppQuery">AnswerWebAppQuery</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use AnswerWebAppQuery instead")]
    public static Task<SentWebAppMessage> AnswerWebAppQueryAsync(
        this ITelegramBotClient botClient,
        string webAppQueryId,
        InlineQueryResult result,
        CancellationToken cancellationToken = default
    ) => botClient.AnswerWebAppQuery(webAppQueryId, result, cancellationToken);

    /// <summary>Stores a message that can be sent by a user of a Mini App.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">Unique identifier of the target user that can use the prepared message</param>
    /// <param name="result">An object describing the message to be sent</param>
    /// <param name="allowUserChats">Pass <see langword="true"/> if the message can be sent to private chats with users</param>
    /// <param name="allowBotChats">Pass <see langword="true"/> if the message can be sent to private chats with bots</param>
    /// <param name="allowGroupChats">Pass <see langword="true"/> if the message can be sent to group and supergroup chats</param>
    /// <param name="allowChannelChats">Pass <see langword="true"/> if the message can be sent to channel chats</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>A <see cref="PreparedInlineMessage"/> object.</returns>
    public static async Task<PreparedInlineMessage> SavePreparedInlineMessage(
        this ITelegramBotClient botClient,
        long userId,
        InlineQueryResult result,
        bool allowUserChats = default,
        bool allowBotChats = default,
        bool allowGroupChats = default,
        bool allowChannelChats = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SavePreparedInlineMessageRequest
    {
        UserId = userId,
        Result = result,
        AllowUserChats = allowUserChats,
        AllowBotChats = allowBotChats,
        AllowGroupChats = allowGroupChats,
        AllowChannelChats = allowChannelChats,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SavePreparedInlineMessage">SavePreparedInlineMessage</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SavePreparedInlineMessage instead")]
    public static Task<PreparedInlineMessage> SavePreparedInlineMessageAsync(
        this ITelegramBotClient botClient,
        long userId,
        InlineQueryResult result,
        bool allowUserChats = default,
        bool allowBotChats = default,
        bool allowGroupChats = default,
        bool allowChannelChats = default,
        CancellationToken cancellationToken = default
    ) => botClient.SavePreparedInlineMessage(userId, result, allowUserChats, allowBotChats, allowGroupChats, allowChannelChats, cancellationToken);

    #endregion Inline mode

    #region Payments

    /// <summary>Use this method to send invoices.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
    /// <param name="title">Product name, 1-32 characters</param>
    /// <param name="description">Product description, 1-255 characters</param>
    /// <param name="payload">Bot-defined invoice payload, 1-128 bytes. This will not be displayed to the user, use it for your internal processes.</param>
    /// <param name="currency">Three-letter ISO 4217 currency code, see <a href="https://core.telegram.org/bots/payments#supported-currencies">more on currencies</a>. Pass “XTR” for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="prices">Price breakdown, a list of components (e.g. product price, tax, discount, delivery cost, delivery tax, bonus, etc.). Must contain exactly one item for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="providerToken">Payment provider token, obtained via <a href="https://t.me/botfather">@BotFather</a>. Pass an empty string for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="providerData">JSON-serialized data about the invoice, which will be shared with the payment provider. A detailed description of required fields should be provided by the payment provider.</param>
    /// <param name="maxTipAmount">The maximum accepted amount for tips in the <em>smallest units</em> of the currency (integer, <b>not</b> float/double). For example, for a maximum tip of <c>US$ 1.45</c> pass <c><paramref name="maxTipAmount"/> = 145</c>. See the <em>exp</em> parameter in <a href="https://core.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies). Defaults to 0. Not supported for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="suggestedTipAmounts">A array of suggested amounts of tips in the <em>smallest units</em> of the currency (integer, <b>not</b> float/double). At most 4 suggested tip amounts can be specified. The suggested tip amounts must be positive, passed in a strictly increased order and must not exceed <paramref name="maxTipAmount"/>.</param>
    /// <param name="photoUrl">URL of the product photo for the invoice. Can be a photo of the goods or a marketing image for a service. People like it better when they see what they are paying for.</param>
    /// <param name="photoSize">Photo size in bytes</param>
    /// <param name="photoWidth">Photo width</param>
    /// <param name="photoHeight">Photo height</param>
    /// <param name="needName">Pass <see langword="true"/> if you require the user's full name to complete the order. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="needPhoneNumber">Pass <see langword="true"/> if you require the user's phone number to complete the order. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="needEmail">Pass <see langword="true"/> if you require the user's email address to complete the order. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="needShippingAddress">Pass <see langword="true"/> if you require the user's shipping address to complete the order. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="sendPhoneNumberToProvider">Pass <see langword="true"/> if the user's phone number should be sent to the provider. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="sendEmailToProvider">Pass <see langword="true"/> if the user's email address should be sent to the provider. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="isFlexible">Pass <see langword="true"/> if the final price depends on the shipping method. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>. If empty, one 'Pay <c>total price</c>' button will be shown. If not empty, the first button must be a Pay button.</param>
    /// <param name="startParameter">Unique deep-linking parameter. If left empty, <b>forwarded copies</b> of the sent message will have a <em>Pay</em> button, allowing multiple users to pay directly from the forwarded message, using the same invoice. If non-empty, forwarded copies of the sent message will have a <em>URL</em> button with a deep link to the bot (instead of a <em>Pay</em> button), with the value used as the start parameter</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent message from forwarding and saving</param>
    /// <param name="messageEffectId">Unique identifier of the message effect to be added to the message; for private chats only</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendInvoice(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string title,
        string description,
        string payload,
        string currency,
        IEnumerable<LabeledPrice> prices,
        string? providerToken = default,
        string? providerData = default,
        int? maxTipAmount = default,
        IEnumerable<int>? suggestedTipAmounts = default,
        string? photoUrl = default,
        int? photoSize = default,
        int? photoWidth = default,
        int? photoHeight = default,
        bool needName = default,
        bool needPhoneNumber = default,
        bool needEmail = default,
        bool needShippingAddress = default,
        bool sendPhoneNumberToProvider = default,
        bool sendEmailToProvider = default,
        bool isFlexible = default,
        ReplyParameters? replyParameters = default,
        InlineKeyboardMarkup? replyMarkup = default,
        string? startParameter = default,
        int? messageThreadId = default,
        bool disableNotification = default,
        bool protectContent = default,
        string? messageEffectId = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendInvoiceRequest
    {
        ChatId = chatId,
        Title = title,
        Description = description,
        Payload = payload,
        Currency = currency,
        Prices = prices,
        ProviderToken = providerToken,
        ProviderData = providerData,
        MaxTipAmount = maxTipAmount,
        SuggestedTipAmounts = suggestedTipAmounts,
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
        ReplyParameters = replyParameters,
        ReplyMarkup = replyMarkup,
        StartParameter = startParameter,
        MessageThreadId = messageThreadId,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        MessageEffectId = messageEffectId,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendInvoice">SendInvoice</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendInvoice instead")]
    public static Task<Message> SendInvoiceAsync(
        this ITelegramBotClient botClient,
        ChatId chatId,
        string title,
        string description,
        string payload,
        string currency,
        IEnumerable<LabeledPrice> prices,
        int? messageThreadId = default,
        string? providerToken = default,
        int? maxTipAmount = default,
        IEnumerable<int>? suggestedTipAmounts = default,
        string? startParameter = default,
        string? providerData = default,
        string? photoUrl = default,
        int? photoSize = default,
        int? photoWidth = default,
        int? photoHeight = default,
        bool needName = default,
        bool needPhoneNumber = default,
        bool needEmail = default,
        bool needShippingAddress = default,
        bool sendPhoneNumberToProvider = default,
        bool sendEmailToProvider = default,
        bool isFlexible = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        string? messageEffectId = default,
        ReplyParameters? replyParameters = default,
        InlineKeyboardMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendInvoice(chatId, title, description, payload, currency, prices, providerToken, providerData, maxTipAmount, suggestedTipAmounts, photoUrl, photoSize, photoWidth, photoHeight, needName, needPhoneNumber, needEmail, needShippingAddress, sendPhoneNumberToProvider, sendEmailToProvider, isFlexible, replyParameters, replyMarkup, startParameter, messageThreadId, disableNotification, protectContent, messageEffectId, allowPaidBroadcast, cancellationToken);

    /// <summary>Use this method to create a link for an invoice.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="title">Product name, 1-32 characters</param>
    /// <param name="description">Product description, 1-255 characters</param>
    /// <param name="payload">Bot-defined invoice payload, 1-128 bytes. This will not be displayed to the user, use it for your internal processes.</param>
    /// <param name="currency">Three-letter ISO 4217 currency code, see <a href="https://core.telegram.org/bots/payments#supported-currencies">more on currencies</a>. Pass “XTR” for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="prices">Price breakdown, a list of components (e.g. product price, tax, discount, delivery cost, delivery tax, bonus, etc.). Must contain exactly one item for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="providerToken">Payment provider token, obtained via <a href="https://t.me/botfather">@BotFather</a>. Pass an empty string for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="providerData">JSON-serialized data about the invoice, which will be shared with the payment provider. A detailed description of required fields should be provided by the payment provider.</param>
    /// <param name="maxTipAmount">The maximum accepted amount for tips in the <em>smallest units</em> of the currency (integer, <b>not</b> float/double). For example, for a maximum tip of <c>US$ 1.45</c> pass <c><paramref name="maxTipAmount"/> = 145</c>. See the <em>exp</em> parameter in <a href="https://core.telegram.org/bots/payments/currencies.json">currencies.json</a>, it shows the number of digits past the decimal point for each currency (2 for the majority of currencies). Defaults to 0. Not supported for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="suggestedTipAmounts">A array of suggested amounts of tips in the <em>smallest units</em> of the currency (integer, <b>not</b> float/double). At most 4 suggested tip amounts can be specified. The suggested tip amounts must be positive, passed in a strictly increased order and must not exceed <paramref name="maxTipAmount"/>.</param>
    /// <param name="photoUrl">URL of the product photo for the invoice. Can be a photo of the goods or a marketing image for a service.</param>
    /// <param name="photoSize">Photo size in bytes</param>
    /// <param name="photoWidth">Photo width</param>
    /// <param name="photoHeight">Photo height</param>
    /// <param name="needName">Pass <see langword="true"/> if you require the user's full name to complete the order. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="needPhoneNumber">Pass <see langword="true"/> if you require the user's phone number to complete the order. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="needEmail">Pass <see langword="true"/> if you require the user's email address to complete the order. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="needShippingAddress">Pass <see langword="true"/> if you require the user's shipping address to complete the order. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="sendPhoneNumberToProvider">Pass <see langword="true"/> if the user's phone number should be sent to the provider. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="sendEmailToProvider">Pass <see langword="true"/> if the user's email address should be sent to the provider. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="isFlexible">Pass <see langword="true"/> if the final price depends on the shipping method. Ignored for payments in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</param>
    /// <param name="subscriptionPeriod">The number of seconds the subscription will be active for before the next payment. The currency must be set to “XTR” (Telegram Stars) if the parameter is used. Currently, it must always be 2592000 (30 days) if specified. Any number of subscriptions can be active for a given bot at the same time, including multiple concurrent subscriptions from the same user. Subscription price must no exceed 2500 Telegram Stars.</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the link will be created. For payments in <a href="https://t.me/BotNews/90">Telegram Stars</a> only.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The created invoice link as <em>String</em> on success.</returns>
    public static async Task<string> CreateInvoiceLink(
        this ITelegramBotClient botClient,
        string title,
        string description,
        string payload,
        string currency,
        IEnumerable<LabeledPrice> prices,
        string? providerToken = default,
        string? providerData = default,
        int? maxTipAmount = default,
        IEnumerable<int>? suggestedTipAmounts = default,
        string? photoUrl = default,
        int? photoSize = default,
        int? photoWidth = default,
        int? photoHeight = default,
        bool needName = default,
        bool needPhoneNumber = default,
        bool needEmail = default,
        bool needShippingAddress = default,
        bool sendPhoneNumberToProvider = default,
        bool sendEmailToProvider = default,
        bool isFlexible = default,
        int? subscriptionPeriod = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new CreateInvoiceLinkRequest
    {
        Title = title,
        Description = description,
        Payload = payload,
        Currency = currency,
        Prices = prices,
        ProviderToken = providerToken,
        ProviderData = providerData,
        MaxTipAmount = maxTipAmount,
        SuggestedTipAmounts = suggestedTipAmounts,
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
        SubscriptionPeriod = subscriptionPeriod,
        BusinessConnectionId = businessConnectionId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="CreateInvoiceLink">CreateInvoiceLink</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use CreateInvoiceLink instead")]
    public static Task<string> CreateInvoiceLinkAsync(
        this ITelegramBotClient botClient,
        string title,
        string description,
        string payload,
        string currency,
        IEnumerable<LabeledPrice> prices,
        string? providerToken = default,
        int? maxTipAmount = default,
        IEnumerable<int>? suggestedTipAmounts = default,
        string? providerData = default,
        string? photoUrl = default,
        int? photoSize = default,
        int? photoWidth = default,
        int? photoHeight = default,
        bool needName = default,
        bool needPhoneNumber = default,
        bool needEmail = default,
        bool needShippingAddress = default,
        bool sendPhoneNumberToProvider = default,
        bool sendEmailToProvider = default,
        bool isFlexible = default,
        int? subscriptionPeriod = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.CreateInvoiceLink(title, description, payload, currency, prices, providerToken, providerData, maxTipAmount, suggestedTipAmounts, photoUrl, photoSize, photoWidth, photoHeight, needName, needPhoneNumber, needEmail, needShippingAddress, sendPhoneNumberToProvider, sendEmailToProvider, isFlexible, subscriptionPeriod, businessConnectionId, cancellationToken);

    /// <summary>If you sent an invoice requesting a shipping address and the parameter <em>IsFlexible</em> was specified, the Bot API will send an <see cref="Update"/> with a <em>ShippingQuery</em> field to the bot. Use this method to reply to shipping queries</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="shippingQueryId">Unique identifier for the query to be answered</param>
    /// <param name="shippingOptions">A array of available shipping options.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task AnswerShippingQuery(
        this ITelegramBotClient botClient,
        string shippingQueryId,
        IEnumerable<ShippingOption> shippingOptions,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new AnswerShippingQueryRequest
    {
        ShippingQueryId = shippingQueryId,
        Ok = true,
        ShippingOptions = shippingOptions,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="AnswerShippingQuery">AnswerShippingQuery</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use AnswerShippingQuery instead")]
    public static Task AnswerShippingQueryAsync(
        this ITelegramBotClient botClient,
        string shippingQueryId,
        IEnumerable<ShippingOption> shippingOptions,
        CancellationToken cancellationToken = default
    ) => botClient.AnswerShippingQuery(shippingQueryId, shippingOptions, cancellationToken);

    /// <summary>If you sent an invoice requesting a shipping address and the parameter <em>IsFlexible</em> was specified, the Bot API will send an <see cref="Update"/> with a <em>ShippingQuery</em> field to the bot. Use this method to reply to shipping queries</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="shippingQueryId">Unique identifier for the query to be answered</param>
    /// <param name="errorMessage">Error message in human readable form that explains why it is impossible to complete the order (e.g. “Sorry, delivery to your desired address is unavailable”). Telegram will display this message to the user.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task AnswerShippingQuery(
        this ITelegramBotClient botClient,
        string shippingQueryId,
        string errorMessage,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new AnswerShippingQueryRequest
    {
        ShippingQueryId = shippingQueryId,
        Ok = false,
        ErrorMessage = errorMessage,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="AnswerShippingQuery">AnswerShippingQuery</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use AnswerShippingQuery instead")]
    public static Task AnswerShippingQueryAsync(
        this ITelegramBotClient botClient,
        string shippingQueryId,
        string errorMessage,
        CancellationToken cancellationToken = default
    ) => botClient.AnswerShippingQuery(shippingQueryId, errorMessage, cancellationToken);

    /// <summary>Once the user has confirmed their payment and shipping details, the Bot API sends the final confirmation in the form of an <see cref="Update"/> with the field <em>PreCheckoutQuery</em>. Use this method to respond to such pre-checkout queries <b>Note:</b> The Bot API must receive an answer within 10 seconds after the pre-checkout query was sent.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="preCheckoutQueryId">Unique identifier for the query to be answered</param>
    /// <param name="errorMessage">Error message in human readable form that explains the reason for failure to proceed with the checkout (e.g. "Sorry, somebody just bought the last of our amazing black T-shirts while you were busy filling out your payment details. Please choose a different color or garment!"). Telegram will display this message to the user.<para/>Leave <see langword="null"/> if everything is alright (goods are available, etc.) and the bot is ready to proceed with the order</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task AnswerPreCheckoutQuery(
        this ITelegramBotClient botClient,
        string preCheckoutQueryId,
        string? errorMessage = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new AnswerPreCheckoutQueryRequest
    {
        PreCheckoutQueryId = preCheckoutQueryId,
        Ok = errorMessage == null,
        ErrorMessage = errorMessage,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="AnswerPreCheckoutQuery">AnswerPreCheckoutQuery</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use AnswerPreCheckoutQuery instead")]
    public static Task AnswerPreCheckoutQueryAsync(
        this ITelegramBotClient botClient,
        string preCheckoutQueryId,
        string? errorMessage = default,
        CancellationToken cancellationToken = default
    ) => botClient.AnswerPreCheckoutQuery(preCheckoutQueryId, errorMessage, cancellationToken);

    /// <summary>Returns the bot's Telegram Star transactions in chronological order.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="offset">Number of transactions to skip in the response</param>
    /// <param name="limit">The maximum number of transactions to be retrieved. Values between 1-100 are accepted. Defaults to 100.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>A <see cref="StarTransactions"/> object.</returns>
    public static async Task<StarTransactions> GetStarTransactions(
        this ITelegramBotClient botClient,
        int? offset = default,
        int? limit = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetStarTransactionsRequest
    {
        Offset = offset,
        Limit = limit,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetStarTransactions">GetStarTransactions</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetStarTransactions instead")]
    public static Task<StarTransactions> GetStarTransactionsAsync(
        this ITelegramBotClient botClient,
        int? offset = default,
        int? limit = default,
        CancellationToken cancellationToken = default
    ) => botClient.GetStarTransactions(offset, limit, cancellationToken);

    /// <summary>Refunds a successful payment in <a href="https://t.me/BotNews/90">Telegram Stars</a>.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">Identifier of the user whose payment will be refunded</param>
    /// <param name="telegramPaymentChargeId">Telegram payment identifier</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task RefundStarPayment(
        this ITelegramBotClient botClient,
        long userId,
        string telegramPaymentChargeId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new RefundStarPaymentRequest
    {
        UserId = userId,
        TelegramPaymentChargeId = telegramPaymentChargeId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="RefundStarPayment">RefundStarPayment</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use RefundStarPayment instead")]
    public static Task RefundStarPaymentAsync(
        this ITelegramBotClient botClient,
        long userId,
        string telegramPaymentChargeId,
        CancellationToken cancellationToken = default
    ) => botClient.RefundStarPayment(userId, telegramPaymentChargeId, cancellationToken);

    /// <summary>Allows the bot to cancel or re-enable extension of a subscription paid in Telegram Stars.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">Identifier of the user whose subscription will be edited</param>
    /// <param name="telegramPaymentChargeId">Telegram payment identifier for the subscription</param>
    /// <param name="isCanceled">Pass <see langword="true"/> to cancel extension of the user subscription; the subscription must be active up to the end of the current subscription period. Pass <see langword="false"/> to allow the user to re-enable a subscription that was previously canceled by the bot.</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task EditUserStarSubscription(
        this ITelegramBotClient botClient,
        long userId,
        string telegramPaymentChargeId,
        bool isCanceled,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new EditUserStarSubscriptionRequest
    {
        UserId = userId,
        TelegramPaymentChargeId = telegramPaymentChargeId,
        IsCanceled = isCanceled,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="EditUserStarSubscription">EditUserStarSubscription</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use EditUserStarSubscription instead")]
    public static Task EditUserStarSubscriptionAsync(
        this ITelegramBotClient botClient,
        long userId,
        string telegramPaymentChargeId,
        bool isCanceled,
        CancellationToken cancellationToken = default
    ) => botClient.EditUserStarSubscription(userId, telegramPaymentChargeId, isCanceled, cancellationToken);

    /// <summary>Informs a user that some of the Telegram Passport elements they provided contains errors. The user will not be able to re-submit their Passport to you until the errors are fixed (the contents of the field for which you returned the error must change).<br/>Use this if the data submitted by the user doesn't satisfy the standards your service requires for any reason. For example, if a birthday date seems invalid, a submitted document is blurry, a scan shows evidence of tampering, etc. Supply some details in the error message to make sure the user knows how to correct the issues.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">User identifier</param>
    /// <param name="errors">A array describing the errors</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetPassportDataErrors(
        this ITelegramBotClient botClient,
        long userId,
        IEnumerable<PassportElementError> errors,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetPassportDataErrorsRequest
    {
        UserId = userId,
        Errors = errors,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetPassportDataErrors">SetPassportDataErrors</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetPassportDataErrors instead")]
    public static Task SetPassportDataErrorsAsync(
        this ITelegramBotClient botClient,
        long userId,
        IEnumerable<PassportElementError> errors,
        CancellationToken cancellationToken = default
    ) => botClient.SetPassportDataErrors(userId, errors, cancellationToken);

    #endregion Payments

    #region Games

    /// <summary>Use this method to send a game.</summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="chatId">Unique identifier for the target chat</param>
    /// <param name="gameShortName">Short name of the game, serves as the unique identifier for the game. Set up your games via <a href="https://t.me/botfather">@BotFather</a>.</param>
    /// <param name="replyParameters">Description of the message to reply to</param>
    /// <param name="replyMarkup">An object for an <a href="https://core.telegram.org/bots/features#inline-keyboards">inline keyboard</a>. If empty, one 'Play GameTitle' button will be shown. If not empty, the first button must launch the game.</param>
    /// <param name="messageThreadId">Unique identifier for the target message thread (topic) of the forum; for forum supergroups only</param>
    /// <param name="disableNotification">Sends the message <a href="https://telegram.org/blog/channels-2-0#silent-messages">silently</a>. Users will receive a notification with no sound.</param>
    /// <param name="protectContent">Protects the contents of the sent message from forwarding and saving</param>
    /// <param name="messageEffectId">Unique identifier of the message effect to be added to the message; for private chats only</param>
    /// <param name="businessConnectionId">Unique identifier of the business connection on behalf of which the message will be sent</param>
    /// <param name="allowPaidBroadcast">Pass <see langword="true"/> to allow up to 1000 messages per second, ignoring <a href="https://core.telegram.org/bots/faq#how-can-i-message-all-of-my-bot-39s-subscribers-at-once">broadcasting limits</a> for a fee of 0.1 Telegram Stars per message. The relevant Stars will be withdrawn from the bot's balance</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendGame(
        this ITelegramBotClient botClient,
        long chatId,
        string gameShortName,
        ReplyParameters? replyParameters = default,
        InlineKeyboardMarkup? replyMarkup = default,
        int? messageThreadId = default,
        bool disableNotification = default,
        bool protectContent = default,
        string? messageEffectId = default,
        string? businessConnectionId = default,
        bool allowPaidBroadcast = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SendGameRequest
    {
        ChatId = chatId,
        GameShortName = gameShortName,
        ReplyParameters = replyParameters,
        ReplyMarkup = replyMarkup,
        MessageThreadId = messageThreadId,
        DisableNotification = disableNotification,
        ProtectContent = protectContent,
        MessageEffectId = messageEffectId,
        BusinessConnectionId = businessConnectionId,
        AllowPaidBroadcast = allowPaidBroadcast,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SendGame">SendGame</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SendGame instead")]
    public static Task<Message> SendGameAsync(
        this ITelegramBotClient botClient,
        long chatId,
        string gameShortName,
        int? messageThreadId = default,
        bool disableNotification = default,
        bool protectContent = default,
        bool allowPaidBroadcast = default,
        string? messageEffectId = default,
        ReplyParameters? replyParameters = default,
        InlineKeyboardMarkup? replyMarkup = default,
        string? businessConnectionId = default,
        CancellationToken cancellationToken = default
    ) => botClient.SendGame(chatId, gameShortName, replyParameters, replyMarkup, messageThreadId, disableNotification, protectContent, messageEffectId, businessConnectionId, allowPaidBroadcast, cancellationToken);

    /// <summary>Use this method to set the score of the specified user in a game message.</summary>
    /// <remarks>Returns an error, if the new score is not greater than the user's current score in the chat and <paramref name="force"/> is <em>False</em>.</remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">User identifier</param>
    /// <param name="score">New score, must be non-negative</param>
    /// <param name="chatId">Unique identifier for the target chat</param>
    /// <param name="messageId">Identifier of the sent message</param>
    /// <param name="force">Pass <see langword="true"/> if the high score is allowed to decrease. This can be useful when fixing mistakes or banning cheaters</param>
    /// <param name="disableEditMessage">Pass <see langword="true"/> if the game message should not be automatically edited to include the current scoreboard</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>The <see cref="Message"/> is returned</returns>
    public static async Task<Message> SetGameScore(
        this ITelegramBotClient botClient,
        long userId,
        int score,
        long chatId,
        int messageId,
        bool force = default,
        bool disableEditMessage = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetGameScoreRequest
    {
        UserId = userId,
        Score = score,
        ChatId = chatId,
        MessageId = messageId,
        Force = force,
        DisableEditMessage = disableEditMessage,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetGameScore">SetGameScore</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetGameScore instead")]
    public static Task<Message> SetGameScoreAsync(
        this ITelegramBotClient botClient,
        long userId,
        int score,
        long chatId,
        int messageId,
        bool force = default,
        bool disableEditMessage = default,
        CancellationToken cancellationToken = default
    ) => botClient.SetGameScore(userId, score, chatId, messageId, force, disableEditMessage, cancellationToken);

    /// <summary>Use this method to set the score of the specified user in a game message.</summary>
    /// <remarks>Returns an error, if the new score is not greater than the user's current score in the chat and <paramref name="force"/> is <em>False</em>.</remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">User identifier</param>
    /// <param name="score">New score, must be non-negative</param>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="force">Pass <see langword="true"/> if the high score is allowed to decrease. This can be useful when fixing mistakes or banning cheaters</param>
    /// <param name="disableEditMessage">Pass <see langword="true"/> if the game message should not be automatically edited to include the current scoreboard</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    public static async Task SetGameScore(
        this ITelegramBotClient botClient,
        long userId,
        int score,
        string inlineMessageId,
        bool force = default,
        bool disableEditMessage = default,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new SetInlineGameScoreRequest
    {
        UserId = userId,
        Score = score,
        InlineMessageId = inlineMessageId,
        Force = force,
        DisableEditMessage = disableEditMessage,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="SetGameScore">SetGameScore</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use SetGameScore instead")]
    public static Task SetGameScoreAsync(
        this ITelegramBotClient botClient,
        long userId,
        int score,
        string inlineMessageId,
        bool force = default,
        bool disableEditMessage = default,
        CancellationToken cancellationToken = default
    ) => botClient.SetGameScore(userId, score, inlineMessageId, force, disableEditMessage, cancellationToken);

    /// <summary>Use this method to get data for high score tables. Will return the score of the specified user and several of their neighbors in a game.</summary>
    /// <remarks>This method will currently return scores for the target user, plus two of their closest neighbors on each side. Will also return the top three users if the user and their neighbors are not among them. Please note that this behavior is subject to change.</remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">Target user id</param>
    /// <param name="chatId">Unique identifier for the target chat</param>
    /// <param name="messageId">Identifier of the sent message</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>An Array of <see cref="GameHighScore"/> objects.</returns>
    public static async Task<GameHighScore[]> GetGameHighScores(
        this ITelegramBotClient botClient,
        long userId,
        long chatId,
        int messageId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetGameHighScoresRequest
    {
        UserId = userId,
        ChatId = chatId,
        MessageId = messageId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetGameHighScores">GetGameHighScores</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetGameHighScores instead")]
    public static Task<GameHighScore[]> GetGameHighScoresAsync(
        this ITelegramBotClient botClient,
        long userId,
        long chatId,
        int messageId,
        CancellationToken cancellationToken = default
    ) => botClient.GetGameHighScores(userId, chatId, messageId, cancellationToken);

    /// <summary>Use this method to get data for high score tables. Will return the score of the specified user and several of their neighbors in a game.</summary>
    /// <remarks>This method will currently return scores for the target user, plus two of their closest neighbors on each side. Will also return the top three users if the user and their neighbors are not among them. Please note that this behavior is subject to change.</remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="userId">Target user id</param>
    /// <param name="inlineMessageId">Identifier of the inline message</param>
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation</param>
    /// <returns>An Array of <see cref="GameHighScore"/> objects.</returns>
    public static async Task<GameHighScore[]> GetGameHighScores(
        this ITelegramBotClient botClient,
        long userId,
        string inlineMessageId,
        CancellationToken cancellationToken = default
    ) => await botClient.ThrowIfNull().SendRequest(new GetInlineGameHighScoresRequest
    {
        UserId = userId,
        InlineMessageId = inlineMessageId,
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>We removed all Async suffix from method names. Use <see cref="GetGameHighScores">GetGameHighScores</see> instead</summary>
    [Obsolete("We removed all Async suffix from method names. Use GetGameHighScores instead")]
    public static Task<GameHighScore[]> GetGameHighScoresAsync(
        this ITelegramBotClient botClient,
        long userId,
        string inlineMessageId,
        CancellationToken cancellationToken = default
    ) => botClient.GetGameHighScores(userId, inlineMessageId, cancellationToken);

    #endregion Games
}
