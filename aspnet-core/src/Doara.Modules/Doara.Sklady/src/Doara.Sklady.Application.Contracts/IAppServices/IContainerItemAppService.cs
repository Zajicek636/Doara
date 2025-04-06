using System;
using System.Threading.Tasks;
using Doara.Sklady.Dto.ContainerItem;
using Volo.Abp.Application.Services;

namespace Doara.Sklady.IAppServices;

public interface IContainerItemAppService : IApplicationService
{
    Task<ContainerItemDto> GetAsync(Guid id);
    Task<ContainerItemDto> CreateAsync(ContainerItemCreateInputDto input);
    Task<ContainerItemDto> UpdateAsync(ContainerItemUpdateInputDto input);
    Task DeleteAsync(Guid id);
    Task<ContainerItemDto> ChangeStateAsync(ContainerItemChangeStateInputDto input);
}