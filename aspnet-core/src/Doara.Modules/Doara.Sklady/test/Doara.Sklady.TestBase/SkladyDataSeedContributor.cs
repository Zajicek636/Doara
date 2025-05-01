using System.Threading.Tasks;
using Doara.Sklady.Entities;
using Doara.Sklady.Enums;
using Doara.Sklady.Repositories;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Doara.Sklady;

public class SkladyDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    private readonly IContainerRepository _containerRepository;
    private readonly IContainerItemRepository _containerItemRepository;
    
    public SkladyDataSeedContributor(IGuidGenerator guidGenerator, ICurrentTenant currentTenant, 
        IContainerRepository containerRepository, IContainerItemRepository containerItemRepository)
    {
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
        _containerRepository = containerRepository;
        _containerItemRepository = containerItemRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await SeedContainerAsync();
            await SeedContainerItemAsync();
        }
    }
    
    private async Task SeedContainerAsync()
    {
        await _containerRepository.CreateAsync(new Container(TestData.Container1Id, "Container1", "Container1Description"));
        await _containerRepository.CreateAsync(new Container(TestData.Container2Id, "Container2", "Container2Description"));
        await _containerRepository.CreateAsync(new Container(TestData.Container3Id, "Container3", "Container3Description"));
    }
    
    private async Task SeedContainerItemAsync()
    {
        await _containerItemRepository.CreateAsync(new ContainerItem(TestData.ContainerItem11Id,
            "ContainerItem11", "ContainerItem11Description", 10, 10, 10, 10, 10, "url1", TestData.Container1Id, 10,
            QuantityType.Grams));
        await _containerItemRepository.CreateAsync(new ContainerItem(TestData.ContainerItem21Id,
            "ContainerItem21", "ContainerItem21Description", 10, 10, 10, 10, 10, null, TestData.Container2Id, 10,
            QuantityType.Liters));
        await _containerItemRepository.CreateAsync(new ContainerItem(TestData.ContainerItem22Id,
            "ContainerItem22", "ContainerItem22Description", 10, 10, 10, 10, 10, "url3", TestData.Container2Id, 10,
            QuantityType.None));
    }
}
