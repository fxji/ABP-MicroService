using FileStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace FileStore.Permissions;

public class FileStorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(FileStorePermissions.GroupName, L("Permission:FileStore"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<FileStoreResource>(name);
    }
}
