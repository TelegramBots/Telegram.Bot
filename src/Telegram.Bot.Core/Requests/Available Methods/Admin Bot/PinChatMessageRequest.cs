﻿using Telegram.Bot.Requests.Abstractions;
using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Pin a message in a supergroup or a channel. The bot must be an administrator in the chat for this to work and must have the ‘can_pin_messages’ admin right in the supergroup or ‘can_edit_messages’ admin right in the channel.
    /// </summary>
    public class PinChatMessageRequest : RequestBase<bool>, INotifiableMessage
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; }

        /// <summary>
        /// Identifier of a message to pin
        /// </summary>
        public int MessageId { get; }

        /// <inheritdoc />
        public bool DisableNotification { get; set; }

        /// <summary>
        /// Initializes a new request with chatId
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        /// <param name="messageId">Identifier of a message to pin</param>
        public PinChatMessageRequest(ChatId chatId, int messageId)
            : base("pinChatMessage")
        {
            ChatId = chatId;
            MessageId = messageId;
        }
    }
}
