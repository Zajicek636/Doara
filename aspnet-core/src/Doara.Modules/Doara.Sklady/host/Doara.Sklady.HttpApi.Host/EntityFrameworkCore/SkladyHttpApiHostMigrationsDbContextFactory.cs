using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Doara.Sklady.EntityFrameworkCore;

public class SkladyHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<SkladyHttpApiHostMigrationsDbContext>
{
    public SkladyHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<SkladyHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Sklady"));

        return new SkladyHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
