using System;
using System.ComponentModel.DataAnnotations;
using Doara.Ucetnictvi.Constants;
using Doara.Ucetnictvi.Enums;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Dto.Invoice;

public class InvoiceUpdateInputDto : EntityDto<Guid>
{
    [Required]
    [StringLength(InvoiceConstants.MaxInvoiceNumberLength)]
    public string InvoiceNumber { get; set; } = null!;
    
    [Required]
    public Guid SupplierId { get; set; }
    
    [Required]
    public Guid CustomerId { get; set; }
    
    [Required]
    public DateTime IssueDate { get; set; }// Datum vystavení faktury
    
    public DateTime? TaxDate { get; set; }// Datum uskutečnění plnění (nebo datum přijetí platby)
    
    public DateTime? DeliveryDate { get; set; }// Datum zdanitelného plnění (pokud se liší)
    
    [Required]
    public decimal TotalNetAmount { get; set; }// Celková částka bez DPH
    
    [Required]
    public decimal TotalVatAmount { get; set; }// Celková DPH
    
    [Required]
    public decimal TotalGrossAmount { get; set; }// Celková částka včetně DPH
    
    [StringLength(InvoiceConstants.MaxPaymentTermsLength)]
    public string? PaymentTerms { get; set; }// Platební podmínky
    
    [Required]
    public VatRate VatRate { get; set; }
    
    [StringLength(InvoiceConstants.MaxVariableSymbolLength)]
    public string? VariableSymbol { get; set; }
    
    [StringLength(InvoiceConstants.MaxConstantSymbolLength)]
    public string? ConstantSymbol { get; set; }
    
    [StringLength(InvoiceConstants.MaxSpecificSymbolLength)]
    public string? SpecificSymbol { get; set; }
}