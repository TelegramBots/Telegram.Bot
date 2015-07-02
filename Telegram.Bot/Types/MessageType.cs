namespace Telegram.Bot.Types
{
    public class MessageType
    {
        private readonly string _method;
        private readonly string _contentParameter;

        private MessageType(string method, string contentParameter)
        {
            _method = method;
            _contentParameter = _contentParameter;
        }

        public static MessageType TextMessage { get { return new MessageType("sendMessage", "text"); } }
        public static MessageType PhotoMessage { get { return new MessageType("sendPhoto", "photo"); } }
        public static MessageType AudioMessage { get { return new MessageType("sendAudio", "audio"); } }
        public static MessageType VideoMessage { get { return new MessageType("sendVideo", "video"); } }
        public static MessageType DocumentMessage { get { return new MessageType("sendDocument", "document"); } }
        public static MessageType StickerMessage { get { return new MessageType("sendSticker", "sticker"); } }
        public static MessageType LocationMessage { get { return new MessageType("sendLocation", "latitude"); } }
        public static MessageType ContactMessage { get { return new MessageType(null, null); } }

        public string Method { get { return _method;} }

        public string ContentParameter { get { return _contentParameter; } }
    }
}
