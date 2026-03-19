using BasicBilling.Domain.Enums;

namespace BasicBilling.Application.DTOs;

public class CreateBillRequest
{
    public int ClientId { get; set; }
    public ServiceType ServiceType { get; set; }
    public required string BillingPeriod { get; set; }
    public decimal Amount { get; set; }
}
