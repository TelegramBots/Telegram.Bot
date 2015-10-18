using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// The Api Response
    /// </summary>
    /// <typeparam name="T">The resultant object</typeparam>
    [JsonObject(MemberSerialization.OptIn)]
    public class ApiResponse<T>
    {
        /// <summary>
        /// Gets a value indicating whether the request was successful.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the request was successful and the result of the query can be found in the ‘result’ field, otherwise <c>false</c>.
        /// </value>
        [JsonProperty(PropertyName = "ok", Required = Required.Always)]
        public bool Ok { get; internal set; }

        /// <summary>
        /// Gets the result object.
        /// </summary>
        /// <value>
        /// The result object.
        /// </value>
        [JsonProperty(PropertyName = "result", Required = Required.Default)]
        public T ResultObject { get; internal set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        [JsonProperty(PropertyName = "description", Required = Required.Default)]
        public string Message { get; internal set; }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        [JsonProperty(PropertyName = "error_code", Required = Required.Default)]
        public int Code { get; internal set; }
    }
}
