using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.Address;
using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.IAppServices;
using Doara.Ucetnictvi.Permissions;
using Doara.Ucetnictvi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Doara.Ucetnictvi.AppServices;

public class AddressAppService(IAddressRepository addressRepository, ICountryRepository countryRepository) : UcetnictviAppService, IAddressAppService
{
    [Authorize(UcetnictviPermissions.ReadAddressPermission)]
    public async Task<AddressDetailDto> GetAsync(Guid id)
    {
        var res = await addressRepository.GetAsync(id);
        return ObjectMapper.Map<Address, AddressDetailDto>(res); 
    }

    [Authorize(UcetnictviPermissions.ReadAddressPermission)]
    public async Task<PagedResultDto<AddressDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
    {
        var res = await addressRepository.GetAllAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(Address.Id), false);
        var totalCount = await addressRepository.GetCountAsync();
        return new PagedResultDto<AddressDto>
        {
            Items = ObjectMapper.Map<List<Address>, List<AddressDto>>(res),
            TotalCount = totalCount
        };
    }
    
    [Authorize(UcetnictviPermissions.ReadAddressPermission)]
    public async Task<PagedResultDto<AddressDetailDto>> GetAllWithDetailAsync(PagedAndSortedResultRequestDto input)
    {
        var res = await addressRepository.GetAllAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(Address.Id), true);
        var totalCount = await addressRepository.GetCountAsync();
        return new PagedResultDto<AddressDetailDto>
        {
            Items = ObjectMapper.Map<List<Address>, List<AddressDetailDto>>(res),
            TotalCount = totalCount
        };
    }

    [Authorize(UcetnictviPermissions.CreateAddressPermission)]
    public async Task<AddressDetailDto> CreateAsync(AddressCreateInputDto input)
    {
        var country = await countryRepository.GetAsync(input.CountryId);
        var guid = GuidGenerator.Create();
        var address = new Address(guid, input.Street, input.City, input.PostalCode, input.CountryId);
        var res = await addressRepository.CreateAsync(address);
        res.SetCountry(country);
        return ObjectMapper.Map<Address, AddressDetailDto>(res); 
    }

    [Authorize(UcetnictviPermissions.UpdateAddressPermission)]
    public async Task<AddressDetailDto> UpdateAsync(Guid id, AddressUpdateInputDto input)
    {
        var country = await countryRepository.GetAsync(input.CountryId);
        var address = await addressRepository.GetAsync(id);
        address.SetCity(input.City).SetStreet(input.Street)
            .SetPostalCode(input.PostalCode).SetCountry(input.CountryId);
        var res = await addressRepository.UpdateAsync(address);
        res.SetCountry(country);
        return ObjectMapper.Map<Address, AddressDetailDto>(res); 
    }

    [Authorize(UcetnictviPermissions.DeleteAddressPermission)]
    public async Task DeleteAsync(Guid id)
    {
        if (!await addressRepository.AnyAsync(x => x.Id == id))
        {
            throw new EntityNotFoundException(typeof(Address), id);
        }
        await addressRepository.DeleteAsync(id);
    }
}