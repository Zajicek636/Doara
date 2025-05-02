namespace Doara.Sklady.Enums;

public enum MovementCategory
{
    Unused = 'N',//(přírůstky či uvolnění rezervací, zvyšuje dostupné množství),
    Reserved = 'R', //(blokace, snižuje dostupné, ale ne fyzické),
    Reserved2Used = 'S', // pohyb rezervace → použití – nezapočítává se do Reserved
    Used = 'U'//(spotřeba, snižuje fyzické množství).
}

public static class MovementCategoryExtensions
{
    public static bool IsOpposite(this MovementCategory from, MovementCategory to)
    {
        if(from == MovementCategory.Unused && to != MovementCategory.Unused)
        {
            return true;
        }

        return from != MovementCategory.Unused && to == MovementCategory.Unused;
    }
}
