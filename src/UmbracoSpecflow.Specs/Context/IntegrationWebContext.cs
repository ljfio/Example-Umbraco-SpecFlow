using System;
using System.Net.Http;
using System.Threading.Tasks;
using UmbracoSpecflow.Specs.Core;

namespace UmbracoSpecflow.Specs.Context;

public class IntegrationWebContext : IDisposable, IAsyncDisposable
{
    private readonly TestWebApplicationFactory _applicationFactory = new();

    public HttpClient Client => _applicationFactory.CreateClient();

    public IServiceProvider Services => _applicationFactory.Services;

    public void Dispose() => _applicationFactory.Dispose();

    public async ValueTask DisposeAsync() => await _applicationFactory.DisposeAsync();
}