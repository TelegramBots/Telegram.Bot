// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about an ownership change in the chat.</summary>
public partial class ChatOwnerChanged
{
    /// <summary>The new owner of the chat</summary>
    [JsonPropertyName("new_owner")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public User NewOwner { get; set; } = default!;

    /// <summary>Implicit conversion to User (NewOwner)</summary>
    public static implicit operator User(ChatOwnerChanged self) => self.NewOwner;
    /// <summary>Implicit conversion from User (NewOwner)</summary>
    public static implicit operator ChatOwnerChanged(User newOwner) => new() { NewOwner = newOwner };
}
