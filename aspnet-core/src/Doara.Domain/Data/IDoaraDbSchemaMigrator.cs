using System.Threading.Tasks;

namespace Doara.Data;

public interface IDoaraDbSchemaMigrator
{
    Task MigrateAsync();
}
