using System;
using Doara.Sklady.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Doara.Sklady.Entities;

public class ContainerItem : AuditedEntity<Guid>, ISoftDelete, IMultiTenant
{
    public virtual bool IsDeleted { get; private set;}
    public virtual ContainerItemState State { get; private set; }
    public virtual string Name { get; private set; }
    public virtual string Description { get; private set; }
    public virtual string? PurchaseUrl { get; private set; }
    public virtual decimal RealPrice { get; private set; }
    public virtual decimal PresentationPrice { get; private set; }
    public virtual decimal Markup { get; private set; } //Marže
    public virtual decimal MarkupRate { get; private set; } //Marže %
    public virtual decimal Discount { get; private set; } //Sleva
    public virtual decimal DiscountRate { get; private set; } //Sleva %
    public virtual Guid ContainerId { get; private set; }
    public virtual Guid? TenantId { get; private set; }

    public ContainerItem(Guid id, string name, string description, decimal realPrice, decimal presentationPrice, 
        decimal markup, decimal markupRate, decimal discount, decimal discountRate, string? purchaseUrl,
            Guid containerId) : base(id)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(Name));
        Description = Check.NotNullOrWhiteSpace(description, nameof(Description));
        RealPrice = realPrice;
        PresentationPrice = presentationPrice;
        Markup = Check.Range(markup, nameof(Markup), 0);
        MarkupRate = Check.Range(markupRate, nameof(MarkupRate), 0);
        Discount = Check.Range(discount, nameof(Discount), 0);
        DiscountRate = Check.Range(discountRate, nameof(DiscountRate), 0);
        PurchaseUrl = purchaseUrl;
        ContainerId = containerId;
    }

    public virtual ContainerItem SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        return this;
    }
    
#pragma warning disable CS8618, CS9264
    protected ContainerItem()
    {
    }
#pragma warning restore CS8618, CS9264
}