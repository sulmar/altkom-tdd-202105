using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;

namespace WebApi.IntegrationTests
{
    public abstract class BaseWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
          where TStartup : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder().UseStartup<TStartup>();
    }

    
}
