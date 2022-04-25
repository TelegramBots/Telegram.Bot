using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

#nullable enable

namespace Telegram.Bot.Tests.Integ.Framework;

/// <summary>
/// Attribute that is applied to a test method. Test methods in a collection will be executed in order based on
/// their line number. By default, test cases will rerun once if test method throws
/// a <see cref="TaskCanceledException"/>.
/// </summary>
[XunitTestCaseDiscoverer(Constants.TestCaseDiscoverer, Constants.AssemblyName)]
public class OrderedFactAttribute : FactAttribute
{
    readonly int _maxRetries = 1;
    readonly int _delaySeconds = 60;
    readonly Type _exceptionType = typeof(TaskCanceledException);

    /// <summary>
    /// Line number in source file
    /// </summary>
    public int LineNumber { get; }

    /// <summary>
    /// Number of test case retry attempts after test case is ran once. Assign 0 to disable test case retry.
    /// </summary>
    public int MaxRetries
    {
        get => _maxRetries;
        init
        {
            if (value < 0) { throw new ArgumentOutOfRangeException(nameof(MaxRetries)); }
            _maxRetries = value;
        }
    }

    /// <summary>
    /// Number of seconds to pause between each retry attempt
    /// </summary>
    public int DelaySeconds
    {
        get => _delaySeconds;
        init
        {
            if (value < 1) { throw new ArgumentOutOfRangeException(nameof(DelaySeconds)); }
            _delaySeconds = value;
        }
    }

    /// <summary>
    /// Type of exception to handle and retry
    /// </summary>
    public Type ExceptionType
    {
        get => _exceptionType;
        init
        {
            if (!typeof(Exception).IsAssignableFrom(value))
            {
                throw new ArgumentException("Type should be an Exception", nameof(ExceptionType));
            }

            _exceptionType = value;
        }
    }

    internal string? ExceptionTypeFullName => _exceptionType.FullName;

    /// <summary>
    /// Initializes an instance of <see cref="OrderedFactAttribute"/> with 1 retry attempt and delay of
    /// 30 seconds if a <see cref="TaskCanceledException"/> is thrown.
    /// </summary>
    /// <param name="description">Description of test case.</param>
    /// <param name="line">Line number in source file.</param>
    public OrderedFactAttribute(string description, [CallerLineNumber] int line = default)
    {
        if (line < 1) { throw new ArgumentOutOfRangeException(nameof(line)); }
        // ReSharper disable once VirtualMemberCallInConstructor
        if (!string.IsNullOrWhiteSpace(description)) { DisplayName = description; }

        LineNumber = line;
    }
}