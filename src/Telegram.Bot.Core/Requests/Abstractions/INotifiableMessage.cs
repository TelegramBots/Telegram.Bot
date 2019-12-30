namespace Telegram.Bot.Requests.Abstractions
{
    /// <summary>
    /// Represents a message with optional notification
    /// </summary>
    public interface INotifiableMessage
    {
        /// <summary>
        /// Sends the message silently. Users will receive a notification with no sound.
        /// </summary>
        bool DisableNotification { get; set; }
    }
}
