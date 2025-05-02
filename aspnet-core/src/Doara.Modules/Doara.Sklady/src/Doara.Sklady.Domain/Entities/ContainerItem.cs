using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Doara.Sklady.Constants;
using Doara.Sklady.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Doara.Sklady.Entities;

public class ContainerItem : AuditedEntity<Guid>, ISoftDelete, IMultiTenant
{
    public virtual bool IsDeleted { get; private set; }
    public virtual QuantityType QuantityType { get; private set; }
    public virtual string Name { get; private set; }
    public virtual string Description { get; private set; }
    public virtual string? PurchaseUrl { get; private set; }
    
    public decimal OnHand => Movements
                                 .Where(m => m.MovementCategory == MovementCategory.Unused)
                                 .Sum(m => m.Quantity)
                             - Movements
                                 .Where(m => m.MovementCategory == MovementCategory.Used)
                                 .Sum(m => m.Quantity);
    public decimal Reserved => Movements
        .Where(m => m.MovementCategory == MovementCategory.Reserved)
        .Sum(m => m.Quantity);

    public decimal Available => OnHand - Reserved;
    
    public virtual decimal RealPrice { get; private set; }
    public virtual decimal PresentationPrice { get; private set; }
    public virtual decimal Markup { get; private set; } //Marže
    public virtual decimal MarkupRate { get; private set; } //Marže %
    public virtual decimal Discount { get; private set; } //Sleva
    public virtual decimal DiscountRate { get; private set; } //Sleva %
    public virtual Guid ContainerId { get; private set; }
    public virtual Guid? TenantId { get; private set; }
    public virtual Container Container { get; private set; }
    public virtual ICollection<StockMovement> Movements { get; private set; }

    // ReSharper disable once VirtualMemberCallInConstructor
    public ContainerItem(Guid id, string name, string description, decimal realPrice, 
        decimal markup, decimal markupRate, decimal discount, decimal discountRate, string? purchaseUrl,
            Guid containerId, QuantityType quantityType) : base(id)
    {
        SetName(name).SetDescription(description).SetPrice(realPrice, markup, markupRate, discount, discountRate)
            .SetPurchaseUrl(purchaseUrl).SetQuantityType(quantityType).SetContainer(containerId);
        Movements = new Collection<StockMovement>();
    }

    public ContainerItem CalculateAndSetPresentationPrice()
    {
        var percentage = MarkupRate - DiscountRate;
        PresentationPrice = RealPrice;
        if (percentage != 0)
        {
            PresentationPrice *= (100 + percentage) / 100;
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
    
    public ContainerItem SetQuantityType(QuantityType quantityType)
    {
        QuantityType = quantityType;
        return this;
    }
    
    public ContainerItem Reserve(decimal amount, Guid plannedId, Guid? relatedDocId = null)
    {
        var available = Available;
        if (amount > available)
        {
            throw new BusinessException(SkladyErrorCodes.LackOfAvailableResources)
                .WithData("Quantity", available);
        }
        Movements.Add(new StockMovement(plannedId, Id, amount, MovementCategory.Reserved, relatedDocId));
        return this;
    }

    public ContainerItem Use(decimal amount, Guid plannedId, Guid? relatedDocId = null)
    {
        var available = Available;
        if (amount > available)
        {
            throw new BusinessException(SkladyErrorCodes.LackOfAvailableResources)
                .WithData("Quantity", available);
        }
        Movements.Add(new StockMovement(plannedId, Id, amount, MovementCategory.Used, relatedDocId));
        return this;
    }
    
    public ContainerItem Use(Guid reservedId, Guid plannedId)
    {
        var reserved = Movements.FirstOrDefault(x => x.Id == reservedId);
        if (reserved == null)
        {
            throw new EntityNotFoundException(typeof(StockMovement), reservedId);
        }

        if (reserved.MovementCategory != MovementCategory.Reserved)
        {
            throw new BusinessException(SkladyErrorCodes.MovementIsNotReservation);
        }
        
        reserved.SetMovementCategory(MovementCategory.Reserved2Used);
        Movements.Add(new StockMovement(plannedId, Id, reserved.Quantity, MovementCategory.Used, reserved.RelatedDocumentId));
        return this;
    }

    public ContainerItem AddStock(decimal amount, Guid plannedId, Guid? relatedDocId = null)
    {
        if (amount < 0)
        {
            throw new BusinessException(SkladyErrorCodes.AmountShouldNotBeZeroOrLower);
        }
        
        Movements.Add(new StockMovement(plannedId, Id, amount, MovementCategory.Unused, relatedDocId));
        return this;
    }
    
    public ContainerItem RemoveMovement(Guid movementId)
    {
        var movement = Movements.FirstOrDefault(x => x.Id == movementId);
        if (movement == null)
        {
            throw new EntityNotFoundException(typeof(StockMovement), movementId);
        }
        
        if (movement.MovementCategory == MovementCategory.Unused)
        {
            var available = Available;
            if (Available < movement.Quantity)
            {
                throw new BusinessException(SkladyErrorCodes.LackOfAvailableResources)
                    .WithData("Quantity", available);
            }
        }

        Movements.Remove(movement);
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

    protected ContainerItem()
    {
    }
}