using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Telegram.Bot.Tests.Integ.Common
{
    public class TestCaseOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
            where TTestCase : ITestCase
        {
            Type attributeType = typeof(ExecutionOrderAttribute);

            testCases = testCases.OrderBy(tcase =>
                tcase.TestMethod.Method.GetCustomAttributes(attributeType).Single()
                    .GetNamedArgument<double>(nameof(ExecutionOrderAttribute.ExecutionOrder)));

            foreach (var testCase in testCases)
            {
                yield return testCase;
            }
        }
    }
}
