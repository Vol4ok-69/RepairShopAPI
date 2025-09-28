using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepairShopAPI;
using RepairShopAPI.Interfaces;
using RepairShopAPI.Models;

namespace RepairShopAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController(RepairShopContext db, ITokenService tokenService, ILogger<AuthController> logger) : ControllerBase
    {
        private readonly ITokenService _tokenService = tokenService;
        private readonly ILogger<AuthController> _logger = logger;
        private readonly RepairShopContext _db = db;

        [HttpPost("registerUser")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            try
            {
                #region Базовые проверки
                if (!Functions.IsEmail(user.Email) && user.Email != null)
                    return BadRequest("Необходим корректный email");

                if (!Functions.IsPhoneNumber(user.Phone) && user.Phone != null)
                    return BadRequest("Необходим корректный телефон");

                if (string.IsNullOrEmpty(user.Passwordhash))
                    return BadRequest("Необходим пароль");

                if (user.Passwordhash.Length < 7)
                    return BadRequest("Необходим пароль из не менее чем 8 символов");

                if (user.Passwordhash.Length > 25)
                    return BadRequest("Слишком длинный пароль, максимум 25 символов");

                if (_db.Users.Any(u => u.Email == user.Email))
                    return BadRequest("Этот email уже используется");

                if (_db.Users.Any(u => u.Phone == user.Phone))
                    return BadRequest("Этот номер телефона уже используется");
                #endregion

                user.Passwordhash = Functions.GetHash(user.Passwordhash);

                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();

                _logger.LogInformation($"Зарегистрирован новый {(user.Role.Name == "Клиент" ? "клиент" : "сотрудник")}: {user.Firstname} {user.Lastname} (ID: {user.Id})");

                return Ok(new { user.Firstname, user.Lastname, user.Role });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при регистрации сотрудника");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPost("loginUser")]
        public async Task<IActionResult> LoginEmployee([FromBody] LoginRequest loginUser)
        {
            try
            {
                User user = await _db.Users.FirstOrDefaultAsync(e =>
                    e.Email == loginUser.Email &&
                    loginUser.Password != null &&
                    e.Passwordhash == Functions.GetHash(loginUser.Password));
                
                if (user == null)
                {
                    _logger.LogWarning($"Неудачная попытка входа: {loginUser.Email}");
                    return Unauthorized("Не удалось найти пользователя с таким email и паролем");
                }
                var token = _tokenService.GenerateToken(user);
                _logger.LogInformation($"Успешный вход клиента: {user.Email}");

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при входе клиента");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            _logger.LogInformation($"Пользователь вышел из системы: {User.Identity?.Name}");
            return Ok("Успешный выход. Токен должен быть удален на клиенте.");
        }
    }
    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

}
