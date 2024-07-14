using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SocialPets.Models
{
    public class Recuperacion
    {

        public string codigo { get; set; }

        public DateTime creado_en { get; set; }

        public bool en_uso { get; set; }

        [Key]
        public int id_recuperacion { get; set; }

        public int usuario_id_usuario { get; set; }

        
    }
}