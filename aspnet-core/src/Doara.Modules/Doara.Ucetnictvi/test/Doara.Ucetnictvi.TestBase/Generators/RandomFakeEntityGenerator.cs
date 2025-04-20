using System;
using Doara.Ucetnictvi.Enums;
using Doara.Ucetnictvi.FakeEntities;
using TestHelper.Generators;

namespace Doara.Ucetnictvi.Generators;

public static class RandomFakeEntityGenerator
{
    public static FakeCountry RandomFakeCountry()
    {
        return new FakeCountry
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            Name = RandomGenerator.RandomAlpNum(1, FakeCountry.MaxNameLength),
            Code = RandomGenerator.RandomAlpNum(1, FakeCountry.MaxCodeLength),
            TenantId = null
        };
    }
    
    public static FakeAddress RandomFakeAddress()
    {
        return new FakeAddress
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            Street = RandomGenerator.RandomAlpNum(1, FakeAddress.MaxStreetLength),
            City = RandomGenerator.RandomAlpNum(1, FakeAddress.MaxCityLength),
            PostalCode = RandomGenerator.RandomAlpNum(1, FakeAddress.MaxPostalCodeLength),
            CountryId = Guid.NewGuid(),
            TenantId = null
        };
    }
    
    public static FakeSubject RandomFakeSubject()
    {
        return new FakeSubject
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            Name = RandomGenerator.RandomAlpNum(1, FakeSubject.MaxNameLength),
            AddressId = Guid.NewGuid(),
            Ic = RandomGenerator.RandomAlpNum(1, FakeSubject.MaxIcLength),
            Dic = RandomGenerator.RandomAlpNum(1, FakeSubject.MaxDicLength),
            IsVatPayer = RandomGenerator.RandomNumber(0, 2) == 1,
            TenantId = null
        };
    }
    
    public static FakeInvoiceItem RandomFakeInvoiceItem()
    {
        return new FakeInvoiceItem
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            TenantId = null,
            InvoiceId = Guid.NewGuid(),
            Description = RandomGenerator.RandomAlpNum(1, FakeInvoiceItem.MaxDescriptionLength),
            Quantity = RandomGenerator.RandomNumber(FakeInvoiceItem.MinQuantity),
            UnitPrice = RandomGenerator.RandomNumber(),
            NetAmount = RandomGenerator.RandomNumber(),
            VatRate = RandomGenerator.RandomFromEnum<VatRate>(),
            VatAmount = RandomGenerator.RandomNumber(),
            GrossAmount = RandomGenerator.RandomNumber()
        };
    }
    
    public static FakeInvoice RandomFakeInvoice()
    {
        return new FakeInvoice
        {
            Id = Guid.NewGuid(),
            IsDeleted = false,
            TenantId = null,
            InvoiceNumber = RandomGenerator.RandomAlpNum(1, FakeInvoice.MaxInvoiceNumberLength),
            SupplierId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            IssueDate = new DateTime(),
            TaxDate = new DateTime(),
            DeliveryDate = new DateTime(),
            TotalNetAmount = RandomGenerator.RandomNumber(),
            TotalVatAmount = RandomGenerator.RandomNumber(),
            TotalGrossAmount = RandomGenerator.RandomNumber(),
            VatRate = RandomGenerator.RandomFromEnum<VatRate>(),
            VariableSymbol = RandomGenerator.RandomAlpNum(1, FakeInvoice.MaxVariableSymbolLength),
            ConstantSymbol = RandomGenerator.RandomAlpNum(1, FakeInvoice.MaxConstantSymbolLength),
            SpecificSymbol = RandomGenerator.RandomAlpNum(1, FakeInvoice.MaxSpecificSymbolLength),
        };
    }
}