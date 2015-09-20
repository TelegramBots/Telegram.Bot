using System;
using System.IO;
using System.Linq;
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

                    if (update.Message.Photo != null)
                    {
                        var file = await Bot.GetFile(update.Message.Photo.LastOrDefault()?.FileId);

                        var filename = file.FileId+"."+file.FilePath.Split('.').Last();

                        using (var profileImageStream = File.Open(filename, FileMode.Create))
                        {
                            await file.FileStream.CopyToAsync(profileImageStream);
                        }


                    }

                    offset = update.Id + 1;
                }

                await Task.Delay(1000);
            }
        }
    }
}
