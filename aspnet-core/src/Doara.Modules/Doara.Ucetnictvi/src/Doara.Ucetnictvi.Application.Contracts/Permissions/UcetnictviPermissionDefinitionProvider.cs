using Doara.Ucetnictvi.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Doara.Ucetnictvi.Permissions;

public class UcetnictviPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(UcetnictviPermissions.GroupName, L("Permission:Ucetnictvi"));
        
        var country = myGroup.AddPermission(UcetnictviPermissions.CountryPermission, L("Permission:Country"));
        country.AddChild(UcetnictviPermissions.CreateCountryPermission, L("Permission:Country:Create"));
        country.AddChild(UcetnictviPermissions.ReadCountryPermission, L("Permission:Country:Read"));
        country.AddChild(UcetnictviPermissions.UpdateCountryPermission, L("Permission:Country:Update"));
        country.AddChild(UcetnictviPermissions.DeleteCountryPermission, L("Permission:Country:Delete"));
        
        var address = myGroup.AddPermission(UcetnictviPermissions.AddressPermission, L("Permission:Address"));
        address.AddChild(UcetnictviPermissions.CreateAddressPermission, L("Permission:Address:Create"));
        address.AddChild(UcetnictviPermissions.ReadAddressPermission, L("Permission:Address:Read"));
        address.AddChild(UcetnictviPermissions.UpdateAddressPermission, L("Permission:Address:Update"));
        address.AddChild(UcetnictviPermissions.DeleteAddressPermission, L("Permission:Address:Delete"));
        
        var subject = myGroup.AddPermission(UcetnictviPermissions.SubjectPermission, L("Permission:Subject"));
        subject.AddChild(UcetnictviPermissions.CreateSubjectPermission, L("Permission:Subject:Create"));
        subject.AddChild(UcetnictviPermissions.ReadSubjectPermission, L("Permission:Subject:Read"));
        subject.AddChild(UcetnictviPermissions.UpdateSubjectPermission, L("Permission:Subject:Update"));
        subject.AddChild(UcetnictviPermissions.DeleteSubjectPermission, L("Permission:Subject:Delete"));
        
        var invoiceItem = myGroup.AddPermission(UcetnictviPermissions.InvoiceItemPermission, L("Permission:InvoiceItem"));
        invoiceItem.AddChild(UcetnictviPermissions.CreateInvoiceItemPermission, L("Permission:InvoiceItem:Create"));
        invoiceItem.AddChild(UcetnictviPermissions.ReadInvoiceItemPermission, L("Permission:InvoiceItem:Read"));
        invoiceItem.AddChild(UcetnictviPermissions.UpdateInvoiceItemPermission, L("Permission:InvoiceItem:Update"));
        invoiceItem.AddChild(UcetnictviPermissions.DeleteInvoiceItemPermission, L("Permission:InvoiceItem:Delete"));
        
        var invoice = myGroup.AddPermission(UcetnictviPermissions.InvoicePermission, L("Permission:Invoice"));
        invoice.AddChild(UcetnictviPermissions.CreateInvoicePermission, L("Permission:Invoice:Create"));
        invoice.AddChild(UcetnictviPermissions.ReadInvoicePermission, L("Permission:Invoice:Read"));
        invoice.AddChild(UcetnictviPermissions.UpdateInvoicePermission, L("Permission:Invoice:Update"));
        invoice.AddChild(UcetnictviPermissions.DeleteInvoicePermission, L("Permission:Invoice:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<UcetnictviResource>(name);
    }
}
