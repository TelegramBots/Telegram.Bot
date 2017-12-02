# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).

## [Unreleased]

### Added

- `Stickers` test cases
- `StickerOwnerUserId` parameter to Systems Integration Tests configs
- Exception `InvalidStickerSetNameException`
- Exception `InvalidStickerEmojisException`
- Exception `InvalidStickerDimensionsException`
- Exception `StickerSetNameExistsException`

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
