using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.Subject;
using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.IAppServices;
using Doara.Ucetnictvi.Permissions;
using Doara.Ucetnictvi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Doara.Ucetnictvi.AppServices;

public class SubjectAppService(ISubjectRepository subjectRepository, IAddressRepository addressRepository, ICountryRepository countryRepository) : UcetnictviAppService, ISubjectAppService
{
    [Authorize(UcetnictviPermissions.ReadSubjectPermission)]
    public async Task<SubjectDetailDto> GetAsync(Guid id)
    {
        var res = await subjectRepository.GetAsync(id);
        return ObjectMapper.Map<Subject, SubjectDetailDto>(res); 
    }

    [Authorize(UcetnictviPermissions.ReadSubjectPermission)]
    public async Task<PagedResultDto<SubjectDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
    {
        var res = await subjectRepository.GetAllAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(Subject.Id), false);
        var totalCount = await subjectRepository.GetCountAsync();
        return new PagedResultDto<SubjectDto>
        {
            Items = ObjectMapper.Map<List<Subject>, List<SubjectDto>>(res),
            TotalCount = totalCount
        };
    }
    
    [Authorize(UcetnictviPermissions.ReadSubjectPermission)]
    public async Task<PagedResultDto<SubjectDetailDto>> GetAllWithDetailAsync(PagedAndSortedResultRequestDto input)
    {
        var res = await subjectRepository.GetAllAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(Subject.Id), true);
        var totalCount = await subjectRepository.GetCountAsync();
        return new PagedResultDto<SubjectDetailDto>
        {
            Items = ObjectMapper.Map<List<Subject>, List<SubjectDetailDto>>(res),
            TotalCount = totalCount
        };
    }

    [Authorize(UcetnictviPermissions.CreateSubjectPermission)]
    public async Task<SubjectDetailDto> CreateAsync(SubjectCreateInputDto input)
    {
        var address = await addressRepository.GetAsync(input.AddressId);
        var guid = GuidGenerator.Create();
        var subject = new Subject(guid, input.Name, input.AddressId, input.Ic, input.Dic, input.IsVatPayer);
        var res = await subjectRepository.CreateAsync(subject);
        res.SetAddress(address);
        return ObjectMapper.Map<Subject, SubjectDetailDto>(res); 
    }
    
    [Authorize(UcetnictviPermissions.CreateAddressPermission)]
    [Authorize(UcetnictviPermissions.CreateSubjectPermission)]
    public async Task<SubjectDetailDto> CreateWithAddressAsync(SubjectWithAddressCreateInputDto input)
    {
        var country = await countryRepository.GetAsync(input.Address.CountryId);
        var addressGuid = GuidGenerator.Create();
        var guid = GuidGenerator.Create();
        
        var a = new Address(addressGuid, input.Address.Street, input.Address.City, input.Address.PostalCode, input.Address.CountryId);
        var subject = new Subject(guid, input.Name, addressGuid, input.Ic, input.Dic, input.IsVatPayer);
        
        var address = (await addressRepository.CreateAsync(a)).SetCountry(country);
        var res = (await subjectRepository.CreateAsync(subject)).SetAddress(address);
        return ObjectMapper.Map<Subject, SubjectDetailDto>(res); 
    }

    [Authorize(UcetnictviPermissions.UpdateSubjectPermission)]
    public async Task<SubjectDetailDto> UpdateAsync(SubjectUpdateInputDto input)
    {
        var address = await addressRepository.GetAsync(input.AddressId);
        var subject = await subjectRepository.GetAsync(input.Id);
        subject.SetName(input.Name).SetAddress(input.AddressId)
            .SetIc(input.Ic).SetDic(input.Dic).SetIsVatPayer(input.IsVatPayer);
        var res = await subjectRepository.UpdateAsync(subject);
        res.SetAddress(address);
        return ObjectMapper.Map<Subject, SubjectDetailDto>(res); 
    }
    
    [Authorize(UcetnictviPermissions.UpdateAddressPermission)]
    [Authorize(UcetnictviPermissions.UpdateSubjectPermission)]
    public async Task<SubjectDetailDto> UpdateWithAddressAsync(SubjectWithAddressUpdateInputDto input)
    {
        var country = await countryRepository.GetAsync(input.Address.CountryId);
        var a = await addressRepository.GetAsync(input.Address.Id);
        a.SetCity(input.Address.City).SetStreet(input.Address.Street)
            .SetPostalCode(input.Address.PostalCode).SetCountry(input.Address.CountryId);
        
        var subject = await subjectRepository.GetAsync(input.Id);
        subject.SetName(input.Name).SetAddress(input.Address.Id)
            .SetIc(input.Ic).SetDic(input.Dic).SetIsVatPayer(input.IsVatPayer);
        var address = (await addressRepository.UpdateAsync(a)).SetCountry(country);
        var res = (await subjectRepository.UpdateAsync(subject)).SetAddress(address);
        return ObjectMapper.Map<Subject, SubjectDetailDto>(res); 
    }

    [Authorize(UcetnictviPermissions.DeleteSubjectPermission)]
    public async Task DeleteAsync(Guid id)
    {
        if (!await subjectRepository.AnyAsync(x => x.Id == id))
        {
            throw new EntityNotFoundException(typeof(Subject), id);
        }
        await subjectRepository.DeleteAsync(id);
    }
}