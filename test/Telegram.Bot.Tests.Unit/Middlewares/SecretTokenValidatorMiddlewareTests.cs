using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot.AspNet.Exceptions;
using Telegram.Bot.AspNet.Middlewares.SecretTokenValidator;
using Telegram.Bot.AspNet.Options;
using Xunit;

namespace Telegram.Bot.Tests.Unit.Middlewares;

public class SecretTokenValidatorMiddlewareTests
{
    private const string SecretTokenHeader = "X-Telegram-Bot-Api-Secret-Token";
    private const string SecretToken = "my-secret-token";

    [Fact]
    public async Task SendAsync_WithValidSecretToken_ShouldReturn404()
    {
        IHost host = await CreateHost();
        TestServer server = host.GetTestServer();
        HttpContext context = await server.SendAsync(c => c.Request.Headers.Add(SecretTokenHeader, SecretToken));
        Assert.Equal(context.Response.StatusCode, (int)HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task SendAsync_WithValidToken_ToProtectedPath_ShouldReturn404()
    {
        const string protectedPath = "/telegram";
        IHost host = await CreateHostWithProtectedPath(protectedPath);
        TestServer server = host.GetTestServer();
        HttpContext context = await server.SendAsync(c =>
        {
            c.Request.Path = protectedPath;
            c.Request.Headers.Add(SecretTokenHeader, SecretToken);
        });
        Assert.Equal(context.Response.StatusCode, (int)HttpStatusCode.NotFound);
    }


    [Fact]
    public async Task SendAsync_WithNoToken_ToUnprotectedPath_ShouldReturn404()
    {
        const string protectedPath = "/telegram";
        const string unprotectedPath = "/path";
        IHost host = await CreateHostWithProtectedPath(protectedPath);
        TestServer server = host.GetTestServer();
        HttpContext context = await server.SendAsync(c => { c.Request.Path = unprotectedPath; });
        Assert.Equal(context.Response.StatusCode, (int)HttpStatusCode.NotFound);
    }


    [Fact]
    public async Task SendAsync_WithInvalidSecretToken_ShouldThrow()
    {
        IHost host = await CreateHost();
        TestServer server = host.GetTestServer();

        await Assert.ThrowsAsync<InvalidSecretTokenException>(async () => await server.SendAsync(c =>
        {
            c.Request.Headers.Add(SecretTokenHeader, "invalid-secret-token");
        }));
    }

    [Fact]
    public async Task SendAsync_WithEmptySecretToken_ShouldThrow()
    {
        IHost host = await CreateHost();
        TestServer server = host.GetTestServer();

        await Assert.ThrowsAsync<InvalidSecretTokenException>(async () => await server.SendAsync(c => { }));
    }

    private static async Task<IHost> CreateHost()
    {
        IHost? host = await new HostBuilder()
                           .ConfigureWebHost(builder =>
                            {
                                builder.UseTestServer()
                                       .ConfigureServices(services =>
                                        {
                                            services.Configure<TelegramBotClientWebhookOptions>(options =>
                                                options.SecretToken = SecretToken);
                                            services.AddTransient<SecretTokenValidatorMiddleware>();
                                        })
                                       .Configure(app => app.UseSecretTokenValidator());
                            }).StartAsync();
        return host;
    }

    private static async Task<IHost> CreateHostWithProtectedPath(string pathToProtect)
    {
        IHost? host = await new HostBuilder()
                           .ConfigureWebHost(builder =>
                            {
                                builder.UseTestServer()
                                       .ConfigureServices(services =>
                                        {
                                            services.Configure<TelegramBotClientWebhookOptions>(options =>
                                                options.SecretToken = SecretToken);
                                            services.AddTransient<SecretTokenValidatorMiddleware>();
                                        })
                                       .Configure(app =>
                                            app.UseWhen(
                                                c => c.Request.Path.StartsWithSegments(pathToProtect,
                                                    StringComparison.InvariantCultureIgnoreCase),
                                                applicationBuilder => applicationBuilder.UseSecretTokenValidator()));
                            }).StartAsync();
        return host;
    }
}
