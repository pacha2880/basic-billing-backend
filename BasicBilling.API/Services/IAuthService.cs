using System.Threading.Tasks;

namespace BasicBilling.API.Services;

public interface IAuthService
{
    Task<string> GenerateTokenAsync(int clientId);
}
