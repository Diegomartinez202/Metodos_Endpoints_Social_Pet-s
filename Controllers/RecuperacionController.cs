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
    public class RecuperacionController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<RecuperacionController> _logger;

        public RecuperacionController(SocialPetsDB socialPetsDB, ILogger<RecuperacionController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }

        // Crear una nueva solicitud de recuperación
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Solicitud de recuperación creada exitosamente", typeof(Recuperacion))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la solicitud inválidos")]
        public async Task<ActionResult<Recuperacion>> CreateRecuperacion([FromBody] Recuperacion recuperacion)
        {
            if (recuperacion == null)
            {
                _logger.LogWarning("Se intentó crear una solicitud de recuperación nula");
                return BadRequest("Datos de la solicitud inválidos");
            }

            _socialPetsDB.RecuperacionDb.Add(recuperacion);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecuperacion), new { id = recuperacion.id_recuperacion }, recuperacion);
        }

        // Obtener una solicitud de recuperación por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Solicitud de recuperación encontrada exitosamente", typeof(Recuperacion))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Solicitud de recuperación no encontrada")]
        public async Task<ActionResult<Recuperacion>> GetRecuperacion(int id)
        {
            var recuperacion = await _socialPetsDB.RecuperacionDb.FindAsync(id).ConfigureAwait(false);
            if (recuperacion == null)
            {
                _logger.LogWarning("Solicitud de recuperación con id {Id} no encontrada", id);
                return NotFound("Solicitud no encontrada");
            }

            return Ok(recuperacion);
        }

        // Editar una solicitud de recuperación existente
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Solicitud de recuperación editada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Solicitud de recuperación no encontrada")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la solicitud inválidos")]
        public async Task<IActionResult> EditRecuperacion(int id, [FromBody] Recuperacion recuperacion)
        {
            if (id != recuperacion.id_recuperacion)
            {
                _logger.LogWarning("El id de la solicitud en la URL no coincide con el id de la solicitud en el cuerpo de la solicitud");
                return BadRequest("Datos de la solicitud inválidos");
            }

            var existingRecuperacion = await _socialPetsDB.RecuperacionDb.FindAsync(id).ConfigureAwait(false);
            if (existingRecuperacion == null)
            {
                _logger.LogWarning("Solicitud de recuperación con id {Id} no encontrada", id);
                return NotFound("Solicitud no encontrada");
            }

            existingRecuperacion.codigo = recuperacion.codigo;
            existingRecuperacion.creado_en = recuperacion.creado_en;
            existingRecuperacion.en_uso = recuperacion.en_uso;
            existingRecuperacion.usuario_id_usuario = recuperacion.usuario_id_usuario;

            await _socialPetsDB.SaveChangesAsync();

            return Ok("Solicitud de recuperación editada exitosamente");
        }

        // Eliminar una solicitud de recuperación por ID
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Solicitud de recuperación eliminada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Solicitud de recuperación no encontrada")]
        public async Task<IActionResult> DeleteRecuperacion(int id)
        {
            var recuperacion = await _socialPetsDB.RecuperacionDb.FindAsync(id).ConfigureAwait(false);
            if (recuperacion == null)
            {
                _logger.LogWarning("Solicitud de recuperación con id {Id} no encontrada", id);
                return NotFound("Solicitud no encontrada");
            }

            _socialPetsDB.RecuperacionDb.Remove(recuperacion);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Solicitud de recuperación eliminada exitosamente");
        }

        // Obtener todas las solicitudes de recuperación
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Solicitudes de recuperación encontradas exitosamente", typeof(IEnumerable<Recuperacion>))]
        public async Task<ActionResult<IEnumerable<Recuperacion>>> GetAllRecuperaciones()
        {
            var recuperaciones = await _socialPetsDB.RecuperacionDb.ToListAsync();

            return Ok(recuperaciones);
        }
    }
}
