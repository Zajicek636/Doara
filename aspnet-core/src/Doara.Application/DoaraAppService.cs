using System;
using System.Collections.Generic;
using System.Text;
using Doara.Localization;
using Volo.Abp.Application.Services;

namespace Doara;

/* Inherit your application services from this class.
 */
public abstract class DoaraAppService : ApplicationService
{
    protected DoaraAppService()
    {
        LocalizationResource = typeof(DoaraResource);
    }
}
