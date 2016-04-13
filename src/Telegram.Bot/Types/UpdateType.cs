namespace Telegram.Bot.Types
{
    /// <summary>
    /// The type of an Update
    /// </summary>
    public enum UpdateType
    {
        UnkownUpdate = 0,
        MessageUpdate,
        InlineQueryUpdate,
        ChosenInlineResultUpdate,
        CallbackQueryUpdate,
    }
}
