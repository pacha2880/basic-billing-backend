using System;

namespace BasicBilling.Domain.Entities;

public class Payment
{
    public int Id { get; set; }

    public int BillId { get; set; }
    public Bill? Bill { get; set; }

    public DateTime PaidAt { get; set; }

    public decimal AmountPaid { get; set; }
}
