namespace Telegram.Bot.Types
{
    /// <summary>
    /// Represents bot API response
    /// </summary>
    /// <typeparam name="TResult">Expected type of operation result</typeparam>
    public class ApiResponse<TResult>
    {
        /// <summary>
        /// Gets a value indicating whether the request was successful.
        /// </summary>
        public bool Ok { get; set; }

        /// <summary>
        /// Gets the result object.
        /// </summary>
        public TResult Result { get; set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Contains information about why a request was unsuccessful.
        /// </summary>
        public ResponseParameters Parameters { get; set; }
    }
}
