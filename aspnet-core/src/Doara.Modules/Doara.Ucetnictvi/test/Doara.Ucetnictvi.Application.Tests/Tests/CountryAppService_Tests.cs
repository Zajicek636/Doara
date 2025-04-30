using System;
using System.Threading.Tasks;
using Doara.Ucetnictvi.IAppServices;
using Doara.Ucetnictvi.Utils.Converters;
using Shouldly;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;
using Xunit;

namespace Doara.Ucetnictvi.Tests;

public class CountryAppService_Tests : UcetnictviApplicationTestBase<UcetnictviApplicationTestModule>
{
    private readonly ICountryAppService _countryAppService;
    private readonly IGuidGenerator _guidGenerator;
    
    public CountryAppService_Tests()
    {
        _countryAppService = GetRequiredService<ICountryAppService>();
        _guidGenerator = GetRequiredService<IGuidGenerator>();
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
        var id = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _countryAppService.GetAsync(id);
        });
        exception.Message.ShouldContain(nameof(Entities.Country));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Create_Country()
    {
        var input = Converter.CreateCountryInput();
        var country = await _countryAppService.CreateAsync(input);
        
        country.Id.ShouldNotBe(Guid.Empty);
        country.Code.ShouldBe(input.Code);
        country.Name.ShouldBe(input.Name);
    }

    [Fact]
    public async Task Should_Update_Country()
    {
        var country = await _countryAppService.GetAsync(TestData.CzCountryId);
        country.Code = Converter.DefaultCountryCode;
        country.Name = Converter.DefaultCountryName;
        
        var updatedCountry = await _countryAppService.UpdateAsync(country.Id, Converter.Convert2UpdateInput(country));
        
        updatedCountry.Id.ShouldBe(country.Id);
        updatedCountry.Code.ShouldBe(country.Code);
        updatedCountry.Name.ShouldBe(country.Name);
    }
    
    [Fact]
    public async Task Should_Throw_Update_Non_Existing_Country()
    {
        var id = _guidGenerator.Create();
        var country = await _countryAppService.GetAsync(TestData.CzCountryId);
        country.Code = Converter.DefaultCountryCode;
        country.Name = Converter.DefaultCountryName;
        
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _countryAppService.UpdateAsync(id, Converter.Convert2UpdateInput(country));
        });
        exception.Message.ShouldContain(nameof(Entities.Country));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Delete_Country()
    {
        await _countryAppService.DeleteAsync(TestData.CzCountryId);

        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _countryAppService.GetAsync(TestData.CzCountryId);
        });
        exception.Message.ShouldContain(nameof(Entities.Country));
        exception.Message.ShouldContain(TestData.CzCountryId.ToString());
    }
    
    [Fact]
    public async Task Should_Throw_Delete_Non_Existing_Country()
    {
        var id = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _countryAppService.DeleteAsync(id);
        });
        exception.Message.ShouldContain(nameof(Entities.Country));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_GetAll()
    {
        var entities = await _countryAppService.GetAllAsync(new PagedAndSortedResultRequestDto
        {
            SkipCount = 1,
            MaxResultCount = 4,
            Sorting = "code desc"
        });
        
        entities.TotalCount.ShouldBe(3);
        entities.Items.Count.ShouldBe(2);
        entities.Items[0].Id.ShouldBe(TestData.SkCountryId);
        entities.Items[1].Id.ShouldBe(TestData.CzCountryId);
    }
}