using Volo.Abp.Reflection;

namespace FileStore.Permissions;

public class FileStorePermissions
{
    public const string GroupName = "FileStore";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(FileStorePermissions));
    }
}
