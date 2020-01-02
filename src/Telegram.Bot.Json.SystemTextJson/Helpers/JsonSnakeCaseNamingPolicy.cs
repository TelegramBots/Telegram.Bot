using System;
using System.Text.Json;

namespace Telegram.Bot.Json.Helpers
{
    internal sealed class JsonSnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return string.Empty;

            var countOfCapitalLettersFromFirstIndex = 0;
            var nameSpan = name.AsSpan();
            var isFirstLetterCapital = char.IsLetter(nameSpan[0]) && char.IsUpper(nameSpan[0]);

            foreach (var nameChar in nameSpan.Slice(1))
                if (char.IsLetter(nameChar) && char.IsUpper(nameChar))
                    countOfCapitalLettersFromFirstIndex++;

            if (countOfCapitalLettersFromFirstIndex == 0)
            {
                if (!isFirstLetterCapital)
                    return name;

#if NETCOREAPP3_0 || NETCOREAPP3_1
                return string.Create(nameSpan.Length, name, (destination, source) =>
                {
                    source.AsSpan().CopyTo(destination);
                    destination[0] = char.ToLowerInvariant(destination[0]);
                });
#else
                Span<char> spanWithoutCapitalLetterAtZeroIndex = stackalloc char[nameSpan.Length];
                nameSpan.Slice(1).CopyTo(spanWithoutCapitalLetterAtZeroIndex.Slice(1));
                spanWithoutCapitalLetterAtZeroIndex[0] =
                    char.ToLowerInvariant(spanWithoutCapitalLetterAtZeroIndex[0]);
                return spanWithoutCapitalLetterAtZeroIndex.ToString();
#endif
            }

#if NETCOREAPP3_0 || NETCOREAPP3_1
            return string.Create(nameSpan.Length + countOfCapitalLettersFromFirstIndex, name, ConvertToSnakeCase);
#else
            Span<char> span = stackalloc char[nameSpan.Length + countOfCapitalLettersFromFirstIndex];
            ConvertToSnakeCase(span, name);
            return span.ToString();
#endif
        }

        private static void ConvertToSnakeCase(Span<char> destination, string name)
        {
            var nameSpan = name.AsSpan();
            var position = 0;

            foreach (var nameChar in nameSpan)
            {
                if (char.IsLetter(nameChar) && char.IsUpper(nameChar))
                {
                    if (position > 0)
                        destination[position++] = '_';
                    destination[position++] = char.ToLowerInvariant(nameChar);
                }
                else
                {
                    destination[position++] = nameChar;
                }
            }
        }
    }
}
