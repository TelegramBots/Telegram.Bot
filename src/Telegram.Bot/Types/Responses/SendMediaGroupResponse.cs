using Telegram.Bot.Types.Requests;
using Telegram.Bot.Types.Responses;

// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    /// <summary>
    /// Response of <see cref="SendMediaGroupRequest"/> request
    /// </summary>
    public class SendMediaGroupResponse : JsonArrayResponse<Message>
    {
    }
}
