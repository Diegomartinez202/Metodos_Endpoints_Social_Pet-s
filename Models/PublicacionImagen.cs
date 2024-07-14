using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SocialPets.Models
{
    public class PublicacionImagen
    {
        [Key]
        public int id_publicacion_imagen { get; set; }

        public int imagen_id_imagen { get; set; }

        public int publicacion_id_publicacion { get; set; }

    }
}