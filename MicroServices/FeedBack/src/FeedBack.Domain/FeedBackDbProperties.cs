namespace FeedBack;

public static class FeedBackDbProperties
{
    public static string DbTablePrefix { get; set; } = "FeedBack";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "FeedBack";
}
