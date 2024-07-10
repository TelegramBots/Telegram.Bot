using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types
{
    public partial class Message
    {
        /// <summary>
        /// <em>Optional</em>. For forwarded messages, sender of the original message
        /// </summary>
        [JsonIgnore]
        public User? ForwardFrom => (ForwardOrigin as MessageOriginUser)?.SenderUser;

        /// <summary>
        /// <em>Optional</em>. For messages forwarded from channels or from anonymous administrators, information about the
        /// original sender chat
        /// </summary>
        [JsonIgnore]
        public Chat? ForwardFromChat => ForwardOrigin switch
        {
            MessageOriginChannel originChannel => originChannel.Chat,
            MessageOriginChat originChat => originChat.SenderChat,
            _ => null,
        };

        /// <summary>
        /// <em>Optional</em>. For messages forwarded from channels, identifier of the original message in the channel
        /// </summary>
        [JsonIgnore]
        public int? ForwardFromMessageId => (ForwardOrigin as MessageOriginChannel)?.MessageId;

        /// <summary>
        /// <em>Optional</em>. For messages forwarded from channels, signature of the post author if present
        /// </summary>
        [JsonIgnore]
        public string? ForwardSignature => (ForwardOrigin as MessageOriginChannel)?.AuthorSignature;

        /// <summary>
        /// <em>Optional</em>. Sender's name for messages forwarded from users who disallow adding a link to their account in
        /// forwarded messages
        /// </summary>
        [JsonIgnore]
        public string? ForwardSenderName => (ForwardOrigin as MessageOriginHiddenUser)?.SenderUserName;

        /// <summary>
        /// <em>Optional</em>. For forwarded messages, date the original message was sent
        /// </summary>
        [JsonIgnore]
        public DateTime? ForwardDate => ForwardOrigin?.Date;

        /// <summary>
        /// Gets the entity values.
        /// </summary>
        /// <value>
        /// The entity contents.
        /// </value>
        [JsonIgnore]
        public IEnumerable<string>? EntityValues =>
            Text is null
                ? default
                : Entities?.Select(entity => Text.Substring(entity.Offset, entity.Length));

        /// <summary>
        /// Gets the caption entity values.
        /// </summary>
        /// <value>
        /// The caption entity contents.
        /// </value>
        [JsonIgnore]
        public IEnumerable<string>? CaptionEntityValues =>
            Caption is null
                ? default
                : CaptionEntities?.Select(entity => Caption.Substring(entity.Offset, entity.Length));
    }

    public partial class Chat
    {
        /// <inheritdoc/>
        public override string ToString() => Type switch
        {
            ChatType.Private => Username != null ? $"Private chat with @{Username} ({Id})" : $"Private chat with {FirstName}{LastName?.Insert(0, " ")} ({Id})",
            _ => $"{Type} \"{Title}\" ({Id})"
        };
    }

    public partial class User
    {
        /// <inheritdoc/>
        public override string ToString() =>
            Username != null ? $"@{Username} ({Id})" : $"{FirstName}{LastName?.Insert(0, " ")} ({Id})";
    }

    public partial class ReplyParameters
    {
        /// <summary>Implicit operator when you just want to reply to a message in same chat</summary>
        public static implicit operator ReplyParameters(int replyToMessageId) => new() { MessageId = replyToMessageId };
        /// <summary>Implicit operator when you just want to reply to a message in same chat</summary>
        public static implicit operator ReplyParameters(Message msg) => new() { MessageId = msg.MessageId };
    }

    public partial class MessageId
    {
        /// <summary>Implicit operator to int</summary>
        public static implicit operator int(MessageId msgID) => msgID.Id;
        /// <summary>Implicit operator from int</summary>
        public static implicit operator MessageId(int id) => new() { Id = id };
        /// <summary>Implicit operator from Message</summary>
        public static implicit operator MessageId(Message msg) => new() { Id = msg.MessageId };
    }

    public abstract partial class ReactionType
    {
        /// <summary>Implicit operator ReactionTypeEmoji from string</summary>
        public static implicit operator ReactionType(string emoji) => new ReactionTypeEmoji { Emoji = emoji };
        /// <summary>Implicit operator ReactionTypeCustomEmoji from long customEmojiId</summary>
        public static implicit operator ReactionType(long customEmojiId) => new ReactionTypeCustomEmoji { CustomEmojiId = customEmojiId.ToString(CultureInfo.InvariantCulture) };
    }

    public partial class LinkPreviewOptions
    {
        /// <summary>To get the same behaviour as previous parameter <c>disableWebPagePreview:</c></summary>
        public static implicit operator LinkPreviewOptions(bool disabled) => new() { IsDisabled = disabled };
    }

    public partial class BotName
    {
        /// <summary>implicit to string</summary>
        public static implicit operator string(BotName bn) => bn.Name;
        /// <summary>implicit from string</summary>
        public static implicit operator BotName(string bn) => new() { Name = bn };
    }

    public partial class BotShortDescription
    {
        /// <summary>implicit to string</summary>
        public static implicit operator string(BotShortDescription bsd) => bsd.ShortDescription;
        /// <summary>implicit from string</summary>
        public static implicit operator BotShortDescription(string bsd) => new() { ShortDescription = bsd };
    }
    
    public partial class BotDescription
    {
        /// <summary>implicit to string</summary>
        public static implicit operator string(BotDescription bd) => bd.Description;
        /// <summary>implicit from string</summary>
        public static implicit operator BotDescription(string bd) => new() { Description = bd };
    }

    public partial class WebAppInfo
    {
        /// <summary>implicit to string (URL)</summary>
        public static implicit operator string(WebAppInfo wai) => wai.Url;
        /// <summary>implicit from string (URL)</summary>
        public static implicit operator WebAppInfo(string url) => new() { Url = url };
    }

    public partial class InputPollOption
    {
        /// <summary>Implicit operator for compatibility</summary>
        public static implicit operator InputPollOption(string text) => new() { Text = text };
    }

    public partial class BotCommandScope
    {
        /// <summary>
        /// Create a default <see cref="BotCommandScope"/> instance
        /// </summary>
        /// <returns></returns>
        public static BotCommandScopeDefault Default() => new();

        /// <summary>
        /// Create a <see cref="BotCommandScope"/> instance for all private chats
        /// </summary>
        /// <returns></returns>
        public static BotCommandScopeAllPrivateChats AllPrivateChats() => new();

        /// <summary>
        /// Create a <see cref="BotCommandScope"/> instance for all group chats
        /// </summary>
        public static BotCommandScopeAllGroupChats AllGroupChats() => new();

        /// <summary>
        /// Create a <see cref="BotCommandScope"/> instance for all chat administrators
        /// </summary>
        public static BotCommandScopeAllChatAdministrators AllChatAdministrators() =>
            new();

        /// <summary>
        /// Create a <see cref="BotCommandScope"/> instance for a specific <see cref="Chat"/>
        /// </summary>
        /// <param name="chatId">
        /// Unique identifier for the target <see cref="Chat"/> or username of the target supergroup
        /// </param>
        public static BotCommandScopeChat Chat(ChatId chatId) => new() { ChatId = chatId };

        /// <summary>
        /// Create a <see cref="BotCommandScope"/> instance for a specific member in a specific <see cref="Chat"/>
        /// </summary>
        /// <param name="chatId">
        /// Unique identifier for the target <see cref="Chat"/> or username of the target supergroup
        /// </param>
        public static BotCommandScopeChatAdministrators ChatAdministrators(ChatId chatId) =>
            new() { ChatId = chatId };

        /// <summary>
        /// Represents the <see cref="BotCommandScope">scope</see> of bot commands, covering a specific member of a group or supergroup chat.
        /// </summary>
        /// <param name="chatId">
        /// Unique identifier for the target <see cref="Chat"/> or username of the target supergroup
        /// </param>
        /// <param name="userId">Unique identifier of the target user</param>
        public static BotCommandScopeChatMember ChatMember(ChatId chatId, long userId) =>
            new() { ChatId = chatId, UserId = userId };
    }

    public partial class CallbackQuery
    {
        /// <summary>
        /// Indicates if the User requests a Game
        /// </summary>
        [JsonIgnore]
        public bool IsGameQuery => GameShortName != default;
    }

    namespace ReplyMarkups
    {
        public partial class ReplyKeyboardMarkup
        {
            /// <summary>Initializes a new instance of <see cref="ReplyKeyboardMarkup"/> with one button</summary>
            /// <param name="button">Button or text on keyboard</param>
            [SetsRequiredMembers]
            public ReplyKeyboardMarkup(KeyboardButton button) : this(new List<List<KeyboardButton>> { new() { button } }) { }

            /// <summary>Initializes a new instance of <see cref="ReplyKeyboardMarkup"/></summary>
            /// <param name="keyboardRow">The keyboard row.</param>
            [SetsRequiredMembers]
            public ReplyKeyboardMarkup(IEnumerable<KeyboardButton> keyboardRow) : this(new List<List<KeyboardButton>> { keyboardRow.ToList() }) { }

#pragma warning disable MA0016 // Prefer using collection abstraction instead of implementation
            /// <summary>Initializes a new instance of <see cref="ReplyKeyboardMarkup"/></summary>
            /// <param name="keyboardRow">The keyboard row.</param>
            [SetsRequiredMembers]
            public ReplyKeyboardMarkup(List<KeyboardButton> keyboardRow) : this(new List<List<KeyboardButton>> { keyboardRow }) { }
#pragma warning restore MA0016 // Prefer using collection abstraction instead of implementation

            /// <summary>Initializes a new instance of <see cref="ReplyKeyboardMarkup"/></summary>
            /// <param name="keyboardRow">A row of buttons or texts.</param>
            [SetsRequiredMembers]
            public ReplyKeyboardMarkup(params KeyboardButton[] keyboardRow) : this(new List<List<KeyboardButton>> { keyboardRow.ToList() }) { }

            /// <summary>Instantiates a new <see cref="ReplyKeyboardMarkup"/></summary>
            [SetsRequiredMembers]
            public ReplyKeyboardMarkup(bool resizeKeyboard) : this() { ResizeKeyboard = resizeKeyboard; }

            /// <summary>Generates a reply keyboard markup with one button</summary>
            /// <param name="text">Button's text</param>
            public static implicit operator ReplyKeyboardMarkup?(string? text) => text is null ? default : new(text);

            /// <summary>Generates a reply keyboard markup with multiple buttons on one row</summary>
            /// <param name="texts">Texts of buttons</param>
            public static implicit operator ReplyKeyboardMarkup?(string[]? texts) => texts is null ? default : new[] { texts };

            /// <summary>Generates a reply keyboard markup with multiple buttons</summary>
            /// <param name="textsItems">Texts of buttons</param>
            public static implicit operator ReplyKeyboardMarkup?(string[][]? textsItems) => textsItems is null ? default
                : new ReplyKeyboardMarkup(textsItems.Select(texts => texts.Select(t => new KeyboardButton(t)).ToList()).ToList());

            /// <summary>Add a button to the last row</summary>
            /// <param name="button">The button or text to add</param>
            /// <returns></returns>
            public ReplyKeyboardMarkup AddButton(KeyboardButton button)
            {
                if (Keyboard is not List<List<KeyboardButton>> keyboard)
                    throw new InvalidOperationException("This method works only with a List<List<KeyboardButton>> keyboard");
                if (keyboard.Count == 0) keyboard.Add([]);
                keyboard[^1].Add(button);
                return this;
            }

            /// <summary>Add buttons to the last row</summary>
            /// <param name="buttons">The buttons or texts to add</param>
            /// <returns></returns>
            public ReplyKeyboardMarkup AddButtons(params KeyboardButton[] buttons)
            {
                if (Keyboard is not List<List<KeyboardButton>> keyboard)
                    throw new InvalidOperationException("This method works only with a List<List<KeyboardButton>> keyboard");
                if (keyboard.Count == 0) keyboard.Add([]);
                keyboard[^1].AddRange([.. buttons]);
                return this;
            }

            /// <summary>Add a new row of buttons</summary>
            /// <param name="buttons">Optional: buttons or texts for the new row</param>
            /// <returns></returns>
            public ReplyKeyboardMarkup AddNewRow(params KeyboardButton[] buttons)
            {
                if (Keyboard is not List<List<KeyboardButton>> keyboard)
                    throw new InvalidOperationException("This method works only with a List<List<KeyboardButton>> keyboard");
                keyboard.Add([.. buttons]);
                return this;
            }
        }

        public partial class InlineKeyboardMarkup
        {
            /// <summary>Initializes a new instance of the <see cref="InlineKeyboardMarkup"/> class with only one keyboard button</summary>
            /// <param name="inlineKeyboardButton">Keyboard button</param>
            [SetsRequiredMembers]
            public InlineKeyboardMarkup(InlineKeyboardButton inlineKeyboardButton) : this(new List<List<InlineKeyboardButton>> { new() { inlineKeyboardButton } }) { }

#pragma warning disable MA0016 // Prefer using collection abstraction instead of implementation
            /// <summary>Initializes a new instance of the <see cref="InlineKeyboardMarkup"/> class with a one-row keyboard</summary>
            /// <param name="inlineKeyboardRow">The inline keyboard row</param>
            [SetsRequiredMembers]
            public InlineKeyboardMarkup(List<InlineKeyboardButton> inlineKeyboardRow) : this(new List<List<InlineKeyboardButton>> { inlineKeyboardRow }) { }
#pragma warning restore MA0016 // Prefer using collection abstraction instead of implementation

            /// <summary>Initializes a new instance of the <see cref="InlineKeyboardMarkup"/> class with a one-row keyboard</summary>
            /// <param name="inlineKeyboardRow">The inline keyboard row</param>
            [SetsRequiredMembers]
            public InlineKeyboardMarkup(IEnumerable<InlineKeyboardButton> inlineKeyboardRow) : this(new List<List<InlineKeyboardButton>> { inlineKeyboardRow.ToList() }) { }

            /// <summary>Initializes a new instance of the <see cref="InlineKeyboardMarkup"/> class with a one-row keyboard</summary>
            /// <param name="inlineKeyboardRow">The inline keyboard row</param>
            [SetsRequiredMembers]
            public InlineKeyboardMarkup(params InlineKeyboardButton[] inlineKeyboardRow) : this(new List<List<InlineKeyboardButton>> { inlineKeyboardRow.ToList() }) { }

            /// <summary>Generate an empty inline keyboard markup</summary>
            /// <returns>Empty inline keyboard markup</returns>
            public static InlineKeyboardMarkup Empty() => new();

            /// <summary>Generate an inline keyboard markup with one button</summary>
            /// <param name="button">Inline keyboard button</param>
            [return: NotNullIfNotNull(nameof(button))]
            public static implicit operator InlineKeyboardMarkup?(InlineKeyboardButton? button) => button is null ? default : new(button);

            /// <summary>Generate an inline keyboard markup with one button</summary>
            /// <param name="buttonText">Text of the button</param>
            [return: NotNullIfNotNull(nameof(buttonText))]
            public static implicit operator InlineKeyboardMarkup?(string? buttonText) => buttonText is null ? default : new(buttonText!);

            /// <summary>Generate an inline keyboard markup from multiple buttons</summary>
            /// <param name="inlineKeyboard">Keyboard buttons</param>
            [return: NotNullIfNotNull(nameof(inlineKeyboard))]
            public static implicit operator InlineKeyboardMarkup?(IEnumerable<InlineKeyboardButton>[]? inlineKeyboard) =>
                inlineKeyboard is null ? default : new(inlineKeyboard);

            /// <summary>Generate an inline keyboard markup from multiple buttons on 1 row</summary>
            /// <param name="inlineKeyboard">Keyboard buttons</param>
            [return: NotNullIfNotNull(nameof(inlineKeyboard))]
            public static implicit operator InlineKeyboardMarkup?(InlineKeyboardButton[]? inlineKeyboard) =>
                inlineKeyboard is null ? default : new(inlineKeyboard);

            /// <summary>Add a button to the last row</summary>
            /// <param name="button">The button to add</param>
            /// <returns></returns>
            public InlineKeyboardMarkup AddButton(InlineKeyboardButton button)
            {
                if (InlineKeyboard is not List<List<InlineKeyboardButton>> keyboard)
                    throw new InvalidOperationException("This method works only with a List<List<InlineKeyboardButton>> keyboard");
                if (keyboard.Count == 0) keyboard.Add([]);
                keyboard[^1].Add(button);
                return this;
            }

            /// <summary>Add a callback button to the last row</summary>
            /// <param name="text">Label text on the button</param>
            /// <param name="callbackData">Data to be sent in a <see cref="CallbackQuery">callback query</see> to the bot when the button is pressed, 1-64 bytes</param>
            /// <returns></returns>
            public InlineKeyboardMarkup AddButton(string text, string callbackData)
                => AddButton(InlineKeyboardButton.WithCallbackData(text, callbackData));

            /// <summary>Add buttons to the last row</summary>
            /// <param name="buttons">The buttons to add</param>
            /// <returns></returns>
            public InlineKeyboardMarkup AddButtons(params InlineKeyboardButton[] buttons)
            {
                if (InlineKeyboard is not List<List<InlineKeyboardButton>> keyboard)
                    throw new InvalidOperationException("This method works only with a List<List<KeyboardButton>> keyboard");
                if (keyboard.Count == 0) keyboard.Add([]);
                keyboard[^1].AddRange([.. buttons]);
                return this;
            }

            /// <summary>Add a new row of buttons</summary>
            /// <param name="buttons">Optional: buttons for the new row</param>
            /// <returns></returns>
            public InlineKeyboardMarkup AddNewRow(params InlineKeyboardButton[] buttons)
            {
                if (InlineKeyboard is not List<List<InlineKeyboardButton>> keyboard)
                    throw new InvalidOperationException("This method works only with a List<List<InlineKeyboardButton>> keyboard");
                keyboard.Add([.. buttons]);
                return this;
            }
        }

        public partial class InlineKeyboardButton
        {
            /// <summary>
            /// Performs an implicit conversion from <see cref="string"/> to <see cref="InlineKeyboardButton"/>
            /// with callback data
            /// </summary>
            /// <param name="textAndCallbackData">Label text and callback data of the button</param>
            /// <returns>
            /// The result of the conversion.
            /// </returns>
            public static implicit operator InlineKeyboardButton?(string? textAndCallbackData) =>
                textAndCallbackData is null
                    ? default
                    : WithCallbackData(textAndCallbackData);

            /// <summary>
            /// Creates an inline keyboard button that sends <see cref="CallbackQuery"/> to bot when pressed
            /// </summary>
            /// <param name="textAndCallbackData">
            /// Text and data of the button to be sent in a <see cref="CallbackQuery">callback query</see> to the bot when
            /// button is pressed, 1-64 bytes
            /// </param>
            public static InlineKeyboardButton WithCallbackData(string textAndCallbackData) =>
                new(textAndCallbackData) { CallbackData = textAndCallbackData };
        }

        public partial class KeyboardButton
        {
            /// <summary>
            /// Generate a keyboard button from text
            /// </summary>
            /// <param name="text">Button's text</param>
            /// <returns>Keyboard button</returns>
            public static implicit operator KeyboardButton(string text)
                => new(text);


            /// <summary>
            /// Generate a keyboard button to request users
            /// </summary>
            /// <param name="text">Button's text</param>
            /// <param name="requestId">
            /// Signed 32-bit identifier of the request that will be received back in the <see cref="UsersShared"/> object.
            /// Must be unique within the message
            /// </param>
            public static KeyboardButton WithRequestUsers(string text, int requestId) =>
                new(text) { RequestUsers = new(requestId) };

            /// <summary>
            /// Creates a keyboard button. Pressing the button will open a list of suitable chats. Tapping on a chat will send its identifier to the bot in a <see cref="ChatShared"/> service message. Available in private chats only.
            /// </summary>
            /// <param name="text">Button's text</param>
            /// <param name="requestId">
            /// Signed 32-bit identifier of the request, which will be received back in the <see cref="ChatShared"/> object.
            /// Must be unique within the message
            /// </param>
            /// <param name="chatIsChannel">
            /// Pass <see langword="true"/> to request a channel chat, pass <see langword="false"/> to request a group or a supergroup chat.
            /// </param>
            public static KeyboardButton WithRequestChat(string text, int requestId, bool chatIsChannel) =>
                new(text) { RequestChat = new(requestId, chatIsChannel) };

            /// <summary>
            /// Generate a keyboard button to request a poll
            /// </summary>
            /// <param name="text">Button's text</param>
            /// <returns>Keyboard button</returns>
            public static KeyboardButton WithRequestPoll(string text) =>
                new(text) { RequestPoll = new() };
        }

        public partial class KeyboardButtonPollType
        {
            /// <summary>implicit from string</summary>
            public static implicit operator KeyboardButtonPollType(string? type) => new() { Type = type };
        }
    }

    namespace InlineQueryResults
    {
        public partial class InputTextMessageContent
        {
            /// <summary>
            /// Disables link previews for links in this message
            /// </summary>
            [JsonIgnore]
            [Obsolete($"This property is deprecated, use {nameof(LinkPreviewOptions)} instead")]
            public bool DisableWebPagePreview
            {
                get => LinkPreviewOptions?.IsDisabled ?? false;
                set
                {
                    LinkPreviewOptions ??= new();
                    LinkPreviewOptions.IsDisabled = value;
                }
            }
        }
    }
}

