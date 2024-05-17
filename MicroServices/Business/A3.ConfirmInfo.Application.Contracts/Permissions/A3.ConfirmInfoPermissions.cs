using System;
using System.Collections.Generic;
using System.Text;

namespace A3.ConfirmInfo.Permissions
{
    public static class A3.ConfirmInfoPermissions
    {
        public const string A3.ConfirmInfo = "A3.ConfirmInfo";

        public static class ConfirmInfo
        {
            public const string Default = A3.ConfirmInfo + ".ConfirmInfo";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        //Code generation...
    }
}
