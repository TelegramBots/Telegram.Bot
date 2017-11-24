// ReSharper disable once CheckNamespace
namespace Telegram.Bot.Types
{
    public abstract class InputMediaBase
    {
        public string Type { get; protected set; }

        public InputMediaType Media { get; set; }

        public string Caption { get; set; }
    }
}
