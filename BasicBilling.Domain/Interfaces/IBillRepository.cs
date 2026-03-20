using System.Collections.Generic;
using System.Threading.Tasks;
using BasicBilling.Domain.Entities;
using BasicBilling.Domain.Enums;

namespace BasicBilling.Domain.Interfaces;

public interface IBillRepository
{
    Task<Bill?> GetBillAsync(int clientId, ServiceType serviceType, string billingPeriod);
    Task<Bill?> GetPendingBillAsync(int clientId, ServiceType serviceType, string billingPeriod);
    Task<IReadOnlyCollection<Bill>> GetPendingBillsByClientAsync(int clientId);
    Task AddAsync(Bill bill);
    Task SaveChangesAsync();
}
