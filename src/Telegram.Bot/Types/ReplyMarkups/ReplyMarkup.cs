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
    /// <summary>Generates a reply keyboard markup with one button</summary>
    /// <param name="text">Button's text</param>
    public static implicit operator ReplyMarkup?(string? text) => text is null ? default : new ReplyKeyboardMarkup(text) { ResizeKeyboard = true };

    /// <summary>Generates a reply keyboard markup with multiple buttons on one row</summary>
    /// <param name="texts">Texts of buttons</param>
    public static implicit operator ReplyMarkup?(string[]? texts) => texts is null ? default : new[] { texts };

    /// <summary>Generates a reply keyboard markup with multiple buttons</summary>
    /// <param name="textsItems">Texts of buttons</param>
    public static implicit operator ReplyMarkup?(string[][]? textsItems) => textsItems is null ? default
        : new ReplyKeyboardMarkup(textsItems.Select(texts => texts.Select(t => new KeyboardButton(t)).ToList()).ToList()) { ResizeKeyboard = true };

    /// <summary>Generates a reply keyboard markup with one button</summary>
    /// <param name="button">Keyboard button</param>
    public static implicit operator ReplyMarkup?(KeyboardButton? button) => button is null ? default : new ReplyKeyboardMarkup(button) { ResizeKeyboard = true };

    /// <summary>Generates a reply keyboard markup with multiple buttons on one row</summary>
    /// <param name="buttons">Keyboard buttons</param>
    public static implicit operator ReplyMarkup?(KeyboardButton[]? buttons) => buttons is null ? default : new ReplyKeyboardMarkup([buttons]) { ResizeKeyboard = true };

    /// <summary>Generates a reply keyboard markup with multiple buttons</summary>
    /// <param name="buttons">Keyboard buttons</param>
    public static implicit operator ReplyMarkup?(IEnumerable<KeyboardButton>[]? buttons) => buttons is null ? default : new ReplyKeyboardMarkup(buttons) { ResizeKeyboard = true };

    /// <summary>Generate an inline keyboard markup from a single button</summary>
    /// <param name="button">Inline keyboard button</param>
    [return: NotNullIfNotNull(nameof(button))]
    public static implicit operator ReplyMarkup?(InlineKeyboardButton? button) => button is null ? default : new InlineKeyboardMarkup(button);

    /// <summary>Generate an inline keyboard markup from multiple buttons on 1 row</summary>
    /// <param name="inlineKeyboard">Keyboard buttons</param>
    [return: NotNullIfNotNull(nameof(inlineKeyboard))]
    public static implicit operator ReplyMarkup?(InlineKeyboardButton[]? inlineKeyboard) => inlineKeyboard is null ? default : new InlineKeyboardMarkup(inlineKeyboard);

    /// <summary>Generate an inline keyboard markup from multiple buttons</summary>
    /// <param name="inlineKeyboard">Keyboard buttons</param>
    [return: NotNullIfNotNull(nameof(inlineKeyboard))]
    public static implicit operator ReplyMarkup?(IEnumerable<InlineKeyboardButton>[]? inlineKeyboard) => inlineKeyboard is null ? default : new InlineKeyboardMarkup(inlineKeyboard);
}
