using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Doara.Ucetnictvi.MongoDB;

[ConnectionStringName(UcetnictviDbProperties.ConnectionStringName)]
public interface IUcetnictviMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
