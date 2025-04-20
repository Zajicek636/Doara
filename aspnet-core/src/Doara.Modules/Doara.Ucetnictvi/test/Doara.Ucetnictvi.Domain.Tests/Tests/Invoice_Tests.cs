using System;
using Doara.Ucetnictvi.Enums;
using Doara.Ucetnictvi.FakeEntities;
using Doara.Ucetnictvi.Generators;
using TestHelper.Utils;
using Xunit;

namespace Doara.Ucetnictvi.Tests;

public class Invoice_Tests : UcetnictviDomainModule
{
    private readonly FakeInvoice _data;

    public Invoice_Tests()
    {
        _data = RandomFakeEntityGenerator.RandomFakeInvoice();
    }
  
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeInvoice.MaxInvoiceNumberLength], MemberType = typeof(PropertyTester))]
    public void Test_Invoice_InvoiceNumber(string invoiceNumber, bool shouldThrow)
    {
        _data.InvoiceNumber = invoiceNumber;
        _data.TestSetProperty<Entities.Invoice, ArgumentException>(shouldThrow, nameof(Entities.Invoice.InvoiceNumber));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [false, FakeInvoice.MaxInvoiceNumberLength], MemberType = typeof(PropertyTester))]
    public void Test_Invoice_InvoiceNumber_Setter(string invoiceNumber, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Invoice, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetInvoiceNumber(invoiceNumber);
            _data.InvoiceNumber = invoiceNumber;
            return ci;
        }, shouldThrow, nameof(Entities.Invoice.InvoiceNumber));
    }
    
    [Theory]
    [InlineData("2025-01-01", false)]
    [InlineData(null, false)]
    public void Test_Invoice_TaxDate(string? taxDateString, bool shouldThrow)
    {
        _data.TaxDate = taxDateString != null ? DateTime.Parse(taxDateString) : null;
        _data.TestSetProperty<Entities.Invoice, ArgumentException>(shouldThrow, nameof(Entities.Invoice.TaxDate));
    }
    
    [Theory]
    [InlineData("2025-01-01", false)]
    [InlineData(null, false)]
    public void Test_Invoice_TaxDate_Setter(string? taxDateString, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Invoice, ArgumentException>((data, _) =>
        {
            DateTime? taxDate = taxDateString != null ? DateTime.Parse(taxDateString) : null;
            var ci = data.CreateOriginalEntity().SetTaxDate(taxDate);
            _data.TaxDate = taxDate;
            return ci;
        }, shouldThrow, nameof(Entities.Invoice.TaxDate));
    }
    
    [Theory]
    [InlineData("2025-01-01", false)]
    [InlineData(null, false)]
    public void Test_Invoice_DeliveryDate(string? deliveryDateString, bool shouldThrow)
    {
        _data.DeliveryDate = deliveryDateString != null ? DateTime.Parse(deliveryDateString) : null;
        _data.TestSetProperty<Entities.Invoice, ArgumentException>(shouldThrow, nameof(Entities.Invoice.DeliveryDate));
    }
    
    [Theory]
    [InlineData("2025-01-01", false)]
    [InlineData(null, false)]
    public void Test_Invoice_DeliveryDate_Setter(string? deliveryDateString, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Invoice, ArgumentException>((data, _) =>
        {
            DateTime? deliveryDate = deliveryDateString != null ? DateTime.Parse(deliveryDateString) : null;
            var ci = data.CreateOriginalEntity().SetDeliveryDate(deliveryDate);
            _data.DeliveryDate = deliveryDate;
            return ci;
        }, shouldThrow, nameof(Entities.Invoice.DeliveryDate));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [true, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_Invoice_TotalNetAmount(decimal value, bool shouldThrow)
    {
        _data.TotalNetAmount = value;
        _data.TestSetProperty<Entities.Invoice, ArgumentException>(shouldThrow, nameof(Entities.Invoice.TotalNetAmount));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [true, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_Invoice_TotalNetAmount_Setter(decimal value, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Invoice, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetTotalNetAmount(value);
            _data.TotalNetAmount = value;
            return ci;
        }, shouldThrow, nameof(Entities.Invoice.TotalNetAmount));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [true, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_Invoice_TotalVatAmount(decimal value, bool shouldThrow)
    {
        _data.TotalVatAmount = value;
        _data.TestSetProperty<Entities.Invoice, ArgumentException>(shouldThrow, nameof(Entities.Invoice.TotalVatAmount));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [true, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_Invoice_TotalVatAmount_Setter(decimal value, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Invoice, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetTotalVatAmount(value);
            _data.TotalVatAmount = value;
            return ci;
        }, shouldThrow, nameof(Entities.Invoice.TotalVatAmount));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [true, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_Invoice_TotalGrossAmount(decimal value, bool shouldThrow)
    {
        _data.TotalGrossAmount = value;
        _data.TestSetProperty<Entities.Invoice, ArgumentException>(shouldThrow, nameof(Entities.Invoice.TotalGrossAmount));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetDecimalSignTestData), [true, true, true, 2], MemberType = typeof(PropertyTester))]
    public void Test_Invoice_TotalGrossAmount_Setter(decimal value, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Invoice, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetTotalGrossAmount(value);
            _data.TotalGrossAmount = value;
            return ci;
        }, shouldThrow, nameof(Entities.Invoice.TotalGrossAmount));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [true, FakeInvoice.MaxPaymentTermsLength], MemberType = typeof(PropertyTester))]
    public void Test_Invoice_PaymentTerms(string value, bool shouldThrow)
    {
        _data.PaymentTerms = value;
        _data.TestSetProperty<Entities.Invoice, ArgumentException>(shouldThrow, nameof(Entities.Invoice.PaymentTerms));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [true, FakeInvoice.MaxPaymentTermsLength], MemberType = typeof(PropertyTester))]
    public void Test_Invoice_PaymentTerms_Setter(string value, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Invoice, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetPaymentTerms(value);
            _data.PaymentTerms = value;
            return ci;
        }, shouldThrow, nameof(Entities.Invoice.PaymentTerms));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [true, FakeInvoice.MaxVariableSymbolLength], MemberType = typeof(PropertyTester))]
    public void Test_Invoice_VariableSymbol(string value, bool shouldThrow)
    {
        _data.VariableSymbol = value;
        _data.TestSetProperty<Entities.Invoice, ArgumentException>(shouldThrow, nameof(Entities.Invoice.VariableSymbol));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [true, FakeInvoice.MaxVariableSymbolLength], MemberType = typeof(PropertyTester))]
    public void Test_Invoice_VariableSymbol_Setter(string value, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Invoice, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetVariableSymbol(value);
            _data.VariableSymbol = value;
            return ci;
        }, shouldThrow, nameof(Entities.Invoice.VariableSymbol));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [true, FakeInvoice.MaxConstantSymbolLength], MemberType = typeof(PropertyTester))]
    public void Test_Invoice_ConstantSymbol(string value, bool shouldThrow)
    {
        _data.ConstantSymbol = value;
        _data.TestSetProperty<Entities.Invoice, ArgumentException>(shouldThrow, nameof(Entities.Invoice.ConstantSymbol));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [true, FakeInvoice.MaxConstantSymbolLength], MemberType = typeof(PropertyTester))]
    public void Test_Invoice_ConstantSymbol_Setter(string value, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Invoice, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetConstantSymbol(value);
            _data.ConstantSymbol = value;
            return ci;
        }, shouldThrow, nameof(Entities.Invoice.ConstantSymbol));
    }
    
    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [true, FakeInvoice.MaxSpecificSymbolLength], MemberType = typeof(PropertyTester))]
    public void Test_Invoice_SpecificSymbol(string value, bool shouldThrow)
    {
        _data.SpecificSymbol = value;
        _data.TestSetProperty<Entities.Invoice, ArgumentException>(shouldThrow, nameof(Entities.Invoice.SpecificSymbol));
    }

    [Theory]
    [MemberData(nameof(PropertyTester.GetStringPropertyTestData), [true, FakeInvoice.MaxSpecificSymbolLength], MemberType = typeof(PropertyTester))]
    public void Test_Invoice_SpecificSymbol_Setter(string value, bool shouldThrow)
    {
        _data.TestSetProperty<Entities.Invoice, ArgumentException>((data, _) =>
        {
            var ci = data.CreateOriginalEntity().SetSpecificSymbol(value);
            _data.SpecificSymbol = value;
            return ci;
        }, shouldThrow, nameof(Entities.Invoice.SpecificSymbol));
    }

    [Fact]
    public void Test_Invoice_VatRate_Nullable_Convert()
    {
        _data.VatRate = null;
        var entity = _data.CreateOriginalEntity(false);
        _data.VatRate = VatRate.None;
        _data.CheckIfSame(entity);
    }
    
    [Fact]
    public void Test_Invoice_VatRate_Nullable_Convert_Setter()
    {
        var entity = _data.CreateOriginalEntity(false)
            .SetVatRate(null);
        _data.VatRate = VatRate.None;
        _data.CheckIfSame(entity);
    }
}