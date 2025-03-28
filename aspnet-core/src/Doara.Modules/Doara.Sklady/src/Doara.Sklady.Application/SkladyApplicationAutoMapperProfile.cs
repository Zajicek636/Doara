using AutoMapper;
using Doara.Sklady.Dto;
using Doara.Sklady.Entities;

namespace Doara.Sklady;

public class SkladyApplicationAutoMapperProfile : Profile
{
    public SkladyApplicationAutoMapperProfile()
    {
        CreateMap<Container, ContainerDto>();
    }
}
