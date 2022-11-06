using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Telegram.Bot.Types;

/// <summary>
/// Represent a color in RGB space
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public readonly record struct Color
{
    const int MaxRgbValue = 16777215;
    const int BlueShift = 4;
    const int GreenShift = BlueShift * 2;
    const int RedShift = GreenShift * 2;

    /// <summary>
    /// Red component
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Red { get; }

    /// <summary>
    /// Green component
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Green { get; }

    /// <summary>
    /// Blue component
    /// </summary>
    [JsonProperty(Required = Required.Always)]
    public int Blue { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="red">Red component</param>
    /// <param name="green">Green component</param>
    /// <param name="blue">Blue component</param>
    public Color(int red, int green, int blue)
    {
        CheckByte(red);
        CheckByte(green);
        CheckByte(blue);
        (Red, Green, Blue) = (red, green, blue);
    }

    /// <summary>
    /// Instantiate a new color value
    /// </summary>
    /// <param name="red">Red component</param>
    /// <param name="green">Green component</param>
    /// <param name="blue">Blue component</param>
    public Color(uint red, uint green, uint blue)
    {
        CheckByte(red);
        CheckByte(green);
        CheckByte(blue);
        (Red, Green, Blue) = ((int)red, (int)green, (int)blue);
    }

    /// <summary>
    /// Instantiate a new color value
    /// </summary>
    /// <param name="color">Numeric value of color in RGB space</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public Color(int color)
    {
        if (color is > MaxRgbValue or < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(color), color, "Color is out of range");
        }

        Red = color >> RedShift;
        Green = color >> GreenShift;
        Blue = color >> BlueShift;
    }

    /// <summary>
    /// Instantiate a new color value
    /// </summary>
    /// <param name="color">Numeric value of color in RGB space</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public Color(uint color)
    {
        if (color is > MaxRgbValue or < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(color), color, "Color is out of range");
        }

        Red = unchecked((int)color >> RedShift);
        Green = unchecked((int)color >> GreenShift);
        Blue = unchecked((int)color >> BlueShift);
    }

    public override string ToString() => $"#{ToInt():X24}";

    /// <summary>
    ///
    /// </summary>
    /// <returns>Numeric representation of current color</returns>
    public int ToInt()
    {
#if NETCOREAPP3_1_OR_GREATER
        ReadOnlySpan<byte> components = stackalloc [] { (byte)Red,(byte)Green, (byte)Blue };
        return BitConverter.ToInt32(components);
#else
        byte[] components = { (byte)Red,(byte)Green, (byte)Blue };
        return BitConverter.ToInt32(components, 0);
#endif
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public byte[] ToBytes() => BitConverter.GetBytes(ToInt());

    /// <summary>
    ///
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static explicit operator int(Color color) => color.ToInt();

    /// <summary>
    ///
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static explicit operator byte[](Color color) => color.ToBytes();

    /// <summary>
    /// Blue color
    /// </summary>
    public static Color BlueColor = new(0x6FB9F0);

    /// <summary>
    /// Yellow color
    /// </summary>
    public static Color YellowColor = new(0xFFD67E);

    /// <summary>
    /// Violet color
    /// </summary>
    public static Color VioletColor = new(0xCB86DB);

    /// <summary>
    /// Green color
    /// </summary>
    public static Color GreenColor = new(0x8EEE98);

    /// <summary>
    /// Pink color
    /// </summary>
    public static Color PinkColor = new(0xFF93B2);

    /// <summary>
    /// Red color
    /// </summary>
    public static Color RedColor = new(0xFB6F5F);

    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="value"/> is out of byte range
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void CheckByte(long value, [CallerArgumentExpression("value")] string? componentName = default)
    {
        if (value is > byte.MaxValue or < byte.MinValue)
            throw new ArgumentOutOfRangeException(
                paramName: componentName,
                actualValue: value,
                message: $"{componentName} component is out of range"
            );
    }
}
