using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialPets.DBConnection;
using SocialPets.Models;
using System.Net;
using System.Threading.Tasks;

namespace SocialPets.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MensageController : ControllerBase
    {
        private readonly SocialPetsDB _socialPetsDB;
        private readonly ILogger<MensageController> _logger;

        public MensageController(SocialPetsDB socialPetsDB, ILogger<MensageController> logger)
        {
            _socialPetsDB = socialPetsDB;
            _logger = logger;
        }
        //Obtener mensaje por ID
        [HttpGet]
        public async Task<ActionResult<Mensage>> Get([FromQuery] int id)
        {
            Mensage mensage = await _socialPetsDB.MensageDb.FindAsync(id).ConfigureAwait(false);

            if (mensage == null)
            {
                return NotFound();
            }

            return Ok(mensage);
        }
        //Obtener todos los mensajes

        [HttpGet("Full")]
        public async Task<ActionResult<IEnumerable<Mensage>>> GetAll([FromQuery] int id_conversacion)
        {
            var mensages = await _socialPetsDB.MensageDb.Where(x => x.conversacion_id_conversacion == id_conversacion).ToListAsync();

            if (mensages == null)
            {
                return NotFound();
            }

            return Ok(mensages);
        }
        //Crear un mensaje
        [HttpPost]
        public async Task<ActionResult<Mensage>> Create([FromBody] Mensage mensage)
        {
            if (mensage == null)
            {
                return BadRequest();
            }            

            _socialPetsDB.MensageDb.Add(mensage);
            await _socialPetsDB.SaveChangesAsync();

            return Ok("Mensaje enviado");
        }
    }
}
