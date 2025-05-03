using System;
using Doara.Ucetnictvi.Dto.InvoiceItem;
using Doara.Ucetnictvi.Enums;

namespace Doara.Ucetnictvi.Utils.Converters;

public static partial class Converter
{
    public const string DefaultInvoiceItemDescription = "InvoiceItem Description";
    public const decimal DefaultInvoiceItemQuantity = 1;
    public const decimal DefaultInvoiceItemUnitPrice = 2;
    public const decimal DefaultInvoiceItemNetAmount = 3;
    public const VatRate DefaultInvoiceItemVatRate = VatRate.Reduced12;
    public const decimal DefaultInvoiceItemVatAmount = 5;
    public const decimal DefaultInvoiceItemGrossAmount = 8;
    
    public static InvoiceItemManageManyDto CreateInvoiceItemManageMany(Guid? id = null, Guid? containerItemId = null, string description = DefaultInvoiceItemDescription, 
        decimal quantity = DefaultInvoiceItemQuantity, decimal unitPrice = DefaultInvoiceItemUnitPrice, 
        decimal netAmount = DefaultInvoiceItemNetAmount, VatRate? vatRate = DefaultInvoiceItemVatRate,
        decimal vatAmount = DefaultInvoiceItemVatAmount, decimal grossAmount = DefaultInvoiceItemGrossAmount)
    {
        return new InvoiceItemManageManyDto
        {
            Id = id,
            Description = description,
            Quantity = quantity,
            UnitPrice = unitPrice,
            NetAmount = netAmount,
            VatRate = vatRate,
            VatAmount = vatAmount,
            GrossAmount = grossAmount,
            ContainerItemId = containerItemId
        };
    }
}