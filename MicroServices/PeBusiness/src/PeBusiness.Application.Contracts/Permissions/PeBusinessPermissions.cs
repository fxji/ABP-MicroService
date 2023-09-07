using Volo.Abp.Reflection;

namespace PeBusiness.Permissions;

public class PeBusinessPermissions
{
    public const string GroupName = "PeBusiness";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(PeBusinessPermissions));
    }
}
