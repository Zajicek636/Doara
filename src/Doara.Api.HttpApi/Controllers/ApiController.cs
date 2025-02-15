using Doara.Api.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Doara.Api.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ApiController : AbpControllerBase
{
    protected ApiController()
    {
        LocalizationResource = typeof(ApiResource);
    }
}
