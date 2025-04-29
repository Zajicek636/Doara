using System.Text.Json;

namespace TestHelper.FakeEntities;

/// <summary>
/// Třída <c>JsonConfig</c> poskytuje výchozí konfigurace pro JSON serializaci.
/// Obsahuje předdefinované možnosti, které lze použít pro standardizované nastavení serializace JSON v testech.
/// </summary>
public static class JsonConfig
{
    /// <summary>
    /// Výchozí nastavení serializace JSON.
    /// Umožňuje serializaci polí a používá volnější pravidla pro kódování znaků,
    /// která umožňují speciální znaky bez nutnosti explicitního escapování.
    /// </summary>
    public static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new()
    {
        IncludeFields = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
}