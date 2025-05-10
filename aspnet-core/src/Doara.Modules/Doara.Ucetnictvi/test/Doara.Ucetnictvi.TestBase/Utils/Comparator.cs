using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.FakeEntities;
using TestHelper.Utils;

namespace Doara.Ucetnictvi.Utils;

public static class Comparator
{
    public static void CheckIfSame(this FakeCountry data, Country entity)
    {
        var exShould = new ExtendedShould<Country>(data);
        exShould.ShouldBe(entity.Id, data.Id);
        exShould.ShouldBe(entity.IsDeleted, data.IsDeleted);
        exShould.ShouldBe(entity.Name, data.Name);
        exShould.ShouldBe(entity.Code, data.Code);
        exShould.ShouldBe(entity.TenantId, data.TenantId);
    }
    
    public static void CheckIfSame(this FakeAddress data, Address entity)
    {
        var exShould = new ExtendedShould<Address>(data);
        exShould.ShouldBe(entity.Id, data.Id);
        exShould.ShouldBe(entity.IsDeleted, data.IsDeleted);
        exShould.ShouldBe(entity.Street, data.Street);
        exShould.ShouldBe(entity.City, data.City);
        exShould.ShouldBe(entity.PostalCode, data.PostalCode);
        exShould.ShouldBe(entity.CountryId, data.CountryId);
        exShould.ShouldBe(entity.TenantId, data.TenantId);
    }
    
    public static void CheckIfSame(this FakeSubject data, Subject entity)
    {
        var exShould = new ExtendedShould<Subject>(data);
        exShould.ShouldBe(entity.Id, data.Id);
        exShould.ShouldBe(entity.IsDeleted, data.IsDeleted);
        exShould.ShouldBe(entity.Name, data.Name);
        exShould.ShouldBe(entity.AddressId, data.AddressId);
        exShould.ShouldBe(entity.Ic, data.Ic);
        exShould.ShouldBe(entity.Dic, data.Dic);
        exShould.ShouldBe(entity.TenantId, data.TenantId);
    }
    
    public static void CheckIfSame(this FakeInvoiceItem data, InvoiceItem entity)
    {
        var exShould = new ExtendedShould<InvoiceItem>(data);
        exShould.ShouldBe(entity.Id, data.Id);
        exShould.ShouldBe(entity.IsDeleted, data.IsDeleted);
        exShould.ShouldBe(entity.TenantId, data.TenantId);
        exShould.ShouldBe(entity.InvoiceId, data.InvoiceId);
        exShould.ShouldBe(entity.Description, data.Description);
        exShould.ShouldBe(entity.Quantity, data.Quantity);
        exShould.ShouldBe(entity.UnitPrice, data.UnitPrice);
        exShould.ShouldBe(entity.NetAmount, data.NetAmount);
        exShould.ShouldBe(entity.VatRate, data.VatRate);
        exShould.ShouldBe(entity.VatAmount, data.VatAmount);
        exShould.ShouldBe(entity.GrossAmount, data.GrossAmount);
        exShould.ShouldBe(entity.ContainerItemId, data.ContainerItemId);
        exShould.ShouldBe(entity.StockMovementId, data.StockMovementId);
    }
    
    public static void CheckIfSame(this FakeInvoice data, Invoice entity)
    {
        var exShould = new ExtendedShould<Invoice>(data);
        exShould.ShouldBe(entity.Id, data.Id);
        exShould.ShouldBe(entity.IsDeleted, data.IsDeleted);
        exShould.ShouldBe(entity.TenantId, data.TenantId);
        exShould.ShouldBe(entity.InvoiceType, data.InvoiceType);
        exShould.ShouldBe(entity.InvoiceNumber, data.InvoiceNumber);
        exShould.ShouldBe(entity.SupplierId, data.SupplierId);
        exShould.ShouldBe(entity.CustomerId, data.CustomerId);
        exShould.ShouldBe(entity.IssueDate, data.IssueDate);
        exShould.ShouldBe(entity.TaxDate, data.TaxDate);
        exShould.ShouldBe(entity.DeliveryDate, data.DeliveryDate);
        exShould.ShouldBe(entity.TotalNetAmount, data.TotalNetAmount);
        exShould.ShouldBe(entity.TotalVatAmount, data.TotalVatAmount);
        exShould.ShouldBe(entity.TotalGrossAmount, data.TotalGrossAmount);
        exShould.ShouldBe(entity.PaymentTerms, data.PaymentTerms);
        exShould.ShouldBe(entity.VatRate, data.VatRate);
        exShould.ShouldBe(entity.VariableSymbol, data.VariableSymbol);
        exShould.ShouldBe(entity.ConstantSymbol, data.ConstantSymbol);
        exShould.ShouldBe(entity.SpecificSymbol, data.SpecificSymbol);
    }
}