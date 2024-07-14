using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SocialPets.Models
{
    public class Conversacion
    {
        [Key]
        public int conversacion_id { get; set; }

        public DateTime creado_en { get; set; }

        public int usuario_id_usuario { get; set; }
        public int usuario_id_recibe { get; set; }
    }
}
