namespace TestHelper.FakeEntities;

/// <summary>
/// Rozhraní <c>IFakeEntity</c> definuje kontrakt pro testovací (fake) entity.
/// Slouží k usnadnění serializace, porovnávání a ověřování shodnosti mezi testovací a původní entitou v testovacím prostředí.
/// </summary>
/// <typeparam name="TOriginal">Typ skutečné (původní) entity, kterou tato fake entita reprezentuje pro účely testování.</typeparam>
public interface IFakeEntity<TOriginal>
{
    /// <summary>
    /// Serializuje aktuální testovací entitu do formátu JSON, což umožňuje její zalogovaní v případě chyby.
    /// </summary>
    /// <returns>Řetězec ve formátu JSON reprezentující aktuální stav fake entity.</returns>
    string SerializeToJson();
    
    /// <summary>
    /// Vytvoří původní (originální) instanci entity typu <typeparamref name="TOriginal"/> na základě aktuální testovací entity.
    /// Používá se ke kontrole stavu nebo porovnání mezi fake a originální entitou.
    /// </summary>
    /// <param name="checkIfNotThrow">
    /// Určuje, zda má v případě chyby při vytváření originální entity vyvolaná chyba "vybublat".
    /// Pokud je nastaveno na <c>true</c>, chyba se vyvolá a test může zachytit chybový stav.
    /// Pokud je nastaveno na <c>false</c>, chyba je potlačena a test pokračuje bez přerušení.
    /// </param>
    /// <returns>Původní instance typu <typeparamref name="TOriginal"/>.</returns>
    TOriginal CreateOriginalEntity(bool checkIfNotThrow = true);
    
    /// <summary>
    /// Porovná aktuální testovací entitu s danou původní entitou typu <typeparamref name="TOriginal"/> a ověří jejich shodnost.
    /// Používá se k potvrzení, že fake entita odpovídá originální.
    /// </summary>
    /// <param name="entity">Původní entita, se kterou se provádí srovnání.</param>
    void CheckIfSame(TOriginal entity);
}