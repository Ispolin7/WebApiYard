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

            CreateMap<Controllers.ValidationModels.OrderCreate, Services.Models.Order>();
            CreateMap<Controllers.ValidationModels.OrderUpdate, Services.Models.Order>();

            CreateMap<Controllers.ValidationModels.ProductCreate, Services.Models.Product>();
            CreateMap<Controllers.ValidationModels.ProductUpdate, Services.Models.Product>();

            CreateMap<Controllers.ValidationModels.OrderItemCreate, Controllers.ViewModels.OrderItem>();
            CreateMap<Controllers.ValidationModels.OrderItemUpdate, Controllers.ViewModels.OrderItem>();

            //CreateMap<Repositories.Models.Address, Services.Models.Address>();
            //CreateMap<Services.Models.Customer, Repositories.Models.Customer>().ReverseMap();
        }
    }
}
