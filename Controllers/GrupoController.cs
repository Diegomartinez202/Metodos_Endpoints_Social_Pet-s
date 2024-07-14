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
    public class GrupoController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<GrupoController> _logger;

        public GrupoController(SocialPetsDB socialPetsDB, ILogger<GrupoController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }

        // Crear un nuevo grupo
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Grupo creado exitosamente", typeof(Grupo))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos del grupo inválidos")]
        public async Task<ActionResult<Grupo>> CreateGrupo([FromBody] Grupo grupo)
        {
            if (grupo == null)
            {
                _logger.LogWarning("Se intentó crear un grupo nulo");
                return BadRequest("Datos del grupo inválidos");
            }

            grupo.creado_en = DateTime.UtcNow;
            _socialPetsDB.GrupoDb.Add(grupo);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGrupo), new { id = grupo.id_grupo }, grupo);
        }

        // Obtener un grupo por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Grupo encontrado exitosamente", typeof(Grupo))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Grupo no encontrado")]
        public async Task<ActionResult<Grupo>> GetGrupo(int id)
        {
            var grupo = await _socialPetsDB.GrupoDb.FindAsync(id).ConfigureAwait(false);
            if (grupo == null)
            {
                _logger.LogWarning("Grupo con id {GrupoId} no encontrado", id);
                return NotFound("Grupo no encontrado");
            }

            return Ok(grupo);
        }

        // Editar un grupo
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Grupo editado exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Grupo no encontrado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos del grupo inválidos")]
        public async Task<IActionResult> EditGrupo(int id, [FromBody] Grupo grupo)
        {
            if (id != grupo.id_grupo)
            {
                _logger.LogWarning("El id del grupo en la URL no coincide con el id del grupo en el cuerpo de la solicitud");
                return BadRequest("Datos del grupo inválidos");
            }
            var existingGrupo = await _socialPetsDB.GrupoDb.FindAsync(id).ConfigureAwait(false);
            if (existingGrupo == null)
            {
                _logger.LogWarning("Grupo con id {GrupoId} no encontrado", id);
                return NotFound("Grupo no encontrado");
            }
            existingGrupo.descripcion = grupo.descripcion;
            existingGrupo.estado = grupo.estado;
            existingGrupo.imagen = grupo.imagen;
            existingGrupo.titulo = grupo.titulo;
            existingGrupo.usuario_id_usuario = grupo.usuario_id_usuario;

            await _socialPetsDB.SaveChangesAsync();

            return Ok("Grupo editado exitosamente");
        }

        // Eliminar un grupo
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Grupo eliminado exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Grupo no encontrado")]
        public async Task<IActionResult> DeleteGrupo(int id)
        {
            var grupo = await _socialPetsDB.GrupoDb.FindAsync(id).ConfigureAwait(false);
            if (grupo == null)
            {
                _logger.LogWarning("Grupo con id {GrupoId} no encontrado", id);
                return NotFound("Grupo no encontrado");
            }

            _socialPetsDB.GrupoDb.Remove(grupo);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Grupo eliminado exitosamente");
        }
    }
}
