using Sklady.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Sklady.Permissions;

public class SkladyPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SkladyPermissions.GroupName, L("Permission:Sklady"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SkladyResource>(name);
    }
}
