using System;
using Doara.Sklady.Enums;
using Doara.Sklady.FakeEntities;
using Doara.Sklady.Generators;
using Shouldly;
using TestHelper.Utils;
using Xunit;

namespace Doara.Sklady.Tests;

public class ContainerItem_Tests : SkladyDomainModule
{
    private readonly FakeContainerItem _data;
    private static decimal CalculatePresentationPrice(FakeContainerItem fci)
    {
        return fci.RealPrice * (100 + fci.MarkupRate - fci.DiscountRate) / 100 + fci.Markup - fci.Discount;
    }

    public ContainerItem_Tests()
    {
        _data = RandomFakeEntityGenerator.RandomFakeContainerItem();
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeContainerItem.MaxNameLength], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_Name(string name, bool shouldThrow)
    {
        _data.Name = name;
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>(shouldThrow, nameof(Entities.ContainerItem.Name));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeContainerItem.MaxNameLength], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_Name_Setter(string name, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity();
            ci.SetName(name);
            _data.Name = name;
            return ci;
        }, shouldThrow, nameof(Entities.ContainerItem.Name));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeContainerItem.MaxDescriptionLength], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_Description(string description, bool shouldThrow)
    {
        _data.Description = description;
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>(shouldThrow, nameof(Entities.ContainerItem.Description));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeContainerItem.MaxDescriptionLength], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_Description_Setter(string description, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity();
            ci.SetDescription(description);
            _data.Description = description;
            return ci;
        }, shouldThrow, nameof(Entities.ContainerItem.Description));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [true, FakeContainerItem.MaxPurchaseUrlLength], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_PurchaseUrl(string purchaseUrl, bool shouldThrow)
    {
        _data.PurchaseUrl = purchaseUrl;
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>(shouldThrow, nameof(Entities.ContainerItem.PurchaseUrl));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [true, FakeContainerItem.MaxPurchaseUrlLength], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_PurchaseUrl_Setter(string purchaseUrl, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity();
            ci.SetPurchaseUrl(purchaseUrl);
            _data.PurchaseUrl = purchaseUrl;
            return ci;
        }, shouldThrow, nameof(Entities.ContainerItem.PurchaseUrl));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [false, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_Quantity(decimal quantity, bool shouldThrow)
    {
        _data.Quantity = quantity;
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>(shouldThrow, nameof(Entities.ContainerItem.Quantity));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [false, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_Quantity_Setter(decimal quantity, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity();
            ci.SetQuantity(quantity);
            _data.Quantity = quantity;
            ci.PresentationPrice.ShouldBe(CalculatePresentationPrice(_data));
            return ci;
        }, shouldThrow, nameof(Entities.ContainerItem.Quantity));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [false, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_RealPrice(decimal realPrice, bool shouldThrow)
    {
        _data.RealPrice = realPrice;
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>(shouldThrow, nameof(Entities.ContainerItem.RealPrice));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [false, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_RealPrice_Setter(decimal realPrice, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity();
            ci.SetRealPrice(realPrice);
            _data.RealPrice = realPrice;
            ci.PresentationPrice.ShouldBe(CalculatePresentationPrice(_data));
            return ci;
        }, shouldThrow, nameof(Entities.ContainerItem.RealPrice));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [false, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_Markup(decimal markup, bool shouldThrow)
    {
        _data.Markup = markup;
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>(shouldThrow, nameof(Entities.ContainerItem.Markup));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [false, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_Markup_Setter(decimal markup, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity();
            ci.SetMarkup(markup);
            _data.Markup = markup;
            ci.PresentationPrice.ShouldBe(CalculatePresentationPrice(_data));
            return ci;
        }, shouldThrow, nameof(Entities.ContainerItem.Markup));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [false, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_MarkupRate(decimal markupRate, bool shouldThrow)
    {
        _data.MarkupRate = markupRate;
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>(shouldThrow, nameof(Entities.ContainerItem.MarkupRate));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [false, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_MarkupRate_Setter(decimal markupRate, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity();
            ci.SetMarkupRate(markupRate);
            _data.MarkupRate = markupRate;
            ci.PresentationPrice.ShouldBe(CalculatePresentationPrice(_data));
            return ci;
        }, shouldThrow, nameof(Entities.ContainerItem.MarkupRate));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [false, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_Discount(decimal discount, bool shouldThrow)
    {
        _data.Discount = discount;
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>(shouldThrow, nameof(Entities.ContainerItem.Discount));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [false, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_Discount_Setter(decimal discount, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity();
            ci.SetDiscount(discount);
            _data.Discount = discount;
            ci.PresentationPrice.ShouldBe(CalculatePresentationPrice(_data));
            return ci;
        }, shouldThrow, nameof(Entities.ContainerItem.Discount));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [false, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_DiscountRate(decimal discountRate, bool shouldThrow)
    {
        _data.DiscountRate = discountRate;
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>(shouldThrow, nameof(Entities.ContainerItem.DiscountRate));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [false, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_DiscountRate_Setter(decimal discountRate, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity();
            ci.SetDiscountRate(discountRate);
            _data.DiscountRate = discountRate;
            ci.PresentationPrice.ShouldBe(CalculatePresentationPrice(_data));
            return ci;
        }, shouldThrow, nameof(Entities.ContainerItem.DiscountRate));
    }
    
    [Fact]
    public void Test_ContainerItem_PresentationPrice()
    {
        _data.RealPrice = 100;
        _data.Markup = 100;
        _data.MarkupRate = 100;
        _data.Discount = 50;
        _data.DiscountRate = 50;
        var ci = _data.CreateOriginalEntity();
        _data.CheckIfSame(ci);
        ci.PresentationPrice.ShouldBe(200);
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetEnumPropertyTestData), [typeof(QuantityType)], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_QuantityType_Setter(QuantityType quantityType, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity();
            ci.SetQuantityType(quantityType);
            _data.QuantityType = quantityType;
            return ci;
        }, shouldThrow, nameof(Entities.ContainerItem.QuantityType));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetEnumPropertyTestData), [typeof(ContainerItemState)], MemberType = typeof(PropertyTester))]
    public void Test_ContainerItem_ContainerItemState_Setter(ContainerItemState state, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.ContainerItem, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity();
            ci.SetState(state);
            _data.State = state;
            return ci;
        }, shouldThrow, nameof(Entities.ContainerItem.State));
    }
}