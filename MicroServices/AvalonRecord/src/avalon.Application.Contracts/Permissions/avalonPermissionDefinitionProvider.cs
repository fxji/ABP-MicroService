using avalon.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace avalon.Permissions;

public class avalonPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(avalonPermissions.GroupName, L("Permission:avalon"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<avalonResource>(name);
    }
}
