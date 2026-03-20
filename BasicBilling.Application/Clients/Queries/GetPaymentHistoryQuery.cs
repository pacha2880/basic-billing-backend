using BasicBilling.Application.DTOs;
using MediatR;

namespace BasicBilling.Application.Clients.Queries;

public record GetPaymentHistoryQuery(int ClientId) : IRequest<IEnumerable<PaymentHistoryDto>>;
