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
    public class ComentarioController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<ComentarioController> _logger;

        public ComentarioController(SocialPetsDB socialPetsDB, ILogger<ComentarioController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }

        // Obtener un comentario por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Comentario encontrado exitosamente", typeof(Comentario))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Comentario no encontrado")]
        public async Task<ActionResult<Comentario>> Get(int id)
        {
            var comentario = await _socialPetsDB.ComentarioDb.FindAsync(id).ConfigureAwait(false);

            if (comentario == null)
            {
                _logger.LogWarning("Comentario con id {ComentarioId} no encontrado", id);
                return NotFound("Comentario no encontrado");
            }

            return Ok(comentario);
        }

        // Crear un nuevo comentario
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Comentario creado exitosamente", typeof(Comentario))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos del comentario inválidos")]
        public async Task<ActionResult<Comentario>> CreateComentario([FromBody] Comentario comentario)
        {
            if (comentario == null)
            {
                _logger.LogWarning("Se intentó crear un comentario nulo");
                return BadRequest("Datos del comentario inválidos");
            }

            _socialPetsDB.ComentarioDb.Add(comentario);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = comentario.id_comentario }, comentario);
        }

        // Editar un comentario
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Comentario editado exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Comentario no encontrado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos del comentario inválidos")]
        public async Task<IActionResult> EditComentario(int id, [FromBody] Comentario comentario)
        {
            if (id != comentario.id_comentario)
            {
                _logger.LogWarning("El id del comentario en la URL no coincide con el id del comentario en el cuerpo de la solicitud");
                return BadRequest("Datos del comentario inválidos");
            }

            var existingComentario = await _socialPetsDB.ComentarioDb.FindAsync(id).ConfigureAwait(false);
            if (existingComentario == null)
            {
                _logger.LogWarning("Comentario con id {ComentarioId} no encontrado", id);
                return NotFound("Comentario no encontrado");
            }

            existingComentario.contenido = comentario.contenido;
            existingComentario.usuario_id = comentario.usuario_id;
            existingComentario.referencia_id = comentario.referencia_id;
            existingComentario.creado_en = comentario.creado_en;

            await _socialPetsDB.SaveChangesAsync();

            return Ok("Comentario editado exitosamente");
        }

        // Eliminar un comentario
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Comentario eliminado exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Comentario no encontrado")]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            var comentario = await _socialPetsDB.ComentarioDb.FindAsync(id).ConfigureAwait(false);
            if (comentario == null)
            {
                _logger.LogWarning("Comentario con id {ComentarioId} no encontrado", id);
                return NotFound("Comentario no encontrado");
            }

            _socialPetsDB.ComentarioDb.Remove(comentario);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Comentario eliminado exitosamente");
        }
    }
}
