using Doara.Api.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Doara.Api.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ApiPageModel : AbpPageModel
{
    protected ApiPageModel()
    {
        LocalizationResourceType = typeof(ApiResource);
    }
}
