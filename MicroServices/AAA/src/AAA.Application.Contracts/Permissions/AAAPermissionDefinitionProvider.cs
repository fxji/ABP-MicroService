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

            var ContainmentAction = AAA.AddPermission(AAAPermissions.ContainmentAction.Default, L("ContainmentAction"));
            ContainmentAction.AddChild(AAAPermissions.ContainmentAction.Update, L("Edit"));
            ContainmentAction.AddChild(AAAPermissions.ContainmentAction.Delete, L("Delete"));
            ContainmentAction.AddChild(AAAPermissions.ContainmentAction.Create, L("Create"));

            var RiskAssessment = AAA.AddPermission(AAAPermissions.RiskAssessment.Default, L("RiskAssessment"));
            RiskAssessment.AddChild(AAAPermissions.RiskAssessment.Update, L("Edit"));
            RiskAssessment.AddChild(AAAPermissions.RiskAssessment.Delete, L("Delete"));
            RiskAssessment.AddChild(AAAPermissions.RiskAssessment.Create, L("Create"));

            var Cause = AAA.AddPermission(AAAPermissions.Cause.Default, L("Cause"));
            Cause.AddChild(AAAPermissions.Cause.Update, L("Edit"));
            Cause.AddChild(AAAPermissions.Cause.Delete, L("Delete"));
            Cause.AddChild(AAAPermissions.Cause.Create, L("Create"));

            var CorrectiveAction = AAA.AddPermission(AAAPermissions.CorrectiveAction.Default, L("CorrectiveAction"));
            CorrectiveAction.AddChild(AAAPermissions.CorrectiveAction.Update, L("Edit"));
            CorrectiveAction.AddChild(AAAPermissions.CorrectiveAction.Delete, L("Delete"));
            CorrectiveAction.AddChild(AAAPermissions.CorrectiveAction.Create, L("Create"));
            //Code generation...
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AAAResource>(name);
        }
    }
}
