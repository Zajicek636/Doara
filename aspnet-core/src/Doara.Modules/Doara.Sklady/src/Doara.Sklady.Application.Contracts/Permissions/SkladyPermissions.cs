using Volo.Abp.Reflection;

namespace Doara.Sklady.Permissions;

public class SkladyPermissions
{
    public const string GroupName = "Sklady";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(SkladyPermissions));
    }
}
