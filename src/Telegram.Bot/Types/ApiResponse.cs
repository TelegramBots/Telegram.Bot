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
        [JsonProperty("ok", Required = Required.Always)]
        public bool Ok { get; set; }

        /// <summary>
        /// Gets the result object.
        /// </summary>
        [JsonProperty("result", Required = Required.Default)]
        public T ResultObject { get; set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        [JsonProperty("description", Required = Required.Default)]
        public string Message { get; set; }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        [JsonProperty("error_code", Required = Required.Default)]
        public int Code { get; set; }

        /// <summary>
        /// Contains information about why a request was unsuccessful.
        /// </summary>
        [JsonProperty("parameters")]
        public ResponseParameters Parameters { get; set; }
    }
}
