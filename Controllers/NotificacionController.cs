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

        // Crear una nueva notificaci�n
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Notificaci�n creada exitosamente", typeof(Notificacion))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la notificaci�n inv�lidos")]
        public async Task<ActionResult<Notificacion>> CreateNotificacion([FromBody] Notificacion notificacion)
        {
            if (notificacion == null)
            {
                _logger.LogWarning("Se intent� crear una notificaci�n nula");
                return BadRequest("Datos de la notificaci�n inv�lidos");
            }

            notificacion.creado_en = DateTime.UtcNow;
            _socialPetsDB.NotificacionDb.Add(notificacion);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNotificacion), new { id = notificacion.id_notificacion }, notificacion);
        }

        // Obtener una notificaci�n por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Notificaci�n encontrada exitosamente", typeof(Notificacion))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Notificaci�n no encontrada")]
        public async Task<ActionResult<Notificacion>> GetNotificacion(int id)
        {
            var notificacion = await _socialPetsDB.NotificacionDb.FindAsync(id).ConfigureAwait(false);
            if (notificacion == null)
            {
                _logger.LogWarning("Notificaci�n con id {NotificacionId} no encontrada", id);
                return NotFound("Notificaci�n no encontrada");
            }

            return Ok(notificacion);
        }

        // Editar una notificaci�n
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Notificaci�n editada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Notificaci�n no encontrada")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la notificaci�n inv�lidos")]
        public async Task<IActionResult> EditNotificacion(int id, [FromBody] Notificacion notificacion)
        {
            if (id != notificacion.id_notificacion)
            {
                _logger.LogWarning("El id de la notificaci�n en la URL no coincide con el id de la notificaci�n en el cuerpo de la solicitud");
                return BadRequest("Datos de la notificaci�n inv�lidos");
            }

            var existingNotificacion = await _socialPetsDB.NotificacionDb.FindAsync(id).ConfigureAwait(false);
            if (existingNotificacion == null)
            {
                _logger.LogWarning("Notificaci�n con id {NotificacionId} no encontrada", id);
                return NotFound("Notificaci�n no encontrada");
            }

            existingNotificacion.leido = notificacion.leido;
            existingNotificacion.referencia_id = notificacion.referencia_id;
            existingNotificacion.tipo_id = notificacion.tipo_id;
            existingNotificacion.usuario_id_usuario = notificacion.usuario_id_usuario;

            await _socialPetsDB.SaveChangesAsync();

            return Ok("Notificaci�n editada exitosamente");
        }

        // Eliminar una notificaci�n
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Notificaci�n eliminada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Notificaci�n no encontrada")]
        public async Task<IActionResult> DeleteNotificacion(int id)
        {
            var notificacion = await _socialPetsDB.NotificacionDb.FindAsync(id).ConfigureAwait(false);
            if (notificacion == null)
            {
                _logger.LogWarning("Notificaci�n con id {NotificacionId} no encontrada", id);
                return NotFound("Notificaci�n no encontrada");
            }

            _socialPetsDB.NotificacionDb.Remove(notificacion);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Notificaci�n eliminada exitosamente");
        }
    }
}
