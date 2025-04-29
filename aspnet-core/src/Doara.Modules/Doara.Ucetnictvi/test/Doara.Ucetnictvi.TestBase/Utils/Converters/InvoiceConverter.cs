using System;
using Doara.Ucetnictvi.Dto.Invoice;
using Doara.Ucetnictvi.Enums;

namespace Doara.Ucetnictvi.Utils.Converters;

public static partial class Converter
{
    public const string DefaultInvoiceInvoiceNumber = "FV20250001";
    public static DateTime DefaultInvoiceIssueDate = new (2000, 1, 1);
    public static DateTime DefaultInvoiceTaxDate = new (2000, 1, 1);
    public static DateTime DefaultInvoiceDeliveryDate = new (2000, 1, 15);
    public const decimal DefaultInvoiceTotalNetAmount = 1;
    public const decimal DefaultInvoiceTotalVatAmount = 0.21M;
    public const decimal DefaultInvoiceTotalGrossAmount = 1.21M;
    public const string DefaultInvoicePaymentTerms = "Standard 21";
    public const VatRate DefaultInvoiceVatRate = VatRate.Standard21;
    public const string DefaultInvoiceVariableSymbol = "2104202501";
    public const string DefaultInvoiceConstantSymbol = "2104202502";
    public const string DefaultInvoiceSpecificSymbol = "2104202503";
    
    public static InvoiceCreateInputDto CreateInvoiceInput(Guid supplierId, Guid customerId, DateTime issueDate,
        DateTime? taxDate, DateTime? deliveryDate, string invoiceNumber = DefaultInvoiceInvoiceNumber, 
        decimal totalNetAmount = DefaultInvoiceTotalNetAmount, decimal totalVatAmount = DefaultInvoiceTotalVatAmount,
        decimal totalGrossAmount = DefaultInvoiceTotalGrossAmount, string paymentTerms = DefaultInvoicePaymentTerms,
        VatRate vatRate = DefaultInvoiceVatRate, string? variableSymbol = DefaultInvoiceVariableSymbol, 
        string? constantSymbol = DefaultInvoiceConstantSymbol, string? specificSymbol = DefaultInvoiceSpecificSymbol)
    {
        return new InvoiceCreateInputDto
        {
            InvoiceNumber = invoiceNumber,
            SupplierId = supplierId,
            CustomerId = customerId,
            IssueDate = issueDate,
            TaxDate = taxDate,
            DeliveryDate = deliveryDate,
            TotalNetAmount = totalNetAmount,
            TotalVatAmount = totalVatAmount,
            TotalGrossAmount = totalGrossAmount,
            PaymentTerms = paymentTerms,
            VatRate = vatRate,
            VariableSymbol = variableSymbol,
            ConstantSymbol = constantSymbol,
            SpecificSymbol = specificSymbol
        };
    }
    
    public static InvoiceUpdateInputDto Convert2UpdateInput(InvoiceDetailDto input)
    {
        return new InvoiceUpdateInputDto
        {
            InvoiceNumber = input.InvoiceNumber,
            SupplierId = input.Supplier.Id,
            CustomerId = input.Customer.Id,
            IssueDate = input.IssueDate,
            TaxDate = input.TaxDate,
            DeliveryDate = input.DeliveryDate,
            TotalNetAmount = input.TotalNetAmount,
            TotalVatAmount = input.TotalVatAmount,
            TotalGrossAmount = input.TotalGrossAmount,
            PaymentTerms = input.PaymentTerms,
            VatRate = input.VatRate,
            VariableSymbol = input.VariableSymbol,
            ConstantSymbol = input.ConstantSymbol,
            SpecificSymbol = input.SpecificSymbol,
            Id = input.Id
        };
    }
}