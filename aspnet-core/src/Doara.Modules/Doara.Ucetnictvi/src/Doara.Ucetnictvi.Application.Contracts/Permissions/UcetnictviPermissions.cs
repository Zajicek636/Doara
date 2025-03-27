using Volo.Abp.Reflection;

namespace Doara.Ucetnictvi.Permissions;

public class UcetnictviPermissions
{
    public const string GroupName = "Ucetnictvi";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(UcetnictviPermissions));
    }
}
