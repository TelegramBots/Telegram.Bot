namespace Telegram.Bot.Serialization;

internal sealed class ReplyMarkupConverter : JsonConverter<ReplyMarkup>
{
	// Only handle the abstract base, so derived types use their normal converters
	public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(ReplyMarkup);

	public override ReplyMarkup? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		using var doc = JsonDocument.ParseValue(ref reader);
		var root = doc.RootElement;
		Type target =
			root.TryGetProperty("inline_keyboard", out _) ? typeof(InlineKeyboardMarkup) :
			root.TryGetProperty("keyboard", out _) ? typeof(ReplyKeyboardMarkup) :
			root.TryGetProperty("remove_keyboard", out _) ? typeof(ReplyKeyboardRemove) :
			root.TryGetProperty("force_reply", out _) ? typeof(ForceReplyMarkup) :
			throw new JsonException("Unrecognized reply_markup type.");
		return (ReplyMarkup?)root.Deserialize(target, options);
	}

	public override void Write(Utf8JsonWriter writer, ReplyMarkup value, JsonSerializerOptions options)
		=> JsonSerializer.Serialize(writer, value, value.GetType(), options);
}
