using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Telegram.Bot.Echo
{
    class Program
    {
        static void Main(string[] args)
        {
            var Bot = new Api("Your Api Key");

            var name = Bot.GetMe().Result.Username;

            Console.WriteLine("Hello my name is {0}", name);

            var offset = 0;

            while (true)
            {
                var updates = Bot.GetUpdates(offset).Result;

                foreach (var update in updates)
                {
                    if (update.Message.Text != null)
                        Bot.SendTextMessage(((User)update.Message.Chat).Id, update.Message.Text).Wait();

                    offset = update.Id+1;
                }

                Thread.Sleep(100);
            }
        }
    }
}
