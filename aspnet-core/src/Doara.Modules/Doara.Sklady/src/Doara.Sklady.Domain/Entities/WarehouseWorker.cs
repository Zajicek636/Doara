using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Doara.Sklady.Entities;

public class WarehouseWorker : Entity<Guid>
{
    public virtual string Name { get; private set; }
    public virtual Guid ContainerId { get; private set; }
    

    public WarehouseWorker(Guid id, string name) : base(id)
    {
        Name = name;
    }

    public virtual WarehouseWorker SetTitle(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        return this;
    }
    
#pragma warning disable CS8618, CS9264
    protected WarehouseWorker() { }
#pragma warning restore CS8618, CS9264
}