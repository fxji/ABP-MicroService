using AAA.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace AAA.Permissions
{
    public class AAAPermissionDefinitionProvider : PermissionDefinitionProvider
    {

        public override void Define(IPermissionDefinitionContext context)
        {
            var AAA = context.AddGroup(AAAPermissions.GroupName, L("AAA"));

            var A3 = AAA.AddPermission(AAAPermissions.A3.Default, L("A3"));
            A3.AddChild(AAAPermissions.A3.Update, L("Edit"));
            A3.AddChild(AAAPermissions.A3.Delete, L("Delete"));
            A3.AddChild(AAAPermissions.A3.Create, L("Create"));


            var Issue = AAA.AddPermission(AAAPermissions.Issue.Default, L("Issue"));
            Issue.AddChild(AAAPermissions.Issue.Update, L("Edit"));
            Issue.AddChild(AAAPermissions.Issue.Delete, L("Delete"));
            Issue.AddChild(AAAPermissions.Issue.Create, L("Create"));
            //Code generation...
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AAAResource>(name);
        }
    }
}
