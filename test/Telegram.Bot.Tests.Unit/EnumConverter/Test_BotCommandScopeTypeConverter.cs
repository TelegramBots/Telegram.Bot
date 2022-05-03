using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class Test_BotCommandScopeTypeConverter
{
    [Theory]
    [InlineData(BotCommandScopeType.Default, "default")]
    [InlineData(BotCommandScopeType.AllPrivateChats, "all_private_chats")]
    [InlineData(BotCommandScopeType.AllGroupChats, "all_group_chats")]
    [InlineData(BotCommandScopeType.AllChatAdministrators, "all_chat_administrators")]
    [InlineData(BotCommandScopeType.Chat, "chat")]
    [InlineData(BotCommandScopeType.ChatAdministrators, "chat_administrators")]
    [InlineData(BotCommandScopeType.ChatMember, "chat_member")]
    public void Sould_Convert_BotCommandScopeType_To_String(BotCommandScopeType botCommandScopeType, string value)
    {
        BotCommandScope botCommandScope = new BotCommandScope(){ Type = botCommandScopeType };
        string expectedResult = @$"{{""type"":""{value}""}}";

        string result = JsonConvert.SerializeObject(botCommandScope);

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
    public void Sould_Convert_String_To_BotCommandScopeType(BotCommandScopeType botCommandScopeType, string value)
    {
        BotCommandScope expectedResult = new BotCommandScope() { Type = botCommandScopeType };
        string jsonData = @$"{{""type"":""{value}""}}";

        BotCommandScope result = JsonConvert.DeserializeObject<BotCommandScope>(jsonData);

        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Sould_Return_Zero_For_Incorrect_BotCommandScopeType()
    {
        string jsonData = @$"{{""type"":""{int.MaxValue}""}}";

        BotCommandScope result = JsonConvert.DeserializeObject<BotCommandScope>(jsonData);

        Assert.Equal((BotCommandScopeType)0, result.Type);
    }

    [Fact]
    public void Sould_Throw_NotSupportedException_For_Incorrect_BotCommandScopeType()
    {
        BotCommandScope botCommandScope = new BotCommandScope() { Type = (BotCommandScopeType)int.MaxValue };

        NotSupportedException ex =  Assert.Throws<NotSupportedException>(() =>
            JsonConvert.SerializeObject(botCommandScope));
    }

    [JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    private class BotCommandScope
    {
        [JsonProperty(Required = Required.Always)]
        public BotCommandScopeType Type { get; init; }
    }
}
