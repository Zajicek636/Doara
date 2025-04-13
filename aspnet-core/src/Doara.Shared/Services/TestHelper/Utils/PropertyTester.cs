using System.Reflection;
using TestHelper.FakeEntities;
using TestHelper.Generators;
using Volo.Abp;

namespace TestHelper.Utils;

/// <summary>
/// Třída <c>PropertyTester</c> poskytuje metody pro generování testovacích dat a ověřování chování testovaných entit v rámci různých scénářů.
/// Pomocí těchto metod lze generovat různá vstupní data pro testování vlastností entit, ať už pro testy řetězcových, číselných, desetinných nebo logických vlastností.
/// </summary>
public static class PropertyTester
{
    /// <summary>
    /// Generuje testovací data pro řetězcové vlastnosti.
    /// </summary>
    /// <param name="nullable">Určuje, zda testovaná vlastnost může být null.</param>
    /// <param name="maxLength">Maximální délka řetězce pro generovaná testovací data.</param>
    /// <returns>Seznam testovacích dat obsahujících různé hodnoty pro řetězcové vlastnosti.</returns>
    public static IEnumerable<object[]> GetStringPropertyTestData(bool nullable, int maxLength)
    {
        yield return [null!, !nullable]; // null hodnota, pokud nullable == false
        yield return [RandomGenerator.RandomAlpNum(maxLength), false]; // Řetězec s délkou <= maxLength
        yield return [RandomGenerator.RandomAlpNum(maxLength + 1), true]; // Řetězec s délkou > maxLength
    }
    
    public static IEnumerable<object[]> GetEnumPropertyTestData(Type e)
    {
        var values = Enum.GetValues(e);
        foreach (var value in values)
        {
            yield return [value, false];
        }
    }
    
    /// <summary>
    /// Generuje testovací data pro číselné vlastnosti.
    /// </summary>
    /// <param name="min">Minimální hodnota pro testování.</param>
    /// <param name="max">Maximální hodnota pro testování.</param>
    /// <returns>Seznam testovacích dat pro číselné vlastnosti.</returns>
    public static IEnumerable<object[]> GetNumberPropertyTestData(int min, int max)
    {
        yield return [min, false]; // Validní hodnota (min)
        yield return [max, false]; // Validní hodnota (max)
        yield return [min - 1, true]; // Hodnota menší než min (nevalidní)
        yield return [max + 1, true]; // Hodnota větší než max (nevalidní)
    }
    
    /// <summary>
    /// Generuje testovací data pro desetinné vlastnosti.
    /// </summary>
    /// <param name="integerPlace">Počet míst před desetinnou čárkou.</param>
    /// <param name="decimalPlace">Počet desetinných míst.</param>
    /// <returns>Seznam testovacích dat pro desetinné vlastnosti.</returns>
    public static IEnumerable<object[]> GetDecimalPropertyTestData(int integerPlace, int decimalPlace)
    {
        var min = -(decimal)Math.Pow(10, integerPlace) + (decimal)Math.Pow(10, -decimalPlace);
        var max = (decimal)Math.Pow(10, integerPlace) - (decimal)Math.Pow(10, -decimalPlace);
        var deviation = (decimal)Math.Pow(10, -decimalPlace);
        yield return [min - deviation, true]; // Hodnota menší než minimální (nevalidní)
        yield return [max + deviation, true]; // Hodnota větší než maximální (nevalidní)
        yield return [min, false]; // Validní minimální hodnota
        yield return [max, false]; // Validní maximální hodnota
    }

    /// <summary>
    /// Generuje testovací data pro validaci znaménka desetinných hodnot.
    /// Vrací hodnoty odpovídající nule, zápornému a kladnému číslu s určenou přesností.
    /// </summary>
    /// <param name="canBeNegative">Určuje, zda je záporná hodnota považována za validní.</param>
    /// <param name="canBeZero">Určuje, zda je nula považována za validní.</param>
    /// <param name="canBePositive">Určuje, zda je kladná hodnota považována za validní.</param>
    /// <param name="decimalPlace">Počet desetinných míst, které má mít testovací hodnota.</param>
    /// <returns>Sekvence dvojic: testovací hodnota a očekávaná informace, zda je nevalidní (true = nevalidní).</returns>
    public static IEnumerable<object[]> GetDecimalSignTestData(bool canBeNegative, bool canBeZero, bool canBePositive, int decimalPlace)
    {
        var deviation = (decimal)Math.Pow(10, -decimalPlace);
        yield return [0, !canBeZero];
        yield return [-deviation, !canBeNegative];
        yield return [deviation, !canBePositive];
    }

    /// <summary>
    /// Generuje testovací data pro logické vlastnosti.
    /// </summary>
    /// <param name="nullable">Určuje, zda je logická hodnota nullable.</param>
    /// <returns>Seznam testovacích dat pro logické vlastnosti.</returns>
    public static IEnumerable<object[]> GetBooleanPropertyTestData(bool nullable)
    {
        yield return [null!, !nullable];
        yield return [true, false];
        yield return [false, false];
    }

