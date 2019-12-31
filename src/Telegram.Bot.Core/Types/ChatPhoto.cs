using System.Runtime.Serialization;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// Collection of fileIds of profile pictures of a chat.
    /// </summary>
    [DataContract]
    public class ChatPhoto
    {
        /// <summary>
        /// File id of the big version of this <see cref="ChatPhoto"/>
        /// </summary>
        [DataMember(IsRequired = true)]
        public string BigFileId { get; set; }

        /// <summary>
        /// File id of the small version of this <see cref="ChatPhoto"/>
        /// </summary>
        [DataMember(IsRequired = true)]
        public string SmallFileId { get; set; }
    }
}
