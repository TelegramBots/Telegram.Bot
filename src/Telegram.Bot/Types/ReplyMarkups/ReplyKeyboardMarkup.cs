using System.Collections.Generic;
using System.Linq;

namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>
/// Represents a custom keyboard with reply options
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class ReplyKeyboardMarkup : ReplyMarkupBase
{
    /// <summary>
    /// Array of button rows, each represented by an Array of KeyboardButton objects
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public IEnumerable<IEnumerable<KeyboardButton>> Keyboard { get; set; }

    /// <summary>
    /// Optional. Requests clients to always show the keyboard when the regular keyboard is hidden. Defaults to
    /// <see langword="false"/>, in which case the custom keyboard can be hidden and opened with a keyboard icon.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? IsPersistent { get; set; }

    /// <summary>
    /// Optional. Requests clients to resize the keyboard vertically for optimal fit (e.g., make the keyboard smaller if there are just two rows of buttons). Defaults to false, in which case the custom keyboard is always of the same height as the app's standard keyboard.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? ResizeKeyboard { get; set; }

    /// <summary>
    /// Optional. Requests clients to hide the keyboard as soon as it's been used. The keyboard will still be available, but clients will automatically display the usual letter-keyboard in the chat – the user can press a special button in the input field to see the custom keyboard again. Defaults to false.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? OneTimeKeyboard { get; set; }

    /// <summary>
    /// Optional. The placeholder to be shown in the input field when the keyboard is active; 1-64 characters
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? InputFieldPlaceholder { get; set; }

    /// <summary>
    /// Initializes a new instance of <see cref="ReplyKeyboardMarkup"/> with one button
    /// </summary>
    /// <param name="button">Button on keyboard</param>
    public ReplyKeyboardMarkup(KeyboardButton button)
        : this(new[] { button })
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="ReplyKeyboardMarkup"/>
    /// </summary>
    /// <param name="keyboardRow">The keyboard row.</param>
    public ReplyKeyboardMarkup(IEnumerable<KeyboardButton> keyboardRow)
        : this(new[] { keyboardRow })
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReplyKeyboardMarkup"/> class.
    /// </summary>
    /// <param name="keyboard">The keyboard.</param>
    [JsonConstructor]
    public ReplyKeyboardMarkup(IEnumerable<IEnumerable<KeyboardButton>> keyboard)
    {
        Keyboard = keyboard;
    }

    /// <summary>
    /// Generates a reply keyboard markup with one button
    /// </summary>
    /// <param name="text">Button's text</param>
    public static implicit operator ReplyKeyboardMarkup?(string? text) =>
        text is null
            ? default
            : new(new[] { new KeyboardButton(text) });

    /// <summary>
    /// Generates a reply keyboard markup with multiple buttons on one row
    /// </summary>
    /// <param name="texts">Texts of buttons</param>
    public static implicit operator ReplyKeyboardMarkup?(string[]? texts) =>
        texts is null
            ? default
            : new[] { texts };

    /// <summary>
    /// Generates a reply keyboard markup with multiple buttons
    /// </summary>
    /// <param name="textsItems">Texts of buttons</param>
    public static implicit operator ReplyKeyboardMarkup?(string[][]? textsItems) =>
        textsItems is null
            ? default
            : new ReplyKeyboardMarkup(
                textsItems.Select(texts =>
                    texts.Select(t => new KeyboardButton(t))
                ));
}
