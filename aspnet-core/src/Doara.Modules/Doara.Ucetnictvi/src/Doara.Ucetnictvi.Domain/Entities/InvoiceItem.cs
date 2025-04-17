using System;
using Doara.Ucetnictvi.Constants;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Doara.Ucetnictvi.Entities;

public class InvoiceItem : AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
{
    public virtual bool IsDeleted { get; }
    public virtual Guid? TenantId { get; }
    public virtual Guid InvoiceId { get; private set; }
    public virtual Invoice Invoice { get; }
    public virtual string Description { get; private set; }
    public virtual decimal Quantity { get; private set; }
    public virtual decimal UnitPrice { get; private set; }
    public virtual decimal NetAmount { get; private set; }
    public virtual decimal VatRate { get; private set; }
    public virtual decimal VatAmount { get; private set; }
    public virtual decimal GrossAmount { get; private set; }

    public InvoiceItem(Guid id, Guid invoiceId, string description, decimal quantity, decimal unitPrice, decimal netAmount, decimal vatRate, decimal vatAmount, decimal grossAmount) : base(id)
    {
        InvoiceId = Check.NotNull(invoiceId, nameof(InvoiceId));
        Description = Check.NotNullOrWhiteSpace(description, nameof(Description), InvoiceItemConstants.MaxDescriptionLength);
        Quantity = Check.Range(quantity, nameof(Quantity), InvoiceItemConstants.MinQuantity);
        UnitPrice = Check.NotNull(unitPrice, nameof(UnitPrice));
        NetAmount = Check.NotNull(netAmount, nameof(NetAmount));
        VatRate = Check.Range(vatRate, nameof(VatRate), InvoiceItemConstants.MinVatRate);
        VatAmount = Check.NotNull(netAmount, nameof(VatAmount));
        GrossAmount = Check.NotNull(grossAmount, nameof(GrossAmount));
    }
}