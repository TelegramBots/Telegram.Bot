using Telegram.Bot.Extensions;
using Xunit;

namespace Telegram.Bot.Tests.Unit;

public class FormatExtensionsTests
{
    [Fact]
    public void HtmlText_Truncate_Simple()
    {
        Assert.Equal("abcd", /**/ HtmlText.Truncate("abcd", 5, ""));
        Assert.Equal("abcd", /**/ HtmlText.Truncate("abcd", 4, ""));
        Assert.Equal("abc", /* */ HtmlText.Truncate("abcd", 3, ""));
        Assert.Equal("ab", /*  */ HtmlText.Truncate("abcd", 2, ""));
        Assert.Equal("a", /*   */ HtmlText.Truncate("abcd", 1, ""));
        Assert.Equal("", /*    */ HtmlText.Truncate("abcd", 0, ""));
        Assert.Equal("abcd", /**/ HtmlText.Truncate("abcd", 5));
        Assert.Equal("abcd", /**/ HtmlText.Truncate("abcd", 4));
        Assert.Equal("ab…", /* */ HtmlText.Truncate("abcd", 3));
        Assert.Equal("a…", /*  */ HtmlText.Truncate("abcd", 2));
        Assert.Equal("…", /*   */ HtmlText.Truncate("abcd", 1));
        Assert.Equal("abcd", /**/ HtmlText.Truncate("abcd", 5, ".."));
        Assert.Equal("abcd", /**/ HtmlText.Truncate("abcd", 4, ".."));
        Assert.Equal("a..", /* */ HtmlText.Truncate("abcd", 3, ".."));
        Assert.Equal("..", /*  */ HtmlText.Truncate("abcd", 2, ".."));
    }

    [Fact]
    public void HtmlText_Truncate_With_Surrogate_Pairs()
    {
        Assert.Equal("a🅱cd", /**/ HtmlText.Truncate("a🅱cd", 5, ""));
        Assert.Equal("a🅱cd", /**/ HtmlText.Truncate("a🅱cd", 4, ""));
        Assert.Equal("a🅱c", /* */ HtmlText.Truncate("a🅱cd", 3, ""));
        Assert.Equal("a🅱", /*  */ HtmlText.Truncate("a🅱cd", 2, ""));
        Assert.Equal("a", /*    */ HtmlText.Truncate("a🅱cd", 1, ""));
        Assert.Equal("", /*     */ HtmlText.Truncate("a🅱cd", 0, ""));
        Assert.Equal("a🅱cd", /**/ HtmlText.Truncate("a🅱cd", 5));
        Assert.Equal("a🅱cd", /**/ HtmlText.Truncate("a🅱cd", 4));
        Assert.Equal("a🅱…", /* */ HtmlText.Truncate("a🅱cd", 3));
        Assert.Equal("a…", /*   */ HtmlText.Truncate("a🅱cd", 2));
        Assert.Equal("…", /*    */ HtmlText.Truncate("a🅱cd", 1));
        Assert.Equal("a🅱cd", /**/ HtmlText.Truncate("a🅱cd", 5, ".."));
        Assert.Equal("a🅱cd", /**/ HtmlText.Truncate("a🅱cd", 4, ".."));
        Assert.Equal("a..", /* */ HtmlText.Truncate("a🅱cd", 3, ".."));
        Assert.Equal("..", /*   */ HtmlText.Truncate("a🅱cd", 2, ".."));
        Assert.Equal("🅰🅱", /**/ HtmlText.Truncate("🅰🅱", 3, ""));
        Assert.Equal("🅰🅱", /**/ HtmlText.Truncate("🅰🅱", 2, ""));
        Assert.Equal("🅰", /*  */ HtmlText.Truncate("🅰🅱", 1, ""));
        Assert.Equal("", /*     */ HtmlText.Truncate("🅰🅱", 0, ""));
        Assert.Equal("🅰🅱", /**/ HtmlText.Truncate("🅰🅱", 3));
        Assert.Equal("🅰🅱", /**/ HtmlText.Truncate("🅰🅱", 2));
        Assert.Equal("…", /*    */ HtmlText.Truncate("🅰🅱", 1));
        Assert.Equal("🅰🅱", /**/ HtmlText.Truncate("🅰🅱", 3, ".."));
        Assert.Equal("🅰🅱", /**/ HtmlText.Truncate("🅰🅱", 2, ".."));
        Assert.Equal("..", /*  */ HtmlText.Truncate("🅰x🅱", 2, ".."));
    }

