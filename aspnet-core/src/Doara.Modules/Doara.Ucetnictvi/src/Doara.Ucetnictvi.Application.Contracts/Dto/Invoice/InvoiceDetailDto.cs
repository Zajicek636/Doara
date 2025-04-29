using System;
using System.Collections.Generic;
using Doara.Ucetnictvi.Dto.InvoiceItem;
using Doara.Ucetnictvi.Dto.Subject;
using Doara.Ucetnictvi.Enums;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Dto.Invoice;

public class InvoiceDetailDto : EntityDto<Guid>
{
    public string InvoiceNumber { get; set; } = null!;
    public SubjectDetailDto Supplier { get; set; } = null!;
    public SubjectDetailDto Customer { get; set; } = null!;
    public DateTime IssueDate { get; set; }// Datum vystavení faktury
    public DateTime? TaxDate { get; set; }// Datum uskutečnění plnění (nebo datum přijetí platby)
    public DateTime? DeliveryDate { get; set; }// Datum zdanitelného plnění (pokud se liší)
    public decimal TotalNetAmount { get; set; }// Celková částka bez DPH
    public decimal TotalVatAmount { get; set; }// Celková DPH
    public decimal TotalGrossAmount { get; set; }// Celková částka včetně DPH
    public string? PaymentTerms { get; set; }// Platební podmínky
    public VatRate VatRate { get; set; }
    public string? VariableSymbol { get; set; }
    public string? ConstantSymbol { get; set; }
    public string? SpecificSymbol { get; set; }
    public List<InvoiceItemDto> Items { get; set; } = [];
}