using System;
using Newtonsoft.Json;
using Telegram.Bot.Converters;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Types
{
    /// <summary>
    /// This object contains information about one member of the chat.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class ChatMember
    {
        /// <summary>
        /// Information about the user
        /// </summary>
        [JsonProperty("user", Required = Required.Always)]
        public User User { get; set; }

        /// <summary>
        /// The member's status in the chat.
        /// </summary>
        [JsonProperty("status", Required = Required.Always)]
        public ChatMemberStatus Status { get; set; }

        /// <summary>
        /// Optional. Restictred and kicked only. Date when restrictions will be lifted for this user, UTC time
        /// </summary>
        [JsonProperty(PropertyName = "until_date", Required = Required.Default)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime UntilDate { get; set; }

        /// <summary>
        /// Optional. Administrators only. True, if the bot is allowed to edit administrator privileges of that user
        /// </summary>
        [JsonProperty(PropertyName = "can_be_edited", Required = Required.Default)]
        public bool CanBeEdited { get; set; }

        /// <summary>
        /// Optional. Administrators only. True, if the administrator can change the chat title, photo and other settings
        /// </summary>
        [JsonProperty(PropertyName = "can_change_info", Required = Required.Default)]
        public bool CanChangeInfo { get; set; }

        /// <summary>
        /// Optional. Administrators only. True, if the administrator can post in the channel, channels only
        /// </summary>
        [JsonProperty(PropertyName = "can_post_messages", Required = Required.Default)]
        public bool CanPostMessages { get; set; }

        /// <summary>
        /// Optional. Administrators only. True, if the administrator can edit messages of other users, channels only
        /// </summary>
        [JsonProperty(PropertyName = "can_edit_messages", Required = Required.Default)]
        public bool CanEditMessages { get; set; }

        /// <summary>
        /// Optional. Administrators only. True, if the administrator can delete messages of other users
        /// </summary>
        [JsonProperty(PropertyName = "can_delete_messages", Required = Required.Default)]
        public bool CanDeleteMessages { get; set; }

        /// <summary>
        /// Optional. Administrators only. True, if the administrator can invite new users to the chat
        /// </summary>
        [JsonProperty(PropertyName = "can_invite_users", Required = Required.Default)]
        public bool CanInviteUsers { get; set; }

        /// <summary>
        /// Optional. Administrators only. True, if the administrator can restrict, ban or unban chat members
        /// </summary>
        [JsonProperty(PropertyName = "can_restrict_members", Required = Required.Default)]
        public bool CanRestrictMembers { get; set; }

        /// <summary>
        /// Optional. Administrators only. True, if the administrator can pin messages, supergroups only
        /// </summary>
        [JsonProperty(PropertyName = "can_pin_messages", Required = Required.Default)]
        public bool CanPinMessages { get; set; }

        /// <summary>
        /// Optional. Administrators only. True, if the administrator can add new administrators with a subset of his own privileges or demote administrators that he has promoted, directly or indirectly (promoted by administrators that were appointed by the user)
        /// </summary>
        [JsonProperty(PropertyName = "can_promote_members", Required = Required.Default)]
        public bool CanPromoteMembers { get; set; }

        /// <summary>
        /// Optional. Restricted only. True, if the user can send text messages, contacts, locations and venues
        /// </summary>
        [JsonProperty(PropertyName = "can_send_messages", Required = Required.Default)]
        public bool CanSendMessages { get; set; }

        /// <summary>
        /// Optional. Restricted only. True, if the user can send audios, documents, photos, videos, video notes and voice notes, implies <see cref="CanSendMessages"/>
        /// </summary>
        [JsonProperty(PropertyName = "can_send_media_messages", Required = Required.Default)]
        public bool CanSendMediaMessages { get; set; }

        /// <summary>
        /// Optional. Restricted only. True, if the user can send animations, games, stickers and use inline bots, implies <see cref="CanSendMediaMessages"/>
        /// </summary>
        [JsonProperty(PropertyName = "can_send_other_messages", Required = Required.Default)]
        public bool CanSendOtherMessages { get; set; }

        /// <summary>
        /// Optional. Restricted only. True, if user may add web page previews to his messages, implies <see cref="CanSendMediaMessages"/>
        /// </summary>
        [JsonProperty(PropertyName = "can_add_web_page_previews", Required = Required.Default)]
        public bool CanAddWebPagePreviews { get; set; }
    }
}
