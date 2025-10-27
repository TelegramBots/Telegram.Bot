using System.Linq;

namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>Common abstract class for reply markups that define how a <see cref="User"/> can reply to the sent <see cref="Message"/></summary>
[JsonPolymorphic(TypeDiscriminatorPropertyName = null)]
[JsonDerivedType(typeof(ForceReplyMarkup))]
[JsonDerivedType(typeof(ReplyKeyboardMarkup))]
[JsonDerivedType(typeof(InlineKeyboardMarkup))]
[JsonDerivedType(typeof(ReplyKeyboardRemove))]
public abstract class ReplyMarkup
{
    /// <summary>Use this to remove a previous reply keyboard</summary>
    public static readonly ReplyKeyboardRemove RemoveKeyboard = new();

    /// <summary>Use this to switch the user into replying mode to this bot message</summary>
    public static readonly ForceReplyMarkup ForceReply = new();

    /// <summary>Generates a reply keyboard markup with one button</summary>
    /// <param name="text">Button text</param>
    public static implicit operator ReplyMarkup?(string? text) => text is null ? default
        : new ReplyKeyboardMarkup(text) { ResizeKeyboard = true };

    /// <summary>Generates a reply keyboard markup with multiple buttons on one row</summary>
    /// <param name="texts">Buttons text</param>
    public static implicit operator ReplyMarkup?(string[]? texts) => texts is null ? default
        : new ReplyKeyboardMarkup(texts.Select(t => new KeyboardButton(t))) { ResizeKeyboard = true };

    /// <summary>Generates a reply keyboard markup with multiple buttons on one row</summary>
    /// <param name="texts">Buttons text</param>
    public static implicit operator ReplyMarkup?(List<string>? texts) => texts is null ? default
        : new ReplyKeyboardMarkup(texts.Select(t => new KeyboardButton(t))) { ResizeKeyboard = true };

    /// <summary>Generates a reply keyboard markup with multiple rows of buttons</summary>
    /// <param name="textRows">Rows of buttons text</param>
    public static implicit operator ReplyMarkup?(List<List<string>>? textRows) => textRows is null ? default
        : new ReplyKeyboardMarkup(textRows.Select(texts => texts.Select(t => new KeyboardButton(t)))) { ResizeKeyboard = true };

    /// <summary>Generates a reply keyboard markup with multiple rows of buttons</summary>
    /// <param name="textRows">Rows of buttons text</param>
    public static implicit operator ReplyMarkup?(string[][]? textRows) => textRows is null ? default
        : new ReplyKeyboardMarkup(textRows.Select(texts => texts.Select(t => new KeyboardButton(t)))) { ResizeKeyboard = true };


    /// <summary>Generates a reply keyboard markup with one button</summary>
    /// <param name="button">Reply keyboard button</param>
    public static implicit operator ReplyMarkup?(KeyboardButton? button) => button is null ? default
        : new ReplyKeyboardMarkup(button) { ResizeKeyboard = true };

    /// <summary>Generates a reply keyboard markup with multiple buttons on one row</summary>
    /// <param name="buttons">Reply keyboard buttons</param>
    public static implicit operator ReplyMarkup?(KeyboardButton[]? buttons) => buttons is null ? default
        : new ReplyKeyboardMarkup([buttons]) { ResizeKeyboard = true };

    /// <summary>Generates a reply keyboard markup with multiple buttons on one row</summary>
    /// <param name="buttons">Reply keyboard buttons</param>
    public static implicit operator ReplyMarkup?(List<KeyboardButton>? buttons) => buttons is null ? default
        : new ReplyKeyboardMarkup(buttons) { ResizeKeyboard = true };

    /// <summary>Generates a reply keyboard markup with multiple rows of buttons</summary>
    /// <param name="buttonRows">Rows of reply keyboard buttons</param>
    public static implicit operator ReplyMarkup?(List<List<KeyboardButton>>? buttonRows) => buttonRows is null ? default
        : new ReplyKeyboardMarkup(buttonRows) { ResizeKeyboard = true };

    /// <summary>Generates a reply keyboard markup with multiple rows of buttons</summary>
    /// <param name="buttonRows">Rows of reply keyboard buttons</param>
    public static implicit operator ReplyMarkup?(IEnumerable<KeyboardButton>[]? buttonRows) => buttonRows is null ? default
        : new ReplyKeyboardMarkup(buttonRows) { ResizeKeyboard = true };


    /// <summary>Generate an inline keyboard markup with one button</summary>
    /// <param name="button">Inline keyboard button</param>
    [return: NotNullIfNotNull(nameof(button))]
    public static implicit operator ReplyMarkup?(InlineKeyboardButton button) => button is null ? default
        : new InlineKeyboardMarkup(button);

    /// <summary>Generate an inline keyboard markup with multiple buttons on one row</summary>
    /// <param name="buttons">Inline keyboard buttons</param>
    [return: NotNullIfNotNull(nameof(buttons))]
    public static implicit operator ReplyMarkup?(InlineKeyboardButton[]? buttons) => buttons is null ? default
        : new InlineKeyboardMarkup(buttons);

    /// <summary>Generate an inline keyboard markup with multiple buttons on one row</summary>
    /// <param name="buttons">Inline keyboard buttons</param>
    [return: NotNullIfNotNull(nameof(buttons))]
    public static implicit operator ReplyMarkup?(List<InlineKeyboardButton>? buttons) => buttons is null ? default
        : new InlineKeyboardMarkup(buttons);

    /// <summary>Generate an inline keyboard markup with multiple rows of buttons</summary>
    /// <param name="buttonRows">Rows of inline keyboard buttons</param>
    [return: NotNullIfNotNull(nameof(buttonRows))]
    public static implicit operator ReplyMarkup?(IEnumerable<InlineKeyboardButton>[]? buttonRows) => buttonRows is null ? default
        : new InlineKeyboardMarkup(buttonRows);

    /// <summary>Generate an inline keyboard markup with multiple rows of buttons</summary>
    /// <param name="buttonRows">Rows of inline keyboard buttons</param>
    [return: NotNullIfNotNull(nameof(buttonRows))]
    public static implicit operator ReplyMarkup?(List<List<InlineKeyboardButton>>? buttonRows) => buttonRows is null ? default
        : new InlineKeyboardMarkup(buttonRows);


    /// <summary>Generate an inline keyboard markup with one button</summary>
    /// <param name="button">Inline keyboard button (text, callback/url)</param>
    [return: NotNullIfNotNull(nameof(button))]
    public static implicit operator ReplyMarkup((string text, string callbackDataOrUrl) button)
        => new InlineKeyboardMarkup(button);

    /// <summary>Generate an inline keyboard markup with multiple buttons on one row</summary>
    /// <param name="buttons">Inline keyboard buttons (text, callback/url)</param>
    [return: NotNullIfNotNull(nameof(buttons))]
    public static implicit operator ReplyMarkup((string text, string callbackDataOrUrl)[] buttons)
        => new InlineKeyboardMarkup(buttons.Select(tuple => (InlineKeyboardButton)tuple));

    /// <summary>Generate an inline keyboard markup with multiple rows of buttons</summary>
    /// <param name="buttonRows">Rows of inline keyboard buttons (text, callback/url)</param>
    [return: NotNullIfNotNull(nameof(buttonRows))]
    public static implicit operator ReplyMarkup((string text, string callbackDataOrUrl)[][] buttonRows)
        => new InlineKeyboardMarkup(buttonRows.Select(buttons => buttons.Select(tuple => (InlineKeyboardButton)tuple)));
} 
