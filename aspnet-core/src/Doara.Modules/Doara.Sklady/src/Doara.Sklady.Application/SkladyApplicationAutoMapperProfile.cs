using AutoMapper;
using Doara.Sklady.Dto.Container;
using Doara.Sklady.Dto.ContainerItem;
using Doara.Sklady.Dto.StockMovement;
using Doara.Sklady.Entities;

namespace Doara.Sklady;

public class SkladyApplicationAutoMapperProfile : Profile
{
    public SkladyApplicationAutoMapperProfile()
    {
        CreateMap<Container, ContainerDto>();
        CreateMap<Container, ContainerDetailDto>();
        CreateMap<ContainerItem, ContainerItemDto>();
        CreateMap<ContainerItem, ContainerItemDetailDto>();
        CreateMap<StockMovement, StockMovementDto>();
    }
}
