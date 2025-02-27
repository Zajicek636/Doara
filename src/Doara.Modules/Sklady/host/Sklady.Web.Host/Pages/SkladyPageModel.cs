using Sklady.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Sklady.Pages;

public abstract class SkladyPageModel : AbpPageModel
{
    protected SkladyPageModel()
    {
        LocalizationResourceType = typeof(SkladyResource);
    }
}
