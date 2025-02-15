using System.Threading.Tasks;

namespace Doara.Api.Data;

public interface IApiDbSchemaMigrator
{
    Task MigrateAsync();
}
