using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace DevicesApi.Api.UnitTests.Helpers;

public class InMemoryDbApplication : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(collection =>
        {
            collection.RemoveDbContext().AddInMemorySql();
        });

        return base.CreateHost(builder);
    }
}