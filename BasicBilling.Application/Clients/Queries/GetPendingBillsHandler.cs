using AutoMapper;
using BasicBilling.Application.DTOs;
using BasicBilling.Domain.Interfaces;
using MediatR;

namespace BasicBilling.Application.Clients.Queries;

public class GetPendingBillsHandler : IRequestHandler<GetPendingBillsQuery, IEnumerable<BillDto>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IBillRepository _billRepository;
    private readonly IMapper _mapper;

    public GetPendingBillsHandler(
        IClientRepository clientRepository,
        IBillRepository billRepository,
        IMapper mapper)
    {
        _clientRepository = clientRepository;
        _billRepository = billRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BillDto>> Handle(GetPendingBillsQuery request, CancellationToken cancellationToken)
    {
        if (!await _clientRepository.ExistsAsync(request.ClientId))
        {
            throw new KeyNotFoundException("Client not found.");
        }

        var bills = await _billRepository.GetPendingBillsByClientAsync(request.ClientId);
        return _mapper.Map<IEnumerable<BillDto>>(bills);
    }
}
