using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Helpers
{
    /// <summary>
    /// Extension Methods
    /// </summary>
    internal static class Extensions
    {
        private static string EncodeUtf8(this string value) =>
            string.Join(string.Empty, Encoding.UTF8.GetBytes(value).Select(Convert.ToChar));

        internal static void AddStreamContent(
            this MultipartFormDataContent multipartContent,
            Stream content,
            string name,
            string? fileName = default)
        {
            fileName ??= name;
            var contentDisposition = $@"form-data; name=""{name}""; filename=""{fileName}"""
                .EncodeUtf8();

            HttpContent mediaPartContent = new StreamContent(content)
            {
                Headers =
                {
                    {"Content-Type", "application/octet-stream"},
                    {"Content-Disposition", contentDisposition}
                }
            };

            multipartContent.Add(mediaPartContent, name, fileName);
        }

        internal static void AddContentIfInputFileStream(
            this MultipartFormDataContent multipartContent,
            params IInputMedia[] inputMedia)
        {
            foreach (var input in inputMedia)
            {
                if (input.Media.FileType == FileType.Stream)
                {
                    multipartContent.AddStreamContent(input.Media.Content!, input.Media.FileName!);
                }

                var mediaThumb = (input as IInputMediaThumb)?.Thumb;
                if (mediaThumb?.FileType == FileType.Stream)
                {
                    multipartContent.AddStreamContent(mediaThumb.Content!, mediaThumb.FileName!);
                }
            }
        }

        /// <summary>
        /// Streaming JSON deserialization
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> with content</param>
        /// <typeparam name="T">Type of the resulting object</typeparam>
        /// <returns></returns>
        [return: MaybeNull]
        private static T? DeserializeJsonFromStream<T>(this Stream stream)
            where T : class
        {
            if (stream == null || stream.CanRead == false)
                return default;

            using var streamReader = new StreamReader(stream);
            using var jsonTextReader = new JsonTextReader(streamReader);

            var jsonSerializer = new JsonSerializer();
            var searchResult = jsonSerializer.Deserialize<T>(jsonTextReader);

            return searchResult;
        }

        /// <summary>
        /// Deserialize body from HttpContent into <see cref="T"/>
        /// </summary>
        /// <param name="httpResponse"><see cref="HttpResponseMessage"/> instance</param>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> of the response</param>
        /// <typeparam name="T">Type of the resulting object</typeparam>
        /// <returns></returns>
        /// <exception cref="RequestException">
        /// Thrown when body in the <see cref="httpResponse"/> can not be deserialized into <see cref="T"/>
        /// </exception>
        internal static async Task<T?> DeserializeContentAsync<T>(
            this HttpResponseMessage httpResponse,
            HttpStatusCode statusCode)
            where T : class
        {
            T? deserializedObject;
            Stream? contentStream = null;
            try
            {
                contentStream = await httpResponse.Content
                    .ReadAsStreamAsync()
                    .ConfigureAwait(false);

                deserializedObject = contentStream
                    .DeserializeJsonFromStream<T>();
            }
            catch (Exception exception)
            {
                var stringifiedResponse = await httpResponse.Content
                    .ReadAsStringAsync()
                    .ConfigureAwait(false);

                throw new RequestException(
                    "Required properties not found in response.",
                    statusCode,
                    stringifiedResponse,
                    exception
                );
            }
            finally
            {
                contentStream?.Dispose();
            }

            return deserializedObject;
        }
    }
}
