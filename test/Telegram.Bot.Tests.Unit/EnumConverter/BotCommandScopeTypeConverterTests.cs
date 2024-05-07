using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class BotCommandScopeTypeConverterTests
{
    [Theory]
    [ClassData(typeof(BotCommandScopeData))]
    public void Should_Convert_BotCommandScopeType_To_String(BotCommandScope botCommandScope, string value)
    {
        string result = JsonSerializer.Serialize(botCommandScope, TelegramBotClientJsonSerializerContext.Instance.BotCommandScope);

        Assert.Equal(value, result);
    }

    [Theory]
    [ClassData(typeof(BotCommandScopeData))]
    public void Should_Convert_String_To_BotCommandScopeType(BotCommandScope expectedResult, string value)
    {
        BotCommandScope? result = JsonSerializer.Deserialize(value, TelegramBotClientJsonSerializerContext.Instance.BotCommandScope);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_BotCommandScopeType()
    {
        BotCommandScopeType? result =
            JsonSerializer.Deserialize(int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.BotCommandScopeType);
        Assert.NotNull(result);
        Assert.Equal((BotCommandScopeType)0, result);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_BotCommandScopeType()
    {
        Assert.Throws<JsonException>(() =>
            JsonSerializer.Serialize((BotCommandScopeType)int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.BotCommandScopeType));
    }

    private class BotCommandScopeData : IEnumerable<object[]>
    {
        private static BotCommandScope NewBotCommandScope(BotCommandScopeType botCommandScopeType)
        {
            return botCommandScopeType switch
            {
                BotCommandScopeType.Default => new BotCommandScopeDefault(),
                BotCommandScopeType.AllPrivateChats => new BotCommandScopeAllPrivateChats(),
                BotCommandScopeType.AllGroupChats => new BotCommandScopeAllGroupChats(),
                BotCommandScopeType.AllChatAdministrators => new BotCommandScopeAllChatAdministrators(),
                BotCommandScopeType.Chat => new BotCommandScopeChat { ChatId = 1234 },
                BotCommandScopeType.ChatAdministrators => new BotCommandScopeChatAdministrators { ChatId = 1234 },
                BotCommandScopeType.ChatMember => new BotCommandScopeChatMember { ChatId = 123456789, UserId = 1234 },
                _ => throw new ArgumentOutOfRangeException(nameof(botCommandScopeType), botCommandScopeType, null),
            };

        }

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [NewBotCommandScope(BotCommandScopeType.Default), """{"type":"default"}"""];
            yield return [NewBotCommandScope(BotCommandScopeType.AllPrivateChats), """{"type":"all_private_chats"}"""];
            yield return [NewBotCommandScope(BotCommandScopeType.AllGroupChats), """{"type":"all_group_chats"}"""];
            yield return [NewBotCommandScope(BotCommandScopeType.AllChatAdministrators), """{"type":"all_chat_administrators"}"""];
            yield return [NewBotCommandScope(BotCommandScopeType.Chat), """{"type":"chat","chat_id":1234}"""];
            yield return [NewBotCommandScope(BotCommandScopeType.ChatAdministrators), """{"type":"chat_administrators","chat_id":1234}"""];
            yield return [NewBotCommandScope(BotCommandScopeType.ChatMember), """{"type":"chat_member","chat_id":123456789,"user_id":1234}"""];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
