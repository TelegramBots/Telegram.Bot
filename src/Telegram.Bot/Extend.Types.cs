using System.Globalization;
using System.Linq;

#pragma warning disable IDE0290 // Use primary constructor

namespace Telegram.Bot.Types
{
    public partial class Message
    {
        /// <summary>Same as <see cref="Id"/></summary>
        [JsonIgnore]
        public int MessageId => Id;

        /// <summary><em>Optional</em>. For forwarded messages, sender of the original message</summary>
        [JsonIgnore]
        public User? ForwardFrom => (ForwardOrigin as MessageOriginUser)?.SenderUser;
        /// <summary><em>Optional</em>. For messages forwarded from channels or from anonymous administrators, information about the original sender chat</summary>
        [JsonIgnore]
        public Chat? ForwardFromChat => ForwardOrigin switch
        {
            MessageOriginChannel originChannel => originChannel.Chat,
            MessageOriginChat originChat => originChat.SenderChat,
            _ => null,
        };
        /// <summary><em>Optional</em>. For messages forwarded from channels, identifier of the original message in the channel</summary>
        [JsonIgnore]
        public int? ForwardFromMessageId => (ForwardOrigin as MessageOriginChannel)?.MessageId;
        /// <summary><em>Optional</em>. For messages forwarded from channels, signature of the post author if present</summary>
        [JsonIgnore]
        public string? ForwardSignature => (ForwardOrigin as MessageOriginChannel)?.AuthorSignature;
        /// <summary><em>Optional</em>. Sender's name for messages forwarded from users who disallow adding a link to their account in forwarded messages</summary>
        [JsonIgnore]
        public string? ForwardSenderName => (ForwardOrigin as MessageOriginHiddenUser)?.SenderUserName;
        /// <summary><em>Optional</em>. For forwarded messages, date the original message was sent</summary>
        [JsonIgnore]
        public DateTime? ForwardDate => ForwardOrigin?.Date;

        /// <summary>Gets the entity values.</summary>
        /// <value>The texts covered by each entity.</value>
        [JsonIgnore]
        public IEnumerable<string>? EntityValues => Text is null ? default : Entities?.Select(entity => Text.Substring(entity.Offset, entity.Length));
        /// <summary>Gets the caption entity values.</summary>
        /// <value>The caption texts covered by each entity.</value>
        [JsonIgnore]
        public IEnumerable<string>? CaptionEntityValues => Caption is null ? default : CaptionEntities?.Select(entity => Caption.Substring(entity.Offset, entity.Length));

        /// <summary>Returns the <a href="t.me">t.me</a> link to this message, or null if the message was not in a Supergroup or Channel</summary>
        public string? MessageLink()
        {
            var threadPart = Chat.IsForum && MessageThreadId.HasValue ? $"{MessageThreadId.Value.ToString(CultureInfo.InvariantCulture)}/" : string.Empty;

            return Chat.Type is ChatType.Channel or ChatType.Supergroup ? Chat.Username is null
                ? $"https://t.me/c/{(-1000000000000 - Chat.Id).ToString(CultureInfo.InvariantCulture)}/{threadPart}{Id.ToString(CultureInfo.InvariantCulture)}"
                : $"https://t.me/{Chat.Username}/{threadPart}{Id.ToString(CultureInfo.InvariantCulture)}"
                : null;
        }

        /// <summary><see langword="true"/> if it's a service message, <see langword="false"/> if it's a content message</summary>
        [JsonIgnore]
        public bool IsServiceMessage => this switch
        {
            { Text: { } } or { Caption: { } } or { Photo: { } } or { Video: { } } or { Document: { } } or { Sticker: { } } or
            { Animation: { } } or { Audio: { } } or { Voice: { } } or { Poll: { } } or { Dice: { } } or { Checklist: { } } or
            { Game: { } } or { Location: { } } or { PaidMedia: { } } or { Story: { } } or { VideoNote: { } } or { Contact: { } } or
            { Venue: { } } or { Invoice: { } } or { Giveaway: { } } or { GiveawayWinners: { } } or { Gift: { } } or { UniqueGift: { } }
              => false,
            _ => true
        };
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
        public override string ToString() => Username != null ? $"@{Username} ({Id})" : $"{FirstName}{LastName?.Insert(0, " ")} ({Id})";
    }

