using PeBusiness.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace PeBusiness.Permissions;

public class PeBusinessPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(PeBusinessPermissions.GroupName, L("Permission:PeBusiness"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<PeBusinessResource>(name);
    }
}
