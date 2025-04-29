using System;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.Country;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.IAppServices;

public interface ICountryAppService
{
    Task<CountryDto> GetAsync(Guid id);
    Task<PagedResultDto<CountryDto>> GetAllAsync(PagedAndSortedResultRequestDto input);
    Task<CountryDto> CreateAsync(CountryCreateInputDto input);
    Task<CountryDto> UpdateAsync(CountryUpdateInputDto input);
    Task DeleteAsync(Guid id);
}