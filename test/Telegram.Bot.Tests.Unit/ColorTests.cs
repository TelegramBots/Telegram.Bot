using System;
using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Tests.Unit;

public class ColorTests
{
    [Theory]
    [InlineData(0x646464, 100, 100, 100)] // 6579300
    [InlineData(0x6FB9F0, 111, 185, 240)] // 7322096
    [InlineData(0xFFD67E, 255, 214, 126)] // 16766590
    [InlineData(0xCB86DB, 203, 134, 219)] // 13338331
    [InlineData(0x8EEE98, 142, 238, 152)] // 9367192
    [InlineData(0xFF93B2, 255, 147, 178)] // 16749490
    [InlineData(0xFB6F5F, 251, 111, 95)]  // 16478047
    [InlineData(0xFFFFFF, 255, 255, 255)] // 16777215
    [InlineData(0x0, 0, 0, 0)]            // 0
    public void Should_Create_Int_Color_Instance_With_Correct_Components(int color, int red, int green, int blue)
    {
        var (r, g, b) = new Color(color);
        Assert.Equal(red, r);
        Assert.Equal(green, g);
        Assert.Equal(blue, b);
    }

    [Theory]
    [InlineData(0x646464, 100, 100, 100)] // 6579300
    [InlineData(0x6FB9F0, 111, 185, 240)] // 7322096
    [InlineData(0xFFD67E, 255, 214, 126)] // 16766590
    [InlineData(0xCB86DB, 203, 134, 219)] // 13338331
    [InlineData(0x8EEE98, 142, 238, 152)] // 9367192
    [InlineData(0xFF93B2, 255, 147, 178)] // 16749490
    [InlineData(0xFB6F5F, 251, 111, 95)]  // 16478047
    [InlineData(0xFFFFFF, 255, 255, 255)] // 16777215
    [InlineData(0x0, 0, 0, 0)]            // 0
    public void Should_Create_Uint_Color_Instance_With_Correct_Components(uint color, int red, int green, int blue)
    {
        var (r, g, b) = new Color(color);
        Assert.Equal(red, r);
        Assert.Equal(green, g);
        Assert.Equal(blue, b);
    }

    [Theory]
    [InlineData(100, 100, 100, 0x646464)] // 6579300
    [InlineData(111, 185, 240, 0x6FB9F0)] // 7322096
    [InlineData(255, 214, 126, 0xFFD67E)] // 16766590
    [InlineData(203, 134, 219, 0xCB86DB)] // 13338331
    [InlineData(142, 238, 152, 0x8EEE98)] // 9367192
    [InlineData(255, 147, 178, 0xFF93B2)] // 16749490
    [InlineData(251, 111, 95, 0xFB6F5F)]  // 16478047
    [InlineData(255, 255, 255, 0xFFFFFF)] // 16777215
    [InlineData(0, 0, 0, 0x0)]            // 0
    public void Should_Create_Int_Color_Instance_From_Components(int red, int green, int blue, int color)
    {
        Color c = new(red: red, green: green, blue: blue);
        Assert.Equal(red, c.Red);
        Assert.Equal(green, c.Green);
        Assert.Equal(blue, c.Blue);
        Assert.Equal(c.ToInt(), color);
    }

    [Theory]
    [InlineData(100, 100, 100, 0x646464)] // 6579300
    [InlineData(111, 185, 240, 0x6FB9F0)] // 7322096
    [InlineData(255, 214, 126, 0xFFD67E)] // 16766590
    [InlineData(203, 134, 219, 0xCB86DB)] // 13338331
    [InlineData(142, 238, 152, 0x8EEE98)] // 9367192
    [InlineData(255, 147, 178, 0xFF93B2)] // 16749490
    [InlineData(251, 111, 95, 0xFB6F5F)]  // 16478047
    [InlineData(255, 255, 255, 0xFFFFFF)] // 16777215
    [InlineData(0, 0, 0, 0x0)]            // 0
    public void Should_Create_Uint_Color_Instance_From_Components(uint red, uint green, uint blue, int color)
    {
        Color c = new(red: red, green: green, blue: blue);
        Assert.Equal((int)red, c.Red);
        Assert.Equal((int)green, c.Green);
        Assert.Equal((int)blue, c.Blue);
        Assert.Equal(c.ToInt(), color);
    }

    [Theory]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    [InlineData(16777216)]
    public void Should_Throw_On_Incorrect_Int_Color_Value(int color) =>
        Assert.Throws<ArgumentOutOfRangeException>(() => new Color(color));

    [Theory]
    [InlineData(uint.MaxValue)]
    [InlineData(16777216)]
    public void Should_Throw_On_Incorrect_Uint_Color_Value(uint color) =>
        Assert.Throws<ArgumentOutOfRangeException>(() => new Color(color));

    [Theory]
    [InlineData(255, 255, 256, "blue")]
    [InlineData(256, 255, 255, "red")]
    [InlineData(255, 256, 255, "green")]
    [InlineData(-1, 255, 255, "red")]
    [InlineData(255, -1, 255, "green")]
    [InlineData(255, 255, -1, "blue")]
    public void Should_Throw_On_Incorrect_Int_Color_Component(int red, int green, int blue, string componentName)
    {
        ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => new Color(red: red, green: green, blue: blue)
        );
        Assert.Equal(componentName, exception.ParamName);
    }

    [Theory]
    [InlineData(255, 255, 256, "blue")]
    [InlineData(256, 255, 255, "red")]
    [InlineData(255, 256, 255, "green")]
    public void Should_Throw_On_Incorrect_Uint_Color_Component(uint red, uint green, uint blue, string componentName)
    {
        ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(
            () => new Color(red: red, green: green, blue: blue)
        );
        Assert.Equal(componentName, exception.ParamName);
    }
}
