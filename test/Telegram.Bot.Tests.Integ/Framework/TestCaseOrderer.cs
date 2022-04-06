using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Telegram.Bot.Tests.Integ.Framework;

public class TestCaseOrderer : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
        where TTestCase : ITestCase
    {
        return testCases
            .Select(tc =>
            {
                var attribute = tc.TestMethod.Method
                    .ToRuntimeMethod()
                    .GetCustomAttribute<OrderedFactAttribute>();

                return new
                {
                    TestCase = tc,
                    Attribute = attribute ?? throw new(
                        $@"Test case ""{tc.DisplayName}"" doesn't have {nameof(OrderedFactAttribute)}."
                    )
                };
            })
            .OrderBy(x => x.Attribute!.LineNumber)
            .Select(x => x.TestCase);
    }
}