    public partial class ChatMember
    {
        /// <summary>Check if the user is chat admin or owner</summary>
        [JsonIgnore]
        public bool IsAdmin => Status is ChatMemberStatus.Administrator or ChatMemberStatus.Creator;
        /// <summary>Check if the user is in the chat</summary>
        [JsonIgnore]
        public bool IsInChat => Status switch
        {
            ChatMemberStatus.Left or ChatMemberStatus.Kicked => false,
            ChatMemberStatus.Restricted => ((ChatMemberRestricted)this).IsMember,
            _ => true
        };

        /// <summary>Date when restrictions will be lifted for this user (null if forever) or expiration of member's subscription</summary>
        [JsonIgnore]
        public DateTime? ExpireDate => this switch
        {
            ChatMemberBanned banned => banned.UntilDate,
            ChatMemberRestricted restricted => restricted.UntilDate,
            ChatMemberMember member => member.UntilDate,
            _ => null
        };
    }

    public partial class ChatMemberRestricted
    {
        /// <summary>Check if all CanSend* fields are false</summary>
        [JsonIgnore]
        public bool IsMuted => !CanSendMessages && !CanSendAudios && !CanSendDocuments && !CanSendPhotos && !CanSendVideos
            && !CanSendVideoNotes && !CanSendVoiceNotes && !CanSendPolls && !CanSendOtherMessages;
    }

    public partial class ChatPermissions
    {
        /// <summary>Initializes a new <see cref="ChatPermissions"/> instance with all fields set to <see langword="false"/>.</summary>
        public ChatPermissions() { }
        /// <summary>Initializes a new <see cref="ChatPermissions"/> instance with all fields set to the specified value.</summary>
        /// <param name="defaultValue"><see langword="true"/> to allow all permissions by default</param>
        public ChatPermissions(bool defaultValue)
        {
            CanSendMessages = CanSendAudios = CanSendDocuments = CanSendPhotos = defaultValue;
            CanSendVideos = CanSendVideoNotes = CanSendVoiceNotes = CanSendPolls = CanSendOtherMessages = defaultValue;
            CanAddWebPagePreviews = CanChangeInfo = CanInviteUsers = CanPinMessages = CanManageTopics = defaultValue;
        }
    }

    public partial class ChatAdministratorRights
    {
        /// <summary>Initializes a new <see cref="ChatAdministratorRights"/> instance with all fields set to <see langword="false"/>.</summary>
        public ChatAdministratorRights() { }
        /// <summary>Initializes a new <see cref="ChatAdministratorRights"/> instance with all <c>Can*</c> fields set to the specified value.</summary>
        /// <param name="defaultValue"><see langword="true"/> to allow all permissions by default</param>
        public ChatAdministratorRights(bool defaultValue)
        {
            CanManageChat = CanDeleteMessages = CanManageVideoChats = CanRestrictMembers = CanPromoteMembers = defaultValue;
            CanChangeInfo = CanInviteUsers = CanPostStories = CanEditStories = CanDeleteStories = defaultValue;
            CanPostMessages = CanEditMessages = CanPinMessages = CanManageTopics = CanManageDirectMessages = defaultValue;
        }
    }

    public partial class ReplyParameters
    {
        /// <summary>Implicit operator when you just want to reply to a message in same chat</summary>
        public static implicit operator ReplyParameters(int replyToMessageId) => new() { MessageId = replyToMessageId };
        /// <summary>Implicit operator when you just want to reply to a message</summary>
        public static implicit operator ReplyParameters?(Message? msg) => msg == null ? null : new() { MessageId = msg.Id, ChatId = msg.Chat.Id };
    }

    public partial class MessageId
    {
        /// <summary>Implicit operator from Message</summary>
        public static implicit operator MessageId(Message msg) => new() { Id = msg.Id };
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
        /// <summary>Simply pass a URL string to request a preview of that link</summary>
        public static implicit operator LinkPreviewOptions(string url) => new() { Url = url };
    }

    public partial class InputPollOption
    {
        /// <summary>Implicit operator for compatibility</summary>
        public static implicit operator InputPollOption(string text) => new() { Text = text };
    }

