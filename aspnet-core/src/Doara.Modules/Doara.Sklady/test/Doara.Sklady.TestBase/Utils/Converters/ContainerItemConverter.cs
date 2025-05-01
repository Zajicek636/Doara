using System;
using Doara.Sklady.Dto.ContainerItem;
using Doara.Sklady.Enums;

namespace Doara.Sklady.Utils.Converters;

public static partial class Converter
{
    public const string DefaultContainerItemName = "ContainerItem Name";
    public const string DefaultContainerItemDescription = "ContainerItem Description";
    public const decimal DefaultContainerItemDiscountRate = 10;
    public const decimal DefaultContainerItemDiscount = 10;
    public const decimal DefaultContainerItemMarkup = 10;
    public const decimal DefaultContainerItemMarkupRate = 10;
    public const decimal DefaultContainerItemRealPrice = 10;
    public const decimal DefaultContainerItemQuantity = 10;
    public const string DefaultContainerItemPurchaseUrl = "custom url";
    public const QuantityType DefaultContainerItemQuantityType = QuantityType.Kilograms;
    
    public static ContainerItemCreateInputDto CreateContainerItemInput(Guid containerId, string name = DefaultContainerItemName, 
        string description = DefaultContainerItemDescription, decimal discountRate = DefaultContainerItemDiscountRate,
        decimal discount = DefaultContainerItemDiscount, decimal markup = DefaultContainerItemMarkup, 
        decimal markupRate = DefaultContainerItemMarkupRate, decimal realPrice = DefaultContainerItemRealPrice, 
        decimal quantity = DefaultContainerItemQuantity, string? purchaseUrl = DefaultContainerItemPurchaseUrl,
        QuantityType quantityType = DefaultContainerItemQuantityType)
    {
        return new ContainerItemCreateInputDto
        {
            Name = name,
            Description = description,
            ContainerId = containerId,
            DiscountRate = discountRate,
            Discount = discount,
            Markup = markup,
            MarkupRate = markupRate,
            RealPrice = realPrice,
            PurchaseUrl = purchaseUrl,
            QuantityType = quantityType,
        };
    }
    
    public static ContainerItemUpdateInputDto Convert2UpdateInput(ContainerItemDetailDto input)
    {
        return new ContainerItemUpdateInputDto
        {
            Name = input.Name,
            Description = input.Description,
            ContainerId = input.Container.Id,
            DiscountRate = input.DiscountRate,
            Discount = input.Discount,
            Markup = input.Markup,
            MarkupRate = input.MarkupRate,
            RealPrice = input.RealPrice,
            PurchaseUrl = input.PurchaseUrl,
            QuantityType = input.QuantityType
        };
    }
}