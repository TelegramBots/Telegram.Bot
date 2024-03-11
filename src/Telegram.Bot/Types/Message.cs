using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Converters;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Passport;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram.Bot.Types;

/// <summary>
/// This object represents a message.
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class Message : MaybeInaccessibleMessage
{
    /// <summary>
    /// Unique message identifier inside this chat
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int MessageId { get; set; }

    /// <summary>
    /// Optional. Unique identifier of a message thread to which the message belongs; for supergroups only
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? MessageThreadId { get; set; }

    /// <summary>
    /// Optional. Sender, empty for messages sent to channels
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public User? From { get; set; }

    /// <summary>
    /// Optional. Sender of the message, sent on behalf of a chat. The channel itself for channel messages.
    /// The supergroup itself for messages from anonymous group administrators. The linked channel for messages
    /// automatically forwarded to the discussion group
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Chat? SenderChat { get; set; }

    /// <summary>
    /// Optional. If the sender of the message boosted the chat, the number of boosts added by the user
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int? SenderBoostCount { get; set; }

    /// <summary>
    /// Date the message was sent
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime Date { get; set; }

    /// <summary>
    /// Conversation the message belongs to
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public Chat Chat { get; set; } = default!;

    /// <summary>
    /// Optional. For forwarded messages, sender of the original message
    /// </summary>
    [JsonIgnore]
    [Obsolete($"This property is deprecated, use {nameof(ForwardOrigin)} property")]
    public User? ForwardFrom => (ForwardOrigin as MessageOriginUser)?.SenderUser;

    /// <summary>
    /// Optional. For messages forwarded from channels or from anonymous administrators, information about the
    /// original sender chat
    /// </summary>
    [JsonIgnore]
    [Obsolete($"This property is deprecated, use {nameof(ForwardOrigin)} property")]
    public Chat? ForwardFromChat => ForwardOrigin switch
    {
        MessageOriginChannel originChannel => originChannel.Chat,
        MessageOriginChat originChat => originChat.SenderChat,
        _ => null,
    };

    /// <summary>
    /// Optional. For messages forwarded from channels, identifier of the original message in the channel
    /// </summary>
    [JsonIgnore]
    [Obsolete($"This property is deprecated, use {nameof(ForwardOrigin)} property")]
    public int? ForwardFromMessageId => (ForwardOrigin as MessageOriginChannel)?.MessageId;

    /// <summary>
    /// Optional. For messages forwarded from channels, signature of the post author if present
    /// </summary>
    [JsonIgnore]
    [Obsolete($"This property is deprecated, use {nameof(ForwardOrigin)} property")]
    public string? ForwardSignature => (ForwardOrigin as MessageOriginChannel)?.AuthorSignature;

    /// <summary>
    /// Optional. Sender's name for messages forwarded from users who disallow adding a link to their account in
    /// forwarded messages
    /// </summary>
    [JsonIgnore]
    [Obsolete($"This property is deprecated, use {nameof(ForwardOrigin)} property")]
    public string? ForwardSenderName => (ForwardOrigin as MessageOriginHiddenUser)?.SenderUserName;

    /// <summary>
    /// Optional. For forwarded messages, date the original message was sent
    /// </summary>
    [JsonIgnore]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    [Obsolete($"This property is deprecated, use {nameof(ForwardOrigin)} property")]
    public DateTime? ForwardDate => ForwardOrigin?.Date;

    /// <summary>
    ///Optional. Information about the original message for forwarded messages
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public MessageOrigin? ForwardOrigin { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the message is sent to a forum topic
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? IsTopicMessage { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if the message is a channel post that was automatically forwarded to the connected
    /// discussion group
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? IsAutomaticForward { get; set; }

    /// <summary>
    /// Optional. For replies, the original message. Note that the <see cref="Message"/> object in this field
    /// will not contain further <see cref="ReplyToMessage"/> fields even if it itself is a reply.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Message? ReplyToMessage { get; set; }

    /// <summary>
    /// Optional. Information about the message that is being replied to, which may come from
    /// another chat or forum topic
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ExternalReplyInfo? ExternalReply { get; set; }

    /// <summary>
    /// Optional. For replies that quote part of the original message, the quoted part of the message
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public TextQuote? Quote { get; set; }

    /// <summary>
    /// Optional. For replies to a story, the original story
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Story? ReplyToStory { get; set; }

    /// <summary>
    /// Optional. Bot through which the message was sent
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public User? ViaBot { get; set; }

    /// <summary>
    /// Optional. Date the message was last edited
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime? EditDate { get; set; }

    /// <summary>
    /// Optional. <see langword="true"/>, if messages from the chat can't be forwarded to other chats.
    /// Returned only in <see cref="Requests.GetChatRequest"/>.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? HasProtectedContent { get; set; }

    /// <summary>
    /// Optional. The unique identifier of a media message group this message belongs to
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? MediaGroupId { get; set; }

    /// <summary>
    /// Optional. Signature of the post author for messages in channels, or the custom title of an anonymous
    /// group administrator
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? AuthorSignature { get; set; }

    /// <summary>
    /// Optional. For text messages, the actual text of the message, 0-4096 characters
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Text { get; set; }

    /// <summary>
    /// Optional. For text messages, special entities like usernames, URLs, bot commands, etc. that appear
    /// in the text
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public MessageEntity[]? Entities { get; set; }

    /// <summary>
    /// Gets the entity values.
    /// </summary>
    /// <value>
    /// The entity contents.
    /// </value>
    public IEnumerable<string>? EntityValues =>
        Text is null
            ? default
            : Entities?.Select(entity => Text.Substring(entity.Offset, entity.Length));

    /// <summary>
    /// Optional. Options used for link preview generation for the message, if it is a text message
    /// and link preview options were changed
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public LinkPreviewOptions? LinkPreviewOptions { get; set; }

    /// <summary>
    /// Optional. Message is an animation, information about the animation. For backward compatibility, when this
    /// field is set, the <see cref="Document"/> field will also be set
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Animation? Animation { get; set; }

    /// <summary>
    /// Optional. Message is an audio file, information about the file
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Audio? Audio { get; set; }

    /// <summary>
    /// Optional. Message is a general file, information about the file
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Document? Document { get; set; }

    /// <summary>
    /// Optional. Message is a photo, available sizes of the photo
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public PhotoSize[]? Photo { get; set; }

    /// <summary>
    /// Optional. Message is a sticker, information about the sticker
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Sticker? Sticker { get; set; }

    /// <summary>
    /// Optional. Message is a forwarded story
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Story? Story { get; set; }

    /// <summary>
    /// Optional. Message is a video, information about the video
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Video? Video { get; set; }

    /// <summary>
    /// Optional. Message is a video note, information about the video message
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public VideoNote? VideoNote { get; set; }

    /// <summary>
    /// Optional. Message is a voice message, information about the file
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Voice? Voice { get; set; }

    /// <summary>
    /// Optional. Caption for the animation, audio, document, photo, video or voice, 0-1024 characters
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? Caption { get; set; }

    /// <summary>
    /// Optional. For messages with a caption, special entities like usernames, URLs, bot commands, etc. that
    /// appear in the caption
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public MessageEntity[]? CaptionEntities { get; set; }

    /// <summary>
    /// Gets the caption entity values.
    /// </summary>
    /// <value>
    /// The caption entity contents.
    /// </value>
    public IEnumerable<string>? CaptionEntityValues =>
        Caption is null
            ? default
            : CaptionEntities?.Select(entity => Caption.Substring(entity.Offset, entity.Length));

    /// <summary>
    /// Optional. <see langword="true"/>, if the message media is covered by a spoiler animation
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? HasMediaSpoiler { get; set; }

    /// <summary>
    /// Optional. Message is a shared contact, information about the contact
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Contact? Contact { get; set; }

    /// <summary>
    /// Optional. Message is a dice with random value
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Dice? Dice { get; set; }

    /// <summary>
    ///Optional. Message is a game, information about the game
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Game? Game { get; set; }

    /// <summary>
    /// Optional. Message is a native poll, information about the poll
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Poll? Poll { get; set; }

    /// <summary>
    /// Optional. Message is a venue, information about the venue. For backward compatibility, when this field
    /// is set, the <see cref="Location"/> field will also be set
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Venue? Venue { get; set; }

    /// <summary>
    /// Optional. Message is a shared location, information about the location
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Location? Location { get; set; }

    /// <summary>
    /// Optional. New members that were added to the group or supergroup and information about them
    /// (the bot itself may be one of these members)
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public User[]? NewChatMembers { get; set; }

    /// <summary>
    /// Optional. A member was removed from the group, information about them (this member may be the bot itself)
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public User? LeftChatMember { get; set; }

    /// <summary>
    /// Optional. A chat title was changed to this value
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? NewChatTitle { get; set; }

    /// <summary>
    /// Optional. A chat photo was change to this value
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public PhotoSize[]? NewChatPhoto { get; set; }

    /// <summary>
    /// Optional. Service message: the chat photo was deleted
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? DeleteChatPhoto { get; set; }

    /// <summary>
    /// Optional. Service message: the group has been created
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? GroupChatCreated { get; set; }

    /// <summary>
    /// Optional. Service message: the supergroup has been created. This field can't be received in a message
    /// coming through updates, because bot can't be a member of a supergroup when it is created. It can only be
    /// found in <see cref="ReplyToMessage"/> if someone replies to a very first message in a directly created
    /// supergroup.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? SupergroupChatCreated { get; set; }

    /// <summary>
    /// Optional. Service message: the channel has been created. This field can't be received in a message coming
    /// through updates, because bot can't be a member of a channel when it is created. It can only be found in
    /// <see cref="ReplyToMessage"/> if someone replies to a very first message in a channel.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public bool? ChannelChatCreated { get; set; }

    /// <summary>
    /// Optional. Service message: auto-delete timer settings changed in the chat
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public MessageAutoDeleteTimerChanged? MessageAutoDeleteTimerChanged { get; set; }

    /// <summary>
    /// Optional. The group has been migrated to a supergroup with the specified identifier
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public long? MigrateToChatId { get; set; }

    /// <summary>
    /// Optional. The supergroup has been migrated from a group with the specified identifier
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public long? MigrateFromChatId { get; set; }

    /// <summary>
    /// Optional. Specified message was pinned. Note that the <see cref="Message"/> object in this field
    /// will not contain further <see cref="ReplyToMessage"/> fields even if it itself is a reply.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public MaybeInaccessibleMessage? PinnedMessage { get; set; }

    /// <summary>
    /// Optional. Message is an invoice for a
    /// <a href="https://core.telegram.org/bots/api#payments">payment</a>, information about the invoice
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Invoice? Invoice { get; set; }

    /// <summary>
    /// Optional. Message is a service message about a successful payment, information about the payment
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public SuccessfulPayment? SuccessfulPayment { get; set; }

    /// <summary>
    /// Optional. Service message: a user was shared with the bot
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public UsersShared? UsersShared { get; set; }

    /// <summary>
    /// Optional. Service message: a user was shared with the bot
    /// </summary>
    [Obsolete($"This property is deprecated, use property {nameof(UsersShared)}")]
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public UserShared? UserShared { get; set; }

    /// <summary>
    /// Optional. Service message: a chat was shared with the bot
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ChatShared? ChatShared { get; set; }

    /// <summary>
    /// Optional. The domain name of the website on which the user has logged in
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string? ConnectedWebsite { get; set; }

    /// <summary>
    /// Optional. Service message: the user allowed the bot added to the attachment menu to write messages
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public WriteAccessAllowed? WriteAccessAllowed { get; set; }

    /// <summary>
    /// Optional. Telegram Passport data
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public PassportData? PassportData { get; set; }

    /// <summary>
    /// Optional. Service message. A user in the chat triggered another user's proximity alert while
    /// sharing Live Location
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ProximityAlertTriggered? ProximityAlertTriggered { get; set; }

    /// <summary>
    /// Optional. Service message: user boosted the chat
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ChatBoostAdded? BoostAdded { get; set; }

    /// <summary>
    /// Optional. Service message: forum topic created
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ForumTopicCreated? ForumTopicCreated { get; set; }

    /// <summary>
    /// Optional. Service message: forum topic edited
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ForumTopicEdited? ForumTopicEdited { get; set; }

    /// <summary>
    /// Optional. Service message: forum topic closed
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ForumTopicClosed? ForumTopicClosed { get; set; }

    /// <summary>
    /// Optional. Service message: forum topic reopened
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public ForumTopicReopened? ForumTopicReopened { get; set; }

    /// <summary>
    /// Optional. Service message: the 'General' forum topic hidden
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public GeneralForumTopicHidden? GeneralForumTopicHidden { get; set; }

    /// <summary>
    /// Optional. Service message: the 'General' forum topic unhidden
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public GeneralForumTopicUnhidden? GeneralForumTopicUnhidden { get; set; }

    /// <summary>
    /// Optional. Service message: a scheduled giveaway was created
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public GiveawayCreated? GiveawayCreated { get; set; }

    /// <summary>
    /// Optional. The message is a scheduled giveaway message
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public Giveaway? Giveaway { get; set; }

    /// <summary>
    /// Optional. A giveaway with public winners was completed
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public GiveawayWinners? GiveawayWinners { get; set; }

    /// <summary>
    /// Optional. Service message: a giveaway without public winners was completed
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public GiveawayCompleted? GiveawayCompleted { get; set; }

    /// <summary>
    /// Optional. Service message: video chat scheduled
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public VideoChatScheduled? VideoChatScheduled { get; set; }

    /// <summary>
    /// Optional. Service message: video chat started
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public VideoChatStarted? VideoChatStarted { get; set; }

    /// <summary>
    /// Optional. Service message: video chat ended
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public VideoChatEnded? VideoChatEnded { get; set; }

    /// <summary>
    /// Optional. Service message: new participants invited to a video chat
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public VideoChatParticipantsInvited? VideoChatParticipantsInvited { get; set; }

    /// <summary>
    /// Optional. Service message: data sent by a Web App
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public WebAppData? WebAppData { get; set; }

    /// <summary>
    /// Optional. Inline keyboard attached to the message. <see cref="LoginUrl"/> buttons are represented as
    /// ordinary url buttons.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
    public InlineKeyboardMarkup? ReplyMarkup { get; set; }

    /// <summary>
    /// Gets the <see cref="MessageType"/> of the <see cref="Message"/>
    /// </summary>
    /// <value>
    /// The <see cref="MessageType"/> of the <see cref="Message"/>
    /// </value>
    [JsonIgnore]
    public MessageType Type =>
        this switch
        {
            { Text: not null }                          => MessageType.Text,
            { Animation: not null }                     => MessageType.Animation,
            { Audio: not null }                         => MessageType.Audio,
            { Document: not null }                      => MessageType.Document,
            { Photo: not null }                         => MessageType.Photo,
            { Sticker: not null }                       => MessageType.Sticker,
            { Story: not null }                         => MessageType.Story,
            { Video: not null }                         => MessageType.Video,
            { VideoNote: not null }                     => MessageType.VideoNote,
            { Voice: not null }                         => MessageType.Voice,
            { Contact: not null }                       => MessageType.Contact,
            { Dice: not null }                          => MessageType.Dice,
            { Game: not null }                          => MessageType.Game,
            { Poll: not null }                          => MessageType.Poll,
            { Venue: not null }                         => MessageType.Venue,
            { Location: not null } and { Venue: null }  => MessageType.Location,
            { NewChatMembers.Length: > 0 }              => MessageType.NewChatMembers,
            { LeftChatMember: not null }                => MessageType.LeftChatMember,
            { NewChatTitle: not null }                  => MessageType.NewChatTitle,
            { NewChatPhoto: not null }                  => MessageType.NewChatPhoto,
            { DeleteChatPhoto: not null }               => MessageType.DeleteChatPhoto,
            { GroupChatCreated: not null }              => MessageType.GroupChatCreated,
            { SupergroupChatCreated: not null }         => MessageType.SupergroupChatCreated,
            { ChannelChatCreated: not null }            => MessageType.ChannelChatCreated,
            { MessageAutoDeleteTimerChanged: not null } => MessageType.MessageAutoDeleteTimerChanged,
            { MigrateToChatId: not null }               => MessageType.MigrateToChatId,
            { MigrateFromChatId: not null }             => MessageType.MigrateFromChatId,
            { PinnedMessage: not null }                 => MessageType.PinnedMessage,
            { Invoice: not null }                       => MessageType.Invoice,
            { SuccessfulPayment: not null }             => MessageType.SuccessfulPayment,
            { UsersShared: not null }                   => MessageType.UsersShared,
            { UserShared: not null }                    => MessageType.UserShared,
            { ChatShared: not null }                    => MessageType.ChatShared,
            { ConnectedWebsite: not null }              => MessageType.ConnectedWebsite,
            { WriteAccessAllowed: not null }            => MessageType.WriteAccessAllowed,
            { PassportData: not null }                  => MessageType.PassportData,
            { ProximityAlertTriggered: not null }       => MessageType.ProximityAlertTriggered,
            { BoostAdded: not null }                    => MessageType.BoostAdded,
            { ForumTopicCreated: not null }             => MessageType.ForumTopicCreated,
            { ForumTopicEdited: not null }              => MessageType.ForumTopicEdited,
            { ForumTopicClosed: not null }              => MessageType.ForumTopicClosed,
            { ForumTopicReopened: not null }            => MessageType.ForumTopicReopened,
            { GeneralForumTopicHidden: not null }       => MessageType.GeneralForumTopicHidden,
            { GeneralForumTopicUnhidden: not null }     => MessageType.GeneralForumTopicUnhidden,
            { GiveawayCreated: not null }               => MessageType.GiveawayCreated,
            { Giveaway: not null }                      => MessageType.Giveaway,
            { GiveawayWinners: not null }               => MessageType.GiveawayWinners,
            { GiveawayCompleted: not null }             => MessageType.GiveawayCompleted,
            { VideoChatScheduled: not null }            => MessageType.VideoChatScheduled,
            { VideoChatStarted: not null }              => MessageType.VideoChatStarted,
            { VideoChatEnded: not null }                => MessageType.VideoChatEnded,
            { VideoChatParticipantsInvited: not null }  => MessageType.VideoChatParticipantsInvited,
            { WebAppData: not null }                    => MessageType.WebAppData,
            _                                           => MessageType.Unknown
        };
}
