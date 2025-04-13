using Doara.Sklady.Entities;
using Doara.Sklady.FakeEntities;
using TestHelper.Utils;

namespace Doara.Sklady.Utils.Converters;

public static partial class Converter
{
    public static ContainerItem CreateOriginalEntity(this FakeContainerItem data, bool checkIfNotThrow = true)
    {
        return data.DoActionWithNotThrowCheck(() => new ContainerItem(data.Id, data.Name, data.Description, 
            data.RealPrice, data.Markup, data.MarkupRate, data.Discount, data.DiscountRate, data.PurchaseUrl,
            data.ContainerId, data.Quantity, data.QuantityType), checkIfNotThrow);
    }
}