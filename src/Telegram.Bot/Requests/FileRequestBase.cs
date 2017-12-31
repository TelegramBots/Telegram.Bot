using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Helpers;
using Telegram.Bot.Types.InputFiles;

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
        /// ToDo
        /// </summary>
        /// <param name="fileParameterName"></param>
        /// <param name="inputFile"></param>
        /// <returns></returns>
        protected MultipartFormDataContent ToMultipartFormDataContent(string fileParameterName, InputFileStream inputFile)
        {
            var multipartContent = GenerateMultipartFormDataContent(fileParameterName);

            multipartContent.AddStreamContent(inputFile.Content, fileParameterName, inputFile.FileName);

            return multipartContent;
        }

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="exceptPropertyNames"></param>
        /// <returns></returns>
        protected MultipartFormDataContent GenerateMultipartFormDataContent(params string[] exceptPropertyNames)
        {
            var multipartContent = new MultipartFormDataContent(Guid.NewGuid().ToString() + DateTime.UtcNow.Ticks);

            var stringContents = JObject.FromObject(this)
                .Properties()
                .Where(prop => exceptPropertyNames?.Contains(prop.Name) == false)
                .Select(prop => new
                {
                    prop.Name,
                    Content = new StringContent(prop.Value.ToString())
                });
            foreach (var strContent in stringContents)
                multipartContent.Add(strContent.Content, strContent.Name);

            return multipartContent;
        }

        /// <summary>
        /// Creates MultipartFormData request
        /// </summary>
        /// <param name="parameters">Request parameters</param>
        /// <returns>Content of HTTP request</returns>
        [Obsolete]
        protected HttpContent GetMultipartContent(IDictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }
    }
}
