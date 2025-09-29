using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepairShopAPI.Models;

namespace RepairShopAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]
    public class UserController(RepairShopContext db, ILogger<UserController> logger) : ControllerBase
    {
        private readonly ILogger<UserController> _logger = logger;
        private readonly RepairShopContext _db = db;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<User> users = await _db.Users.ToListAsync();
                _logger.LogInformation($"Запрошен список всех пользователей. Найдено: {users.Count}");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении всех пользователей");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                User user = await _db.Users.FirstOrDefaultAsync(e => e.Id == id);
                if (user == null)
                {
                    _logger.LogWarning($"Попытка удаления несуществующего пользователя с id: {id}");
                    return NotFound("Не удалось найти пользователя с таким id");
                }
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при удалении пользователя {id}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
    }
}