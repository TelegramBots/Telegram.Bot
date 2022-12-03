using System.Net.Http;
using Telegram.Bot.Requests.Abstractions;

namespace Telegram.Bot.Args;

/// <summary>
/// Provides data for MakingApiRequest event
/// </summary>
public class ApiRequestEventArgs : EventArgs
{
    /// <summary>
    /// Bot API Request
    /// </summary>
    public IRequest Request { get; }

    /// <summary>
    /// HTTP Request Message
    /// </summary>
    public HttpRequestMessage? HttpRequestMessage { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="request"></param>
    /// <param name="httpRequestMessage"></param>
    public ApiRequestEventArgs(IRequest request, HttpRequestMessage? httpRequestMessage = default)
    {
        Request = request;
        HttpRequestMessage = httpRequestMessage;
    }
}