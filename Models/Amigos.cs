using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SocialPets.Models
{
    public class Amigos
    {
        [Key]
        public bool aceptado { get; set; }

        public DateTime creado_en { get; set; }

        public int id_amigos { get; set; }

        public bool leido { get; set; }

        public int perfil_id_perfil { get; set; }

        public int usuario_id_usuario { get; set; }

        
    }
}
