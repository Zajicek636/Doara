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

public class AddressAppService_Tests : UcetnictviApplicationTestBase<UcetnictviApplicationTestModule>
{
    private readonly IAddressAppService _addressAppService;
    private readonly IGuidGenerator _guidGenerator;
    
    public AddressAppService_Tests()
    {
        _guidGenerator = GetRequiredService<IGuidGenerator>();
        _addressAppService = GetRequiredService<IAddressAppService>();
    }
    
    [Fact]
    public async Task Should_Get_Address()
    {
        var czAddress = await _addressAppService.GetAsync(TestData.CzAddressId);
        czAddress.Id.ShouldBe(TestData.CzAddressId);
        czAddress.Street.ShouldBe("Václavské náměstí 1");
        czAddress.City.ShouldBe("Praha");
        czAddress.PostalCode.ShouldBe("11000");
        czAddress.CountryId.ShouldBe(TestData.CzCountryId);
        czAddress.CountryCode.ShouldBe("CZ");
        czAddress.CountryName.ShouldBe("Česká republika");
    }
    
    [Fact]
    public async Task Should_Throw_Get_Non_Existing_Address()
    {
        var id = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _addressAppService.GetAsync(id);
        });
        exception.Message.ShouldContain(nameof(Entities.Address));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Create_Address()
    {
        var input = Converter.CreateAddressInput(TestData.CzCountryId);
        var address = await _addressAppService.CreateAsync(input);
        
        address.Id.ShouldNotBe(Guid.Empty);
        address.Street.ShouldBe(input.Street);
        address.City.ShouldBe(input.City);
        address.PostalCode.ShouldBe(input.PostalCode);
        address.CountryId.ShouldBe(input.CountryId);
        address.CountryCode.ShouldBe("CZ");
        address.CountryName.ShouldBe("Česká republika");
    }
    
    [Fact]
    public async Task Should_Create_Address_Non_Existing_Country()
    {
        var id = _guidGenerator.Create();
        var input = Converter.CreateAddressInput(id);
        
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
           await _addressAppService.CreateAsync(input);
        });
        exception.Message.ShouldContain(nameof(Entities.Country));
        exception.Message.ShouldContain(id.ToString());
    }

    [Fact]
    public async Task Should_Update_Address()
    {
        var address = await _addressAppService.GetAsync(TestData.CzAddressId);
        address.CountryId = TestData.UsCountryId;
        address.Street = Converter.DefaultAddressStreet;
        address.City = Converter.DefaultAddressCity;
        address.PostalCode = Converter.DefaultAddressPostalCode;
        
        var updatedAddress = await _addressAppService.UpdateAsync(Converter.Convert2UpdateInput(address));
        
        updatedAddress.Id.ShouldBe(address.Id);
        updatedAddress.Street.ShouldBe(address.Street);
        updatedAddress.City.ShouldBe(address.City);
        updatedAddress.PostalCode.ShouldBe(address.PostalCode);
        updatedAddress.CountryId.ShouldBe(address.CountryId);
        updatedAddress.CountryCode.ShouldBe("USA");
        updatedAddress.CountryName.ShouldBe("United States");
    }
    
    [Fact]
    public async Task Should_Throw_Update_Non_Existing_Address()
    {
        var id = _guidGenerator.Create();
        var address = await _addressAppService.GetAsync(TestData.CzAddressId);
        address.CountryId = TestData.UsCountryId;
        address.Street = Converter.DefaultAddressStreet;
        address.City = Converter.DefaultAddressCity;
        address.PostalCode = Converter.DefaultAddressPostalCode;
        address.Id = id;
        
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _addressAppService.UpdateAsync(Converter.Convert2UpdateInput(address));
        });
        exception.Message.ShouldContain(nameof(Entities.Address));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Throw_Update_Non_Existing_Country()
    {
        var id = _guidGenerator.Create();
        var address = await _addressAppService.GetAsync(TestData.CzAddressId);
        address.CountryId = id;
        address.Street = Converter.DefaultAddressStreet;
        address.City = Converter.DefaultAddressCity;
        address.PostalCode = Converter.DefaultAddressPostalCode;
        
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _addressAppService.UpdateAsync(Converter.Convert2UpdateInput(address));
        });
        exception.Message.ShouldContain(nameof(Entities.Country));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_Delete_Address()
    {
        await _addressAppService.DeleteAsync(TestData.CzAddressId);

        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _addressAppService.GetAsync(TestData.CzAddressId);
        });
        exception.Message.ShouldContain(nameof(Entities.Address));
        exception.Message.ShouldContain(TestData.CzAddressId.ToString());
    }
    
    [Fact]
    public async Task Should_Throw_Delete_Non_Existing_Address()
    {
        var id = _guidGenerator.Create();
        var exception = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _addressAppService.DeleteAsync(id);
        });
        exception.Message.ShouldContain(nameof(Entities.Address));
        exception.Message.ShouldContain(id.ToString());
    }
    
    [Fact]
    public async Task Should_GetAll()
    {
        var entities = await _addressAppService.GetAllAsync(new PagedAndSortedResultRequestDto
        {
            SkipCount = 1,
            MaxResultCount = 4,
            Sorting = "City desc"
        });
        
        entities.TotalCount.ShouldBe(3);
        entities.Items.Count.ShouldBe(2);
        entities.Items[0].Id.ShouldBe(TestData.CzAddressId);
        entities.Items[1].Id.ShouldBe(TestData.SkAddressId);
    }
}