    public partial class BotCommand
    {
        /// <summary>Instantiates a new BotCommand</summary>
        /// <param name="command">Text of the command, 1-32 characters. Can contain only lowercase English letters, digits and underscores</param>
        /// <param name="description">Description of the command, 1-256 characters</param>
        public BotCommand(string command, string description) { Command = command; Description = description; }
        /// <summary>Instantiates a new BotCommand like this: <code>new BotCommand { Command = "/start", Description = "Start the bot" }</code></summary>
        public BotCommand() { }
        /// <summary>Instantiates a new BotCommand from a tuple</summary>
        public static implicit operator BotCommand((string text, string description) tuple) => new(tuple.text, tuple.description);
    }

    public partial class BotCommandScope
    {
        /// <summary>Create a default <see cref="BotCommandScope"/> instance</summary>
        public static BotCommandScopeDefault Default() => new();
        /// <summary>Create a <see cref="BotCommandScope"/> instance for all private chats</summary>
        public static BotCommandScopeAllPrivateChats AllPrivateChats() => new();
        /// <summary>Create a <see cref="BotCommandScope"/> instance for all group chats</summary>
        public static BotCommandScopeAllGroupChats AllGroupChats() => new();
        /// <summary>Create a <see cref="BotCommandScope"/> instance for all chat administrators</summary>
        public static BotCommandScopeAllChatAdministrators AllChatAdministrators() => new();
        /// <summary>Create a <see cref="BotCommandScope"/> instance for a specific <see cref="Chat"/></summary>
        /// <param name="chatId">Unique identifier for the target <see cref="Chat"/> or username of the target supergroup</param>
        public static BotCommandScopeChat Chat(ChatId chatId) => new() { ChatId = chatId };
        /// <summary>Create a <see cref="BotCommandScope"/> instance for a specific member in a specific <see cref="Chat"/></summary>
        /// <param name="chatId">Unique identifier for the target <see cref="Chat"/> or username of the target supergroup</param>
        public static BotCommandScopeChatAdministrators ChatAdministrators(ChatId chatId) => new() { ChatId = chatId };
        /// <summary>Represents the <see cref="BotCommandScope">scope</see> of bot commands, covering a specific member of a group or supergroup chat.</summary>
        /// <param name="chatId">Unique identifier for the target <see cref="Chat"/> or username of the target supergroup</param>
        /// <param name="userId">Unique identifier of the target user</param>
        public static BotCommandScopeChatMember ChatMember(ChatId chatId, long userId) => new() { ChatId = chatId, UserId = userId };
    }

    namespace Payments
    {
        public partial class LabeledPrice
        {
            /// <summary>Instantiates a new <see cref="LabeledPrice"/> from a tuple</summary>
            public static implicit operator LabeledPrice((string label, long amount) t) => new(t.label, t.amount);
        }
    }

    namespace ReplyMarkups
    {
        public partial class ReplyKeyboardMarkup
        {
            /// <summary>Creates a <see cref="ReplyKeyboardMarkup"/> with one button</summary>
            /// <param name="button">Button or text on keyboard</param>
            [SetsRequiredMembers]
            public ReplyKeyboardMarkup(KeyboardButton button) => Keyboard = new List<List<KeyboardButton>> { new() { button } };

            /// <summary>Creates a <see cref="ReplyKeyboardMarkup"/> with multiple buttons on one row</summary>
            /// <param name="keyboardRow">Reply keyboard buttons</param>
            [SetsRequiredMembers]
            public ReplyKeyboardMarkup(params KeyboardButton[] keyboardRow) => Keyboard = new List<List<KeyboardButton>> { keyboardRow.ToList() };

            /// <summary>Creates a <see cref="ReplyKeyboardMarkup"/> with multiple buttons on one row</summary>
            /// <param name="keyboardRow">Reply keyboard buttons</param>
            [SetsRequiredMembers]
            public ReplyKeyboardMarkup(IEnumerable<KeyboardButton> keyboardRow) => Keyboard = new List<List<KeyboardButton>> { keyboardRow.ToList() };

#pragma warning disable MA0016 // Prefer using collection abstraction instead of implementation
            /// <summary>Creates a <see cref="ReplyKeyboardMarkup"/> with multiple buttons on one row</summary>
            /// <param name="keyboardRow">Reply keyboard buttons</param>
            [SetsRequiredMembers]
            public ReplyKeyboardMarkup(List<KeyboardButton> keyboardRow) => Keyboard = new List<List<KeyboardButton>> { keyboardRow };
#pragma warning restore MA0016 // Prefer using collection abstraction instead of implementation

