using System;
using System.Collections.Generic;
using System.Text;

namespace AAA.Permissions
{
    public static class AAAPermissions
    {
        public const string AAA = "AAA";

        public static class A3Member
        {
            public const string Default = AAA + ".A3Member";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        //Code generation...
    }
}
