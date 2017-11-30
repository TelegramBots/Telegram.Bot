﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types.ReplyMarkups
{
    /// <summary>
    /// Defines how clients display a reply interface to the user
    /// </summary>
    /// <seealso cref="Telegram.Bot.Types.ReplyMarkups.IReplyMarkup" />
    [JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public abstract class ReplyMarkup : IReplyMarkup
    {
        /// <summary>
        /// Optional. Use this parameter if you want to show the keyboard to specific users only. Targets: 1) users that are @mentioned in the text of the Description object; 2) if the bot's message is a reply (has <see cref="Message.ReplyToMessage"/>), sender of the original message.
        /// Example: A user requests to change the bot's language, bot replies to the request with a keyboard to select the new language. Other users in the group don't see the keyboard.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public bool Selective { get; set; }
    }
}
