using Doara.Sklady.Enums;

namespace Doara.Sklady.Dto.ContainerItem;

public class ContainerItemUpdateInputDto : ContainerItemCreateInputDto
{
    public ContainerItemState? State { get; set; }
}