using System.Runtime.Serialization;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// The type of an Update
    /// </summary>
    public enum UpdateType
    {
        [EnumMember(Value = "unknown_update")]
        UnkownUpdate = 0,

        [EnumMember(Value = "message_update")]
        MessageUpdate,

        [EnumMember(Value = "inline_query_update")]
        InlineQueryUpdate,

        [EnumMember(Value = "chosen_inline_result_update")]
        ChosenInlineResultUpdate,

        [EnumMember(Value = "callback_query_update")]
        CallbackQueryUpdate,

        [EnumMember(Value = "edited_message")]
        EditedMessage,
    }
}
