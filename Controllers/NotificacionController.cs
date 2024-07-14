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
    public class NotificacionController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<NotificacionController> _logger;

        public NotificacionController(SocialPetsDB socialPetsDB, ILogger<NotificacionController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }

        // Crear una nueva notificación
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Notificación creada exitosamente", typeof(Notificacion))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la notificación inválidos")]
        public async Task<ActionResult<Notificacion>> CreateNotificacion([FromBody] Notificacion notificacion)
        {
            if (notificacion == null)
            {
                _logger.LogWarning("Se intentó crear una notificación nula");
                return BadRequest("Datos de la notificación inválidos");
            }

            notificacion.creado_en = DateTime.UtcNow;
            _socialPetsDB.NotificacionDb.Add(notificacion);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNotificacion), new { id = notificacion.id_notificacion }, notificacion);
        }

        // Obtener una notificación por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Notificación encontrada exitosamente", typeof(Notificacion))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Notificación no encontrada")]
        public async Task<ActionResult<Notificacion>> GetNotificacion(int id)
        {
            var notificacion = await _socialPetsDB.NotificacionDb.FindAsync(id).ConfigureAwait(false);
            if (notificacion == null)
            {
                _logger.LogWarning("Notificación con id {NotificacionId} no encontrada", id);
                return NotFound("Notificación no encontrada");
            }

            return Ok(notificacion);
        }

        // Editar una notificación
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Notificación editada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Notificación no encontrada")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la notificación inválidos")]
        public async Task<IActionResult> EditNotificacion(int id, [FromBody] Notificacion notificacion)
        {
            if (id != notificacion.id_notificacion)
            {
                _logger.LogWarning("El id de la notificación en la URL no coincide con el id de la notificación en el cuerpo de la solicitud");
                return BadRequest("Datos de la notificación inválidos");
            }

            var existingNotificacion = await _socialPetsDB.NotificacionDb.FindAsync(id).ConfigureAwait(false);
            if (existingNotificacion == null)
            {
                _logger.LogWarning("Notificación con id {NotificacionId} no encontrada", id);
                return NotFound("Notificación no encontrada");
            }

            existingNotificacion.leido = notificacion.leido;
            existingNotificacion.referencia_id = notificacion.referencia_id;
            existingNotificacion.tipo_id = notificacion.tipo_id;
            existingNotificacion.usuario_id_usuario = notificacion.usuario_id_usuario;

            await _socialPetsDB.SaveChangesAsync();

            return Ok("Notificación editada exitosamente");
        }

        // Eliminar una notificación
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Notificación eliminada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Notificación no encontrada")]
        public async Task<IActionResult> DeleteNotificacion(int id)
        {
            var notificacion = await _socialPetsDB.NotificacionDb.FindAsync(id).ConfigureAwait(false);
            if (notificacion == null)
            {
                _logger.LogWarning("Notificación con id {NotificacionId} no encontrada", id);
                return NotFound("Notificación no encontrada");
            }

            _socialPetsDB.NotificacionDb.Remove(notificacion);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Notificación eliminada exitosamente");
        }
    }
}
