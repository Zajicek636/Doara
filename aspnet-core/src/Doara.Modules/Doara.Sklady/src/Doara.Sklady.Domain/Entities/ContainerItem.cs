using System;
using Doara.Sklady.Constants;
using Doara.Sklady.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Doara.Sklady.Entities;

public class ContainerItem : AuditedEntity<Guid>, ISoftDelete, IMultiTenant
{
    public virtual bool IsDeleted { get; private set; }
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
    public virtual Guid? TenantId { get; private set; }
    public virtual Container Container { get; private set; }

    // ReSharper disable once VirtualMemberCallInConstructor
    public ContainerItem(Guid id, string name, string description, decimal realPrice, 
        decimal markup, decimal markupRate, decimal discount, decimal discountRate, string? purchaseUrl,
            Guid containerId, decimal quantity, QuantityType quantityType) : base(id)
    {
        SetName(name).SetDescription(description).SetPrice(realPrice, markup, markupRate, discount, discountRate)
            .SetPurchaseUrl(purchaseUrl).SetState(ContainerItemState.New).SetQuantity(quantity)
            .SetQuantityType(quantityType).SetContainer(containerId);
    }

    public ContainerItem CalculateAndSetPresentationPrice()
    {
        var percentage = MarkupRate - DiscountRate;
        if (percentage != 0)
        {
            PresentationPrice = RealPrice * (100 + percentage) / 100;
        }
        PresentationPrice += Markup - Discount;
        return this;
    }

    public ContainerItem SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(Name), ContainerItemConstants.MaxNameLength);
        return this;
    }
    
    public ContainerItem SetDescription(string description)
    {
        Description = Check.NotNullOrWhiteSpace(description, nameof(Description), ContainerItemConstants.MaxDescriptionLength);
        return this;
    }

    public ContainerItem SetPurchaseUrl(string? purchaseUrl)
    {
        PurchaseUrl = Check.Length(purchaseUrl, nameof(PurchaseUrl), ContainerItemConstants.MaxPurchaseUrlLength);
        return this;
    }

    public ContainerItem SetState(ContainerItemState state)
    {
        State = state;
        return this;
    }
    
    public ContainerItem SetQuantityType(QuantityType quantityType)
    {
        QuantityType = quantityType;
        return this;
    }
    
    public ContainerItem SetQuantity(decimal quantity)
    {
        Quantity = Check.Range(quantity, nameof(Quantity), 0);
        return this;
    }
    
    public ContainerItem SetPrice(decimal realPrice, decimal markup, decimal markupRate, decimal discount, decimal discountRate)
    {
        RealPrice = Check.Range(realPrice, nameof(RealPrice), ContainerItemConstants.MinRealPrice);
        Markup = Check.Range(markup, nameof(Markup), ContainerItemConstants.MinMarkup);
        MarkupRate = Check.Range(markupRate, nameof(MarkupRate), ContainerItemConstants.MinMarkupRate);
        Discount = Check.Range(discount, nameof(Discount), ContainerItemConstants.MinDiscount);
        DiscountRate = Check.Range(discountRate, nameof(DiscountRate), ContainerItemConstants.MinDiscountRate);
        return CalculateAndSetPresentationPrice();
    }
    
    public ContainerItem SetRealPrice(decimal realPrice)
    {
        return SetPrice(realPrice, Markup, MarkupRate, Discount, DiscountRate);
    }
    
    public ContainerItem SetMarkup(decimal markup)
    {
        return SetPrice(RealPrice, markup, MarkupRate, Discount, DiscountRate);
    }
    
    public ContainerItem SetMarkupRate(decimal markupRate)
    {
        return SetPrice(RealPrice, Markup, markupRate, Discount, DiscountRate);
    }
    
    public ContainerItem SetDiscount(decimal discount)
    {
        return SetPrice(RealPrice, Markup, MarkupRate, discount, DiscountRate);
    }
    
    public ContainerItem SetDiscountRate(decimal discountRate)
    {
        return SetPrice(RealPrice, Markup, MarkupRate, Discount, discountRate);
    }
    
    public ContainerItem SetContainer(Guid container)
    {
        ContainerId = container;
        return this;
    }
    
    public ContainerItem SetContainer(Container container)
    {
        Container = container;
        return SetContainer(container.Id);
    }
}