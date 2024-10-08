namespace DataExport;

public static class DataExportDbProperties
{
    public static string DbTablePrefix { get; set; } = "DataExport";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "DataExport";
}
