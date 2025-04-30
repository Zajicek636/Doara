using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.Address;
using Doara.Ucetnictvi.IAppServices;
using Doara.Ucetnictvi.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Controllers;

[Area(UcetnictviRemoteServiceConsts.ModuleName)]
[RemoteService(Name = UcetnictviRemoteServiceConsts.RemoteServiceName)]
[Route("api/Ucetnictvi/Address")]
public class AddressController(IAddressAppService addressAppService) : UcetnictviController, IAddressAppService
{
    [HttpGet("{id:guid}")]
    [Authorize(UcetnictviPermissions.ReadAddressPermission)]
    public async Task<AddressDetailDto> GetAsync([FromRoute][Required] Guid id)
    {
        return await addressAppService.GetAsync(id);
    }
    
    [HttpGet("GetAll")]
    [Authorize(UcetnictviPermissions.ReadAddressPermission)]
    public async Task<PagedResultDto<AddressDto>> GetAllAsync(PagedAndSortedResultRequestDto input)
    {
        return await addressAppService.GetAllAsync(input);
    }
    
    [HttpGet("GetAllWithDetail")]
    [Authorize(UcetnictviPermissions.ReadAddressPermission)]
    public async Task<PagedResultDto<AddressDetailDto>> GetAllWithDetailAsync(PagedAndSortedResultRequestDto input)
    {
        return await addressAppService.GetAllWithDetailAsync(input);
    }
    
    [HttpPost]
    [Authorize(UcetnictviPermissions.CreateAddressPermission)]
    public async Task<AddressDetailDto> CreateAsync(AddressCreateInputDto input)
    {
        return await addressAppService.CreateAsync(input);
    }
    
    [HttpPut("{id:guid}")]
    [Authorize(UcetnictviPermissions.UpdateAddressPermission)]
    public async Task<AddressDetailDto> UpdateAsync([Required] Guid id, AddressUpdateInputDto input)
    {
        return await addressAppService.UpdateAsync(id, input);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(UcetnictviPermissions.DeleteAddressPermission)]
    public async Task DeleteAsync([Required] Guid id)
    {
        await addressAppService.DeleteAsync(id);
    }
}