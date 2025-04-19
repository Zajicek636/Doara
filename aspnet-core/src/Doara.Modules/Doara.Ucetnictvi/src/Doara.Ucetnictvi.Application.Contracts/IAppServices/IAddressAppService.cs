using System;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.Address;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Doara.Ucetnictvi.IAppServices;

public interface IAddressAppService : IApplicationService
{
    Task<AddressDetailedDto> GetAsync(Guid id);
    Task<PagedResultDto<AddressDetailedDto>> GetAllAsync(PagedAndSortedResultRequestDto input);
    Task<AddressDetailedDto> CreateAsync(AddressCreateInputDto input);
    Task<AddressDetailedDto> UpdateAsync(AddressUpdateInputDto input);
    Task DeleteAsync(Guid id);
}