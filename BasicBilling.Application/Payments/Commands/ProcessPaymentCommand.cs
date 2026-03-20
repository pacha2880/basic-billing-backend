using BasicBilling.Application.DTOs;
using BasicBilling.Domain.Enums;
using MediatR;

namespace BasicBilling.Application.Payments.Commands;

public record ProcessPaymentCommand(
    int ClientId,
    ServiceType ServiceType,
    string BillingPeriod) : IRequest<PaymentHistoryDto>;
