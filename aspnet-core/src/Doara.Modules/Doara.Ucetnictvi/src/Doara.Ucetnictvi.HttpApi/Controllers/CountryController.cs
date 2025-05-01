using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.Country;
using Doara.Ucetnictvi.IAppServices;
using Doara.Ucetnictvi.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Controllers;

[Area(UcetnictviRemoteServiceConsts.ModuleName)]
[RemoteService(Name = UcetnictviRemoteServiceConsts.RemoteServiceName)]
[Route("api/Ucetnictvi/Country")]
public class CountryController(ICountryAppService countryAppService) : UcetnictviController, ICountryAppService
{
    [HttpGet("{id:guid}")]
    [Authorize(UcetnictviPermissions.ReadCountryPermission)]
    public async Task<CountryDto> GetAsync([Required] Guid id)
    {
        return await countryAppService.GetAsync(id);
    }
    
    [HttpGet("GetAll")]
    [Authorize(UcetnictviPermissions.ReadCountryPermission)]
    public async Task<PagedResultDto<CountryDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
    {
        return await countryAppService.GetAllAsync(input);
    }

    [HttpPost]
    [Authorize(UcetnictviPermissions.CreateCountryPermission)]
    public async Task<CountryDto> CreateAsync(CountryCreateInputDto input)
    {
        return await countryAppService.CreateAsync(input);
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(UcetnictviPermissions.UpdateCountryPermission)]
    public async Task<CountryDto> UpdateAsync([Required] Guid id, CountryUpdateInputDto input)
    {
        return await countryAppService.UpdateAsync(id, input);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(UcetnictviPermissions.DeleteCountryPermission)]
    public async Task DeleteAsync([Required] Guid id)
    {
        await countryAppService.DeleteAsync(id);
    }
}