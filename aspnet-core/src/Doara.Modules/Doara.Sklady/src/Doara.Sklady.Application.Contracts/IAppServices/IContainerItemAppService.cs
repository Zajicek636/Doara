using System;
using System.Threading.Tasks;
using Doara.Sklady.Dto.ContainerItem;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Doara.Sklady.IAppServices;

public interface IContainerItemAppService : IApplicationService
{
    Task<ContainerItemDetailDto> GetAsync(Guid id);
    Task<PagedResultDto<ContainerItemDto>> GetAllAsync(ContainerItemGetAllDto input);
    Task<PagedResultDto<ContainerItemDetailDto>> GetAllWithDetailAsync(ContainerItemGetAllDto input);
    Task<ContainerItemDetailDto> CreateAsync(ContainerItemCreateInputDto input);
    Task<ContainerItemDetailDto> UpdateAsync(Guid id, ContainerItemUpdateInputDto input);
    Task DeleteAsync(Guid id);
}