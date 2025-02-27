using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Sklady.EntityFrameworkCore;

[ConnectionStringName(SkladyDbProperties.ConnectionStringName)]
public class SkladyDbContext : AbpDbContext<SkladyDbContext>, ISkladyDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public SkladyDbContext(DbContextOptions<SkladyDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureSklady();
    }
}
