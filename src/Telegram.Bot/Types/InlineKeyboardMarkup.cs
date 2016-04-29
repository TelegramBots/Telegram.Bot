﻿using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents an inline keyboard that appears right next to the message it belongs to.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class InlineKeyboardMarkup : Interfaces.IReplyMarkup
    {
        /// <summary>
        /// Array of button rows, each represented by an Array of InlineKeyboardButton objects
        /// </summary>
        [JsonProperty("inline_keyboard", Required = Required.Always)]
        public InlineKeyboardButton[][] InlineKeyboard { get; set; }
    }
}
