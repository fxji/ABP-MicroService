using Volo.Abp.Reflection;

namespace DataExport.Permissions;

public class DataExportPermissions
{
    public const string GroupName = "DataExport";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(DataExportPermissions));
    }
}
