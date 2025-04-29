namespace Doara.Ucetnictvi;

public static class UcetnictviErrorCodes
{
    public const string CustomerDoesNotExist = "Ucetnictvi:010000";
    public const string SupplierDoesNotExist = "Ucetnictvi:010001";
    public const string SupplierIsSameAsCustomer = "Ucetnictvi:010002";
    
    public const string InvoiceItemCreateGeneralError = "Ucetnictvi:020000";
    public const string InvoiceItemUpdateGeneralError = "Ucetnictvi:020001";
    public const string InvoiceItemNotExistInInvoice = "Ucetnictvi:020002";
}
