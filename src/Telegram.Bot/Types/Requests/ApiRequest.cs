using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to be sent to telegrams bot API
    /// </summary>
    public class ApiRequest : IApiRequest
    {
        /// <summary>
        /// The encoding that should be used for this request
        /// </summary>
        public RequestEncoding Encoding => FileStream == null ? RequestEncoding.Json : RequestEncoding.Multipart;
        /// <summary>
        /// The method of this request
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// The parameters of this object
        /// </summary>
        public Dictionary<string, object> Parameters { get; set; }
        /// <summary>
        /// The file to be sent. Only present if the File is sent as a stream.
        /// </summary>
        public Stream FileStream { get; set; }
        /// <summary>
        /// The name of the file. Only present if <see cref="FileStream"/> is present.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// The parameter name of the file. Only present if <see cref="FileStream"/> is present.
        /// </summary>
        public string FileParameterName { get; set; }
    }

    /// <summary>
    /// The encoding that should be used for this request
    /// </summary>
    public enum RequestEncoding
    {
        /// <summary>
        /// application/json encoding
        /// </summary>
        Json,
        /// <summary>
        /// multipart/form-data encoding
        /// </summary>
        Multipart
    }
}
