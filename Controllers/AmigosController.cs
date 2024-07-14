using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySqlX.XDevAPI.Common;
using SocialPets.DBConnection;
using SocialPets.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SocialPets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AmigoController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<AmigoController> _logger;

        public AmigoController(SocialPetsDB socialPetsDB, ILogger<AmigoController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }

        [HttpGet("{usuarioId}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Amigos encontrados exitosamente", typeof(IEnumerable<Amigos>))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "No se encontraron amigos para el usuario especificado")]
        public async Task<ActionResult<IEnumerable<Amigos>>> GetAmigos(int usuarioId)
        {
            var amigos = await _socialPetsDB.AmigoDb.Where(a => a.id_amigos == usuarioId).ToListAsync();
            if (amigos == null || amigos.Count == 0)
            {
                _logger.LogWarning("No se encontraron amigos para el usuario con id {UsuarioId}", usuarioId);
                return NotFound("No se encontraron amigos para el usuario especificado");
            }

            return Ok(amigos);
        }

        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Amigo agregado exitosamente", typeof(Amigos))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de amigo inválidos")]
        public async Task<ActionResult<Amigos>> AddAmigo([FromBody] Amigos amigo)
        {
            if (amigo == null)
            {
                _logger.LogWarning("Se intentó agregar un amigo nulo");
                return BadRequest("Datos de amigo inválidos");
            }

            _socialPetsDB.AmigoDb.Add(amigo);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAmigos), new { usuarioId = amigo.usuario_id_usuario }, amigo);
        }

        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Amigo eliminado exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Amigo no encontrado")]
        public async Task<IActionResult> DeleteAmigo(int id)
        {
            var amigo = await _socialPetsDB.AmigoDb.FindAsync(id).ConfigureAwait(false);
            if (amigo == null)
            {
                _logger.LogWarning("Amigo con id {AmigoId} no encontrado", id);
                return NotFound("Amigo no encontrado");
            }

            _socialPetsDB.AmigoDb.Remove(amigo);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Amigo eliminado exitosamente");
        }
    }
}
