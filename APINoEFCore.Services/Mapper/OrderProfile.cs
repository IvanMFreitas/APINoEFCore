using AutoMapper;
using APINoEFCore.Entities;
using APINoEFCore.Entities.ViewModels;
using APINoEFCore.Entities.Models;

namespace APINoEFCore.Services.Mapper{
    public class OrderProfile : Profile
    {
        public OrderProfile(){
            CreateMap<Order, OrderViewModel>()
                .ForMember(t => t.Id, o => o.MapFrom(t => t.Id))
                .ForMember(t => t.Qty, o => o.MapFrom(t => t.Qty))
                .ForMember(t => t.Total, o => o.MapFrom(t => t.Total))
                .ForMember(t => t.PersonId, o => o.MapFrom(t => t.PersonId))
                .ForMember(t => t.ProductId, o => o.MapFrom(t => t.ProductId))
                .ForMember(t => t.CreatedAt, o => o.MapFrom(t => t.CreatedAt))
                .ReverseMap();
        }
    }
}
