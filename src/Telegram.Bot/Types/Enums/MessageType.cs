namespace Telegram.Bot.Types.Enums
{
    /// <summary>
    /// The type of a <see cref="Message"/>
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// The <see cref="Message"/> is unknown
        /// </summary>
        UnknownMessage = 0,

        /// <summary>
        /// The <see cref="Message"/> contains text
        /// </summary>
        TextMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="PhotoSize"/>
        /// </summary>
        PhotoMessage,

        /// <summary>
        /// The <see cref="Message"/> contains an <see cref="Audio"/>
        /// </summary>
        AudioMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="Video"/>
        /// </summary>
        VideoMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="Voice"/>
        /// </summary>
        VoiceMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="Document"/>
        /// </summary>
        DocumentMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="Sticker"/>
        /// </summary>
        StickerMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="Location"/>
        /// </summary>
        LocationMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="Contact"/>
        /// </summary>
        ContactMessage,

        /// <summary>
        /// The <see cref="Message"/> contains meta informations, for example <see cref="Message.GroupChatCreated"/> or <see cref="Message.NewChatTitle"/>
        /// </summary>
        ServiceMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="Venue"/>
        /// </summary>
        VenueMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="Game"/>
        /// </summary>
        GameMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="VideoNote"/>
        /// </summary>
        VideoNoteMessage,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="Invoice"/>
        /// </summary>
        Invoice,

        /// <summary>
        /// The <see cref="Message"/> contains a <see cref="SuccessfulPayment"/>
        /// </summary>
        SuccessfulPayment,
    }
}
