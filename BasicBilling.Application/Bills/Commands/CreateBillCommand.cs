using BasicBilling.Application.DTOs;
using BasicBilling.Domain.Enums;
using MediatR;

namespace BasicBilling.Application.Bills.Commands;

public record CreateBillCommand(
    int ClientId,
    ServiceType ServiceType,
    string BillingPeriod,
    decimal Amount) : IRequest<BillDto>;
