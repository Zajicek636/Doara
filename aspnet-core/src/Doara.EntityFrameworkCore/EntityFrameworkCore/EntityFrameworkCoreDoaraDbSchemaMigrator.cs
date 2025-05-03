using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Doara.Data;
using Doara.Sklady.EntityFrameworkCore.Base;
using Doara.Ucetnictvi.EntityFrameworkCore.Base;
using Volo.Abp.DependencyInjection;

namespace Doara.EntityFrameworkCore;

public class EntityFrameworkCoreDoaraDbSchemaMigrator
    : IDoaraDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreDoaraDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the DoaraDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<DoaraDbContext>()
            .Database
            .MigrateAsync();
        
        await _serviceProvider
            .GetRequiredService<SkladyDbContext>()
            .Database
            .MigrateAsync();
        
        await _serviceProvider
            .GetRequiredService<UcetnictviDbContext>()
            .Database
            .MigrateAsync();
    }
}
