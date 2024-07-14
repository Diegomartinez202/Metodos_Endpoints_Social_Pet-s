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
    public class PerfilController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<PerfilController> _logger;

        public PerfilController(SocialPetsDB socialPetsDB, ILogger<PerfilController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }

        // Crear un nuevo perfil
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Perfil creado exitosamente", typeof(Perfil))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos del perfil inválidos")]
        public async Task<ActionResult<Perfil>> CreatePerfil([FromBody] Perfil perfil)
        {
            if (perfil == null)
            {
                _logger.LogWarning("Se intentó crear un perfil nulo");
                return BadRequest("Datos del perfil inválidos");
            }

            _socialPetsDB.PerfilesDb.Add(perfil);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPerfil), new { id = perfil.id_perfil }, perfil);
        }

        // Obtener un perfil por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Perfil encontrado exitosamente", typeof(Perfil))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Perfil no encontrado")]
        public async Task<ActionResult<Perfil>> GetPerfil(int id)
        {
            var perfil = await _socialPetsDB.PerfilesDb.FindAsync(id).ConfigureAwait(false);
            if (perfil == null)
            {
                _logger.LogWarning("Perfil con id {PerfilId} no encontrado", id);
                return NotFound("Perfil no encontrado");
            }

            return Ok(perfil);
        }

        // Editar un perfil
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Perfil editado exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Perfil no encontrado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos del perfil inválidos")]
        public async Task<IActionResult> EditPerfil(int id, [FromBody] Perfil perfil)
        {
            if (id != perfil.id_perfil)
            {
                _logger.LogWarning("El id del perfil en la URL no coincide con el id del perfil en el cuerpo de la solicitud");
                return BadRequest("Datos del perfil inválidos");
            }

            var existingPerfil = await _socialPetsDB.PerfilesDb.FindAsync(id).ConfigureAwait(false);
            if (existingPerfil == null)
            {
                _logger.LogWarning("Perfil con id {PerfilId} no encontrado", id);
                return NotFound("Perfil no encontrado");
            }

            existingPerfil.biografia = perfil.biografia;
            existingPerfil.ciudad_id_ciudad = perfil.ciudad_id_ciudad;
            existingPerfil.correo_electronico_id = perfil.correo_electronico_id;
            existingPerfil.dia_cumpleaños = perfil.dia_cumpleaños;
            existingPerfil.direccion = perfil.direccion;
            existingPerfil.genero = perfil.genero;
            existingPerfil.imagen_perfil = perfil.imagen_perfil;
            existingPerfil.imagen_portada = perfil.imagen_portada;
            existingPerfil.me_gusta = perfil.me_gusta;
            existingPerfil.nivel_id_nivel = perfil.nivel_id_nivel;
            existingPerfil.no_me_gusta = perfil.no_me_gusta;
            existingPerfil.numero_telefono = perfil.numero_telefono;
            existingPerfil.sentimiento_id_sentimiento = perfil.sentimiento_id_sentimiento;
            existingPerfil.titulo = perfil.titulo;
            existingPerfil.usuario_id_usuario = perfil.usuario_id_usuario;

            await _socialPetsDB.SaveChangesAsync();

            return Ok("Perfil editado exitosamente");
        }

        // Eliminar un perfil
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Perfil eliminado exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Perfil no encontrado")]
        public async Task<IActionResult> DeletePerfil(int id)
        {
            var perfil = await _socialPetsDB.PerfilesDb.FindAsync(id).ConfigureAwait(false);
            if (perfil == null)
            {
                _logger.LogWarning("Perfil con id {PerfilId} no encontrado", id);
                return NotFound("Perfil no encontrado");
            }

            _socialPetsDB.PerfilesDb.Remove(perfil);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Perfil eliminado exitosamente");
        }
    }
}
