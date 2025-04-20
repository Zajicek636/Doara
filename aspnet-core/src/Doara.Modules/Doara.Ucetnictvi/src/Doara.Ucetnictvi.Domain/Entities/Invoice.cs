using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Doara.Ucetnictvi.Constants;
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

    public Invoice(Guid id, string invoiceNumber, Guid supplierId, Guid customerId, DateTime issueDate, DateTime? taxDate, DateTime? deliveryDate, decimal totalNetAmount, decimal totalVatAmount, decimal totalGrossAmount, string? paymentTerms, VatRate? vatRate, string? variableSymbol, string? constantSymbol, string? specificSymbol) :
        this(id, invoiceNumber, supplierId, customerId, issueDate, taxDate, deliveryDate, totalNetAmount, totalVatAmount, totalGrossAmount, paymentTerms, vatRate ?? VatRate.None, variableSymbol, constantSymbol, specificSymbol)
    {
    }
    
    protected Invoice(Guid id, string invoiceNumber, Guid supplierId, Guid customerId, DateTime issueDate, DateTime? taxDate, DateTime? deliveryDate, decimal totalNetAmount, decimal totalVatAmount, decimal totalGrossAmount, string? paymentTerms, VatRate vatRate, string? variableSymbol, string? constantSymbol, string? specificSymbol) : base(id)
    {
        SetInvoiceNumber(invoiceNumber)
            .SetSupplier(supplierId)
            .SetCustomer(customerId)
            .SetIssueDate(issueDate)
            .SetTaxDate(taxDate)
            .SetDeliveryDate(deliveryDate)
            .SetTotalNetAmount(totalNetAmount)
            .SetTotalVatAmount(totalVatAmount)
            .SetTotalGrossAmount(totalGrossAmount)
            .SetPaymentTerms(paymentTerms)
            .SetVatRate(vatRate)
            .SetVariableSymbol(variableSymbol)
            .SetConstantSymbol(constantSymbol)
            .SetSpecificSymbol(specificSymbol);
        Items = new Collection<InvoiceItem>();
    }

    public Invoice SetInvoiceNumber(string invoiceNumber)
    {
        InvoiceNumber = Check.NotNullOrWhiteSpace(invoiceNumber, nameof(InvoiceNumber), InvoiceConstants.MaxInvoiceNumberLength);
        return this;
    }
    
    public Invoice SetSupplier(Guid supplierId)
    {
        SupplierId = Check.NotNull(supplierId, nameof(SupplierId));
        return this;
    }
    
    public Invoice SetCustomer(Guid customerId)
    {
        CustomerId = Check.NotNull(customerId, nameof(CustomerId));
        return this;
    }
    
    public Invoice SetIssueDate(DateTime issueDate)
    {
        IssueDate = Check.NotNull(issueDate, nameof(IssueDate));
        return this;
    }
    
    public Invoice SetTaxDate(DateTime? taxDate)
    {
        TaxDate = taxDate;
        return this;
    }
    
    public Invoice SetDeliveryDate(DateTime? deliveryDate)
    {
        DeliveryDate = deliveryDate;
        return this;
    }
    
    public Invoice SetTotalNetAmount(decimal totalNetAmount)
    {
        TotalNetAmount = Check.NotNull(totalNetAmount, nameof(TotalNetAmount));
        return this;
    }
    
    public Invoice SetTotalVatAmount(decimal totalVatAmount)
    {
        TotalVatAmount = Check.NotNull(totalVatAmount, nameof(TotalVatAmount));
        return this;
    }
    
    public Invoice SetTotalGrossAmount(decimal totalGrossAmount)
    {
        TotalGrossAmount = Check.NotNull(totalGrossAmount, nameof(TotalGrossAmount));
        return this;
    }
    
    public Invoice SetPaymentTerms(string? paymentTerms)
    {
        PaymentTerms = Check.Length(paymentTerms, nameof(PaymentTerms), InvoiceConstants.MaxPaymentTermsLength);
        return this;
    }
    
    public Invoice SetVatRate(VatRate? vatRate)
    {
        VatRate = vatRate ?? VatRate.None;
        return this;
    }
    
    public Invoice SetVariableSymbol(string? variableSymbol)
    {
        VariableSymbol = Check.Length(variableSymbol, nameof(VariableSymbol), InvoiceConstants.MaxVariableSymbolLength);
        return this;
    }
    
    public Invoice SetConstantSymbol(string? constantSymbol)
    {
        ConstantSymbol = Check.Length(constantSymbol, nameof(ConstantSymbol), InvoiceConstants.MaxConstantSymbolLength);
        return this;
    }
    
    public Invoice SetSpecificSymbol(string? specificSymbol)
    {
        SpecificSymbol = Check.Length(specificSymbol, nameof(SpecificSymbol), InvoiceConstants.MaxSpecificSymbolLength);
        return this;
    }
}