using AutoMapper;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Products.Commands;

namespace ProductCatalog.Application.Mappings;
public class DTOToCommandMappingProfile : Profile
{
    public DTOToCommandMappingProfile()
    {
        CreateMap<ProductDTO, ProductCreateCommand>();
        CreateMap<ProductDTO, ProductUpdateCommand>();
    }
}
