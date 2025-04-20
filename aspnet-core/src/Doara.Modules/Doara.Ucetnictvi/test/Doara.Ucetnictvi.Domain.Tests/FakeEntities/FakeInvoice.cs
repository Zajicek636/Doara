using System;
using System.Text.Json;
using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.Enums;
using Doara.Ucetnictvi.Utils;
using Doara.Ucetnictvi.Utils.Converters;
using TestHelper.FakeEntities;

namespace Doara.Ucetnictvi.FakeEntities;

public class FakeInvoice : IFakeEntity<Invoice>
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public Guid? TenantId { get; set; }
    public string InvoiceNumber { get; set; } = null!;
    public Guid SupplierId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime IssueDate { get; set; }// Datum vystavení faktury
    public DateTime? TaxDate { get; set; }// Datum uskutečnění plnění (nebo datum přijetí platby)
    public DateTime? DeliveryDate { get; set; }// Datum zdanitelného plnění (pokud se liší)
    public decimal TotalNetAmount { get; set; }// Celková částka bez DPH
    public decimal TotalVatAmount { get; set; }// Celková DPH
    public decimal TotalGrossAmount { get; set; }// Celková částka včetně DPH
    public string? PaymentTerms { get; set; }// Platební podmínky
    public VatRate? VatRate { get; set; }
    public string? VariableSymbol { get; set; }
    public string? ConstantSymbol { get; set; }
    public string? SpecificSymbol { get; set; }
    
    public string SerializeToJson()
    {
        return JsonSerializer.Serialize(this, JsonConfig.DefaultJsonSerializerOptions);
    }

    public Invoice CreateOriginalEntity(bool checkIfNotThrow = true)
    {
        return Converter.CreateOriginalEntity(this, checkIfNotThrow);
    }

    public void CheckIfSame(Invoice entity)
    {
        Comparator.CheckIfSame(this, entity);
    }
    
    public const int MaxInvoiceNumberLength = 50;
    public const int MaxPaymentTermsLength = 100;
    public const int MaxVariableSymbolLength = 20;
    public const int MaxConstantSymbolLength = 10;
    public const int MaxSpecificSymbolLength = 20;
}