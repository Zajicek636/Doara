using System;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Dto.Subject;

public class SubjectDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!;
    public Guid AddressId { get; set; }
    public string Ic { get; set; } = null!;
    public string Dic { get; set; } = null!;
    public bool IsVatPayer { get; set; }
}