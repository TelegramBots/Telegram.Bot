using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

#nullable enable

namespace Telegram.Bot.Tests.Integ.Framework;

public class TestConfiguration : IValidatableObject
{
    private static readonly Regex UsernamePattern = new("[a-zA-Z0-9_]+", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    [Required(ErrorMessage = "API token is not provided or is empty.")]
    [RegularExpression("[0-9]+:.+", ErrorMessage = "API is invalid.")]
    [MinLength(25, ErrorMessage = "API token is too short.")]
    public string ApiToken { get; set; } = default!;

    public string? AllowedUserNamesString { get; set; }

    public string[] AllowedUserNames { get; set; } = Array.Empty<string>();

    [Required(ErrorMessage = "Supergroup ID is not provided or is empty.")]
    public long SuperGroupChatId { get; set; }

    public long? ChannelChatId { get; set; }

    public string? PaymentProviderToken { get; set; }

    public long? TesterPrivateChatId { get; set; }

    public long? StickerOwnerUserId { get; set; }

    public long? RegularGroupMemberId { get; set; }

    public int RetryCount { get; set; } = 3;

    public int DefaultRetryTimeout { get; set; } = 30;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        foreach (var username in AllowedUserNames)
        {
            if (!UsernamePattern.IsMatch(username))
            {
                yield return new($"Username {username} is invalid", new [] { nameof(AllowedUserNames) });
            }
        }

        if (RetryCount < 0)
        {
            yield return new("RetryCount must be greater or equal to 0", new [] { nameof(RetryCount) });
        }

        if (DefaultRetryTimeout < 0)
        {
            yield return new("DefaultRetryTimeout must be greater or equal to 0", new [] { nameof(RetryCount) });
        }

        yield return ValidationResult.Success!;
    }
}