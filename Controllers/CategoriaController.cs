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
    public class CategoriaController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(SocialPetsDB socialPetsDB, ILogger<CategoriaController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }

        // Crear una nueva categoría
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.Created, "Categoría creada exitosamente", typeof(Categoria))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la categoría inválidos")]
        public async Task<ActionResult<Categoria>> CreateCategoria([FromBody] Categoria categoria)
        {
            if (categoria == null)
            {
                _logger.LogWarning("Se intentó crear una categoría nula");
                return BadRequest("Datos de la categoría inválidos");
            }

            _socialPetsDB.CategoriaDb.Add(categoria);
            await _socialPetsDB.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.id_categoria }, categoria);
        }

        // Obtener una categoría por ID
        [HttpGet("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Categoría encontrada exitosamente", typeof(Categoria))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Categoría no encontrada")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _socialPetsDB.CategoriaDb.FindAsync(id).ConfigureAwait(false);
            if (categoria == null)
            {
                _logger.LogWarning("Categoría con id {CategoriaId} no encontrada", id);
                return NotFound("Categoría no encontrada");
            }

            return Ok(categoria);
        }

        // Editar una categoría
        [HttpPut("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Categoría editada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Categoría no encontrada")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Datos de la categoría inválidos")]
        public async Task<IActionResult> EditCategoria(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.id_categoria)
            {
                _logger.LogWarning("El id de la categoría en la URL no coincide con el id de la categoría en el cuerpo de la solicitud");
                return BadRequest("Datos de la categoría inválidos");
            }

            var existingCategoria = await _socialPetsDB.CategoriaDb.FindAsync(id).ConfigureAwait(false);
            if (existingCategoria == null)
            {
                _logger.LogWarning("Categoría con id {CategoriaId} no encontrada", id);
                return NotFound("Categoría no encontrada");
            }

            existingCategoria.nombre = categoria.nombre;
            existingCategoria.publicaciones_id_publicaciones = categoria.publicaciones_id_publicaciones;

            await _socialPetsDB.SaveChangesAsync();

            return Ok("Categoría editada exitosamente");
        }

        // Eliminar una categoría
        [HttpDelete("{id}")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Categoría eliminada exitosamente")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Categoría no encontrada")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _socialPetsDB.CategoriaDb.FindAsync(id).ConfigureAwait(false);
            if (categoria == null)
            {
                _logger.LogWarning("Categoría con id {CategoriaId} no encontrada", id);
                return NotFound("Categoría no encontrada");
            }

            _socialPetsDB.CategoriaDb.Remove(categoria);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Categoría eliminada exitosamente");
        }
    }
}
