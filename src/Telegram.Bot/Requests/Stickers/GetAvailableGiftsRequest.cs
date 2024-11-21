namespace Telegram.Bot.Requests;

/// <summary>Returns the list of gifts that can be sent by the bot to users.<para>Returns: A <see cref="GiftList"/> object.</para></summary>
public partial class GetAvailableGiftsRequest() : ParameterlessRequest<GiftList>("getAvailableGifts")
{}
