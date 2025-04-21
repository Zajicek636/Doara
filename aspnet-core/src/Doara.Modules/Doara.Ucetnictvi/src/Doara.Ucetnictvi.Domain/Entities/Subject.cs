using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Doara.Ucetnictvi.Constants;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Doara.Ucetnictvi.Entities;

public class Subject : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
{
    public virtual bool IsDeleted { get; private set; }
    public virtual Guid? TenantId { get; private set; }
    public virtual string Name { get; private set; }
    public virtual Guid AddressId { get; private set; }
    public virtual Address Address { get; private set; }
    public virtual string? Ic { get; private set; }
    public virtual string? Dic { get; private set; }
    public virtual bool IsVatPayer { get; private set; }
    public virtual ICollection<Invoice> InvoicesPairAsSupplier { get; private set; }
    public virtual ICollection<Invoice> InvoicesPairAsCustomer { get; private set; }

    public Subject(Guid id, string name, Guid addressId, string? ic, string? dic, bool isVatPayer) : base(id)
    {
        SetName(name)
            .SetAddress(addressId)
            .SetIc(ic)
            .SetDic(dic)
            .SetIsVatPayer(isVatPayer);
        InvoicesPairAsSupplier = new Collection<Invoice>();
        InvoicesPairAsCustomer = new Collection<Invoice>();
    }

    public Subject SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(Name), SubjectConstants.MaxNameLength);
        return this;
    }
    
    public Subject SetAddress(Guid addressId)
    {
        AddressId = Check.NotNull(addressId, nameof(AddressId));
        return this;
    }
    
    public Subject SetAddress(Address address)
    {
        Address = address;
        return SetAddress(address.Id);
    }
    
    public Subject SetIc(string? ic)
    {
        Ic = Check.Length(ic, nameof(Ic), SubjectConstants.MaxIcLength);
        return this;
    }
    
    public Subject SetDic(string? dic)
    {
        Dic = Check.Length(dic, nameof(Dic), SubjectConstants.MaxDicLength);
        return this;
    }
    
    public Subject SetIsVatPayer(bool isVatPayer)
    {
        IsVatPayer = isVatPayer;
        return this;
    }
}