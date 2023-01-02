# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/)
and this project adheres to [Semantic Versioning](https://semver.org/).

<!--

## [Unreleased]

### Added

### Changed

### Fixed

### Removed

-->

<!-- markdownlint-configure-file { "MD024": false } -->

## [v19.0.0-preview.2] - Unreleased

> [Bot API 6.4](https://core.telegram.org/bots/api#december-30-2022) (December 30, 2022)

### Added

- New requests related to topics:
  - `CloseGeneralForumTopicRequest`
  - `EditGeneralForumTopicRequest`
  - `ReopenGeneralForumTopicRequest`
  - `HideGeneralForumTopicRequest`
  - `UnhideGeneralForumTopicRequest`
- New methods related to topics:
  - `ITelegramBotClient.CloseGeneralForumTopicAsync`
  - `ITelegramBotClient.EditGeneralForumTopicAsync`
  - `ITelegramBotClient.ReopenGeneralForumTopicAsync`
  - `ITelegramBotClient.HideGeneralForumTopicAsync`
  - `ITelegramBotClient.UnhideGeneralForumTopicAsync`
- Optional property `HasSpoiler`
- Added property `bool? HasSpoiler` to the following classes:
  - `SendAnimationRequest`
  - `SendVideoRequest`
  - `SendPhotoRequest`
  - `InputMediaAnimation`
  - `InputMediaPhoto`
  - `InputMediaVideo`
- Added following properties to class `Message`:
  - `HasMediaSpoiler`
  - `WriteAccessAllowed`
  - `ForumTopicEdited`
  - `GeneralForumTopicHidden`
  - `GeneralForumTopicUnhidden`
- Property `IsPersistent` to class `ReplyKeyboardMarkup`
- Types `ForumTopicEdited`, `GeneralForumTopicHidden`, `GeneralForumTopicUnhidden`, `WriteAccessAllowed`
- Property `int? MessageThreadId` to class `SendChatActionRequest`
- Properties `HasAggressiveAntiSpamEnabled` and `HasHiddenMembers` to class `Chat`
- Enum members `ForumTopicEdited`, `GeneralForumTopicHidden`, `GeneralForumTopicUnhidden`, `WriteAccessAllowed` to enum `MessageType`,

### Changed
- Properties `EditForumTopicRequest.Name` and `EditForumTopicRequest.IconCustomEmojiId` are made nullable
- Added setters to properties `EditForumTopicRequest.Name` and `EditForumTopicRequest.IconCustomEmojiId`
- Parameters `name` and `iconCustomEmojiId` and ctor of `EditForumTopicRequest` are made nullable and optional
- Added optional parameter `bool? hasSpoiler` to the following methods
  - `ITelegramBotClient.SendAnimationAsync`
  - `ITelegramBotClient.SendVideoAsync`
  - `ITelegramBotClient.SendPhotoAsync`
- Added optional parameter `int? messageThreadId` to method `ITelegramBotClient.SendAnimationRequest`
- Parameters `name` and `iconCustomEmojiId` in method `ITelegramBotClient.EditForumTopicAsync` are made nullable and optional

## [v19.0.0-preview.1] - 2022-12-03

> [Bot API 6.1](https://core.telegram.org/bots/api#june-20-2022) (June 20, 2022)

> [Bot API 6.2](https://core.telegram.org/bots/api#august-12-2022) (August 12, 2022)

> [Bot API 6.3](https://core.telegram.org/bots/api#november-5-2022) (November 5, 2022)

### Added

- .NET 6 to targeted frameworks
- Following topic releated types:
  - `Color` to represent color of topics
  - `ForumTopic`
  - `ForumTopicClosed`
  - `ForumTopicCreated`
  - `ForumTopicReopened`
- New requests for managing topics:
  - `CloseForumTopicRequest`
  - `CreateForumTopicRequest`
  - `DeleteForumTopicRequest`
  - `EditForumTopicRequest`
  - `ReopenForumTopicRequest`
  - `UnpinAllForumTopicMessagesRequest`
- Property `MessageThreadId` to following requests
  - `SendMessageRequest`
  - `SendPhotoRequest` 
  - `SendVideoRequest` 
  - `SendAnimationRequest`
  - `SendAudioRequest`
  - `SendDocumentRequest`
  - `SendStickerRequest`
  - `SendVideoNoteRequest`
  - `SendVoiceRequest`
  - `SendLocationRequest`
  - `SendVenueRequest`
  - `SendContactRequest`
  - `SendPollRequest`
  - `SendDiceRequest`
  - `SendInvoiceRequest`
  - `SendGameRequest`
  - `SendMediaGroupRequest`
  - `CopyMessageRequest`
  - `ForwardMessageRequest`
- Following properties to type `Chat`:
  - `bool? IsForum`
  - `string[]? ActiveUsernames`
  - `string? EmojiStatusCustomEmojiId`
  - `bool? HasRestrictedVoiceAndVideoMessages`
- Property `bool? CanManageTopics` to following types:
  - `ChatAdministratorRights`
  - `ChatPermissions`
  - `ChatMemberOwner`
  - `ChatMemberRestricted`
  - `PromoteChatMemberRequest`
- Following enum members to `MessageType`:
  - `ForumTopicCreated`
  - `ForumTopicClosed`
  - `ForumTopicReopened`
  - `Animation`
- Following properties to type `Message`:
  - `int? MessageThreadId`
  - `bool? IsTopicMessage`
  - `ForumTopicCreated? ForumTopicCreated`
  - `ForumTopicClosed? ForumTopicClosed`
  - `ForumTopicClosed? ForumTopicClosed`
- Enum member `CustomEmoji` to `MessageEntityType`
- Property `CustomEmojiId`  to `MessageEntity`
- Extension method `GetCustomEmojiStickersAsync`
- Request `GetCustomEmojiStickersRequest`
- Enum `StickerType`
- Properties `Type` and `CustomEmojiId` to `Sticker`
- Property `StickerType` to `StickerSet`
- Property `StickerType` to `CreateNewStickerSetRequest`
- Parameter `stickerType` to `CreateNew*StickerSetAsync` extension methods
- Property `HasRestrictedVoiceAndVideoMessages` to `Chat`
- Properties `JoinToSendMessages`, `JoinByRequest` to `Chat`
- Properties `IsPremium`, `AddedToAttachmentMenu` to `User`
- Property `PremiumAnimation` to `Sticker`
- Property `SecretToken` to `SetWebhookRequest`
- Parameter `secretToken` to `SetWebhookAsync`
- Request `CreateInvoiceLinkRequest`
- Method `CreateInvoiceLinkAsync`

### Changed

- Constructors in following requests accept `IInputFile` or inheritors instead of
  - `AddAnimatedStickerToSetRequest`
  - `AddStaticStickerToSetRequest`
  - `AddVideoStickerToSetRequest`
  - `CreateNewAnimatedStickerSetRequest`
  - `CreateNewStaticStickerSetRequest`
  - `CreateNewVideoStickerSetRequest`
- Added optional parameter `int? messageThreadId` to following methods:
  - `ITelegramBotClient.SendTextMessageAsync`
  - `ITelegramBotClient.SendPhotoAsync`
  - `ITelegramBotClient.SendVideoAsync`
  - `ITelegramBotClient.SendAnimationAsync`
  - `ITelegramBotClient.SendAudioAsync`
  - `ITelegramBotClient.SendDocumentAsync`
  - `ITelegramBotClient.SendStickerAsync`
  - `ITelegramBotClient.SendVideoNoteAsync`
  - `ITelegramBotClient.SendVoiceAsync`
  - `ITelegramBotClient.SendLocationAsync`
  - `ITelegramBotClient.SendVenueAsync`
  - `ITelegramBotClient.SendContactAsync`
  - `ITelegramBotClient.SendPollAsync`
  - `ITelegramBotClient.SendDiceAsync`
  - `ITelegramBotClient.SendInvoiceAsync`
  - `ITelegramBotClient.SendGameAsync`
  - `ITelegramBotClient.SendMediaGroupAsync`
  - `ITelegramBotClient.CopyMessageAsync`
  - `ITelegramBotClient.ForwardMessageAsync`
- Added optional parameter `bool? canManageTopic` to method `ITelegramBotClient.PromoteChatMemberAsync`

### Fixed

- Property 'Message.Type' returns `MessageType.Animation` when a `message` contains `Animation` 

### Removed

- Implicit conversion from `ChatId` to `string`
- .NET Core 3.1 from targeted frameworks
- Property `ContainsMasks` from `StickerSet`
- Property `ContainsMasks` from `CreateNewStickerSetRequest`
- Parameter `containsMasks` from `CreateNew*StickerSetAsync` extension methods

## [v18.0.0] - 2022-06-16

> [Bot API 6.0](https://core.telegram.org/bots/api#april-16-2022) (April 16, 2022)

### Added

- Package `Telegram.Bot.Extensions.Polling` is merged in the main package
- Type `TelegramBotClientOptions`
- Types `WebAppInfo`, `SentWebAppMessage`, `WebAppData`, `MenuButton`, `MenuButtonCommands`, `MenuButtonWebApp`,
  `MenuButtonDefault`, `ChatAdministratorRights`
- Requests `AnswerWebAppQueryRequest`, `SetChatMenuButtonRequest`, `GetChatMenuButtonRequest`,
  `SetMyDefaultAdministratorRightsRequest`, `GetMyDefaultAdministratorRightsRequest`
- Properties `WebAppInfo KeyboardButton.WebApp`, `WebAppInfo InlineKeyboardButton.WebApp`
- Property `DateTime? WebHookInfo.LastSynchronizationErrorDate`
- Static methods `KeyboardButton.WithWebApp`, `InlineKeyboardButton.WithWebApp`
- Properties `VideoChatScheduled`, `VideoChatStarted`, `VideoChatEnded`, `VideoChatParticipantsInvited` and 
  `WebAppData` in type `Message`
- Enum members `VideoChatScheduled`, `VideoChatStarted`, `VideoChatEnded`, and `VideoChatParticipantsInvited` 
  in type `MessageType`
- Property `bool ITelegramBotClient.LocalBotServer`
- `TelegramBotClient` constructor that accepts an instance `TelegramBotClientOptions` and `HttpClient`

### Changed
- Renamed properties `ChatMemberAdministrator.CanManageVoiceChats`, `PromoteChatMemberRequest.CanManageVoiceChats` to 
  `ChatMemberAdministrator.CanManageVideoChats ` and `PromoteChatMemberRequest.CanManageVideoChats`
- Removed `baseUrl` parameter from constructor in `TelegramBotClient` that accepts a token
- Type of property `FileBase.FileSize` changed from `int?` to `long?`

### Fixed
- Argument `protectContent` in method `TelegramBotClientExtensions.ForwardMessageAsync` is passed to the
  corresponding request

### Removed
- Enum members `VoiceChatScheduled`, `VoiceChatStarted`, `VoiceChatEnded`, and `VoiceChatParticipantsInvited`
  in type `MessageType`
- Properties `VoiceChatScheduled`, `VoiceChatStarted`, `VoiceChatEnded` and `VoiceChatParticipantsInvited`
  in type `Message`
- Property `PinChatMessageRequest.ProtectedContent`

## [v18.0.0-alpha.1] - 2022-02-13

> [Bot API 5.7](https://core.telegram.org/bots/api#january-31-2022) (January 31, 2022)

> [Bot API 5.6](https://core.telegram.org/bots/api#december-30-2021) (December 30, 2021)

> [Bot API 5.5](https://core.telegram.org/bots/api#december-7-2021) (December 7, 2021)

### Changed

- `ApiRequestEventArgs` has full request information

### Added

- Requests `CreateNewVideoStickerSetRequest`, `AddVideoStickerToSetRequest`, `BanChatSenderChatRequest`, `UnbanChatSenderChatRequest`
- Extension methods `TelegramBotClientExtensions.CreateNewVideoStickerSetAsync`, `TelegramBotClientExtensions.AddVideoStickerToSetAsync`, `TelegramBotClientExtensions.BanChatSenderChatRequestAsync`, `TelegramBotClientExtensions.UnbanChatSenderChatRequestAsync`
- Property `int? MessageAutoDeleteTime` to class `Chat`
- Property `bool? HasPrivateForwards` to class `Chat`
- Property `bool? HasProtectedContent` to class `Chat`
- Property `int? MessageAutoDeleteTime` to class `Message`
- Property `bool? IsAutomaticForward` to class `Message`
- Property `bool? HasProtectedContent` to class `Message`
- Property `bool? ProtectContent` to following requests:
  - `SendLocationRequest`
  - `SendVenueRequest`
  - `CopyMessageRequest`
  - `ForwardMessageRequest`
  - `SendAnimationRequest`
  - `SendAudioRequest`
  - `SendContactRequest`
  - `SendDiceRequest`
  - `SendDocumentRequest`
  - `SendMediaGroupRequest`
  - `SendMessageRequest`
  - `SendPhotoRequest`
  - `SendPollRequest`
  - `SendVideoNoteRequest`
  - `SendVideoRequest`
  - `SendVoiceRequest`
- Property `IsVideo` to class `Sticker`
- Property `IsVideo` to class `StickerSet`
- Parameter `bool? protectContent = default` to following methods:
  - `TelegramBotExtensions.SendLocationRequestAsync`
  - `TelegramBotExtensions.SendVenueRequestAsync`
  - `TelegramBotExtensions.CopyMessageRequestAsync`
  - `TelegramBotExtensions.ForwardMessageRequestAsync`
  - `TelegramBotExtensions.SendAnimationRequestAsync`
  - `TelegramBotExtensions.SendAudioRequestAsync`
  - `TelegramBotExtensions.SendContactRequestAsync`
  - `TelegramBotExtensions.SendDiceRequestAsync`
  - `TelegramBotExtensions.SendDocumentRequestAsync`
  - `TelegramBotExtensions.SendMediaGroupRequestAsync`
  - `TelegramBotExtensions.SendMessageRequestAsync`
  - `TelegramBotExtensions.SendPhotoRequestAsync`
  - `TelegramBotExtensions.SendPollRequestAsync`
  - `TelegramBotExtensions.SendVideoNoteRequestAsync`
  - `TelegramBotExtensions.SendVideoRequestAsync`
  - `TelegramBotExtensions.SendVoiceRequestAsync`
- Enum member `MessageEntityType.Spoiler`

### Changed

- Method `TelegramBotClient.MakeRequestAsync` is made virtual
- Class `CreateNewStickerSetRequest` renamed to `CreateNewStaticStickerSetRequest`
- Class `CreateNewStickerSetRequest` is made abstract
- Class `AddStickerToSetRequest` renamed to `AddStaticStickerToSetRequest`
- Class `AddStickerToSetRequest` is made abstract
- Method `TelegramBotClientExtensions.AddStickerToSetAsync` is renamed to `AddStaticStickerToSetAsync`
- Method `TelegramBotClientExtensions.CreateNewStaticStickerSetAsync` is renamed to `CreateNewStaticStickerSetAsync`

### Fixed

- `DeleteWebhookRequest` parameters are now properly serializing
- Added missing json attribute to property `ChatInviteLink.Name`

### Removed

- Parameter `untilDate` is removed from `TelegramBotClientExtensions.BanChatSenderChatAsync`
- Property `UntilDate` is removed from `BanChatSenderChatRequest`

## [v17.0.0] - 2021-11-17

- v17 release

## [v17.0.0-alpha.5] - 2021-11-12

### Added

- Added missing JSON attributes on types `ApproveChatJoinRequest` and `DeclineChatJoinRequest`
- Types `ApproveChatJoinRequest` and `DeclineChatJoinRequest` implement `IChatTargetable` interface

## [v17.0.0-alpha.4] - 2021-11-06

> [Bot API 5.4](https://core.telegram.org/bots/api#november-5-2021) (November 5, 2021)

### Added

- Request `ApproveChatJoinRequest`
- Request `DeclineChatJoinRequest`
- Property `bool? CreateChatInviteLinkRequest.CreatesJoinRequest`
- Property `string? CreateChatInviteLinkRequest.Name`
- Property `bool? EditChatInviteLinkRequest.CreatesJoinRequest`
- Property `string? EditChatInviteLinkRequest.Name`
- Property `bool ChatInviteLink.CreatesJoinRequest`
- Property `int? ChatInviteLink.PendingJoinRequestCount`
- Type `ChatJoinRequest`
- Property `ChatJoinRequest Update.ChatJoinRequest`
- Enum member `ChatAction.ChooseSticker`
- Extension method `TelegramBotClientExtensions.ApproveChatJoinRequestAsync`
- Extension method `TelegramBotClientExtensions.DeclineChatJoinRequestAsync`

### Changed

- Extension method `TelegramBotClientExtensions.EditChatInviteLinkAsync`:
  - Added parameters `string? name` and `bool? createsJoinRequest`
- Extension method `TelegramBotClientExtensions.CreateChatInviteLinkAsync`:
  - Added parameters `string? name` and `bool? createsJoinRequest`

### Changed

- Fields `ChatId.Identifier` and `ChatId.Username` changed into get-only properties

## [v17.0.0-alpha.3] - 2021-09-01

### Changed

- Method `GetInfoAndDownloadFileAsync` moved into static class `TelegramBotClientExtensions` as an extension method
- Symbols are always include in the package

## [v17.0.0-alpha.2] - 2021-09-01

### Added

- Interface `IExceptionsParser`
- Type `ApiResponse`
- Property `ITelegramBotClient.ExceptionsParser`

## [v.16.0.2] - 2021-08-16

### Fixed

- Parameter name `ChatLocation.String` replaced with `ChatLocation.Address`

## [v16.0.1] - 2021-07-10

### Fixed

- `ITelegramBotClient.SendDocumentAsync` passed wrong value into `DisableContentTypeDetection` property

## [v17.0.0-alpha.1] - 2021-06-13

> [Bot API 5.3](https://core.telegram.org/bots/api#june-25-2021) (June 25, 2021)

### Added

- Enum `InputMediaType`
- Type `BanCommandScope`
- Type `BanCommandScopeDefault`
- Type `BanCommandScopeAllPrivateChats`
- Type `BanCommandScopeAllGroupChats`
- Type `BanCommandScopeAllChatAdministrators`
- Type `BanCommandScopeChat`
- Type `BanCommandScopeChatAdministrators`
- Type `BanCommandScopeChatMember`
- Enum `BanCommandScopeType`
- Type `ChatMemberOwner`
- Type `ChatMemberAdministrator`
- Type `ChatMemberMember`
- Type `ChatMemberRestricted`
- Type `ChatMemberLeft`
- Type `ChatMemberBanned`
- Request `BanChatMemberRequest`
- Request `BanChatMemberRequest`  
- Request `DeleteMyCommandsRequest`
- Request `GetChatMemberCountRequest`
- Method `ITelegramBotClient.DeleteMyCommandsAsync`
- Method `ITelegramBotClient.BanChatMemberAsync`
- Method `ITelegramBotClient.GetChatMemberCountAsync`
- Property `BotCommandScope GetMyCommandsRequest.Scope { get; set; }`
- Property `string GetMyCommandsRequest.LanguageCode { get; set; }`
- Property `BotCommandScope SetMyCommandsRequest.Scope { get; set; }`
- Property `string SetMyCommandsRequest.LanguageCode { get; set; }`
- Property `IRequest<TResponse>.IsWebhookResponse { get; set; }`
- Protected constructor `InputMediaBase` that accepts `InputMedia`
- Protected constructor `InputTelegramFile` that accepts `FileType`
- Property `string ForceReplyMarkup.InputFieldPlaceholder { get; set; }`
- Property `string ReplyKeyboardMarkup.InputFieldPlaceholder { get; set; }`
- Enum `EncryptedPassportElementType`
- Interface `IChatTargetable`
- Interface `IUserTargetable`

### Changed

- Type `InlineQueryResultBase` renamed to `InlineQueryResult`
- Type `ChatMember` is made abstract
- Property `ChatMember.Status` is made abstract
- Every use of enum `ParseMode` is made nullable to represent default text mode without any markup
- Type `KickChatMemberRequest` is marked as obsolete
- Type `GetChatMembersCountRequest` is marked as obsolete
- Method `ITelegramBotClient.KickChatMemberAsync` is marked as obsolete
- Method `ITelegramBotClient.GetChatMembersCountAsync` is marked as obsolete
- All underlying enum values changed to start from `1` instead of `0`. `0` value are reserved for unknown enum values.
- Type `ChatMember` is made abstract and it's properties are moved into separate inheriting classes
- Changed parameters in `ITelegramBotClient.GetMyCommandsAsync`: added parameters `BotCommandScope scope` and `string language`
- Changed parameters in `ITelegramBotClient.SetMyCommandsAsync`: added parameters `BotCommandScope scope` and `string language`
- Type of property `IInputMedia.Type` changed from `string` to `InputMediaType`
- Property `InputFileStream.FileType` is no longer virtual
- Constructor of type `InputFileStream` that accepts both `Stream content` and `string fileName`: `filename` parameter is made optional
- Constructor of type `InputOnlineFile` that accepts both `Stream content` and `string fileName`: `filename` parameter is made optional
- Constructor of type `InputTelegramFile` that accepts both `Stream content` and `string fileName`: `filename` parameter is made optional
- Property `InputMediaBase.Type` is made abstract
- Protected setter `InputTelegramFile.FileId` is made private protected
- Type of property `EncryptedPassportElement.Type` changed from `string` to `EncryptedPassportElementType`
- All optional types are made nullable be it value or reference types

### Removed

- Public setter `ChatMember.Status`
- Enum member `ParseMode.Default`
- Enum members `ChatAction.RecordAudio` and `ChatAction.UploadAudio`
- Protected setter from property `InputFileStream.Content`
- Constructor of type `InputFileStream` that accepts only `Stream`
- Constructor of type `InputOnlineFile` that accepts only `Stream`
- Constructor of type `InputTelegramFile` that accepts only `Stream`
- Property setter `InputMediaBase.Media`
- Protected setter `InputOnlineFile.Url`
- All obsolete types, methods and properties related to polling events
- Following interfaces: `ICaptionEntities`, `IEntities`, `IFormattableEntities`, `IInlineMessage`,
  `IInlineReplyMarkupMessage`, `INotifiableMessage`, `IReplyMarkupMessage`, `IReplyMessage`, `IThumbMediaMessage`,
  `ICaptionInlineQueryResult`, `ILocationInlineQueryResult`, `IThumbnailInlineQueryResult`,
  `IThumbnailUrlInlineQueryResult`

## [v16.0.0] - 2021-06-13

### Changed

- `Animation` inherits from `FileBase`
- All custom exceptions are marked as obsolete

## [v16.0.0-alpha.2] - 2021-05-10

> [Bot API 5.2](https://core.telegram.org/bots/api#april-26-2021) (April 26, 2021)

### Added

- Property `VoiceChatScheduled` to the class `Message`.
- Types `VoiceChatScheduled`, `InputInvoiceMessageContent`
- New `MessageType` value: `VoiceChatScheduled`
- Property `ChatType` to the class `InlineQuery`.
- New `ChatType` value: `Sender`
- New `ChatAction` values: `RecordVoice`, `UploadVoice`
- Optional parameters `maxTipAmount` and `suggestedTipAmounts` to `ITelegramBotClient.SendInvoiceAsync`
- Properties `MaxTipAmount` and `SuggestedTipAmounts` to `SendInvoiceRequest`

### Changed

- Parameter order in `ITelegramBotClient.UnpinChatMessageAsync`
- Parameter `startParameter` of the method `ITelegramBotClient.SendInvoiceAsync` became optional
- `ChatAction` values `RecordAudio` and `UploadAudio` marked as obsolete
- `ReplyToMessageId` and `AllowSendingWithoutReply` in `IReplyMessage`, `CopyMessageRequest`, `SendLocationRequest`, `SendAnimationRequest`, `SendAudioRequest`, `SendContactRequest`, `SendDiceRequest`, `SendDocumentRequest`, `SendMediaGroupRequest`, `SendMessageRequest`, `SendPhotoRequest`, `SendPollRequest`, `SendVenueRequest`, `SendVideoNoteRequest`, `SendVideoRequest`, `SendVoiceRequest`, `SendGameRequest`, `SendStickerRequest` marked as optional

> ⚠️ WARNING! ⚠️
> After the next Bot API update (Bot API 5.3) there will be a one-time change of the value of the field `FileUniqueId` in objects of the type `PhotoSize` and of the fields `SmallFileUniqueId` and `BigFileUniqueId` in objects of the type `ChatPhoto`.

<!-- -->
> ⚠️ WARNING! ⚠️
> Service messages about non-bot users joining the chat will be soon removed from large groups. We recommend using the “chat_member” update as a replacement.

<!-- -->
> ⚠️ WARNING! ⚠️
> After one of the upcoming Bot API updates, user identifiers will become bigger than 2^31 - 1 and it will be no longer possible to store them in a signed 32-bit integer type. User identifiers will have up to 52 significant bits, so a 64-bit integer or double-precision float type would still be safe for storing them. Please make sure that your code can correctly handle such user identifiers.

### Fixed

- Align property order and description with official docs

### Removed

- Parameter `startParameter` from `SendInvoiceRequest` constructor

## [v16.0.0-alpha.1] - 2021-05-01

> [Bot API 5.1](https://core.telegram.org/bots/api#march-9-2021) (March 9, 2021)
> [Bot API 5.0](https://core.telegram.org/bots/api#november-4-2020) (November 4, 2020)

### Added

- The method `ITelegramBotClient.CreateChatInviteLinkAsync`
- The method `ITelegramBotClient.EditChatInviteLinkAsync`
- The method `ITelegramBotClient.RevokeChatInviteLinkAsync`
- Optional parameter `revokeMessages` to `ITelegramBotClient.KickChatMemberAsync`
- Optional parameters `canManageChat`, `canManageVoiceChats` to `ITelegramBotClient.KickChatMemberAsync`
- Property `RevokeMessages` to `KickChatMemberRequest`
- Properties `CanManageChat`, `CanManageVoiceChats` to `PromoteChatMemberRequest`
- Properties `CanManageChat`, `CanManageVoiceChats` to `ChatMember`
- Properties `MessageAutoDeleteTimerChanged`, `VoiceChatStarted`, `VoiceChatEnded`, `VoiceChatParticipantsInvited` to `Message`
- Properties `MyChatMember` and `ChatMember` to `Update`
- Types `CreateChatInviteLinkRequest`, `EditChatInviteLinkRequest`, `RevokeChatInviteLinkRequest`, `ChatInviteLink`, `ChatMemberUpdated`, `MessageAutoDeleteTimerChanged`, `VoiceChatEnded`, `VoiceChatParticipantsInvited`, `VoiceChatStarted`
- New enum value `Bowling` for `Emoji`
- New enum values `MessageAutoDeleteTimerChanged`, `ProximityAlertTriggered`, `VoiceChatStarted`, `VoiceChatEnded`, `VoiceChatParticipantsInvited` for `MessageType`
- New enum values `MyChatMember`, `ChatMember` for `UpdateType`
- Delegate `AsyncEventHandler<T>`
- Methods:
  - `ITelegramBotClient.LogOutAsync`
  - `ITelegramBotClient.CloseAsync`
  - `ITelegramBotClient.CopyMessageAsync`
  - `ITelegramBotClient.UnpinAllChatMessages`
- Optional parameter `ipAddress` to `ITelegramBotClient.SetWebhookAsync`
- Optional parameter `dropPendingUpdates` to `ITelegramBotClient.SetWebhookAsync`, `ITelegramBotClient.DeleteWebhookAsync`
- Optional parameter `allowSendingWithoutReply` to the methods `SendTextMessageAsync`, `SendPhotoAsync`, `SendVideoAsync`, `SendAnimationAsync`, `SendAudioAsync`, `SendDocumentAsync`, `SendStickerAsync`, `SendVideoNoteAsync`, `SendVoiceAsync`, `SendLocationAsync`, `SendVenueAsync`, `SendContactAsync`, `SendPollAsync`, `SendDiceAsync`, `SendInvoiceAsync`, `SendGameAsync`, `SendMediaGroupAsync`
- Optional parameter `captionEntities` to `SendTextMessageAsync`, `SendPhotoAsync`, `SendVideoAsync`, `SendAnimationAsync`, `SendAudioAsync`, `SendDocumentAsync`, `SendVoiceAsync`, `SendPollAsync`, `EditMessageTextAsync`, `EditMessageCaptionAsync`
- Optional parameter `isAnonymous` to `ITelegramBotClient.PromoteChatMemberAsync`
- Optional parameter `messageId` to `ITelegramBotClient.UnpinChatMessageAsync`
- Optional parameter `onlyIfBanned` to `ITelegramBotClient.UnbanChatMemberAsync`
- Optional parameter `disableContentTypeDetection` to `ITelegramBotClient.SendDocumentAsync`
- Optional parameter `heading` to `ITelegramBotClient.SendLocationAsync`, `ITelegramBotClient.EditMessageLiveLocationAsync`
- Optional parameter `proximityAlertRadius` to `ITelegramBotClient.SendLocationAsync`, `ITelegramBotClient.EditMessageLiveLocationAsync`
- Optional parameter `horizontalAccuracy` to `ITelegramBotClient.SendLocationAsync`, `ITelegramBotClient.EditMessageLiveLocationAsync`
- Optional parameters `googlePlaceId`, `googlePlaceType` to `ITelegramBotClient.SendVenueAsync`
- Property `IpAddress` to `SetWebhookRequest`, `WebhookInfo`
- Property `DropPendingUpdates` to `SetWebhookRequest`, `DeleteWebhookRequest`
- Property `AllowSendingWithoutReply` to `SendMessageRequest`, `SendPhotoRequest`, `SendAudioRequest`, `SendDocumentRequest`, `SendStickerRequest`, `SendVideoRequest`, `SendAnimationRequest`, `SendVoiceRequest`, `SendVideoNoteRequest`, `SendMediaGroupRequest`, `SendLocationRequest`, `SendVenueRequest`, `SendContactRequest`, `SendPollRequest`, `SendDiceRequest`, `SendInvoiceRequest`, `SendGameRequest`
- Property `CaptionEntities` to `InputMediaBase`, `InlineQueryResultPhoto`, `InlineQueryResultGif`, `InlineQueryResultMpeg4Gif`, `InlineQueryResultVideo`, `InlineQueryResultAudio`, `InlineQueryResultVoice`, `InlineQueryResultDocument`, `InlineQueryResultCachedPhoto`, `InlineQueryResultCachedGif`, `InlineQueryResultCachedMpeg4Gif`, `InlineQueryResultCachedVideo`, `InlineQueryResultCachedAudio`, `InlineQueryResultCachedVoice`, `InlineQueryResultCachedDocument`
- Property `Entities` to `InputTextMessageContent`
- Properties `SenderChat`, `AuthorSignature`, `ProximityAlertTriggered` to `Message`
- Properties `Bio`, `LinkedChatId`, `Location` to `Chat`
- Property `IsAnonymous` to `ChatMember`, `PromoteChatMemberRequest`
- Property `LivePeriod` to `Location`
- Property `ProximityAlertRadius` to `Location`, `InlineQueryResultLocation`, `InputLocationMessageContent`, `SendLocationRequest`, `EditMessageLiveLocationRequest`, `EditInlineMessageLiveLocationRequest`
- Property `HorizontalAccuracy` to `Location`, `InlineQueryResultLocation`, `InputLocationMessageContent`, `SendLocationRequest`, `EditMessageLiveLocationRequest`, `EditInlineMessageLiveLocationRequest`
- Property `Heading` to `Location`, `InlineQueryResultLocation`, `SendLocationRequest`, `EditMessageLiveLocationRequest`, `EditInlineMessageLiveLocationRequest`
- Property `MessageId` to `PinChatMessageRequest`
- Property `OnlyIfBanned` to `UnbanChatMemberRequest`
- Property `FileName` to `Audio`, `Video`
- Property `DisableContentTypeDetection` to `MakeRequestAsync`, `InputMediaDocument`
- Properties `GooglePlaceId`, `GooglePlaceType` to `SendVenueRequest`, `Venue`, `InlineQueryResultVenue`, `InputVenueMessageContent`
- New enum values `Football`, `SlotMachine` for `Emoji`
- Type `ChatLocation`
- Type `ProximityAlertTriggered`
- Support for sending and receiving audio and document albums in the method `SendMediaGroupAsync`

### Changed

- Constructor in `TelegramBotClient` accepts base url for custom Bot API server as optional third parameter, it accepts only URL's with protocol, host and port parts, everything else is ignored
- Marked constructor for `TelegramBotClient` accepting `IWebProxy` as obsolete
- Property `ITelegramBotClient.BotId` to `long?`
- Event `MakingApiRequest` renamed to `OnMakingApiRequest` and it's type is changed to `AsyncEventHandler<ApiRequestEventArgs>`
- Event `ApiResponseReceived` renamed to `OnApiResponseReceived` and it's type is changed to `AsyncEventHandler<ApiResponseEventArgs>`
- Parameters order in following methods (to reflect [official docs](https://core.telegram.org/bots/api#available-methods)):
  - `SetWebhookAsync`, `DeleteWebhookAsync`, `SendTextMessageAsync`, `SendPhotoAsync`, `SendAudioAsync`, `SendDocumentAsync`, `SendStickerAsync`, `SendVideoAsync`, `SendAnimationAsync`, `SendVoiceAsync`, `SendVideoNoteAsync`, `SendMediaGroupAsync`, `SendLocationAsync`, `SendVenueAsync`, `SendContactAsync`, `SendPollAsync`, `SendDiceAsync`, `KickChatMemberAsync`, `UnbanChatMemberAsync`, `PromoteChatMemberAsync`, `EditMessageTextAsync`, `EditMessageCaptionAsync`, `EditMessageLiveLocationAsync`, `SendInvoiceAsync`, `SendGameAsync`
- Polling inside the library is now considered obsolete. The code, related to polling will be removed. It is recommended to use [Telegram.Bot.Extensions.Polling](https://github.com/TelegramBots/Telegram.Bot.Extensions.Polling) package instead.
  - These methods are now obsolete: `StartReceiving`, `StopReceiving`
  - These events are now obsolete: `OnUpdate`, `OnMessage`, `OnMessageEdited`, `OnInlineQuery`, `OnInlineResultChosen`, `OnCallbackQuery`, `OnReceiveError`, `OnReceiveGeneralError`
  - These fields are now obsolete: `IsReceiving`, `MessageOffset`
- Error `429 Too Many Request` is now handled by the client and is thrown as `ApiRequestException`

>⚠️ WARNING! ⚠️
>
>After one of the upcoming Bot API updates, _some_ user identifiers will become bigger than 2^31 - 1 and it will be no longer possible to store them in a signed 32-bit integer type. User identifiers will have up to 52 significant bits, so a 64-bit integer or double-precision float type would still be safe for storing them. Please make sure that your code can correctly handle such user identifiers.

### Fixed

- Incorrect property name `ExplanationCaptionEntities` -> `ExplanationEntities` in `SendPollRequest`

### Removed

- Obsolete overload method `ITelegramBotClient.DownloadFileAsync`
- Obsolete overload method `ITelegramBotClient.SendMediaGroupAsync`
- Obsolete constructor for `SendMediaGroupRequest`
- Obsolete constructor for `InputMediaPhoto`
- Obsolete constructor for `InputMediaVideo`
- Obsolete property `AllMembersAreAdministrators` from `Chat`
- Obsolete property `IsForwarded` from `Message`
- Obsolete value `Animation` from enum `MessageType`

## [15.7.1] - 2020-06-18

### Added

- Source Link support
- Fully deterministic build

## [15.7.0] - 2020-06-13

### Added

- Enum member `Emoji.Basketball`
- Property `InlineQueryResultGif.ThumbMimeType`
- Property `InlineQueryResultMpeg4Gif.ThumbMimeType`
- Property `Message.ViaBot`

## [15.6.0] - 2020-05-30

### Added

- Enum `Emoji`
- Property `Poll.Explanation`
- Property `Poll.ExplanationEntities`
- Property `Poll.OpenPeriod`
- Property `Poll.CloseDate`
- Property `Dice.Emoji`
- Following optional properties to `SendPollRequest`:
  - `Explanation`
  - `ExplanationParseMode`
  - `OpenPeriod`
  - `CloseDate`
- Optional property `Emoji` to `SendDiceRequest`

### Changed

- Following optional parameters to `ITelegramBotClient.SendPollAsync`:
  - `explanation`
  - `explanationParseMode`
  - `openPeriod`
  - `closeDate`
- Optional parameter `emoji` to `ITelegramBotClient.SendDiceAsync`

## [15.5.1] - 2020-04-02

### Fixed

- Implementation of `ITelegramBotClient.CreateNewAnimatedStickerSetAsync`

## [15.5.0] - 2020-04-02

### Added

- Requests
  - `GetMyCommandsRequest`
  - `SetMyCommandsRequest`
  - `CreateNewAnimatedStickerSetRequest`
  - `AddNewAnimatedStickerToSetRequest`
  - `SendDiceRequest`
  - `SetStickerSetThumbRequest`
- Methods:
  - `ITelegramBotClient.SendDiceAsync`
  - `ITelegramBotClient.CreateNewAnimatedStickerSetAsync`
  - `ITelegramBotClient.AddNewAnimatedStickerToSetAsync`
  - `ITelegramBotClient.SetStickerSetThumbAsync`
  - `ITelegramBotClient.GetMyCommandsAsync`
  - `ITelegramBotClient.SetMyCommandsAsync`
- Type `Dice`
- Type `BotCommand`
- Enum member `MessageType.Dice`
- Property `Message.Dice`
- Property `StickerSet.Thumb`

## [15.4.0] - 2020-02-22

### Added

- Property `SendInvoiceRequest.SendPhoneNumberToProvider`
- Property `SendInvoiceRequest.SendEmailToProvider`
- Optional parameter `sendPhoneNumberToProvider` to method `ITelegramBotClient.SendInvoiceAsync`
- Optional parameter `sendEmailToProvider` to method `ITelegramBotClient.SendInvoiceAsync`

## [15.3.0] - 2020-01-31

### Added

- Type `KeyboardButtonPollType`
- Static method `KeyboardButton.WithRequestPoll`
- Type `PollAnswer`
- Property `KeyboardButton.RequestPoll`
- Enum `PollType`
- Property `MessageEntity.Language`
- Following properties to type `Poll`:
  - `bool? IsAnonymous`
  - `string Type`
  - `bool? AllowsMultipleAnswers`
  - `int? CorrectOptionId`
- Following properties to type `SendPollRequest`:
  - `bool? IsAnonymous`
  - `string Type`
  - `bool? AllowsMultipleAnswers`
  - `int? CorrectOptionId`
  - `bool? IsClosed`
- Property `Update.PollAnswer`
- Enum member `UpdateType.PollAnswer`
- Following properties to type `User`:
  - `bool? CanJoinGroups`
  - `bool? CanReadAllGroupMessages`
  - `bool? SupportsInlineQueries`

### Changed

- Method `ITelegramBotClient.SendPollAsync`, added following optional parameters:
  - `bool? isAnonymous`
  - `string type`
  - `bool? allowsMultipleAnswers`
  - `int? correctOptionId`
  - `bool? isClosed`
- Method `User.Equals` takes into account new properties

## [15.2.1] - 2020-01-23

### Changed

- All base request classes use explicit opt-in serialization strategy
- All non Bot API properties in base request classes are annotated by `JsonIgnoreAttribute`
- Type `ContactRequestException` is made obsolete due to Telegram changed it's error message

### Fixed

- A bug that prevented requests to be serialized when default `JsonSerializerSettings` were set

## [15.2.0] - 2020-01-03

### Added

- Type `SetChatAdministratorCustomTitleRequest`
- Method `ITelegramBotClient.SetChatAdministratorCustomTitleAsync`
- Property `FileBase.FileUniqueId`
- Property `Animation.FileUniqueId`
- Property `ChatPhoto.BigFileUniqueId`
- Property `ChatPhoto.SmallFileUniqueId`
- Property `Chat.SlowModeDelay`
- Property `ChatMember.CustomTitle`
- Enum value `ParseMode.MarkdownV2`
- Enum value `MessageEntityType.Underline`
- Enum value `MessageEntityType.Strikethrough`

## [15.1.0] - 2019-11-29

### Added

- Property `RequestBase.IsWebhookResponse`
- The client outputs `method` property in the resulting HTTP body with the value from `IRequest<T>.MethodName` when `RequestBase.IsWebhookResponse` is set to true
- Constructor with `Uri` param for `InputOnlineFile` (overloaded)

## [15.0.0] - 2019-08-07

### Added

- Type `ChatPermissions`
- Type `SetChatPermissionsRequest`
- Property `Sticker.IsAnimated`
- Property `StickerSet.IsAnimated`
- Property `Chat.Permissions`
- Property `ChatMember.CanSendPolls`

### Changed

- Individual permission properties in `RestrictChatMemberRequest` changed to `Permissions` property of type `ChatPermissions`
- Individual permission parameters in `ITelegramBotClient.RestrictChatMemberAsync` changed to a single parameter of type `ChatPermissions`
- Marked `Chat.AllMembersAreAdministrators` as obsolete

### Fixed

- XML doc comments about caption maximum length

## [14.12.0] - 2019-06-10

### Added

- Type `LoginUrl`
- Property `Message.ReplyMarkup`
- Property `InlineKeyboardButton.LoginUrl`
- Method `InlineKeyboardButton.WithLoginUrl`

## [14.11.0] - 2019-04-23

### Added

- Type `Poll`
- Type `PollOption`
- Type `SendPollRequest`
- Type `StopPollRequest`
- Method `SendPollAsync`
- Method `StopPollAsync`
- Property `Update.Poll`
- Property `Message.Poll`
- Property `Message.ForwardSenderName`
- Property `ChatMember.IsMember`
- Enum value `UpdateType.Poll`
- Enum value `MessageType.Poll`

### Changed

- Marked `InvalidQueryIdException` as obsolete

## [14.10.0] - 2018-09-04

### Added

- Telegram Passport support
- Type `EncryptedCredentials`
- Type `EncryptedPassportElement`
- Type `PassportData`
- Type `PassportFile`
- Property `ITelegramBotClient.BotId`
- Property `Message.PassportData`

### Changed

- Marked `DownloadFileAsync(string,CancellationToken)` obsolete

## [14.9.0] - 2018-08-06

### Added

- Method `SendAnimationAsync`
- Parameter `thumb` to method `SendAudioAsync`
- Parameter `thumb` to method `SendDocumentAsync`
- Parameter `thumb` to method `SendVideoAsync`
- Parameter `thumb` to method `SendVideoNoteAsync`
- Type `IChatMessage`
- Type `IThumbMediaMessage`
- Type `SendAnimationRequest`
- Property `SendAudioRequest.Thumb`
- Property `SendDocumentRequest.Thumb`
- Property `SendVideoRequest.Thumb`
- Property `SendVideoNoteRequest.Thumb`
- Property `Audio.Thumb`

## [14.8.0] - 2018-07-29

### Added

- Parameter `vCard` to method `SendContactAsync`
- Parameter `foursquareType` to method `SendVenueAsync`
- Property `SendContactRequest.Vcard`
- Property `SendVenueRequest.FoursquareType`
- Property `InlineQueryResultContact.Vcard`
- Property `InlineQueryResultVenue.FoursquareType`
- Property `InputContactMessageContent.Vcard`
- Property `InputVenueMessageContent.FoursquareType`
- Property `Contact.Vcard`
- Property `Venue.FoursquareType`
- Enum value `MessageEntityType.Cashtag`

### Changed

- Marked `MessageType.Animation` as obsolete

## [14.7.0] - 2018-07-29

### Added

- Partial support for Bot API v4.0
- Method `SendMediaGroupAsync` (overloaded)
- Method `EditMessageMediaAsync`
- Method `EditMessageMediaAsync`
- Type `EditMessageMediaRequest`
- Type `EditInlineMessageMediaRequest`
- Type `InputMediaAudio`
- Type `InputMediaDocument`
- Type `InputMediaAnimation`
- Type `IInputMedia`
- Type `IAlbumInputMedia`
- Type `IInputMediaThumb`
- Property `Animation.Width`
- Property `Animation.Height`
- Property `Animation.Duration`
- Enum member `MessageType.Animation`
- Property `InputMediaVideo.Thumb`
- Property `Message.Animation`
- Constructor with required parameters for `InputMediaPhoto` (overloaded)
- Constructor with required parameters for `InputMediaVideo` (overloaded)
- Constructor with required parameters for `SendMediaGroupRequest` (overloaded)

### Fixed

- Serialization error of `InlineQueryResultDocument.Description` being required
- Incorrect serialization of `InputMediaVideo`

### Changed

- Marked method `SendMediaGroupAsync` as obsolete. An overload is provided.
- Marked parameterless constructor of `InputMediaPhoto` as obsolete. An overload is provided.
- Marked parameterless constructor of `InputMediaVideo` as obsolete. An overload is provided.

## [14.6.0] - 2018-06-12

### Added

- Property `ParseMode` to requests with a caption
  - `EditMessageCaptionRequest`
  - `EditInlineMessageCaptionRequest`
- Parameter `parseMode` to method `ITelegramBotClient.EditMessageCaptionAsync`

## [14.5.0] - 2018-06-06

### Added

- New enum member `MessageEntityType.PhoneNumber`
- New enum member `MessageEntityType.Unknown`

### Fixed

- Exception during deserialization of unknown message entity type

## [14.4.0] - 2018-05-17

### Changed

- `MakeRequestAsync` throws `ApiRequestException` with `ErrorCode = HttpStatusCode.Unauthorized` and `Message = apiResponse.Description` ("Unauthorized"), to be consistent with Telegram Bot API
- `TelegramBotClient` ctor does not check API token format: Telegram Bot API does not provide token format specification
- `TestApiAsync` return `false` when `ApiRequestException.ErrorCode == 401` (API Token is modified or recalled)
- Stop catching user exceptions from event handlers
- Make `Message.IsForward` property obsolete

### Fixed

- `EditMessageTextAsync` pass `ParseMode` to request

## [14.3.0] - 2018-05-05

### Added

- Implicit cast of `IEnumerable<InlineKeyboardButton>[]` to `InlineKeyboardMarkup`
- Implicit cast of `InlineKeyboardButton[]` to `InlineKeyboardMarkup`
- Exception `InvalidGameShortNameException`
- Exception `InvalidQueryIdException`

### Changed

- Made `callbackGame` parameter of `InlineKeyboardButton.WithCallBackGame` optional
- `Newtonsoft.Json` updated to version `11.0.2`

### Fixed

- Assigning param `foursquareId` of `SendInvoiceAsync` method to its request
- Access modifier of abstract class `BadRequestException` and `ForbiddenException` ctors to `protected`

### Removed

- Parameterless ctor of `InlineKeyboardMarkup`
- Exception `BotBlockedException`
- Exception `BotRestrictedException`
- Exception `MissingParameterException`
- Exception `NotEnoughRightsException`
- Exception `WrongChatTypeException`

## [14.2.0-rc-452] - 2018-02-24

### Added

- Property `Message.MediaGroupId`
- Property `ICaptionInlineQueryResult.ParseMode`
- Property `ParseMode` to inline query results with a caption
  - `InlineQueryResultPhoto`
  - `InlineQueryResultGif`
  - `InlineQueryResultCachedMpeg4Gif`
  - `InlineQueryResultVideo`
  - `InlineQueryResultAudio`
  - `InlineQueryResultVoice`
  - `InlineQueryResultDocument`
  - `InlineQueryResultCachedPhoto`
  - `InlineQueryResultCachedGif`
  - `InlineQueryResultCachedMpeg4Gif`
  - `InlineQueryResultCachedDocument`
  - `InlineQueryResultVideo`
  - `InlineQueryResultCachedVoice`
  - `InlineQueryResultCachedAudio`

## [14.1.0-rc-424] - 2018-02-24

### Added

- Support for Bot API v3.6
- Override equality comparison for `User` type
- Property `ParseMode` to file requests with a caption
  - `SendVideoRequest`
  - `SendPhotoRequest`
  - `SendAudioRequest`
  - `SendDocumentRequest`
  - `SendVoiceRequest`
- Property `InputMediaBase.ParseMode`
- Property `SendVideoRequest.SupportsStreaming`
- Property `InputMediaVideo.SupportsStreaming`
- Property `Message.ConnectedWebsite`
- Parameter `parseMode` to methods
  - `ITelegramBotClient.SendVideoAsync`
  - `ITelegramBotClient.SendAudioAsync`
  - `ITelegramBotClient.SendPhotoAsync`
  - `ITelegramBotClient.SendDocumentAsync`
  - `ITelegramBotClient.SendVoiceAsync`
- Parameter `supportsStreaming` to method `ITelegramBotClient.SendVideoAsync`
- New members to enum `MessageType`
  - `WebsiteConnected`
  - `ChatMembersAdded`
  - `ChatMemberLeft`
  - `ChatTitleChanged`
  - `ChatPhotoChanged`
  - `MessagePinned`
  - `ChatPhotoDeleted`
  - `GroupCreated`
  - `SupergroupCreated`
  - `ChannelCreated`
  - `MigratedToSupergroup`
  - `MigratedFromGroup`
- Exception `MessageIsNotModifiedException`

### Changed

- Changed `InputMessageContent` to abstract class `InputMessageContentBase`
- Access modifier of parameterless ctors of all `InlineQueryResult` and `InputMessageContent` types to `private`

### Fixed

- Renamed `InputVenueMessageContent.Name` to `InputVenueMessageContent.Title`
- Property `Message.Type` returns correct value after group chat migration

### Removed

- Enum member `MessageType.Service`

## [14.0.0-rc-367] - 2018-01-04

### Added

- Type `InvalidParameterException`
- Type `FileBase`

### Changed

- Moved all types in namespace `Telegram.Bot.Types.InputMessageContents` to namespace `Telegram.Bot.Types.InlineQueryResults`
- Value names of enums `MessageType` and `UpdateType`

### Removed

- Property `File.Stream`
- Property `FilePath` from Types `Audio`, `Document`, `PhotoSize`, `Sticker`, `Video`, `VideoNote`, and `Voice`
- Property `Message.NewChatMember`

## [14.0.0-beta-342] - 2018-01-03

### Added

- More `PaymentTests` cases
- Type `IKeyboardButton`
- Implicit cast of `string[]` to `ReplyKeyboardMarkup`
- Implicit cast of `string[][]` to `ReplyKeyboardMarkup`
- Inline query results
  - Type `ICaptionInlineQueryResult`
  - Type `IInputMessageContentResult`
  - Type `ILocationInlineQueryResult`
  - Type `IThumbnailInlineQueryResult`
  - Type `IThumbnailUrlInlineQueryResult`
  - Type `ITitleInlineQueryResult`
  - Constructor with required parameters in `InlineQueryResult` and all derived classes
  - Property `InlineQueryResultVoice.Caption`

### Changed

- All keyboard button classes inherit `IKeyboardButton` interface
- All keyboard buttons moved to namespace `Telegram.Bot.Types.ReplyMarkups.Buttons`
- Renamed type `ReplyMarkup` to `ReplyMarkupBase`
- Renamed type `ForceReply` to `ForceReplyMarkup`
- `InlineQueryResult` to abstract

### Removed

- All specific `KeyboardButton` types
- All specific `InlineKeyboardButton` types
- Implicit cast of `InlineKeyboardButton` to `KeyboardButton`
- Type `InlineQueryResultNew`
- Type `InlineQueryResultCached`
- Property `InlineQueryResult.Title`
- Property `InlineQueryResult.InputMessageContent`
- Property `InlineQueryResultAudio.FileId`
- JSON serialization attribute `Required.Always` of property `InlineQueryResultAudio.Duration`

### Fixed

- Invalid default value for `SwitchInlineQueryCurrentChat` in `InlineKeyboardSwitchInlineQueryCurrentChatButton` constructor

## [14.0.0-alpha0] - 2017-12-31

### Added

- Request classes
  - Type `GetUpdatesRequest`
  - Type `SetWebhookRequest`
  - Type `DeleteWebhookRequest`
  - Type `GetWebhookInfoRequest`
  - Type `GetMeRequest`
  - Type `SendMessageRequest`
  - Type `ForwardMessageRequest`
  - Type `SendPhotoRequest`
  - Type `SendAudioRequest`
  - Type `SendDocumentRequest`
  - Type `SendVideoRequest`
  - Type `SendVoiceRequest`
  - Type `SendVideoNoteRequest`
  - Type `SendLocationRequest`
  - Type `EditMessageLiveLocationRequest`
  - Type `EditInlineMessageLiveLocationRequest`
  - Type `StopMessageLiveLocationRequest`
  - Type `StopInlineMessageLiveLocationRequest`
  - Type `SendVenueRequest`
  - Type `SendContactRequest`
  - Type `SendChatActionRequest`
  - Type `GetUserProfilePicturesRequest`
  - Type `KickChatMemberRequest`
  - Type `UnbanChatMemberRequest`
  - Type `RestrictChatMemberRequest`
  - Type `PromoteChatMemberRequest`
  - Type `ExportChatInviteLinkRequest`
  - Type `SetChatPhotoRequest`
  - Type `DeleteChatPhotoRequest`
  - Type `SetChatTitleRequest`
  - Type `SetChatDescriptionRequest`
  - Type `PinChatMessageRequest`
  - Type `UnpinChatMessageRequest`
  - Type `LeaveChatRequest`
  - Type `GetChatRequest`
  - Type `GetChatAdministratorsRequest`
  - Type `GetChatMembersCountRequest`
  - Type `GetChatMemberRequest`
  - Type `SetChatStickerSetRequest`
  - Type `DeleteChatStickerSetRequest`
  - Type `AnswerCallbackQueryRequest`
  - Type `EditMessageTextRequest`
  - Type `EditInlineMessageTextRequest`
  - Type `EditMessageCaptionRequest`
  - Type `EditInlineMessageCaptionRequest`
  - Type `EditMessageReplyMarkupRequest`
  - Type `EditInlineMessageReplyMarkupRequest`
  - Type `DeleteMessageRequest`
  - Type `SendStickerRequest`
  - Type `GetStickerSetRequest`
  - Type `SetStickerPositionInSetRequest`
  - Type `AnswerInlineQueryRequest`
  - Type `DeleteStickerFromSetRequest`
  - Type `AddStickerToSetRequest`
  - Type `CreateNewStickerSetRequest`
  - Type `SendInvoiceRequest`
  - Type `AnswerShippingQueryRequest`
  - Type `AnswerPreCheckoutQueryRequest`
  - Type `SendGameRequest`
  - Type `SetGameScoreRequest`
  - Type `SetInlineGameScoreRequest`
  - Type `GetGameHighScoresRequest`
  - Type `GetInlineGameHighScoresRequest`
  - Type `GetFileRequest`
  - Type `FileRequestBase`
  - Type `UploadStickerFileRequest`
  - Type `INotifiableMessage`
  - Type `IReplyMessage`
  - Type `IFormattableMessage`
  - Type `IInlineMessage`
  - Type `IReplyMarkupMessage`
  - Type `IInlineReplyMarkupMessage`
- Type `IInputFile`
- Type `InputFileStream`
- Type `InputTelegramFile`
- Type `InputOnlineFile`
- Type `InputFileConverter`

### Changed

- Type of parameter `allowedUpdates` in method `SetWebhookAsync` changed to `IEnumerable<UpdateType>`
- Type of parameter `allowedUpdates` in method `GetUpdatesAsync` changed to `IEnumerable<UpdateType>`
- Type of parameter `offset` in method `GetUserProfilePhotosAsync` changed to `int`
- Type of parameter `replyMarkup` in methods `EditMessageLiveLocationAsync` changed to `InlineKeyboardMarkup`
- Type of parameter `replyMarkup` in methods `StopMessageLiveLocationAsync` changed to `InlineKeyboardMarkup`
- Type of parameter `replyMarkup` in method `EditMessageTextAsync` changed to `InlineKeyboardMarkup`
- Type of parameter `replyMarkup` in method `EditMessageCaptionAsync` changed to `InlineKeyboardMarkup`
- Type of parameter `replyMarkup` in method `EditMessageReplyMarkupAsync` changed to `InlineKeyboardMarkup`
- Type of parameter `replyMarkup` in method `SendGameAsync` changed to `InlineKeyboardMarkup`
- Type of parameter `replyMarkup` in method `SendInvoiceAsync` changed to `InlineKeyboardMarkup`
- Type of parameter `prices` in method `SendInvoiceAsync` changed to `IEnumerable<LabeledPrice>`
- Type of parameter `shippingOptions` in method `AnswerShippingQueryAsync` changed to `IEnumerable<ShippingOption>`
- Type of parameter `chatId` in method `SendInvoiceAsync` changed to `int`
- Type of parameter `chatId` in method `SendGameAsync` changed to `long`
- Type of parameter `chatId` in methods `SetGameScoreAsync` changed to `long`
- Type of parameter `chatId` in methods `GetGameHighScoresAsync` changed to `long`
- Type parameter of type `SendMediaGroupRequest` changed to `Message[]`
- Return type of method `GetChatIdFromTesterAsync` in `TestFixture` changed to `Task<long>`
- Type of property `TesterPrivateChatId` on type `PaymentTestsFixture` changed to `long`
- Replace method `EditInlineMessageTextAsync` with overload for `EditMessageTextAsync`
- Replace method `EditInlineMessageCaptionAsync` with overload for `EditMessageCaptionAsync`
- Replace method `EditInlineMessageReplyMarkupAsync` with overload for `EditMessageReplyMarkupAsync`
- Reorder parameters of method `SendInvoiceAsync`
- Divide `AnswerShippingQueryAsync` method into two overloads
- Divide `AnswerPreCheckoutQueryAsync` method into two overloads
- Method `CreateNewStickerSetAsync` renamed to `CreateNewStickerSetAsync`
- Method return type changed from `Task<bool>` to `Task`:
  - Method `DeleteWebhookAsync`
  - Method `KickChatMemberAsync`
  - Method `LeaveChatAsync`
  - Method `UnbanChatMemberAsync`
  - Method `AnswerCallbackQueryAsync`
  - Method `RestrictChatMemberAsync`
  - Method `PromoteChatMemberAsync`
  - Method `StopMessageLiveLocationAsync`
  - Method `EditMessageTextAsync`
  - Method `EditMessageCaptionAsync`
  - Method `EditMessageReplyMarkupAsync`
  - Method `EditMessageLiveLocationAsync`
  - Method `DeleteMessageAsync`
  - Method `AnswerInlineQueryAsync`
  - Method `AnswerShippingQueryAsync`
  - Method `AnswerPreCheckoutQueryAsync`
  - Method `SetGameScoreAsync`
  - Method `CreateNewStickerSetAsync`
  - Method `AddStickerToSetAsync`
  - Method `SetStickerPositionInSetAsync`
  - Method `DeleteStickerFromSetAsync`
  - Method `SetChatPhotoAsync`
  - Method `DeleteChatPhotoAsync`
  - Method `SetChatTitleAsync`
  - Method `SetChatDescriptionAsync`
  - Method `PinChatMessageAsync`
  - Method `UnpinChatMessageAsync`
  - Method `SetChatStickerSetAsync`
  - Method `DeleteChatStickerSetAsync`
- Type of `StickerSet.Stickers` from `List<Sticker>` to `Sticker[]`
- Type of `ChatMember` properties to nullable e.g. `bool` to `bool?`
- Type of `Message.Entities` from `List<MessageEntity>` to `MessageEntity[]`
- Type of `Message.CaptionEntities` from `List<MessageEntity>` to `MessageEntity[]`
- Type of `Message.EntityValues` from `List<string>` to `IEnumerable<string>`
- Type `InputMediaType` to `InputMedia`
- Type `InputMediaTypeConverter` to `InputMediaConverter`
- Changed member type from `FileToSend` to an implementation of `IInputFile`
  - Parameter `certificate` of method `SetWebhookAsync`
  - Parameter `photo` of method `SendPhotoAsync`
  - Parameter `video` of method `SendVideoAsync`
  - Parameter `videoNote` of method `SendVideoNoteAsync`
  - Parameter `document` of method `SendDocumentAsync`
  - Parameter `sticker` of method `SendStickerAsync`
  - Parameter `pngSticker` of method `UploadStickerFileAsync`
  - Parameter `pngSticker` of method `CreateNewStickerSetAsync`
  - Parameter `pngSticker` of method `AddStickerToSetAsync`
  - Parameter `photo` of method `SetChatPhotoAsync`
  - Parameter `audio` of method `SendAudioAsync`

### Removed

- Type `FileToSend`
- Interface `IResponse`
- Type parameter constraint from interface `IRequest`
- Redundant parameter `editMessage` in methods `SetGameScoreAsync`
- Redundant custom converter `ParseModeConverter`
- Value `All` from `UpdateType` enum
- Value `Unknown` from `FileType` enum
- Default value of parameter `url` of method `SetWebhookAsync`
- Property `Width` of `VideoNote`
- Property `Height` of `VideoNote`
- Property `Zoom` of `MaskPosition`

### Fixed

- Method `SetWebhookAsync` interface `ITelegramBotClient` returns `bool` on success
- Passing wrong `chatId` value in method `GetGameHighScoresAsync`
- Passing wrong `chatId` value in method `SendGameAsync`
- Passing wrong `chatId` value in method `SendInvoiceAsync`
- Passing wrong `replyMarkup` value in method `SendGameAsync`
- Passing wrong `replyMarkup` value in method `SendInvoiceAsync`
- Passing wrong `replyMarkup` value in method `EditMessageLiveLocationAsync`
- Passing wrong `replyMarkup` value in method `EditMessageReplyMarkupAsync`
- Passing wrong `replyMarkup` value in method `EditMessageCaptionAsync`
- Passing wrong `replyMarkup` value in method `EditMessageTextAsync`
- Passing wrong `replyMarkup` value in method `StopMessageLiveLocationAsync`
- Passing wrong `pngSticker` value in method `UploadStickerFileAsync`
- Passing wrong `photo` value in method `SetChatPhotoAsync`
- Passing wrong `certificate` value in method `SetWebhookAsync`
- Incorrect spelling of the method `CreateNewStickerSetAsync`
- `TestApiAsync()` throws exception instead of returning `false`

## [13.4.0] - 2017-12-07

### Added

- `Stickers` test cases
- `StickerOwnerUserId` parameter to Systems Integration Tests settings
- Exception `ChatNotFoundException`
- Exception `ContactRequestException`
- Exception `InvalidUserIdException`
- Exception `UserNotFoundException`
- Exception `InvalidStickerSetNameException`
- Exception `InvalidStickerEmojisException`
- Exception `InvalidStickerDimensionsException`
- Exception `StickerSetNameExistsException`
- Exception `StickerSetNotModifiedException`

### Changed

- Include XML docs in NuGet package

## [13.4.0-rc2] - 2017-11-26

### Fixed

- Default snake-cased property name serialization

## [13.4.0-rc1] - 2017-11-26

### Added

- Support for Bot API 3.5
- Method `SendMediaGroupAsync`
- Types `InputFileBase` and `InputMediaBase`
- Test Collection `AlbumMessageTests`
- Test Collection `ChannelAdminBotTests`

## [13.3.0-rc1]

> ToDo

## [13.2.2-rc]

> ToDo

## [13.2.1] - 2017-09-04

### Added

- Systems integrations test project

### Changed

- Sandcastle project moved to `docs` branch
- TravisCI configurations to run systems integrations tests

### Fixed

- Quoting the string containing channel ids
- Double escaping the escape character in the caption of messages
- Serialization errors of `PhotoSize`
- Sending wrong filenames containing non-ASCII characters
- Deserialization of `PhotoSize`

## [13.1.0] - 2017-07-23

### Added

- Method `KickChatMemberAsync` parameter `untilDate`
- Method `RestrictChatMemberAsync`
- Method `PromoteChatMemberAsync`
- Method `ExportChatInviteLinkAsync`
- Method `SetChatPhotoAsync`
- Method `DeleteChatPhotoAsync`
- Method `SetChatTitleAsync`
- Method `SetChatDescriptionAsync`
- Method `PinChatMessageAsync`
- Method `UnpinChatMessageAsync`
- Method `SendVideoNoteAsync` parameter `length`
- Method `GetStickerSetAsync`
- Method `UploadStickerFileAsync`
- Method `CreateNewStickerSetAsync`
- Method `AddStickerToSetAsync`
- Method `SetStickerPositionInSetAsync`
- Method `DeleteStickerFromSetAsync`
- Type `VideoNote` property `Length`
- Type `Chat`properties `Photo`, `Description`, `InviteLink`
- Type `ChatMember` properties `UntilDate`, `Can*`
- Type `Sticker` properties `SetName` and `MaskPosition`
- Type `MaskPosition`
- Type `StickerSet`
- Type `ChatPhoto`
- Types `InlineKeyboard*Button`
- Enum `MaskPositionPoint`

### Changed

- User and Chat Ids reverted to base types
- DateTimes are now in local time zone
- Split Keyboard buttons to `InlineKeyboardCallbackButton`, `InlineKeyboardCallbackGameButton`, `InlineKeyboardPayButton`, `InlineKeyboardSwitchCallbackQueryCurrentButton`, `InlineKeyboardSwitchInlineQueryButton` and `InlineKeyboardUrlButton`

### Fixed

- Inline message editing
- InlineQueryResult* `ThumbHight` and `ThumbWidth`
- Method `SetWebHookAsync` parameter `max_connections`
- Method `SetGameStoreAsync`
- Type `CallbackQuery` Property `Data` optimal
- Type `Message` can now be a `VideoNoteMessage`

## [12.0.0] - Beta only

### Added

- Method `DeleteMessageAsync`
- Method `SendVideoNoteAsync`
- Method `SendInvoiceAsync`
- Method `AnswerShippingQueryAsync`
- Method `AnswerPreCheckoutQueryAsync`
- Type `Invoice`
- Type `LabeledPrice`
- Type `ShippingAddress`
- Type `ShippingOption`
- Type `ShippingQuery`
- Type `SuccessfulPayment`
- Type `OrderInfo`
- Type `PreCheckoutQuery`
- Type `VideoNote`
- Type `Message` properties `VideoNote`, `Invoice` and `SuccessfulPayment`
- Type `User` property `LanguageCode`
- Type `Update` properties `ShippingQuery` and `PreCheckoutQuery`
- Type `InlineQueryResultGif` property `Duration`
- Type `InlineQueryResultMpeg4Gif` property `Duration`
- Type `InlineKeyboardButton` property `Pay`
- Enum `ChatAction` members `RecordVideoNote` and `UploadVideoNote`
- Enum `UpdateType` members `ShippingQuery` and `PreCheckoutQuery`

### Changed

- Property `NewChatMember` replaced with `NewChatMembers` on Type `Message`

## [Past]

### Added

- Method `SetWebHookAsync` parameters `maxConnections`, `allowedUpdates`
- Method `AnswerCallbackQueryAsync` parameter `cacheTime`
- Method `StartReceiving` parameter `allowedUpdates`
- Method `DeleteWebhookAsync`
- Method `GetWebhookInfoAsync`
- Type `ApiExceptions` property `Parameters`
- Type `InlineKeyboardButton` property `SwitchInlineQueryCurrentChat`
- Type `ResponseParameters`
- Type `WebhookInfo`
- Type `ChatId`
- Type `Chat` property `AllMembersAreAdministrators`
- Type `Message`property `ForwardFromMessageId`
- Type `Update` property `ChannelPost`, `EditedChannelPost`
- Event `OnReceiveGeneralError`
- Enum `UpdateType` member `ChannelPost`, `EditedChannelPost`
- Enum `FileType`
- Game Support
  - Method `SetGameScoreAsync`
  - Method `SendGameAsync`
  - Method `GetGameHighScoresAsync`
  - Method `AnswerCallbackQueryAsync` parameter `url`
  - Type `Animation`
  - Type `CallbackGame`
  - Type `CallbackQuery` properties `ChatInstance`, `GameShortName`
  - Type `GameHighScore`
  - Type `InlineKeyboardButton` property `CallbackGame`
  - Type `InlineQueryResults`
  - Type `Message` property `Game`
  - Enum `InlineQueryResultType` member `Game`
  - Enum `MessageType` member `Game`

### Changed

- Now the `HttpClient` will be reused
- Consolidated timeouts
- To use a proxy, use the constructor
- Unified the `chatId` parameters
- Replaced `ReplyKeyboardHide` with `ReplyKeyboardRemove`
- Replaced all file sending overloads with `FileToSend`

### Removed

- Removed deprecated API class
- Removed deprecated methods and events
- Removed `StartReceiving` overload with `timeout` parameter
