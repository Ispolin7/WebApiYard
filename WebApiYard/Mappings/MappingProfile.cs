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
            CreateMap<Controllers.ViewModels.CustomerView, Services.Models.CustomerServiceModel>()
                .ForMember(d => d.FirstName, s => s.MapFrom(i => i.Name));
            CreateMap<Services.Models.CustomerServiceModel, Controllers.ViewModels.CustomerView>()
                .ForMember(d => d.Name, s => s.MapFrom(i => i.FirstName));

            CreateMap<Controllers.ValidationModels.OrderCreate, Services.Models.OrderServiceModel>();
            CreateMap<Controllers.ValidationModels.OrderUpdate, Services.Models.OrderServiceModel>();

            CreateMap<Controllers.ValidationModels.ProductCreate, Services.Models.ProductServiceModel>();
            CreateMap<Controllers.ValidationModels.ProductUpdate, Services.Models.ProductServiceModel>();

            CreateMap<Controllers.ValidationModels.OrderItemCreate, Controllers.ViewModels.OrderItemView>();
            CreateMap<Controllers.ValidationModels.OrderItemUpdate, Controllers.ViewModels.OrderItemView>();

            //CreateMap<Repositories.Models.Address, Services.Models.Address>();
            //CreateMap<Services.Models.Customer, Repositories.Models.Customer>().ReverseMap();
        }
    }
}
