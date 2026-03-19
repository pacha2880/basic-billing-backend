using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicBilling.Domain.Entities;
using BasicBilling.Domain.Interfaces;
using BasicBilling.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BasicBilling.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _db;

    public PaymentRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyCollection<Payment>> GetPaymentHistoryByClientAsync(int clientId)
    {
        var payments = await _db.Payments
            .Include(p => p.Bill)
            .Where(p => p.Bill != null && p.Bill.ClientId == clientId)
            .OrderBy(p => p.PaidAt)
            .ToListAsync();

        return payments;
    }

    public Task AddAsync(Payment payment)
    {
        _db.Payments.Add(payment);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
    {
        return _db.SaveChangesAsync();
    }
}
