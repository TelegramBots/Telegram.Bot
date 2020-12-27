using Interactivity.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Telegram.Bot.Types;

namespace Interactivity.Types
{

    public class InteractivityProcess<T>
    {
        /// <summary>
        /// The Chat of this interactivity process.
        /// </summary>
        public Chat Chat { get; set; }
        /// <summary>
        /// This process's condition.
        /// </summary>
        public Predicate<T> Predicate { get; set; }
        /// <summary>
        /// The wait handle of this process.
        /// </summary>
        public AsyncAutoResetEvent WaitHandle { get; } = new AsyncAutoResetEvent();
        /// <summary>
        /// The result of this process.
        /// </summary>
        public InteractivityResult<T> InteractivityResult { get; set; }
        /// <summary>
        /// The cancellation token of the time out thread.
        /// </summary>
        public CancellationTokenSource TimeoutThreadToken { get; private set; }
        /// <summary>
        /// The Author of this interactivity process..
        /// </summary>
        public User Author { get; set; }
        /// <summary>
        /// Create a new InteractivityProcess object.
        /// </summary>
        /// <param name="timeoutThreadToken">Cancellation token for the time out thread.</param>
        /// <param name="chat">The chat of this process.</param>
        /// <param name="author">The author of this process.</param>
        /// <param name="predicate">The condition of this process.</param>
        public InteractivityProcess(CancellationTokenSource timeoutThreadToken,
            Chat chat,
            User author,
            Predicate<T> predicate = null)
        {
            this.TimeoutThreadToken = timeoutThreadToken;
            this.Chat = chat;
            this.Author = author;
            this.Predicate = predicate;
        }

    }
}
