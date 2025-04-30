using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Doara.Sklady.Constants;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Doara.Sklady.Entities;

public class Container : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
{
    public virtual bool IsDeleted { get; private set; }
    public virtual string Name { get; private set; }
    public virtual string Description { get; private set; }
    public virtual Guid? TenantId { get; private set; }
    public virtual ICollection<ContainerItem> Items { get; private set; }

    public Container(Guid id, string name, string description) : base(id)
    {
        SetName(name).SetDescription(description);
        // ReSharper disable once VirtualMemberCallInConstructor
        Items = new Collection<ContainerItem>();
    }

    public Container SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), ContainerConstants.MaxNameLength);
        return this;
    }
    
    public Container SetDescription(string description)
    {
        Description = Check.NotNullOrWhiteSpace(description, nameof(Description), ContainerConstants.MaxDescriptionLength);
        return this;
    }
}