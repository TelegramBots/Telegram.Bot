# Change Log
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/)
and this project adheres to [Semantic Versioning](http://semver.org/).

## [12.0.0] - Upcomming
### Added
- Method `DeleteMessage`
- Method `SendVideoNote`
- Type `VideoNote`
- Type `Message` property `VideoNote`
- Type `User` poperty `LanguageCode`
- Type `InlineQueryResultGif` porperty `Duration`
- Type `InlineQueryResultMpeg4Gif` porperty `Duration`
- Enum `ChatAction` members `RecordVideoNote` and `UplaodVideoNote`
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
