using AutoMapper;
using NewProject.API.Requests.Order;
using NewProject.API.Requests.Payment;
using NewProject.Domain.Entities.Order;
using NewProject.Domain.Entities.Payment;
using NewProject.Services.Entities.Order;
using NewProject.Services.Entities.Payment;

namespace NewProject.API.Infrastructure.Automapper
{
    public class PaymentMappingProfile:Profile
    {
        public PaymentMappingProfile() 
        {
            CreateMap<GetPaymentRequest, GetPaymentDto>();
            CreateMap<Payment, GetPaymentDto>().ReverseMap();

            CreateMap<SavePaymentRequest, SavePaymentDto>();
            CreateMap<Payment, SavePaymentDto>().ReverseMap();

        }
    }
}
