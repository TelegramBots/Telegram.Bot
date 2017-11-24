// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    public class InputMediaVideo : InputMediaBase
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public int Duration { get; set; }

        public InputMediaVideo()
        {
            Type = "video";
        }
    }
}
