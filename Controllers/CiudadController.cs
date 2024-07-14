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
    public class CiudadController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<CiudadController> _logger;

        public CiudadController(SocialPetsDB socialPetsDB, ILogger<CiudadController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }

        // Crear una nueva ciudad
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Ciudad creada exitosamente", typeof(Ciudad))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la ciudad inválidos")]
        public async Task<ActionResult<Ciudad>> CreateCiudad([FromBody] Ciudad ciudad)
        {
            if (ciudad == null)
            {
                _logger.LogWarning("Se intentó crear una ciudad nula");
                return BadRequest("Datos de la ciudad inválidos");
            }

            _socialPetsDB.CiudadDb.Add(ciudad);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCiudad), new { id = ciudad.id_ciudad }, ciudad);
        }

        // Obtener una ciudad por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Ciudad encontrada exitosamente", typeof(Ciudad))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Ciudad no encontrada")]
        public async Task<ActionResult<Ciudad>> GetCiudad(int id)
        {
            var ciudad = await _socialPetsDB.CiudadDb.FindAsync(id).ConfigureAwait(false);
            if (ciudad == null)
            {
                _logger.LogWarning("Ciudad con id {CiudadId} no encontrada", id);
                return NotFound("Ciudad no encontrada");
            }

            return Ok(ciudad);
        }

        // Editar una ciudad
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Ciudad editada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Ciudad no encontrada")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la ciudad inválidos")]
        public async Task<IActionResult> EditCiudad(int id, [FromBody] Ciudad ciudad)
        {
            if (id != ciudad.id_ciudad)
            {
                _logger.LogWarning("El id de la ciudad en la URL no coincide con el id de la ciudad en el cuerpo de la solicitud");
                return BadRequest("Datos de la ciudad inválidos");
            }

            var existingCiudad = await _socialPetsDB.CiudadDb.FindAsync(id).ConfigureAwait(false);
            if (existingCiudad == null)
            {
                _logger.LogWarning("Ciudad con id {CiudadId} no encontrada", id);
                return NotFound("Ciudad no encontrada");
            }

            existingCiudad.name = ciudad.name;
            existingCiudad.prefijo = ciudad.prefijo;

            await _socialPetsDB.SaveChangesAsync();

            return Ok("Ciudad editada exitosamente");
        }

        // Eliminar una ciudad
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Ciudad eliminada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Ciudad no encontrada")]
        public async Task<IActionResult> DeleteCiudad(int id)
        {
            var ciudad = await _socialPetsDB.CiudadDb.FindAsync(id).ConfigureAwait(false);
            if (ciudad == null)
            {
                _logger.LogWarning("Ciudad con id {CiudadId} no encontrada", id);
                return NotFound("Ciudad no encontrada");
            }

            _socialPetsDB.CiudadDb.Remove(ciudad);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Ciudad eliminada exitosamente");
        }
    }
}
