using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// This object represents an error in the Telegram Passport element which was submitted that should be resolved by the user.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public abstract class PassportElementError
    {
        /// <summary>
        /// Error source.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Type { get; }

        /// <summary>
        /// The section of the user's Telegram Passport which has the error.
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Source { get; }

        /// <summary>
        /// Error message
        /// </summary>
        [JsonProperty(Required = Required.Always)]
        public string Message { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type">The section of the user's Telegram Passport which has the issue</param>
        /// <param name="source">Error source</param>
        /// <param name="message">Error message</param>
        protected PassportElementError(string type, string source, string message)
        {
            Type = type;
            Source = source;
            Message = message;
        }
    }
}
