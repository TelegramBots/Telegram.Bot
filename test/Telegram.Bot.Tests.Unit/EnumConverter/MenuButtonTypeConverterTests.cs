using System.Collections;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Xunit;

namespace Telegram.Bot.Tests.Unit.EnumConverter;

public class MenuButtonTypeConverterTests
{
    [Theory]
    [ClassData(typeof(MenuButtonData))]
    public void Should_Convert_MenuButtonType_To_String(MenuButton menuButton, string value)
    {
        string result = JsonSerializer.Serialize(menuButton, TelegramBotClientJsonSerializerContext.Instance.MenuButton);

        Assert.Equal(value, result);
    }

    [Theory]
    [ClassData(typeof(MenuButtonData))]
    public void Should_Convert_String_To_MenuButtonType(MenuButton expectedResult, string value)
    {
        MenuButton? result = JsonSerializer.Deserialize(value, TelegramBotClientJsonSerializerContext.Instance.MenuButton);

        Assert.NotNull(result);
        Assert.Equal(expectedResult.Type, result.Type);
    }

    [Fact]
    public void Should_Return_Zero_For_Incorrect_MenuButtonType()
    {
        MenuButtonType? result = JsonSerializer.Deserialize(int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.MenuButtonType);

        Assert.NotNull(result);
        Assert.Equal((MenuButtonType)0, result);
    }

    [Fact]
    public void Should_Throw_JsonException_For_Incorrect_MenuButtonType()
    {
        Assert.Throws<JsonException>(() => JsonSerializer.Serialize((MenuButtonType)int.MaxValue, TelegramBotClientJsonSerializerContext.Instance.MenuButtonType));
    }

    private class MenuButtonData : IEnumerable<object[]>
    {
        private static MenuButton NewMenuButton(MenuButtonType menuButtonType)
        {
            return menuButtonType switch
            {
                MenuButtonType.Default => new MenuButtonDefault(),
                MenuButtonType.Commands => new MenuButtonCommands() ,
                MenuButtonType.WebApp => new MenuButtonWebApp
                {
                    Text = "a", WebApp = new WebAppInfo
                    {
                        Url = "https://example.com",
                    }
                },
                _ => throw new ArgumentOutOfRangeException(nameof(menuButtonType), menuButtonType, null)
            };

        }

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [NewMenuButton(MenuButtonType.Default), """{"type":"default"}"""];
            yield return [NewMenuButton(MenuButtonType.Commands), """{"type":"commands"}"""];
            yield return [NewMenuButton(MenuButtonType.WebApp), """{"type":"web_app","text":"a","web_app":{"url":"https://example.com"}}"""];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
