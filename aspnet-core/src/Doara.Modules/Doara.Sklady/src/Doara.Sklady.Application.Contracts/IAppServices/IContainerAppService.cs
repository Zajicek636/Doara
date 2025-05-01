using System;
using System.Threading.Tasks;
using Doara.Sklady.Dto.Container;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Doara.Sklady.IAppServices;

public interface IContainerAppService : IApplicationService
{
    Task<ContainerDetailDto> GetAsync(Guid id);
    Task<PagedResultDto<ContainerDto>> GetAllAsync(PagedAndSortedResultRequestDto input);
    Task<PagedResultDto<ContainerDetailDto>> GetAllWithDetailAsync(PagedAndSortedResultRequestDto input);
    Task<ContainerDetailDto> CreateAsync(ContainerCreateInputDto input);
    Task<ContainerDetailDto> UpdateAsync(Guid id, ContainerUpdateInputDto input);
    Task DeleteAsync(Guid id);
}