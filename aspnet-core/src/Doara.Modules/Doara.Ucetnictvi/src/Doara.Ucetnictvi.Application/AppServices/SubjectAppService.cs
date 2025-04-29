using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.Address;
using Doara.Ucetnictvi.Dto.Subject;
using Doara.Ucetnictvi.Entities;
using Doara.Ucetnictvi.IAppServices;
using Doara.Ucetnictvi.Permissions;
using Doara.Ucetnictvi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Doara.Ucetnictvi.AppServices;

public class SubjectAppService(ISubjectRepository subjectRepository, IAddressRepository addressRepository) : UcetnictviAppService, ISubjectAppService
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