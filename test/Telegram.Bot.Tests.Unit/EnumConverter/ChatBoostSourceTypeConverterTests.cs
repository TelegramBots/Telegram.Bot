using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class ChatBoostSourceTypeConverterTests
{
    [Theory]
    [ClassData(typeof(ChatBoostSourceData))]
    public void Should_Convert_ChatBoostSourceType_To_String(ChatBoostSource chatBoostSource, string value)
    {
        string result = JsonSerializer.Serialize(chatBoostSource, TelegramBotClientJsonSerializerContext.Instance.ChatBoostSource);

        Assert.Equal(value, result);
    }

    [Theory]
    [ClassData(typeof(ChatBoostSourceData))]
    public void Should_Convert_String_ToChatBoostSourceType(ChatBoostSource expectedResult, string value)
    {
        ChatBoostSource? result = JsonSerializer.Deserialize(value, TelegramBotClientJsonSerializerContext.Instance.ChatBoostSource);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Source, result.Source);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_ChatBoostSourceType()
    {
        ChatBoostSourceType? result = JsonSerializer.Deserialize(int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.ChatBoostSourceType);

        Assert.NotNull(result);
        Assert.Equal((ChatBoostSourceType)0, result);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_ChatBoostSourceType()
    {
        Assert.Throws<JsonException>(() =>
            JsonSerializer.Serialize((ChatBoostSourceType)int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.ChatBoostSourceType));
    }

    private class ChatBoostSourceData : IEnumerable<object[]>
    {
        private static ChatBoostSource NewChatBoostSource(ChatBoostSourceType chatBoostSourceType)
        {
            return chatBoostSourceType switch
            {
                ChatBoostSourceType.Premium => new ChatBoostSourcePremium
                {
                    User = NewUser()
                },
                ChatBoostSourceType.GiftCode => new ChatBoostSourceGiftCode
                {
                    User = NewUser()
                },
                ChatBoostSourceType.Giveaway => new ChatBoostSourceGiveaway
                {
                    GiveawayMessageId = 1
                },
                _ => throw new ArgumentOutOfRangeException(nameof(chatBoostSourceType), chatBoostSourceType, null)
            };
        }

        private static User NewUser()
        {
            return new User
            {
                Id = 1,
                IsBot = false,
                FirstName = "FirstName",
            };
        }
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [NewChatBoostSource(ChatBoostSourceType.Premium), """{"source":"premium","user":{"id":1,"is_bot":false,"first_name":"FirstName"}}"""];
            yield return [NewChatBoostSource(ChatBoostSourceType.GiftCode), """{"source":"gift_code","user":{"id":1,"is_bot":false,"first_name":"FirstName"}}"""];
            yield return [NewChatBoostSource(ChatBoostSourceType.Giveaway), """{"source":"giveaway","giveaway_message_id":1}"""];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
