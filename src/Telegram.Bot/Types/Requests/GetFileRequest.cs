using System.Collections.Generic;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to get a file
    /// </summary>
    public class GetFileRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFileRequest"/> class
        /// </summary>
        /// <param name="fileId">File identifier</param>
        public GetFileRequest(string fileId) : base("getFile", new Dictionary<string, object> { { "file_id", fileId } })
        {

        }
    }
}
