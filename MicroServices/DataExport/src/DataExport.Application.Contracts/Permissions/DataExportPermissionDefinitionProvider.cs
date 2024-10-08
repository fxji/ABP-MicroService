using DataExport.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace DataExport.Permissions;

public class DataExportPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(DataExportPermissions.GroupName, L("Permission:DataExport"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DataExportResource>(name);
    }
}
