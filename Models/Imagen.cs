using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SocialPets.Models
{
    public class Imagen
    {
        
        public int album_id_album{ get; set; }

        public string contenido { get; set; }

        public DateTime creado_en { get; set; }

        public string fuente { get; set; }

        [Key]

        public int id_imagen { get; set; }

        public int nivel_id_nivel { get; set; }

        public string titulo { get; set; }

        public int usuario_id_usuario { get; set; }

    }
}