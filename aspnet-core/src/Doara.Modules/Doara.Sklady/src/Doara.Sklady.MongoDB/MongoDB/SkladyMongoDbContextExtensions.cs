using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Doara.Sklady.MongoDB;

public static class SkladyMongoDbContextExtensions
{
    public static void ConfigureSklady(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
