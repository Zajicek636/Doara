using System;
using System.Threading.Tasks;
using Doara.Sklady.Dto;
using Doara.Sklady.Dto.Container;
using Volo.Abp.Application.Services;

namespace Doara.Sklady.IAppServices;

public interface IContainerAppService : IApplicationService
{
    Task<ContainerDto> GetAsync(Guid id);
    Task<ContainerDto> CreateAsync(ContainerCreateInputDto input);
    Task<ContainerDto> UpdateAsync(ContainerUpdateInputDto input);
    Task DeleteAsync(Guid id);
    Task<ContainerDto> ChangeStateAsync(ContainerChangeStateInputDto input);
}