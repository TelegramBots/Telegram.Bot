using System.Collections.Generic;
using Newtonsoft.Json;
using Telegram.Bot.Types.InlineQueryResults;

namespace Telegram.Bot.Requests
{
    /// <summary>
    /// Send answers to an inline query. On success, True is returned. No more than 50 results per query are allowed.
    /// </summary>
    public class AnswerInlineQueryRequest : RequestBase<bool>
    {
        private string _nextOffset;
        private string _switchPmText;
        private string _switchPmParameter;

        /// <summary>
        /// Unique identifier for the answered query
        /// </summary>
        public string InlineQueryId { get; set; }

        /// <summary>
        /// A JSON-serialized array of results for the inline query
        /// </summary>
        public IEnumerable<InlineQueryResult> Results { get; set; }

        /// <summary>
        /// The maximum amount of time in seconds that the result of the inline query may be cached on the server. Defaults to 300.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public int? CacheTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsPersonal { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string NextOffset
        {
            get => _nextOffset;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _nextOffset = value;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SwitchPmText
        {
            get => _switchPmText;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _switchPmText = value;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string SwitchPmParameter
        {
            get => _switchPmParameter;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _switchPmParameter = value;
                }
            }
        }

        /// <summary>
        /// Initializes a new request
        /// </summary>
        public AnswerInlineQueryRequest()
            : base("answerInlineQuery")
        { }

        /// <summary>
        /// Initializes a new request with inlineQueryId and an array of <see cref="InlineQueryResult"/>
        /// </summary>
        /// <param name="inlineQueryId">Unique identifier for the answered query</param>
        /// <param name="results">A JSON-serialized array of results for the inline query</param>
        public AnswerInlineQueryRequest(string inlineQueryId, IEnumerable<InlineQueryResult> results)
            : this()
        {
            InlineQueryId = inlineQueryId;
            Results = results;
        }
    }
}
