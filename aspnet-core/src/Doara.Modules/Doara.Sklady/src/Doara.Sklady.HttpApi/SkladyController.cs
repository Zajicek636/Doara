using Doara.Sklady.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Doara.Sklady;

public abstract class SkladyController : AbpControllerBase
{
    protected SkladyController()
    {
        LocalizationResource = typeof(SkladyResource);
    }
}
