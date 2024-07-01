namespace Telegram.Bot.Types;

/// <summary>This object represents a message.</summary>
public partial class Message
{
    /// <summary>Unique message identifier inside this chat</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public int MessageId { get; set; }

    /// <summary><em>Optional</em>. Unique identifier of a message thread to which the message belongs; for supergroups only</summary>
    public int? MessageThreadId { get; set; }

    /// <summary><em>Optional</em>. Sender of the message; empty for messages sent to channels. For backward compatibility, the field contains a fake sender user in non-channel chats, if the message was sent on behalf of a chat.</summary>
    public User? From { get; set; }

    /// <summary><em>Optional</em>. Sender of the message, sent on behalf of a chat. For example, the channel itself for channel posts, the supergroup itself for messages from anonymous group administrators, the linked channel for messages automatically forwarded to the discussion group. For backward compatibility, the field <see cref="From">From</see> contains a fake sender user in non-channel chats, if the message was sent on behalf of a chat.</summary>
    public Chat? SenderChat { get; set; }

    /// <summary><em>Optional</em>. If the sender of the message boosted the chat, the number of boosts added by the user</summary>
    public int? SenderBoostCount { get; set; }

    /// <summary><em>Optional</em>. The bot that actually sent the message on behalf of the business account. Available only for outgoing messages sent on behalf of the connected business account.</summary>
    public User? SenderBusinessBot { get; set; }

    /// <summary>Date the message was sent. It is always a valid date.</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime Date { get; set; }

    /// <summary><em>Optional</em>. Unique identifier of the business connection from which the message was received. If non-empty, the message belongs to a chat of the corresponding business account that is independent from any potential bot chat which might share the same identifier.</summary>
    public string? BusinessConnectionId { get; set; }

    /// <summary>Chat the message belongs to</summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public Chat Chat { get; set; } = default!;

