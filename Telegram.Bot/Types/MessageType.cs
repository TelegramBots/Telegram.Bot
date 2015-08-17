namespace Telegram.Bot.Types
{
    public class MessageType
    {
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
    }
}
