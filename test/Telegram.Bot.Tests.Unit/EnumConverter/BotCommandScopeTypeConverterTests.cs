using Telegram.Bot.Types.Enums;
using Xunit;
using JsonSerializerOptionsProvider = Telegram.Bot.Serialization.JsonSerializerOptionsProvider;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class BotCommandScopeTypeConverterTests
{
    [Theory]
    [InlineData(BotCommandScopeType.Default, "default")]
    [InlineData(BotCommandScopeType.AllPrivateChats, "all_private_chats")]
    [InlineData(BotCommandScopeType.AllGroupChats, "all_group_chats")]
    [InlineData(BotCommandScopeType.AllChatAdministrators, "all_chat_administrators")]
    [InlineData(BotCommandScopeType.Chat, "chat")]
    [InlineData(BotCommandScopeType.ChatAdministrators, "chat_administrators")]
    [InlineData(BotCommandScopeType.ChatMember, "chat_member")]
    public void Should_Convert_BotCommandScopeType_To_String(BotCommandScopeType botCommandScopeType, string value)
    {
        BotCommandScope botCommandScope = new(){ Type = botCommandScopeType };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonSerializer.Serialize(botCommandScope, JsonSerializerOptionsProvider.Options);

        Assert.Equal(expectedResult, result);
    }

    [Theory]
    [InlineData(BotCommandScopeType.Default, "default")]
    [InlineData(BotCommandScopeType.AllPrivateChats, "all_private_chats")]
    [InlineData(BotCommandScopeType.AllGroupChats, "all_group_chats")]
    [InlineData(BotCommandScopeType.AllChatAdministrators, "all_chat_administrators")]
    [InlineData(BotCommandScopeType.Chat, "chat")]
    [InlineData(BotCommandScopeType.ChatAdministrators, "chat_administrators")]
    [InlineData(BotCommandScopeType.ChatMember, "chat_member")]
    public void Should_Convert_String_To_BotCommandScopeType(BotCommandScopeType botCommandScopeType, string value)
    {
        BotCommandScope expectedResult = new() { Type = botCommandScopeType };
        string jsonData = @$"{{""type"":""{value}""}}";

        BotCommandScope? result = JsonSerializer.Deserialize<BotCommandScope>(jsonData, JsonSerializerOptionsProvider.Options);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_BotCommandScopeType()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        BotCommandScope? result = JsonSerializer.Deserialize<BotCommandScope>(jsonData, JsonSerializerOptionsProvider.Options);
        Assert.NotNull(result);
        Assert.Equal((BotCommandScopeType)0, result.Type);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_BotCommandScopeType()
    {
        BotCommandScope botCommandScope = new() { Type = (BotCommandScopeType)int.MaxValue };

        Assert.Throws<JsonException>(() => JsonSerializer.Serialize(botCommandScope, JsonSerializerOptionsProvider.Options));
    }


    class BotCommandScope
    {
        [JsonRequired]
        public BotCommandScopeType Type { get; init; }
    }
}
