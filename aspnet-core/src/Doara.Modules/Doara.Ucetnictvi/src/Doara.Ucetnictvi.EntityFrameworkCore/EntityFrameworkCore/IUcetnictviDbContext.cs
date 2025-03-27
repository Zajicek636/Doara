using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Doara.Ucetnictvi.EntityFrameworkCore;

[ConnectionStringName(UcetnictviDbProperties.ConnectionStringName)]
public interface IUcetnictviDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
