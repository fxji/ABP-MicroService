using AAA.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace AAA.Permissions
{
    public class AAAPermissionDefinitionProvider: PermissionDefinitionProvider
    {

        public override void Define(IPermissionDefinitionContext context)
        {
            var AAA = context.AddGroup(AAAPermissions.AAA, L("AAA"), MultiTenancySides.Both);

            var A3Member = AAA.AddPermission(AAAPermissions.A3Member.Default, L("A3Member"));
            A3Member.AddChild(AAAPermissions.A3Member.Update, L("Edit"));
            A3Member.AddChild(AAAPermissions.A3Member.Delete, L("Delete"));
            A3Member.AddChild(AAAPermissions.A3Member.Create, L("Create"));

            //Code generation...
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AAAResource>(name);
        }
    }
}
