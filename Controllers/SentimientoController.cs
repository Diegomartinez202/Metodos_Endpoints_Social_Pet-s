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
    public class SentimientoController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<SentimientoController> _logger;

        public SentimientoController(SocialPetsDB socialPetsDB, ILogger<SentimientoController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }

        // Crear un nuevo sentimiento
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Sentimiento creado exitosamente", typeof(Sentimiento))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos del sentimiento inválidos")]
        public async Task<ActionResult<Sentimiento>> CreateSentimiento([FromBody] Sentimiento sentimiento)
        {
            if (sentimiento == null)
            {
                _logger.LogWarning("Se intentó crear un sentimiento nulo");
                return BadRequest("Datos del sentimiento inválidos");
            }

            _socialPetsDB.SentimientoDb.Add(sentimiento);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSentimiento), new { id = sentimiento.id_sentimiento }, sentimiento);
        }

        // Obtener un sentimiento por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Sentimiento encontrado exitosamente", typeof(Sentimiento))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Sentimiento no encontrado")]
        public async Task<ActionResult<Sentimiento>> GetSentimiento(int id)
        {
            var sentimiento = await _socialPetsDB.SentimientoDb.FindAsync(id).ConfigureAwait(false);
            if (sentimiento == null)
            {
                _logger.LogWarning("Sentimiento con id {Id} no encontrado", id);
                return NotFound("Sentimiento no encontrado");
            }

            return Ok(sentimiento);
        }

        // Editar un sentimiento existente
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Sentimiento editado exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Sentimiento no encontrado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos del sentimiento inválidos")]
        public async Task<IActionResult> EditSentimiento(int id, [FromBody] Sentimiento sentimiento)
        {
            if (id != sentimiento.id_sentimiento)
            {
                _logger.LogWarning("El id del sentimiento en la URL no coincide con el id del sentimiento en el cuerpo de la solicitud");
                return BadRequest("Datos del sentimiento inválidos");
            }

            var existingSentimiento = await _socialPetsDB.SentimientoDb.FindAsync(id).ConfigureAwait(false);
            if (existingSentimiento == null)
            {
                _logger.LogWarning("Sentimiento con id {Id} no encontrado", id);
                return NotFound("Sentimiento no encontrado");
            }

            existingSentimiento.name = sentimiento.name;

            await _socialPetsDB.SaveChangesAsync();

            return Ok("Sentimiento editado exitosamente");
        }

        // Eliminar un sentimiento por ID
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Sentimiento eliminado exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Sentimiento no encontrado")]
        public async Task<IActionResult> DeleteSentimiento(int id)
        {
            var sentimiento = await _socialPetsDB.SentimientoDb.FindAsync(id).ConfigureAwait(false);
            if (sentimiento == null)
            {
                _logger.LogWarning("Sentimiento con id {Id} no encontrado", id);
                return NotFound("Sentimiento no encontrado");
            }

            _socialPetsDB.SentimientoDb.Remove(sentimiento);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Sentimiento eliminado exitosamente");
        }

        // Obtener todos los sentimientos
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Sentimientos encontrados exitosamente", typeof(IEnumerable<Sentimiento>))]
        public async Task<ActionResult<IEnumerable<Sentimiento>>> GetAllSentimientos()
        {
            var sentimientos = await _socialPetsDB.SentimientoDb.ToListAsync();

            return Ok(sentimientos);
        }
    }
}
