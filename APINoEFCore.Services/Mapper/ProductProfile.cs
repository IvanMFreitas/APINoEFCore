using AutoMapper;
using APINoEFCore.Entities.ViewModels;
using APINoEFCore.Entities.Models;

namespace APINoEFCore.Services.Mapper{
    public class ProductProfile : Profile
    {
        public ProductProfile(){
            CreateMap<Product, ProductViewModel>()
                .ForMember(t => t.Id, o => o.MapFrom(t => t.Id))
                .ForMember(t => t.Name, o => o.MapFrom(t => t.Name))
                .ForMember(t => t.Price, o => o.MapFrom(t => t.Price))
                .ForMember(t => t.CreatedAt, o => o.MapFrom(t => t.CreatedAt))
                .ReverseMap();

            CreateMap<ProductViewModel, Product>()
                .ForMember(t => t.Id, o => o.MapFrom(t => t.Id))
                .ForMember(t => t.Name, o => o.MapFrom(t => t.Name))
                .ForMember(t => t.Price, o => o.MapFrom(t => t.Price))
                .ForMember(t => t.CreatedAt, o => o.MapFrom(t => t.CreatedAt))
                .ReverseMap();
        }
    }
}
