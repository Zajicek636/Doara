using System;
using Volo.Abp.Application.Dtos;

namespace Doara.Sklady.Dto.ContainerItem;

public class ContainerItemDto : EntityDto<Guid> 
{
    public string Name { get; set; } = null!;
}