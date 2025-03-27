using Doara.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Doara.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class DoaraController : AbpControllerBase
{
    protected DoaraController()
    {
        LocalizationResource = typeof(DoaraResource);
    }
}