    /// <summary>
    /// Testuje vlastnost entit a ověřuje, zda se vytvořená entita shoduje s očekáváním.
    /// Pokud je <paramref name="shouldThrow"/> true, očekává se, že při volání metody bude vyvolána výjimka.
    /// </summary>
    /// <param name="data">Testovací data, která reprezentují testovanou entitu.</param>
    /// <param name="shouldThrow">Určuje, zda se očekává vyvolání výjimky.</param>
    /// <param name="errMessageContains">Řetězec, který se má nacházet v chybové zprávě při neúspěchu.</param>
    /// <typeparam name="TOriginal">Typ originální entity, kterou testujeme.</typeparam>
    public static void TestSetProperty<TOriginal>(this IFakeEntity<TOriginal> data, bool shouldThrow, string errMessageContains)
    {
        data.TestSetProperty<TOriginal, UserFriendlyException>(shouldThrow, errMessageContains);
    }
    
    /// <summary>
    /// Testuje vlastnost entit a ověřuje, zda se vytvořená entita shoduje s očekáváním.
    /// Pokud je <paramref name="shouldThrow"/> true, očekává se, že při volání metody bude vyvolána výjimka.
    /// </summary>
    /// <param name="data">Testovací data, která reprezentují testovanou entitu.</param>
    /// <param name="shouldThrow">Určuje, zda se očekává vyvolání výjimky.</param>
    /// <param name="errMessageContains">Řetězec, který se má nacházet v chybové zprávě při neúspěchu.</param>
    /// <typeparam name="TOriginal">Typ originální entity, kterou testujeme.</typeparam>
    /// <typeparam name="TException">Typ chyby, kterou očekávame že funkce hodí.</typeparam>
    public static void TestSetProperty<TOriginal, TException>(this IFakeEntity<TOriginal> data, bool shouldThrow, string errMessageContains)
    where TException : Exception
    {
        if (!shouldThrow)
        {
            var r = data.CreateOriginalEntity();
            data.CheckIfSame(r);
        }
        else
        {
            var exShould = new ExtendedShould<TOriginal>(data);
            var err = exShould.Throw<TException>(() => data.CreateOriginalEntity(false));
            exShould.ShouldContain(err.Message, errMessageContains);
        }
    }
    
    /// <summary>
    /// Testuje vlastnost entit a ověřuje, zda se vytvořená entita shoduje s očekáváním.
    /// Pokud je <paramref name="shouldThrow"/> true, očekává se, že při volání funkce bude vyvolána výjimka.
    /// </summary>
    /// <param name="data">Testovací data, která reprezentují testovanou entitu.</param>
    /// <param name="action">Funkce, která provádí testovanou akci (např. vytvoření entity).</param>
    /// <param name="shouldThrow">Určuje, zda se očekává vyvolání výjimky.</param>
    /// <param name="errMessageContains">Řetězec, který se má nacházet v chybové zprávě při neúspěchu.</param>
    /// <typeparam name="TOriginal">Typ originální entity, kterou testujeme.</typeparam>
    public static void TestSetProperty<TOriginal>(this IFakeEntity<TOriginal> data, 
        Func<IFakeEntity<TOriginal>, bool, TOriginal> action, bool shouldThrow, string errMessageContains)
    {
        data.TestSetProperty<TOriginal, UserFriendlyException>(action, shouldThrow, errMessageContains);
    }
    
    /// <summary>
    /// Testuje vlastnost entit a ověřuje, zda se vytvořená entita shoduje s očekáváním.
    /// Pokud je <paramref name="shouldThrow"/> true, očekává se, že při volání funkce bude vyvolána výjimka.
    /// </summary>
    /// <param name="data">Testovací data, která reprezentují testovanou entitu.</param>
    /// <param name="action">Funkce, která provádí testovanou akci (např. vytvoření entity).</param>
    /// <param name="shouldThrow">Určuje, zda se očekává vyvolání výjimky.</param>
    /// <param name="errMessageContains">Řetězec, který se má nacházet v chybové zprávě při neúspěchu.</param>
    /// <typeparam name="TOriginal">Typ originální entity, kterou testujeme.</typeparam>
    /// <typeparam name="TException">Typ chyby, kterou očekávame že funkce hodí.</typeparam>
    public static void TestSetProperty<TOriginal, TException>(this IFakeEntity<TOriginal> data, 
        Func<IFakeEntity<TOriginal>, bool, TOriginal> action, bool shouldThrow, string errMessageContains)
    where TException : Exception
    {
        if (!shouldThrow)
        {
            var r = action(data, true);
            data.CheckIfSame(r);
        }
        else
        {
            var exShould = new ExtendedShould<TOriginal>(data);
            var err = exShould.Throw<TException>(() => action(data, false));
            exShould.ShouldContain(err.Message, errMessageContains);
        }
    }
}