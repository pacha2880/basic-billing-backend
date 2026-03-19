using System.Collections.Generic;

namespace BasicBilling.Domain.Entities;

public class Client
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
