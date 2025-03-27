using Doara.Ucetnictvi.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Doara.Ucetnictvi.Permissions;

public class UcetnictviPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(UcetnictviPermissions.GroupName, L("Permission:Ucetnictvi"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<UcetnictviResource>(name);
    }
}
