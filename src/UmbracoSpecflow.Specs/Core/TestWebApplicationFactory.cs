using System.IO;
using System.Threading.Tasks;
using J2N.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using UmbracoSpecflow.Web;

namespace UmbracoSpecflow.Specs.Core;

public class TestWebApplicationFactory : WebApplicationFactory<Startup>
{
    private string _connectionString;
    private readonly SqliteConnection _sharedConnection;
    
    public TestWebApplicationFactory()
    {
        _sharedConnection = new SqliteConnection(ConnectionString);
        _sharedConnection.Open();
    }
    
    protected string ConnectionString
    {
        get
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                var builder = new SqliteConnectionStringBuilder
                {
                    DataSource = "InMemory",
                    Cache = SqliteCacheMode.Shared,
                    Mode = SqliteOpenMode.Memory,
                    Pooling = true,
                };
    
                _connectionString = builder.ConnectionString;
            }
    
            return _connectionString;
        }
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var path = Path.Combine(currentDirectory, "appsettings.Integration.json");

        builder.ConfigureAppConfiguration(config =>
        {
            config.AddJsonFile(path);
            
            var values = new Dictionary<string, string>()
            {
                ["ConnectionStrings:umbracoDbDSN"] = ConnectionString,
                ["ConnectionStrings:umbracoDbDSN_ProviderName"] = "Microsoft.Data.Sqlite",
            };
            
            config.AddInMemoryCollection(values);
        });

        builder.UseEnvironment("Development");
    }
    
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _sharedConnection.Dispose();
    }
    
    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await _sharedConnection.DisposeAsync();
    }
}