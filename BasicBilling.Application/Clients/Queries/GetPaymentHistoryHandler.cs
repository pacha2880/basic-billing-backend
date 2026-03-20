using AutoMapper;
using BasicBilling.Application.DTOs;
using BasicBilling.Domain.Interfaces;
using MediatR;

namespace BasicBilling.Application.Clients.Queries;

public class GetPaymentHistoryHandler : IRequestHandler<GetPaymentHistoryQuery, IEnumerable<PaymentHistoryDto>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public GetPaymentHistoryHandler(
        IClientRepository clientRepository,
        IPaymentRepository paymentRepository,
        IMapper mapper)
    {
        _clientRepository = clientRepository;
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PaymentHistoryDto>> Handle(GetPaymentHistoryQuery request, CancellationToken cancellationToken)
    {
        if (!await _clientRepository.ExistsAsync(request.ClientId))
        {
            throw new KeyNotFoundException("Client not found.");
        }

        var payments = await _paymentRepository.GetPaymentHistoryByClientAsync(request.ClientId);
        return _mapper.Map<IEnumerable<PaymentHistoryDto>>(payments);
    }
}
