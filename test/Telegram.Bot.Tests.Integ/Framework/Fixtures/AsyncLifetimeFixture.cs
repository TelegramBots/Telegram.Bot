using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

#nullable enable

namespace Telegram.Bot.Tests.Integ.Framework.Fixtures;

public abstract class AsyncLifetimeFixture : IAsyncLifetime
{
    readonly List<IAsyncLifetime> _lifetimes = new();

    protected void AddLifetime(IAsyncLifetime lifetime) =>
        _lifetimes.Add(lifetime);

    protected void AddLifetime(Func<Task>? initialize = default, Func<Task>? dispose = default) =>
        _lifetimes.Add(new AsyncLifetimeAction(initialize, dispose));

    protected void AddLifetime(Action? initialize = default, Action? dispose = default) =>
        _lifetimes.Add(new AsyncLifetimeAction(initialize, dispose));

    public async Task InitializeAsync()
    {
        foreach (var asyncLifetime in _lifetimes)
        {
            await asyncLifetime.InitializeAsync();
        }
    }

    public async Task DisposeAsync()
    {
        // dispose in reverse order because later lifetimes might depend on previous lifetimes to be intact
        foreach (var asyncLifetime in ((IEnumerable<IAsyncLifetime>)_lifetimes).Reverse())
        {
            await asyncLifetime.DisposeAsync();
        }
    }

    sealed class AsyncLifetimeAction : IAsyncLifetime
    {
        readonly Func<Task>? _initialize;
        readonly Func<Task>? _dispose;

        public AsyncLifetimeAction(Func<Task>? initialize = default, Func<Task>? dispose = default)
        {
            _initialize = initialize;
            _dispose = dispose;
        }

        public AsyncLifetimeAction(Action? initialize = default, Action? dispose = default)
        {
            if (initialize is not null)
            {
                _initialize = () =>
                {
                    initialize.Invoke();
                    return Task.CompletedTask;
                };
            }

            if (dispose is not null)
            {
                _dispose = () =>
                {
                    dispose.Invoke();
                    return Task.CompletedTask;
                };
            }
        }

        public async Task InitializeAsync()
        {
            if (_initialize is not null) { await _initialize(); }
        }

        public async Task DisposeAsync()
        {
            if (_dispose is not null) { await _dispose(); }
        }
    }
}