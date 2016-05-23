using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// Type of action the Bot is performing
    /// </summary>
    public enum ChatAction
    {
        [EnumMember(Value = "typing")]
        Typing,

        [EnumMember(Value = "upload_photo")]
        UploadPhoto,

        [EnumMember(Value = "record_video")]
        RecordVideo,

        [EnumMember(Value = "upload_video")]
        UploadVideo,

        [EnumMember(Value = "record_audio")]
        RecordAudio,

        [EnumMember(Value = "upload_audio")]
        UploadAudio,

        [EnumMember(Value = "upload_document")]
        UploadDocument,

        [EnumMember(Value = "find_location")]
        FindLocation,
    }

    internal static class ChatActionExtensions
    {
        internal static string ToActionString(this ChatAction action)
        {
            return action.GetType()
                .GetRuntimeField(action.ToString())
                .GetCustomAttributes(typeof(EnumMemberAttribute), true)
                .Select(a => ((EnumMemberAttribute)a).Value).FirstOrDefault();
        }
    }
}
