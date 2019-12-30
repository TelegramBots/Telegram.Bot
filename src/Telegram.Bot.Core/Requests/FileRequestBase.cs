using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
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
        /// <param name="jsonConverter"></param>
        /// <param name="methodName">Bot API method</param>
        protected FileRequestBase(ITelegramBotJsonConverter jsonConverter, string methodName)
            : base(jsonConverter, methodName)
        { }

        /// <summary>
        /// Initializes an instance of request
        /// </summary>
        /// <param name="jsonConverter"></param>
        /// <param name="methodName">Bot API method</param>
        /// <param name="method">HTTP method to use</param>
        protected FileRequestBase(ITelegramBotJsonConverter jsonConverter, string methodName, HttpMethod method)
            : base(jsonConverter, methodName, method)
        { }

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="fileParameterName"></param>
        /// <param name="inputFile"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        protected async ValueTask<MultipartFormDataContent> ToMultipartFormDataContentAsync(string fileParameterName,
            InputFileStream inputFile, CancellationToken ct)
        {
            var multipartContent = await GenerateMultipartFormDataContent(ct, fileParameterName);

            multipartContent.AddStreamContent(inputFile.Content, fileParameterName, inputFile.FileName);

            return multipartContent;
        }

        /// <summary>
        /// ToDo
        /// </summary>
        /// <param name="ct">Cancellation token used for cancellation of the multipart form-data generation.</param>
        /// <param name="exceptPropertyNames">Property names to remove from multipart form-data content.</param>
        /// <returns></returns>
        protected async ValueTask<MultipartFormDataContent> GenerateMultipartFormDataContent(
            CancellationToken ct, params string[] exceptPropertyNames)
        {
            var multipartContent = new MultipartFormDataContent(Guid.NewGuid().ToString() + DateTime.UtcNow.Ticks);

            var nodes = await JsonConverter.ToNodesAsync(this, exceptPropertyNames, ct);

            foreach (var strContent in nodes)
                multipartContent.Add(strContent.Value, strContent.Key);

            return multipartContent;
        }
    }
}
