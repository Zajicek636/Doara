using System;
using System.Threading.Tasks;
using Doara.Ucetnictvi.Dto.Subject;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.IAppServices;

public interface ISubjectAppService
{
    Task<SubjectDto> GetAsync(Guid id);
    Task<PagedResultDto<SubjectDto>> GetAllAsync(PagedAndSortedResultRequestDto input);
    Task<SubjectDto> CreateAsync(SubjectCreateInputDto input);
    Task<SubjectDto> UpdateAsync(SubjectUpdateInputDto input);
    Task DeleteAsync(Guid id);
}