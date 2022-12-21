using AutoMapper;
using NewProject.API.Requests.Machine;
using NewProject.API.Requests.Order;
using NewProject.Domain.Entities.Machine;
using NewProject.Domain.Entities.Order;
using NewProject.Services.Entities.Machine;
using NewProject.Services.Entities.Order;

namespace NewProject.API.Infrastructure.Automapper
{
    public class OrderMappingProfile:Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<GetOrderRequest, GetOrderDto>();
            CreateMap<Order, GetOrderDto>().ReverseMap();

            CreateMap<SaveOrderRequest, SaveOrderDto>();
            CreateMap<Order, SaveOrderDto>().ReverseMap();

            CreateMap<UpdateOrderRequest, UpdateOrderDto  >();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();

            CreateMap<UpdateStatusrequest, UpdateStatusDto>();
            CreateMap<Order, UpdateStatusDto>().ReverseMap();



            CreateMap<DeleteOrderRequest, DeleteOrderDto>();
            CreateMap<Order, DeleteOrderDto>().ReverseMap();

        }
    }
}