namespace Telegram.Bot.Requests
{
    public partial class SendMediaGroupRequest
    {
        /// <inheritdoc />
        public override HttpContent ToHttpContent()
        {
            var multipartContent = GenerateMultipartFormDataContent();

            foreach (var mediaItem in Media)
            {
                if (mediaItem is InputMedia { Media: InputFileStream file })
                {
                    multipartContent.AddContentIfInputFile(file, file.FileName!);
                }

                if (mediaItem is IInputMediaThumb { Thumbnail: InputFileStream thumbnail })
                {
                    multipartContent.AddContentIfInputFile(thumbnail, thumbnail.FileName!);
                }
            }

            return multipartContent;
        }
    }

    public partial class SendPaidMediaRequest
    {
        /// <inheritdoc />
        public override HttpContent ToHttpContent()
        {
            var multipartContent = GenerateMultipartFormDataContent();

            foreach (var mediaItem in Media)
            {
                if (mediaItem is InputPaidMedia { Media: InputFileStream file })
                {
                    multipartContent.AddContentIfInputFile(file, file.FileName!);
                }

                if (mediaItem is IInputMediaThumb { Thumbnail: InputFileStream thumbnail })
                {
                    multipartContent.AddContentIfInputFile(thumbnail, thumbnail.FileName!);
                }
            }

            return multipartContent;
        }
    }