            /// <summary>Instantiates a new <see cref="ReplyKeyboardMarkup"/></summary>
            /// <param name="resizeKeyboard">Request to resize the keyboard vertically for optimal fit</param>
            [SetsRequiredMembers]
            public ReplyKeyboardMarkup(bool resizeKeyboard) : this() => ResizeKeyboard = resizeKeyboard;

            /// <summary>Generates a reply keyboard markup with one button</summary>
            /// <param name="text">Button text</param>
            [return: NotNullIfNotNull(nameof(text))]
            public static implicit operator ReplyKeyboardMarkup?(string? text) => text is null ? default : new(text);

            /// <summary>Generates a reply keyboard markup with multiple buttons on one row</summary>
            /// <param name="texts">Buttons text</param>
            [return: NotNullIfNotNull(nameof(texts))]
            public static implicit operator ReplyKeyboardMarkup?(string[]? texts) => texts is null ? default : new[] { texts };

            /// <summary>Generates a reply keyboard markup with multiple rows of buttons</summary>
            /// <param name="textRows">Rows of buttons text</param>
            [return: NotNullIfNotNull(nameof(textRows))]
            public static implicit operator ReplyKeyboardMarkup?(string[][]? textRows) => textRows is null ? default
                : new ReplyKeyboardMarkup(textRows.Select(texts => texts.Select(t => new KeyboardButton(t)).ToList()).ToList());

            /// <summary>Generates a reply keyboard markup with one button</summary>
            /// <param name="button">Reply keyboard button</param>
            [return: NotNullIfNotNull(nameof(button))]
            public static implicit operator ReplyKeyboardMarkup?(KeyboardButton? button) => button is null ? default : new(button);

            /// <summary>Generates a reply keyboard markup with multiple buttons on one row</summary>
            /// <param name="buttons">Reply keyboard buttons</param>
            [return: NotNullIfNotNull(nameof(buttons))]
            public static implicit operator ReplyKeyboardMarkup?(KeyboardButton[]? buttons) => buttons is null ? default : new(buttons);

            /// <summary>Generates a reply keyboard markup with multiple buttons on one row</summary>
            /// <param name="buttons">Reply keyboard buttons</param>
            [return: NotNullIfNotNull(nameof(buttons))]
            public static implicit operator ReplyKeyboardMarkup?(List<KeyboardButton>? buttons) => buttons is null ? default : new(buttons);

            /// <summary>Generates a reply keyboard markup with multiple rows of buttons</summary>
            /// <param name="buttonRows">Rows of reply keyboard buttons</param>
            [return: NotNullIfNotNull(nameof(buttonRows))]
            public static implicit operator ReplyKeyboardMarkup?(List<List<KeyboardButton>>? buttonRows) => buttonRows is null ? default : new(buttonRows);

            /// <summary>Generates a reply keyboard markup with multiple rows of buttons</summary>
            /// <param name="buttonRows">Rows of reply keyboard buttons</param>
            [return: NotNullIfNotNull(nameof(buttonRows))]
            public static implicit operator ReplyKeyboardMarkup?(IEnumerable<KeyboardButton>[]? buttonRows) => buttonRows is null ? default : new(buttonRows);

            /// <summary>Add a button to the last row</summary>
            /// <param name="button">The button or text to add</param>
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
            /// <summary>Creates an <see cref="InlineKeyboardMarkup"/> with one button</summary>
            /// <param name="inlineKeyboardButton">Inline keyboard button</param>
            [SetsRequiredMembers]
            public InlineKeyboardMarkup(InlineKeyboardButton inlineKeyboardButton)
                => InlineKeyboard = new List<List<InlineKeyboardButton>> { new() { inlineKeyboardButton } };

            /// <summary>Creates an <see cref="InlineKeyboardMarkup"/> with multiple buttons on one row</summary>
            /// <param name="inlineKeyboardRow">Rows of inline keyboard buttons</param>
            [SetsRequiredMembers]
            public InlineKeyboardMarkup(params InlineKeyboardButton[] inlineKeyboardRow)
                => InlineKeyboard = new List<List<InlineKeyboardButton>> { inlineKeyboardRow.ToList() };

