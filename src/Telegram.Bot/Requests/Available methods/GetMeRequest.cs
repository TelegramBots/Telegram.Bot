namespace Telegram.Bot.Requests;

/// <summary>A simple method for testing your bot's authentication token.<para>Returns: Basic information about the bot in form of a <see cref="User"/> object.</para></summary>
public partial class GetMeRequest : ParameterlessRequest<User>
{
    /// <summary>Instantiates a new <see cref="GetMeRequest"/></summary>
    public GetMeRequest() : base("getMe") { }
}
