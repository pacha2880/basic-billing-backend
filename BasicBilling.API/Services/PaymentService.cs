using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BasicBilling.Application.DTOs;
using BasicBilling.Domain.Entities;
using BasicBilling.Domain.Interfaces;

namespace BasicBilling.API.Services;

public class PaymentService : IPaymentService
{
    private readonly IClientRepository _clientRepository;
    private readonly IBillRepository _billRepository;
    private readonly IPaymentRepository _paymentRepository;

    private static readonly Regex PeriodRegex = new("^\\d{6}$");

    public PaymentService(
        IClientRepository clientRepository,
        IBillRepository billRepository,
        IPaymentRepository paymentRepository)
    {
        _clientRepository = clientRepository;
        _billRepository = billRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<PaymentHistoryDto> ProcessPaymentAsync(PaymentRequest request)
    {
        if (!await _clientRepository.ExistsAsync(request.ClientId))
        {
            throw new InvalidOperationException("Client does not exist.");
        }

        if (string.IsNullOrWhiteSpace(request.BillingPeriod) || !PeriodRegex.IsMatch(request.BillingPeriod))
        {
            throw new InvalidOperationException("BillingPeriod must be in format YYYYMM.");
        }

        var bill = await _billRepository.GetPendingBillAsync(request.ClientId, request.ServiceType, request.BillingPeriod);
        if (bill is null)
        {
            throw new InvalidOperationException("Pending bill not found.");
        }

        bill.Status = BasicBilling.Domain.Enums.BillStatus.Paid;

        var payment = new Payment
        {
            BillId = bill.Id,
            AmountPaid = bill.Amount,
            PaidAt = DateTime.UtcNow
        };

        await _paymentRepository.AddAsync(payment);
        await _billRepository.SaveChangesAsync();
        await _paymentRepository.SaveChangesAsync();

        return MapToDto(payment, bill);
    }

    public async Task<IReadOnlyCollection<PaymentHistoryDto>> GetPaymentHistoryAsync(int clientId)
    {
        if (!await _clientRepository.ExistsAsync(clientId))
        {
            throw new InvalidOperationException("Client does not exist.");
        }

        var payments = await _paymentRepository.GetPaymentHistoryByClientAsync(clientId);
        return payments
            .Select(p => MapToDto(p, p.Bill!))
            .ToArray();
    }

    private static PaymentHistoryDto MapToDto(Payment payment, Bill bill)
    {
        return new PaymentHistoryDto
        {
            BillId = payment.BillId,
            ServiceType = bill.ServiceType.ToString(),
            BillingPeriod = bill.BillingPeriod,
            AmountPaid = payment.AmountPaid,
            PaidAt = payment.PaidAt,
            Status = "Paid"
        };
    }
}
