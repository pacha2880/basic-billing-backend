using BasicBilling.Domain.Enums;

namespace BasicBilling.Application.DTOs;

public class PaymentRequest
{
    public int ClientId { get; set; }
    public ServiceType ServiceType { get; set; }
    public required string BillingPeriod { get; set; }
}
