using System.Runtime.CompilerServices;

namespace Telegram.Bot.Types;

/// <summary>
/// Represent a color in RGB space
/// </summary>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public readonly record struct Color
{
    const int MaxRgbValue = 16777215;
    const int GreenShift = 8;
    const int RedShift = 16;
    const int BlueMask = 0xFF;
    const int GreenMask = 0xFF00;
    const int RedMask = 0xFF0000;

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

        (Red, Green, Blue) = Convert(color);
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

        (Red, Green, Blue) = Convert((int)color);
    }

    /// <inheritdoc />
    public override string ToString() => $"#{ToInt():X6}";

    /// <summary>
    /// Converts current <see cref="Color"/> instance to it's numeric representation
    /// </summary>
    /// <returns>Numeric representation of current color</returns>
    public int ToInt() => (Red << RedShift) | (Green << GreenShift) | Blue;

    /// <summary>
    /// Converts current <see cref="Color"/> instance to it's numeric representation
    /// </summary>
    /// <returns>Numeric representation of current color</returns>
    public uint ToUint() => (uint)ToInt();

    /// <summary>
    /// Converts current <see cref="Color"/> instance to it's <see cref="T:byte[]"/> representation
    /// </summary>
    /// <returns></returns>
    public byte[] ToBytes() => BitConverter.GetBytes(ToInt());

    /// <summary>
    /// Deconstructs current instance of <see cref="Color"/> into it's RGB components
    /// </summary>
    /// <param name="red"></param>
    /// <param name="green"></param>
    /// <param name="blue"></param>
    public void Deconstruct(out int red, out int green, out int blue) => (red, green, blue) = (Red, Green, Blue);

    /// <summary>
    /// Converts current <see cref="Color"/> instance to it's numeric representation
    /// </summary>
    /// <param name="color"></param>
    /// <returns>Numeric representation of the current <see cref="Color"/></returns>
    public static explicit operator int(Color color) => color.ToInt();

    /// <summary>
    /// Converts current <see cref="Color"/> instance to it's numeric representation
    /// </summary>
    /// <param name="color"></param>
    /// <returns>Numeric representation of the current <see cref="Color"/></returns>
    public static explicit operator uint(Color color) => color.ToUint();

    /// <summary>
    /// Converts current <see cref="Color"/> instance to it's <see cref="T:byte[]"/> representation
    /// </summary>
    /// <param name="color"></param>
    /// <returns><see cref="T:byte[]"/> representation of the current <see cref="Color"/></returns>
    public static explicit operator byte[](Color color) => color.ToBytes();

    /// <summary>
    /// Blue color
    /// </summary>
    public static readonly Color BlueColor = new(0x6FB9F0); // 111, 185, 240

    /// <summary>
    /// Yellow color
    /// </summary>
    public static readonly Color YellowColor = new(0xFFD67E); // 255, 214, 126

    /// <summary>
    /// Violet color
    /// </summary>
    public static readonly Color VioletColor = new(0xCB86DB); // 203, 134, 219

    /// <summary>
    /// Green color
    /// </summary>
    public static readonly Color GreenColor = new(0x8EEE98); // 142, 238, 152

    /// <summary>
    /// Pink color
    /// </summary>
    public static readonly Color PinkColor = new(0xFF93B2); // 255, 147, 178

    /// <summary>
    /// Red color
    /// </summary>
    public static readonly Color RedColor = new(0xFB6F5F); // 251, 111, 95

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static (int red, int green, int blue) Convert(int color) =>
        ((color & RedMask) >> RedShift, (color & GreenMask) >> GreenShift, color & BlueMask);
}
