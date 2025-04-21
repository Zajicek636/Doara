using AutoMapper;
using Doara.Ucetnictvi.Dto.Address;
using Doara.Ucetnictvi.Dto.Country;
using Doara.Ucetnictvi.Dto.Invoice;
using Doara.Ucetnictvi.Dto.InvoiceItem;
using Doara.Ucetnictvi.Dto.Subject;
using Doara.Ucetnictvi.Entities;

namespace Doara.Ucetnictvi;

public class UcetnictviApplicationAutoMapperProfile : Profile
{
    public UcetnictviApplicationAutoMapperProfile()
    {
        CreateMap<Address, AddressDto>();
        CreateMap<Address, AddressDetailDto>();
        CreateMap<Country, CountryDto>();
        CreateMap<Subject, SubjectDto>();
        CreateMap<Subject, SubjectDetailDto>();
        CreateMap<InvoiceItem, InvoiceItemDto>();
        CreateMap<Invoice, InvoiceDto>();
    }
}
