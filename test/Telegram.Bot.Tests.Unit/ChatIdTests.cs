using System;
using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Unit;

public class ChatIdTests
{
    [Theory]
    [InlineData("username")]
    [InlineData("1.23")]
    public void Should_Throw_Exception_When_Incorrect_Identifier_Provided(string badIdentifier)
    {
        Assert.Throws<ArgumentException>(() => new ChatId(badIdentifier));
    }

    [Theory]
    [InlineData(123)]
    [InlineData(123123412123341234L)]
    public void Should_Create_ChatId_From_Value(long identifier)
    {
        ChatId chatId = new(identifier);

        Assert.Null(chatId.Username);
        Assert.Equal(identifier, chatId.Identifier);
    }

    [Theory]
    [InlineData("123", 123)]
    [InlineData("123123412123341234", 123123412123341234L)]
    public void Should_Create_ChatId_From_String_Value(string identifier, long identifierValue)
    {
        ChatId chatId = new(identifier);

        Assert.Null(chatId.Username);
        Assert.Equal(identifierValue, chatId.Identifier);
    }

    [Theory]
    [InlineData("@valid_username")]
    public void Should_Create_ChatId_From_String(string identifier)
    {
        ChatId chatId = new(identifier);

        Assert.Equal(identifier, chatId.Username);
        Assert.Null(chatId.Identifier);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("123123412123341234")]
    [InlineData("@valid_username")]
    public void Should_Convert_ChatId_With_String_Value_ToString(string identifier)
    {
        ChatId chatId = new(identifier);

        Assert.Equal(identifier, chatId.ToString());
    }

    [Theory]
    [InlineData(123, "123")]
    [InlineData(123123412123341234L, "123123412123341234")]
    public void Should_Convert_ChatId_ToString(long identifier, string identifierValue)
    {
        ChatId chatId = new(identifier);

        Assert.Equal(identifierValue, chatId.ToString());
    }

    [Theory]
    [InlineData(123)]
    [InlineData(123123412123341234L)]
    public void Should_Be_Equal_ChatId_Identifier_To_Long_Value(long identifier)
    {
        ChatId chatId = new(identifier);

        Assert.True(chatId.Equals(identifier));
        // ReSharper disable once SuspiciousTypeConversion.Global
        Assert.False(identifier.Equals(chatId));
        Assert.True(chatId == identifier);
        Assert.True(identifier == chatId);
    }

    [Theory]
    [InlineData("123", 123)]
    [InlineData("123123412123341234", 123123412123341234L)]
    public void Should_Be_Equal_ChatId_Identifier_To_Value(string identifier, long identifierValue)
    {
        ChatId chatId = new(identifier);

        Assert.True(chatId.Equals(identifierValue));
        // ReSharper disable once SuspiciousTypeConversion.Global
        Assert.False(identifierValue.Equals(chatId));
        Assert.True(chatId == identifierValue);
        Assert.True(identifierValue == chatId);
    }

    [Theory]
    [InlineData("@valid_username")]
    public void Should_Be_Equal_ChatId_Username_To_Identifier(string identifier)
    {
        ChatId chatId = new(identifier);

        Assert.True(chatId.Equals(identifier));
        Assert.True(chatId == identifier);
        Assert.True(identifier == chatId);
    }

    [Theory]
    [ClassData(typeof(ChatIdTestData))]
    public void Should_Compare_ChatId_With_Equals_Operator(ChatId id1, ChatId id2, bool result)
    {
        Assert.Equal(id1 == id2, result);
    }
}

public class ChatIdTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new ChatId(0), new ChatId(0), true };
        yield return new object[] { new ChatId(50), new ChatId(50), true };
        yield return new object[] { new ChatId(100), new ChatId(50), false };
        yield return new object[] { new ChatId("@user"), new ChatId("@user"), true };
        yield return new object[] { new ChatId(50), new ChatId("@50"), false };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
