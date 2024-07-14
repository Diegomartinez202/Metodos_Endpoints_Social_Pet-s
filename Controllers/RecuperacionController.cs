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
    public class RecuperacionController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<RecuperacionController> _logger;

        public RecuperacionController(SocialPetsDB socialPetsDB, ILogger<RecuperacionController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }

        // Crear una nueva solicitud de recuperaci�n
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Solicitud de recuperaci�n creada exitosamente", typeof(Recuperacion))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la solicitud inv�lidos")]
        public async Task<ActionResult<Recuperacion>> CreateRecuperacion([FromBody] Recuperacion recuperacion)
        {
            if (recuperacion == null)
            {
                _logger.LogWarning("Se intent� crear una solicitud de recuperaci�n nula");
                return BadRequest("Datos de la solicitud inv�lidos");
            }

            _socialPetsDB.RecuperacionDb.Add(recuperacion);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecuperacion), new { id = recuperacion.id_recuperacion }, recuperacion);
        }

        // Obtener una solicitud de recuperaci�n por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Solicitud de recuperaci�n encontrada exitosamente", typeof(Recuperacion))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Solicitud de recuperaci�n no encontrada")]
        public async Task<ActionResult<Recuperacion>> GetRecuperacion(int id)
        {
            var recuperacion = await _socialPetsDB.RecuperacionDb.FindAsync(id).ConfigureAwait(false);
            if (recuperacion == null)
            {
                _logger.LogWarning("Solicitud de recuperaci�n con id {Id} no encontrada", id);
                return NotFound("Solicitud no encontrada");
            }

            return Ok(recuperacion);
        }

        // Editar una solicitud de recuperaci�n existente
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Solicitud de recuperaci�n editada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Solicitud de recuperaci�n no encontrada")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la solicitud inv�lidos")]
        public async Task<IActionResult> EditRecuperacion(int id, [FromBody] Recuperacion recuperacion)
        {
            if (id != recuperacion.id_recuperacion)
            {
                _logger.LogWarning("El id de la solicitud en la URL no coincide con el id de la solicitud en el cuerpo de la solicitud");
                return BadRequest("Datos de la solicitud inv�lidos");
            }

            var existingRecuperacion = await _socialPetsDB.RecuperacionDb.FindAsync(id).ConfigureAwait(false);
            if (existingRecuperacion == null)
            {
                _logger.LogWarning("Solicitud de recuperaci�n con id {Id} no encontrada", id);
                return NotFound("Solicitud no encontrada");
            }

            existingRecuperacion.codigo = recuperacion.codigo;
            existingRecuperacion.creado_en = recuperacion.creado_en;
            existingRecuperacion.en_uso = recuperacion.en_uso;
            existingRecuperacion.usuario_id_usuario = recuperacion.usuario_id_usuario;

            await _socialPetsDB.SaveChangesAsync();

            return Ok("Solicitud de recuperaci�n editada exitosamente");
        }

        // Eliminar una solicitud de recuperaci�n por ID
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Solicitud de recuperaci�n eliminada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Solicitud de recuperaci�n no encontrada")]
        public async Task<IActionResult> DeleteRecuperacion(int id)
        {
            var recuperacion = await _socialPetsDB.RecuperacionDb.FindAsync(id).ConfigureAwait(false);
            if (recuperacion == null)
            {
                _logger.LogWarning("Solicitud de recuperaci�n con id {Id} no encontrada", id);
                return NotFound("Solicitud no encontrada");
            }

            _socialPetsDB.RecuperacionDb.Remove(recuperacion);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Solicitud de recuperaci�n eliminada exitosamente");
        }

        // Obtener todas las solicitudes de recuperaci�n
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Solicitudes de recuperaci�n encontradas exitosamente", typeof(IEnumerable<Recuperacion>))]
        public async Task<ActionResult<IEnumerable<Recuperacion>>> GetAllRecuperaciones()
        {
            var recuperaciones = await _socialPetsDB.RecuperacionDb.ToListAsync();

            return Ok(recuperaciones);
        }
    }
}
