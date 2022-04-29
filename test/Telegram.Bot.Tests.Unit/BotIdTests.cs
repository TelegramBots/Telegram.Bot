using System;
using Xunit;

namespace Telegram.Bot.Tests.Unit;

public class BotIdTests
{
    [Theory]
    [InlineData("1234567:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy", 1234567)]
    [InlineData("9:jdsaghdfilghdfiugherh", 9)]
    [InlineData("0:foo", 0)]
    [InlineData("5:", 5)]
    [InlineData("-123::::", -123)]
    public void Should_Parse_Bot_Id(string token, long expectedId)
    {
        TelegramBotClientOptions options = new(token);
        Assert.Equal(expectedId, options.BotId);
    }

    [Fact]
    public void Should_Throw_On_Null_Token()
    {
        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new TelegramBotClient(token: null!)
        );
        Assert.Equal("token", exception.ParamName);
    }

    [Fact]
    public void Should_Throw_On_Null_Options()
    {
        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new TelegramBotClient(options: null!)
        );
        Assert.Equal("options", exception.ParamName);
    }

    [Theory]
    [InlineData("")]
    [InlineData(":")]
    [InlineData("1234567")]
    [InlineData("INVALID:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy")]
    public void Should_Throw_On_Invalid_Token(string invalidToken)
    {
        TelegramBotClientOptions options = new(token: invalidToken);
        Assert.Null(options.BotId);
    }
}
