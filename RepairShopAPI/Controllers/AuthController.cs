using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
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
        
        [HttpPost("registerEmployee")]
        public async Task<IActionResult> RegisterEmployee([FromBody] Employee employee)
        {
            try
            {
                if (_db.Employees.Any(u => u.Email == employee.Email))
                    return BadRequest("Этот email уже используется");

                if (!string.IsNullOrEmpty(employee.Phone) && _db.Employees.Any(u => u.Phone == employee.Phone))
                    return BadRequest("Этот номер телефона уже используется");

                employee.Password = Functions.GetHash(employee.Password);

                await _db.Employees.AddAsync(employee);
                await _db.SaveChangesAsync();

                _logger.LogInformation($"Зарегистрирован новый сотрудник: {employee.Firstname} {employee.Lastname} (ID: {employee.Id})");

                return Ok(new { employee.Firstname, employee.Lastname, employee.Role });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при регистрации сотрудника");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPost("registerClient")]
        public async Task<IActionResult> RegisterClient([FromBody] Client client)
        {
            try
            {
                if (_db.Clients.Any(u => u.Email == client.Email))
                    return BadRequest("Этот email уже используется");

                if (!string.IsNullOrEmpty(client.Phone) && _db.Clients.Any(u => u.Phone == client.Phone))
                    return BadRequest("Этот номер телефона уже используется");

                client.Password = Functions.GetHash(client.Password);

                await _db.Clients.AddAsync(client);
                await _db.SaveChangesAsync();

                _logger.LogInformation($"Зарегистрирован новый клиент: {client.Firstname} {client.Lastname} (ID: {client.Id})");

                return Ok(new { client.Firstname, client.Lastname });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при регистрации клиента");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPost("loginClient")]
        public IActionResult LoginClient([FromBody] LoginRequest login)
        {
            try
            {
                var client = _db.Clients.FirstOrDefault(u =>
                    u.Email == login.Email && u.Password == Functions.GetHash(login.Password));

                if (client == null)
                {
                    _logger.LogWarning($"Неудачная попытка входа: {login.Email}");
                    return Unauthorized("Неверный email или пароль");
                }

                var token = _tokenService.GenerateToken(client);
                _logger.LogInformation($"Успешный вход клиента: {client.Email}");

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при входе клиента");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPost("loginEmployee")]
        public IActionResult LoginEmployee([FromBody] LoginRequest login)
        {
            try
            {
                var employee = _db.Employees.FirstOrDefault(u =>
                    u.Email == login.Email && u.Password == Functions.GetHash(login.Password));

                if (employee == null)
                {
                    _logger.LogWarning($"Неудачная попытка входа: {login.Email}");
                    return Unauthorized("Неверный email или пароль");
                }

                var token = _tokenService.GenerateToken(employee);
                _logger.LogInformation($"Успешный вход клиента: {employee.Email}");

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
