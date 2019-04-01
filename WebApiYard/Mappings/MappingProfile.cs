using AutoMapper;
using WebApiYard.Controllers.ValidationModels;
using WebApiYard.Services.Models;

namespace WebApiYard.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<CustomerValidation, CustomerServiceModel>()
            //    .ForMember(d => d.FirstName, s => s.MapFrom(i => i.Name));
            //CreateMap<CustomerServiceModel, CustomerValidation>()
            //    .ForMember(d => d.Name, s => s.MapFrom(i => i.FirstName));

            CreateMap<CustomerCreate, CustomerServiceModel>();
            CreateMap<CustomerUpdate, CustomerServiceModel>();

            CreateMap<OrderCreate, OrderServiceModel>();
            CreateMap<OrderUpdate, OrderServiceModel>();

            CreateMap<ProductCreate, ProductServiceModel>();
            CreateMap<ProductUpdate, ProductServiceModel>();

            CreateMap<OrderItemCreate, OrderItemServiceModel>();
            CreateMap<OrderItemUpdate, OrderItemServiceModel>();

            //CreateMap<Repositories.Models.Address, Services.Models.Address>();
            //CreateMap<Services.Models.Customer, Repositories.Models.Customer>().ReverseMap();
        }
    }
}
