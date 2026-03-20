using AutoMapper;
using BasicBilling.Application.DTOs;
using BasicBilling.Domain.Entities;
using BasicBilling.Domain.Enums;
using BasicBilling.Domain.Interfaces;
using MediatR;

namespace BasicBilling.Application.Payments.Commands;

public class ProcessPaymentHandler : IRequestHandler<ProcessPaymentCommand, PaymentHistoryDto>
{
    private readonly IClientRepository _clientRepository;
    private readonly IBillRepository _billRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IMapper _mapper;

    public ProcessPaymentHandler(
        IClientRepository clientRepository,
        IBillRepository billRepository,
        IPaymentRepository paymentRepository,
        IMapper mapper)
    {
        _clientRepository = clientRepository;
        _billRepository = billRepository;
        _paymentRepository = paymentRepository;
        _mapper = mapper;
    }

    public async Task<PaymentHistoryDto> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
    {
        if (!await _clientRepository.ExistsAsync(request.ClientId))
        {
            throw new KeyNotFoundException("Client not found.");
        }

        var bill = await _billRepository.GetPendingBillAsync(request.ClientId, request.ServiceType, request.BillingPeriod);
        if (bill is null)
        {
            var existingBill = await _billRepository.GetBillAsync(request.ClientId, request.ServiceType, request.BillingPeriod);
            if (existingBill is not null)
            {
                throw new InvalidOperationException("Bill already paid.");
            }

            throw new KeyNotFoundException("Bill not found.");
        }

        bill.Status = BillStatus.Paid;

        var payment = new Payment
        {
            BillId = bill.Id,
            AmountPaid = bill.Amount,
            PaidAt = DateTime.UtcNow
        };

        await _paymentRepository.AddAsync(payment);
        await _billRepository.SaveChangesAsync();
        await _paymentRepository.SaveChangesAsync();

        // Set navigation property for mapping
        payment.Bill = bill;

        return _mapper.Map<PaymentHistoryDto>(payment);
    }
}
