using Sklady.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Sklady;

public abstract class SkladyController : AbpControllerBase
{
    protected SkladyController()
    {
        LocalizationResource = typeof(SkladyResource);
    }
}
