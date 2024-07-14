using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SocialPets.DBConnection;
using SocialPets.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace SocialPets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<AlbumController> _logger;

        public AlbumController(SocialPetsDB socialPetsDB, ILogger<AlbumController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }

        // Crear un nuevo álbum
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Álbum creado exitosamente", typeof(Album))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos del álbum inválidos")]
        public async Task<ActionResult<Album>> CreateAlbum([FromBody] Album album)
        {
            if (album == null)
            {
                _logger.LogWarning("Se intentó crear un álbum nulo");
                return BadRequest("Datos del álbum inválidos");
            }

            _socialPetsDB.AlbumDb.Add(album);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlbum), new { id = album.id_album }, album);
        }

        // Obtener un álbum por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Álbum encontrado exitosamente", typeof(Album))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Álbum no encontrado")]
        public async Task<ActionResult<Album>> GetAlbum(int id)
        {
            var album = await _socialPetsDB.AlbumDb.FindAsync(id).ConfigureAwait(false);
            if (album == null)
            {
                _logger.LogWarning("Álbum con id {AlbumId} no encontrado", id);
                return NotFound("Álbum no encontrado");
            }

            return Ok(album);
        }

        // Editar un álbum
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Álbum editado exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Álbum no encontrado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos del álbum inválidos")]
        public async Task<IActionResult> EditAlbum(int id, [FromBody] Album album)
        {
            if (id != album.id_album)
            {
                _logger.LogWarning("El id del álbum en la URL no coincide con el id del álbum en el cuerpo de la solicitud");
                return BadRequest("Datos del álbum inválidos");
            }

            var existingAlbum = await _socialPetsDB.AlbumDb.FindAsync(id).ConfigureAwait(false);
            if (existingAlbum == null)
            {
                _logger.LogWarning("Álbum con id {AlbumId} no encontrado", id);
                return NotFound("Álbum no encontrado");
            }

            existingAlbum.titulo = album.titulo;
            existingAlbum.creado_en = album.creado_en;
            existingAlbum.usuario_id_usuario = album.usuario_id_usuario;

            await _socialPetsDB.SaveChangesAsync();

            return Ok("Álbum editado exitosamente");
        }

        // Eliminar un álbum
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Álbum eliminado exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Álbum no encontrado")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var album = await _socialPetsDB.AlbumDb.FindAsync(id).ConfigureAwait(false);
            if (album == null)
            {
                _logger.LogWarning("Álbum con id {AlbumId} no encontrado", id);
                return NotFound("Álbum no encontrado");
            }

            _socialPetsDB.AlbumDb.Remove(album);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Álbum eliminado exitosamente");
        }
    }
}
