using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace SocialPets.Models
{
    public class Ciudad
    {
        [Key]
        public int id_ciudad { get; set; }

        public string name { get; set; }

        public string prefijo { get; set; }

    }
}