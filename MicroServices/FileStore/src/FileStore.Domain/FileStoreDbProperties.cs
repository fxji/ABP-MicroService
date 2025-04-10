namespace FileStore;

public static class FileStoreDbProperties
{
    public static string DbTablePrefix { get; set; } = "FileStore";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "FileStorage";
}
