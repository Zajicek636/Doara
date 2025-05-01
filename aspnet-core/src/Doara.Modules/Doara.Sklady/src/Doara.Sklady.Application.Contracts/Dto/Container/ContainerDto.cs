using System;
using Volo.Abp.Application.Dtos;

namespace Doara.Sklady.Dto.Container;

public class ContainerDto : EntityDto<Guid> 
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}