    [Fact]
    public void HtmlText_Truncate_With_Entities()
    {
        Assert.Equal("a&amp;cd", /*     */ HtmlText.Truncate("a&amp;cd", 5, ""));
        Assert.Equal("a&amp;cd", /*     */ HtmlText.Truncate("a&amp;cd", 4, ""));
        Assert.Equal("a&amp;c", /*      */ HtmlText.Truncate("a&amp;cd", 3, ""));
        Assert.Equal("a&amp;", /*       */ HtmlText.Truncate("a&amp;cd", 2, ""));
        Assert.Equal("a", /*            */ HtmlText.Truncate("a&amp;cd", 1, ""));
        Assert.Equal("", /*             */ HtmlText.Truncate("a&amp;cd", 0, ""));
        Assert.Equal("a&amp;cd", /*     */ HtmlText.Truncate("a&amp;cd", 5));
        Assert.Equal("a&amp;cd", /*     */ HtmlText.Truncate("a&amp;cd", 4));
        Assert.Equal("a&amp;…", /*      */ HtmlText.Truncate("a&amp;cd", 3));
        Assert.Equal("a…", /*           */ HtmlText.Truncate("a&amp;cd", 2));
        Assert.Equal("…", /*            */ HtmlText.Truncate("a&amp;cd", 1));
        Assert.Equal("a&amp;cd", /*     */ HtmlText.Truncate("a&amp;cd", 5, ".."));
        Assert.Equal("a&amp;cd", /*     */ HtmlText.Truncate("a&amp;cd", 4, ".."));
        Assert.Equal("a..", /*          */ HtmlText.Truncate("a&amp;cd", 3, ".."));
        Assert.Equal("..", /*           */ HtmlText.Truncate("a&amp;cd", 2, ".."));
        Assert.Equal("&lt;&amp;&gt;", /**/ HtmlText.Truncate("&lt;&amp;&gt;", 4, ""));
        Assert.Equal("&lt;&amp;&gt;", /**/ HtmlText.Truncate("&lt;&amp;&gt;", 3, ""));
        Assert.Equal("&lt;&amp;", /*    */ HtmlText.Truncate("&lt;&amp;&gt;", 2, ""));
        Assert.Equal("&lt;", /*         */ HtmlText.Truncate("&lt;&amp;&gt;", 1, ""));
        Assert.Equal("", /*             */ HtmlText.Truncate("&lt;&amp;&gt;", 0, ""));
        Assert.Equal("&lt;&amp;&gt;", /**/ HtmlText.Truncate("&lt;&amp;&gt;", 4));
        Assert.Equal("&lt;&amp;&gt;", /**/ HtmlText.Truncate("&lt;&amp;&gt;", 3));
        Assert.Equal("&lt;…", /*        */ HtmlText.Truncate("&lt;&amp;&gt;", 2));
        Assert.Equal("…", /*            */ HtmlText.Truncate("&lt;&amp;&gt;", 1));
        Assert.Equal("&lt;&amp;&gt;", /**/ HtmlText.Truncate("&lt;&amp;&gt;", 4, ".."));
        Assert.Equal("&lt;&amp;&gt;", /**/ HtmlText.Truncate("&lt;&amp;&gt;", 3, ".."));
        Assert.Equal("..", /*           */ HtmlText.Truncate("&lt;&amp;&gt;", 2, ".."));
    }

