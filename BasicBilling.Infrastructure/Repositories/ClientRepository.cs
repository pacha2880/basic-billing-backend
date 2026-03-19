using System.Threading.Tasks;
using BasicBilling.Domain.Entities;
using BasicBilling.Domain.Interfaces;
using BasicBilling.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BasicBilling.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _db;

    public ClientRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<Client?> GetByIdAsync(int id)
    {
        return _db.Clients.FindAsync(id).AsTask();
    }

    public Task<bool> ExistsAsync(int id)
    {
        return _db.Clients.AnyAsync(c => c.Id == id);
    }
}
