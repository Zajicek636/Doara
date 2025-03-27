using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Doara.Ucetnictvi.MongoDB;

public static class UcetnictviMongoDbContextExtensions
{
    public static void ConfigureUcetnictvi(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
