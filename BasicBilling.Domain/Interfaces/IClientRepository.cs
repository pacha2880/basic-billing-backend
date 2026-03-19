using System.Threading.Tasks;
using BasicBilling.Domain.Entities;

namespace BasicBilling.Domain.Interfaces;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
}
