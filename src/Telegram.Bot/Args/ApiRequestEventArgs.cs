using System;
using System.Net.Http;

namespace Telegram.Bot.Args
{
    /// <summary>
    /// Provides data for MakingApiRequest event
    /// </summary>
    public class ApiRequestEventArgs : EventArgs
    {
        /// <summary>
        /// Bot API method name
        /// </summary>
        public string MethodName { get; }

        /// <summary>
        /// HTTP content of the request message
        /// </summary>
        public HttpContent? HttpContent { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="httpContent"></param>
        public ApiRequestEventArgs(string methodName, HttpContent? httpContent = default)
        {
            MethodName = methodName;
            HttpContent = httpContent;
        }
    }
}
