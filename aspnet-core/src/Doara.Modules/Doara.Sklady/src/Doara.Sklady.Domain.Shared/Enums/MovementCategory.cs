namespace Doara.Sklady.Enums;

public enum MovementCategory
{
    Unused = 'N',//(přírůstky či uvolnění rezervací, zvyšuje dostupné množství),
    Reserved = 'R', //(blokace, snižuje dostupné, ale ne fyzické),
    Reserved2Used = 'S', // pohyb rezervace → použití – nezapočítává se do Reserved
    Used = 'U'//(spotřeba, snižuje fyzické množství).
}