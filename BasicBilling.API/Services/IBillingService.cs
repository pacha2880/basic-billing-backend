using System.Collections.Generic;
using System.Threading.Tasks;
using BasicBilling.Application.DTOs;

namespace BasicBilling.API.Services;

public interface IBillingService
{
    Task<BillDto> CreateBillAsync(CreateBillRequest request);
    Task<IReadOnlyCollection<BillDto>> GetPendingBillsAsync(int clientId);
}
