using System;
using Volo.Abp.Application.Dtos;

namespace Doara.Ucetnictvi.Dto.Country;

public class CountryDto : EntityDto<Guid>
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}