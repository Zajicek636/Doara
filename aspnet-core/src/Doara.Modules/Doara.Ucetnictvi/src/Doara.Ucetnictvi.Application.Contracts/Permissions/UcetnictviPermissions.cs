using Volo.Abp.Reflection;

namespace Doara.Ucetnictvi.Permissions;

public class UcetnictviPermissions
{
    public const string GroupName = "Ucetnictvi";
    
    public const string AddressPermission = $"{GroupName}.Address";
    public const string CreateAddressPermission = $"{AddressPermission}.Create";
    public const string ReadAddressPermission = $"{AddressPermission}.Read";
    public const string UpdateAddressPermission = $"{AddressPermission}.Update";
    public const string DeleteAddressPermission = $"{AddressPermission}.Delete";
    
    public const string CountryPermission = $"{GroupName}.Country";
    public const string CreateCountryPermission = $"{CountryPermission}.Create";
    public const string ReadCountryPermission = $"{CountryPermission}.Read";
    public const string UpdateCountryPermission = $"{CountryPermission}.Update";
    public const string DeleteCountryPermission = $"{CountryPermission}.Delete";

    public const string SubjectPermission = $"{GroupName}.Subject";
    public const string CreateSubjectPermission = $"{SubjectPermission}.Create";
    public const string ReadSubjectPermission = $"{SubjectPermission}.Read";
    public const string UpdateSubjectPermission = $"{SubjectPermission}.Update";
    public const string DeleteSubjectPermission = $"{SubjectPermission}.Delete";
    
    public const string InvoiceItemPermission = $"{GroupName}.InvoiceItem";
    public const string CreateInvoiceItemPermission = $"{InvoiceItemPermission}.Create";
    public const string ReadInvoiceItemPermission = $"{InvoiceItemPermission}.Read";
    public const string UpdateInvoiceItemPermission = $"{InvoiceItemPermission}.Update";
    public const string DeleteInvoiceItemPermission = $"{InvoiceItemPermission}.Delete";
    
    public const string InvoicePermission = $"{GroupName}.Invoice";
    public const string CreateInvoicePermission = $"{InvoicePermission}.Create";
    public const string ReadInvoicePermission = $"{InvoicePermission}.Read";
    public const string UpdateInvoicePermission = $"{InvoicePermission}.Update";
    public const string DeleteInvoicePermission = $"{InvoicePermission}.Delete";
    
    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(UcetnictviPermissions));
    }
}
