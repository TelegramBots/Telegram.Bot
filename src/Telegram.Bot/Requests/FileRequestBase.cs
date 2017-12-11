using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Telegram.Bot.Helpers;
using Telegram.Bot.Types;

namespace Telegram.Bot.Requests
{

    /// <summary>
    /// Represents an API request with a file
    /// </summary>
    /// <typeparam name="TResponse">Type of result expected in result</typeparam>
    public abstract class FileRequestBase<TResponse> : RequestBase<TResponse>
    {
        /// <summary>
        /// Initializes an instance of request
        /// </summary>
        /// <param name="methodName">Bot API method</param>
        protected FileRequestBase(string methodName)
            : base(methodName)
        { }

        /// <summary>
        /// Initializes an instance of request
        /// </summary>
        /// <param name="methodName">Bot API method</param>
        /// <param name="method">HTTP method to use</param>
        protected FileRequestBase(string methodName, HttpMethod method)
            : base(methodName, method)
        { }

        /// <summary>
        /// Creates MultipartFormData request
        /// </summary>
        /// <param name="parameters">Request parameters</param>
        /// <param name="serializerSettings">JSON serialization setting</param>
        /// <returns>Content of HTTP request</returns>
        protected HttpContent GetMultipartContent(
            IDictionary<string, object> parameters,
            JsonSerializerSettings serializerSettings)
        {
            var multipartContent = new MultipartFormDataContent(Guid.NewGuid().ToString() + DateTime.UtcNow.Ticks);

            foreach (var parameter in parameters.Where(parameter => parameter.Value != null))
            {
                if (parameter.Value is FileToSend fts)
                {
                    multipartContent.AddStreamContent(fts.Content, parameter.Key, fts.Filename);
                }
                else
                {
                    var content = ConvertParameterValue(parameter.Value, serializerSettings);
                    multipartContent.Add(content, parameter.Key);
                }
            }

            return multipartContent;
        }

        private static HttpContent ConvertParameterValue(object value, JsonSerializerSettings serializerSettings)
        {
            HttpContent httpContent;

            switch (value)
            {
                case string str: // Prevent escaping back-slash character: "\r\n" should not be "\\r\\n"
                    httpContent = new StringContent(str);
                    break;
                default:
                    httpContent = new StringContent(JsonConvert.SerializeObject(value, serializerSettings).Trim('"'));
                    break;
            }

            return httpContent;
        }
    }
}
