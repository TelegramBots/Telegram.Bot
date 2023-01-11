using System.Globalization;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Extensions;

namespace Telegram.Bot.Requests;

/// <summary>
/// Represents an API request with a file
/// </summary>
/// <typeparam name="TResponse">Type of result expected in result</typeparam>
[JsonObject(MemberSerialization.OptIn, NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public abstract class FileRequestBase<TResponse> : RequestBase<TResponse>
{
    /// <summary>
    /// Initializes an instance of request
    /// </summary>
    /// <param name="methodName">Bot API method</param>
    protected FileRequestBase(string methodName)
        : base(methodName)
    { }

    /// <summary>
    /// Initializes an instance of request
    /// </summary>
    /// <param name="methodName">Bot API method</param>
    /// <param name="method">HTTP method to use</param>
    protected FileRequestBase(string methodName, HttpMethod method)
        : base(methodName, method)
    { }

    /// <summary>
    /// Generate multipart form data content
    /// </summary>
    /// <param name="fileParameterName"></param>
    /// <param name="inputFile"></param>
    /// <returns></returns>
    protected MultipartFormDataContent ToMultipartFormDataContent(
        string fileParameterName,
        InputFile inputFile)
    {
        if (inputFile is null or { Content: null })
        {
            throw new ArgumentNullException(nameof(inputFile), $"{nameof(inputFile)} or it's content is null");
        }

        return GenerateMultipartFormDataContent(fileParameterName)
            .AddContentIfInputFile(media: inputFile, name: fileParameterName);
    }

    /// <summary>
    /// Generate multipart form data content
    /// </summary>
    /// <param name="exceptPropertyNames"></param>
    /// <returns></returns>
    protected MultipartFormDataContent GenerateMultipartFormDataContent(params string[] exceptPropertyNames)
    {
        var multipartContent = new MultipartFormDataContent($"{Guid.NewGuid()}{DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture)}");

        var stringContents = JObject.FromObject(this)
            .Properties()
            .Where(prop => exceptPropertyNames.Contains(prop.Name, StringComparer.InvariantCulture) is false)
            .Select(prop => new
            {
                prop.Name,
                Content = new StringContent(prop.Value.ToString())
            });

        foreach (var strContent in stringContents)
        {
            multipartContent.Add(content: strContent.Content, name: strContent.Name);
        }

        return multipartContent;
    }
}
