using System.Runtime.CompilerServices;
using System.Text;

namespace Telegram.Bot.Extensions;

internal static class HttpContentExtensions
{
    readonly static Encoding Latin1 = Encoding.GetEncoding(28591);

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
        string contentDisposition = $@"form-data; name=""{name}""; filename=""{fileName}""";
        contentDisposition = Latin1.GetString(Encoding.UTF8.GetBytes(contentDisposition));

        // It will be dispose of after the request is made
#pragma warning disable CA2000
        var mediaPartContent = new StreamContent(inputFile.Content)
        {
            Headers =
            {
                {"Content-Type", "application/octet-stream"},
                {"Content-Disposition", contentDisposition},
            },
        };
#pragma warning restore CA2000

        multipartContent.Add(mediaPartContent, name, fileName);

        return multipartContent;
    }
}
