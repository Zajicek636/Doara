using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Doara.Ucetnictvi.MongoDB;

[ConnectionStringName(UcetnictviDbProperties.ConnectionStringName)]
public class UcetnictviMongoDbContext : AbpMongoDbContext, IUcetnictviMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureUcetnictvi();
    }
}
