namespace Telegram.Bot.Types
{
    public class MessageType
    {
        public override int GetHashCode()
        {
            unchecked
            {
                return (Method.GetHashCode()*397) ^ ContentParameter.GetHashCode();
            }
        }

        protected bool Equals(MessageType other) => string.Equals(Method, other.Method) && string.Equals(ContentParameter, other.ContentParameter);

        public override bool Equals(object obj)
        {
            return obj.GetType() == GetType() && (this == (MessageType)obj || Equals((MessageType)obj));
        }

        private MessageType(string method, string contentParameter)
        {
            Method = method;
            ContentParameter = contentParameter;
        }

        public static MessageType TextMessage => new MessageType("sendMessage", "text");
        public static MessageType PhotoMessage => new MessageType("sendPhoto", "photo");
        public static MessageType AudioMessage => new MessageType("sendAudio", "audio");
        public static MessageType VideoMessage => new MessageType("sendVideo", "video");
        public static MessageType VoiceMessage => new MessageType("sendVoice", "voice");
        public static MessageType DocumentMessage => new MessageType("sendDocument", "document");
        public static MessageType StickerMessage => new MessageType("sendSticker", "sticker");
        public static MessageType LocationMessage => new MessageType("sendLocation", "latitude");
        public static MessageType ContactMessage => new MessageType(null, null);

        public string Method { get; }

        public string ContentParameter { get; }

        public static bool operator ==(MessageType a, MessageType b) => (a?.ContentParameter == b?.ContentParameter && a?.Method == b?.Method);

        public static bool operator !=(MessageType a, MessageType b) => !(a == b);
    }
}
