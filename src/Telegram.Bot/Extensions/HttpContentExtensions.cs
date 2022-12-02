using System.Net.Http;
using System.Runtime.CompilerServices;

namespace Telegram.Bot.Extensions;

internal static class HttpContentExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static MultipartFormDataContent AddContentIfInputFile(
        this MultipartFormDataContent multipartContent,
        IInputFile? media,
        string name)
    {
        if (media is not InputFile inputFile) // || inputFile is not { })
            return multipartContent;

        string fileName = inputFile.FileName ?? name;
        string contentDisposition = $@"form-data; name=""{name}""; filename=""{fileName}""".EncodeUtf8();

        // It will be dispose of after the request is made
#pragma warning disable CA2000
        var mediaPartContent = new StreamContent(inputFile.Content)
        {
            Headers =
            {
                {"Content-Type", "application/octet-stream"},
                {"Content-Disposition", contentDisposition}
            }
        };
#pragma warning restore CA2000

        multipartContent.Add(mediaPartContent, name, fileName);

        return multipartContent;
    }
}
