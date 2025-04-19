using System;
using System.ComponentModel.DataAnnotations;
using Doara.Ucetnictvi.Constants;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Dto.Subject;

public class SubjectUpdateInputDto : EntityDto<Guid>
{
    [Required]
    [StringLength(SubjectConstants.MaxNameLength)]
    public string Name { get; set; } = null!;
    
    [Required]
    public Guid AddressId { get; set; }
    
    [StringLength(SubjectConstants.MaxNameLength)]
    public string Ic { get; set; } = null!;
    
    [StringLength(SubjectConstants.MaxNameLength)]
    public string Dic { get; set; } = null!;
    
    [Required]
    public bool IsVatPayer { get; set; }
}