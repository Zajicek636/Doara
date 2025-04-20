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
    private readonly IAddressRepository _addressRepository;

    public UcetnictviDataSeedContributor(
        IGuidGenerator guidGenerator, ICurrentTenant currentTenant, ICountryRepository countryRepository, 
        IAddressRepository addressRepository)
    {
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
        _countryRepository = countryRepository;
        _addressRepository = addressRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        using (_currentTenant.Change(context?.TenantId))
        {
            await SeedCountryAsync();
            await SeedAddressAsync();
        }
    }

    private async Task SeedCountryAsync()
    {
        await _countryRepository.CreateAsync(new Country(TestData.CzCountryId, "Česká republika", "CZ"));
        await _countryRepository.CreateAsync(new Country(TestData.SkCountryId, "Slovensko", "SK"));
        await _countryRepository.CreateAsync(new Country(TestData.UsCountryId, "United States", "USA"));
    }
    
    private async Task SeedAddressAsync()
    {
        await _addressRepository.CreateAsync(new Address(
            TestData.CzAddressId, 
            "Václavské náměstí 1", 
            "Praha", 
            "11000", 
            TestData.CzCountryId
        ));

        await _addressRepository.CreateAsync(new Address(
            TestData.SkAddressId,
            "Hlavná 5", 
            "Bratislava", 
            "81101", 
            TestData.SkCountryId
        ));

        await _addressRepository.CreateAsync(new Address(
            TestData.UsAddressId,
            "1600 Pennsylvania Avenue NW", 
            "Washington", 
            "20500", 
            TestData.UsCountryId
        ));
    }
}
