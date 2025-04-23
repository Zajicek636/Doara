using System;
using Doara.Ucetnictvi.Constants;
using Doara.Ucetnictvi.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Doara.Ucetnictvi.Entities;

public class InvoiceItem : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
{
    public virtual bool IsDeleted { get; private set; }
    public virtual Guid? TenantId { get; private set; }
    public virtual Guid InvoiceId { get; private set; }
    public virtual Invoice Invoice { get; private set; }
    public virtual string Description { get; private set; }
    public virtual decimal Quantity { get; private set; }
    public virtual decimal UnitPrice { get; private set; }
    public virtual decimal NetAmount { get; private set; }
    public virtual VatRate VatRate { get; private set; }
    public virtual decimal VatAmount { get; private set; }
    public virtual decimal GrossAmount { get; private set; }

    public InvoiceItem(Guid id, Guid invoiceId, string description, decimal quantity, decimal unitPrice,
        decimal netAmount, VatRate? vatRate, decimal vatAmount, decimal grossAmount) :
        this(id, invoiceId, description, quantity, unitPrice, netAmount, vatRate ?? VatRate.None, vatAmount,
            grossAmount)
    {
    }

    public InvoiceItem(Guid id, Guid invoiceId, string description, decimal quantity, decimal unitPrice, decimal netAmount, VatRate vatRate, decimal vatAmount, decimal grossAmount) : base(id)
    {
        SetInvoice(invoiceId)
            .SetDescription(description)
            .SetQuantity(quantity)
            .SetUnitPrice(unitPrice)
            .SetNetAmount(netAmount)
            .SetVatRate(vatRate)
            .SetVatAmount(vatAmount)
            .SetGrossAmount(grossAmount);
    }

    public InvoiceItem SetInvoice(Guid invoiceId)
    {
        InvoiceId = Check.NotNull(invoiceId, nameof(InvoiceId));
        return this;
    }
    
    public InvoiceItem SetDescription(string description)
    {
        Description = Check.NotNullOrWhiteSpace(description, nameof(Description), InvoiceItemConstants.MaxDescriptionLength);
        return this;
    }
    
    public InvoiceItem SetQuantity(decimal quantity)
    {
        Quantity = Check.Range(quantity, nameof(Quantity), InvoiceItemConstants.MinQuantity);
        return this;
    }
    
    public InvoiceItem SetUnitPrice(decimal unitPrice)
    {
        UnitPrice = Check.NotNull(unitPrice, nameof(UnitPrice));
        return this;
    }
    
    public InvoiceItem SetNetAmount(decimal netAmount)
    {
        NetAmount = Check.NotNull(netAmount, nameof(NetAmount));
        return this;
    }
    
    public InvoiceItem SetVatRate(VatRate? vatRate)
    {
        VatRate = vatRate ?? VatRate.None;
        return this;
    }
    
    public InvoiceItem SetVatAmount(decimal vatAmount)
    {
        VatAmount = Check.NotNull(vatAmount, nameof(VatAmount));
        return this;
    }
    
    public InvoiceItem SetGrossAmount(decimal grossAmount)
    {
        GrossAmount = Check.NotNull(grossAmount, nameof(GrossAmount));
        return this;
    }
    
    public InvoiceItem GetCopy()
    {
        return (InvoiceItem)MemberwiseClone();
    }
}