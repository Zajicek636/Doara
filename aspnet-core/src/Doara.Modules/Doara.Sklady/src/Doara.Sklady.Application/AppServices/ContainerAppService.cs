using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doara.Sklady.Dto.Container;
using Doara.Sklady.Entities;
using Doara.Sklady.IAppServices;
using Doara.Sklady.Permissions;
using Doara.Sklady.Repositories;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Doara.Sklady.AppServices;

public class ContainerAppService(IContainerRepository containerRepository) : SkladyAppService, IContainerAppService
{
    [Authorize(SkladyPermissions.ReadContainerPermission)]
    public async Task<ContainerDetailDto> GetAsync(Guid id)
    {
        var res = await containerRepository.GetAsync(id);
        return ObjectMapper.Map<Container, ContainerDetailDto>(res); 
    }
    
    [Authorize(SkladyPermissions.ReadContainerPermission)]
    public async Task<PagedResultDto<ContainerDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
    {
        var res = await containerRepository.GetAllAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(Container.Id), false);
        var totalCount = await containerRepository.GetCountAsync();
        return new PagedResultDto<ContainerDto>
        {
            Items = ObjectMapper.Map<List<Container>, List<ContainerDto>>(res),
            TotalCount = totalCount
        };
    }
    
    [Authorize(SkladyPermissions.ReadContainerPermission)]
    public async Task<PagedResultDto<ContainerDetailDto>> GetAllWithDetailAsync(PagedAndSortedResultRequestDto input)
    {
        var res = await containerRepository.GetAllAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(Container.Id), true);
        var totalCount = await containerRepository.GetCountAsync();
        return new PagedResultDto<ContainerDetailDto>
        {
            Items = ObjectMapper.Map<List<Container>, List<ContainerDetailDto>>(res),
            TotalCount = totalCount
        };
    }
    
    [Authorize(SkladyPermissions.CreateContainerPermission)]
    public async Task<ContainerDetailDto> CreateAsync(ContainerCreateInputDto input)
    {
        var guid = GuidGenerator.Create();
        var container = new Container(guid, input.Name, input.Description);
        var res = await containerRepository.CreateAsync(container);
        return ObjectMapper.Map<Container, ContainerDetailDto>(res); 
    }

    [Authorize(SkladyPermissions.UpdateContainerPermission)]
    public async Task<ContainerDetailDto> UpdateAsync(Guid id, ContainerUpdateInputDto input)
    {
        var container = await containerRepository.GetAsync(id);
        container.SetName(input.Name).SetDescription(input.Description);
        var res = await containerRepository.UpdateAsync(container);
        return ObjectMapper.Map<Container, ContainerDetailDto>(res); 
    }

    [Authorize(SkladyPermissions.DeleteContainerPermission)]
    public async Task DeleteAsync(Guid id)
    {
        if (!await containerRepository.AnyAsync(x => x.Id == id))
        {
            throw new EntityNotFoundException(typeof(Container), id);
        }
        await containerRepository.DeleteAsync(id);
    }
}