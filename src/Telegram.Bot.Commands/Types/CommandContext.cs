using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram.Bot.CommandHandler.Types
{
    public class CommandContext
    {
        /// <summary>
        /// Message that invoked the command
        /// </summary>
        public Message Message { get; private set; }
        /// <summary>
        /// Bot client
        /// </summary>
        public TelegramBotClient BotClient { get; private set; }
        /// <summary>
        /// Id of the message that invoked the command
        /// </summary>
        public int MessageId
        {
            get {
                return Message.MessageId;
            }
        }
        /// <summary>
        /// Chat of the message that invoked the command
        /// </summary>
        public Chat Chat
        {
            get {
                return Message.Chat;
            }
        }
        /// <summary>
        /// Id of the chat of the message that invoked the command
        /// </summary>
        public long ChatId
        {
            get {
                return Message.Chat.Id;
            }
        }

        /// <summary>
        /// Create a new command context
        /// </summary>
        /// <param name="message">Message that invoked the command</param>
        /// <param name="botClient">Bot client</param>
        public CommandContext(Message message, TelegramBotClient botClient)
        {
            Message = message;
            BotClient = botClient;
        }

        /// <summary>
        /// Quick way to respond to the message
        /// </summary>
        /// <param name="response">What to respond with</param>
        /// <returns></returns>
        public async Task RespondAsync(string response)
        {
            await BotClient.SendTextMessageAsync(ChatId, response);
        }

    }
}
