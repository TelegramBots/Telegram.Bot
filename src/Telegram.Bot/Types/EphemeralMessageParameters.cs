// GENERATED FILE - DO NOT MODIFY MANUALLY
namespace Telegram.Bot.Types;

/// <summary></summary>
public partial class EphemeralMessageParameters
{
    /// <summary><em>Optional</em>. Identifier of the user who will receive the message; required unless the message is sent as an ephemeral reply to a callback query. If omitted, then the parameter <em>ChatId</em> of the method is ignored. If specified together with <see cref="CallbackQueryId">CallbackQueryId</see>, then it must be the identifier of the user who pressed the button. Must be omitted for messages sent in chats where the bot isn't a member, unless the message is a reply to an ephemeral message. It is not guaranteed that the user will receive the message, especially if they are offline. See <a href="https://core.telegram.org/bots/api#ephemeral-messages-and-commands">here</a> for more details.</summary>
    [JsonPropertyName("receiver_user_id")]
    public long? ReceiverUserId { get; set; }

    /// <summary><em>Optional</em>. Identifier of the callback query which triggered the message, if any. If specified and <see cref="ReceiverUserId">ReceiverUserId</see> is omitted, then the message is sent as an ephemeral reply to the callback query and will be shown only to the user who pressed the button. Ephemeral replies can't be sent to callback queries from messages in channel direct messages chats. The bot must still answer the callback query using <see cref="TelegramBotClientExtensions.AnswerCallbackQuery">AnswerCallbackQuery</see>.</summary>
    [JsonPropertyName("callback_query_id")]
    public string? CallbackQueryId { get; set; }

    /// <summary><em>Optional</em>. Pass <see langword="true"/> if the ephemeral message must be shown in place of the original message. Must be <see langword="false"/> for callback queries from ephemeral messages, which must be edited using regular <em>editEphemeralMessage…</em> methods.</summary>
    [JsonPropertyName("replace_callback_query_message")]
    public bool ReplaceCallbackQueryMessage { get; set; }
}
