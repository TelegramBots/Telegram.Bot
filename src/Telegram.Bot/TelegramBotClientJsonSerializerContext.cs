using Telegram.Bot.Exceptions;
using Telegram.Bot.Requests;
using Telegram.Bot.Serialization;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot;


/// <summary>
/// A context for JSON serialization and deserialization
/// </summary>


#region Requests

[JsonSerializable(typeof(SetMyNameRequest))]
[JsonSerializable(typeof(SendMessageRequest))]
[JsonSerializable(typeof(SetMyDefaultAdministratorRightsRequest))]
[JsonSerializable(typeof(SetChatMenuButtonRequest))]
[JsonSerializable(typeof(GetUserChatBoostsRequest))]
[JsonSerializable(typeof(GetMyNameRequest))]
[JsonSerializable(typeof(GetMyDefaultAdministratorRightsRequest))]
[JsonSerializable(typeof(GetMeRequest))]
[JsonSerializable(typeof(GetChatMenuButtonRequest))]
[JsonSerializable(typeof(GetBusinessConnectionRequest))]
[JsonSerializable(typeof(AnswerCallbackQueryRequest))]
[JsonSerializable(typeof(SetMyShortDescriptionRequest))]
[JsonSerializable(typeof(GetMyShortDescriptionRequest))]
[JsonSerializable(typeof(SetMessageReactionRequest))]
[JsonSerializable(typeof(SendVoiceRequest))]
[JsonSerializable(typeof(SendVideoRequest))]
[JsonSerializable(typeof(SendVideoNoteRequest))]
[JsonSerializable(typeof(SendPollRequest))]
[JsonSerializable(typeof(SendPhotoRequest))]
[JsonSerializable(typeof(SendMediaGroupRequest))]
[JsonSerializable(typeof(SendDocumentRequest))]
[JsonSerializable(typeof(SendDiceRequest))]
[JsonSerializable(typeof(SendContactRequest))]
[JsonSerializable(typeof(SendChatActionRequest))]
[JsonSerializable(typeof(SendAudioRequest))]
[JsonSerializable(typeof(SendAnimationRequest))]
[JsonSerializable(typeof(ForwardMessagesRequest))]
[JsonSerializable(typeof(ForwardMessageRequest))]
[JsonSerializable(typeof(CopyMessagesRequest))]
[JsonSerializable(typeof(CopyMessageRequest))]
[JsonSerializable(typeof(StopMessageLiveLocationRequest))]
[JsonSerializable(typeof(StopInlineMessageLiveLocationRequest))]
[JsonSerializable(typeof(SendVenueRequest))]
[JsonSerializable(typeof(SendLocationRequest))]
[JsonSerializable(typeof(EditMessageLiveLocationRequest))]
[JsonSerializable(typeof(EditInlineMessageLiveLocationRequest))]
[JsonSerializable(typeof(StopPollRequest))]
[JsonSerializable(typeof(EditMessageTextRequest))]
[JsonSerializable(typeof(EditMessageReplyMarkupRequest))]
[JsonSerializable(typeof(EditMessageMediaRequest))]
[JsonSerializable(typeof(EditMessageCaptionRequest))]
[JsonSerializable(typeof(EditInlineMessageTextRequest))]
[JsonSerializable(typeof(EditInlineMessageReplyMarkupRequest))]
[JsonSerializable(typeof(EditInlineMessageMediaRequest))]
[JsonSerializable(typeof(EditInlineMessageCaptionRequest))]
[JsonSerializable(typeof(DeleteMessagesRequest))]
[JsonSerializable(typeof(DeleteMessageRequest))]
[JsonSerializable(typeof(UploadStickerFileRequest))]
[JsonSerializable(typeof(SetStickerSetTitleRequest))]
[JsonSerializable(typeof(SetStickerSetThumbnailRequest))]
[JsonSerializable(typeof(SetStickerPositionInSetRequest))]
[JsonSerializable(typeof(SetStickerMaskPositionRequest))]
[JsonSerializable(typeof(SetStickerKeywordsRequest))]
[JsonSerializable(typeof(SetStickerEmojiListRequest))]
[JsonSerializable(typeof(SetCustomEmojiStickerSetThumbnailRequest))]
[JsonSerializable(typeof(SendStickerRequest))]
[JsonSerializable(typeof(ReplaceStickerInSetRequest))]
[JsonSerializable(typeof(GetStickerSetRequest))]
[JsonSerializable(typeof(GetForumTopicIconStickersRequest))]
[JsonSerializable(typeof(GetCustomEmojiStickersRequest))]
[JsonSerializable(typeof(DeleteStickerSetRequest))]
[JsonSerializable(typeof(DeleteStickerFromSetRequest))]
[JsonSerializable(typeof(CreateNewStickerSetRequest))]
[JsonSerializable(typeof(AddStickerToSetRequest))]
[JsonSerializable(typeof(AnswerPreCheckoutQueryRequest))]
[JsonSerializable(typeof(AnswerShippingQueryRequest))]
[JsonSerializable(typeof(CreateInvoiceLinkRequest))]
[JsonSerializable(typeof(SendInvoiceRequest))]
[JsonSerializable(typeof(AnswerWebAppQueryRequest))]
[JsonSerializable(typeof(AnswerInlineQueryRequest))]
[JsonSerializable(typeof(SetWebhookRequest))]
[JsonSerializable(typeof(GetWebhookInfoRequest))]
[JsonSerializable(typeof(GetUpdatesRequest))]
[JsonSerializable(typeof(DeleteWebhookRequest))]
[JsonSerializable(typeof(SetInlineGameScoreRequest))]
[JsonSerializable(typeof(SetGameScoreRequest))]
[JsonSerializable(typeof(SendGameRequest))]
[JsonSerializable(typeof(GetInlineGameHighScoresRequest))]
[JsonSerializable(typeof(GetGameHighScoresRequest))]
[JsonSerializable(typeof(ApproveChatJoinRequest))]
[JsonSerializable(typeof(CreateChatInviteLinkRequest))]
[JsonSerializable(typeof(DeclineChatJoinRequest))]
[JsonSerializable(typeof(EditChatInviteLinkRequest))]
[JsonSerializable(typeof(ExportChatInviteLinkRequest))]
[JsonSerializable(typeof(RevokeChatInviteLinkRequest))]
[JsonSerializable(typeof(GetChatAdministratorsRequest))]
[JsonSerializable(typeof(GetChatMemberCountRequest))]
[JsonSerializable(typeof(GetChatRequest))]
[JsonSerializable(typeof(GetChatMemberRequest))]
[JsonSerializable(typeof(BanChatMemberRequest))]
[JsonSerializable(typeof(BanChatSenderChatRequest))]
[JsonSerializable(typeof(CloseForumTopicRequest))]
[JsonSerializable(typeof(CloseGeneralForumTopicRequest))]
[JsonSerializable(typeof(CreateForumTopicRequest))]
[JsonSerializable(typeof(DeleteChatPhotoRequest))]
[JsonSerializable(typeof(DeleteChatStickerSetRequest))]
[JsonSerializable(typeof(DeleteForumTopicRequest))]
[JsonSerializable(typeof(EditForumTopicRequest))]
[JsonSerializable(typeof(EditGeneralForumTopicRequest))]
[JsonSerializable(typeof(HideGeneralForumTopicRequest))]
[JsonSerializable(typeof(LeaveChatRequest))]
[JsonSerializable(typeof(PinChatMessageRequest))]
[JsonSerializable(typeof(PromoteChatMemberRequest))]
[JsonSerializable(typeof(ReopenForumTopicRequest))]
[JsonSerializable(typeof(ReopenGeneralForumTopicRequest))]
[JsonSerializable(typeof(RestrictChatMemberRequest))]
[JsonSerializable(typeof(SetChatAdministratorCustomTitleRequest))]
[JsonSerializable(typeof(SetChatDescriptionRequest))]
[JsonSerializable(typeof(SetChatPermissionsRequest))]
[JsonSerializable(typeof(SetChatPhotoRequest))]
[JsonSerializable(typeof(SetChatStickerSetRequest))]
[JsonSerializable(typeof(SetChatTitleRequest))]
[JsonSerializable(typeof(UnbanChatMemberRequest))]
[JsonSerializable(typeof(UnbanChatSenderChatRequest))]
[JsonSerializable(typeof(UnhideGeneralForumTopicRequest))]
[JsonSerializable(typeof(UnpinAllChatMessagesRequest))]
[JsonSerializable(typeof(UnpinAllForumTopicMessagesRequest))]
[JsonSerializable(typeof(UnpinAllGeneralForumTopicMessages))]
[JsonSerializable(typeof(UnpinAllGeneralForumTopicMessagesRequest))]
[JsonSerializable(typeof(UnpinChatMessageRequest))]
[JsonSerializable(typeof(LogOutRequest))]
[JsonSerializable(typeof(CloseRequest))]
[JsonSerializable(typeof(GetUserProfilePhotosRequest))]
[JsonSerializable(typeof(GetFileRequest))]
[JsonSerializable(typeof(SetMyDescriptionRequest))]
[JsonSerializable(typeof(GetMyDescriptionRequest))]
[JsonSerializable(typeof(SetMyCommandsRequest))]
[JsonSerializable(typeof(GetMyCommandsRequest))]
[JsonSerializable(typeof(DeleteMyCommandsRequest))]

