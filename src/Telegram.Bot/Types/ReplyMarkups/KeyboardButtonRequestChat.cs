// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types.ReplyMarkups;

/// <summary>This object defines the criteria used to request a suitable chat. Information about the selected chat will be shared with the bot when the corresponding button is pressed. The bot will be granted requested rights in the chat if appropriate. <a href="https://core.telegram.org/bots/features#chat-and-user-selection">More about requesting chats »</a>.</summary>
public partial class KeyboardButtonRequestChat
{
    /// <summary>Signed 32-bit identifier of the request, which will be received back in the <see cref="ChatShared"/> object. Must be unique within the message</summary>
    [JsonPropertyName("request_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public required int RequestId { get; set; }

    /// <summary>Pass <see langword="true"/> to request a channel chat, pass <see langword="false"/> to request a group or a supergroup chat.</summary>
    [JsonPropertyName("chat_is_channel")]
    public required bool ChatIsChannel { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> to request a forum supergroup, pass <see langword="false"/> to request a non-forum chat. If not specified, no additional restrictions are applied.</summary>
    [JsonPropertyName("chat_is_forum")]
    public bool? ChatIsForum { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> to request a supergroup or a channel with a username, pass <see langword="false"/> to request a chat without a username. If not specified, no additional restrictions are applied.</summary>
    [JsonPropertyName("chat_has_username")]
    public bool? ChatHasUsername { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> to request a chat owned by the user. Otherwise, no additional restrictions are applied.</summary>
    [JsonPropertyName("chat_is_created")]
    public bool ChatIsCreated { get; set; }

    /// <summary><em>Optional</em>. An object listing the required administrator rights of the user in the chat. The rights must be a superset of <see cref="BotAdministratorRights">BotAdministratorRights</see>. If not specified, no additional restrictions are applied.</summary>
    [JsonPropertyName("user_administrator_rights")]
    public ChatAdministratorRights? UserAdministratorRights { get; set; }

    /// <summary><em>Optional</em>. An object listing the required administrator rights of the bot in the chat. The rights must be a subset of <see cref="UserAdministratorRights">UserAdministratorRights</see>. If not specified, no additional restrictions are applied.</summary>
    [JsonPropertyName("bot_administrator_rights")]
    public ChatAdministratorRights? BotAdministratorRights { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> to request a chat with the bot as a member. Otherwise, no additional restrictions are applied.</summary>
    [JsonPropertyName("bot_is_member")]
    public bool BotIsMember { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> to request the chat's title</summary>
    [JsonPropertyName("request_title")]
    public bool RequestTitle { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> to request the chat's username</summary>
    [JsonPropertyName("request_username")]
    public bool RequestUsername { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> to request the chat's photo</summary>
    [JsonPropertyName("request_photo")]
    public bool RequestPhoto { get; set; }

    /// <summary>Initializes an instance of <see cref="KeyboardButtonRequestChat"/></summary>
    /// <param name="requestId">Signed 32-bit identifier of the request, which will be received back in the <see cref="ChatShared"/> object. Must be unique within the message</param>
    /// <param name="chatIsChannel">Pass <see langword="true"/> to request a channel chat, pass <see langword="false"/> to request a group or a supergroup chat.</param>
    [SetsRequiredMembers]
    public KeyboardButtonRequestChat(int requestId, bool chatIsChannel)
    {
        RequestId = requestId;
        ChatIsChannel = chatIsChannel;
    }

    /// <summary>Instantiates a new <see cref="KeyboardButtonRequestChat"/></summary>
    public KeyboardButtonRequestChat() { }
}
