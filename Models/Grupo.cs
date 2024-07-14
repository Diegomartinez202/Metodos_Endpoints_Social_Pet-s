using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SocialPets.Models
{
    public class Grupo
    {
        public DateTime creado_en { get; set; }

        public string descripcion { get; set; }

        public int estado { get; set; }
        [Key]
        public int id_grupo { get; set; }

        public string imagen { get; set; }

        public string titulo { get; set; }

        public int usuario_id_usuario { get; set; }

    }
}