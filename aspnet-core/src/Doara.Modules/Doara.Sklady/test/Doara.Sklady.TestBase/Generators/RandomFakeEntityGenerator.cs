using System;
using Doara.Sklady.Enums;
using Doara.Sklady.FakeEntities;
using TestHelper.Generators;

namespace Doara.Sklady.Generators;

public static class RandomFakeEntityGenerator
{
    public static FakeContainerItem RandomFakeContainerItem()
    {
        return new FakeContainerItem
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            State = ContainerItemState.New,
            QuantityType = RandomGenerator.RandomFromEnum<QuantityType>(),
            Name = RandomGenerator.RandomAlpNum(1, FakeContainerItem.MaxNameLength),
            Description = RandomGenerator.RandomAlpNum(1, FakeContainerItem.MaxDescriptionLength),
            PurchaseUrl = RandomGenerator.RandomAlpNum(1, FakeContainerItem.MaxPurchaseUrlLength),
            Quantity = RandomGenerator.RandomNumber(FakeContainerItem.MinQuantity),
            RealPrice = RandomGenerator.RandomNumber(FakeContainerItem.MinRealPrice),
            Markup = RandomGenerator.RandomNumber(FakeContainerItem.MinMarkup),
            MarkupRate = RandomGenerator.RandomNumber(FakeContainerItem.MinMarkupRate),
            Discount = RandomGenerator.RandomNumber(FakeContainerItem.MinDiscount),
            DiscountRate = RandomGenerator.RandomNumber(FakeContainerItem.MinDiscountRate),
            ContainerId = Guid.NewGuid(),
            TenantId = null
        };
    }
    
    public static FakeContainer RandomFakeContainer()
    {
        return new FakeContainer
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            Name = RandomGenerator.RandomAlpNum(1, FakeContainer.MaxNameLength),
            Description = RandomGenerator.RandomAlpNum(1, FakeContainer.MaxDescriptionLength),
            TenantId = null
        };
    }
}