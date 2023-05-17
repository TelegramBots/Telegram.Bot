using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;

namespace Telegram.Bot.Extensions;

internal static class HttpResponseMessageExtensions
{
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
#if NET6_0_OR_GREATER
            if (contentStream is not null)
            {
                await contentStream.DisposeAsync().ConfigureAwait(false);
            }
#else
            contentStream?.Dispose();
#endif
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static RequestException CreateRequestException(
        HttpResponseMessage httpResponse,
        string message,
        Exception? exception = default
    ) =>
        exception is null
            ? new(
                message: message,
                httpStatusCode: httpResponse.StatusCode
            )
            : new(
                message: message,
                httpStatusCode: httpResponse.StatusCode,
                innerException: exception
            );
}
