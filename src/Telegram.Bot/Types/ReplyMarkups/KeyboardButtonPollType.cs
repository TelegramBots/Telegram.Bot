// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>This object represents type of a poll, which is allowed to be created and sent when the corresponding button is pressed.</summary>
public partial class KeyboardButtonPollType
{
    /// <summary><em>Optional</em>. If <em>quiz</em> is passed, the user will be allowed to create only polls in the quiz mode. If <em>regular</em> is passed, only regular polls will be allowed. Otherwise, the user will be allowed to create a poll of any type.</summary>
    public PollType? Type { get; set; }

    /// <summary>Implicit conversion to PollType (Type)</summary>
    public static implicit operator PollType?(KeyboardButtonPollType self) => self.Type;
    /// <summary>Implicit conversion from PollType (Type)</summary>
    public static implicit operator KeyboardButtonPollType(PollType? type) => new() { Type = type };
}
