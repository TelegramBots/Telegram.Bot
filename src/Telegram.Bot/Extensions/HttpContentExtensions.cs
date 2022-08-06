using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions;

internal static class HttpContentExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AddStreamContent(
        this MultipartFormDataContent multipartContent,
        Stream content,
        string name,
        string? fileName = default)
    {
        fileName ??= name;
        var contentDisposition = $@"form-data; name=""{name}""; filename=""{fileName}""".EncodeUtf8();

        // It will be dispose of after the request is made
#pragma warning disable CA2000
        var mediaPartContent = new StreamContent(content)
        {
            Headers =
            {
                {"Content-Type", "application/octet-stream"},
                {"Content-Disposition", contentDisposition}
            }
        };
#pragma warning restore CA2000

        multipartContent.Add(mediaPartContent, name, fileName);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AddContentIfInputFile(
        this MultipartFormDataContent multipartContent,
        IEnumerable<InputMedia> inputMedia)
    {
        foreach (var input in inputMedia)
        {
            multipartContent.AddContentIfInputFile(input);
        }
    }

    internal static void AddContentIfInputFile(this MultipartFormDataContent multipartContent, InputMedia inputMedia)
    {
        if (inputMedia.Media is InputFile media)
        {
            multipartContent.AddStreamContent(
                            content: media.Content,
                            name: media.FileName
                        );
        }
        else
        {
            if (inputMedia is IInputMediaThumb mediaThumb
                            && mediaThumb.Thumb is InputFile thumb)
            {
                multipartContent.AddStreamContent(
                    content: thumb.Content,
                    name: thumb.FileName
                );
            }
        }
    }
}
