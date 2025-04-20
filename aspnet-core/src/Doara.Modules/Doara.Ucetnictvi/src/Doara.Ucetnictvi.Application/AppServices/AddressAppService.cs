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
    public async Task<AddressDetailedDto> GetAsync(Guid id)
    {
        var res = await addressRepository.GetAsync(id);
        return ObjectMapper.Map<Address, AddressDetailedDto>(res); 
    }

    [Authorize(UcetnictviPermissions.ReadAddressPermission)]
    public async Task<PagedResultDto<AddressDetailedDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
    {
        var res = await addressRepository.GetAllAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(Address.Id));
        var totalCount = await addressRepository.GetCountAsync();
        return new PagedResultDto<AddressDetailedDto>
        {
            Items = ObjectMapper.Map<List<Address>, List<AddressDetailedDto>>(res),
            TotalCount = totalCount
        };
    }

    [Authorize(UcetnictviPermissions.CreateAddressPermission)]
    public async Task<AddressDetailedDto> CreateAsync(AddressCreateInputDto input)
    {
        var country = await countryRepository.GetAsync(input.CountryId);
        var guid = GuidGenerator.Create();
        var address = new Address(guid, input.Street, input.City, input.PostalCode, input.CountryId);
        var res = await addressRepository.CreateAsync(address);
        res.SetCountry(country);
        return ObjectMapper.Map<Address, AddressDetailedDto>(res); 
    }

    [Authorize(UcetnictviPermissions.UpdateAddressPermission)]
    public async Task<AddressDetailedDto> UpdateAsync(AddressUpdateInputDto input)
    {
        var country = await countryRepository.GetAsync(input.CountryId);
        var address = await addressRepository.GetAsync(input.Id);
        address.SetCity(input.City).SetStreet(input.Street)
            .SetPostalCode(input.PostalCode).SetCountry(input.CountryId);
        var res = await addressRepository.UpdateAsync(address);
        res.SetCountry(country);
        return ObjectMapper.Map<Address, AddressDetailedDto>(res); 
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