            /// <summary>Creates an <see cref="InlineKeyboardMarkup"/> with multiple buttons on one row</summary>
            /// <param name="inlineKeyboardRow">Rows of inline keyboard buttons</param>
            [SetsRequiredMembers]
            public InlineKeyboardMarkup(IEnumerable<InlineKeyboardButton> inlineKeyboardRow)
                => InlineKeyboard = new List<List<InlineKeyboardButton>> { inlineKeyboardRow.ToList() };

#pragma warning disable MA0016 // Prefer using collection abstraction instead of implementation
            /// <summary>Creates an <see cref="InlineKeyboardMarkup"/> with multiple buttons on one row</summary>
            /// <param name="inlineKeyboardRow">Rows of inline keyboard buttons</param>
            [SetsRequiredMembers]
            public InlineKeyboardMarkup(List<InlineKeyboardButton> inlineKeyboardRow)
                => InlineKeyboard = new List<List<InlineKeyboardButton>> { inlineKeyboardRow };
#pragma warning restore MA0016 // Prefer using collection abstraction instead of implementation

            /// <summary>Generate an empty inline keyboard markup</summary>
            /// <returns>Empty inline keyboard markup</returns>
            public static InlineKeyboardMarkup Empty() => new();

            /// <summary>Generate an inline keyboard markup with one button</summary>
            /// <param name="button">Inline keyboard button</param>
            [return: NotNullIfNotNull(nameof(button))]
            public static implicit operator InlineKeyboardMarkup?(InlineKeyboardButton? button) => button is null ? default : new(button);

            /// <summary>Generate an inline keyboard markup with one button</summary>
            /// <param name="buttonText">Text serving as the label of the button, as well as the URL to be opened or the callback data to be sent</param>
            [return: NotNullIfNotNull(nameof(buttonText))]
            public static implicit operator InlineKeyboardMarkup?(string? buttonText) => buttonText is null ? default : new(buttonText);

            /// <summary>Generate an inline keyboard markup with multiple buttons on one row</summary>
            /// <param name="buttons">Inline keyboard buttons</param>
            [return: NotNullIfNotNull(nameof(buttons))]
            public static implicit operator InlineKeyboardMarkup?(InlineKeyboardButton[]? buttons) => buttons is null ? default : new(buttons);

            /// <summary>Generate an inline keyboard markup with multiple buttons on one row</summary>
            /// <param name="buttons">Inline keyboard buttons</param>
            [return: NotNullIfNotNull(nameof(buttons))]
            public static implicit operator InlineKeyboardMarkup?(List<InlineKeyboardButton>? buttons) => buttons is null ? default : new(buttons);

            /// <summary>Generate an inline keyboard markup with multiple rows of buttons</summary>
            /// <param name="buttonRows">Rows of inline keyboard buttons</param>
            [return: NotNullIfNotNull(nameof(buttonRows))]
            public static implicit operator InlineKeyboardMarkup?(List<List<InlineKeyboardButton>>? buttonRows) => buttonRows is null ? default : new(buttonRows);

            /// <summary>Generate an inline keyboard markup with multiple rows of buttons</summary>
            /// <param name="buttonRows">Rows of inline keyboard buttons</param>
            [return: NotNullIfNotNull(nameof(buttonRows))]
            public static implicit operator InlineKeyboardMarkup?(IEnumerable<InlineKeyboardButton>[]? buttonRows) => buttonRows is null ? default : new(buttonRows);

            /// <summary>Add a button to the last row</summary>
            /// <param name="button">The button to add</param>
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
            public InlineKeyboardMarkup AddButton(string text, string callbackData)
                => AddButton(InlineKeyboardButton.WithCallbackData(text, callbackData));

