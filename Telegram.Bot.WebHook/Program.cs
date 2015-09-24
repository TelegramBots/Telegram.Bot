using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Hosting;
using Owin;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace Telegram.Bot.WebHook
{
    static class Bot
    {
        public static readonly Api Api = new Api("Your Api Key");
    }

    static class Program
    {
        static void Main(string[] args)
        {
            // Endpoint musst be configured with netsh:
            // netsh http add urlacl url=https://+:8443/ user=<username>
            // netsh http add sslcert ipport=0.0.0.0:8443 certhash=<cert thumbprint> appid=<random guid>

            using (WebApp.Start<Startup>("https://+:8443"))
            {
                // Register WebHook
                Bot.Api.SetWebhook("https://YourHostname:8443/WebHook").Wait();

                Console.WriteLine("Server Started");

                // Stop Server after <Enter>
                Console.ReadLine();

                // Unregister WebHook
                Bot.Api.SetWebhook().Wait();
            }
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var configuration = new HttpConfiguration();

            configuration.Routes.MapHttpRoute("WebHook", "{controller}");

            app.UseWebApi(configuration);
        }
    }

    public class WebHookController : ApiController
    {
        public async Task<IHttpActionResult> Post(Update update)
        {
            var message = update.Message;

            Console.WriteLine("Received Message from {0}", message.Chat.Id);

            if (message.Type.Equals(MessageType.TextMessage))
            {
                // Echo each Message
                await Bot.Api.SendTextMessage(message.Chat.Id, message.Text);
            }
            else if (message.Type.Equals(MessageType.PhotoMessage))
            {
                // Download Photo
                var file = await Bot.Api.GetFile(message.Photo.LastOrDefault()?.FileId);

                var filename = file.FileId + "." + file.FilePath.Split('.').Last();

                using (var saveImageStream = File.Open(filename, FileMode.Create))
                {
                    await file.FileStream.CopyToAsync(saveImageStream);
                }

                await Bot.Api.SendTextMessage(message.Chat.Id, "Thx for the Pics");
            }

            return Ok();
        }
    }
}
