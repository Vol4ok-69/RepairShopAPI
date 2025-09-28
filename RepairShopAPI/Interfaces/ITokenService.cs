using RepairShopAPI.Models;
namespace RepairShopAPI.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}