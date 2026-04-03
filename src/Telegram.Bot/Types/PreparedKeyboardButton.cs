// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a keyboard button to be used by a user of a Mini App.</summary>
public partial class PreparedKeyboardButton
{
    /// <summary>Unique identifier of the keyboard button</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required string Id { get; set; }

    /// <summary>Initializes an instance of <see cref="PreparedKeyboardButton"/></summary>
    /// <param name="id">Unique identifier of the keyboard button</param>
    [SetsRequiredMembers]
    public PreparedKeyboardButton(string id) => Id = id;

    /// <summary>Instantiates a new <see cref="PreparedKeyboardButton"/></summary>
    public PreparedKeyboardButton() { }

    /// <summary>Implicit conversion to string (Id)</summary>
    public static implicit operator string(PreparedKeyboardButton self) => self.Id;
    /// <summary>Implicit conversion from string (Id)</summary>
    public static implicit operator PreparedKeyboardButton(string id) => new() { Id = id };
}
