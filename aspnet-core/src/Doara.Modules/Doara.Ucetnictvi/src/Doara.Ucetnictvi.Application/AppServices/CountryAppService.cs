using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.Country;
using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.IAppServices;
using Doara.Ucetnictvi.Permissions;
using Doara.Ucetnictvi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Doara.Ucetnictvi.AppServices;

public class CountryAppService(ICountryRepository countryRepository) : UcetnictviAppService, ICountryAppService
{
    [Authorize(UcetnictviPermissions.ReadCountryPermission)]
    public async Task<CountryDto> GetAsync(Guid id)
    {
        var res = await countryRepository.GetAsync(id);
        return ObjectMapper.Map<Country, CountryDto>(res); 
    }

    [Authorize(UcetnictviPermissions.ReadCountryPermission)]
    public async Task<PagedResultDto<CountryDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
    {
        var res = await countryRepository.GetAllAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(Country.Id));
        var totalCount = await countryRepository.GetCountAsync();
        return new PagedResultDto<CountryDto>
        {
            Items = ObjectMapper.Map<List<Country>, List<CountryDto>>(res),
            TotalCount = totalCount
        };
    }

    [Authorize(UcetnictviPermissions.CreateCountryPermission)]
    public async Task<CountryDto> CreateAsync(CountryCreateInputDto input)
    {
        var guid = GuidGenerator.Create();
        var country = new Country(guid, input.Name, input.Code);
        var res = await countryRepository.CreateAsync(country);
        return ObjectMapper.Map<Country, CountryDto>(res); 
    }

    [Authorize(UcetnictviPermissions.UpdateCountryPermission)]
    public async Task<CountryDto> UpdateAsync(CountryUpdateInputDto input)
    {
        var country = await countryRepository.GetAsync(input.Id);
        country.SetName(input.Name).SetCode(input.Code);
        var res = await countryRepository.UpdateAsync(country);
        return ObjectMapper.Map<Country, CountryDto>(res); 
    }

    [Authorize(UcetnictviPermissions.DeleteCountryPermission)]
    public async Task DeleteAsync(Guid id)
    {
        if (!await countryRepository.AnyAsync(x => x.Id == id))
        {
            throw new EntityNotFoundException(typeof(Country), id);
        }
        await countryRepository.DeleteAsync(id);
    }
}