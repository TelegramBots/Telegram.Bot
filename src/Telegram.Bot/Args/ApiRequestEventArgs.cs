namespace Telegram.Bot.Args;

/// <summary>Provides data for MakingApiRequest event</summary>
/// <param name="request"></param>
/// <param name="httpRequestMessage"></param>
public class ApiRequestEventArgs(IRequest request, HttpRequestMessage? httpRequestMessage = default) : EventArgs
{
    /// <summary>Bot API Request</summary>
    public IRequest Request { get; } = request;

    /// <summary>HTTP Request Message</summary>
    public HttpRequestMessage? HttpRequestMessage { get; } = httpRequestMessage;
}
