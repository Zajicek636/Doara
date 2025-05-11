using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.FakeEntities;
using TestHelper.Utils;

namespace Doara.Ucetnictvi.Utils.Converters;

public static partial class Converter
{
    public static Country CreateOriginalEntity(this FakeCountry data, bool checkIfNotThrow = true)
    {
        return data.DoActionWithNotThrowCheck(() => new Country(data.Id, data.Name, data.Code), checkIfNotThrow);
    }
    
    public static Address CreateOriginalEntity(this FakeAddress data, bool checkIfNotThrow = true)
    {
        return data.DoActionWithNotThrowCheck(() => new Address(data.Id, data.Street, data.City, data.PostalCode, 
            data.CountryId), checkIfNotThrow);
    }
    
    public static Subject CreateOriginalEntity(this FakeSubject data, bool checkIfNotThrow = true)
    {
        return data.DoActionWithNotThrowCheck(() => new Subject(data.Id, data.Name, data.AddressId, data.Ic, 
            data.Dic, data.IsVatPayer), checkIfNotThrow);
    }
    
    public static InvoiceItem CreateOriginalEntity(this FakeInvoiceItem data, bool checkIfNotThrow = true)
    {
        return data.DoActionWithNotThrowCheck(() => new InvoiceItem(data.Id, data.InvoiceId, data.Description, 
            data.Quantity, data.UnitPrice, data.NetAmount, data.VatRate, data.VatAmount, 
            data.GrossAmount, data.ContainerItemId, data.StockMovementId), checkIfNotThrow);
    }
    
    public static Invoice CreateOriginalEntity(this FakeInvoice data, bool checkIfNotThrow = true)
    {
        return data.DoActionWithNotThrowCheck(() => new Invoice(data.Id, data.InvoiceType, data.InvoiceNumber, data.SupplierId, 
            data.CustomerId, data.IssueDate, data.TaxDate, data.DeliveryDate, data.TotalNetAmount,
            data.TotalVatAmount, data.TotalGrossAmount, data.PaymentTerms, data.VatRate,
            data.VariableSymbol, data.ConstantSymbol, data.SpecificSymbol), checkIfNotThrow);
    }
}