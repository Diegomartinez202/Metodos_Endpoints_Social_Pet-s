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
    public class CorazonController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<CorazonController> _logger;

        public CorazonController(SocialPetsDB socialPetsDB, ILogger<CorazonController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }

        // Crear un nuevo coraz�n
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Coraz�n creado exitosamente", typeof(Corazon))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos del coraz�n inv�lidos")]
        public async Task<ActionResult<Corazon>> CreateCorazon([FromBody] Corazon corazon)
        {
            if (corazon == null)
            {
                _logger.LogWarning("Se intent� crear un coraz�n nulo");
                return BadRequest("Datos del coraz�n inv�lidos");
            }

            corazon.creado_en = DateTime.UtcNow;
            _socialPetsDB.CorazonDb.Add(corazon);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCorazon), new { id = corazon.id_corazon }, corazon);
        }

        // Obtener un coraz�n por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Coraz�n encontrado exitosamente", typeof(Corazon))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Coraz�n no encontrado")]
        public async Task<ActionResult<Corazon>> GetCorazon(int id)
        {
            var corazon = await _socialPetsDB.CorazonDb.FindAsync(id).ConfigureAwait(false);
            if (corazon == null)
            {
                _logger.LogWarning("Coraz�n con id {CorazonId} no encontrado", id);
                return NotFound("Coraz�n no encontrado");
            }

            return Ok(corazon);
        }

        // Editar un coraz�n
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Coraz�n editado exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Coraz�n no encontrado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos del coraz�n inv�lidos")]
        public async Task<IActionResult> EditCorazon(int id, [FromBody] Corazon corazon)
        {
            if (id != corazon.id_corazon)
            {
                _logger.LogWarning("El id del coraz�n en la URL no coincide con el id del coraz�n en el cuerpo de la solicitud");
                return BadRequest("Datos del coraz�n inv�lidos");
            }

            var existingCorazon = await _socialPetsDB.CorazonDb.FindAsync(id).ConfigureAwait(false);
            if (existingCorazon == null)
            {
                _logger.LogWarning("Coraz�n con id {CorazonId} no encontrado", id);
                return NotFound("Coraz�n no encontrado");
            }

            existingCorazon.referencia_id = corazon.referencia_id;
            existingCorazon.tipo_id = corazon.tipo_id;
            existingCorazon.usuario_id_usuario = corazon.usuario_id_usuario;

            await _socialPetsDB.SaveChangesAsync();

            return Ok("Coraz�n editado exitosamente");
        }

        // Eliminar un coraz�n
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Coraz�n eliminado exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Coraz�n no encontrado")]
        public async Task<IActionResult> DeleteCorazon(int id)
        {
            var corazon = await _socialPetsDB.CorazonDb.FindAsync(id).ConfigureAwait(false);
            if (corazon == null)
            {
                _logger.LogWarning("Coraz�n con id {CorazonId} no encontrado", id);
                return NotFound("Coraz�n no encontrado");
            }

            _socialPetsDB.CorazonDb.Remove(corazon);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Coraz�n eliminado exitosamente");
        }
    }
}
