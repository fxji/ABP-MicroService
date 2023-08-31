using Volo.Abp.Reflection;

namespace AAA.Permissions;

public class AAAPermissions
{
    public const string GroupName = "AAA";

    public static class A3
    {
        public const string Default = GroupName + ".A3";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(AAAPermissions));
    }
}
