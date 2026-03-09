namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>The enum is for use with <see cref="KeyboardButton"/> or <see cref="InlineKeyboardButton"/> constructors</summary>
public enum InlineButtonType
{
    /// <summary><c>value</c> = HTTP or tg:// URL to be opened when the button is pressed. Links <c>tg://user?id=&lt;UserId&gt;</c> can be used to mention a user by their identifier without using a username, if this is allowed by their privacy settings.</summary>
    Url,
    /// <summary><c>value</c> = Data to be sent in a <see cref="CallbackQuery">callback query</see> to the bot when the button is pressed, 1-64 bytes</summary>
    Callback,
    /// <summary><c>value</c> = An HTTPS URL of the <a href="https://core.telegram.org/bots/webapps">Web App</a> that will be launched when the user presses the button. The Web App will be able to send an arbitrary message on behalf of the user using the method <see cref="TelegramBotClientExtensions.AnswerWebAppQuery">AnswerWebAppQuery</see>. Available only in private chats between a user and the bot. Not supported for messages sent on behalf of a Telegram Business account.</summary>
    WebApp,
    /// <summary><c>value</c> = An HTTPS URL used to automatically authorize the user. Can be used as a replacement for the <a href="https://core.telegram.org/widgets/login">Telegram Login Widget</a>.</summary>
    LoginUrl,
    /// <summary>Pressing the button will prompt the user to select one of their chats, open that chat and insert the bot's username and the specified inline query <c>value</c> in the input field.<br/><c>value</c> may be left empty, in which case just the bot's username will be inserted. Not supported for messages sent in channel direct messages chats and on behalf of a Telegram Business account.</summary>
    SwitchInlineQuery,
    /// <summary>Pressing the button will insert the bot's username and the specified inline query <c>value</c> in the current chat's input field.<br/><c>value</c> may be left empty, in which case only the bot's username will be inserted.<br/><br/>This offers a quick way for the user to open your bot in inline mode in the same chat - good for selecting something from multiple options. Not supported in channels and for messages sent in channel direct messages chats and on behalf of a Telegram Business account.</summary>
    SwitchInlineQueryCurrentChat,
    /// <summary><c>value</c> = The text to be copied to the clipboard when button is pressed; 1-256 characters</summary>
    CopyText,
    /// <summary>Launching a game (set in <a href="https://t.me/botfather">@BotFather</a>) when the user presses the button.<br/><br/><b>NOTE:</b> This type of button <b>must</b> always be the first button in the first row.</summary>
    Game,
    /// <summary><a href="https://core.telegram.org/bots/api#payments">Pay button</a>. Substrings “⭐” and “XTR” in the buttons's text will be replaced with a Telegram Star icon.<br/><br/><b>NOTE:</b> This type of button <b>must</b> always be the first button in the first row and can only be used in invoice messages.</summary>
    Pay,
}
