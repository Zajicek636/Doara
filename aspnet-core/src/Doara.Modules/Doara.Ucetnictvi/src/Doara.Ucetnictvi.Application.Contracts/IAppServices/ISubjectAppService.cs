using System;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.Subject;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.IAppServices;

public interface ISubjectAppService
{
    Task<SubjectDetailDto> GetAsync(Guid id);
    Task<PagedResultDto<SubjectDto>> GetAllAsync(PagedAndSortedResultRequestDto input);
    Task<PagedResultDto<SubjectDetailDto>> GetAllWithDetailAsync(PagedAndSortedResultRequestDto input);
    Task<SubjectDetailDto> CreateAsync(SubjectCreateInputDto input);
    Task<SubjectDetailDto> CreateWithAddressAsync(SubjectWithAddressCreateInputDto input);
    Task<SubjectDetailDto> UpdateAsync(SubjectUpdateInputDto input);
    Task<SubjectDetailDto> UpdateWithAddressAsync(SubjectWithAddressUpdateInputDto input);
    Task DeleteAsync(Guid id);
}