using Sklady.Localization;
using Volo.Abp.Application.Services;

namespace Sklady;

public abstract class SkladyAppService : ApplicationService
{
    protected SkladyAppService()
    {
        LocalizationResource = typeof(SkladyResource);
        ObjectMapperContext = typeof(SkladyApplicationModule);
    }
}
