using System;

namespace Telegram.Bot.Tests.Integ.Common
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ExecutionOrderAttribute : Attribute
    {
        public double ExecutionOrder { get; }

        public ExecutionOrderAttribute(double executionOrder)
        {
            // todo: validate the number. eg. 1 <= N
            ExecutionOrder = executionOrder;
        }
    }
}
