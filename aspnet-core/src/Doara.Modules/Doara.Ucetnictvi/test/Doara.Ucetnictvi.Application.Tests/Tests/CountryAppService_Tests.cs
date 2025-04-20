using System;
using System.Threading.Tasks;
using Doara.Ucetnictvi.IAppServices;
using Shouldly;
using Volo.Abp.Domain.Entities;
using Xunit;

namespace Doara.Ucetnictvi.Tests;

public class CountryAppService_Tests : UcetnictviApplicationTestBase<UcetnictviApplicationTestModule>
{
    private readonly ICountryAppService _countryAppService;
    
    public CountryAppService_Tests()
    {
        _countryAppService = GetRequiredService<ICountryAppService>();
    }
    
    [Fact]
    public async Task Should_Get_Country()
    {
        var czCountry = await _countryAppService.GetAsync(TestData.CzCountryId);
        czCountry.Id.ShouldBe(TestData.CzCountryId);
        czCountry.Code.ShouldBe("CZ");
        czCountry.Name.ShouldBe("Česká republika");
    }
    
    [Fact]
    public async Task Should_Throw_Get_Non_Existing_Country()
    {
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _countryAppService.GetAsync(Guid.NewGuid());
        });
        exception.Message.ShouldContain(nameof(Entities.Country));
    }
    
    [Fact]
    public async Task Should_Create_Country()
    {
        var input = Converter.CreateHodnotaTrideniInput(Converter.NowMinusFive, Converter.NowPlusFive, kodUj: "unique");
        var czCountry = await _countryAppService.CreateAsync(TestData.CzCountryId);
        czCountry.Id.ShouldBe(TestData.CzCountryId);
        czCountry.Code.ShouldBe("CZ");
        czCountry.Name.ShouldBe("Česká republika");
    }
    /*[Fact]
    public async Task Should_Create_HodnotaTrideni()
    {
        var input = Converter.CreateHodnotaTrideniInput(Converter.NowMinusFive, Converter.NowPlusFive, kodUj: "unique");
        var createResult = await _appService.CreateAsync(input);
        createResult.ShouldNotBeNull();
        var result = await _appService.GetAsync(createResult.Id);

        ShouldBe(result, Converter.ToDto(input, createResult.Id));
    }*/
}