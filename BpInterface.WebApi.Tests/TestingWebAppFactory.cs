using BpInterface.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BpInterface.WebApi.IntegrationTests
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(async services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<BpContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                services.AddDbContext<BpContext>(options =>
                {
                    options.UseInMemoryDatabase("BpDataBase");
                });
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<BpContext>())
                {
                    try
                    {
                        appContext.Database.EnsureCreated();
                        await BpContextSeed.SeedAsync(appContext);
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            });
        }
    }
}