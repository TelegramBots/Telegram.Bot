# Change Log

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).

## [12.0.0] - 2017-07-10

### Added

- Integration Tests
- Method `DeleteMessageAsync`
- Method `SendVideoNoteAsync`
- Method `SendInvoiceAsync`
- Method `AnswerShippingQueryAsync`
- Method `AnswerPreCheckoutQueryAsync`
- Method `RestrictChatMemberAsync`
- Method `PromoteChatMemberAsync`
- Method `ExportChatInviteLinkAsync`
- Method `SetChatPhotoAsync`
- Method `DeleteChatPhotoAsync`
- Method `SetChatTitle`
- Method `SetChatDescriptionAsync`
- Method `PinChatMessageAsync`
- Method `UnpinChatMessageAsync`
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
- Type `Chat` properties `ChatPhoto`, `Description`, and `InviteLink`
- Type `ChatMember` properties `UntilDate`, `CanBeEdited`, `CanChangeInfo`, `CanPostMessages`, `CanEditMessages`, `CanDeleteMessages`, `CanInviteUsers`, `CanRestrictMembers`, `CanPinMessages`, `CanPromoteMembers`, `CanSendMessages`, `CanSendMediaMessages`, `CanSendOtherMessages`, and `CanAddWebPagePreviews`
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
