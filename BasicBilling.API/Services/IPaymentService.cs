using System.Collections.Generic;
using System.Threading.Tasks;
using BasicBilling.Application.DTOs;

namespace BasicBilling.API.Services;

public interface IPaymentService
{
    Task<PaymentHistoryDto> ProcessPaymentAsync(PaymentRequest request);
    Task<IReadOnlyCollection<PaymentHistoryDto>> GetPaymentHistoryAsync(int clientId);
}
