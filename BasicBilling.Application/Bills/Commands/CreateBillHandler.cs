using AutoMapper;
using BasicBilling.Application.DTOs;
using BasicBilling.Domain.Entities;
using BasicBilling.Domain.Enums;
using BasicBilling.Domain.Interfaces;
using MediatR;

namespace BasicBilling.Application.Bills.Commands;

public class CreateBillHandler : IRequestHandler<CreateBillCommand, BillDto>
{
    private readonly IClientRepository _clientRepository;
    private readonly IBillRepository _billRepository;
    private readonly IMapper _mapper;

    public CreateBillHandler(
        IClientRepository clientRepository,
        IBillRepository billRepository,
        IMapper mapper)
    {
        _clientRepository = clientRepository;
        _billRepository = billRepository;
        _mapper = mapper;
    }

    public async Task<BillDto> Handle(CreateBillCommand request, CancellationToken cancellationToken)
    {
        if (!await _clientRepository.ExistsAsync(request.ClientId))
        {
            throw new KeyNotFoundException("Client not found.");
        }

        var bill = new Bill
        {
            ClientId = request.ClientId,
            ServiceType = request.ServiceType,
            BillingPeriod = request.BillingPeriod,
            Amount = request.Amount,
            Status = BillStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        await _billRepository.AddAsync(bill);
        await _billRepository.SaveChangesAsync();

        return _mapper.Map<BillDto>(bill);
    }
}