    public partial class SendPollRequest
    {
        /// <summary>
        /// Initializes an instance of <see cref="SendPollRequest"/>
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format <c>@channelusername</c>)</param>
        /// <param name="question">Poll question, 1-300 characters</param>
        /// <param name="options">A list of 2-10 answer options</param>
        [SetsRequiredMembers]
        [Obsolete("Use parameterless constructor with required properties")]
        public SendPollRequest(ChatId chatId, string question, IEnumerable<string> options)
            : this(chatId, question, options.Select(o => (InputPollOption)o))
        { }
    }

    public partial class CreateNewStickerSetRequest
    {
        /// <inheritdoc/>
        public override HttpContent ToHttpContent()
        {
            var multipartContent = GenerateMultipartFormDataContent();

            foreach (var inputSticker in Stickers)
            {
                if (inputSticker is { Sticker: InputFileStream file })
                {
                    multipartContent.AddContentIfInputFile(file, file.FileName!);
                }
            }

            return multipartContent;
        }
    }

    public partial class EditInlineMessageMediaRequest
    {
        /// <inheritdoc />
        public override HttpContent? ToHttpContent()
        {
            if (Media.Media.FileType is not FileType.Stream &&
                Media is not IInputMediaThumb { Thumbnail.FileType: FileType.Stream })
            {
                return base.ToHttpContent();
            }

            var multipartContent = GenerateMultipartFormDataContent();

            if (Media.Media is InputFileStream file)
            {
                multipartContent.AddContentIfInputFile(file, file.FileName!);
            }
            if (Media is IInputMediaThumb { Thumbnail: InputFileStream thumbnail })
            {
                multipartContent.AddContentIfInputFile(thumbnail, thumbnail.FileName!);
            }

            return multipartContent;
        }
    }

    public partial class EditMessageMediaRequest
    {
        /// <inheritdoc />
        public override HttpContent? ToHttpContent()
        {
            if (Media.Media.FileType is not FileType.Stream &&
                Media is not IInputMediaThumb { Thumbnail.FileType: FileType.Stream })
            {
                return base.ToHttpContent();
            }

            var multipartContent = GenerateMultipartFormDataContent();

            if (Media.Media is InputFileStream file)
            {
                multipartContent.AddContentIfInputFile(file, file.FileName!);
            }
            if (Media is IInputMediaThumb { Thumbnail: InputFileStream thumbnail })
            {
                multipartContent.AddContentIfInputFile(thumbnail, thumbnail.FileName!);
            }

            return multipartContent;
        }
    }
}
