namespace Telegram.Bot.Requests.Abstractions;

/// <summary>
/// Represents a request to Bot API
/// </summary>
/// <typeparam name="TResponse">Type of result expected in result</typeparam>
// ReSharper disable once UnusedTypeParameter
public interface IRequest<TResponse> : IRequest
{ }