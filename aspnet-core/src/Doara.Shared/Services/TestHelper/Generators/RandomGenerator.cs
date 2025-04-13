namespace TestHelper.Generators;

/// <summary>
/// Třída <c>RandomGenerator</c> poskytuje metody pro generování náhodných řetězců, čísel a hodnot z enumerací.
/// Slouží ke generování náhodných dat pro testovací účely.
/// </summary>
public static class RandomGenerator
{
    private static readonly Random Random = new ();
    
    /// <summary>
    /// Vytvoří náhodný alfanumerický řetězec z velkých a malých písmen (včetně diakritických znaků české abecedy) a čísel.
    /// </summary>
    /// <param name="length">Délka generovaného řetězce. Výchozí hodnota je 10.</param>
    /// <returns>Náhodný alfanumerický řetězec s délkou určenou parametrem <paramref name="length"/>.</returns>
    public static string RandomAlpNum(int length = 10)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZĚŠČŘŽÝÁÍÉÚŮ";
        chars += chars.ToLower();
        chars += "0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());
    }
    
    /// <summary>
    /// Vytvoří náhodný alfanumerický řetězec s délkou v rozmezí určeném parametry <paramref name="min"/> a <paramref name="max"/>.
    /// </summary>
    /// <param name="min">Minimální délka řetězce (včetně).</param>
    /// <param name="max">Maximální délka řetězce (vyjma).</param>
    /// <returns>Náhodný alfanumerický řetězec s náhodnou délkou v rozmezí <paramref name="min"/> až <paramref name="max"/>.</returns>
    public static string RandomAlpNum(uint min, uint max)
    {
        var length = RandomNumber(Convert.ToInt32(min), Convert.ToInt32(max));
        return RandomAlpNum(length);
    }
    
    /// <summary>
    /// Vytvoří náhodný řetězec čísel o určené délce.
    /// </summary>
    /// <param name="length">Délka generovaného řetězce.</param>
    /// <returns>Náhodný řetězec obsahující pouze čísla s délkou určenou parametrem <paramref name="length"/>.</returns>
    public static string RandomNumeralString(int length)
    {
        const string chars = "0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[Random.Next(s.Length)]).ToArray());  
    }
    
    /// <summary>
    /// Generuje náhodné celé číslo v zadaném rozsahu.
    /// </summary>
    /// <param name="min">Dolní hranice rozsahu (včetně). Výchozí hodnota je minimální hodnota pro <c>int</c>.</param>
    /// <param name="max">Horní hranice rozsahu (vyjma). Výchozí hodnota je maximální hodnota pro <c>int</c>.</param>
    /// <returns>Náhodné celé číslo v rozsahu od <paramref name="min"/> do <paramref name="max"/>.</returns>
    public static int RandomNumber(int min = int.MinValue, int max = int.MaxValue)
    {
        return Random.Next(min, max);
    }
    
    /// <summary>
    /// Vrátí náhodnou hodnotu z dané enumerace <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Typ enumerace.</typeparam>
    /// <returns>Náhodná hodnota z enumerace <typeparamref name="T"/>.</returns>
    /// <exception cref="ArgumentException">Vyvoláno, pokud <typeparamref name="T"/> není typu enumerace.</exception>
    public static T RandomFromEnum<T>()
    {
        var values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(Random.Next(values.Length))!;
    }

    /// <summary>
    /// Vybere náhodný prvek ze zadaného seznamu.
    /// </summary>
    /// <param name="values">Seznam prvků, ze kterého se vybírá náhodný prvek.</param>
    /// <typeparam name="T">Typ prvků v seznamu.</typeparam>
    /// <returns>Náhodný prvek z <paramref name="values"/>.</returns>
    /// <exception cref="ArgumentException">Vyvoláno, pokud je seznam <paramref name="values"/> prázdný.</exception>
    public static T RandomFromList<T>(List<T> values)
    {
        return values[Random.Next(values.Count)];
    }
}