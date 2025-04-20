using System.Threading.Tasks;
using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.Repositories;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Doara.Ucetnictvi;

public class UcetnictviDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    private readonly ICountryRepository _countryRepository;

    public UcetnictviDataSeedContributor(
        IGuidGenerator guidGenerator, ICurrentTenant currentTenant, ICountryRepository countryRepository)
    {
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
        _countryRepository = countryRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await SeedCountryAsync();
        }
    }

    private async Task SeedCountryAsync()
    {
        await _countryRepository.CreateAsync(new Country(TestData.CzCountryId, "Česká republika", "CZ"));
        await _countryRepository.CreateAsync(new Country(TestData.SkCountryId, "Slovensko", "SK"));
        await _countryRepository.CreateAsync(new Country(TestData.UsCountryId, "United States", "USA"));
    }
}
