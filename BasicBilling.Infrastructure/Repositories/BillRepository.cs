using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicBilling.Domain.Entities;
using BasicBilling.Domain.Enums;
using BasicBilling.Domain.Interfaces;
using BasicBilling.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BasicBilling.Infrastructure.Repositories;

public class BillRepository : IBillRepository
{
    private readonly AppDbContext _db;

    public BillRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<Bill?> GetPendingBillAsync(int clientId, ServiceType serviceType, string billingPeriod)
    {
        return _db.Bills
            .Where(b => b.ClientId == clientId
                        && b.ServiceType == serviceType
                        && b.BillingPeriod == billingPeriod
                        && b.Status == BillStatus.Pending)
            .FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<Bill>> GetPendingBillsByClientAsync(int clientId)
    {
        var bills = await _db.Bills
            .Where(b => b.ClientId == clientId && b.Status == BillStatus.Pending)
            .ToListAsync();

        return bills;
    }

    public Task AddAsync(Bill bill)
    {
        _db.Bills.Add(bill);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
    {
        return _db.SaveChangesAsync();
    }
}
