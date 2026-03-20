using AutoMapper;
using BasicBilling.Application.DTOs;
using BasicBilling.Domain.Entities;

namespace BasicBilling.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Bill, BillDto>()
            .ForMember(d => d.ServiceType, opt => opt.MapFrom(s => s.ServiceType.ToString()))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));

        CreateMap<Payment, PaymentHistoryDto>()
            .ForMember(d => d.BillId, opt => opt.MapFrom(s => s.BillId))
            .ForMember(d => d.ServiceType, opt => opt.MapFrom(s => s.Bill!.ServiceType.ToString()))
            .ForMember(d => d.BillingPeriod, opt => opt.MapFrom(s => s.Bill!.BillingPeriod))
            .ForMember(d => d.AmountPaid, opt => opt.MapFrom(s => s.AmountPaid))
            .ForMember(d => d.PaidAt, opt => opt.MapFrom(s => s.PaidAt))
            .ForMember(d => d.Status, opt => opt.MapFrom(_ => "Paid"));
    }
}
