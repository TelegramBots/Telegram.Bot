using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object represents a service message about a voice chat scheduled in the chat.
    /// </summary>
    public class VoiceChatScheduled
    {
        /// <summary>
        /// Point in time when the voice chat is supposed to be started by a chat administrator
        /// </summary>
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime StartDate { get; set; }
    }
}
