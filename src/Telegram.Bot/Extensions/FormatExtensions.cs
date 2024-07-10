using System;
using System.Collections.Generic;
using System.Text;

namespace Telegram.Bot.Extensions;

/// <summary>Helpers/Extensions for MarkdownV2 texts</summary>
public static class Markdown
{
    /// <summary>Generate MarkdownV2 text from the Message Text or Caption</summary>
    /// <param name="msg">The message</param>
    public static string? ToMarkdown(this Message msg)
    {
        if (msg.Text != null) return ToMarkdown(msg.Text, msg.Entities);
        if (msg.Caption != null) return ToMarkdown(msg.Caption, msg.CaptionEntities);
        return null;
    }

    /// <summary>Converts the (plain text + entities) format used by Telegram messages into a <a href="https://core.telegram.org/bots/api/#markdownv2-style">MarkdownV2 text</a></summary>
    /// <param name="message">The plain text, typically obtained from <see cref="Message.Text"/></param>
    /// <param name="entities">The array of formatting entities, typically obtained from <see cref="Message.Entities"/></param>
    /// <returns>The message text with MarkdownV2 formattings </returns>
    public static string ToMarkdown(string message, MessageEntity[]? entities)
	{
		if (entities == null || entities.Length == 0) return Escape(message);
		var closings = new List<(int offset, string md)>();
		var sb = new StringBuilder(message);
		int entityIndex = 0;
		var nextEntity = entities[entityIndex];
		bool inBlockQuote = false;
		char lastCh = '\0';
		for (int offset = 0, i = 0; ; offset++, i++)
		{
			while (closings.Count != 0 && offset == closings[0].offset)
			{
				var md = closings[0].md;
				closings.RemoveAt(0);
				if (i > 0 && md[0] == '_' && sb[i - 1] == '_') md = '\r' + md;
				if (md[0] == '>') { inBlockQuote = false; if (lastCh != '\n' && i < sb.Length && sb[i] != '\n') md = "\n"; else continue; }
				sb.Insert(i, md); i += md.Length;
			}
			if (i == sb.Length) break;
			if (lastCh == '\n' && inBlockQuote) sb.Insert(i++, '>');
			for (; offset == nextEntity?.Offset; nextEntity = ++entityIndex < entities.Length ? entities[entityIndex] : null)
			{
				if (EntityToMD.TryGetValue(nextEntity.Type, out var md))
				{
					var closing = (nextEntity.Offset + nextEntity.Length, md);
					if (md[0] is '[' or '!')
					{
						if (nextEntity.Type is MessageEntityType.TextLink)
							closing.md = $"]({nextEntity.Url?.Replace("\\", "\\\\").Replace(")", "\\)").Replace(">", "%3E")})";
						else if (nextEntity.Type is MessageEntityType.TextMention)
							closing.md = $"](tg://user?id={nextEntity.User?.Id})";
						else if (nextEntity.Type is MessageEntityType.CustomEmoji)
							closing.md = $"](tg://emoji?id={nextEntity.CustomEmojiId})";
					}
					else if (md[0] == '>')
					{ inBlockQuote = true; if (lastCh is not '\n' and not '\0') md = "\n>"; }
					else if (nextEntity.Type is MessageEntityType.Pre)
						md = $"```{nextEntity.Language}\n";
					int index = ~closings.BinarySearch(closing, Comparer<(int, string)>.Create((x, y) => x.Item1.CompareTo(y.Item1) | 1));
					closings.Insert(index, closing);
					if (i > 0 && md[0] == '_' && sb[i - 1] == '_') md = '\r' + md;
					sb.Insert(i, md); i += md.Length;
				}
			}
			switch (lastCh = sb[i])
			{
				case '_': case '*': case '~': case '`': case '#': case '+': case '-': case '=': case '.': case '!':
				case '[': case ']': case '(': case ')': case '{': case '}': case '>': case '|': case '\\':
                    if (closings.Count == 0 || closings[0].md[0] != '`')
					    sb.Insert(i++, '\\');
					break;
			}
		}
		return sb.ToString();
	}

	static readonly Dictionary<MessageEntityType, string> EntityToMD = new()
	{
		[MessageEntityType.Bold] = "*",
		[MessageEntityType.Italic] = "_",
		[MessageEntityType.Code] = "`",
		[MessageEntityType.Pre] = "```",
		[MessageEntityType.TextLink] = "[",
		[MessageEntityType.TextMention] = "[",
		[MessageEntityType.Underline] = "__",
		[MessageEntityType.Strikethrough] = "~",
		[MessageEntityType.Spoiler] = "||",
		[MessageEntityType.CustomEmoji] = "![",
		[MessageEntityType.Blockquote] = ">",
	};

    /// <summary>Insert backslashes in front of MarkdownV2 reserved characters</summary>
    /// <param name="text">The text to escape</param>
    /// <returns>The escaped text (may return null if input is null)</returns>
    [return: NotNullIfNotNull(nameof(text))]
	public static string? Escape(string? text)
	{
		if (text == null) return null;
		StringBuilder? sb = null;
		for (int index = 0, added = 0; index < text.Length; index++)
		{
			switch (text[index])
			{
				case '_': case '*': case '~': case '`': case '#': case '+': case '-': case '=': case '.': case '!':
				case '[': case ']': case '(': case ')': case '{': case '}': case '>': case '|': case '\\':
					sb ??= new StringBuilder(text, text.Length + 32);
					sb.Insert(index + added++, '\\');
					break;
			}
		}
		return sb?.ToString() ?? text;
	}
}

