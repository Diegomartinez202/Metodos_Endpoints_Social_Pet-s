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
    public class NivelController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<NivelController> _logger;

        public NivelController(SocialPetsDB socialPetsDB, ILogger<NivelController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }

        // Crear un nuevo nivel
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Nivel creado exitosamente", typeof(Nivel))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos del nivel inválidos")]
        public async Task<ActionResult<Nivel>> CreateNivel([FromBody] Nivel nivel)
        {
            if (nivel == null)
            {
                _logger.LogWarning("Se intentó crear un nivel nulo");
                return BadRequest("Datos del nivel inválidos");
            }

            _socialPetsDB.NivelDb.Add(nivel);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNivel), new { id = nivel.id_nivel }, nivel);
        }

        // Obtener un nivel por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Nivel encontrado exitosamente", typeof(Nivel))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Nivel no encontrado")]
        public async Task<ActionResult<Nivel>> GetNivel(int id)
        {
            var nivel = await _socialPetsDB.NivelDb.FindAsync(id).ConfigureAwait(false);
            if (nivel == null)
            {
                _logger.LogWarning("Nivel con id {NivelId} no encontrado", id);
                return NotFound("Nivel no encontrado");
            }

            return Ok(nivel);
        }

        // Editar un nivel
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Nivel editado exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Nivel no encontrado")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos del nivel inválidos")]
        public async Task<IActionResult> EditNivel(int id, [FromBody] Nivel nivel)
        {
            if (id != nivel.id_nivel)
            {
                _logger.LogWarning("El id del nivel en la URL no coincide con el id del nivel en el cuerpo de la solicitud");
                return BadRequest("Datos del nivel inválidos");
            }

            var existingNivel = await _socialPetsDB.NivelDb.FindAsync(id).ConfigureAwait(false);
            if (existingNivel == null)
            {
                _logger.LogWarning("Nivel con id {NivelId} no encontrado", id);
                return NotFound("Nivel no encontrado");
            }

            existingNivel.name = nivel.name;

            await _socialPetsDB.SaveChangesAsync();

            return Ok("Nivel editado exitosamente");
        }

        // Eliminar un nivel
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Nivel eliminado exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Nivel no encontrado")]
        public async Task<IActionResult> DeleteNivel(int id)
        {
            var nivel = await _socialPetsDB.NivelDb.FindAsync(id).ConfigureAwait(false);
            if (nivel == null)
            {
                _logger.LogWarning("Nivel con id {NivelId} no encontrado", id);
                return NotFound("Nivel no encontrado");
            }

            _socialPetsDB.NivelDb.Remove(nivel);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Nivel eliminado exitosamente");
        }
    }
}
