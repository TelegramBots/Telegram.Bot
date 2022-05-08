namespace Telegram.Bot.Generators.Extensions;

internal static class NameExtensions
{
    public static string ToPascalCase(this string snakeCaseName)
    {
        ReadOnlySpan<char> snakeCaseNameSpan = snakeCaseName.AsSpan();

        var amountOfUnderscores = 0;
        foreach (char c in snakeCaseNameSpan)
            if (c == '_')
                amountOfUnderscores++;

        Span<char> result = stackalloc char[snakeCaseNameSpan.Length - amountOfUnderscores];
        var isUpper = true;
        var index = 0;

        foreach (char c in snakeCaseNameSpan)
        {
            if (c == '_')
            {
                isUpper = true;
                continue;
            }

            if (isUpper)
            {
                isUpper = false;
                result[index] = char.ToUpperInvariant(c);
            }
            else
            {
                result[index] = c;
            }

            index++;
        }

        return result.ToString();
    }

    public static string ToSnakeCase(this string input)
    {
        var countOfWordsInPropertyName = 0;
        ReadOnlySpan<char> nameSpan = input.AsSpan();

        foreach (char nameChar in nameSpan)
            if (char.IsUpper(nameChar))
                countOfWordsInPropertyName++;

        if (countOfWordsInPropertyName == 1)
        {
            Span<char> destination = stackalloc char[nameSpan.Length];
            input.AsSpan().CopyTo(destination);
            destination[0] = char.ToLowerInvariant(destination[0]);
            return destination.ToString();
        }
        else
        {
            Span<char> destination = stackalloc char[nameSpan.Length + countOfWordsInPropertyName - 1];
            ConvertToSeparatedCase(destination, input.AsSpan(), '_');
            return destination.ToString();
        }
    }

    private static void ConvertToSeparatedCase(Span<char> destination, ReadOnlySpan<char> nameSpan, char separator)
    {
        var position = 0;

        foreach (char nameChar in nameSpan)
        {
            if (char.IsUpper(nameChar))
            {
                if (position > 0)
                    destination[position++] = separator;
                destination[position++] = char.ToLowerInvariant(nameChar);
            }
            else
            {
                destination[position++] = nameChar;
            }
        }
    }
}
