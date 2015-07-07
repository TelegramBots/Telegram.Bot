using System;
using System.Threading.Tasks;

namespace Telegram.Bot.Echo
{
    class Program
    {
        static void Main(string[] args)
        {
            Run().Wait();
        }

        static async Task Run()
        {
            var Bot = new Api("Your Api Key");

            var me = await Bot.GetMe();

            Console.WriteLine("Hello my name is {0}", me.Username);

            var offset = 0;

            while (true)
            {
                var updates = await Bot.GetUpdates(offset);

                foreach (var update in updates)
                {
                    if (update.Message.Text != null)
                        await Bot.SendTextMessage(update.Message.Chat.Id, update.Message.Text);

                    offset = update.Id + 1;
                }

                await Task.Delay(1000);
            }

        }
    }
}
