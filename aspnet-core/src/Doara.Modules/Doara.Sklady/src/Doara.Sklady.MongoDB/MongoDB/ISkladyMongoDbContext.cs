using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Doara.Sklady.MongoDB;

[ConnectionStringName(SkladyDbProperties.ConnectionStringName)]
public interface ISkladyMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
