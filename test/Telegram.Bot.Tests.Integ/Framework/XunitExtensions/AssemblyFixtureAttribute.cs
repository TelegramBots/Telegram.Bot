using System;

namespace Telegram.Bot.Tests.Integ.Framework.XunitExtensions;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class AssemblyFixtureAttribute(Type fixtureType) : Attribute
{
    public Type FixtureType { get; } = fixtureType;
}