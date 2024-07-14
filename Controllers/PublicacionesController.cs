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
    public class PublicacionesController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<PublicacionesController> _logger;

        public PublicacionesController(SocialPetsDB socialPetsDB, ILogger<PublicacionesController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }

        // Crear una nueva publicación
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Publicación creada exitosamente", typeof(Publicaciones))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la publicación inválidos")]
        public async Task<ActionResult<Publicaciones>> CreatePublicacion([FromBody] Publicaciones publicacion)
        {
            if (publicacion == null)
            {
                _logger.LogWarning("Se intentó crear una publicación nula");
                return BadRequest("Datos de la publicación inválidos");
            }

            _socialPetsDB.PublicacionDb.Add(publicacion);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPublicacion), new { id = publicacion.id_publicacion }, publicacion);
        }

        // Obtener una publicación por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Publicación encontrada exitosamente", typeof(Publicaciones))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Publicación no encontrada")]
        public async Task<ActionResult<Publicaciones>> GetPublicacion(int id)
        {
            var publicacion = await _socialPetsDB.PublicacionDb.FindAsync(id).ConfigureAwait(false);
            if (publicacion == null)
            {
                _logger.LogWarning("Publicación con id {PublicacionId} no encontrada", id);
                return NotFound("Publicación no encontrada");
            }

            return Ok(publicacion);
        }

        // Editar una publicación existente
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Publicación editada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Publicación no encontrada")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la publicación inválidos")]
        public async Task<IActionResult> EditPublicacion(int id, [FromBody] Publicaciones publicacion)
        {
            if (id != publicacion.id_publicacion)
            {
                _logger.LogWarning("El id de la publicación en la URL no coincide con el id de la publicación en el cuerpo de la solicitud");
                return BadRequest("Datos de la publicación inválidos");
            }

            var existingPublicacion = await _socialPetsDB.PublicacionDb.FindAsync(id).ConfigureAwait(false);
            if (existingPublicacion == null)
            {
                _logger.LogWarning("Publicación con id {PublicacionId} no encontrada", id);
                return NotFound("Publicación no encontrada");
            }

            existingPublicacion.titulo = publicacion.titulo;
            existingPublicacion.contenido = publicacion.contenido;
            existingPublicacion.latitud = publicacion.latitud;
            existingPublicacion.longitud = publicacion.longitud;
            existingPublicacion.comenzar_en = publicacion.comenzar_en;
            existingPublicacion.finalizado_en = publicacion.finalizado_en;
            existingPublicacion.tipo_receptor_id = publicacion.tipo_receptor_id;
            existingPublicacion.referencia_autor_id = publicacion.referencia_autor_id;
            existingPublicacion.referencia_receptor_id = publicacion.referencia_receptor_id;
            existingPublicacion.nivel_id_nivel = publicacion.nivel_id_nivel;
            existingPublicacion.tipo_publicacion_id = publicacion.tipo_publicacion_id;
            existingPublicacion.creado_en = publicacion.creado_en;
            existingPublicacion.imagen = publicacion.imagen;

            await _socialPetsDB.SaveChangesAsync();

            return Ok("Publicación editada exitosamente");
        }

        // Eliminar una publicación por ID
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Publicación eliminada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Publicación no encontrada")]
        public async Task<IActionResult> DeletePublicacion(int id)
        {
            var publicacion = await _socialPetsDB.PublicacionDb.FindAsync(id).ConfigureAwait(false);
            if (publicacion == null)
            {
                _logger.LogWarning("Publicación con id {PublicacionId} no encontrada", id);
                return NotFound("Publicación no encontrada");
            }

            _socialPetsDB.PublicacionDb.Remove(publicacion);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Publicación eliminada exitosamente");
        }

        // Obtener todas las publicaciones
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Publicaciones encontradas exitosamente", typeof(IEnumerable<Publicaciones>))]
        public async Task<ActionResult<IEnumerable<Publicaciones>>> GetAllPublicaciones()
        {
            var publicaciones = await _socialPetsDB.PublicacionDb.ToListAsync();

            return Ok(publicaciones);
        }
    }
}
