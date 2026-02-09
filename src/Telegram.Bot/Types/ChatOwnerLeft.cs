// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary>Describes a service message about the chat owner leaving the chat.</summary>
public partial class ChatOwnerLeft
{
    /// <summary><em>Optional</em>. The user which will be the new owner of the chat if the previous owner does not return to the chat</summary>
    [JsonPropertyName("new_owner")]
    public User? NewOwner { get; set; }

    /// <summary>Implicit conversion to User (NewOwner)</summary>
    public static implicit operator User?(ChatOwnerLeft self) => self.NewOwner;
    /// <summary>Implicit conversion from User (NewOwner)</summary>
    public static implicit operator ChatOwnerLeft(User? newOwner) => new() { NewOwner = newOwner };
}
