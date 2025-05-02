using System;
using Doara.Sklady.Enums;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Doara.Sklady.Entities;

public class StockMovement : FullAuditedEntity<Guid>
{
    public virtual Guid ContainerItemId { get; private set; }
    public virtual ContainerItem ContainerItem { get; private set; }
    public virtual decimal Quantity { get; private set; }

    public virtual MovementCategory MovementCategory { get; private set; }
    public virtual Guid? RelatedDocumentId { get; private set; }
    
    public StockMovement(Guid id, Guid containerItemId, decimal quantity, MovementCategory movementCategory, Guid? relatedDocId = null) : base(id)
    {
        SetMovementCategory(movementCategory).SetQuantity(quantity)
            .SetRelatedDocumentId(relatedDocId).SetContainerItem(containerItemId);
    }

    public StockMovement SetQuantity(decimal quantity)
    {
        Quantity = Check.Range(quantity, nameof(Quantity), 0);
        return this;
    }
    
    public StockMovement SetMovementCategory(MovementCategory movementCategory)
    {
        MovementCategory = movementCategory;
        return this;
    }

    public StockMovement SetRelatedDocumentId(Guid? relatedDocumentId)
    {
        RelatedDocumentId = relatedDocumentId;
        return this;
    }

    public StockMovement SetContainerItem(Guid containerItemId)
    {
        ContainerItemId = containerItemId;
        return this;
    }

    public StockMovement SetContainerItem(ContainerItem containerItem)
    {
        ContainerItem = containerItem;
        return SetContainerItem(containerItem.Id);
    }
    
    protected StockMovement() { }
}