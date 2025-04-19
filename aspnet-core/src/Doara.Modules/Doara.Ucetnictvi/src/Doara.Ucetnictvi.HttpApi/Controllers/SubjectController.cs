using System;
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
    public async Task<SubjectDto> GetAsync([Required] Guid id)
    {
        return await subjectAppService.GetAsync(id);
    }
    
    [HttpGet("GetAll")]
    [Authorize(UcetnictviPermissions.ReadSubjectPermission)]
    public async Task<PagedResultDto<SubjectDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
    {
        return await subjectAppService.GetAllAsync(input);
    }

    [HttpPost]
    [Authorize(UcetnictviPermissions.CreateSubjectPermission)]
    public async Task<SubjectDto> CreateAsync(SubjectCreateInputDto input)
    {
        return await subjectAppService.CreateAsync(input);
    }
    
    [HttpPut]
    [Authorize(UcetnictviPermissions.UpdateSubjectPermission)]
    public async Task<SubjectDto> UpdateAsync(SubjectUpdateInputDto input)
    {
        return await subjectAppService.UpdateAsync(input);
    }
    
    [HttpDelete]
    [Authorize(UcetnictviPermissions.DeleteSubjectPermission)]
    public async Task DeleteAsync([Required] Guid id)
    {
        await subjectAppService.DeleteAsync(id);
    }
}