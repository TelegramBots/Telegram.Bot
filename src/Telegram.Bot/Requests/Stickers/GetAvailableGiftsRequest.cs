namespace Telegram.Bot.Requests;

/// <summary>Returns the list of gifts that can be sent by the bot to users.<para>Returns: A <see cref="GiftList"/> object.</para></summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public partial class GetAvailableGiftsRequest() : ParameterlessRequest<GiftList>("getAvailableGifts")
{}
