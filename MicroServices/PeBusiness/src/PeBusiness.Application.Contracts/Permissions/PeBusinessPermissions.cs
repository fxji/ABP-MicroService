using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Reflection;

namespace PeBusiness.Permissions
{
    public static class PeBusinessPermissions
    {
        public const string PeBusiness = "PeBusiness";

        public static class PeTask
        {
            public const string Default = PeBusiness + ".PeTask";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(PeBusinessPermissions));
        }

        //Code generation...
    }
}
