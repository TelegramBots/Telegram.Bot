# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).

## [Unreleased]

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
  - Type `UnbanChatmemberRequest`
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

### Changed

- Type of parameter `certificate` in method `SetWebhookAsync` changed to `Stream`
- Type of parameter `allowedUpdates` in method `SetWebhookAsync` changed to `IEnumerable<UpdateType>`
- Type of parameter `allowedUpdates` in method `GetUpdatesAsync` changed to `IEnumerable<UpdateType>`
- Type of parameter `offset` in method `GetUserProfilePhotosAsync` changed to `int`
- Type of parameter `photo` in method `SetChatPhotoAsync` changed to `Stream`
- Type of parameter `replyMarkup` in methods `EditMessageLiveLocationAsync` changed to `InlineKeyboardMarkup`
- Type of parameter `replyMarkup` in methods `StopMessageLiveLocationAsync` changed to `InlineKeyboardMarkup`
- Type of parameter `replyMarkup` in method `EditMessageTextAsync` changed to `InlineKeyboardMarkup`
- Type of parameter `replyMarkup` in method `EditMessageCaptionAsync` changed to `InlineKeyboardMarkup`
- Type of parameter `replyMarkup` in method `EditMessageReplyMarkupAsync` changed to `InlineKeyboardMarkup`
- Type of parameter `replyMarkup` in method `SendGameAsync` changed to `InlineKeyboardMarkup`
- Type of parameter `replyMarkup` in method `SendInvoiceAsync` changed to `InlineKeyboardMarkup`
- Type of parameter `prices` in method `SendInvoiceAsync` changed to `IEnumerable<LabeledPrice>`
- Type of parameter `shippingOptions` in method `AnswerShippingQueryAsync` changed to `IEnumerable<ShippingOption>`
- Type of parameter `chatId` in method `SendInvoiceAsync` changed to `long`
- Type of parameter `chatId` in method `SendGameAsync` changed to `long`
- Type of parameter `chatId` in methods `SetGameScoreAsync` changed to `long`
- Type of parameter `chatId` in methods `GetGameHighScoresAsync` changed to `long`
- Default value of parameter `maxConnection` in method `SetWebhookAsync` changed to `default` keyword
- Default value of parameter `limit` in method `GetUpdatesAsync` changed to `default` keyword
- Default value of parameter `parseMode` in method `SendTextMessageAsync` changed to `default` keyword
- Default value of parameter `caption` in method `SendVideoAsync` changed to `default` keyword
- Default value of parameter `caption` in method `SendDocumentAsync` changed to `default` keyword
- Default value of parameter `caption` in method `SendPhotoAsync` changed to `default` keyword
- Default value of parameter `caption` in method `SendVoiceAsync` changed to `default` keyword
- Default value of parameter `limit` in method `GetUserProfilePhotosAsync` changed to `default` keyword
- Default value of parameter `parseMode` in method `EditMessageTextAsync` changed to `default` keyword
- Default value of parameter `pngSticker` in method `UploadStickerFileAsync` changed to `Stream`
- Literal default values in interface `ITelegramBotClient` replaced with `default` keyword
- Type parameter of type `SendMediaGroupRequest` changed to `Message[]`
- Return type of method `GetChatIdFromTesterAsync` in `TestFixture` changed to `Task<long>`
- Type of propery `TesterPrivateChatId` on type `PaymentTestsFixture` changed to `long`
- Replace method `EditInlineMessageTextAsync` with overload for `EditMessageTextAsync`
- Replace method `EditInlineMessageCaptionAsync` with overload for `EditMessageCaptionAsync`
- Replace method `EditInlineMessageReplyMarkupAsync` with overload for `EditMessageReplyMarkupAsync`
- Request methods in `TelegramBotClient` annotated as `async`
- Method `CreateNewStickerSetAsnyc` renamed to `CreateNewStickerSetAsync`
- Type `ITelegramBotClient` method `DeleteWebhookAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `KickChatMemberAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `LeaveChatAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `UnbanChatMemberAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `AnswerCallbackQueryAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `RestrictChatMemberAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `PromoteChatMemberAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `StopMessageLiveLocationAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `EditMessageTextAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `EditMessageCaptionAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `EditMessageReplyMarkupAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `EditMessageLiveLocationAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `DeleteMessageAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `AnswerInlineQueryAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `AnswerShippingQueryAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `AnswerPreCheckoutQueryAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `SetGameScoreAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `CreateNewStickerSetAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `AddStickerToSetAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `SetStickerPositionInSetAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `DeleteStickerFromSetAsync` return type changedfrom `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `SetChatPhotoAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `DeleteChatPhotoAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `SetChatTitleAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `SetChatDescriptionAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `PinChatMessageAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `UnpinChatMessageAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `SetChatStickerSetAsync` return type changed from `Task<bool>` to `Task`
- Type `ITelegramBotClient` method `DeleteChatStickerSetAsync` return type changed from `Task<bool>` to `Task`

### Removed

- Interface `IResponse`
- Type parameter constraint from interface `IRequest`
- Redundant parameter `editMessage` in methods `SetGameScoreAsync`
- Redundant custom converter `ParseModeConverter`
- Value `All` from `UpdateType` enum
- Default value of parameter `url` of mehtod `SetWebhookAsync`

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
- `TestApiAsync()` throws exception instead of returing `false`

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
- Splitedd Keyboardbuttons in `InlineKeyboardCallbackButton`, `InlineKeyboardCallbackGameButton`, `InlineKeyboardPayButton`, `InlineKeyboardSwitchCallbackQueryCurrentButton`, `InlineKeyboardSwitchInlineQueryButton` and `InlineKeyboardUrlButton`

### Fixed

- Inline messge editing
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
- Type `User` poperty `LanguageCode`
- Type `Update` properties `ShippingQuery` and `PreCheckoutQuery`
- Type `InlineQueryResultGif` porperty `Duration`
- Type `InlineQueryResultMpeg4Gif` porperty `Duration`
- Type `InlineeyboardButton` property `Pay`
- Enum `ChatAction` members `RecordVideoNote` and `UplaodVideoNote`
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
  - Type `CallBackGame`
  - Type `CallBackquery` properties `ChatInstance`, `GameShortName`
  - Type `GameHighScore`
  - Type `InlineKeyboardButton` propertiy `CallbackGame`
  - Type `InlineQueryResults`
  - Type `Message` property `Game`
  - Enum `InlineQueryResultType` member `Game`
  - Enum `MesageType` member `Game`

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
