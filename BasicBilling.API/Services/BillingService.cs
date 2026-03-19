using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BasicBilling.Application.DTOs;
using BasicBilling.Domain.Entities;
using BasicBilling.Domain.Enums;
using BasicBilling.Domain.Interfaces;

namespace BasicBilling.API.Services;

public class BillingService : IBillingService
{
    private readonly IClientRepository _clientRepository;
    private readonly IBillRepository _billRepository;

    private static readonly Regex PeriodRegex = new("^\\d{6}$");

    public BillingService(IClientRepository clientRepository, IBillRepository billRepository)
    {
        _clientRepository = clientRepository;
        _billRepository = billRepository;
    }

    public async Task<BillDto> CreateBillAsync(CreateBillRequest request)
    {
        if (!await _clientRepository.ExistsAsync(request.ClientId))
        {
            throw new InvalidOperationException("Client does not exist.");
        }

        if (string.IsNullOrWhiteSpace(request.BillingPeriod) || !PeriodRegex.IsMatch(request.BillingPeriod))
        {
            throw new InvalidOperationException("BillingPeriod must be in format YYYYMM.");
        }

        if (request.Amount <= 0)
        {
            throw new InvalidOperationException("Amount must be greater than zero.");
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

        return MapToDto(bill);
    }

    public async Task<IReadOnlyCollection<BillDto>> GetPendingBillsAsync(int clientId)
    {
        if (!await _clientRepository.ExistsAsync(clientId))
        {
            throw new InvalidOperationException("Client does not exist.");
        }

        var bills = await _billRepository.GetPendingBillsByClientAsync(clientId);
        return bills.Select(MapToDto).ToArray();
    }

    private static BillDto MapToDto(Bill bill)
    {
        return new BillDto
        {
            Id = bill.Id,
            ClientId = bill.ClientId,
            ServiceType = bill.ServiceType.ToString(),
            BillingPeriod = bill.BillingPeriod,
            Amount = bill.Amount,
            Status = bill.Status.ToString(),
            CreatedAt = bill.CreatedAt
        };
    }
}
