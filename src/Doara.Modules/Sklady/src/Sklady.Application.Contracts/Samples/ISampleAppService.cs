using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Sklady.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
