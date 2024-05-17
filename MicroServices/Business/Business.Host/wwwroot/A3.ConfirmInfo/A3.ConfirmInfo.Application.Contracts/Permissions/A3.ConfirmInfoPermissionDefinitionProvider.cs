using A3.ConfirmInfo.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace A3.ConfirmInfo.Permissions
{
    public class A3.ConfirmInfoPermissionDefinitionProvider: PermissionDefinitionProvider
    {

        public override void Define(IPermissionDefinitionContext context)
        {
            var A3.ConfirmInfo = context.AddGroup(A3.ConfirmInfoPermissions.A3.ConfirmInfo, L("A3.ConfirmInfo"), MultiTenancySides.Both);

            var ConfirmInfo = A3.ConfirmInfo.AddPermission(A3.ConfirmInfoPermissions.ConfirmInfo.Default, L("ConfirmInfo"));
            ConfirmInfo.AddChild(A3.ConfirmInfoPermissions.ConfirmInfo.Update, L("Edit"));
            ConfirmInfo.AddChild(A3.ConfirmInfoPermissions.ConfirmInfo.Delete, L("Delete"));
            ConfirmInfo.AddChild(A3.ConfirmInfoPermissions.ConfirmInfo.Create, L("Create"));

            //Code generation...
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<A3.ConfirmInfoResource>(name);
        }
    }
}
