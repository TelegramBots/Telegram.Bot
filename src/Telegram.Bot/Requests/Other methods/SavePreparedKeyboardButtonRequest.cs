// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Requests;

/// <summary>Stores a keyboard button that can be used by a user within a Mini App.<para>Returns: A <see cref="PreparedKeyboardButton"/> object.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class SavePreparedKeyboardButtonRequest() : RequestBase<PreparedKeyboardButton>("savePreparedKeyboardButton"), IUserTargetable
{
    /// <summary>Unique identifier of the target user that can use the button</summary>
    [JsonPropertyName("user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required long UserId { get; set; }

    /// <summary>An object describing the button to be saved. The button must be of the type <em>RequestUsers</em>, <em>RequestChat</em>, or <em>RequestManagedBot</em></summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required KeyboardButton Button { get; set; }
}
