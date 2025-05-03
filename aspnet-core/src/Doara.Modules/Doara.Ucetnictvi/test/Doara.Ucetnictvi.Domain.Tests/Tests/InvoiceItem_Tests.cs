using System;
using Doara.Ucetnictvi.Enums;
using Doara.Ucetnictvi.FakeEntities;
using Doara.Ucetnictvi.Generators;
using Shouldly;
using TestHelper.Utils;
using Xunit;

namespace Doara.Ucetnictvi.Tests;

public class InvoiceItem_Tests : UcetnictviDomainModule
{
    private readonly FakeInvoiceItem _data;

    public InvoiceItem_Tests()
    {
        _data = RandomFakeEntityGenerator.RandomFakeInvoiceItem();
    }

    [Fact]
    public void Test_InvoiceItem()
    {
        var entity = _data.CreateOriginalEntity();
        _data.CheckIfSame(entity);
    }
    
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Test_InvoiceItem_ContainerItemId(bool isNull)
    {
        _data.ContainerItemId = isNull ? null : Guid.NewGuid();
        var entity = _data.CreateOriginalEntity();
        _data.CheckIfSame(entity);
    }
    
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Test_InvoiceItem_StockMovementId(bool isNull)
    {
        _data.StockMovementId = isNull ? null : Guid.NewGuid();
        var entity = _data.CreateOriginalEntity();
        _data.CheckIfSame(entity);
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeInvoiceItem.MaxDescriptionLength], MemberType = typeof(PropertyTester))]
    public void Test_InvoiceItem_Description(string description, bool shouldThrow)
    {
        _data.Description = description;
        _data.TestSetProperty<Entities.InvoiceItem, ArgumentException>(shouldThrow, nameof(Entities.InvoiceItem.Description));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeInvoiceItem.MaxDescriptionLength], MemberType = typeof(PropertyTester))]
    public void Test_InvoiceItem_Description_Setter(string description, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.InvoiceItem, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetDescription(description);
            _data.Description = description;
            return ci;
        }, shouldThrow, nameof(Entities.InvoiceItem.Description));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [false, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_InvoiceItem_Quantity(decimal quantity, bool shouldThrow)
    {
        _data.Quantity = quantity;
        _data.TestSetProperty<Entities.InvoiceItem, ArgumentException>(shouldThrow, nameof(Entities.InvoiceItem.Quantity));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [false, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_InvoiceItem_Quantity_Setter(decimal quantity, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.InvoiceItem, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetQuantity(quantity);
            _data.Quantity = quantity;
            return ci;
        }, shouldThrow, nameof(Entities.InvoiceItem.Quantity));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [true, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_InvoiceItem_UnitPrice(decimal unitPrice, bool shouldThrow)
    {
        _data.UnitPrice = unitPrice;
        _data.TestSetProperty<Entities.InvoiceItem, ArgumentException>(shouldThrow, nameof(Entities.InvoiceItem.UnitPrice));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [true, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_InvoiceItem_UnitPrice_Setter(decimal unitPrice, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.InvoiceItem, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetUnitPrice(unitPrice);
            _data.UnitPrice = unitPrice;
            return ci;
        }, shouldThrow, nameof(Entities.InvoiceItem.UnitPrice));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [true, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_InvoiceItem_NetAmount(decimal netAmount, bool shouldThrow)
    {
        _data.NetAmount = netAmount;
        _data.TestSetProperty<Entities.InvoiceItem, ArgumentException>(shouldThrow, nameof(Entities.InvoiceItem.NetAmount));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [true, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_InvoiceItem_NetAmount_Setter(decimal netAmount, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.InvoiceItem, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetNetAmount(netAmount);
            _data.NetAmount = netAmount;
            return ci;
        }, shouldThrow, nameof(Entities.InvoiceItem.NetAmount));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [true, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_InvoiceItem_VatAmount(decimal vatAmount, bool shouldThrow)
    {
        _data.VatAmount = vatAmount;
        _data.TestSetProperty<Entities.InvoiceItem, ArgumentException>(shouldThrow, nameof(Entities.InvoiceItem.VatAmount));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [true, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_InvoiceItem_VatAmount_Setter(decimal vatAmount, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.InvoiceItem, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetVatAmount(vatAmount);
            _data.VatAmount = vatAmount;
            return ci;
        }, shouldThrow, nameof(Entities.InvoiceItem.VatAmount));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [true, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_InvoiceItem_GrossAmount(decimal grossAmount, bool shouldThrow)
    {
        _data.GrossAmount = grossAmount;
        _data.TestSetProperty<Entities.InvoiceItem, ArgumentException>(shouldThrow, nameof(Entities.InvoiceItem.GrossAmount));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [true, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_InvoiceItem_GrossAmount_Setter(decimal grossAmount, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.InvoiceItem, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetGrossAmount(grossAmount);
            _data.GrossAmount = grossAmount;
            return ci;
        }, shouldThrow, nameof(Entities.InvoiceItem.GrossAmount));
    }
    
    [Fact]
    public void Test_InvoiceItem_VatRate_Nullable_Convert()
    {
        _data.VatRate = null;
        var entity = _data.CreateOriginalEntity(false);
        _data.VatRate = VatRate.None;
        _data.CheckIfSame(entity);
    }
    
    [Fact]
    public void Test_InvoiceItem_VatRate_Nullable_Convert_Setter()
    {
        var entity = _data.CreateOriginalEntity(false)
            .SetVatRate(null);
        _data.VatRate = VatRate.None;
        _data.CheckIfSame(entity);
    }
    
    [Fact]
    public void Test_InvoiceItem_GetCopy()
    {
        var entity = _data.CreateOriginalEntity(false);
        var copy = entity.GetCopy();
        
        entity.ShouldNotBe(copy);
        entity.ShouldBeEquivalentTo(copy);
    }
}