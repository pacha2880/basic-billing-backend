namespace BasicBilling.Application.DTOs;

public class BillDto
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public required string ServiceType { get; set; }
    public required string BillingPeriod { get; set; }
    public decimal Amount { get; set; }
    public required string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
