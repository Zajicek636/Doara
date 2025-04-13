using System;
using Doara.Sklady.Entities;
using System.Text.Json;
using Doara.Sklady.Enums;
using Doara.Sklady.Utils;
using TestHelper.FakeEntities;

namespace Doara.Sklady.FakeEntities;

public class FakeContainerItem : IFakeEntity<ContainerItem>
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set;}
    public ContainerItemState State { get; set; }
    public QuantityType QuantityType { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? PurchaseUrl { get; set; }
    public decimal Quantity { get; set; }
    public decimal RealPrice { get; set; }
    public decimal Markup { get; set; } //Marže
    public decimal MarkupRate { get; set; } //Marže %
    public decimal Discount { get; set; } //Sleva
    public decimal DiscountRate { get; set; } //Sleva %
    public Guid ContainerId { get; set; }
    public Guid? TenantId { get; set; }
    
    public string SerializeToJson()
    {
        return JsonSerializer.Serialize(this, JsonConfig.DefaultJsonSerializerOptions);
    }

    public ContainerItem CreateOriginalEntity(bool checkIfNotThrow = true)
    {
        return Utils.Converters.Converter.CreateOriginalEntity(this, checkIfNotThrow);
    }

    public void CheckIfSame(ContainerItem entity)
    {
        Comparator.CheckIfSame(this, entity);
    }
    
    public const int MaxNameLength = 255;
    public const int MaxDescriptionLength = 4000;
    public const int MaxPurchaseUrlLength = 2048;
    public const int MinQuantity = 0;
    public const int MinRealPrice = 0;
    public const int MinMarkup = 0;
    public const int MinMarkupRate = 0;
    public const int MinDiscount = 0;
    public const int MinDiscountRate = 0;
}