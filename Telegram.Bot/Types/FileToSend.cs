using System.IO;

namespace Telegram.Bot.Types
{
    public struct FileToSend
    {
        public string Filename { get; set; }
        public Stream Content { get; set; }

        public FileToSend(string filename, Stream content)
        {
            Filename = filename;
            Content = content;
        }
    }
}
