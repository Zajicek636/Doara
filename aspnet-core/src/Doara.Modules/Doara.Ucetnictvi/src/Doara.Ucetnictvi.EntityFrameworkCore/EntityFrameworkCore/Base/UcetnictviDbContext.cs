using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Doara.Ucetnictvi.EntityFrameworkCore.Base;

[ConnectionStringName(UcetnictviDbProperties.ConnectionStringName)]
public class UcetnictviDbContext : AbpDbContext<UcetnictviDbContext>, IUcetnictviDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public UcetnictviDbContext(DbContextOptions<UcetnictviDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureUcetnictvi();
    }
}
