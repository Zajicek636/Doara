using System;
using Volo.Abp.Application.Dtos;

namespace Doara.Sklady.Dto.ContainerItem;

public class ContainerItemGetAllDto : PagedAndSortedResultRequestDto
{
    public Guid? ContainerId { get; set; }
}