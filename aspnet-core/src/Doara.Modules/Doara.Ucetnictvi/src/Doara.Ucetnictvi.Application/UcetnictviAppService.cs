using Doara.Ucetnictvi.Localization;
using Volo.Abp.Application.Services;

namespace Doara.Ucetnictvi;

public abstract class UcetnictviAppService : ApplicationService
{
    protected UcetnictviAppService()
    {
        LocalizationResource = typeof(UcetnictviResource);
        ObjectMapperContext = typeof(UcetnictviApplicationModule);
    }
}
