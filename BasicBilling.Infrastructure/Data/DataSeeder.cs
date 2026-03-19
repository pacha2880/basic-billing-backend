using System;
using System.Linq;
using System.Threading.Tasks;
using BasicBilling.Domain.Entities;
using BasicBilling.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace BasicBilling.Infrastructure.Data;

public static class DataSeeder
{
    private static readonly int[] ClientIds = { 100, 200, 300, 400, 500 };
    private static readonly string[] ClientNames =
    {
        "Joseph Carlton",
        "Maria Juarez",
        "Albert Kenny",
        "Jessica Phillips",
        "Charles Johnson"
    };

    private static readonly string[] BillingPeriods = { "202501", "202502" };

    private static readonly ServiceType[] ServiceTypes =
    {
        ServiceType.Water,
        ServiceType.Electricity,
        ServiceType.Sewer
    };

    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Clients.AnyAsync())
        {
            return;
        }

        for (var i = 0; i < ClientIds.Length; i++)
        {
            db.Clients.Add(new Client
            {
                Id = ClientIds[i],
                Name = ClientNames[i],
            });
        }

        var rnd = new Random(1234);
        foreach (var clientId in ClientIds)
        {
            foreach (var period in BillingPeriods)
            {
                foreach (var service in ServiceTypes)
                {
                    var amount = Convert.ToDecimal(rnd.Next(20, 151));
                    db.Bills.Add(new Bill
                    {
                        ClientId = clientId,
                        ServiceType = service,
                        BillingPeriod = period,
                        Amount = amount,
                        Status = BillStatus.Pending,
                        CreatedAt = DateTime.UtcNow,
                    });
                }
            }
        }

        await db.SaveChangesAsync();
    }
}
