namespace BasicBilling.Application.DTOs;

public class PaymentHistoryDto
{
    public int BillId { get; set; }
    public required string ServiceType { get; set; }
    public required string BillingPeriod { get; set; }
    public decimal AmountPaid { get; set; }
    public DateTime PaidAt { get; set; }
    public required string Status { get; set; }
}