#endregion

#region Responses

[JsonSerializable(typeof(ApiResponse))]
[JsonSerializable(typeof(ApiResponse<bool>))]
[JsonSerializable(typeof(ApiResponse<int>))]
[JsonSerializable(typeof(ApiResponse<string>))]
[JsonSerializable(typeof(ApiResponse<BotCommand[]>))]
[JsonSerializable(typeof(ApiResponse<BotName>))]
[JsonSerializable(typeof(ApiResponse<BotDescription>))]
[JsonSerializable(typeof(ApiResponse<BotShortDescription>))]
[JsonSerializable(typeof(ApiResponse<BusinessConnection>))]
[JsonSerializable(typeof(ApiResponse<User>))]
[JsonSerializable(typeof(ApiResponse<UserChatBoosts>))]
[JsonSerializable(typeof(ApiResponse<Update[]>))]
[JsonSerializable(typeof(ApiResponse<WebhookInfo>))]
[JsonSerializable(typeof(ApiResponse<StickerSet>))]
[JsonSerializable(typeof(ApiResponse<Sticker[]>))]
[JsonSerializable(typeof(ApiResponse<SentWebAppMessage>))]
[JsonSerializable(typeof(ApiResponse<GameHighScore[]>))]
[JsonSerializable(typeof(ApiResponse<File>))]
[JsonSerializable(typeof(ApiResponse<MenuButton>))]
[JsonSerializable(typeof(ApiResponse<ChatAdministratorRights>))]
[JsonSerializable(typeof(ApiResponse<Poll>))]
[JsonSerializable(typeof(ApiResponse<ForumTopic>))]
[JsonSerializable(typeof(ApiResponse<Chat>))]
[JsonSerializable(typeof(ApiResponse<ChatMember>))]
[JsonSerializable(typeof(ApiResponse<ChatMember[]>))]
[JsonSerializable(typeof(ApiResponse<ChatInviteLink>))]
[JsonSerializable(typeof(ApiResponse<UserProfilePhotos>))]
[JsonSerializable(typeof(ApiResponse<Message>))]
[JsonSerializable(typeof(ApiResponse<Message[]>))]
[JsonSerializable(typeof(ApiResponse<MessageId>))]
[JsonSerializable(typeof(ApiResponse<MessageId[]>))]
[JsonSerializable(typeof(ApiResponse<Sticker[]>))]

