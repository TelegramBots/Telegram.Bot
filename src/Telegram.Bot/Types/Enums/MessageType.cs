namespace Telegram.Bot.Types.Enums;

/// <summary>
/// The type of a <see cref="Message"/>
/// </summary>
[JsonConverter(typeof(MessageTypeConverter))]
public enum MessageType
{
    /// <summary>
    /// The <see cref="Message"/> is unknown
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The <see cref="Message"/> contains <see cref="Message.Text"/>
    /// </summary>
    Text,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.Photo"/>
    /// </summary>
    Photo,

    /// <summary>
    /// The <see cref="Message"/> contains an <see cref="Message.Audio"/>
    /// </summary>
    Audio,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.Video"/>
    /// </summary>
    Video,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.Voice"/>
    /// </summary>
    Voice,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.Document"/>
    /// </summary>
    Document,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.Sticker"/>
    /// </summary>
    Sticker,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.Location"/>
    /// </summary>
    Location,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.Contact"/>
    /// </summary>
    Contact,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.Venue"/>
    /// </summary>
    Venue,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.Game"/>
    /// </summary>
    Game,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.VideoNote"/>
    /// </summary>
    VideoNote,

    /// <summary>
    /// The <see cref="Message"/> contains an <see cref="Message.Invoice"/>
    /// </summary>
    Invoice,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.SuccessfulPayment"/>
    /// </summary>
    SuccessfulPayment,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.ConnectedWebsite"/>
    /// </summary>
    ConnectedWebsite,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.NewChatMembers"/>
    /// </summary>
    NewChatMembers,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.LeftChatMember"/>
    /// </summary>
    LeftChatMember,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.NewChatTitle"/>
    /// </summary>
    NewChatTitle,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.NewChatPhoto"/>
    /// </summary>
    NewChatPhoto,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.PinnedMessage"/>
    /// </summary>
    PinnedMessage,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.DeleteChatPhoto"/>
    /// </summary>
    DeleteChatPhoto,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.GroupChatCreated"/>
    /// </summary>
    GroupChatCreated,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.SupergroupChatCreated"/>
    /// </summary>
    SupergroupChatCreated,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.ChannelChatCreated"/>
    /// </summary>
    ChannelChatCreated,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.MigrateFromChatId"/>
    /// </summary>
    MigrateFromChatId,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.MigrateToChatId"/>
    /// </summary>
    MigrateToChatId,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.Poll"/>
    /// </summary>
    Poll,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.Dice"/>
    /// </summary>
    Dice,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.MessageAutoDeleteTimerChanged"/>
    /// </summary>
    MessageAutoDeleteTimerChanged,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.ProximityAlertTriggered"/>
    /// </summary>
    ProximityAlertTriggered,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.WebAppData"/>
    /// </summary>
    WebAppData,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.VideoChatScheduled"/>
    /// </summary>
    VideoChatScheduled,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.VideoChatStarted"/>
    /// </summary>
    VideoChatStarted,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.VideoChatEnded"/>
    /// </summary>
    VideoChatEnded,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.VideoChatParticipantsInvited"/>
    /// </summary>
    VideoChatParticipantsInvited,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.Animation"/>
    /// </summary>
    Animation,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.ForumTopicCreated"/>
    /// </summary>
    ForumTopicCreated,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.ForumTopicClosed"/>
    /// </summary>
    ForumTopicClosed,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.ForumTopicReopened"/>
    /// </summary>
    ForumTopicReopened,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.ForumTopicEdited"/>
    /// </summary>
    ForumTopicEdited,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.GeneralForumTopicHidden"/>
    /// </summary>
    GeneralForumTopicHidden,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.GeneralForumTopicUnhidden"/>
    /// </summary>
    GeneralForumTopicUnhidden,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.WriteAccessAllowed"/>
    /// </summary>
    WriteAccessAllowed,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.UserShared"/>
    /// </summary>
    [Obsolete($"This member is obsolete, use {nameof(MessageType)}.{nameof(UsersShared)} instead")]
    UserShared,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.UsersShared"/>
    /// </summary>
    UsersShared,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.ChatShared"/>
    /// </summary>
    ChatShared,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.Story"/>
    /// </summary>
    Story,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.PassportData"/>
    /// </summary>
    PassportData,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.GiveawayCreated"/>
    /// </summary>
    GiveawayCreated,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.Giveaway"/>
    /// </summary>
    Giveaway,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.GiveawayWinners"/>
    /// </summary>
    GiveawayWinners,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.GiveawayCompleted"/>
    /// </summary>
    GiveawayCompleted,

    /// <summary>
    /// The <see cref="Message"/> contains a <see cref="Message.BoostAdded"/>
    /// </summary>
    BoostAdded,
}
