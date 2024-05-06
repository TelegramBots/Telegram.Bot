using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization.Metadata;
using Telegram.Bot.Extensions;
using Telegram.Bot.Serialization;

namespace Telegram.Bot.Requests;

/// <summary>
/// Represents an API request with a file
/// </summary>
/// <typeparam name="TResponse">Type of result expected in result</typeparam>

public abstract class FileRequestBase<TResponse> : RequestBase<TResponse>
{
    /// <summary>
    /// Initializes an instance of request
    /// </summary>
    /// <param name="methodName">Bot API method</param>
    /// <param name="jsonTypeInfo"></param>
    protected FileRequestBase(string methodName, JsonTypeInfo jsonTypeInfo)
        : base(methodName, jsonTypeInfo)
    { }

    /// <summary>
    /// Initializes an instance of request
    /// </summary>
    /// <param name="methodName">Bot API method</param>
    /// <param name="method">HTTP method to use</param>
    /// <param name="jsonTypeInfo"></param>
    protected FileRequestBase(string methodName, HttpMethod method, JsonTypeInfo jsonTypeInfo)
        : base(methodName, method, jsonTypeInfo)
    { }

    /// <summary>
    /// Generate multipart form data content
    /// </summary>
    /// <param name="fileParameterName"></param>
    /// <param name="context"></param>
    /// <param name="inputFile"></param>
    /// <returns></returns>
    protected MultipartFormDataContent ToMultipartFormDataContent(
        JsonTypeInfo context,
        string fileParameterName,
        InputFileStream inputFile)
    {
        if (inputFile is null or { Content: null })
        {
            throw new ArgumentNullException(nameof(inputFile), $"{nameof(inputFile)} or it's content is null");
        }

        return GenerateMultipartFormDataContent(context, fileParameterName)
            .AddContentIfInputFile(media: inputFile, name: fileParameterName);
    }

    /// <summary>
    /// Generate multipart form data content
    /// </summary>
    /// <param name="context"></param>
    /// <param name="exceptPropertyNames"></param>
    /// <returns></returns>
    protected MultipartFormDataContent GenerateMultipartFormDataContent(
        JsonTypeInfo context,
        params string[] exceptPropertyNames)
    {
        var boundary = $"{Guid.NewGuid()}{DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture)}";
        var multipartContent = new MultipartFormDataContent(boundary);

        var jsonElement = JsonSerializer.SerializeToElement(this, context);

        var stringContents = jsonElement
            .EnumerateObject()
            .Where(prop => exceptPropertyNames.Contains(prop.Name, StringComparer.InvariantCulture) is false)
            .Select(prop => (name: prop.Name, content: new StringContent(prop.Value.ToString())));

        foreach (var (name, content) in stringContents)
        {
            multipartContent.Add(content: content, name: name);
        }

        return multipartContent;
    }
}
