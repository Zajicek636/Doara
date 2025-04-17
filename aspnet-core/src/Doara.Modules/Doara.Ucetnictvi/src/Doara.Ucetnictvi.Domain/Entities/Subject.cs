using System;
using Doara.Ucetnictvi.Constants;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Doara.Ucetnictvi.Entities;

public class Subject : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
{
    public virtual bool IsDeleted { get; }
    public virtual Guid? TenantId { get; }
    public virtual string Name { get; private set; }
    public virtual Guid AddressId { get; private set; }
    public virtual Address Address { get; }
    public virtual string Ic { get; private set; }
    public virtual string Dic { get; private set; }
    
    public virtual bool IsVatPayer { get; private set; }

    public Subject(Guid id, string name, Guid addressId, string ic, string dic, bool isVatPayer) : base(id)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(Name), SubjectConstants.MaxNameLength);
        AddressId = Check.NotNull(addressId, nameof(AddressId));
        Ic = Check.NotNullOrWhiteSpace(ic, nameof(Ic), SubjectConstants.MaxIcLength);
        Dic = Check.NotNullOrWhiteSpace(dic, nameof(Dic), SubjectConstants.MaxDicLength);
        IsVatPayer = isVatPayer;
    }
}