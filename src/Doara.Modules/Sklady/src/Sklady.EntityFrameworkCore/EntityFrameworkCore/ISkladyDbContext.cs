using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Sklady.EntityFrameworkCore;

[ConnectionStringName(SkladyDbProperties.ConnectionStringName)]
public interface ISkladyDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
