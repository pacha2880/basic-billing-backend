using BasicBilling.Application.DTOs;
using MediatR;

namespace BasicBilling.Application.Clients.Queries;

public record GetPendingBillsQuery(int ClientId) : IRequest<IEnumerable<BillDto>>;
