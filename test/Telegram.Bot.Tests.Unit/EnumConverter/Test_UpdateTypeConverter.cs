using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class Test_UpdateTypeConverter
{
    [Theory]
    [InlineData(UpdateType.Unknown, "unknown")]
    [InlineData(UpdateType.Message, "message")]
    [InlineData(UpdateType.InlineQuery, "inline_query")]
    [InlineData(UpdateType.ChosenInlineResult, "chosen_inline_result")]
    [InlineData(UpdateType.CallbackQuery, "callback_query")]
    [InlineData(UpdateType.EditedMessage, "edited_message")]
    [InlineData(UpdateType.ChannelPost, "channel_post")]
    [InlineData(UpdateType.EditedChannelPost, "edited_channel_post")]
    [InlineData(UpdateType.ShippingQuery, "shipping_query")]
    [InlineData(UpdateType.PreCheckoutQuery, "pre_checkout_query")]
    [InlineData(UpdateType.Poll, "poll")]
    [InlineData(UpdateType.PollAnswer, "poll_answer")]
    [InlineData(UpdateType.MyChatMember, "my_chat_member")]
    [InlineData(UpdateType.ChatMember, "chat_member")]
    [InlineData(UpdateType.ChatJoinRequest, "chat_join_request")]
    public void Sould_Convert_UpdateType_To_String(UpdateType updateType, string value)
    {
        Update update = new Update() { Type = updateType };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonConvert.SerializeObject(update);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(UpdateType.Unknown, "unknown")]
    public void Sould_Convert_String_To_UpdateType(UpdateType updateType, string value)
    {
        Update expectedResult = new Update() { Type = updateType };
        string jsonData = @$"{{""type"":""{value}""}}";

        Update result = JsonConvert.DeserializeObject<Update>(jsonData);

        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Sould_Return_Unknown_For_Incorrect_UpdateType()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        Update result = JsonConvert.DeserializeObject<Update>(jsonData);

        Assert.Equal(UpdateType.Unknown, result.Type);
    }

    [Fact]
    public void Sould_Convert_To_Unknown_For_Incorrect_UpdateType()
    {
        Update update = new Update() { Type = (UpdateType)int.MaxValue };
        const string expectedResult = @"{""type"":""unknown""}";

        string result = JsonConvert.SerializeObject(update);

        Assert.Equal(expectedResult, result);
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    private class Update
    {
        [JsonProperty(Required = Required.Always)]
        public UpdateType Type { get; init; }
    }
}
