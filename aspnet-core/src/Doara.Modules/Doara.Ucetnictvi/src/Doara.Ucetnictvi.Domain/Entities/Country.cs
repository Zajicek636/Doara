using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public virtual ICollection<Address> Addresses { get; private set; }

    public Country(Guid id, string name, string code) : base(id)
    {
        SetName(name)
            .SetCode(code);
        Addresses = new Collection<Address>();
    }

    public Country SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(Name), CountryConstants.MaxNameLength);
        return this;
    }
    
    public Country SetCode(string code)
    {
        Code = Check.NotNullOrWhiteSpace(code, nameof(Code), CountryConstants.MaxCodeLength);
        return this;
    }
}