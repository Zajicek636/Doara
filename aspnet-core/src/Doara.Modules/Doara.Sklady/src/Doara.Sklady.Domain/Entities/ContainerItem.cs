using System;
using Doara.Sklady.Constants;
using Doara.Sklady.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Doara.Sklady.Entities;

public class ContainerItem : AuditedEntity<Guid>, ISoftDelete, IMultiTenant
{
    public virtual bool IsDeleted { get; }
    public virtual ContainerItemState State { get; private set; }
    public virtual QuantityType QuantityType { get; private set; }
    public virtual string Name { get; private set; }
    public virtual string Description { get; private set; }
    public virtual string? PurchaseUrl { get; private set; }
    public virtual decimal Quantity { get; private set; }
    public virtual decimal RealPrice { get; private set; }
    public virtual decimal PresentationPrice { get; private set; }
    public virtual decimal Markup { get; private set; } //Marže
    public virtual decimal MarkupRate { get; private set; } //Marže %
    public virtual decimal Discount { get; private set; } //Sleva
    public virtual decimal DiscountRate { get; private set; } //Sleva %
    public virtual Guid ContainerId { get; private set; }
    public virtual Guid? TenantId { get; }
    public virtual Container Container { get; }

    // ReSharper disable once VirtualMemberCallInConstructor
    public ContainerItem(Guid id, string name, string description, decimal realPrice, 
        decimal markup, decimal markupRate, decimal discount, decimal discountRate, string? purchaseUrl,
            Guid containerId, decimal quantity, QuantityType quantityType) : base(id)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(Name), ContainerItemConstants.MaxNameLength);
        Description = Check.NotNullOrWhiteSpace(description, nameof(Description), ContainerItemConstants.MaxDescriptionLength);
        SetPrice(realPrice, markup, markupRate, discount, discountRate);
        PurchaseUrl = Check.Length(purchaseUrl, nameof(PurchaseUrl), ContainerItemConstants.MaxPurchaseUrlLength);
        ContainerId = containerId;
        State = ContainerItemState.New;
        Quantity = Check.Range(quantity, nameof(Quantity), ContainerItemConstants.MinQuantity);
        QuantityType = quantityType;
    }

    public virtual ContainerItem CalculateAndSetPresentationPrice()
    {
        var percentage = MarkupRate - DiscountRate;
        if (percentage != 0)
        {
            PresentationPrice = RealPrice * (100 + percentage) / 100;
        }
        PresentationPrice += Markup - Discount;
        return this;
    }

    public virtual ContainerItem SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(Name), ContainerItemConstants.MaxNameLength);
        return this;
    }
    
    public virtual ContainerItem SetDescription(string description)
    {
        Description = Check.NotNullOrWhiteSpace(description, nameof(Description), ContainerItemConstants.MaxDescriptionLength);
        return this;
    }

    public virtual ContainerItem SetPurchaseUrl(string? purchaseUrl)
    {
        PurchaseUrl = Check.Length(purchaseUrl, nameof(PurchaseUrl), ContainerItemConstants.MaxPurchaseUrlLength);
        return this;
    }

    public virtual ContainerItem SetState(ContainerItemState state)
    {
        State = state;
        return this;
    }
    
    public virtual ContainerItem SetQuantityType(QuantityType quantityType)
    {
        QuantityType = quantityType;
        return this;
    }
    
    public virtual ContainerItem SetQuantity(decimal quantity)
    {
        Quantity = Check.Range(quantity, nameof(Quantity), 0);
        return this;
    }
    
    public virtual ContainerItem SetPrice(decimal realPrice, decimal markup, decimal markupRate, decimal discount, decimal discountRate)
    {
        RealPrice = Check.Range(realPrice, nameof(RealPrice), ContainerItemConstants.MinRealPrice);
        Markup = Check.Range(markup, nameof(Markup), ContainerItemConstants.MinMarkup);
        MarkupRate = Check.Range(markupRate, nameof(MarkupRate), ContainerItemConstants.MinMarkupRate);
        Discount = Check.Range(discount, nameof(Discount), ContainerItemConstants.MinDiscount);
        DiscountRate = Check.Range(discountRate, nameof(DiscountRate), ContainerItemConstants.MinDiscountRate);
        return CalculateAndSetPresentationPrice();
    }
    
    public virtual ContainerItem SetRealPrice(decimal realPrice)
    {
        return SetPrice(realPrice, Markup, MarkupRate, Discount, DiscountRate);
    }
    
    public virtual ContainerItem SetMarkup(decimal markup)
    {
        return SetPrice(RealPrice, markup, MarkupRate, Discount, DiscountRate);
    }
    
    public virtual ContainerItem SetMarkupRate(decimal markupRate)
    {
        return SetPrice(RealPrice, Markup, markupRate, Discount, DiscountRate);
    }
    
    public virtual ContainerItem SetDiscount(decimal discount)
    {
        return SetPrice(RealPrice, Markup, MarkupRate, discount, DiscountRate);
    }
    
    public virtual ContainerItem SetDiscountRate(decimal discountRate)
    {
        return SetPrice(RealPrice, Markup, MarkupRate, Discount, discountRate);
    }
    
#pragma warning disable CS8618, CS9264
    protected ContainerItem()
    {
    }
#pragma warning restore CS8618, CS9264
}