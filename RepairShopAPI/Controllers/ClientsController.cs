using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RepairShopAPI.Models;

namespace RepairShopAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly RepairShopContext _db;

        public ClientsController(RepairShopContext db)
        {
            _db = db;   
        }

        // получить всех клиентов
        [HttpGet]
        public IActionResult GetAll()
        {
            var clients = _db.Clients.ToList();
            return Ok(clients);
        }

        // добавить клиента
        [HttpPost]
        public async Task<IActionResult> Add(Client client)
        {
            await _db.Clients.AddAsync(client);
            await _db.SaveChangesAsync();        
            return Ok(client);
        }

        // удалить клиента
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _db.Clients.FindAsync(id);
            if (client == null) return NotFound();

            _db.Clients.Remove(client);
            await _db.SaveChangesAsync();
            return Ok();
        }

        // пропатчить клиента, в json то,что патчим
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchClient(int id, JsonPatchDocument<Client> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var client = await _db.Clients.FindAsync(id);
            if (client == null) return NotFound();

            patchDoc.ApplyTo(client);
            await _db.SaveChangesAsync();

            return Ok(client);
        }
    }
}