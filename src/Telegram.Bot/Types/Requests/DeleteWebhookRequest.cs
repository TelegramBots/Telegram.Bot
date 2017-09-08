using System;
using System.Collections.Generic;
using System.Text;

namespace Telegram.Bot.Types.Requests
{
    /// <summary>
    /// Represents a request to telegrams bot API to delete the current webhook
    /// </summary>
    public class DeleteWebhookRequest : ApiRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteWebhookRequest"/> class
        /// </summary>
        public DeleteWebhookRequest() : base("deleteWebhook")
        {

        }
    }
}
