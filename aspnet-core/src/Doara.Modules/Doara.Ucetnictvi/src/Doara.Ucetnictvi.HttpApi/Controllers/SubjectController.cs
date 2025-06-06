﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.Subject;
using Doara.Ucetnictvi.IAppServices;
using Doara.Ucetnictvi.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Controllers;

[Area(UcetnictviRemoteServiceConsts.ModuleName)]
[RemoteService(Name = UcetnictviRemoteServiceConsts.RemoteServiceName)]
[Route("api/Ucetnictvi/Subject")]
public class SubjectController(ISubjectAppService subjectAppService) : UcetnictviController, ISubjectAppService
{
    [HttpGet]
    [Authorize(UcetnictviPermissions.ReadSubjectPermission)]
    public async Task<SubjectDetailDto> GetAsync([Required] Guid id)
    {
        return await subjectAppService.GetAsync(id);
    }
    
    [HttpGet("GetAll")]
    [Authorize(UcetnictviPermissions.ReadSubjectPermission)]
    public async Task<PagedResultDto<SubjectDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
    {
        return await subjectAppService.GetAllAsync(input);
    }
    
    [HttpGet("GetAllWithDetail")]
    [Authorize(UcetnictviPermissions.ReadSubjectPermission)]
    public async Task<PagedResultDto<SubjectDetailDto>> GetAllWithDetailAsync(PagedAndSortedResultRequestDto input)
    {
        return await subjectAppService.GetAllWithDetailAsync(input);
    }

    [HttpPost]
    [Authorize(UcetnictviPermissions.CreateSubjectPermission)]
    public async Task<SubjectDetailDto> CreateAsync(SubjectCreateInputDto input)
    {
        return await subjectAppService.CreateAsync(input);
    }
    
    [HttpPost("CreateWithAddress")]
    [Authorize(UcetnictviPermissions.CreateSubjectPermission)]
    [Authorize(UcetnictviPermissions.CreateAddressPermission)]
    public async Task<SubjectDetailDto> CreateWithAddressAsync(SubjectWithAddressCreateInputDto input)
    {
        return await subjectAppService.CreateWithAddressAsync(input);
    }

    [HttpPut]
    [Authorize(UcetnictviPermissions.UpdateSubjectPermission)]
    public async Task<SubjectDetailDto> UpdateAsync(Guid id, SubjectUpdateInputDto input)
    {
        return await subjectAppService.UpdateAsync(id, input);
    }
    
    [HttpPut("UpdateWithAddress")]
    [Authorize(UcetnictviPermissions.UpdateSubjectPermission)]
    [Authorize(UcetnictviPermissions.UpdateAddressPermission)]
    public async Task<SubjectDetailDto> UpdateWithAddressAsync(Guid id, Guid addressId, SubjectWithAddressUpdateInputDto input)
    {
        return await subjectAppService.UpdateWithAddressAsync(id, addressId, input);
    }
    
    [HttpDelete]
    [Authorize(UcetnictviPermissions.DeleteSubjectPermission)]
    public async Task DeleteAsync([Required] Guid id)
    {
        await subjectAppService.DeleteAsync(id);
    }
}