using System;
using Volo.Abp.Application.Dtos;

namespace Doara.Sklady.Dto;

public class ContainerDto : EntityDto<Guid> 
{
    public string Name { get; set; } = null!;
}