using Doara.Ucetnictvi.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Doara.Ucetnictvi;

public abstract class UcetnictviController : AbpControllerBase
{
    protected UcetnictviController()
    {
        LocalizationResource = typeof(UcetnictviResource);
    }
}
