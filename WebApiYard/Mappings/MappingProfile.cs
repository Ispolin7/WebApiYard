using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiYard.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Controllers.ViewModels.Customer, Services.Models.Customer>()
                .ForMember(d => d.FirstName, s => s.MapFrom(i => i.Name));
            CreateMap<Services.Models.Customer, Controllers.ViewModels.Customer>()
                .ForMember(d => d.Name, s => s.MapFrom(i => i.FirstName));

            //CreateMap<Services.Models.Customer, Repositories.Models.Customer>().ReverseMap();
        }
    }
}
