using RepairShopAPI.Models;
namespace RepairShopAPI.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Client client);
        string GenerateToken(Employee employee);
    }
}