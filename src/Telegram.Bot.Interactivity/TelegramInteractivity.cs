using Interactivity.Exceptions;
using Interactivity.Extensions;
using Interactivity.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace Interactivity
{
    public partial class TelegramInteractivity
    {
        /// <summary>
        /// The client associated with this interactivity object.
        /// </summary>
        private readonly TelegramBotClient client;
        /// <summary>
        /// The current Message Interactivity processes with this client.
        /// </summary>
        public List<InteractivityProcess<Message>> CurrentMessageInteractivityObjects { get; set; } = new List<InteractivityProcess<Message>>();
        /// <summary>
        /// The configuration of this process.
        /// </summary>
        public InteractivityConfiguration Configuration { get; set; }

        /// <summary>
        /// Create a new telegram interactivity object.
        /// </summary>
        /// <param name="client">The client to associate with this object.</param>
        /// <param name="configuration">This object's configuration.</param>
        public TelegramInteractivity(TelegramBotClient client, InteractivityConfiguration configuration)
        {
            this.client = client;
            this.Configuration = configuration;
            Setup();
        }

        /// <summary>
        /// Setup the handler.
        /// </summary>
        private void Setup()
        {
            client.OnMessage += (sender, e) =>
            {
                Handlers.InteractivityMessageHandler.OnMessageSent(sender, e, this);
            };
        }

        /// <summary>
        /// Wait for a message with a condition.
        /// </summary>
        /// <param name="chat">What Chat to wait for a message in.</param>
        /// <param name="author">Which user to wait for a message from.</param>
        /// <param name="condition">The message's condition. If null, all messages will be accepted as a result.</param>
        /// <param name="defaultTimeoutTimeOverride">Overrides the TelegramInteractivity's configuration's DefaultTimeOutTime.</param>
        /// <returns></returns>
        public async Task<InteractivityResult<Message>> WaitForMessageAsync(
            Chat chat,
            User author,
            Predicate<Message> condition = null,
            TimeSpan? defaultTimeoutTimeOverride = null)
        {
            // Get the interactivity.
            var interactivity = client.GetInteractivity();
            // Check if there is already an ongoing process.
            if (CurrentMessageInteractivityObjects.Any(x => x.Author.Id == author.Id))
            {
                await client.SendTextMessageAsync(chat, interactivity.Configuration.UserAlreadyHasOngoingOperationMessage);
                return new InteractivityResult<Message>(null, false);
            }
            // Get the timeout time.
            var timeOutTime = defaultTimeoutTimeOverride.HasValue ? defaultTimeoutTimeOverride :
                interactivity.Configuration.DefaultTimeOutTime;
            // Create a new cancellation token for the time out thread.
            var cancellationTokenSource = new CancellationTokenSource();
            // Create a new process object.
            var iObject = new InteractivityProcess<Message>(cancellationTokenSource, chat, author,condition);
            // Add it to the current processes.
            interactivity.CurrentMessageInteractivityObjects
                .Add(iObject);
            // If the timespan is not infinite/null, create a new thread to cancel the process after the time.
            if (timeOutTime.HasValue)
            {
                var timeSpanValue = timeOutTime.Value;
                new Thread(() =>
                {
                    // Wait until cancelled or time has passed.
                    var cancelled = cancellationTokenSource.Token.WaitHandle.WaitOne(timeSpanValue);
                    // If it hasn't been cancelled
                    if (!cancelled)
                    {
                        CurrentMessageInteractivityObjects.Remove(iObject);
                        iObject.InteractivityResult = new InteractivityResult<Message>(null, true);
                        iObject.WaitHandle.Set();
                    }
                }).Start();
            }
            // Wait until a result gets passed.
            await iObject.WaitHandle.WaitAsync();
            return iObject.InteractivityResult;
        }

        public void ClearOnGoingProcesses()
        {
            CurrentMessageInteractivityObjects.Clear();
        }

    }
}
