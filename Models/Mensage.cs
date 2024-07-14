using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SocialPets.Models
{
    [XmlSerializerAssembly]
    public class Mensage
    {
        [Key]
        public int id_mensage { get; set; }

        public string contenido { get; set; }

        public int usuario_id_usuario { get; set; }

        public int conversacion_id_conversacion { get; set; }

        public DateTime creado_en { get; set; }

        public bool leido { get; set; }
    }
}
