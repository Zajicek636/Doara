using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Shouldly;
using TestHelper.FakeEntities;

namespace TestHelper.Utils;

/// <summary>
/// Třída <c>ExtendedShould</c> poskytuje rozšíření pro testovací entity (<c>IFakeEntity</c>) a implementuje různé metody pro asserce (ověření) chování.
/// Umožňuje testování výjimek, obsahových shod, porovnávání hodnot a ověření, že akce nebo funkce nevyvolá výjimku.
/// Každá metoda podporuje přizpůsobení chybových zpráv prostřednictvím serializace testovací entity do formátu JSON.
/// </summary>
/// <param name="data4CustomMessage">Testovací entita implementující rozhraní <c>IFakeEntity</c>, která bude použita k přizpůsobení chybových zpráv.</param>
/// <typeparam name="TOriginal">Typ originální entity, kterou reprezentuje testovací (fake) entita.</typeparam>
public class ExtendedShould<TOriginal>(IFakeEntity<TOriginal> data4CustomMessage)
{
    private IFakeEntity<TOriginal> Data4CustomMessage { get; } = data4CustomMessage;
    
    /// <summary>
    /// Ověří, že daná akce vyvolá specifikovanou výjimku typu <typeparamref name="TException"/>.
    /// </summary>
    /// <typeparam name="TException">Typ výjimky, který očekáváme, že bude vyvolán při provádění akce.</typeparam>
    /// <param name="actual">Akce, která by měla vyvolat výjimku.</param>
    /// <returns>Vyvolanou výjimku, která bude vrácena pro další zpracování.</returns>
    public TException Throw<TException>([InstantHandle] Action actual) where TException : Exception
    {
        return Should.Throw<TException>(actual, Data4CustomMessage.SerializeToJson());
    }

    /// <summary>
    /// Ověří, že <paramref name="actual"/> obsahuje řetězec <paramref name="expected"/>.
    /// </summary>
    /// <param name="actual">Skutečný řetězec, který má být testován.</param>
    /// <param name="expected">Očekávaný řetězec, který má být obsažen v <paramref name="actual"/>.</param>
    /// <param name="caseSensitivity">Určuje, zda porovnání má být provedeno bez ohledu na velikost písmen (výchozí je <c>Insensitive</c>).</param>
    public void ShouldContain(string actual, string expected, Case caseSensitivity = Case.Insensitive)
    {
        actual.ShouldContain(expected, caseSensitivity, Data4CustomMessage.SerializeToJson());
    }
    
    /// <summary>
    /// Ověří, že hodnoty <paramref name="actual"/> a <paramref name="expected"/> jsou shodné.
    /// </summary>
    /// <typeparam name="TCompare">Typ hodnot, které se porovnávají.</typeparam>
    /// <param name="actual">Skutečná hodnota.</param>
    /// <param name="expected">Očekávaná hodnota.</param>
    public void ShouldBe<TCompare>(
        [NotNullIfNotNull("expected")] TCompare actual,
        [NotNullIfNotNull("actual")] TCompare expected)
    {
        actual.ShouldBe(expected, Data4CustomMessage.SerializeToJson());
    }

    /// <summary>
    /// Ověří, že daná akce nevyvolá žádnou výjimku.
    /// </summary>
    /// <param name="action">Akce, která by neměla vyvolat žádnou výjimku.</param>
    public void NotThrow([InstantHandle] Action action)
    {
        Should.NotThrow(action, Data4CustomMessage.SerializeToJson());
    }

    /// <summary>
    /// Ověří, že daná funkce nevyvolá žádnou výjimku a vrátí hodnotu.
    /// </summary>
    /// <typeparam name="TN">Typ hodnoty, kterou funkce vrací.</typeparam>
    /// <param name="action">Funkce, která by neměla vyvolat výjimku a vrátí hodnotu.</param>
    /// <returns>Výsledek funkce.</returns>
    public TN NotThrow<TN>(Func<TN> action)
    {
        return Should.NotThrow(action, Data4CustomMessage.SerializeToJson());
    }
    
    public void CheckDate(DateTime? actual, DateTime? expected)
    {
        if (expected == null && actual == null)
        {
            return;
        }
        ShouldBe(expected?.Year, actual?.Year);
        ShouldBe(expected?.Month, actual?.Month);
        ShouldBe(expected?.Day, actual?.Day);
    }
    
    public void CheckMoment(DateTime? actual, DateTime? expected, long allowedDifference = 0)
    {
        if (expected == null && actual == null)
        {
            return;
        }
        CheckDate(expected, actual);
        Math.Abs(expected!.Value.Ticks - actual!.Value.Ticks).ShouldBeLessThanOrEqualTo(allowedDifference);
    }
}