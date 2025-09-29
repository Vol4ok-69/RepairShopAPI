using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepairShopAPI.HelperControllerClasses;
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
        public async Task<IActionResult> GetAllUsers()
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                User user = await _db.Users.FindAsync(id);
                if (user == null)
                {
                    _logger.LogWarning($"Попытка получения несуществующего пользователя с id: {id}");
                    return NotFound("Не удалось найти пользователя с таким id");
                }
                _logger.LogInformation($"Запрошен пользователь {id}");
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении пользователя {id}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                User user = await _db.Users.FindAsync(id);
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

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, UserUpdateDto userDto)
        {
            try
            {
                User user = await _db.Users.FindAsync(id);
                if (user == null)
                {
                    _logger.LogWarning($"Попытка изменения несуществующего пользователя с id: {id}");
                    return NotFound("Не удалось найти пользователя с таким id");
                }
                if (await _db.Users.AnyAsync(u => u.Email == userDto.Email && u.Id != id))
                {
                    _logger.LogWarning($"Попытка использования занятого email: {userDto.Email} пользователем {id}");
                    return BadRequest("Email уже занят другим пользователем");
                }
                if (!string.IsNullOrEmpty(userDto.Phone) &&
                    await _db.Users.AnyAsync(u => u.Phone == userDto.Phone && u.Id != id))
                {
                    _logger.LogWarning($"Попытка использования занятого телефона: {userDto.Phone} пользователем {id}");
                    return BadRequest("Номер телефона уже занят другим пользователем");
                }
                if (!await _db.Roles.AnyAsync(r => r.Id == userDto.Roleid))
                {
                    _logger.LogWarning($"Попытка назначения несуществующей роли: {userDto.Roleid}");
                    return BadRequest("Указанная роль не существует");
                }

                if (!string.IsNullOrEmpty(userDto.Password))
                {
                    user.Passwordhash = Functions.GetHash(userDto.Password);
                }
                user.Email = userDto.Email;
                user.Roleid = userDto.Roleid;
                user.Phone = userDto.Phone;
                user.Firstname = userDto.Firstname;
                user.Lastname = userDto.Lastname;
                user.Patronymic = userDto.Patronymic;

                await _db.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при изменении пользователя {id}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchUser(Guid id, JsonPatchDocument<UserPatchDto> patchDoc)
        {
            try
            {
                if (patchDoc == null)
                {
                    _logger.LogWarning("Передан null patch документ");
                    return BadRequest("Patch документ не может быть null");
                }

                User user = await _db.Users.FindAsync(id);
                if (user == null)
                {
                    _logger.LogWarning($"Попытка изменения несуществующего пользователя с id: {id}");
                    return NotFound("Не удалось найти пользователя с таким id");
                }

                var userToPatch = new UserPatchDto
                {
                    Email = user.Email,
                    Phone = user.Phone,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    Patronymic = user.Patronymic,
                    Roleid = user.Roleid
                };

                patchDoc.ApplyTo(userToPatch, ModelState);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning($"Невалидный patch документ для пользователя {id}");
                    return BadRequest(ModelState);
                }

                if (string.IsNullOrEmpty(userToPatch.Firstname) ||
                    string.IsNullOrEmpty(userToPatch.Lastname) ||
                    string.IsNullOrEmpty(userToPatch.Patronymic))
                {
                    return BadRequest("Имя, фамилия и отчество обязательны для заполнения");
                }

                if (userToPatch.Email != user.Email &&
                    await _db.Users.AnyAsync(u => u.Email == userToPatch.Email && u.Id != id))
                {
                    _logger.LogWarning($"Попытка использования занятого email: {userToPatch.Email}");
                    return BadRequest("Email уже занят другим пользователем");
                }

                if (userToPatch.Phone != user.Phone &&
                    !string.IsNullOrEmpty(userToPatch.Phone) &&
                    await _db.Users.AnyAsync(u => u.Phone == userToPatch.Phone && u.Id != id))
                {
                    _logger.LogWarning($"Попытка использования занятого телефона: {userToPatch.Phone}");
                    return BadRequest("Номер телефона уже занят другим пользователем");
                }

                if (userToPatch.Roleid.HasValue && !await _db.Roles.AnyAsync(r => r.Id == userToPatch.Roleid.Value))
                {
                    _logger.LogWarning($"Попытка назначения несуществующей роли: {userToPatch.Roleid}");
                    return BadRequest("Указанная роль не существует");
                }


                user.Email = userToPatch.Email;
                user.Phone = userToPatch.Phone;
                user.Firstname = userToPatch.Firstname;
                user.Lastname = userToPatch.Lastname;
                user.Patronymic = userToPatch.Patronymic;

                if (userToPatch.Roleid.HasValue)
                {
                    user.Roleid = userToPatch.Roleid.Value;
                }

                if (!string.IsNullOrEmpty(userToPatch.Password))
                {
                    user.Passwordhash = Functions.GetHash(userToPatch.Password);
                }

                await _db.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при частичном обновлении пользователя {id}");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
    }
}