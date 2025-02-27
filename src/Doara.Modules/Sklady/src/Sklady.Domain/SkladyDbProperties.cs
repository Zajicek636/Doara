namespace Sklady;

public static class SkladyDbProperties
{
    public static string DbTablePrefix { get; set; } = "Sklady";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Sklady";
}
