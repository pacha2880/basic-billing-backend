using System.Collections.Generic;
using System.Threading.Tasks;
using BasicBilling.Domain.Entities;

namespace BasicBilling.Domain.Interfaces;

public interface IPaymentRepository
{
    Task<IReadOnlyCollection<Payment>> GetPaymentHistoryByClientAsync(int clientId);
    Task AddAsync(Payment payment);
    Task SaveChangesAsync();
}
