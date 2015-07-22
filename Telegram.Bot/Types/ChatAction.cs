namespace Telegram.Bot.Types
{
    public class ChatAction
    {
        private readonly string _value;
        private ChatAction(string value)
        {
            _value = value;
        }

        public static ChatAction Typing => new ChatAction("typing");
        public static ChatAction UploadPhoto => new ChatAction("upload_photo");
        public static ChatAction RecordVideo => new ChatAction("record_video");
        public static ChatAction UploadVideo => new ChatAction("upload_video");
        public static ChatAction RecordAudio => new ChatAction("record_audio");
        public static ChatAction UploadAudio => new ChatAction("upload_audio");
        public static ChatAction UploadDocument => new ChatAction("upload_document");
        public static ChatAction FindLocation => new ChatAction("find_location");

        public override string ToString() => _value;
    }
}
