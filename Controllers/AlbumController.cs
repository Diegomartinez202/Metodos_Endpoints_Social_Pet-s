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

        // Crear un nuevo �lbum
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "�lbum creado exitosamente", typeof(Album))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos del �lbum inv�lidos")]
        public async Task<ActionResult<Album>> CreateAlbum([FromBody] Album album)
        {
            if (album == null)
            {
                _logger.LogWarning("Se intent� crear un �lbum nulo");
                return BadRequest("Datos del �lbum inv�lidos");
            }

            _socialPetsDB.AlbumDb.Add(album);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlbum), new { id = album.id_album }, album);
        }

        // Obtener un �lbum por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "�lbum encontrado exitosamente", typeof(Album))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "�lbum no encontrado")]
        public async Task<ActionResult<Album>> GetAlbum(int id)
        {
            var album = await _socialPetsDB.AlbumDb.FindAsync(id).ConfigureAwait(false);
            if (album == null)
            {
                _logger.LogWarning("�lbum con id {AlbumId} no encontrado", id);
                return NotFound("�lbum no encontrado");
            }

            return Ok(album);
        }

        // Editar un �lbum
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "�lbum editado exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "�lbum no encontrado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos del �lbum inv�lidos")]
        public async Task<IActionResult> EditAlbum(int id, [FromBody] Album album)
        {
            if (id != album.id_album)
            {
                _logger.LogWarning("El id del �lbum en la URL no coincide con el id del �lbum en el cuerpo de la solicitud");
                return BadRequest("Datos del �lbum inv�lidos");
            }

            var existingAlbum = await _socialPetsDB.AlbumDb.FindAsync(id).ConfigureAwait(false);
            if (existingAlbum == null)
            {
                _logger.LogWarning("�lbum con id {AlbumId} no encontrado", id);
                return NotFound("�lbum no encontrado");
            }

            existingAlbum.titulo = album.titulo;
            existingAlbum.creado_en = album.creado_en;
            existingAlbum.usuario_id_usuario = album.usuario_id_usuario;

            await _socialPetsDB.SaveChangesAsync();

            return Ok("�lbum editado exitosamente");
        }

        // Eliminar un �lbum
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "�lbum eliminado exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "�lbum no encontrado")]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            var album = await _socialPetsDB.AlbumDb.FindAsync(id).ConfigureAwait(false);
            if (album == null)
            {
                _logger.LogWarning("�lbum con id {AlbumId} no encontrado", id);
                return NotFound("�lbum no encontrado");
            }

            _socialPetsDB.AlbumDb.Remove(album);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("�lbum eliminado exitosamente");
        }
    }
}