            /// <summary>Add buttons to the last row</summary>
            /// <param name="buttons">The buttons to add</param>
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
            /// <summary>Creates an inline keyboard button for external URL or for data to be sent in a <see cref="CallbackQuery">callback query</see> to the bot when the button is pressed, 1-64 bytes</summary>
            /// <param name="text">Label text on the button</param>
            /// <param name="callbackDataOrUrl">URL (starting with http:// or https://) to be opened, or data (1-64 characters) to be sent in a <see cref="CallbackQuery">callback query</see> to the bot, when the button is pressed</param>
            [SetsRequiredMembers]
            public InlineKeyboardButton(string text, string callbackDataOrUrl)
            {
                Text = text;
                if (callbackDataOrUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || callbackDataOrUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                    Url = callbackDataOrUrl;
                else
                    CallbackData = callbackDataOrUrl;
            }

            /// <summary>Performs an implicit conversion from <see cref="string"/> to <see cref="InlineKeyboardButton"/></summary>
            /// <param name="textAndCallbackDataOrUrl">Text serving as the label of the button, as well as the URL to be opened or the callback data to be sent</param>
            /// <returns>The result of the conversion.</returns>
            public static implicit operator InlineKeyboardButton(string textAndCallbackDataOrUrl)
                => new(textAndCallbackDataOrUrl, textAndCallbackDataOrUrl);

            /// <summary>Performs an implicit conversion from (<see cref="string"/>, <see cref="string"/>) tuple to <see cref="InlineKeyboardButton"/></summary>
            /// <param name="tuple">Tuple with label text, and the URL to be opened or the callback data</param>
            /// <returns>The result of the conversion.</returns>
            public static implicit operator InlineKeyboardButton((string text, string callbackDataOrUrl) tuple)
                => new(tuple.text, tuple.callbackDataOrUrl);

            /// <summary>Creates an inline keyboard button that sends <see cref="CallbackQuery"/> to bot when pressed</summary>
            /// <param name="textAndCallbackData">Text and data of the button to be sent in a <see cref="CallbackQuery">callback query</see> to the bot when button is pressed, 1-64 bytes</param>
            public static InlineKeyboardButton WithCallbackData(string textAndCallbackData)
                => new(textAndCallbackData) { CallbackData = textAndCallbackData };
        }

        public partial class KeyboardButton
        {
            /// <summary>Generate a keyboard button from text</summary>
            /// <param name="text">Button's text</param>
            public static implicit operator KeyboardButton(string text)
                => new(text);

            /// <summary>Generate a keyboard button to request users</summary>
            /// <param name="text">Button's text</param>
            /// <param name="requestId">Signed 32-bit identifier of the request that will be received back in the <see cref="UsersShared"/> object. Must be unique within the message</param>
            /// <param name="maxQuantity"><em>Optional</em>. The maximum number of users to be selected; 1-10. Defaults to 1.</param>
            public static KeyboardButton WithRequestUsers(string text, int requestId, int? maxQuantity = null)
                => new(text) { RequestUsers = new(requestId) { MaxQuantity = maxQuantity } };

            /// <summary>Creates a keyboard button. Pressing the button will open a list of suitable chats. Tapping on a chat will send its identifier to the bot in a <see cref="ChatShared"/> service message. Available in private chats only.</summary>
            /// <param name="text">Button's text</param>
            /// <param name="requestId">Signed 32-bit identifier of the request, which will be received back in the <see cref="ChatShared"/> object. Must be unique within the message</param>
            /// <param name="chatIsChannel">Pass <see langword="true"/> to request a channel chat, pass <see langword="false"/> to request a group or a supergroup chat.</param>
            public static KeyboardButton WithRequestChat(string text, int requestId, bool chatIsChannel)
                => new(text) { RequestChat = new(requestId, chatIsChannel) };
        }
    }

    namespace Passport
    {
        public partial class IdDocumentData
        {
            /// <summary>Date of expiry if available</summary>
            public DateTime? Expiry => DateTime.TryParseExact(ExpiryDate, "dd.MM.yyyy", provider: null, DateTimeStyles.None, out var result) ? result : null;
        }

        public partial class PersonalDetails
        {
            /// <summary>Date of birth</summary>
            public DateTime Birthday => DateTime.ParseExact(BirthDate, "dd.MM.yyyy", provider: null, DateTimeStyles.None);
        }

        public partial class PassportScopeElementOne
        {
            /// <summary>Initializes a new instance of the <see cref="PassportScopeElementOne"/> class with the specified type</summary>
            public PassportScopeElementOne(EncryptedPassportElementType type) => Type = type;
        }
    }
}
