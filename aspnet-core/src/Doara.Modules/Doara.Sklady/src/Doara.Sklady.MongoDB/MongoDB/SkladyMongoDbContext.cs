using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Doara.Sklady.MongoDB;

[ConnectionStringName(SkladyDbProperties.ConnectionStringName)]
public class SkladyMongoDbContext : AbpMongoDbContext, ISkladyMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureSklady();
    }
}
