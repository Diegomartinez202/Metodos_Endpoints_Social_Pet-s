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
    public class PublicacionImagenController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<PublicacionImagenController> _logger;

        public PublicacionImagenController(SocialPetsDB socialPetsDB, ILogger<PublicacionImagenController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }

        // Crear una nueva relación de publicación e imagen
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Relación de publicación e imagen creada exitosamente", typeof(PublicacionImagen))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la relación inválidos")]
        public async Task<ActionResult<PublicacionImagen>> CreatePublicacionImagen([FromBody] PublicacionImagen publicacionImagen)
        {
            if (publicacionImagen == null)
            {
                _logger.LogWarning("Se intentó crear una relación de publicación e imagen nula");
                return BadRequest("Datos de la relación inválidos");
            }

            _socialPetsDB.PublicacionImagenDb.Add(publicacionImagen);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPublicacionImagen), new { id = publicacionImagen.id_publicacion_imagen }, publicacionImagen);
        }

        // Obtener una relación de publicación e imagen por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Relación de publicación e imagen encontrada exitosamente", typeof(PublicacionImagen))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Relación de publicación e imagen no encontrada")]
        public async Task<ActionResult<PublicacionImagen>> GetPublicacionImagen(int id)
        {
            var publicacionImagen = await _socialPetsDB.PublicacionImagenDb.FindAsync(id).ConfigureAwait(false);
            if (publicacionImagen == null)
            {
                _logger.LogWarning("Relación de publicación e imagen con id {Id} no encontrada", id);
                return NotFound("Relación no encontrada");
            }

            return Ok(publicacionImagen);
        }

        // Editar una relación de publicación e imagen existente
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Relación de publicación e imagen editada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Relación de publicación e imagen no encontrada")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la relación inválidos")]
        public async Task<IActionResult> EditPublicacionImagen(int id, [FromBody] PublicacionImagen publicacionImagen)
        {
            if (id != publicacionImagen.id_publicacion_imagen)
            {
                _logger.LogWarning("El id de la relación en la URL no coincide con el id de la relación en el cuerpo de la solicitud");
                return BadRequest("Datos de la relación inválidos");
            }

            var existingPublicacionImagen = await _socialPetsDB.PublicacionImagenDb.FindAsync(id).ConfigureAwait(false);
            if (existingPublicacionImagen == null)
            {
                _logger.LogWarning("Relación de publicación e imagen con id {Id} no encontrada", id);
                return NotFound("Relación no encontrada");
            }

            existingPublicacionImagen.imagen_id_imagen = publicacionImagen.imagen_id_imagen;
            existingPublicacionImagen.publicacion_id_publicacion = publicacionImagen.publicacion_id_publicacion;

            await _socialPetsDB.SaveChangesAsync();

            return Ok("Relación de publicación e imagen editada exitosamente");
        }

        // Eliminar una relación de publicación e imagen por ID
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Relación de publicación e imagen eliminada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Relación de publicación e imagen no encontrada")]
        public async Task<IActionResult> DeletePublicacionImagen(int id)
        {
            var publicacionImagen = await _socialPetsDB.PublicacionImagenDb.FindAsync(id).ConfigureAwait(false);
            if (publicacionImagen == null)
            {
                _logger.LogWarning("Relación de publicación e imagen con id {Id} no encontrada", id);
                return NotFound("Relación no encontrada");
            }

            _socialPetsDB.PublicacionImagenDb.Remove(publicacionImagen);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Relación de publicación e imagen eliminada exitosamente");
        }

        // Obtener todas las relaciones de publicación e imagen
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Relaciones de publicación e imagen encontradas exitosamente", typeof(IEnumerable<PublicacionImagen>))]
        public async Task<ActionResult<IEnumerable<PublicacionImagen>>> GetAllPublicacionImagenes()
        {
            var publicacionImagenes = await _socialPetsDB.PublicacionImagenDb.ToListAsync();

            return Ok(publicacionImagenes);
        }
    }
}
