using System;
using Doara.Ucetnictvi.Constants;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Doara.Ucetnictvi.Entities;

public class Country : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
{
    public virtual bool IsDeleted { get; }
    public virtual Guid? TenantId { get; }
    public virtual string Name { get; private set; }
    public virtual string Code { get; private set; }

    public Country(Guid id, string name, string code) : base(id)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(Name), CountryConstants.MaxNameLength);
        Code = Check.NotNullOrWhiteSpace(code, nameof(Code), CountryConstants.MaxCodeLength);
    }
}