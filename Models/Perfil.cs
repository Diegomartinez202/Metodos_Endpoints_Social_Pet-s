using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
namespace SocialPets.Models
{
    public class Perfil
    {
        public string biografia { get; set; }

        public int ciudad_id_ciudad { get; set; }
        
        public bool correo_electronico_id { get; set; }

        public DateTime dia_cumpleaños { get; set; }

        public string direccion { get; set; }

        public string genero { get; set; }

        [Key]
        public int id_perfil { get; set; }

        public string imagen_perfil { get; set; }

        public string imagen_portada { get; set; }

        public string me_gusta { get; set; }

        public int nivel_id_nivel { get; set; }

        public string no_me_gusta { get; set; }

        public int numero_telefono { get; set; }

        public int sentimiento_id_sentimiento { get; set; }

        public string titulo { get; set; }

        public int usuario_id_usuario { get; set; }

    }
}