    /// <summary><em>Optional</em>. Information about the original message for forwarded messages</summary>
    public MessageOrigin? ForwardOrigin { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the message is sent to a forum topic</summary>
    public bool IsTopicMessage { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the message is a channel post that was automatically forwarded to the connected discussion group</summary>
    public bool IsAutomaticForward { get; set; }

    /// <summary><em>Optional</em>. For replies in the same chat and message thread, the original message. Note that the Message object in this field will not contain further <see cref="ReplyToMessage">ReplyToMessage</see> fields even if it itself is a reply.</summary>
    public Message? ReplyToMessage { get; set; }

    /// <summary><em>Optional</em>. Information about the message that is being replied to, which may come from another chat or forum topic</summary>
    public ExternalReplyInfo? ExternalReply { get; set; }

    /// <summary><em>Optional</em>. For replies that quote part of the original message, the quoted part of the message</summary>
    public TextQuote? Quote { get; set; }

    /// <summary><em>Optional</em>. For replies to a story, the original story</summary>
    public Story? ReplyToStory { get; set; }

    /// <summary><em>Optional</em>. Bot through which the message was sent</summary>
    public User? ViaBot { get; set; }

    /// <summary><em>Optional</em>. Date the message was last edited</summary>
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? EditDate { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the message can't be forwarded</summary>
    public bool HasProtectedContent { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the message was sent by an implicit action, for example, as an away or a greeting business message, or as a scheduled message</summary>
    public bool IsFromOffline { get; set; }

    /// <summary><em>Optional</em>. The unique identifier of a media message group this message belongs to</summary>
    public string? MediaGroupId { get; set; }

    /// <summary><em>Optional</em>. Signature of the post author for messages in channels, or the custom title of an anonymous group administrator</summary>
    public string? AuthorSignature { get; set; }

    /// <summary><em>Optional</em>. For text messages, the actual text of the message</summary>
    public string? Text { get; set; }

    /// <summary><em>Optional</em>. For text messages, special entities like usernames, URLs, bot commands, etc. that appear in the text</summary>
    public MessageEntity[]? Entities { get; set; }

    /// <summary><em>Optional</em>. Options used for link preview generation for the message, if it is a text message and link preview options were changed</summary>
    public LinkPreviewOptions? LinkPreviewOptions { get; set; }

    /// <summary><em>Optional</em>. Unique identifier of the message effect added to the message</summary>
    public string? EffectId { get; set; }

    /// <summary><em>Optional</em>. Message is an animation, information about the animation. For backward compatibility, when this field is set, the <see cref="Document">Document</see> field will also be set</summary>
    public Animation? Animation { get; set; }

    /// <summary><em>Optional</em>. Message is an audio file, information about the file</summary>
    public Audio? Audio { get; set; }

    /// <summary><em>Optional</em>. Message is a general file, information about the file</summary>
    public Document? Document { get; set; }

    /// <summary><em>Optional</em>. Message contains paid media; information about the paid media</summary>
    public PaidMediaInfo? PaidMedia { get; set; }

    /// <summary><em>Optional</em>. Message is a photo, available sizes of the photo</summary>
    public PhotoSize[]? Photo { get; set; }

    /// <summary><em>Optional</em>. Message is a sticker, information about the sticker</summary>
    public Sticker? Sticker { get; set; }

    /// <summary><em>Optional</em>. Message is a forwarded story</summary>
    public Story? Story { get; set; }

    /// <summary><em>Optional</em>. Message is a video, information about the video</summary>
    public Video? Video { get; set; }

    /// <summary><em>Optional</em>. Message is a <a href="https://telegram.org/blog/video-messages-and-telescope">video note</a>, information about the video message</summary>
    public VideoNote? VideoNote { get; set; }

    /// <summary><em>Optional</em>. Message is a voice message, information about the file</summary>
    public Voice? Voice { get; set; }

    /// <summary><em>Optional</em>. Caption for the animation, audio, document, paid media, photo, video or voice</summary>
    public string? Caption { get; set; }

    /// <summary><em>Optional</em>. For messages with a caption, special entities like usernames, URLs, bot commands, etc. that appear in the caption</summary>
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the caption must be shown above the message media</summary>
    public bool ShowCaptionAboveMedia { get; set; }

    /// <summary><em>Optional</em>. <see langword="true"/>, if the message media is covered by a spoiler animation</summary>
    public bool HasMediaSpoiler { get; set; }

    /// <summary><em>Optional</em>. Message is a shared contact, information about the contact</summary>
    public Contact? Contact { get; set; }

    /// <summary><em>Optional</em>. Message is a dice with random value</summary>
    public Dice? Dice { get; set; }

    /// <summary><em>Optional</em>. Message is a game, information about the game. <a href="https://core.telegram.org/bots/api#games">More about games »</a></summary>
    public Game? Game { get; set; }

    /// <summary><em>Optional</em>. Message is a native poll, information about the poll</summary>
    public Poll? Poll { get; set; }

    /// <summary><em>Optional</em>. Message is a venue, information about the venue. For backward compatibility, when this field is set, the <see cref="Location">Location</see> field will also be set</summary>
    public Venue? Venue { get; set; }

    /// <summary><em>Optional</em>. Message is a shared location, information about the location</summary>
    public Location? Location { get; set; }

    /// <summary><em>Optional</em>. New members that were added to the group or supergroup and information about them (the bot itself may be one of these members)</summary>
    public User[]? NewChatMembers { get; set; }

    /// <summary><em>Optional</em>. A member was removed from the group, information about them (this member may be the bot itself)</summary>
    public User? LeftChatMember { get; set; }

    /// <summary><em>Optional</em>. A chat title was changed to this value</summary>
    public string? NewChatTitle { get; set; }

    /// <summary><em>Optional</em>. A chat photo was change to this value</summary>
    public PhotoSize[]? NewChatPhoto { get; set; }

    /// <summary><em>Optional</em>. Service message: the chat photo was deleted</summary>
    public bool? DeleteChatPhoto { get; set; }

    /// <summary><em>Optional</em>. Service message: the group has been created</summary>
    public bool? GroupChatCreated { get; set; }

    /// <summary><em>Optional</em>. Service message: the supergroup has been created. This field can't be received in a message coming through updates, because bot can't be a member of a supergroup when it is created. It can only be found in <see cref="ReplyToMessage">ReplyToMessage</see> if someone replies to a very first message in a directly created supergroup.</summary>
    public bool? SupergroupChatCreated { get; set; }

    /// <summary><em>Optional</em>. Service message: the channel has been created. This field can't be received in a message coming through updates, because bot can't be a member of a channel when it is created. It can only be found in <see cref="ReplyToMessage">ReplyToMessage</see> if someone replies to a very first message in a channel.</summary>
    public bool? ChannelChatCreated { get; set; }

    /// <summary><em>Optional</em>. Service message: auto-delete timer settings changed in the chat</summary>
    public MessageAutoDeleteTimerChanged? MessageAutoDeleteTimerChanged { get; set; }

    /// <summary><em>Optional</em>. The group has been migrated to a supergroup with the specified identifier.</summary>
    public long? MigrateToChatId { get; set; }

    /// <summary><em>Optional</em>. The supergroup has been migrated from a group with the specified identifier.</summary>
    public long? MigrateFromChatId { get; set; }

    /// <summary><em>Optional</em>. Specified message was pinned. Note that the Message object in this field will not contain further <see cref="ReplyToMessage">ReplyToMessage</see> fields even if it itself is a reply.</summary>
    public Message? PinnedMessage { get; set; }

    /// <summary><em>Optional</em>. Message is an invoice for a <a href="https://core.telegram.org/bots/api#payments">payment</a>, information about the invoice. <a href="https://core.telegram.org/bots/api#payments">More about payments »</a></summary>
    public Invoice? Invoice { get; set; }

    /// <summary><em>Optional</em>. Message is a service message about a successful payment, information about the payment. <a href="https://core.telegram.org/bots/api#payments">More about payments »</a></summary>
    public SuccessfulPayment? SuccessfulPayment { get; set; }

    /// <summary><em>Optional</em>. Service message: users were shared with the bot</summary>
    public UsersShared? UsersShared { get; set; }

    /// <summary><em>Optional</em>. Service message: a chat was shared with the bot</summary>
    public ChatShared? ChatShared { get; set; }

    /// <summary><em>Optional</em>. The domain name of the website on which the user has logged in. <a href="https://core.telegram.org/widgets/login">More about Telegram Login »</a></summary>
    public string? ConnectedWebsite { get; set; }

    /// <summary><em>Optional</em>. Service message: the user allowed the bot to write messages after adding it to the attachment or side menu, launching a Web App from a link, or accepting an explicit request from a Web App sent by the method <a href="https://core.telegram.org/bots/webapps#initializing-mini-apps">requestWriteAccess</a></summary>
    public WriteAccessAllowed? WriteAccessAllowed { get; set; }

    /// <summary><em>Optional</em>. Telegram Passport data</summary>
    public PassportData? PassportData { get; set; }

    /// <summary><em>Optional</em>. Service message. A user in the chat triggered another user's proximity alert while sharing Live Location.</summary>
    public ProximityAlertTriggered? ProximityAlertTriggered { get; set; }

    /// <summary><em>Optional</em>. Service message: user boosted the chat</summary>
    public ChatBoostAdded? BoostAdded { get; set; }

    /// <summary><em>Optional</em>. Service message: chat background set</summary>
    public ChatBackground? ChatBackgroundSet { get; set; }

    /// <summary><em>Optional</em>. Service message: forum topic created</summary>
    public ForumTopicCreated? ForumTopicCreated { get; set; }

    /// <summary><em>Optional</em>. Service message: forum topic edited</summary>
    public ForumTopicEdited? ForumTopicEdited { get; set; }

    /// <summary><em>Optional</em>. Service message: forum topic closed</summary>
    public ForumTopicClosed? ForumTopicClosed { get; set; }

    /// <summary><em>Optional</em>. Service message: forum topic reopened</summary>
    public ForumTopicReopened? ForumTopicReopened { get; set; }

    /// <summary><em>Optional</em>. Service message: the 'General' forum topic hidden</summary>
    public GeneralForumTopicHidden? GeneralForumTopicHidden { get; set; }

    /// <summary><em>Optional</em>. Service message: the 'General' forum topic unhidden</summary>
    public GeneralForumTopicUnhidden? GeneralForumTopicUnhidden { get; set; }

    /// <summary><em>Optional</em>. Service message: a scheduled giveaway was created</summary>
    public GiveawayCreated? GiveawayCreated { get; set; }

    /// <summary><em>Optional</em>. The message is a scheduled giveaway message</summary>
    public Giveaway? Giveaway { get; set; }

    /// <summary><em>Optional</em>. A giveaway with public winners was completed</summary>
    public GiveawayWinners? GiveawayWinners { get; set; }

    /// <summary><em>Optional</em>. Service message: a giveaway without public winners was completed</summary>
    public GiveawayCompleted? GiveawayCompleted { get; set; }

    /// <summary><em>Optional</em>. Service message: video chat scheduled</summary>
    public VideoChatScheduled? VideoChatScheduled { get; set; }

    /// <summary><em>Optional</em>. Service message: video chat started</summary>
    public VideoChatStarted? VideoChatStarted { get; set; }

    /// <summary><em>Optional</em>. Service message: video chat ended</summary>
    public VideoChatEnded? VideoChatEnded { get; set; }

    /// <summary><em>Optional</em>. Service message: new participants invited to a video chat</summary>
    public VideoChatParticipantsInvited? VideoChatParticipantsInvited { get; set; }

    /// <summary><em>Optional</em>. Service message: data sent by a Web App</summary>
    public WebAppData? WebAppData { get; set; }

    /// <summary><em>Optional</em>. Inline keyboard attached to the message. <c>LoginUrl</c> buttons are represented as ordinary <c>url</c> buttons.</summary>
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>Gets the <see cref="MessageType">type</see> of the <see cref="Message"/></summary>
    /// <value>The <see cref="MessageType">type</see> of the <see cref="Message"/></value>
    [JsonIgnore]
    public MessageType Type => this switch
    {
        { Text: not null }                              => MessageType.Text,
        { Animation: not null }                         => MessageType.Animation,
        { Audio: not null }                             => MessageType.Audio,
        { Document: not null }                          => MessageType.Document,
        { PaidMedia: not null }                         => MessageType.PaidMedia,
        { Photo: not null }                             => MessageType.Photo,
        { Sticker: not null }                           => MessageType.Sticker,
        { Story: not null }                             => MessageType.Story,
        { Video: not null }                             => MessageType.Video,
        { VideoNote: not null }                         => MessageType.VideoNote,
        { Voice: not null }                             => MessageType.Voice,
        { Contact: not null }                           => MessageType.Contact,
        { Dice: not null }                              => MessageType.Dice,
        { Game: not null }                              => MessageType.Game,
        { Poll: not null }                              => MessageType.Poll,
        { Venue: not null }                             => MessageType.Venue,
        { Location: not null }                          => MessageType.Location,
        { NewChatMembers: not null }                    => MessageType.NewChatMembers,
        { LeftChatMember: not null }                    => MessageType.LeftChatMember,
        { NewChatTitle: not null }                      => MessageType.NewChatTitle,
        { NewChatPhoto: not null }                      => MessageType.NewChatPhoto,
        { DeleteChatPhoto: not null }                   => MessageType.DeleteChatPhoto,
        { GroupChatCreated: not null }                  => MessageType.GroupChatCreated,
        { SupergroupChatCreated: not null }             => MessageType.SupergroupChatCreated,
        { ChannelChatCreated: not null }                => MessageType.ChannelChatCreated,
        { MessageAutoDeleteTimerChanged: not null }     => MessageType.MessageAutoDeleteTimerChanged,
        { MigrateToChatId: not null }                   => MessageType.MigrateToChatId,
        { MigrateFromChatId: not null }                 => MessageType.MigrateFromChatId,
        { PinnedMessage: not null }                     => MessageType.PinnedMessage,
        { Invoice: not null }                           => MessageType.Invoice,
        { SuccessfulPayment: not null }                 => MessageType.SuccessfulPayment,
        { UsersShared: not null }                       => MessageType.UsersShared,
        { ChatShared: not null }                        => MessageType.ChatShared,
        { ConnectedWebsite: not null }                  => MessageType.ConnectedWebsite,
        { WriteAccessAllowed: not null }                => MessageType.WriteAccessAllowed,
        { PassportData: not null }                      => MessageType.PassportData,
        { ProximityAlertTriggered: not null }           => MessageType.ProximityAlertTriggered,
        { BoostAdded: not null }                        => MessageType.BoostAdded,
        { ChatBackgroundSet: not null }                 => MessageType.ChatBackgroundSet,
        { ForumTopicCreated: not null }                 => MessageType.ForumTopicCreated,
        { ForumTopicEdited: not null }                  => MessageType.ForumTopicEdited,
        { ForumTopicClosed: not null }                  => MessageType.ForumTopicClosed,
        { ForumTopicReopened: not null }                => MessageType.ForumTopicReopened,
        { GeneralForumTopicHidden: not null }           => MessageType.GeneralForumTopicHidden,
        { GeneralForumTopicUnhidden: not null }         => MessageType.GeneralForumTopicUnhidden,
        { GiveawayCreated: not null }                   => MessageType.GiveawayCreated,
        { Giveaway: not null }                          => MessageType.Giveaway,
        { GiveawayWinners: not null }                   => MessageType.GiveawayWinners,
        { GiveawayCompleted: not null }                 => MessageType.GiveawayCompleted,
        { VideoChatScheduled: not null }                => MessageType.VideoChatScheduled,
        { VideoChatStarted: not null }                  => MessageType.VideoChatStarted,
        { VideoChatEnded: not null }                    => MessageType.VideoChatEnded,
        { VideoChatParticipantsInvited: not null }      => MessageType.VideoChatParticipantsInvited,
        { WebAppData: not null }                        => MessageType.WebAppData,
        _                                               => MessageType.Unknown
    };
}
