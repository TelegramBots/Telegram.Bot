using System.Net.Http;
using System.Runtime.CompilerServices;

namespace Telegram.Bot.Extensions;

internal static class HttpContentExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static MultipartFormDataContent AddContentIfInputFile(
        this MultipartFormDataContent multipartContent,
        InputFile? media,
        string name)
    {
        if (media is not InputFileStream inputFile) // || inputFile is not { })
        {
            return multipartContent;
        }

        string fileName = inputFile.FileName ?? name;
        string contentDisposition = $@"form-data; name=""{name}""; filename=""{fileName}""".EncodeUtf8();

        // Bug #1336 Calling MakeRequestAsync with Stream content more then once results in error
        if (inputFile.Content.CanSeek) inputFile.Content.Position = inputFile.StreamStartPosition;

        // It will be dispose of after the request is made
        var mediaPartContent = new StreamContent(inputFile.Content)
        {
            Headers =
            {
                {"Content-Type", "application/octet-stream"},
                {"Content-Disposition", contentDisposition},
            },
        };
        multipartContent.Add(mediaPartContent, name, fileName);

        return multipartContent;
    }
}
