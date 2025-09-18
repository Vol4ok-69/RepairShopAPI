using RepairShopAPI.Models;
using RepairShopAPI;
using RepairShopAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RepairShopAPI.Controllers
{   
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthController> _logger;
        private readonly RepairShopContext _db;

        public AuthController(RepairShopContext db, ITokenService tokenService, ILogger<AuthController> logger)
        {
            _db = db;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("registerEmployee")]
        public async Task<IActionResult> Register([FromBody] Employee employee)
        {
            try
            {
                if (_db.Employees.Any(u => u.Id == employee.Id))
                {
                    _logger.LogWarning($"Попытка регистрации существующего сотрудника: {employee.Id}");
                    return StatusCode(500, "Внутренняя ошибка сервера");
                }
                else if (_db.Employees.Any(u => u.Email == employee.Email))
                {
                    _logger.LogWarning($"Попытка регистрации сотрудника с уже привязанным email: {employee.Email}");
                    return BadRequest("Этот email уже привязан к другому сотруднику");
                }
                else if (_db.Employees.Any(u => u.Phone == employee.Phone))
                {
                    _logger.LogWarning($"Попытка регистрации сотрудника с уже привязанным номером телефона: {employee.Phone}");
                    return BadRequest("Этот номер телефона уже привязан к другому сотруднику");
                }

                await _db.AddAsync(employee);

                _logger.LogInformation($"Зарегистрирован новый сотрудник: {employee.Firstname}, {employee.Lastname} (ID: {employee.Id})");

                return Ok(new { employee.Firstname,employee.Lastname, employee.Role });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при регистрации сотрудника");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
        [HttpPost("registerClient")]
        public async Task<IActionResult> Register([FromBody] Client client)
        {
            try
            {
                if (_db.Clients.Any(u => u.Id == client.Id))
                {
                    _logger.LogWarning($"Попытка регистрации существующего клиента: {client.Id}");
                    return StatusCode(500, "Внутренняя ошибка сервера");
                }
                else if (_db.Clients.Any(u => u.Email == client.Email))
                {
                    _logger.LogWarning($"Попытка регистрации клиента с уже привязанным email: {client.Email}");
                    return BadRequest("Этот email уже привязан к другому клиенту");
                }
                else if (_db.Clients.Any(u => u.Phone == client.Phone))
                {
                    _logger.LogWarning($"Попытка регистрации с уже привязанным номером телефона: {client.Phone}");
                    return BadRequest("Этот номер телефона уже привязан к другому клиенту");
                }

                await _db.AddAsync(client);

                _logger.LogInformation($"Зарегистрирован новый клиент: {client.Firstname}, {client.Lastname} (ID: {client.Id})");

                return Ok(new { client.Firstname, client.Lastname });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при регистрации клиента");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User login)
        {
            try
            {
                var user = _userRepo.GetAll().FirstOrDefault(u =>
                    u.Username == login.Username && u.Password == login.Password);

                if (user == null)
                {
                    _logger.LogWarning($"Неудачная попытка входа: {login.Username}");
                    return Unauthorized("Неверное имя пользователя или пароль");
                }

                var token = _tokenService.GenerateToken(user);
                _logger.LogInformation($"Успешный вход пользователя: {user.Username}");

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при входе пользователя");
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

}
