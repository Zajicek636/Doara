using Doara.Sklady.Localization;
using Volo.Abp.Application.Services;

namespace Doara.Sklady;

public abstract class SkladyAppService : ApplicationService
{
    protected SkladyAppService()
    {
        LocalizationResource = typeof(SkladyResource);
        ObjectMapperContext = typeof(SkladyApplicationModule);
    }
}
