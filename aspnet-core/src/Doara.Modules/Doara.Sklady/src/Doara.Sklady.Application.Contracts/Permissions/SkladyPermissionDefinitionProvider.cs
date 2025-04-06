using Doara.Sklady.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Doara.Sklady.Permissions;

public class SkladyPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SkladyPermissions.GroupName, L("Permission:Sklady"));
        var container = myGroup.AddPermission(SkladyPermissions.ContainerPermission, L("Permission:Container"));
        container.AddChild(SkladyPermissions.CreateContainerPermission, L("Permission:Container:Create"));
        container.AddChild(SkladyPermissions.ReadContainerPermission, L("Permission:Container:Read"));
        container.AddChild(SkladyPermissions.UpdateContainerPermission, L("Permission:Container:Update"));
        container.AddChild(SkladyPermissions.DeleteContainerPermission, L("Permission:Container:Delete"));
        
        var containerItem = myGroup.AddPermission(SkladyPermissions.ContainerItemPermission, L("Permission:ContainerItem"));
        containerItem.AddChild(SkladyPermissions.CreateContainerItemPermission, L("Permission:ContainerItem:Create"));
        containerItem.AddChild(SkladyPermissions.ReadContainerItemPermission, L("Permission:ContainerItem:Read"));
        containerItem.AddChild(SkladyPermissions.UpdateContainerItemPermission, L("Permission:ContainerItem:Update"));
        containerItem.AddChild(SkladyPermissions.DeleteContainerItemPermission, L("Permission:ContainerItem:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SkladyResource>(name);
    }
}
