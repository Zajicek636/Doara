using System;
using Doara.Ucetnictvi.Dto.Address;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Dto.Subject;

public class SubjectDetailDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!;
    public string Ic { get; set; } = null!;
    public string Dic { get; set; } = null!;
    public bool IsVatPayer { get; set; }
    public AddressDetailDto Address { get; set; } = null!;
}