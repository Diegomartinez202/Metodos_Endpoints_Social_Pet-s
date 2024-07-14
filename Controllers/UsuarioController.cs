using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MySqlX.XDevAPI.Common;
using SocialPets.DBConnection;
using SocialPets.Models;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SocialPets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<UsuarioController>_logger;

        public UsuarioController(SocialPetsDB socialPetsDB, ILogger<UsuarioController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }

        //Obtener usuario

        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Successfully found the person", typeof(Usuario))]
        public async Task<ActionResult<Usuario>> Get([FromQuery] int id)
        {
            Usuario usuario = await _socialPetsDB.UsuarioDb.FindAsync(id).ConfigureAwait(false);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }
        //Create usuario
        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUser([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest();
            }

            var usuario1 = await _socialPetsDB.UsuarioDb.Where(x => x.contrasena_hash == usuario.contrasena_hash
                                                                                && x.correo_electronico == usuario.correo_electronico)
                                                                                .FirstOrDefaultAsync();


            if (usuario1 != null)
            {
                return Conflict("Usuario ya existe en la base de datos");
            }

            _socialPetsDB.UsuarioDb.Add(usuario);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Usuario creado exitosamente");
        }
        //Eliminar usuario
        [HttpDelete]
        public async Task<ActionResult<Usuario>> Delete([FromQuery] int id)
        {
            Usuario usuario = await _socialPetsDB.UsuarioDb.FindAsync(id)
                .ConfigureAwait(false);

            if (usuario == null)
            {
                return NotFound();
            }

            _socialPetsDB.UsuarioDb.Remove(usuario);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Usuario eliminado exitosamente");

        }

       
        //Cambiar contraseña usuario
        [HttpPut]
        public async Task<ActionResult<Usuario>> ChangePass([FromQuery] int id, string pass)
        {
            Usuario usuario = await _socialPetsDB.UsuarioDb.FindAsync(id)
               .ConfigureAwait(false);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.contrasena_hash = pass;

            await _socialPetsDB.SaveChangesAsync();

            return Ok("Contraseña Actualizada Correctamente");
        }
       

        //Obtener todos los usuarios

        [HttpGet("Todos")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAll()
        {
            var usuarios = await _socialPetsDB.UsuarioDb.Where(x => x.id_usuario != null).ToListAsync();


            return usuarios;
        }

        [HttpPost("Signing")]
        public async Task<ActionResult<Usuario>> Signing([FromQuery] string password, string correo) 
        {
            if (String.IsNullOrEmpty(correo) || String.IsNullOrEmpty(password))
            {
                return BadRequest("Diligencie de forma correcta los campos del formulario");
            }

            var usuario = _socialPetsDB.UsuarioDb.Where(x => x.contrasena_hash == password && x.correo_electronico == correo).FirstOrDefault();

            if(usuario == null)
            {
                return NotFound("Usuario no existe con el correo electronico y contraseña ingresados");
            }

            return Ok("Ha ingresado con exito");
        }
    }
}
