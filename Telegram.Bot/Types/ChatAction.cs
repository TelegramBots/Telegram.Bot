namespace Telegram.Bot.Types
{
    public class ChatAction
    {
        private readonly string _value;
        private ChatAction(string value)
        {
            _value = value;
        }

        public static ChatAction Typing { get {  return new ChatAction("typing");} }
        public static ChatAction UploadPhoto { get {  return new ChatAction("upload_photo");} }
        public static ChatAction RecordVideo { get {  return new ChatAction("record_video");} }
        public static ChatAction UploadVideo { get {  return new ChatAction("upload_video");} }
        public static ChatAction RecordAudio { get {  return new ChatAction("record_audio");} }
        public static ChatAction UploadAudio { get {  return new ChatAction("upload_audio");} }
        public static ChatAction UploadDocument { get {  return new ChatAction("upload_document");} }
        public static ChatAction FindLocation { get {  return new ChatAction("find_location");} }

        public override string ToString()
        {
            return _value;
        }
    }
}
