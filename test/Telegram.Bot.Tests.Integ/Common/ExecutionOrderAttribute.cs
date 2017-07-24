using System;

namespace Telegram.Bot.Tests.Integ.Common
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ExecutionOrderAttribute : Attribute
    {
        public double ExecutionOrder { get; }

        public ExecutionOrderAttribute(double executionOrder)
        {
            if (executionOrder < 1)
            {
                throw new ArgumentException("Execution order number cannot be less than 1.",
                    nameof(executionOrder));
            }

            ExecutionOrder = executionOrder;
        }
    }
}
