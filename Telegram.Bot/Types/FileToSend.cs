using System.IO;

namespace Telegram.Bot.Types
{
    public struct FileToSend
    {
        public string Filename;
        public Stream Content;

        public FileToSend(string filename, Stream content)
        {
            Filename = filename;
            Content = content;
        }
    }
}
