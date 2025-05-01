using System;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.Address;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Doara.Ucetnictvi.IAppServices;

public interface IAddressAppService : IApplicationService
{
    Task<AddressDetailDto> GetAsync(Guid id);
    Task<PagedResultDto<AddressDto>> GetAllAsync(PagedAndSortedResultRequestDto input);
    Task<PagedResultDto<AddressDetailDto>> GetAllWithDetailAsync(PagedAndSortedResultRequestDto input);
    Task<AddressDetailDto> CreateAsync(AddressCreateInputDto input);
    Task<AddressDetailDto> UpdateAsync(Guid id, AddressUpdateInputDto input);
    Task DeleteAsync(Guid id);
}