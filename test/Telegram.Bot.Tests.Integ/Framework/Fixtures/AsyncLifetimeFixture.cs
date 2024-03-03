using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

#nullable enable

namespace Telegram.Bot.Tests.Integ.Framework.Fixtures;

public abstract class AsyncLifetimeFixture : IAsyncLifetime
{
    readonly List<IAsyncLifetime> _lifetimes = [];

    protected virtual IEnumerable<IAsyncLifetime> Lifetimes() => [];
    protected virtual IEnumerable<Func<Task>> Initializers() => [];
    protected virtual IEnumerable<Func<Task>> Finalizers() => [];

    protected AsyncLifetimeFixture()
    {
        // ReSharper disable VirtualMemberCallInConstructor
        _lifetimes.AddRange(Initializers().Select(initializer => new AsyncLifetimeAction(initializer: initializer)));
        _lifetimes.AddRange(Lifetimes());
        _lifetimes.AddRange(Finalizers().Select(finalizer => new AsyncLifetimeAction(finalizer: finalizer)));
        // ReSharper restore VirtualMemberCallInConstructor
    }

    protected void AddLifetime(IAsyncLifetime lifetime) =>
        _lifetimes.Add(lifetime);

    protected void AddLifetime(Func<Task>? initializer = default, Func<Task>? finalizer = default) =>
        _lifetimes.Add(new AsyncLifetimeAction(initializer, finalizer));

    protected void AddInitializer(Func<Task> initializer) =>
        _lifetimes.Add(new AsyncLifetimeAction(initializer: initializer));

    protected void AddFinalizer(Func<Task> finalizer) =>
        _lifetimes.Add(new AsyncLifetimeAction(finalizer: finalizer));

    public async Task InitializeAsync()
    {
        foreach (var asyncLifetime in _lifetimes)
        {
            await asyncLifetime.InitializeAsync();
        }
    }

    public async Task DisposeAsync()
    {
        // finalizer in reverse order because later lifetimes might depend on previous lifetimes to be intact
        foreach (var asyncLifetime in ((IEnumerable<IAsyncLifetime>)_lifetimes).Reverse())
        {
            await asyncLifetime.DisposeAsync();
        }
    }

    public sealed class AsyncLifetimeAction(Func<Task>? initializer = default, Func<Task>? finalizer = default)
        : IAsyncLifetime
    {
        public async Task InitializeAsync()
        {
            if (initializer is not null) { await initializer(); }
        }

        public async Task DisposeAsync()
        {
            if (finalizer is not null) { await finalizer(); }
        }
    }
}
