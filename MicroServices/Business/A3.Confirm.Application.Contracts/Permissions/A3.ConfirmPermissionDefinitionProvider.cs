using A3.Confirm.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace A3.Confirm.Permissions
{
    public class A3.ConfirmPermissionDefinitionProvider: PermissionDefinitionProvider
    {

        public override void Define(IPermissionDefinitionContext context)
        {
            var A3.Confirm = context.AddGroup(A3.ConfirmPermissions.A3.Confirm, L("A3.Confirm"), MultiTenancySides.Both);

            var Confirm = A3.Confirm.AddPermission(A3.ConfirmPermissions.Confirm.Default, L("Confirm"));
            Confirm.AddChild(A3.ConfirmPermissions.Confirm.Update, L("Edit"));
            Confirm.AddChild(A3.ConfirmPermissions.Confirm.Delete, L("Delete"));
            Confirm.AddChild(A3.ConfirmPermissions.Confirm.Create, L("Create"));

            var Confirm = A3.Confirm.AddPermission(A3.ConfirmPermissions.Confirm.Default, L("Confirm"));
            Confirm.AddChild(A3.ConfirmPermissions.Confirm.Update, L("Edit"));
            Confirm.AddChild(A3.ConfirmPermissions.Confirm.Delete, L("Delete"));
            Confirm.AddChild(A3.ConfirmPermissions.Confirm.Create, L("Create"));

            //Code generation...
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<A3.ConfirmResource>(name);
        }
    }
}
