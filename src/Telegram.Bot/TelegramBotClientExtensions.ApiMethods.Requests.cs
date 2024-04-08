using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Extensions;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;
using File = Telegram.Bot.Types.File;

namespace Telegram.Bot;

/// <summary>
/// Extension methods that map to requests from Bot API documentation
/// </summary>
public static partial class TelegramBotClientExtensions
{
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
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> AnswerCallbackQueryAsync(
        this ITelegramBotClient botClient,
        AnswerCallbackQueryRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to delete the list of the bot’s commands for the given <see cref="DeleteMyCommandsRequest.Scope"/>
    /// and <see cref="DeleteMyCommandsRequest.LanguageCode">user language</see>. After deletion,
    /// <a href="https://core.telegram.org/bots/api#determining-list-of-commands">higher level commands</a>
    /// will be shown to affected users
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> DeleteMyCommandsAsync(
        this ITelegramBotClient botClient,
        DeleteMyCommandsRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get the current list of the bot’s commands for the given <see cref="GetMyCommandsRequest.Scope"/>
    /// and <see cref="GetMyCommandsRequest.LanguageCode">user language</see>
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// Returns Array of <see cref="BotCommand"/> on success. If commands aren't set, an empty list is returned
    /// </returns>
    public static async Task<BotCommand[]> GetMyCommandsAsync(
        this ITelegramBotClient botClient,
        GetMyCommandsRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the list of the bot’s commands.
    /// See <a href="https://core.telegram.org/bots#commands"/> for more details about bot commands
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetMyCommandsAsync(
        this ITelegramBotClient botClient,
        SetMyCommandsRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get the current <see cref="BotDescription">bot description</see>
    /// for the given <see cref="GetMyDescriptionRequest.LanguageCode">user language</see>.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// Returns <see cref="BotDescription"/> on success.
    /// </returns>
    public static async Task<BotDescription> GetMyDescriptionAsync(
        this ITelegramBotClient botClient,
        GetMyDescriptionRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the bot's description, which is shown in the chat
    /// with the bot if the chat is empty.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetMyDescriptionAsync(
        this ITelegramBotClient botClient,
        SetMyDescriptionRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get basic info about a file and prepare it for downloading. For the moment, bots can
    /// download files of up to 20MB in size. The file can then be downloaded via the link
    /// <c>https://api.telegram.org/file/bot&lt;token&gt;/&lt;file_path&gt;</c>, where <c>&lt;file_path&gt;</c>
    /// is taken from the response. It is guaranteed that the link will be valid for at least 1 hour.
    /// When the link expires, a new one can be requested by calling
    /// <see cref="GetFileAsync(ITelegramBotClient,GetFileRequest,CancellationToken)">GetFileAsync</see> again.
    /// </summary>
    /// <remarks>
    /// You can use <see cref="ITelegramBotClient.DownloadFileAsync"/> or
    /// <see cref="TelegramBotClientExtensions.GetInfoAndDownloadFileAsync"/> methods to download the file
    /// </remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, a <see cref="File"/> object is returned.</returns>
    public static async Task<File> GetFileAsync(
        this ITelegramBotClient botClient,
        GetFileRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get basic info about a file download it. For the moment, bots can download files
    /// of up to 20MB in size.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="fileId">File identifier to get info about</param>
    /// <param name="destination">Destination stream to write file to</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, a <see cref="File"/> object is returned.</returns>
    public static async Task<File> GetInfoAndDownloadFileAsync(
        this ITelegramBotClient botClient,
        string fileId,
        Stream destination,
        CancellationToken cancellationToken = default)
    {
        var file = await botClient.ThrowIfNull()
            .MakeRequestAsync(new GetFileRequest { FileId = fileId }, cancellationToken)
            .ConfigureAwait(false);

        await botClient.DownloadFileAsync(filePath: file.FilePath!, destination, cancellationToken)
            .ConfigureAwait(false);

        return file;
    }

    /// <summary>
    /// Use this method to get a list of profile pictures for a user.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns a <see cref="UserProfilePhotos"/> object</returns>
    public static async Task<UserProfilePhotos> GetUserProfilePhotosAsync(
        this ITelegramBotClient botClient,
        GetUserProfilePhotosRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get the current value of the bot’s menu button in a private chat,
    /// or the default menu button.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns><see cref="MenuButton"/> set for the given chat id or a default one</returns>
    public static async Task<MenuButton> GetChatMenuButtonAsync(
        this ITelegramBotClient botClient,
        GetChatMenuButtonRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// A simple method for testing your bot’s auth token.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns basic information about the bot in form of a <see cref="User"/> object.</returns>
    public static async Task<User> GetMeAsync(
        this ITelegramBotClient botClient,
        GetMeRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get the current default administrator rights of the bot.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Default or channel <see cref="ChatAdministratorRights"/> </returns>
    public static async Task<ChatAdministratorRights> GetMyDefaultAdministratorRightsAsync(
        this ITelegramBotClient botClient,
        GetMyDefaultAdministratorRightsRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get the current bot name for the given user language.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// Returns <see cref="BotName"/> on success.
    /// </returns>
    public static async Task<BotName> GetMyNameAsync(
        this ITelegramBotClient botClient,
        GetMyNameRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get the list of boosts added to a chat by a user.
    /// Requires administrator rights in the chat.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns a <see cref="UserChatBoosts"/> object.</returns>
    public static async Task<UserChatBoosts> GetUserChatBoostsAsync(
        this ITelegramBotClient botClient,
        GetUserChatBoostsRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get information about the connection of the bot with a business account.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns a <see cref="BusinessConnection"/> object.</returns>
    public static async Task<BusinessConnection> GetBusinessConnectionAsync(
        this ITelegramBotClient botClient,
        GetBusinessConnectionRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to close the bot instance before moving it from one local server to another. You need to
    /// delete the webhook before calling this method to ensure that the bot isn't launched again after server
    /// restart. The method will return error 429 in the first 10 minutes after the bot is launched.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> CloseAsync(
        this ITelegramBotClient botClient,
        CloseRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to log out from the cloud Bot API server before launching the bot locally. You <b>must</b>
    /// log out the bot before running it locally, otherwise there is no guarantee that the bot will receive
    /// updates. After a successful call, you can immediately log in on a local server, but will not be able to
    /// log in back to the cloud Bot API server for 10 minutes.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> LogOutAsync(
        this ITelegramBotClient botClient,
        LogOutRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to ban a user in a group, a supergroup or a channel. In the case of supergroups and
    /// channels, the user will not be able to return to the chat on their own using invite links, etc., unless
    /// <see cref="UnbanChatMemberAsync(ITelegramBotClient, UnbanChatMemberRequest, CancellationToken)">unbanned</see>
    /// first. The bot must be an administrator in the chat for this to work and must have the appropriate
    /// admin rights.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> BanChatMemberAsync(
        this ITelegramBotClient botClient,
        BanChatMemberRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns></returns>
    public static async Task<bool> BanChatSenderChatAsync(
        this ITelegramBotClient botClient,
        BanChatSenderChatRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to approve a chat join request. The bot must be an administrator in the chat for this to
    /// work and must have the <see cref="ChatPermissions.CanInviteUsers"/> administrator right.
    /// Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> ApproveChatJoinRequestAsync(
        this ITelegramBotClient botClient,
        ApproveChatJoinRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to create an additional invite link for a chat. The bot must be an administrator
    /// in the chat for this to work and must have the appropriate admin rights. The link can be revoked
    /// using the method
    /// <see cref="RevokeChatInviteLinkAsync(ITelegramBotClient,RevokeChatInviteLinkRequest,CancellationToken)">RevokeChatInviteLinkAsync</see>
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns the new invite link as <see cref="ChatInviteLink"/> object.</returns>
    public static async Task<ChatInviteLink> CreateChatInviteLinkAsync(
        this ITelegramBotClient botClient,
        CreateChatInviteLinkRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to decline a chat join request. The bot must be an administrator in the chat for this to
    /// work and must have the <see cref="ChatPermissions.CanInviteUsers"/> administrator right.
    /// Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> DeclineChatJoinRequestAsync(
        this ITelegramBotClient botClient,
        DeclineChatJoinRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit a non-primary invite link created by the bot. The bot must be an
    /// administrator in the chat for this to work and must have the appropriate admin rights
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns the edited invite link as a <see cref="ChatInviteLink"/> object.</returns>
    public static async Task<ChatInviteLink> EditChatInviteLinkAsync(
        this ITelegramBotClient botClient,
        EditChatInviteLinkRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to generate a new primary invite link for a chat; any previously generated primary
    /// link is revoked. The bot must be an administrator in the chat for this to work and must have the
    /// appropriate admin rights
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<string> ExportChatInviteLinkAsync(
        this ITelegramBotClient botClient,
        ExportChatInviteLinkRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to revoke an invite link created by the bot. If the primary link is revoked, a new
    /// link is automatically generated. The bot must be an administrator in the chat for this to work and
    /// must have the appropriate admin rights
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns the revoked invite link as <see cref="ChatInviteLink"/> object.</returns>
    public static async Task<ChatInviteLink> RevokeChatInviteLinkAsync(
        this ITelegramBotClient botClient,
        RevokeChatInviteLinkRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to close an open topic in a forum supergroup chat. The bot must be an administrator in the chat
    /// for this to work and must have the <see cref="ChatAdministratorRights.CanManageTopics"/> administrator rights,
    /// unless it is the creator of the topic. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> CloseForumTopicAsync(
        this ITelegramBotClient botClient,
        CloseForumTopicRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to close an open 'General' topic in a forum supergroup chat. The bot must be an administrator
    /// in the chat for this to work and must have the <see cref="ChatAdministratorRights.CanManageTopics"/>
    /// administrator rights. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> CloseGeneralForumTopicAsync(
        this ITelegramBotClient botClient,
        CloseGeneralForumTopicRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to create a topic in a forum supergroup chat. The bot must be an administrator in the chat for
    /// this to work and must have the <see cref="ChatAdministratorRights.CanManageTopics"/> administrator rights.
    /// Returns information about the created topic as a <see cref="ForumTopic"/> object.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// Returns information about the created topic as a <see cref="ForumTopic"/> object.
    /// </returns>
    public static async Task<ForumTopic> CreateForumTopicAsync(
        this ITelegramBotClient botClient,
        CreateForumTopicRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to delete a chat photo. Photos can't be changed for private chats. The bot must be an
    /// administrator in the chat for this to work and must have the appropriate admin rights
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> DeleteChatPhotoAsync(
        this ITelegramBotClient botClient,
        DeleteChatPhotoRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to delete a group sticker set from a supergroup. The bot must be an administrator in the
    /// chat for this to work and must have the appropriate admin rights. Use the field
    /// <see cref="Chat.CanSetStickerSet"/> optionally returned in
    /// <see cref="GetChatAsync(ITelegramBotClient,GetChatRequest,CancellationToken)">GetChatAsync</see>
    /// requests to check if the bot can use this method
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> DeleteChatStickerSetAsync(
        this ITelegramBotClient botClient,
        DeleteChatStickerSetRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to delete a forum topic along with all its messages in a forum supergroup chat. The bot must be
    /// an administrator in the chat for this to work and must have the
    /// <see cref="ChatAdministratorRights.CanManageTopics"/> administrator rights. Returns <see langword="true"/>
    /// on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> DeleteForumTopicAsync(
        this ITelegramBotClient botClient,
        DeleteForumTopicRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit name and icon of a topic in a forum supergroup chat. The bot must be an administrator
    /// in the chat for this to work and must have <see cref="ChatAdministratorRights.CanManageTopics"/> administrator
    /// rights, unless it is the creator of the topic. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> EditForumTopicAsync(
        this ITelegramBotClient botClient,
        EditForumTopicRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit the name of the 'General' topic in a forum supergroup chat. The bot must be an
    /// administrator in the chat for this to work and must have <see cref="ChatAdministratorRights.CanManageTopics"/>
    /// administrator rights. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> EditGeneralForumTopicAsync(
        this ITelegramBotClient botClient,
        EditGeneralForumTopicRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get a list of administrators in a chat.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// On success, returns an Array of <see cref="ChatMember"/> objects that contains information about all chat
    /// administrators except other bots. If the chat is a group or a supergroup and no administrators were
    /// appointed, only the creator will be returned
    /// </returns>
    public static async Task<ChatMember[]> GetChatAdministratorsAsync(
        this ITelegramBotClient botClient,
        GetChatAdministratorsRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get the number of members in a chat.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns <see cref="int"/> on success.</returns>
    public static async Task<int> GetChatMemberCountAsync(
        this ITelegramBotClient botClient,
        GetChatMemberCountRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get information about a member of a chat.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns a <see cref="ChatMember"/> object on success.</returns>
    public static async Task<ChatMember> GetChatMemberAsync(
        this ITelegramBotClient botClient,
        GetChatMemberRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get up to date information about the chat (current name of the user for one-on-one
    /// conversations, current username of a user, group or channel, etc.)
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns a <see cref="Chat"/> object on success.</returns>
    public static async Task<Chat> GetChatAsync(
        this ITelegramBotClient botClient,
        GetChatRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to hide the 'General' topic in a forum supergroup chat. The bot must be an administrator in the
    /// chat for this to work and must have the <see cref="ChatAdministratorRights.CanManageTopics"/> administrator
    /// rights. The topic will be automatically closed if it was open. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> HideGeneralForumTopicAsync(
        this ITelegramBotClient botClient,
        HideGeneralForumTopicRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method for your bot to leave a group, supergroup or channel.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> LeaveChatAsync(
        this ITelegramBotClient botClient,
        LeaveChatRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns></returns>
    public static async Task<bool> PinChatMessageAsync(
        this ITelegramBotClient botClient,
        PinChatMessageRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to promote or demote a user in a supergroup or a channel. The bot must be an administrator in
    /// the chat for this to work and must have the appropriate admin rights. Pass <c><see langword="false"/></c> for
    /// all boolean parameters to demote a user.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> PromoteChatMemberAsync(
        this ITelegramBotClient botClient,
        PromoteChatMemberRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to reopen a closed topic in a forum supergroup chat. The bot must be an administrator in the
    /// chat for this to work and must have the <see cref="ChatAdministratorRights.CanManageTopics"/> administrator
    /// rights, unless it is the creator of the topic. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> ReopenForumTopicAsync(
        this ITelegramBotClient botClient,
        ReopenForumTopicRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to reopen a closed 'General' topic in a forum supergroup chat. The bot must be an
    /// administrator in the chat for this to work and must have the
    /// <see cref="ChatAdministratorRights.CanManageTopics"/> administrator rights. The topic will be automatically
    /// unhidden if it was hidden. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> ReopenGeneralForumTopicAsync(
        this ITelegramBotClient botClient,
        ReopenGeneralForumTopicRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to restrict a user in a supergroup. The bot must be an administrator in the supergroup
    /// for this to work and must have the appropriate admin rights. Pass <see langword="true"/> for all permissions to
    /// lift restrictions from a user.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> RestrictChatMemberAsync(
        this ITelegramBotClient botClient,
        RestrictChatMemberRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set a custom title for an administrator in a supergroup promoted by the bot.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetChatAdministratorCustomTitleAsync(
        this ITelegramBotClient botClient,
        SetChatAdministratorCustomTitleRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the description of a group, a supergroup or a channel. The bot must
    /// be an administrator in the chat for this to work and must have the appropriate admin rights
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetChatDescriptionAsync(
        this ITelegramBotClient botClient,
        SetChatDescriptionRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set default chat permissions for all members. The bot must be an administrator
    /// in the group or a supergroup for this to work and must have the
    /// <see cref="ChatAdministratorRights.CanRestrictMembers"/> admin rights
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetChatPermissionsAsync(
        this ITelegramBotClient botClient,
        SetChatPermissionsRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set a new profile photo for the chat. Photos can't be changed for private chats.
    /// The bot must be an administrator in the chat for this to work and must have the appropriate admin rights
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetChatPhotoAsync(
        this ITelegramBotClient botClient,
        SetChatPhotoRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set a new group sticker set for a supergroup. The bot must be an administrator in the
    /// chat for this to work and must have the appropriate admin rights. Use the field
    /// <see cref="Chat.CanSetStickerSet"/> optionally returned in
    /// <see cref="GetChatAsync(ITelegramBotClient,GetChatRequest,CancellationToken)">GetChatAsync</see>
    /// request to check if the bot can use this method.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetChatStickerSetAsync(
        this ITelegramBotClient botClient,
        SetChatStickerSetRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the title of a chat. Titles can't be changed for private chats. The bot
    /// must be an administrator in the chat for this to work and must have the appropriate admin rights
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetChatTitleAsync(
        this ITelegramBotClient botClient,
        SetChatTitleRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to unban a previously banned user in a supergroup or channel. The user will <b>not</b>
    /// return to the group or channel automatically, but will be able to join via link, etc. The bot must be an
    /// administrator for this to work. By default, this method guarantees that after the call the user is not a
    /// member of the chat, but will be able to join it. So if the user is a member of the chat they will also be
    /// <b>removed</b> from the chat.
    /// If you don't want this, use the property <see name="UnbanChatMemberRequest.OnlyIfBanned"/>
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> UnbanChatMemberAsync(
        this ITelegramBotClient botClient,
        UnbanChatMemberRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to unban a previously banned channel chat in a supergroup or channel. The bot must be
    /// an administrator for this to work and must have the appropriate administrator rights.
    /// Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> UnbanChatSenderChatAsync(
        this ITelegramBotClient botClient,
        UnbanChatSenderChatRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to uhhide the 'General' topic in a forum supergroup chat. The bot must be an administrator
    /// in the chat for this to work and must have the <see cref="ChatAdministratorRights.CanManageTopics"/>
    /// administrator rights. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> UnhideGeneralForumTopicAsync(
        this ITelegramBotClient botClient,
        UnhideGeneralForumTopicRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to clear the list of pinned messages in a chat. If the chat is not a private chat,
    /// the bot must be an administrator in the chat for this to work and must have the
    /// '<see cref="ChatMemberAdministrator.CanPinMessages"/>' admin right in a supergroup or
    /// '<see cref="ChatMemberAdministrator.CanEditMessages"/>' admin right in a channel
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> UnpinAllChatMessagesAsync(
        this ITelegramBotClient botClient,
        UnpinAllChatMessagesRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to clear the list of pinned messages in a forum topic. The bot must be an administrator in the
    /// chat for this to work and must have the <see cref="ChatAdministratorRights.CanPinMessages"/> administrator
    /// right in the supergroup. Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> UnpinAllForumTopicMessagesAsync(
        this ITelegramBotClient botClient,
        UnpinAllForumTopicMessagesRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to clear the list of pinned messages in a General forum topic. The bot must be an administrator
    /// in the chat for this to work and must have the <see cref="ChatAdministratorRights.CanPinMessages"/>
    /// administrator right in the supergroup.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> UnpinAllGeneralForumTopicMessagesAsync(
        this ITelegramBotClient botClient,
        UnpinAllGeneralForumTopicMessagesRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to remove a message from the list of pinned messages in a chat. If the chat is not
    /// a private chat, the bot must be an administrator in the chat for this to work and must have the
    /// '<see cref="ChatMemberAdministrator.CanPinMessages"/>' admin right in a supergroup or
    /// '<see cref="ChatMemberAdministrator.CanEditMessages"/>' admin right in a channel
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> UnpinChatMessageAsync(
        this ITelegramBotClient botClient,
        UnpinChatMessageRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to copy messages of any kind. Service messages and invoice messages can't be copied.
    /// The method is analogous to the method
    /// <see cref="ForwardMessagesAsync(ITelegramBotClient,ForwardMessagesRequest,CancellationToken)"/>, but the
    /// copied message doesn't have a link to the original message.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns the <see cref="MessageId"/> of the sent message on success.</returns>
    public static async Task<MessageId> CopyMessageAsync(
        this ITelegramBotClient botClient,
        CopyMessageRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
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
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, an array of <see cref="MessageId"/> of the sent messages is returned.</returns>
    public static async Task<MessageId[]> CopyMessagesAsync(
        this ITelegramBotClient botClient,
        CopyMessagesRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to forward messages of any kind. Service messages can't be forwarded.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> ForwardMessageAsync(
        this ITelegramBotClient botClient,
        ForwardMessageRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to forward multiple messages of any kind. If some of the specified messages can't be found
    /// or forwarded, they are skipped. Service messages and messages with protected content can't be forwarded.
    /// Album grouping is kept for forwarded messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, an array of <see cref="MessageId"/> of the sent messages is returned.</returns>
    public static async Task<MessageId[]> ForwardMessagesAsync(
        this ITelegramBotClient botClient,
        ForwardMessagesRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit live location messages. A location can be edited until its
    /// <see cref="Location.LivePeriod"/> expires or editing is explicitly disabled by a call to
    /// <see cref="StopInlineMessageLiveLocationAsync"/>.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> EditInlineMessageLiveLocationAsync(
        this ITelegramBotClient botClient,
        EditInlineMessageLiveLocationRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit live location messages. A location can be edited until its
    /// <see cref="Location.LivePeriod"/> expires or editing is explicitly disabled by a call to
    /// <see cref="StopMessageLiveLocationAsync(ITelegramBotClient, StopMessageLiveLocationRequest, CancellationToken)"/>.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success the edited <see cref="Message"/> is returned.</returns>
    public static async Task<Message> EditMessageLiveLocationAsync(
        this ITelegramBotClient botClient,
        EditMessageLiveLocationRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send point on the map.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendLocationAsync(
        this ITelegramBotClient botClient,
        SendLocationRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send information about a venue.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    /// <a href="https://core.telegram.org/bots/api#sendvenue"/>
    public static async Task<Message> SendVenueAsync(
        this ITelegramBotClient botClient,
        SendVenueRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to stop updating a live location message before
    /// <see cref="Location.LivePeriod"/> expires.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> StopInlineMessageLiveLocationAsync(
        this ITelegramBotClient botClient,
        StopInlineMessageLiveLocationRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to stop updating a live location message before
    /// <see cref="Location.LivePeriod"/> expires.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success the sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> StopMessageLiveLocationAsync(
        this ITelegramBotClient botClient,
        StopMessageLiveLocationRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send animation files (GIF or H.264/MPEG-4 AVC video without sound). Bots can currently
    /// send animation files of up to 50 MB in size, this limit may be changed in the future.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendAnimationAsync(
        this ITelegramBotClient botClient,
        SendAnimationRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send audio files, if you want Telegram clients to display them in the music player.
    /// Your audio must be in the .MP3 or .M4A format. Bots can currently send audio files of up to 50 MB in size,
    /// this limit may be changed in the future.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendAudioAsync(
        this ITelegramBotClient botClient,
        SendAudioRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method when you need to tell the user that something is happening on the bot’s side. The status is
    /// set for 5 seconds or less (when a message arrives from your bot, Telegram clients clear its typing status).
    /// </summary>
    /// <example>
    /// <para>
    /// The <a href="https://t.me/imagebot">ImageBot</a> needs some time to process a request and upload the
    /// image. Instead of sending a text message along the lines of “Retrieving image, please wait…”, the bot may use
    /// <see cref="SendChatActionAsync(ITelegramBotClient, SendChatActionRequest, CancellationToken)">SendChatActionAsync</see>
    /// with <see cref="SendChatActionRequest.Action"/> = <see cref="ChatAction.UploadPhoto"/>. The user will see a
    /// “sending photo” status for the bot.
    /// </para>
    /// <para>
    /// We only recommend using this method when a response from the bot will take a <b>noticeable</b> amount of
    /// time to arrive.
    /// </para>
    /// </example>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SendChatActionAsync(
        this ITelegramBotClient botClient,
        SendChatActionRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send phone contacts.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendContactAsync(
        this ITelegramBotClient botClient,
        SendContactRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send an animated emoji that will display a random value.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendDiceAsync(
        this ITelegramBotClient botClient,
        SendDiceRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send general files. Bots can currently send files of any type of up to 50 MB in size,
    /// this limit may be changed in the future.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendDocumentAsync(
        this ITelegramBotClient botClient,
        SendDocumentRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send a group of photos, videos, documents or audios as an album. Documents and audio
    /// files can be only grouped in an album with messages of the same type.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, an array of <see cref="Message"/>s that were sent is returned.</returns>
    public static async Task<Message[]> SendMediaGroupAsync(
        this ITelegramBotClient botClient,
        SendMediaGroupRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send text messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendMessageAsync(
        this ITelegramBotClient botClient,
        SendMessageRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send photos.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendPhotoAsync(
        this ITelegramBotClient botClient,
        SendPhotoRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send a native poll.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendPollAsync(
        this ITelegramBotClient botClient,
        SendPollRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// As of <a href="https://telegram.org/blog/video-messages-and-telescope">v.4.0</a>, Telegram clients
    /// support rounded square mp4 videos of up to 1 minute long. Use this method to send video messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendVideoNoteAsync(
        this ITelegramBotClient botClient,
        SendVideoNoteRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send video files, Telegram clients support mp4 videos (other formats may be sent as
    /// <see cref="Document"/>). Bots can currently send video files of up to 50 MB in size, this limit may be
    /// changed in the future.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendVideoAsync(
        this ITelegramBotClient botClient,
        SendVideoRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send audio files, if you want Telegram clients to display the file as a playable voice
    /// message. For this to work, your audio must be in an .OGG file encoded with OPUS (other formats may be sent
    /// as <see cref="Audio"/> or <see cref="Document"/>). Bots can currently send voice messages of up to 50 MB
    /// in size, this limit may be changed in the future.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendVoiceAsync(
        this ITelegramBotClient botClient,
        SendVoiceRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the chosen reactions on a message. Service messages can't be reacted to.
    /// Automatically forwarded messages from a channel to its discussion group have the same
    /// available reactions as messages in the channel.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetMessageReactionAsync(
        this ITelegramBotClient botClient,
        SetMessageReactionRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the bot’s menu button in a private chat, or the default menu button.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetChatMenuButtonAsync(
        this ITelegramBotClient botClient,
        SetChatMenuButtonRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the default administrator rights requested by the bot when it's added as an
    /// administrator to groups or channels. These rights will be suggested to users, but they are free to modify
    /// the list before adding the bot.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetMyDefaultAdministratorRightsAsync(
        this ITelegramBotClient botClient,
        SetMyDefaultAdministratorRightsRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the bot's name.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetMyNameAsync(
        this ITelegramBotClient botClient,
        SetMyNameRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get the current bot <see cref="BotShortDescription">short description</see>
    /// for the given <see cref="GetMyShortDescriptionRequest.LanguageCode">user language</see>.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// Returns <see cref="BotShortDescription"/> on success.
    /// </returns>
    public static async Task<BotShortDescription> GetMyShortDescriptionAsync(
        this ITelegramBotClient botClient,
        GetMyShortDescriptionRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the bot's short description,which is shown on
    /// the bot's profile page and is sent together with the link when users share the bot.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns></returns>
    public static async Task<bool> SetMyShortDescriptionAsync(
        this ITelegramBotClient botClient,
        SetMyShortDescriptionRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
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
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, returns an Array of <see cref="GameHighScore"/> objects.</returns>
    public static async Task<GameHighScore[]> GetGameHighScoresAsync(
        this ITelegramBotClient botClient,
        GetGameHighScoresRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
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
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, returns an Array of <see cref="GameHighScore"/> objects.</returns>
    public static async Task<GameHighScore[]> GetInlineGameHighScoresAsync(
        this ITelegramBotClient botClient,
        GetInlineGameHighScoresRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send a game.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendGameAsync(
        this ITelegramBotClient botClient,
        SendGameRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set the score of the specified user in a game.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// On success returns the edited <see cref="Message"/>. Returns an error, if the new score is not greater
    /// than the user's current score in the chat and <see cref="SetGameScoreRequest.Force"/> is <see langword="false"/>
    /// </returns>
    public static async Task<Message> SetGameScoreAsync(
        this ITelegramBotClient botClient,
        SetGameScoreRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set the score of the specified user in a game.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// Returns an error, if the new score is not greater than the user's current score in the chat and
    /// <see cref="SetGameScoreRequest.Force"/> is <see langword="false"/>
    /// </returns>
    public static async Task<bool> SetInlineGameScoreAsync(
        this ITelegramBotClient botClient,
        SetInlineGameScoreRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to remove webhook integration if you decide to switch back to
    /// <see cref="GetUpdatesAsync(ITelegramBotClient,GetUpdatesRequest,CancellationToken)">GetUpdatesAsync</see>
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns <see langword="true"/> on success</returns>
    public static async Task<bool> DeleteWebhookAsync(
        this ITelegramBotClient botClient,
        DeleteWebhookRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to receive incoming updates using long polling
    /// (<a href="https://en.wikipedia.org/wiki/Push_technology#Long_polling">wiki</a>)
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <remarks>
    /// <list type="number">
    /// <item>This method will not work if an outgoing webhook is set up</item>
    /// <item>
    /// In order to avoid getting duplicate updates, recalculate <see cref="GetUpdatesRequest.Offset"/> after each server
    /// response
    /// </item>
    /// </list>
    /// </remarks>
    /// <returns>An Array of <see cref="Update"/> objects is returned.</returns>
    public static async Task<Update[]> GetUpdatesAsync(
        this ITelegramBotClient botClient,
        GetUpdatesRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get current webhook status.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// On success, returns a <see cref="WebhookInfo"/> object. If the bot is using
    /// <see cref="GetUpdatesAsync(ITelegramBotClient,GetUpdatesRequest,CancellationToken)"/>, will return an object
    /// with the <see cref="WebhookInfo.Url"/> field empty.
    /// </returns>
    public static async Task<WebhookInfo> GetWebhookInfoAsync(
        this ITelegramBotClient botClient,
        GetWebhookInfoRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
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
    /// will contain a header <c>X-Telegram-Bot-Api-Secret-Token</c> with the secret token as content.
    /// </para>
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <remarks>
    /// <list type="number">
    /// <item>
    /// You will not be able to receive updates using
    /// <see cref="GetUpdatesAsync(ITelegramBotClient,GetUpdatesRequest,CancellationToken)"/> for as long as an
    /// outgoing webhook is set up
    /// </item>
    /// <item>
    /// To use a self-signed certificate, you need to upload your
    /// <a href="https://core.telegram.org/bots/self-signed">public key certificate</a> using
    /// <see cref="SetWebhookRequest.Certificate"/> parameter. Please upload as <see cref="InputFileStream"/>,
    /// sending a string will not work
    /// </item>
    /// <item>Ports currently supported for webhooks: <b>443, 80, 88, 8443</b></item>
    /// </list>
    /// If you're having any trouble setting up webhooks, please check out this
    /// <a href="https://core.telegram.org/bots/webhooks">amazing guide to Webhooks</a>.
    /// </remarks>

    public static async Task<bool> SetWebhookAsync(
        this ITelegramBotClient botClient,
        SetWebhookRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send answers to an inline query.
    /// </summary>
    /// <remarks>
    /// No more than <b>50</b> results per query are allowed.
    /// </remarks>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> AnswerInlineQueryAsync(
        this ITelegramBotClient botClient,
        AnswerInlineQueryRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set the result of an interaction with a Web App and send a corresponding message on
    /// behalf of the user to the chat from which the query originated. On success, a <see cref="SentWebAppMessage"/>
    /// object is returned.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<SentWebAppMessage> AnswerWebAppQueryAsync(
        this ITelegramBotClient botClient,
        AnswerWebAppQueryRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
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
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> AnswerPreCheckoutQueryAsync(
        this ITelegramBotClient botClient,
        AnswerPreCheckoutQueryRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// If you sent an invoice requesting a shipping address and the parameter <c>isFlexible"</c> was specified,
    /// the Bot API will send an <see cref="Update"/> with a <see cref="ShippingQuery"/> field
    /// to the bot. Use this method to reply to shipping queries
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> AnswerShippingQueryAsync(
        this ITelegramBotClient botClient,
        AnswerShippingQueryRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to create a link for an invoice.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    public static async Task<string> CreateInvoiceLinkAsync(
        this ITelegramBotClient botClient,
        CreateInvoiceLinkRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send invoices.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the sent <see cref="Message"/> is returned.</returns>
    public static async Task<Message> SendInvoiceAsync(
        this ITelegramBotClient botClient,
        SendInvoiceRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
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
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> AddStickerToSetAsync(
        this ITelegramBotClient botClient,
        AddStickerToSetRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to create a new sticker set owned by a user.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> CreateNewStickerSetAsync(
        this ITelegramBotClient botClient,
        CreateNewStickerSetRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to delete a sticker from a set created by the bot.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> DeleteStickerFromSetAsync(
        this ITelegramBotClient botClient,
        DeleteStickerFromSetRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to replace an existing sticker in a sticker set with a new one. The method is equivalent to
    /// calling <see cref="DeleteStickerFromSetAsync(Telegram.Bot.ITelegramBotClient,Telegram.Bot.Requests.DeleteStickerFromSetRequest,System.Threading.CancellationToken)"/>,
    /// then <see cref="AddStickerToSetAsync(Telegram.Bot.ITelegramBotClient,Telegram.Bot.Requests.AddStickerToSetRequest,System.Threading.CancellationToken)"/>,
    /// then <see cref="SetStickerPositionInSetAsync(Telegram.Bot.ITelegramBotClient,Telegram.Bot.Requests.SetStickerPositionInSetRequest,System.Threading.CancellationToken)"/>.
    /// Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task ReplaceStickerInSetAsync(
        this ITelegramBotClient botClient,
        ReplaceStickerInSetRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to delete a sticker set that was created by the bot.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> DeleteStickerSetAsync(
        this ITelegramBotClient botClient,
        DeleteStickerSetRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get information about custom emoji stickers by their identifiers.
    /// Returns an Array of <see cref="Sticker"/> objects.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, a <see cref="StickerSet"/> object is returned.</returns>
    public static async Task<Sticker[]> GetCustomEmojiStickersAsync(
        this ITelegramBotClient botClient,
        GetCustomEmojiStickersRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get custom emoji stickers, which can be used as a forum topic icon by any user.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>Returns an Array of <see cref="Sticker"/> objects.</returns>
    public static async Task<Sticker[]> GetForumTopicIconStickersAsync(
        this ITelegramBotClient botClient,
        GetForumTopicIconStickersRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to get a sticker set.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// On success, a <see cref="StickerSet"/> object is returned.
    /// </returns>
    public static async Task<StickerSet> GetStickerSetAsync(
        this ITelegramBotClient botClient,
        GetStickerSetRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to send static .WEBP, animated .TGS, or video .WEBM stickers.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// On success, the sent <see cref="Message"/> is returned.
    /// </returns>
    public static async Task<Message> SendStickerAsync(
        this ITelegramBotClient botClient,
        SendStickerRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set the thumbnail of a custom emoji sticker set.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetCustomEmojiStickerSetThumbnailAsync(
        this ITelegramBotClient botClient,
        SetCustomEmojiStickerSetThumbnailRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the list of emoji assigned to a regular or custom emoji sticker.
    /// The sticker must belong to a sticker set created by the bot.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetStickerEmojiListAsync(
        this ITelegramBotClient botClient,
        SetStickerEmojiListRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change search keywords assigned to a regular or custom emoji sticker.
    /// The sticker must belong to a sticker set created by the bot.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetStickerKeywordsAsync(
        this ITelegramBotClient botClient,
        SetStickerKeywordsRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to change the mask position of a mask sticker.
    /// The sticker must belong to a sticker set that was created by the bot.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetStickerMaskPositionAsync(
        this ITelegramBotClient botClient,
        SetStickerMaskPositionRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to move a sticker in a set created by the bot to a specific position.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetStickerPositionInSetAsync(
        this ITelegramBotClient botClient,
        SetStickerPositionInSetRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set the thumbnail of a regular or mask sticker set.
    /// The format of the thumbnail file must match the format of the stickers in the set.
    /// Returns <see langword="true"/> on success.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetStickerSetThumbnailAsync(
        this ITelegramBotClient botClient,
        SetStickerSetThumbnailRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to set the title of a created sticker set.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> SetStickerSetTitleAsync(
        this ITelegramBotClient botClient,
        SetStickerSetTitleRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to upload a file with a sticker for later use in the
    /// <see cref="CreateNewStickerSetRequest"/> and <see cref="AddStickerToSetRequest"/>
    /// methods (the file can be used multiple times).
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>
    /// Returns the uploaded <see cref="File"/> on success.
    /// </returns>
    public static async Task<File> UploadStickerFileAsync(
        this ITelegramBotClient botClient,
        UploadStickerFileRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
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
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> DeleteMessageAsync(
        this ITelegramBotClient botClient,
        DeleteMessageRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to delete multiple messages simultaneously.
    /// If some of the specified messages can't be found, they are skipped.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> DeleteMessagesAsync(
        this ITelegramBotClient botClient,
        DeleteMessagesRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit captions of messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> EditInlineMessageCaptionAsync(
        this ITelegramBotClient botClient,
        EditInlineMessageCaptionRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit animation, audio, document, photo, or video messages. If a message is part of
    /// a message album, then it can be edited only to an audio for audio albums, only to a document for document
    /// albums and to a photo or a video otherwise. Use a previously uploaded file via its
    /// <see cref="InputFileId"/> or specify a URL
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> EditInlineMessageMediaAsync(
        this ITelegramBotClient botClient,
        EditInlineMessageMediaRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit only the reply markup of messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> EditInlineMessageReplyMarkupAsync(
        this ITelegramBotClient botClient,
        EditInlineMessageReplyMarkupRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit text and game messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    public static async Task<bool> EditInlineMessageTextAsync(
        this ITelegramBotClient botClient,
        EditInlineMessageTextRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit captions of messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success the edited <see cref="Message"/> is returned.</returns>
    public static async Task<Message> EditMessageCaptionAsync(
        this ITelegramBotClient botClient,
        EditMessageCaptionRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit animation, audio, document, photo, or video messages. If a message is part of
    /// a message album, then it can be edited only to an audio for audio albums, only to a document for document
    /// albums and to a photo or a video otherwise. Use a previously uploaded file via its
    /// <see cref="InputFileId"/> or specify a URL
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success the edited <see cref="Message"/> is returned.</returns>
    public static async Task<Message> EditMessageMediaAsync(
        this ITelegramBotClient botClient,
        EditMessageMediaRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit only the reply markup of messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success the edited <see cref="Message"/> is returned.</returns>
    public static async Task<Message> EditMessageReplyMarkupAsync(
        this ITelegramBotClient botClient,
        EditMessageReplyMarkupRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to edit text and game messages.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success the edited <see cref="Message"/> is returned.</returns>
    public static async Task<Message> EditMessageTextAsync(
        this ITelegramBotClient botClient,
        EditMessageTextRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);

    /// <summary>
    /// Use this method to stop a poll which was sent by the bot.
    /// </summary>
    /// <param name="botClient">An instance of <see cref="ITelegramBotClient"/></param>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used by other objects or threads to receive notice of cancellation
    /// </param>
    /// <returns>On success, the stopped <see cref="Poll"/> with the final results is returned.</returns>
    public static async Task<Poll> StopPollAsync(
        this ITelegramBotClient botClient,
        StopPollRequest request,
        CancellationToken cancellationToken = default
    ) =>
        await botClient.ThrowIfNull()
            .MakeRequestAsync(request, cancellationToken)
            .ConfigureAwait(false);
}
