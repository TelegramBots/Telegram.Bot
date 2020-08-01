using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

// ReSharper disable CheckNamespace
namespace Telegram.Bot.Types.Passport
{
    /// <summary>
    /// This object represents an error in the Telegram Passport element which was submitted that should be resolved
    /// by the user.
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
        /// Initializes a new passport element error instance with required parameters
        /// </summary>
        /// <param name="source">Error source</param>
        /// <param name="type">The section of the user's Telegram Passport which has the issue</param>
        /// <param name="message">Error message</param>
        /// <exception cref="ArgumentNullException">if any argument is null</exception>
        protected PassportElementError(string source, string type, string message)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }
    }
}
