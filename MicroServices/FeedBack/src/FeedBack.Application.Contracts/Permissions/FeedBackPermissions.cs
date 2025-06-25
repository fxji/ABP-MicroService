using Volo.Abp.Reflection;

namespace FeedBack.Permissions;

public class FeedBackPermissions
{
    public const string FeedBack = "FeedBack";


    public static class ProgramInfo
    {
        public const string Default = FeedBack + ".ProgramInfo";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class ShapeInfo
    {
        public const string Default = FeedBack + ".ShapeInfo";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }


    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(FeedBackPermissions));
    }
}
