using AutoMapper;
using APINoEFCore.Entities.ViewModels;
using APINoEFCore.Entities.Models;

namespace APINoEFCore.Services.Mapper{
    public class PersonProfile : Profile
    {
        public PersonProfile(){
            CreateMap<Person, PersonViewModel>()
                .ForMember(t => t.Id, o => o.MapFrom(t => t.Id))
                .ForMember(t => t.Name, o => o.MapFrom(t => t.Name))
                .ForMember(t => t.Email, o => o.MapFrom(t => t.Email))
                .ForMember(t => t.IsAdmin, o => o.MapFrom(t => t.IsAdmin))
                .ForMember(t => t.CreatedAt, o => o.MapFrom(t => t.CreatedAt))
                .ReverseMap();
        }
    }
}
