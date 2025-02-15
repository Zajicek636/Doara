using System;
using System.Collections.Generic;
using System.Text;
using Doara.Api.Localization;
using Volo.Abp.Application.Services;

namespace Doara.Api;

/* Inherit your application services from this class.
 */
public abstract class ApiAppService : ApplicationService
{
    protected ApiAppService()
    {
        LocalizationResource = typeof(ApiResource);
    }
}
