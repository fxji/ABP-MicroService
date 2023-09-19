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

    public static class Issue
    {
        public const string Default = GroupName + ".Issue";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class ContainmentAction
    {
        public const string Default = GroupName + ".ContainmentAction";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class RiskAssessment
    {
        public const string Default = GroupName + ".RiskAssessment";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class Cause
    {
        public const string Default = GroupName + ".Cause";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static class CorrectiveAction
    {
        public const string Default = GroupName + ".CorrectiveAction";
        public const string Delete = Default + ".Delete";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(AAAPermissions));
    }
}
