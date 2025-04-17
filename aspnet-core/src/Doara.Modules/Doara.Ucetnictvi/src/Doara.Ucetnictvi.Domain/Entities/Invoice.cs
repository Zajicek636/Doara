using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Doara.Ucetnictvi.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Doara.Ucetnictvi.Entities;

public class Invoice : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
{
    public virtual bool IsDeleted { get; }
    public virtual Guid? TenantId { get; }
    public virtual string InvoiceNumber { get; private set; }
    public virtual Guid SupplierId { get; private set; }
    public virtual Subject Supplier { get; }// Dodavatel (plátce DPH)
    public virtual Guid CustomerId { get; private set; }
    public virtual Subject Customer { get; }// Odběratel
    public virtual DateTime IssueDate { get; private set; }// Datum vystavení faktury
    public virtual DateTime? TaxDate { get; private set; }// Datum uskutečnění plnění (nebo datum přijetí platby)
    public virtual DateTime? DeliveryDate { get; private set; }// Datum zdanitelného plnění (pokud se liší)
    public virtual decimal TotalNetAmount { get; private set; }// Celková částka bez DPH
    public virtual decimal TotalVatAmount { get; private set; }// Celková DPH
    public virtual decimal TotalGrossAmount { get; private set; }// Celková částka včetně DPH
    public virtual string? PaymentTerms { get; private set; }// Platební podmínky
    public virtual VatRate VatRate { get; private set; }
    public virtual string? VariableSymbol { get; private set; }
    public virtual string? ConstantSymbol { get; private set; }
    public virtual string? SpecificSymbol { get; private set; }
    public virtual ICollection<InvoiceItem> Items { get; private set; }// Položky faktury

    public Invoice(string invoiceNumber, Guid supplierId, Guid customerId, DateTime issueDate, DateTime? taxDate, DateTime? deliveryDate, decimal totalNetAmount, decimal totalVatAmount, decimal totalGrossAmount, string paymentTerms, VatRate? vatRate, string? variableSymbol, string? constantSymbol, string? specificSymbol)
    {
        InvoiceNumber = Check.NotNullOrWhiteSpace(invoiceNumber, nameof(InvoiceNumber), 50);
        SupplierId = Check.NotNull(supplierId, nameof(SupplierId));
        CustomerId = Check.NotNull(customerId, nameof(CustomerId));
        IssueDate = Check.NotNull(issueDate, nameof(IssueDate));
        TaxDate = taxDate;
        DeliveryDate = deliveryDate;
        TotalNetAmount = Check.NotNull(totalNetAmount, nameof(TotalNetAmount));
        TotalVatAmount = Check.NotNull(totalVatAmount, nameof(TotalVatAmount));
        TotalGrossAmount = Check.NotNull(totalGrossAmount, nameof(TotalGrossAmount));
        PaymentTerms = Check.Length(paymentTerms, nameof(PaymentTerms), 100);
        VatRate = vatRate ?? VatRate.None;
        VariableSymbol = Check.Length(variableSymbol, nameof(VariableSymbol), 20);
        ConstantSymbol = Check.Length(constantSymbol, nameof(ConstantSymbol), 10);
        SpecificSymbol = Check.Length(specificSymbol, nameof(SpecificSymbol), 20);
        Items = new Collection<InvoiceItem>();
    }
}