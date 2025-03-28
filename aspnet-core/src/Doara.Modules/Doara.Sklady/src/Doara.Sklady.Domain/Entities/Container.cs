using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Doara.Sklady.Entities;

public class Container : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
{
    public virtual string Name { get; private set; }
    public virtual ICollection<WarehouseWorker> WarehouseWorkers { get; private set; }
    public virtual ICollection<ContainerItem> Items { get; private set; }
    public virtual bool IsDeleted { get; private set;}
    public virtual Guid? TenantId { get; private set;}

    public Container(Guid id, string name) : base(id)
    {
        Name = name;
        // ReSharper disable once VirtualMemberCallInConstructor
        WarehouseWorkers = new Collection<WarehouseWorker>();
        Items = new Collection<ContainerItem>();
    }

    public virtual Container SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        return this;
    }
    
#pragma warning disable CS8618, CS9264
    protected Container() { }
#pragma warning restore CS8618, CS9264
}