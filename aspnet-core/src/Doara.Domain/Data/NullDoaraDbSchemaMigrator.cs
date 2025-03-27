using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Doara.Data;

/* This is used if database provider does't define
 * IDoaraDbSchemaMigrator implementation.
 */
public class NullDoaraDbSchemaMigrator : IDoaraDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
