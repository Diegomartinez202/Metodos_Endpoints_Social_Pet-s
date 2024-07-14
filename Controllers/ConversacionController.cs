using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class ConversacionController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<ConversacionController> _logger;

        public ConversacionController(SocialPetsDB socialPetsDB, ILogger<ConversacionController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }

        // Obtener una conversación por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Conversación encontrada exitosamente", typeof(Conversacion))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Conversación no encontrada")]
        public async Task<ActionResult<Conversacion>> Get(int id)
        {
            var conversacion = await _socialPetsDB.ConversacionDb.FindAsync(id).ConfigureAwait(false);

            if (conversacion == null)
            {
                _logger.LogWarning("Conversación con id {ConversacionId} no encontrada", id);
                return NotFound("Conversación no encontrada");
            }

            return Ok(conversacion);
        }

        // Crear una nueva conversación
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Conversación creada exitosamente", typeof(Conversacion))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la conversación inválidos")]
        public async Task<ActionResult<Conversacion>> Create([FromBody] Conversacion conversacion)
        {
            if (conversacion == null)
            {
                _logger.LogWarning("Se intentó crear una conversación nula");
                return BadRequest("Datos de la conversación inválidos");
            }

            _socialPetsDB.ConversacionDb.Add(conversacion);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = conversacion.id_conversacion }, conversacion);
        }

        // Editar una conversación existente
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Conversación editada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Conversación no encontrada")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la conversación inválidos")]
        public async Task<IActionResult> Update(int id, [FromBody] Conversacion conversacion)
        {
            if (id != conversacion.id_conversacion)
            {
                _logger.LogWarning("El id de la conversación en la URL no coincide con el id de la conversación en el cuerpo de la solicitud");
                return BadRequest("Datos de la conversación inválidos");
            }

            var existingConversacion = await _socialPetsDB.ConversacionDb.FindAsync(id).ConfigureAwait(false);
            if (existingConversacion == null)
            {
                _logger.LogWarning("Conversación con id {ConversacionId} no encontrada", id);
                return NotFound("Conversación no encontrada");
            }

            existingConversacion.id_conversacion  = conversacion.id_conversacion;
            existingConversacion.usuario_id_usuario = conversacion.usuario_id_usuario;
            existingConversacion.creado_en = conversacion.creado_en;
                        

            await _socialPetsDB.SaveChangesAsync();

            return Ok("Conversación editada exitosamente");
        }

        // Eliminar una conversación
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Conversación eliminada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Conversación no encontrada")]
        public async Task<IActionResult> Delete(int id)
        {
            var conversacion = await _socialPetsDB.ConversacionDb.FindAsync(id).ConfigureAwait(false);
            if (conversacion == null)
            {
                _logger.LogWarning("Conversación con id {ConversacionId} no encontrada", id);
                return NotFound("Conversación no encontrada");
            }

            _socialPetsDB.ConversacionDb.Remove(conversacion);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Conversación eliminada exitosamente");
        }
    }
}
