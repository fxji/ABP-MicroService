using PeBusiness.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace PeBusiness.Permissions
{
    public class PeBusinessPermissionDefinitionProvider: PermissionDefinitionProvider
    {

        public override void Define(IPermissionDefinitionContext context)
        {
            var PeBusiness = context.AddGroup(PeBusinessPermissions.PeBusiness, L("PeBusiness"));

            var PeTask = PeBusiness.AddPermission(PeBusinessPermissions.PeTask.Default, L("PeTask"));
            PeTask.AddChild(PeBusinessPermissions.PeTask.Update, L("Edit"));
            PeTask.AddChild(PeBusinessPermissions.PeTask.Delete, L("Delete"));
            PeTask.AddChild(PeBusinessPermissions.PeTask.Create, L("Create"));

            //Code generation...
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<PeBusinessResource>(name);
        }
    }
}
