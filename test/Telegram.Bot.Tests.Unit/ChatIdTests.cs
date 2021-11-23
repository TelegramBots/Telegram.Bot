using System;
using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Unit
{
    public class ChatIdTests
    {
        [Fact]
        public void Check_Constructor()
        {
            var chatId = new ChatId(123);

            //check int & long
            Assert.Null(chatId.Username);
            Assert.Equal(123, chatId.Identifier);

            chatId = new ChatId(123L);
            Assert.Null(chatId.Username);
            Assert.Equal(123L, chatId.Identifier);

            // check string values
            chatId = new ChatId(123.ToString());
            Assert.Null(chatId.Username);
            Assert.Equal(123, chatId.Identifier);

            chatId = new ChatId(123L.ToString());
            Assert.Null(chatId.Username);
            Assert.Equal(123L, chatId.Identifier);

            chatId = new ChatId("@valid_username");
            Assert.Equal("@valid_username", chatId.Username);
            Assert.Null(chatId.Identifier);

            Assert.Throws<ArgumentException>(() => new ChatId("username"));
        }


        [Fact]
        public void Should_ToString()
        {
            //int
            Assert.Equal("123", new ChatId("123").ToString());
            Assert.Equal("123", new ChatId(123).ToString());

            //long
            Assert.Equal("123456789012", new ChatId((123456789012)).ToString());
            Assert.Equal("123456789012", new ChatId("123456789012").ToString());


            //username
            Assert.Equal("@valid_username", new ChatId("@valid_username").ToString());


            Assert.True(848993490 != new ChatId(1830775754));
        }

        [Fact]
        public void Equals_Test()
        {
            //with Identifier
            var chatId = new ChatId(123);
            Assert.True(chatId.Equals(123));
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.False(123.Equals(chatId)); // to be aware
            Assert.True(chatId == 123);
            Assert.True(123 == chatId);

            chatId = new ChatId("123");
            Assert.True(chatId.Equals(123));
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.False(123.Equals(chatId)); // to be aware
            Assert.True(chatId == 123);
            Assert.True(123 == chatId);

            //with username
            chatId = new ChatId("@username");
            Assert.True(chatId.Equals("@username"));
            Assert.True("@username".Equals(chatId));
            Assert.True(chatId == "@username");
            Assert.True("@username" == chatId);

            //with other ChatId
            Assert.Equal(chatId, chatId);
            Assert.Equal(new ChatId(123), new ChatId(123));
        }

        [Theory]
        [ClassData(typeof(ChatIdTestData))]
        public void Equals_Test2(ChatId id1, ChatId id2, bool result)
        {
            Assert.Equal(id1==id2, result);
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
}
