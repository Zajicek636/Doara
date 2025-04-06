using Volo.Abp.Reflection;

namespace Doara.Sklady.Permissions;

public class SkladyPermissions
{
    public const string GroupName = "Sklady";
    
    public const string ContainerPermission = $"{GroupName}.Container";
    public const string CreateContainerPermission = $"{ContainerPermission}.Create";
    public const string ReadContainerPermission = $"{ContainerPermission}.Read";
    public const string UpdateContainerPermission = $"{ContainerPermission}.Update";
    public const string DeleteContainerPermission = $"{ContainerPermission}.Delete";
    
    public const string ContainerItemPermission = $"{GroupName}.ContainerItem";
    public const string CreateContainerItemPermission = $"{ContainerItemPermission}.Create";
    public const string ReadContainerItemPermission = $"{ContainerItemPermission}.Read";
    public const string UpdateContainerItemPermission = $"{ContainerItemPermission}.Update";
    public const string DeleteContainerItemPermission = $"{ContainerItemPermission}.Delete";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(SkladyPermissions));
    }
}
