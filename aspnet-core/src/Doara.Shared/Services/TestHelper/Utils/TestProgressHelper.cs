using TestHelper.FakeEntities;

namespace TestHelper.Utils;

/// <summary>
/// Třída <c>TestProgressHelper</c> poskytuje rozšíření pro testovací entity (<c>IFakeEntity</c>), která umožňuje vykonávat akce s kontrolou,
/// zda mají vyvolat výjimku nebo ne. To je užitečné pro testování chybových stavů a ověření správného chování v testech.
/// </summary>
public static class TestProgressHelper
{
    /// <summary>
    /// Provede akci bez vyvolání výjimky, pokud je <paramref name="checkIfNotThrow"/> nastaveno na <c>false</c>.
    /// Pokud je <paramref name="checkIfNotThrow"/> nastaveno na <c>true</c>, akce se provede v rámci testu, který ověřuje, že 
    /// nevyvolá výjimku.
    /// </summary>
    /// <typeparam name="T">Typ originální entity, kterou reprezentuje testovací (fake) entita.</typeparam>
    /// <param name="data">Testovací entita implementující rozhraní <c>IFakeEntity</c>.</param>
    /// <param name="action">Akce, která má být provedena.</param>
    /// <param name="checkIfNotThrow">Určuje, zda se má akce provést bez kontroly vyvolání výjimky (<c>false</c>) nebo s kontrolou (<c>true</c>).</param>
    public static void DoActionWithNotThrowCheck<T>(this IFakeEntity<T> data, Action action, bool checkIfNotThrow)
    {
        if (!checkIfNotThrow)
        {
            action();
            return;
        }

        var exShould = new ExtendedShould<T>(data);
        exShould.NotThrow(action);
    }
    
    /// <summary>
    /// Provede akci s návratem hodnoty, a to buď bez vyvolání výjimky, pokud je <paramref name="checkIfNotThrow"/> nastaveno na <c>false</c>,
    /// nebo v rámci testu, který ověřuje, že akce nevyvolá výjimku, pokud je <paramref name="checkIfNotThrow"/> nastaveno na <c>true</c>.
    /// </summary>
    /// <typeparam name="T">Typ originální entity, kterou reprezentuje testovací (fake) entita.</typeparam>
    /// <param name="data">Testovací entita implementující rozhraní <c>IFakeEntity</c>.</param>
    /// <param name="action">Funkce, která vrací hodnotu, jež má být provedena.</param>
    /// <param name="checkIfNotThrow">Určuje, zda se má akce provést bez kontroly na výjimku (<c>false</c>) nebo s kontrolou (<c>true</c>).</param>
    /// <returns>Výsledek akce, pokud nevyvolá výjimku.</returns>
    public static T DoActionWithNotThrowCheck<T>(this IFakeEntity<T> data, Func<T> action, bool checkIfNotThrow)
    {
        if (!checkIfNotThrow)
        {
            return action();
        }

        var exShould = new ExtendedShould<T>(data);
        return exShould.NotThrow(action);
    }
}