﻿using Telegram.Bot.Types;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Unpin a message in a supergroup or a channel. The bot must be an administrator in the chat for this to work and must have the ‘can_pin_messages’ admin right in the supergroup or ‘can_edit_messages’ admin right in the channel.
    /// </summary>
    public class UnpinChatMessageRequest : RequestBase<bool>
    {
        /// <summary>
        /// Unique identifier for the target chat or username of the target channel (in the format @channelusername)
        /// </summary>
        public ChatId ChatId { get; set; }

        /// <summary>
        /// Initializes a new request with chatId
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel</param>
        public UnpinChatMessageRequest(ChatId chatId, ITelegramBotJsonConverter jsonConverter)
            : base(jsonConverter, "unpinChatMessage")
        {
            ChatId = chatId;
        }
    }
}
