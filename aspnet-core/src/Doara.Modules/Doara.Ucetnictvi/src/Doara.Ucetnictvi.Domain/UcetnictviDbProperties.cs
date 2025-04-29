namespace Doara.Ucetnictvi;

public static class UcetnictviDbProperties
{
    public static string DbTablePrefix { get; set; } = "Ucetnictvi";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Ucetnictvi";
}
