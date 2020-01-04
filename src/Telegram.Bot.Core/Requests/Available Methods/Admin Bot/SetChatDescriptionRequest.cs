using System.Diagnostics.CodeAnalysis;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Change the description of a supergroup or a channel. The bot must be an administrator in the chat for this to work and must have the appropriate admin rights.
    /// </summary>
    public sealed class SetChatDescriptionRequest : ChatIdRequestBase<bool>
    {
        /// <summary>
        /// New chat Description, 0-255 characters
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Initializes a new request with <see cref="ChatId"/> set to 0 and <see cref="Description"/> set to <see langword="null"/>
        /// </summary>
        public SetChatDescriptionRequest() : this(0)
        {
        }

        /// <summary>
        /// Initializes a new request with <see cref="ChatId"/> and new <see cref="Description"/>
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="description">New chat Description, 0-255 characters</param>
        public SetChatDescriptionRequest([NotNull] ChatId chatId, [AllowNull] string? description = default)
            : base("setChatDescription")
        {
            ChatId = chatId;
            Description = description;
        }
    }
}
