using Sklady.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Sklady.Blazor.Server.Host;

public abstract class SkladyComponentBase : AbpComponentBase
{
    protected SkladyComponentBase()
    {
        LocalizationResource = typeof(SkladyResource);
    }
}
