using System.Text.Json.Serialization.Metadata;
using Telegram.Bot.Types.InlineQueryResults;

namespace Telegram.Bot.Serialization;

public class InlineQueryResultConverter : JsonConverter<InlineQueryResult?>
{
    public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(InlineQueryResult);

    public override InlineQueryResult? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonDocument.TryParseValue(ref reader, out var element))
        {
            throw new JsonException("Could not read JSON value");
        }

        try
        {
            var root = element.RootElement;
            var type = root.GetProperty("type").GetString() ?? throw new JsonException("Type property not found");

            return type switch
            {
                "audio" => GetMaybeCached("audio_file_id", root, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultCachedAudio,
                    TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultAudio),
                "document" => GetMaybeCached("document_file_id", root, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultCachedDocument,
                    TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultDocument),
                "gif" => GetMaybeCached("gif_file_id", root, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultCachedGif,
                    TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultGif),
                "mpeg4_gif" => GetMaybeCached("mpeg4_file_id", root, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultCachedMpeg4Gif,
                    TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultMpeg4Gif),
                "photo" => GetMaybeCached("photo_file_id", root, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultCachedPhoto,
                    TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultPhoto),
                "sticker" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultCachedSticker),
                "video" => GetMaybeCached("video_file_id", root, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultCachedVideo,
                    TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultVideo),
                "voice" =>  GetMaybeCached("voice_file_id", root, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultCachedVoice,
                    TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultVoice),
                "article" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultArticle),
                "contact" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultContact),
                "game" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultGame),
                "location" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultLocation),
                "venue" => JsonSerializer.Deserialize(root.GetRawText(), TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultVenue),
                _ => throw new JsonException($"Unsupported inline query result type: {type}")
            };
        }
        finally
        {
            element.Dispose();
        }
    }

    // ReSharper disable once CognitiveComplexity
    public override void Write(Utf8JsonWriter writer, InlineQueryResult? value, JsonSerializerOptions options)
    {
        if (value is not null)
        {
            writer.WriteRawValue(value switch
            {
                InlineQueryResultCachedAudio cachedAudio => JsonSerializer.Serialize(cachedAudio, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultCachedAudio),
                InlineQueryResultCachedDocument cachedDocument => JsonSerializer.Serialize(cachedDocument, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultCachedDocument),
                InlineQueryResultCachedGif cachedGif => JsonSerializer.Serialize(cachedGif, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultCachedGif),
                InlineQueryResultCachedMpeg4Gif cachedMpeg4Gif => JsonSerializer.Serialize(cachedMpeg4Gif, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultCachedMpeg4Gif),
                InlineQueryResultCachedPhoto cachedPhoto => JsonSerializer.Serialize(cachedPhoto, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultCachedPhoto),
                InlineQueryResultCachedSticker cachedSticker => JsonSerializer.Serialize(cachedSticker, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultCachedSticker),
                InlineQueryResultCachedVideo cachedVideo => JsonSerializer.Serialize(cachedVideo, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultCachedVideo),
                InlineQueryResultCachedVoice cachedVoice => JsonSerializer.Serialize(cachedVoice, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultCachedVoice),
                InlineQueryResultArticle article => JsonSerializer.Serialize(article, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultArticle),
                InlineQueryResultAudio audio => JsonSerializer.Serialize(audio, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultAudio),
                InlineQueryResultContact contact => JsonSerializer.Serialize(contact, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultContact),
                InlineQueryResultDocument document => JsonSerializer.Serialize(document, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultDocument),
                InlineQueryResultGame game => JsonSerializer.Serialize(game, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultGame),
                InlineQueryResultGif gif => JsonSerializer.Serialize(gif, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultGif),
                InlineQueryResultLocation location => JsonSerializer.Serialize(location, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultLocation),
                InlineQueryResultMpeg4Gif mpeg4Gif => JsonSerializer.Serialize(mpeg4Gif, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultMpeg4Gif),
                InlineQueryResultPhoto game => JsonSerializer.Serialize(game, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultPhoto),
                InlineQueryResultVenue venue => JsonSerializer.Serialize(venue, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultVenue),
                InlineQueryResultVideo video => JsonSerializer.Serialize(video, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultVideo),
                InlineQueryResultVoice voice => JsonSerializer.Serialize(voice, TelegramBotClientJsonSerializerContext.Instance.InlineQueryResultVoice),

                _ => throw new JsonException("Unsupported inline query result type")
            });
        }
        else
        {
            writer.WriteNullValue();
        }
    }

    private InlineQueryResult? GetMaybeCached<TCached, TRegular>(string fieldIdField,
                                                                 JsonElement root,
                                                                 JsonTypeInfo<TCached> cached,
                                                                 JsonTypeInfo<TRegular> regular)
        where TCached : InlineQueryResult
        where TRegular : InlineQueryResult
    {
        if (root.TryGetProperty(fieldIdField, out _))
        {
            return JsonSerializer.Deserialize(root.GetRawText(), cached);
        }

        return JsonSerializer.Deserialize(root.GetRawText(), regular);
    }
}
