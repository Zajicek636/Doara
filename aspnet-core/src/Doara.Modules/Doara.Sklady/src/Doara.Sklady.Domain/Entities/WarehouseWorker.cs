using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Doara.Sklady.Entities;

// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class WarehouseWorker : AuditedEntity<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; private set; }
    public virtual Guid UserId { get; private set; } //IdentityUser
    public virtual Guid ContainerId { get; private set; }
    
    public WarehouseWorker(Guid id) : base(id)
    {
       
    }
    
#pragma warning disable CS8618, CS9264
    protected WarehouseWorker() { }
#pragma warning restore CS8618, CS9264
}