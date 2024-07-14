using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SocialPets.Models
{
    [XmlSerializerAssembly]
    public class Nivel
        
    {

        [Key]

        public int id_nivel { get; set; }

        public string name { get; set; }

    }
}