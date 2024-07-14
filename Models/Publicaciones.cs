using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SocialPets.Models
{
    public class Publicaciones
    {
        [Key]
        public int id_publicacion { get; set; }

        public string titulo { get; set; }

        public string contenido { get; set; }

        public int latitud { get; set; }

        public int longitud { get; set; }

        public DateTime comenzar_en { get; set; }

        public DateTime finalizado_en { get; set; }

        public int tipo_receptor_id { get; set; }

        public int referencia_autor_id { get; set; }

        public int referencia_receptor_id { get; set; }

        public int nivel_id_nivel { get; set; }

        public int tipo_publicacion_id { get; set; }

        public DateTime creado_en { get; set; }

        public string imagen { get; set; }
    }
}
