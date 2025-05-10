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
    public virtual bool IsDeleted { get; private set; }
    public virtual Guid? TenantId { get; private set; }
    public virtual string InvoiceNumber { get; private set; }
    public virtual Guid SupplierId { get; private set; }
    public virtual Subject Supplier { get; private set; }// Dodavatel (plátce DPH)
    public virtual Guid CustomerId { get; private set; }
    public virtual Subject Customer { get; private set; }// Odběratel
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
    public virtual InvoiceType InvoiceType { get; private set; }
    public virtual ICollection<InvoiceItem> Items { get; private set; }// Položky faktury

    public Invoice(Guid id, InvoiceType invoiceType, string invoiceNumber, Guid supplierId, Guid customerId, DateTime issueDate, DateTime? taxDate, DateTime? deliveryDate, decimal totalNetAmount, decimal totalVatAmount, decimal totalGrossAmount, string? paymentTerms, VatRate? vatRate, string? variableSymbol, string? constantSymbol, string? specificSymbol) :
        this(id, invoiceType, invoiceNumber, supplierId, customerId, issueDate, taxDate, deliveryDate, totalNetAmount, totalVatAmount, totalGrossAmount, paymentTerms, vatRate ?? VatRate.None, variableSymbol, constantSymbol, specificSymbol)
    {
    }
    
    protected Invoice(Guid id, InvoiceType invoiceType, string invoiceNumber, Guid supplierId, Guid customerId, DateTime issueDate, DateTime? taxDate, DateTime? deliveryDate, decimal totalNetAmount, decimal totalVatAmount, decimal totalGrossAmount, string? paymentTerms, VatRate vatRate, string? variableSymbol, string? constantSymbol, string? specificSymbol) : base(id)
    {
        EnsureNotCompleted();
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
            .SetSpecificSymbol(specificSymbol)
            .SetInvoiceType(invoiceType);
        Items = new Collection<InvoiceItem>();
    }

    public void EnsureNotCompleted()
    {
        if (InvoiceType == InvoiceType.FinalInvoice)
        {
            throw new BusinessException(UcetnictviErrorCodes.InvoiceCompletedCannotModifyOrDelete);
        }
    }

    public Invoice SetInvoiceType(InvoiceType invoiceType)
    {
        EnsureNotCompleted();
        InvoiceType = invoiceType;
        return this;
    }

    public Invoice SetInvoiceNumber(string invoiceNumber)
    {
        EnsureNotCompleted();
        InvoiceNumber = Check.NotNullOrWhiteSpace(invoiceNumber, nameof(InvoiceNumber), InvoiceConstants.MaxInvoiceNumberLength);
        return this;
    }
    
    public Invoice SetSupplier(Guid supplierId)
    {
        EnsureNotCompleted();
        SupplierId = Check.NotNull(supplierId, nameof(SupplierId));
        return this;
    }
    
    public Invoice SetSupplier(Subject supplier)
    {
        EnsureNotCompleted();
        Supplier = supplier;
        return SetSupplier(supplier.Id);
    }
    
    public Invoice SetCustomer(Guid customerId)
    {
        EnsureNotCompleted();
        CustomerId = Check.NotNull(customerId, nameof(CustomerId));
        return this;
    }
    
    public Invoice SetCustomer(Subject customer)
    {
        EnsureNotCompleted();
        Customer = customer;
        return SetCustomer(customer.Id);
    }
    
    public Invoice SetIssueDate(DateTime issueDate)
    {
        EnsureNotCompleted();
        IssueDate = Check.NotNull(issueDate, nameof(IssueDate));
        return this;
    }
    
    public Invoice SetTaxDate(DateTime? taxDate)
    {
        EnsureNotCompleted();
        TaxDate = taxDate;
        return this;
    }
    
    public Invoice SetDeliveryDate(DateTime? deliveryDate)
    {
        EnsureNotCompleted();
        DeliveryDate = deliveryDate;
        return this;
    }
    
    public Invoice SetTotalNetAmount(decimal totalNetAmount)
    {
        EnsureNotCompleted();
        TotalNetAmount = Check.NotNull(totalNetAmount, nameof(TotalNetAmount));
        return this;
    }
    
    public Invoice SetTotalVatAmount(decimal totalVatAmount)
    {
        EnsureNotCompleted();
        TotalVatAmount = Check.NotNull(totalVatAmount, nameof(TotalVatAmount));
        return this;
    }
    
    public Invoice SetTotalGrossAmount(decimal totalGrossAmount)
    {
        EnsureNotCompleted();
        TotalGrossAmount = Check.NotNull(totalGrossAmount, nameof(TotalGrossAmount));
        return this;
    }
    
    public Invoice SetPaymentTerms(string? paymentTerms)
    {
        EnsureNotCompleted();
        PaymentTerms = Check.Length(paymentTerms, nameof(PaymentTerms), InvoiceConstants.MaxPaymentTermsLength);
        return this;
    }
    
    public Invoice SetVatRate(VatRate? vatRate)
    {
        EnsureNotCompleted();
        VatRate = vatRate ?? VatRate.None;
        return this;
    }
    
    public Invoice SetVariableSymbol(string? variableSymbol)
    {
        EnsureNotCompleted();
        VariableSymbol = Check.Length(variableSymbol, nameof(VariableSymbol), InvoiceConstants.MaxVariableSymbolLength);
        return this;
    }
    
    public Invoice SetConstantSymbol(string? constantSymbol)
    {
        EnsureNotCompleted();
        ConstantSymbol = Check.Length(constantSymbol, nameof(ConstantSymbol), InvoiceConstants.MaxConstantSymbolLength);
        return this;
    }
    
    public Invoice SetSpecificSymbol(string? specificSymbol)
    {
        EnsureNotCompleted();
        SpecificSymbol = Check.Length(specificSymbol, nameof(SpecificSymbol), InvoiceConstants.MaxSpecificSymbolLength);
        return this;
    }
}