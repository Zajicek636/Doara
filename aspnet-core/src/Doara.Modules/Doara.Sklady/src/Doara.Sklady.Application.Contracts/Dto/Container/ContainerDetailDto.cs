using System;
using System.Collections.Generic;
using Doara.Sklady.Dto.ContainerItem;
using Volo.Abp.Application.Dtos;

namespace Doara.Sklady.Dto.Container;

public class ContainerDetailDto : EntityDto<Guid> 
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<ContainerItemDto> Items { get; set; } = [];
}