#endregion

#region Types

[JsonSerializable(typeof(ChatBoostSourcePremium))]
[JsonSerializable(typeof(ChatBoostSourceGiftCode))]
[JsonSerializable(typeof(ChatBoostSourceGiveaway))]
[JsonSerializable(typeof(ChatBoostSourceGiveaway))]
[JsonSerializable(typeof(ReactionTypeCustomEmoji))]
[JsonSerializable(typeof(ReactionTypeEmoji))]
[JsonSerializable(typeof(BotCommandScopeChatMember))]
[JsonSerializable(typeof(BotCommandScopeChatAdministrators))]
[JsonSerializable(typeof(BotCommandScopeChat))]
[JsonSerializable(typeof(BotCommandScopeAllChatAdministrators))]
[JsonSerializable(typeof(BotCommandScopeAllGroupChats))]
[JsonSerializable(typeof(BotCommandScopeAllPrivateChats))]
[JsonSerializable(typeof(BotCommandScopeDefault))]

[JsonSerializable(typeof(InlineQueryResultCachedAudio))]
[JsonSerializable(typeof(InlineQueryResultCachedDocument))]
[JsonSerializable(typeof(InlineQueryResultCachedGif))]
[JsonSerializable(typeof(InlineQueryResultCachedMpeg4Gif))]
[JsonSerializable(typeof(InlineQueryResultCachedPhoto))]
[JsonSerializable(typeof(InlineQueryResultCachedSticker))]
[JsonSerializable(typeof(InlineQueryResultCachedVideo))]
[JsonSerializable(typeof(InlineQueryResultCachedVoice))]
[JsonSerializable(typeof(InlineQueryResultArticle))]
[JsonSerializable(typeof(InlineQueryResultAudio))]
[JsonSerializable(typeof(InlineQueryResultContact))]
[JsonSerializable(typeof(InlineQueryResultDocument))]
[JsonSerializable(typeof(InlineQueryResultGame))]
[JsonSerializable(typeof(InlineQueryResultGif))]
[JsonSerializable(typeof(InlineQueryResultLocation))]
[JsonSerializable(typeof(InlineQueryResultMpeg4Gif))]
[JsonSerializable(typeof(InlineQueryResultPhoto))]
[JsonSerializable(typeof(InlineQueryResultVenue))]
[JsonSerializable(typeof(InlineQueryResultVideo))]
[JsonSerializable(typeof(InlineQueryResultVoice))]
[JsonSerializable(typeof(InputTextMessageContent))]
[JsonSerializable(typeof(InputContactMessageContent))]
[JsonSerializable(typeof(InputLocationMessageContent))]
[JsonSerializable(typeof(InputVenueMessageContent))]
[JsonSerializable(typeof(InputInvoiceMessageContent))]
[JsonSerializable(typeof(InputMediaAnimation))]
[JsonSerializable(typeof(InputMediaAudio))]
[JsonSerializable(typeof(InputMediaDocument))]
[JsonSerializable(typeof(InputMediaPhoto))]
[JsonSerializable(typeof(InputMediaVideo))]
[JsonSerializable(typeof(ChatMemberOwner))]
[JsonSerializable(typeof(ChatMemberBanned))]
[JsonSerializable(typeof(ChatMemberAdministrator))]
[JsonSerializable(typeof(ChatMemberMember))]
[JsonSerializable(typeof(ChatMemberRestricted))]
[JsonSerializable(typeof(ChatMemberLeft))]
[JsonSerializable(typeof(MenuButtonDefault))]
[JsonSerializable(typeof(MenuButtonCommands))]
[JsonSerializable(typeof(MenuButtonWebApp))]
[JsonSerializable(typeof(MessageOriginUser))]
[JsonSerializable(typeof(MessageOriginHiddenUser))]
[JsonSerializable(typeof(MessageOriginChat))]
[JsonSerializable(typeof(MessageOriginChannel))]
[JsonSerializable(typeof(InputFileUrl))]
[JsonSerializable(typeof(ReactionTypeKind))]
[JsonSerializable(typeof(InaccessibleMessage))]

#endregion
public partial class TelegramBotClientJsonSerializerContext : JsonSerializerContext
{
    private static JsonSerializerOptions _defaultOptions = new ()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
        Converters =
        {
            new UnixDateTimeConverter(),
            new BanTimeConverter(),
            new ColorConverter(),
            new InputFileConverter(),
            new ChatIdConverter(),
            new ChatBoostSourceConverter(),
            new BotCommandScopeConverter(),
            new InlineQueryResultConverter(),
            new InputMessageContentConverter(),
            new InputMediaConverter(),
            new ChatMemberConverter(),
            new MenuButtonConverter(),
            new MessageOriginConverter(),
            new ReactionTypeConverter(),
            new MaybeInaccessibleMessageConverter()
        }
    };

    private static TelegramBotClientJsonSerializerContext _current = new (JsonSerializerOptions);
    public static TelegramBotClientJsonSerializerContext Instance => _current;

    public static JsonSerializerOptions JsonSerializerOptions
    {
        get => _defaultOptions;

        set
        {
            _defaultOptions = value;
            _current = new(_defaultOptions);
        }
    }
}
