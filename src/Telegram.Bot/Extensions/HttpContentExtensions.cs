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
    internal static void AddContentIfInputFileStream(
        this MultipartFormDataContent multipartContent,
        params IInputMedia[] inputMedia)
    {
        foreach (var input in inputMedia)
        {
            if (input.Media.FileType == FileType.Stream)
            {
                multipartContent.AddStreamContent(
                    content: input.Media.Content!,
                    name: input.Media.FileName!
                );
            }

            if (input is IInputMediaThumb mediaThumb &&
                mediaThumb.Thumb?.FileType == FileType.Stream)
            {
                multipartContent.AddStreamContent(
                    content: mediaThumb.Thumb.Content!,
                    name: mediaThumb.Thumb.FileName!
                );
            }
        }
    }
}
