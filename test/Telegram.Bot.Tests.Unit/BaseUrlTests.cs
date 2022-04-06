using System;
using Xunit;

namespace Telegram.Bot.Tests.Unit;

public class BaseUrlTests
{
    [Theory]
    [InlineData("http://localhost:8081", "1234:asdfg")]
    [InlineData("https://example:4433", "1234:asdfg")]
    [InlineData("https://api.telegram.org", "1234:asdfg")]
    public void Should_Set_Custom_Base_Url(string baseUrl, string token)
    {
        TelegramBotClient botClient = new(token, baseUrl: baseUrl);

        Assert.Equal($"{baseUrl}/bot{token}", botClient.BaseRequestUrl);
        Assert.Equal($"{baseUrl}/file/bot{token}", botClient.BaseFileUrl);
    }

    [Fact]
    public void Should_Set_Telegram_Base_Url_When_Custom_Url_Is_Empty_Or_Null()
    {
        TelegramBotClient botClient = new("123", baseUrl: null);

        Assert.Equal("https://api.telegram.org/bot123", botClient.BaseRequestUrl);
        Assert.Equal("https://api.telegram.org/file/bot123", botClient.BaseFileUrl);
    }

    [Theory]
    [InlineData("12314")]
    [InlineData(":")]
    [InlineData("1234567")]
    [InlineData("INVALID:4TT8bAc8GHUspu3ERYn-KGcvsvGB9u_n4ddy")]
    public void Should_Throw_On_Invalid_Base_Url(string invalidBaseUrl)
    {
        Assert.Throws<ArgumentException>(
            () => new TelegramBotClient("123", baseUrl: invalidBaseUrl)
        );
    }
}