/// <summary>Helpers/Extensions for HTML texts</summary>
public static class HtmlText
{
    /// <summary>Generate HTML text from the Message Text or Caption</summary>
    /// <param name="msg">The message</param>
    public static string? ToHtml(this Message msg)
    {
        if (msg.Text != null) return ToHtml(msg.Text, msg.Entities);
        if (msg.Caption != null) return ToHtml(msg.Caption, msg.CaptionEntities);
        return null;
    }

    /// <summary>Converts the (plain text + entities) format used by Telegram messages into an <a href="https://core.telegram.org/bots/api/#html-style">HTML-formatted text</a></summary>
    /// <param name="message">The plain text, typically obtained from <see cref="Message.Text"/></param>
    /// <param name="entities">The array of formatting entities, typically obtained from <see cref="Message.Entities"/></param>
    /// <returns>The message text with HTML formatting tags</returns>
    public static string ToHtml(string message, MessageEntity[]? entities)
	{
		if (entities == null || entities.Length == 0) return Escape(message);
		var closings = new List<(int offset, string tag)>();
		var sb = new StringBuilder(message);
		int entityIndex = 0;
		var nextEntity = entities[entityIndex];
		for (int offset = 0, i = 0; ; offset++, i++)
		{
			while (closings.Count != 0 && offset == closings[0].offset)
			{
				var tag = closings[0].tag;
				sb.Insert(i, tag); i += tag.Length;
				closings.RemoveAt(0);
			}
			if (i == sb.Length) break;
			for (; offset == nextEntity?.Offset; nextEntity = ++entityIndex < entities.Length ? entities[entityIndex] : null)
			{
				if (EntityToTag.TryGetValue(nextEntity.Type, out var tag))
				{
					var closing = (nextEntity.Offset + nextEntity.Length, $"</{tag}>");
					if (tag[0] == 'a')
					{
						if (nextEntity.Type is MessageEntityType.TextLink)
							tag = $"<a href=\"{nextEntity.Url}\">";
						else if (nextEntity.Type is MessageEntityType.TextMention)
							tag = $"<a href=\"tg://user?id={nextEntity.User?.Id}\">";
					}
					else if (nextEntity.Type is MessageEntityType.CustomEmoji)
						tag = $"<tg-emoji emoji-id=\"{nextEntity.CustomEmojiId}\">";
					else if (nextEntity.Type is MessageEntityType.Pre && !string.IsNullOrEmpty(nextEntity.Language))
					{
						closing.Item2 = "</code></pre>";
						tag = $"<pre><code class=\"language-{nextEntity.Language}\">";
					}
					else
						tag = $"<{tag}>";
					int index = ~closings.BinarySearch(closing, Comparer<(int, string)>.Create((x, y) => x.Item1.CompareTo(y.Item1) | 1));
					closings.Insert(index, closing);
					sb.Insert(i, tag); i += tag.Length;
				}
			}
			switch (sb[i])
			{
				case '&': sb.Insert(i + 1, "amp;"); i += 4; break;
				case '<': sb.Insert(i, "&lt"); sb[i += 3] = ';'; break;
				case '>': sb.Insert(i, "&gt"); sb[i += 3] = ';'; break;
			}
		}
		return sb.ToString();
	}

	static readonly Dictionary<MessageEntityType, string> EntityToTag = new()
	{
		[MessageEntityType.Bold] = "b",
		[MessageEntityType.Italic] = "i",
		[MessageEntityType.Code] = "code",
		[MessageEntityType.Pre] = "pre",
		[MessageEntityType.TextLink] = "a",
		[MessageEntityType.TextMention] = "a",
		[MessageEntityType.Underline] = "u",
		[MessageEntityType.Strikethrough] = "s",
		[MessageEntityType.Spoiler] = "tg-spoiler",
		[MessageEntityType.CustomEmoji] = "tg-emoji",
		[MessageEntityType.Blockquote] = "blockquote",
	};

    /// <summary>Replace special HTML characters with their &amp;xx; equivalent</summary>
    /// <param name="text">The text to make HTML-safe</param>
    /// <returns>The HTML-safe text (may return null if input is null)</returns>
    [return: NotNullIfNotNull(nameof(text))]
    public static string? Escape(string? text)
		=> text?.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
}
