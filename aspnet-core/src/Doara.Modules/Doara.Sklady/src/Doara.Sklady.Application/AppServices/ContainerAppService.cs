using System;
using System.Threading.Tasks;
using Doara.Sklady.Dto;
using Doara.Sklady.Entities;
using Doara.Sklady.IAppServices;
using Doara.Sklady.Repositories;

namespace Doara.Sklady.AppServices;

public class ContainerAppService : SkladyAppService, IContainerAppService
{
    private readonly IContainerRepository _containerRepository;

    public ContainerAppService(IContainerRepository containerRepository)
    {
        _containerRepository = containerRepository;
    }
    
    public async Task<ContainerDto> GetAsync(Guid id)
    {
        var res = await _containerRepository.GetAsync(id);
        return ObjectMapper.Map<Container, ContainerDto>(res); 
    }
    
    public async Task<ContainerDto> CreateAsync(ContainerCreateInputDto input)
    {
        var guid = GuidGenerator.Create();
        var container = new Container(guid, input.Name);
        var res = await _containerRepository.CreateAsync(container);
        return ObjectMapper.Map<Container, ContainerDto>(res); 
    }

    public async Task<ContainerDto> UpdateAsync(ContainerUpdateInputDto input)
    {
        var container = await _containerRepository.GetAsync(input.Id);
        container.SetName(input.Name);
        var res = await _containerRepository.UpdateAsync(container);
        return ObjectMapper.Map<Container, ContainerDto>(res); 
    }

    public async Task DeleteAsync(Guid id)
    {
        await GetAsync(id);
        await _containerRepository.DeleteAsync(id);
    }

    public async Task<ContainerDto> ChangeStateAsync(ContainerChangeStateInputDto input)
    {
        var container = await _containerRepository.GetAsync(input.Id);
        container.SetName(input.Name);
        var res = await _containerRepository.UpdateAsync(container);
        return ObjectMapper.Map<Container, ContainerDto>(res); 
    }
}