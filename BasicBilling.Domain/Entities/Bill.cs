using System;
using BasicBilling.Domain.Enums;

namespace BasicBilling.Domain.Entities;

public class Bill
{
    public int Id { get; set; }

    public int ClientId { get; set; }
    public Client? Client { get; set; }

    public ServiceType ServiceType { get; set; }

    public required string BillingPeriod { get; set; }

    public decimal Amount { get; set; }

    public BillStatus Status { get; set; } = BillStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Payment? Payment { get; set; }
}
