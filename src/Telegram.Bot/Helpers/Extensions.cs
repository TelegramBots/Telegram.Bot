using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static string EncodeUtf8(this string value) =>
            new(Encoding.UTF8.GetBytes(value).Select(c => Convert.ToChar(c)).ToArray());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void AddStreamContent(
            this MultipartFormDataContent multipartContent,
            Stream content,
            string name,
            string? fileName = default)
        {
            fileName ??= name;
            var contentDisposition = $@"form-data; name=""{name}""; filename=""{fileName}""".EncodeUtf8();

            var mediaPartContent = new StreamContent(content)
            {
                Headers =
                {
                    {"Content-Type", "application/octet-stream"},
                    {"Content-Disposition", contentDisposition}
                }
            };

            multipartContent.Add(mediaPartContent, name, fileName);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void AddContentIfInputFileStream(
            this MultipartFormDataContent multipartContent,
            params IInputMedia[] inputMedia)
        {
            foreach (var input in inputMedia)
            {
                if (input.Media.FileType == FileType.Stream)
                {
                    multipartContent.AddStreamContent(
                        content: input.Media.Content!,
                        name: input.Media.FileName!
                    );
                }

                if (input is IInputMediaThumb mediaThumb &&
                    mediaThumb.Thumb?.FileType == FileType.Stream)
                {
                    multipartContent.AddStreamContent(
                        content: mediaThumb.Thumb.Content!,
                        name: mediaThumb.Thumb.FileName!
                    );
                }
            }
        }

        /// <summary>
        /// Deserialize body from HttpContent into <typeparamref name="T"/>
        /// </summary>
        /// <param name="httpResponse"><see cref="HttpResponseMessage"/> instance</param>
        /// <param name="guard"></param>
        /// <typeparam name="T">Type of the resulting object</typeparam>
        /// <returns></returns>
        /// <exception cref="RequestException">
        /// Thrown when body in the response can not be deserialized into <typeparamref name="T"/>
        /// </exception>
        [MethodImpl(methodImplOptions: MethodImplOptions.AggressiveInlining)]
        internal static async Task<T> DeserializeContentAsync<T>(
            this HttpResponseMessage httpResponse,
            Func<T, bool> guard)
            where T : class
        {
            Stream? contentStream = null;

            if (httpResponse.Content is null)
            {
                throw new RequestException(
                    message: "Response doesn't contain any content",
                    httpStatusCode: httpResponse.StatusCode
                );
            }

            try
            {
                T? deserializedObject;

                try
                {
                    contentStream = await httpResponse.Content
                        .ReadAsStreamAsync()
                        .ConfigureAwait(continueOnCapturedContext: false);

                    deserializedObject = contentStream
                        .DeserializeJsonFromStream<T>();
                }
                catch (Exception exception)
                {
                    throw CreateRequestException(
                        httpResponse: httpResponse,
                        message: "Required properties not found in response",
                        exception: exception
                    );
                }

                if (deserializedObject is null)
                {
                    throw CreateRequestException(
                        httpResponse: httpResponse,
                        message: "Required properties not found in response"
                    );
                }

                if (guard(deserializedObject))
                {
                    throw CreateRequestException(
                        httpResponse: httpResponse,
                        message: "Required properties not found in response"
                    );
                }

                return deserializedObject;
            }
            finally
            {
                if (contentStream is not null)
                {
#if NETCOREAPP3_1_OR_GREATER
                    await contentStream.DisposeAsync();
#else
                    contentStream.Dispose();
#endif
                }
            }
        }

        /// <summary>
        /// Deserialized JSON in Stream into <typeparamref name="T"/>
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> with content</param>
        /// <typeparam name="T">Type of the resulting object</typeparam>
        /// <returns>Deserialized instance of <typeparamref name="T" /> or <c>null</c></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static T? DeserializeJsonFromStream<T>(this Stream? stream)
            where T : class
        {
            if (stream is null || !stream.CanRead) { return default; }

            using var streamReader = new StreamReader(stream);
            using var jsonTextReader = new JsonTextReader(streamReader);

            var jsonSerializer = JsonSerializer.CreateDefault();
            var searchResult = jsonSerializer.Deserialize<T>(jsonTextReader);

            return searchResult;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T ThrowIfNull<T>([AllowNull] this T value, string parameterName) =>
            value ?? throw new ArgumentNullException(parameterName);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static RequestException CreateRequestException(
            HttpResponseMessage httpResponse,
            string message,
            Exception? exception = default
        ) =>
            exception is null
                ? new RequestException(
                    message: message,
                    httpStatusCode: httpResponse.StatusCode
                )
                : new RequestException(
                    message: message,
                    httpStatusCode: httpResponse.StatusCode,
                    innerException: exception
                );
    }
}
