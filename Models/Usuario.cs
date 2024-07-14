using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SocialPets.Models
{
    [XmlSerializerAssembly]
    public class Usuario
    {
        [Key]
        public int id_usuario { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string nombre_usuario { get; set; }

        public string correo_electronico { get; set; }

        public string contrasena_hash { get; set; }

        public bool activo { get; set; }


        public DateTime creado_en { get; set; }

    }
}
