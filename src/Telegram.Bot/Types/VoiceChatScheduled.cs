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
        /// Point in time (Unix timestamp) when the voice chat is supposed to be started by a chat administrator
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? StartDate { get; set; }
    }
}
