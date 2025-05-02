using Doara.Sklady.Entities;
using Doara.Sklady.FakeEntities;
using TestHelper.Utils;

namespace Doara.Sklady.Utils;

public static class Comparator
{
    public static void CheckIfSame(this FakeContainerItem data, ContainerItem entity)
    {
        var exShould = new ExtendedShould<ContainerItem>(data);
        exShould.ShouldBe(entity.Id, data.Id);
        exShould.ShouldBe(entity.IsDeleted, data.IsDeleted);
        exShould.ShouldBe(entity.QuantityType, data.QuantityType);
        exShould.ShouldBe(entity.Name, data.Name);
        exShould.ShouldBe(entity.Description, data.Description);
        exShould.ShouldBe(entity.PurchaseUrl, data.PurchaseUrl);
        exShould.ShouldBe(entity.RealPrice, data.RealPrice);
        exShould.ShouldBe(entity.Markup, data.Markup);
        exShould.ShouldBe(entity.MarkupRate, data.MarkupRate);
        exShould.ShouldBe(entity.Discount, data.Discount);
        exShould.ShouldBe(entity.DiscountRate, data.DiscountRate);
        exShould.ShouldBe(entity.ContainerId, data.ContainerId);
        exShould.ShouldBe(entity.TenantId, data.TenantId);
    }
    
    public static void CheckIfSame(this FakeContainer data, Container entity)
    {
        var exShould = new ExtendedShould<Container>(data);
        exShould.ShouldBe(entity.Id, data.Id);
        exShould.ShouldBe(entity.IsDeleted, data.IsDeleted);
        exShould.ShouldBe(entity.Name, data.Name);
        exShould.ShouldBe(entity.Description, data.Description);
        exShould.ShouldBe(entity.TenantId, data.TenantId);
    }
}