using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Doara.Ucetnictvi.EntityFrameworkCore;

public class UcetnictviHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<UcetnictviHttpApiHostMigrationsDbContext>
{
    public UcetnictviHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<UcetnictviHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Ucetnictvi"));

        return new UcetnictviHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
