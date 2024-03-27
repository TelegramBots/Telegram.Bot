using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class ChatMemberStatusConverterTests
{
    [Theory]
    [InlineData(ChatMemberStatus.Creator, "creator")]
    [InlineData(ChatMemberStatus.Administrator, "administrator")]
    [InlineData(ChatMemberStatus.Member, "member")]
    [InlineData(ChatMemberStatus.Left, "left")]
    [InlineData(ChatMemberStatus.Kicked, "kicked")]
    [InlineData(ChatMemberStatus.Restricted, "restricted")]
    public void Should_Convert_ChatMemberStatus_To_String(ChatMemberStatus chatMemberStatus, string value)
    {
        ChatMember chatMember = new() { Type = chatMemberStatus };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonSerializer.Serialize(chatMember, JsonSerializerOptionsProvider.Options);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(ChatMemberStatus.Creator, "creator")]
    [InlineData(ChatMemberStatus.Administrator, "administrator")]
    [InlineData(ChatMemberStatus.Member, "member")]
    [InlineData(ChatMemberStatus.Left, "left")]
    [InlineData(ChatMemberStatus.Kicked, "kicked")]
    [InlineData(ChatMemberStatus.Restricted, "restricted")]
    public void Should_Convert_String_To_ChatMemberStatus(ChatMemberStatus chatMemberStatus, string value)
    {
        ChatMember expectedResult = new() { Type = chatMemberStatus };
        string jsonData = @$"{{""type"":""{value}""}}";

        ChatMember? result = JsonSerializer.Deserialize<ChatMember>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_ChatMemberStatus()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        ChatMember? result = JsonSerializer.Deserialize<ChatMember>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal((ChatMemberStatus)0, result.Type);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_ChatMemberStatus()
    {
        ChatMember chatMember = new() { Type = (ChatMemberStatus)int.MaxValue };

        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(chatMember, JsonSerializerOptionsProvider.Options));
    }


    class ChatMember
    {
        [JsonRequired]
        public ChatMemberStatus Type { get; init; }
    }
}
