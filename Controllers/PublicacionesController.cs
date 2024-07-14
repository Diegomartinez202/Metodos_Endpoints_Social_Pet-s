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

        // Crear una nueva publicaci�n
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Publicaci�n creada exitosamente", typeof(Publicaciones))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la publicaci�n inv�lidos")]
        public async Task<ActionResult<Publicaciones>> CreatePublicacion([FromBody] Publicaciones publicacion)
        {
            if (publicacion == null)
            {
                _logger.LogWarning("Se intent� crear una publicaci�n nula");
                return BadRequest("Datos de la publicaci�n inv�lidos");
            }

            _socialPetsDB.PublicacionDb.Add(publicacion);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPublicacion), new { id = publicacion.id_publicacion }, publicacion);
        }

        // Obtener una publicaci�n por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Publicaci�n encontrada exitosamente", typeof(Publicaciones))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Publicaci�n no encontrada")]
        public async Task<ActionResult<Publicaciones>> GetPublicacion(int id)
        {
            var publicacion = await _socialPetsDB.PublicacionDb.FindAsync(id).ConfigureAwait(false);
            if (publicacion == null)
            {
                _logger.LogWarning("Publicaci�n con id {PublicacionId} no encontrada", id);
                return NotFound("Publicaci�n no encontrada");
            }

            return Ok(publicacion);
        }

        // Editar una publicaci�n existente
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Publicaci�n editada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Publicaci�n no encontrada")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la publicaci�n inv�lidos")]
        public async Task<IActionResult> EditPublicacion(int id, [FromBody] Publicaciones publicacion)
        {
            if (id != publicacion.id_publicacion)
            {
                _logger.LogWarning("El id de la publicaci�n en la URL no coincide con el id de la publicaci�n en el cuerpo de la solicitud");
                return BadRequest("Datos de la publicaci�n inv�lidos");
            }

            var existingPublicacion = await _socialPetsDB.PublicacionDb.FindAsync(id).ConfigureAwait(false);
            if (existingPublicacion == null)
            {
                _logger.LogWarning("Publicaci�n con id {PublicacionId} no encontrada", id);
                return NotFound("Publicaci�n no encontrada");
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

            return Ok("Publicaci�n editada exitosamente");
        }

        // Eliminar una publicaci�n por ID
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Publicaci�n eliminada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Publicaci�n no encontrada")]
        public async Task<IActionResult> DeletePublicacion(int id)
        {
            var publicacion = await _socialPetsDB.PublicacionDb.FindAsync(id).ConfigureAwait(false);
            if (publicacion == null)
            {
                _logger.LogWarning("Publicaci�n con id {PublicacionId} no encontrada", id);
                return NotFound("Publicaci�n no encontrada");
            }

            _socialPetsDB.PublicacionDb.Remove(publicacion);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Publicaci�n eliminada exitosamente");
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
