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
    public class ImagenController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<ImagenController> _logger;

        public ImagenController(SocialPetsDB socialPetsDB, ILogger<ImagenController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }

        // Crear una nueva imagen
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Imagen creada exitosamente", typeof(Imagen))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la imagen inválidos")]
        public async Task<ActionResult<Imagen>> CreateImagen([FromBody] Imagen imagen)
        {
            if (imagen == null)
            {
                _logger.LogWarning("Se intentó crear una imagen nula");
                return BadRequest("Datos de la imagen inválidos");
            }

            imagen.creado_en = DateTime.UtcNow;
            _socialPetsDB.ImagenesDb.Add(imagen);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(GetImagen), new { id = imagen.id_imagen }, imagen);
        }

        // Obtener una imagen por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Imagen encontrada exitosamente", typeof(Imagen))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Imagen no encontrada")]
        public async Task<ActionResult<Imagen>> GetImagen(int id)
        {
            var imagen = await _socialPetsDB.ImagenesDb.FindAsync(id).ConfigureAwait(false);
            if (imagen == null)
            {
                _logger.LogWarning("Imagen con id {ImagenId} no encontrada", id);
                return NotFound("Imagen no encontrada");
            }

            return Ok(imagen);
        }

        // Editar una imagen
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Imagen editada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Imagen no encontrada")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la imagen inválidos")]
        public async Task<IActionResult> EditImagen(int id, [FromBody] Imagen imagen)
        {
            if (id != imagen.id_imagen)
            {
                _logger.LogWarning("El id de la imagen en la URL no coincide con el id de la imagen en el cuerpo de la solicitud");
                return BadRequest("Datos de la imagen inválidos");
            }
            var existingImagen = await _socialPetsDB.ImagenesDb.FindAsync(id).ConfigureAwait(false);
            if (existingImagen == null)
            {
                _logger.LogWarning("Imagen con id {ImagenId} no encontrada", id);
                return NotFound("Imagen no encontrada");
            }
            existingImagen.album_id_album = imagen.album_id_album;
            existingImagen.contenido = imagen.contenido;
            existingImagen.fuente = imagen.fuente;
            existingImagen.nivel_id_nivel = imagen.nivel_id_nivel;
            existingImagen.titulo = imagen.titulo;
            existingImagen.usuario_id_usuario = imagen.usuario_id_usuario;
            await _socialPetsDB.SaveChangesAsync();
            return Ok("Imagen editada exitosamente");
        }

        // Eliminar una imagen
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Imagen eliminada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Imagen no encontrada")]
        public async Task<IActionResult> DeleteImagen(int id)
        {
            var imagen = await _socialPetsDB.ImagenesDb.FindAsync(id).ConfigureAwait(false);
            if (imagen == null)
            {
                _logger.LogWarning("Imagen con id {ImagenId} no encontrada", id);
                return NotFound("Imagen no encontrada");
            }

            _socialPetsDB.ImagenesDb.Remove(imagen);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Imagen eliminada exitosamente");
        }
    }
}