    [Fact]
    public void HtmlText_Truncate_With_Tags()
    {
        Assert.Equal("a<u>bc</u>d", /*       */ HtmlText.Truncate("a<u>bc</u>d", 5, ""));
        Assert.Equal("a<u>bc</u>d", /*       */ HtmlText.Truncate("a<u>bc</u>d", 4, ""));
        Assert.Equal("a<u>bc</u>", /*        */ HtmlText.Truncate("a<u>bc</u>d", 3, ""));
        Assert.Equal("a<u>b</u>", /*         */ HtmlText.Truncate("a<u>bc</u>d", 2, ""));
        Assert.Equal("a", /*                 */ HtmlText.Truncate("a<u>bc</u>d", 1, ""));
        Assert.Equal("", /*                  */ HtmlText.Truncate("a<u>bc</u>d", 0, ""));
        Assert.Equal("a<u>bc</u>d", /*       */ HtmlText.Truncate("a<u>bc</u>d", 5));
        Assert.Equal("a<u>bc</u>d", /*       */ HtmlText.Truncate("a<u>bc</u>d", 4));
        Assert.Equal("a<u>b…</u>", /*        */ HtmlText.Truncate("a<u>bc</u>d", 3));
        Assert.Equal("a<u>…</u>", /*         */ HtmlText.Truncate("a<u>bc</u>d", 2));
        Assert.Equal("…", /*                 */ HtmlText.Truncate("a<u>bc</u>d", 1));
        Assert.Equal("a<u>bc</u>d", /*       */ HtmlText.Truncate("a<u>bc</u>d", 5, ".."));
        Assert.Equal("a<u>bc</u>d", /*       */ HtmlText.Truncate("a<u>bc</u>d", 4, ".."));
        Assert.Equal("a<u>..</u>", /*        */ HtmlText.Truncate("a<u>bc</u>d", 3, ".."));
        Assert.Equal("..", /*                */ HtmlText.Truncate("a<u>bc</u>d", 2, ".."));
        Assert.Equal("<i>a<u>bc</u>d</i>", /**/ HtmlText.Truncate("<i>a<u>bc</u>d</i>", 5, ""));
        Assert.Equal("<i>a<u>bc</u>d</i>", /**/ HtmlText.Truncate("<i>a<u>bc</u>d</i>", 4, ""));
        Assert.Equal("<i>a<u>bc</u></i>", /* */ HtmlText.Truncate("<i>a<u>bc</u>d</i>", 3, ""));
        Assert.Equal("<i>a<u>b</u></i>", /*  */ HtmlText.Truncate("<i>a<u>bc</u>d</i>", 2, ""));
        Assert.Equal("<i>a</i>", /*          */ HtmlText.Truncate("<i>a<u>bc</u>d</i>", 1, ""));
        Assert.Equal("", /*                  */ HtmlText.Truncate("<i>a<u>bc</u>d</i>", 0, ""));
        Assert.Equal("<i>a<u>bc</u>d</i>", /**/ HtmlText.Truncate("<i>a<u>bc</u>d</i>", 5));
        Assert.Equal("<i>a<u>bc</u>d</i>", /**/ HtmlText.Truncate("<i>a<u>bc</u>d</i>", 4));
        Assert.Equal("<i>a<u>b…</u></i>", /* */ HtmlText.Truncate("<i>a<u>bc</u>d</i>", 3));
        Assert.Equal("<i>a<u>…</u></i>", /*  */ HtmlText.Truncate("<i>a<u>bc</u>d</i>", 2));
        Assert.Equal("<i>…</i>", /*          */ HtmlText.Truncate("<i>a<u>bc</u>d</i>", 1));
        Assert.Equal("<i>a<u>bc</u>d</i>", /**/ HtmlText.Truncate("<i>a<u>bc</u>d</i>", 5, ".."));
        Assert.Equal("<i>a<u>bc</u>d</i>", /**/ HtmlText.Truncate("<i>a<u>bc</u>d</i>", 4, ".."));
        Assert.Equal("<i>a<u>..</u></i>", /* */ HtmlText.Truncate("<i>a<u>bc</u>d</i>", 3, ".."));
        Assert.Equal("<i>..</i>", /*         */ HtmlText.Truncate("<i>a<u>bc</u>d</i>", 2, ".."));
    }

    [Fact]
    public void HtmlText_Truncate_Invalid()
    {
        Assert.Throws<ArgumentException>(() => HtmlText.Truncate("abcd", -1, ""));
        Assert.Throws<ArgumentException>(() => HtmlText.Truncate("abcd", 0));
        Assert.Throws<ArgumentException>(() => HtmlText.Truncate("abcd", 1, ".."));
        Assert.Throws<ArgumentException>(() => HtmlText.Truncate("abcd", 0, ".."));
    }
}
