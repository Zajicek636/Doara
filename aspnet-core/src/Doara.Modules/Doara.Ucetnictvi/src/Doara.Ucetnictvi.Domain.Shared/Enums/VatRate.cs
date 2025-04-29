namespace Doara.Ucetnictvi.Enums;

public enum VatRate
{
    None = 'X',           // není vyplněno
    Standard21 = 'S',     // základní sazba
    Reduced15 = 'R',      // první snížená
    Reduced12 = 'L',      // druhá snížená
    Zero = 'Z',           // 0% sazba
    Exempt = 'E',         // osvobozeno od daně
    ReverseCharge = 'C',  // přenesená daňová povinnost
    NonVatPayer = 'N'     // neplátce DPH
}