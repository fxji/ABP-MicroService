using Volo.Abp.Reflection;

namespace avalon.Permissions;

public class avalonPermissions
{
    public const string GroupName = "avalon";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(avalonPermissions));
    }
}
